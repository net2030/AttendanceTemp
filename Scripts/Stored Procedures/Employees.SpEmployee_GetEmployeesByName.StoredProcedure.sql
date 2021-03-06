USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_GetEmployeesByName]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Employees by name
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_GetEmployeesByName]
(
 @UserAccountId  int = NULL,
 @EmployeeName   nvarchar(100) = NULL,
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
	
	SET @Tables = '#Employees'
	
	SET @Fields = 'EmployeeId, 
	               EmployeeName, 
	               BadgeNo, 
	               DepartmentName, 
	               PositionTypeName, 
	               JobTitleName, 
	               PositionName, 
	               NationalityName, 
	               UmHireDate,
	               FingerRegistered'  		 	
	               
	SET @strFilter = ''
	SET @strGroup = ''
	SET @Sort = 'EmployeeId'

	-- Create A temporary Cards Result Table	
    CREATE TABLE #Employees
    (
     [EmployeeId]         int, 
     [EmployeeName]       nvarchar(100), 
     [BadgeNo]            nvarchar(50), 
     [DepartmentName]     nvarchar(100),      
     [PositionTypeName]   nvarchar(50), 
     [JobTitleName]       nvarchar(50),     
     [PositionName]       nvarchar(50), 
     [NationalityName]    nvarchar(50),   
     [UmHireDate]         nvarchar(10),
     [Picture]            varbinary(max),
     [FingerRegistered]   nvarchar(03)
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
			                        
        INSERT INTO #Employees
        (EmployeeId, 
		 EmployeeName, 
		 BadgeNo, 
		 DepartmentName,      
		 PositionTypeName, 
		 JobTitleName,     
		 PositionName,
		 NationalityName , 
		 Picture,
		 UmHireDate,
		 FingerRegistered)                 
		SELECT Employees.Employee.EmployeeId, 
			 Employees.Employee.EmployeeName, 
			 Employees.Employee.BadgeNo, 
			 Employees.Department.DepartmentName, 
			 Employees.PositionType.PositionTypeName, 
			 Employees.JobTitle.JobTitleName, 
			 Employees.Position.PositionName, 
			 Employees.Nationality.NationalityName, 
			 Employees.Employee.Picture, 			 
			 Common.FnToUmAlqura(Employees.Employee.HireDate, 2) AS UmHireDate,
			 CASE  Employees.Employee.IsFingerRegistered WHEN 0 THEN 'لا' WHEN 1 THEN 'نعم' END 
		FROM Employees.Employee INNER JOIN Employees.Department 
		  ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId 
			 INNER JOIN Employees.JobTitle 
		  ON Employees.Employee.JobTitle = Employees.JobTitle.JobTitleId 
			 INNER JOIN Employees.Position 
		  ON Employees.Employee.PositionId = Employees.Position.PositionId 
			 INNER JOIN Employees.PositionType 
		  ON Employees.Employee.PositionTypeId = Employees.PositionType.PositionTypeId 
			 INNER JOIN Employees.Nationality 
		  ON Employees.Employee.NationalityId = Employees.Nationality.NationalityId   		     		      
		     INNER JOIN CTE
		  ON Employees.Employee.DepartmentId = CTE.DepartmentId    
       WHERE Employees.Employee.EmployeeName LIKE @EmployeeName + '%'
       
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
		SET @PagesTotal = (SELECT COUNT(*) from #Employees)
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END




GO
