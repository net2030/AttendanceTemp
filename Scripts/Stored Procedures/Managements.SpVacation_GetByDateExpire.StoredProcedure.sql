USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_GetByDateExpire]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Vacations 
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_GetByDateExpire]
(
 @UserAccountId  int = NULL,
 @BegDate        date = NULL, 
 @EndDate        date = NULL, 
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
    DECLARE @uEmployeeId int
        
    -- Initializations              
    SET @RowEffected = 0
    
    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END
        
    SET @uEmployeeId = (SELECT EmployeeId 
                         FROM Users.UserAccount
                        WHERE UserAccountId = @UserAccountId)
                                                    
    SET @DepartmentId = (SELECT DepartmentId
                           FROM Employees.EmployeeDepartmentLink
                          WHERE EmployeeId = @uEmployeeId)
                          
	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	SET @Tables = ' Managements.Vacation INNER JOIN Managements.VacationType  
	                ON Managements.Vacation.TypeId = Managements.VacationType.TypeId
	                INNER JOIN Employees.Employee
	                ON Managements.Vacation.EmployeeId = Employees.Employee.EmployeeId
	                INNER JOIN Employees.Department
	                ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
	                INNER JOIN #Departments 
	                ON Employees.Employee.DepartmentId = #Departments.DepartmentId '
                                                                                                                                                                                                              	
	SET @Fields = 'Managements.Vacation.VacationId, 
	               Managements.Vacation.EmployeeId,  
	               Managements.Vacation.TypeId,  
	               Managements.Vacation.EffectiveDate, 
	               Managements.Vacation.DateExpire,  
	               Managements.Vacation.DateOfReturn,
	               Managements.Vacation.VersionNo, 
	               Managements.VacationType.TypeName, 
	               Employees.Employee.EmployeeName, 
	               Employees.Department.DepartmentName,
	               dbo.FnUmAlquraYMD(Managements.Vacation.EffectiveDate) AS UmEffectiveDate,
	               dbo.FnUmAlquraYMD(Managements.Vacation.DateExpire) AS UmExpireDate, 
	               dbo.FnUmAlquraYMD(Managements.Vacation.DateOfReturn) AS UmDateOfReturn, 
	               DATEDIFF(dd, Managements.Vacation.EffectiveDate, Managements.Vacation.DateExpire) + 1 AS VacationDays ' 		 	

                           		     		      		  	               
	SET @strFilter = 'WHERE Managements.Vacation.DateExpire >= ' + '''' + CAST(@BegDate as nvarchar(10)) + '''' + ' AND Managements.Vacation.EffectiveDate <= ' + '''' + CAST(@EndDate as nvarchar(10)) + '''' 
	                 
	SET @strGroup = ''
	SET @Sort = 'Managements.Vacation.VacationId '

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
		SET @PagesTotal = (SELECT COUNT(Managements.Vacation.VacationId)
                             FROM Managements.Vacation INNER JOIN Managements.VacationType  
	                           ON Managements.Vacation.TypeId = Managements.VacationType.TypeId
	                              INNER JOIN Employees.Employee
	                           ON Managements.Vacation.EmployeeId = Employees.Employee.EmployeeId
	                              INNER JOIN Employees.Department
	                           ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
	                              INNER JOIN #Departments 
	                           ON Employees.Employee.DepartmentId = #Departments.DepartmentId
	                        WHERE Managements.Vacation.DateExpire >= @BegDate
	                          AND Managements.Vacation.DateExpire <= @EndDate)	                           
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
