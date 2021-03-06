USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_GetByEmployeeId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Vacations 
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_GetByEmployeeId]
(
 @UserAccountId  int = NULL,
 @EmployeeId     int = NULL,
 @Lang           char(5) = 'ar', 
 @PageNumber     int = 1,
 @PageSize       int = 50,
 
 @PagesTotal     int = 0 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    BEGIN TRY

	       IF @Lang  in ('ar','ar-SA')
		   SELECT Managements.Vacation.VacationId, 
				  Managements.Vacation.EmployeeId,  
				  Managements.Vacation.TypeId,  
				  dbo.FnUmAlquraYMD(Managements.Vacation.EffectiveDate) AS EffectiveDate,
	              dbo.FnUmAlquraYMD(Managements.Vacation.DateExpire) AS DateExpire, 
	              dbo.FnUmAlquraYMD(Managements.Vacation.DateOfReturn) AS DateOfReturn, 
				  Managements.Vacation.VersionNo, 
				  Managements.VacationType.TypeName  AS TypeName,
				  Managements.Vacation.Note,
                  Managements.Vacation.[Address],
				  Managements.Vacation.Contact,
				  Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + ' ' + Employees.Employee.LastName AS Employeename , 
				  Employees.Employee.GovId,
				  Employees.Employee.EmployeeNo,
				  E2.FirstName + ' ' + E2.MiddleName + ' ' + E2.LastName AS AddedBy , 
				  dbo.FnUmAlquraYMD(Managements.Vacation.AddedDate) AS AddedDate, 

				  CASE  
				  WHEN  Managements.Vacation.AltEmployeeId = Managements.Vacation.EmployeeId THEN ''
				  ELSE
				  E1.FirstName + ' ' + E1.MiddleName + ' ' + E1.LastName END AS AltEmployeename , 

				  Employees.Employee.BadgeNo As EmployeeCode, 
				  Employees.Department.DepartmentName,
				  Managements.Vacation.IsApproved,
				   Managements.Vacation.IsRejected,
				   CASE 
			      WHEN Managements.Vacation.IsApproved=1 
			      THEN 'تم الموافقة'
			      WHEN Managements.Vacation.IsRejected=1 
			      THEN 'رفض الطلب'
			      Else 'تحت الدراسة' END AS AprovalStatus,
				  Managements.Vacation.ApprovalNotes,
				  DATEDIFF(dd, Managements.Vacation.EffectiveDate, Managements.Vacation.DateExpire) + 1 AS VacationDays
			 FROM Managements.Vacation INNER JOIN Managements.VacationType  
			   ON Managements.Vacation.TypeId = Managements.VacationType.TypeId
				  INNER JOIN Employees.Employee
			   ON Managements.Vacation.EmployeeId = Employees.Employee.EmployeeId
			     INNER JOIN Employees.Employee E1
			   ON Managements.Vacation.AltEmployeeId = E1.EmployeeId
				  INNER JOIN Employees.Department
			   ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
			   INNER JOIN Employees.Employee E2 ON Managements.Vacation.AddedUserAccountId=E2.EmployeeId
			 
			   WHERE Managements.Vacation.EmployeeId =@EmployeeId 
			   ORDER By EffectiveDate desc

		ELSE

		     SELECT Managements.Vacation.VacationId, 
				  Managements.Vacation.EmployeeId,  
				  Managements.Vacation.TypeId,  
				  dbo.FnUmAlquraYMD(Managements.Vacation.EffectiveDate) AS EffectiveDate,
	              dbo.FnUmAlquraYMD(Managements.Vacation.DateExpire) AS DateExpire, 
	              dbo.FnUmAlquraYMD(Managements.Vacation.DateOfReturn) AS DateOfReturn, 
				  Managements.Vacation.VersionNo, 
				  Managements.VacationType.TypeNameEN as TypeName ,
				  Managements.Vacation.Note,
                  Managements.Vacation.[Address],
				  Managements.Vacation.Contact,
				  Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + ' ' + Employees.Employee.LastName AS Employeename , 
				  Employees.Employee.GovId,
				  Employees.Employee.EmployeeNo,
				  E2.FirstName + ' ' + E2.MiddleName + ' ' + E2.LastName AS AddedBy , 
				  dbo.FnUmAlquraYMD(Managements.Vacation.AddedDate) AS AddedDate, 

				  CASE  
				  WHEN  Managements.Vacation.AltEmployeeId = Managements.Vacation.EmployeeId THEN ''
				  ELSE
				  E1.FirstName + ' ' + E1.MiddleName + ' ' + E1.LastName END AS AltEmployeename , 

				  Employees.Employee.BadgeNo As EmployeeCode, 
				  Employees.Department.DepartmentName,
				  Managements.Vacation.IsApproved,
				   Managements.Vacation.IsRejected,
				   CASE 
			      WHEN Managements.Vacation.IsApproved=1 
			      THEN 'Approved'
			      WHEN Managements.Vacation.IsRejected=1 
			      THEN 'Rejected'
			      Else 'Submitted' END AS AprovalStatus,
				  Managements.Vacation.ApprovalNotes,
				  DATEDIFF(dd, Managements.Vacation.EffectiveDate, Managements.Vacation.DateExpire) + 1 AS VacationDays
			 FROM Managements.Vacation INNER JOIN Managements.VacationType  
			   ON Managements.Vacation.TypeId = Managements.VacationType.TypeId
				  INNER JOIN Employees.Employee
			   ON Managements.Vacation.EmployeeId = Employees.Employee.EmployeeId
			     INNER JOIN Employees.Employee E1
			   ON Managements.Vacation.AltEmployeeId = E1.EmployeeId
				  INNER JOIN Employees.Department
			   ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
			   INNER JOIN Employees.Employee E2 ON Managements.Vacation.AddedUserAccountId=E2.EmployeeId
			 
			   WHERE Managements.Vacation.EmployeeId =@EmployeeId 
			   ORDER By EffectiveDate desc
	 
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
    
   
END






GO
