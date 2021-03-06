USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_GetByDepartmentAndEmployerAndContractType]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan
-- Create date: 02/06/2016
-- Description:	Get All Employees 
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_GetByDepartmentAndEmployerAndContractType]
(
 @DepartmentId   int = NULL,
 @Employer       int = 1,
 @ContractType   int = 1,
 @Lang         char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
     
    -- Declariations
    
        
   
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
		 WHERE DepartmentId =@DepartmentId
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
    

	      if @lang = 'en'


		    SELECT ROW_NUMBER() OVER ( ORDER BY Employees.Employee.FirstName) AS RowSerailID,
			      Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + '  ' + Employees.Employee.LastName AS Employeename,
			      Employees.Employee.GovId, 
                  Employees.Employee.EmployeeNo, 
				  Employees.Employee.BadgeNo, 
				  Employees.Employer.EmployerNameEN as EmployerName, 
				  Employees.ContractType.ContractTypeNameEn as ContractTypeName, 
                  Employees.Department.DepartmentNameEn as DepartmentName, 
				  Employee_1.FirstName + '  ' + Employee_1.MiddleName + '  ' + Employee_1.LastName AS EmployeeManager, 
                  Users.Role.RoleName, 
				  Employees.Nationality.NationalityName, 
                  CASE Employees.Employee.IsFingerRegistered WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END AS IsFingerRegistered, 
				  Employees.Employee.MobileNo, 
                  CASE Employees.Employee.Sex WHEN 1 THEN 'Male' WHEN 2 THEN 'Female' END AS Sex, 
				  Employees.Employee.UserName, 
				  Employees.Employee.EmailAddress, 
                  Employees.Employee.JobTitle, 
				  Employees.Employee.HireDate, 
				  CASE Employees.Employee.IsActive WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END AS IsActive
         FROM     Employees.Employee left JOIN
                  Employees.Department ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId left JOIN
                  Employees.Employer ON Employees.Employee.Employer = Employees.Employer.EmployerId LEFT JOIN
                  Employees.ContractType ON Employees.Employee.ContractType = Employees.ContractType.ContractTypeId LEFT JOIN
                  Employees.Employee AS Employee_1 ON Employees.Employee.ManagerId = Employee_1.EmployeeId LEFT JOIN
                  Users.Role ON Employees.Employee.RoleId = Users.Role.RoleId LEFT JOIN
                  Employees.Nationality ON Employees.Employee.NationalityId = Employees.Nationality.NationalityId AND 
                  Employees.Employee.NationalityId = Employees.Nationality.NationalityId AND Employee_1.NationalityId = Employees.Nationality.NationalityId AND 
                  Employee_1.NationalityId = Employees.Nationality.NationalityId
				 JOIN #Departments ON Employees.Employee.DepartmentId=#Departments.DepartmentId
          WHERE (Employees.Employee.ContractType=@ContractType AND Employees.Employee.Employer=@Employer ) 
		  OR (Employees.Employee.ContractType=@ContractType AND @Employer=0) 
		  OR (Employees.Employee.Employer=@Employer AND @ContractType=0)
		  OR (@Employer=0 AND @ContractType=0)

		  else

		   SELECT ROW_NUMBER() OVER ( ORDER BY Employees.Employee.FirstName) AS RowSerailID,
			      Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + '  ' + Employees.Employee.LastName AS Employeename,
			      Employees.Employee.GovId, 
                  Employees.Employee.EmployeeNo, 
				  Employees.Employee.BadgeNo, 
				  Employees.Employer.EmployerName, 
				  Employees.ContractType.ContractTypeName, 
                  Employees.Department.DepartmentName, 
				  Employee_1.FirstName + '  ' + Employee_1.MiddleName + '  ' + Employee_1.LastName AS EmployeeManager, 
                  Users.Role.RoleName, 
				  Employees.Nationality.NationalityName, 
                  CASE Employees.Employee.IsFingerRegistered WHEN 0 THEN 'لا' WHEN 1 THEN 'نعم' END AS IsFingerRegistered, 
				  Employees.Employee.MobileNo, 
                  CASE Employees.Employee.Sex WHEN 1 THEN 'ذكر' WHEN 2 THEN 'انثى' END AS Sex, 
				  Employees.Employee.UserName, 
				  Employees.Employee.EmailAddress, 
                  Employees.Employee.JobTitle, 
				  Employees.Employee.HireDate, 
				  CASE Employees.Employee.IsActive WHEN 0 THEN 'لا' WHEN 1 THEN 'نعم' END AS IsActive
         FROM     Employees.Employee left JOIN
                  Employees.Department ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId left JOIN
                  Employees.Employer ON Employees.Employee.Employer = Employees.Employer.EmployerId LEFT JOIN
                  Employees.ContractType ON Employees.Employee.ContractType = Employees.ContractType.ContractTypeId LEFT JOIN
                  Employees.Employee AS Employee_1 ON Employees.Employee.ManagerId = Employee_1.EmployeeId LEFT JOIN
                  Users.Role ON Employees.Employee.RoleId = Users.Role.RoleId LEFT JOIN
                  Employees.Nationality ON Employees.Employee.NationalityId = Employees.Nationality.NationalityId AND 
                  Employees.Employee.NationalityId = Employees.Nationality.NationalityId AND Employee_1.NationalityId = Employees.Nationality.NationalityId AND 
                  Employee_1.NationalityId = Employees.Nationality.NationalityId
				 JOIN #Departments ON Employees.Employee.DepartmentId=#Departments.DepartmentId
          WHERE (Employees.Employee.ContractType=@ContractType AND Employees.Employee.Employer=@Employer ) 
		  OR (Employees.Employee.ContractType=@ContractType AND @Employer=0) 
		  OR (Employees.Employee.Employer=@Employer AND @ContractType=0)
		  OR (@Employer=0 AND @ContractType=0)
	 

		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END





GO
