USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepass_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Gatepass 
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepass_GetAll]
(
 @UserAccountId  int = NULL,
 @PageNumber     int = 1,
 @PageSize       int = 50,
 
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
	
	SET @Tables = ' Managements.Gatepass 
	                    INNER JOIN Employees.Employee
	                 ON Managements.Gatepass.EmployeeId = Employees.Employee.EmployeeId
	                    INNER JOIN Managements.GatepassType
	                 ON Managements.Gatepass.GatepassTypeId = Managements.GatepassType.GatepassTypeId                 
	                    INNER JOIN #Departments
	                 ON Employees.Employee.DepartmentId = #Departments.DepartmentId '
	
	SET @Fields = ' Managements.Gatepass.GatepassId, 
	               Managements.Gatepass.EmployeeId, 
	               Managements.Gatepass.GatepassBegDate, 
	               Managements.Gatepass.GatepassEndDate, 
	               Managements.Gatepass.GatepassBegTime, 
	               Managements.Gatepass.GatepassEndTime, 
	               Managements.GatepassType.GatepassTypeName,
	               Managements.Gatepass.Notes, 
	               Managements.Gatepass.VersionNo, 
	               Employees.Employee.EmployeeName,
	               #Departments.DepartmentName,
	               dbo.FnUmAlquraYMD(Managements.Gatepass.GatepassBegDate) AS UmBegDate,
	               dbo.FnUmAlquraYMD(Managements.Gatepass.GatepassEndDate) AS UmEndDate ' 		 	
     		     		      		  	               
	SET @strFilter = ''
	SET @strGroup = ''
	SET @Sort = 'Managements.Gatepass.GatepassBegDate DESC '

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

          -- Set Total Pages and Page No
		SET @PagesTotal = (SELECT COUNT(Managements.Gatepass.GatepassId)
                             FROM Managements.Gatepass 
                                  INNER JOIN Employees.Employee 
                               ON Managements.Gatepass.EmployeeId = Employees.Employee.EmployeeId 
                                  INNER JOIN Employees.Department 
                               ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId   		     		      
		                          INNER JOIN #Departments
		                       ON Employees.Employee.DepartmentId = #Departments.DepartmentId)
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
