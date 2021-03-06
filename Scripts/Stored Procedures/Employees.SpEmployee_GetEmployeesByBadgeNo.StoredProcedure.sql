USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_GetEmployeesByBadgeNo]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Employees by Badge No
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_GetEmployeesByBadgeNo]
(
 @UserAccountId  int = NULL,
 @BadgeNo        nvarchar(50) = NULL,
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

        
    DECLARE @DepartmentId int
    DECLARE @EmployeeId int
    
    -- Initializations              
    SET @RowEffected = 0
    

    IF @UserAccountId=0
	BEGIN
	SELECT Employees.Employee.EmployeeId, 
			 Employees.Employee.FirstName + '  ' + Employees.Employee.MiddleName + '  ' + Employees.Employee.LastName As EmployeeName, 
			 Employees.Employee.BadgeNo, 
			 Employees.Department.DepartmentName,
			 Employees.Employee.DepartmentId, 
			 Employees.PositionType.PositionTypeName, 
			 Employees.Employee.JobTitle,
			 Employees.Position.PositionName, 
			 Employees.Nationality.NationalityName, 
			 Employees.Employee.Picture, 			 
			 Common.FnToUmAlqura(Employees.Employee.HireDate, 2) AS UmHireDate,
			 CASE  Employees.Employee.IsFingerRegistered WHEN 0 THEN 'لا' WHEN 1 THEN 'نعم' END
		FROM Employees.Employee INNER JOIN Employees.Department 
		  ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId 
			 
			 INNER JOIN Employees.Position 
		  ON Employees.Employee.PositionId = Employees.Position.PositionId 
			 INNER JOIN Employees.PositionType 
		  ON Employees.Employee.PositionTypeId = Employees.PositionType.PositionTypeId 
			 INNER JOIN Employees.Nationality 
		  ON Employees.Employee.NationalityId = Employees.Nationality.NationalityId   		     		      
       WHERE Employees.Employee.BadgeNo = @BadgeNo

	   RETURN
	END
                            
    SET @DepartmentId = (SELECT DepartmentId
                           FROM Employees.EmployeeDepartmentLink
                          WHERE EmployeeId = @EmployeeId)


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
		 WHERE DepartmentId in(SELECT DepartmentId FROM Employees.EmployeeDepartmentLink WHERE Employeeid =@UserAccountId)
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
		          
		SELECT Employees.Employee.EmployeeId, 
			 Employees.Employee.FirstName + '  ' + Employees.Employee.MiddleName + '  ' + Employees.Employee.LastName As EmployeeName, 
			 Employees.Employee.BadgeNo, 
			 Employees.Department.DepartmentName,
			 Employees.Employee.DepartmentId, 
			 Employees.PositionType.PositionTypeName, 
			 Employees.Employee.JobTitle,
			 Employees.Position.PositionName, 
			 Employees.Nationality.NationalityName, 
			 Employees.Employee.Picture, 			 
			 Common.FnToUmAlqura(Employees.Employee.HireDate, 2) AS UmHireDate,
			 CASE  Employees.Employee.IsFingerRegistered WHEN 0 THEN 'لا' WHEN 1 THEN 'نعم' END
		FROM Employees.Employee INNER JOIN Employees.Department 
		  ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId 
			 
			 INNER JOIN Employees.Position 
		  ON Employees.Employee.PositionId = Employees.Position.PositionId 
			 INNER JOIN Employees.PositionType 
		  ON Employees.Employee.PositionTypeId = Employees.PositionType.PositionTypeId 
			 INNER JOIN Employees.Nationality 
		  ON Employees.Employee.NationalityId = Employees.Nationality.NationalityId   		     		      
		     INNER JOIN #Departments
		  ON Employees.Employee.DepartmentId = #Departments.DepartmentId    
       WHERE Employees.Employee.BadgeNo = @BadgeNo
       

		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
