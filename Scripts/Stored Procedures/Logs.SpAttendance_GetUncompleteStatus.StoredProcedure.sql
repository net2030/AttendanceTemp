USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendance_GetUncompleteStatus]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Uncomplete status
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendance_GetUncompleteStatus]
(
 @UserAccountId  int = NULL,
 @PageNumber     int = 1,
 @PageSize       int = 50,
 @FilterOptionNo int = 1,
 @SortOptionNo int = 1, 
 
 @PagesTotal     int = 0 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected  int
    DECLARE @strPageSize  nvarchar(50)
    DECLARE @strStartRow  nvarchar(50)
    DECLARE @strFilter    nvarchar(max)
    DECLARE @strGroup     nvarchar(max)
    DECLARE @Group        nvarchar(max)
    DECLARE @Tables       nvarchar(max)
    DECLARE @Fields       nvarchar(max) 
    DECLARE @Filter       nvarchar(max)  
    DECLARE @Sort         nvarchar(500)    
    
    DECLARE @DepartmentId int
    DECLARE @EmployeeId int
        
    -- Initializations              
    SET @RowEffected = 0
    
    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END
        
    SET @EmployeeId = (SELECT EmployeeId 
                         FROM Users.UserAccount
                        WHERE UserAccountId = @UserAccountId)
                            
    SET @DepartmentId = (SELECT DepartmentId
                           FROM Employees.EmployeeDepartmentLink
                          WHERE EmployeeId = @EmployeeId)
                          
	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	SET @Tables = ' Logs.AttendanceLog INNER JOIN Logs.AttendanceStatus 
				ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
				   INNER JOIN Employees.Employee 
				ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
				   INNER JOIN #Departments
				ON Employees.Employee.DepartmentId = #Departments.DepartmentId  
				   INNER JOIN Employees.Position 
				ON Employees.Employee.PositionId = Employees.Position.PositionId '
	
	SET @Fields = 'Logs.AttendanceLog.LogId, 
				   Logs.AttendanceLog.LogDate, 
				   Logs.AttendanceLog.StatusId, 
				   Logs.AttendanceLog.EmployeeId, 
				   Logs.AttendanceLog.InLogId, 
				   Logs.AttendanceLog.InTime, 
				   Logs.AttendanceLog.OutLogId, 
				   Logs.AttendanceLog.OutTime,  
				   Logs.AttendanceStatus.StatusName, 
				   Employees.Employee.EmployeeName, 
				   #Departments.DepartmentName, 
				   dbo.FnUmAlquraYMD(Logs.AttendanceLog.LogDate) AS UmLogDate, 
				   Employees.Position.PositionName ' 		 	
	               
	SET @strFilter = ''
	SET @strGroup = ''	

   /*****************************************************
    Employees filter options
   *****************************************************/ 
    IF @FilterOptionNo  = 1 -- جميع الموظفين
    BEGIN
      SET @strFilter = 'WHERE Employees.Employee.IsActive = 1
						  AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90) '  
    END
    ELSE
    IF @FilterOptionNo  = 2 -- جميع المدنيين
    BEGIN
	  SET @strFilter = ' WHERE Employees.Employee.IsActive = 1
	                       AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90) 
						   AND Employees.Employee.PositionTypeId IN (2, 3) '      
    END
    ELSE
    IF @FilterOptionNo  = 3 -- جميع العسكريين
    BEGIN
      SET @strFilter = ' WHERE Employees.Employee.IsActive = 1
                           AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90)
			               AND Employees.Employee.PositionTypeId = 1 '     
    END
    ELSE
    IF @FilterOptionNo  = 4 -- جميع العسكريين الضباط
    BEGIN
      SET @strFilter = ' WHERE Employees.Employee.IsActive = 1
                           AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90)
			               AND Employees.Employee.PositionTypeId = 1
			               AND Employees.Position.IsOfficer = 1 '    
    END
    ELSE
    IF @FilterOptionNo  = 5 -- جميع العسكريين صف الضباط
    BEGIN
      SET @strFilter = ' WHERE Employees.Employee.IsActive = 1
                           AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90)
			               AND Employees.Employee.PositionTypeId = 1
			               AND Employees.Position.IsOfficer = 0 '     
    END                    

   /*****************************************************
    Employees sort options
   *****************************************************/ 
   IF @SortOptionNo = 1 -- أسم الموظف
   BEGIN
     SET @Sort = 'Employees.Employee.EmployeeName '
   END
   ELSE
   IF @FilterOptionNo  > 1 AND @SortOptionNo = 2 -- الرتبة أو المرتبة
   BEGIN
     SET @Sort = 'Employees.Position.SortIndex '
   END
   ELSE
   IF @SortOptionNo = 3 -- رقم الموظف
   BEGIN
     SET @Sort = 'Employees.Employee.EmployeeId '
   END
   ELSE
   BEGIN
     SET @Sort = 'Employees.Employee.EmployeeName '
   END
         
	-- Create A temporary Cards Result Table	
    CREATE TABLE #Departments
    (
     [DepartmentId]   int, 
     [DepartmentName] nvarchar(100), 
     [ParentId]       int
     )
     
    BEGIN TRY
	    ;WITH CTE(DepartmentId, DepartmentName, ParentId)
		AS
		(
		SELECT DepartmentId, DepartmentName, ParentId
		  From Employees.Department 
		 WHERE DepartmentId = @DepartmentId
		 UNION ALL
		SELECT e.DepartmentId, e.DepartmentName, e.ParentId
		  FROM Employees.Department e
		       INNER JOIN CTE c
		    ON e.ParentId = c.DepartmentId
			)
			                        
		INSERT INTO #Departments
		       (
                [DepartmentId], 
                [DepartmentName], 
                [ParentId] 
		       )
		SELECT  [DepartmentId], 
                [DepartmentName], 
                [ParentId]
           FROM CTE                            		     		      
    

		  --*******************************************************************
		  -- Generate & Execute Dynamic query
		  --*******************************************************************	
		  EXEC(
		  '

		  WITH RowsResult AS 
		  (
		   		SELECT     ROW_NUMBER() OVER ( ORDER BY ' + @Sort+ ') AS RowSerailID,'
		  +@Fields+ ' FROM ' + @Tables + @strFilter + ' ' + @strGroup + ') 
		  SELECT     *
		    FROM   RowsResult
		   WHERE  RowSerailID BETWEEN '+@strStartRow+' AND ('+@strStartRow+'+'+@strPageSize+'-1);'
		  )


		IF @FilterOptionNo  = 1 -- جميع الموظفين
		BEGIN
			  -- Set Total Pages and Page No
			SET @PagesTotal = (SELECT COUNT(*) 
								 FROM Logs.AttendanceLog 
								      INNER JOIN Logs.AttendanceStatus 
								   ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
									  INNER JOIN Employees.Employee 
								   ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
									  INNER JOIN #Departments
								   ON Employees.Employee.DepartmentId = #Departments.DepartmentId  
									  INNER JOIN Employees.Position 
								   ON Employees.Employee.PositionId = Employees.Position.PositionId		
								WHERE Employees.Employee.IsActive = 1
								  AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90))   
		END
		ELSE
		IF @FilterOptionNo  = 2 -- جميع المدنيين
		BEGIN
			  -- Set Total Pages and Page No
			SET @PagesTotal = (SELECT COUNT(*) 
								 FROM Logs.AttendanceLog 
								      INNER JOIN Logs.AttendanceStatus 
								   ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
									  INNER JOIN Employees.Employee 
								   ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
									  INNER JOIN #Departments
								   ON Employees.Employee.DepartmentId = #Departments.DepartmentId  
									  INNER JOIN Employees.Position 
								   ON Employees.Employee.PositionId = Employees.Position.PositionId		
								WHERE Employees.Employee.IsActive = 1
							      AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90) 
							      AND Employees.Employee.PositionTypeId IN (2, 3))		     
		END
		ELSE
		IF @FilterOptionNo  = 3 -- جميع العسكريين
		BEGIN
			  -- Set Total Pages and Page No
			SET @PagesTotal = (SELECT COUNT(*) 
								 FROM Logs.AttendanceLog 
								      INNER JOIN Logs.AttendanceStatus 
								   ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
									  INNER JOIN Employees.Employee 
								   ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
									  INNER JOIN #Departments
								   ON Employees.Employee.DepartmentId = #Departments.DepartmentId  
									  INNER JOIN Employees.Position 
								   ON Employees.Employee.PositionId = Employees.Position.PositionId		
								WHERE Employees.Employee.IsActive = 1
							      AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90)
							      AND Employees.Employee.PositionTypeId = 1)		     
		END
		ELSE
		IF @FilterOptionNo  = 4 -- جميع العسكريين الضباط
		BEGIN
			  -- Set Total Pages and Page No
			SET @PagesTotal = (SELECT COUNT(*) 
								 FROM Logs.AttendanceLog 
								      INNER JOIN Logs.AttendanceStatus 
								   ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
									  INNER JOIN Employees.Employee 
								   ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
									  INNER JOIN #Departments
								   ON Employees.Employee.DepartmentId = #Departments.DepartmentId  
									  INNER JOIN Employees.Position 
								   ON Employees.Employee.PositionId = Employees.Position.PositionId		
								WHERE Employees.Employee.IsActive = 1
							      AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90)
							      AND Employees.Employee.PositionTypeId = 1
							      AND Employees.Position.IsOfficer = 1)		   
		END
		ELSE
		IF @FilterOptionNo  = 5 -- جميع العسكريين صف الضباط
		BEGIN
			  -- Set Total Pages and Page No
			SET @PagesTotal = (SELECT COUNT(*) 
								 FROM Logs.AttendanceLog 
								      INNER JOIN Logs.AttendanceStatus 
								   ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
									  INNER JOIN Employees.Employee 
								   ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
									  INNER JOIN #Departments
								   ON Employees.Employee.DepartmentId = #Departments.DepartmentId  
									  INNER JOIN Employees.Position 
								   ON Employees.Employee.PositionId = Employees.Position.PositionId		
								WHERE Employees.Employee.IsActive = 1
							      AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90)
							      AND Employees.Employee.PositionTypeId = 1
							      AND Employees.Position.IsOfficer = 0)			    
		END                    
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
