USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_InsertLDAP]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 08/02/2017
-- Description:	Insert a new Employee record
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_InsertLDAP]

WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
	   
    -- Declariations

	DECLARE @DepartmentId int =0
	DECLARE @ParentDepartmentId int =0
    

    BEGIN TRY


	  INSERT INTO Employees.Department(DepartmentName,ParentId,IsProtected,AddedUserAccountId,UpdatedUserAccountId,AddedDate,UpdatedDate)
	  SELECT Distinct Department,1,0,1,1,getdate(),GetDate() 
	  FROM EmployeeTemp WHERE EmployeeTemp.Department NOT IN (SELECT DepartmentName FROM Employees.Department)
	  AND EmployeeTemp.Department <> ''  AND FirstName <> '' AND LastName <> ''


	
      INSERT INTO Employees.Department(DepartmentName,ParentId,IsProtected,AddedUserAccountId,UpdatedUserAccountId,AddedDate,UpdatedDate)
	  SELECT Distinct ParentDepartment,1,0,1,1,getdate(),GetDate() 
	  FROM EmployeeTemp WHERE ParentDepartment NOT IN (SELECT DepartmentName FROM Employees.Department)
	  AND ParentDepartment <> '' AND FirstName <> '' AND LastName <> '' 
      
	  UPDATE EmployeeTemp SET DepartmentId = t.DepartmentId
	  FROM Employees.Department t JOIN EmployeeTemp t1 ON t.DepartmentName=t1.Department

	  UPDATE EmployeeTemp SET ParentDepartmentId = t.DepartmentId
	  FROM Employees.Department t JOIN EmployeeTemp t1 ON t.DepartmentName=t1.ParentDepartment

	   UPDATE EmployeeTemp SET DepartmentId = t.DepartmentId
	  FROM Employees.Department t JOIN EmployeeTemp t1 ON t.DepartmentName=t1.ParentDepartment
	  WHERE t1.DepartmentId is null

	  
        

		INSERT INTO  [Employees].[Employee]
				   ( Firstname ,
					 MiddleName ,
					 LastName ,
					 EmployeeNameEnglish,
					 EmployeeName,
					 JobTitle ,
					 GovId,
					 EmployeeNo,
					 BadgeNo ,
					 DepartmentId,
					 NationalityId,
					 HireDate,
					 IsActive,
					 IsFingerRegistered, 
					 IsAllowOvertime,

					 ActionId,
					 LocationId,
					 RoleId,
					 UserName,
					 EmailAddress,
					 [Password],
					 IsForcePasswordChange  ,
					 WorkScheduleId,
					 PositionTypeId,
					 PositionId,
				    [AddedUserAccountId],
				    [UpdatedUserAccountId],
				    [AddedDate],
				    [UpdatedDate])

					SELECT 
					FirstName,
					'',
					LastName,
					EnglishName,
					ArabicName,
					JobTitle,
					GovId,
					EmployeeNo,
					EmployeeNo,
					CASE  WHEN DepartmentId IS NOT NULL THEN DepartmentId ELSE 1 END,
					1,
					GetDate(),
					1,
					0,
					0,
					1,
					1,
					2,
					SAMAccountName,
					mail,
					'FeozkS1TQ0YrnmfTgcWyCXACzrs=',
					1,
					1,
					1,
					1,
					1,
					1,
					GetDate(),
					GetDate()

					FROM EmployeeTemp
					WHERE SAMAccountName NOT IN (SELECT UserName FROM Employees.Employee)
					AND FirstName <> '' AND LastName <> ''

				   UPDATE EmployeeTemp SET ManagerId = t.EmployeeId
				   FROM Employees.Employee t JOIN EmployeeTemp t1
				   ON t.EmployeeNameEnglish=t1.manager
	               

				   UPDATE Employees.Employee  SET ManagerId=t.ManagerId
				   FROM EmployeeTemp t JOIN Employees.Employee t1
				   ON t.SAMAccountName = t1.UserName
				   where t.ManagerId is not null
				   
				        
				        
	
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        
        RETURN    
    END CATCH
END






GO
