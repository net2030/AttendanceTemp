USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepass_GetDepartmentGatepass]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get department Gatepass
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepass_GetDepartmentGatepass]
(
 @DepartmentId int = NULL,
 @Lang         char(2) = 'ar',
 @BegDate      date = NULL,
 @EndDate      date = NULL,
 @OptionNo      int = 1 
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
       IF @Lang = 'en'
		   BEGIN
			;WITH CTE(DepartmentId, DepartmentName, ParentId)
			AS
			(
			SELECT DepartmentId, DepartmentNameEN, ParentId
			  From Employees.Department 
			 WHERE DepartmentId = @DepartmentId
			 UNION ALL
			SELECT e.DepartmentId, e.DepartmentNameEN, e.ParentId
			  FROM Employees.Department e
				   INNER JOIN CTE c
				ON e.ParentId = c.DepartmentId
				)

	   
			SELECT Managements.Gatepass.GatepassId, 
				   Managements.Gatepass.EmployeeId, 
				   Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + ' ' + Employees.Employee.LastName AS Employeename , 
				   Employees.Employee.BadgeNo AS EmployeeCode,
				   Managements.Gatepass.GatepassTypeId, 
				   Managements.Gatepass.GatepassBegDate, 
				   Managements.Gatepass.GatepassEndDate, 
				   Convert(char,dbo.FnUmAlquraYMD(Managements.Gatepass.[GatepassBegDate])) + ' -- ' + SUBSTRING(CAST(Managements.Gatepass.[GatepassBegTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(Managements.Gatepass.[GatepassBegTime] AS NVARCHAR(30)),18 ,2) AS GatepassBegTime ,
				   Convert(char,dbo.FnUmAlquraYMD(Managements.Gatepass.[GatepassBegDate])) + ' -- ' + SUBSTRING(CAST(Managements.Gatepass.[GatepassEndTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(Managements.Gatepass.[GatepassEndTime] AS NVARCHAR(30)),18 ,2) AS GatepassEndTime ,
				   --Managements.Gatepass.GatepassBegTime, 
				   --Managements.Gatepass.GatepassEndTime, 
				   Managements.Gatepass.ApprovedEmployeeId,  
				   Managements.GatepassType.GatepassTypeNameEN AS GatepassTypeName,
				   Managements.Gatepass.IsApproved,
				   Managements.Gatepass.IsRejected,
				   CASE 
				   WHEN Managements.Gatepass.IsApproved=1 
				   THEN 'Approved'
				   WHEN Managements.Gatepass.IsRejected=1 
				   THEN 'Rejected'
				   Else 'Submitted' END AS AprovalStatus,
				   Managements.Gatepass.ApprovalNotes,
				   Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + ' ' + Employees.Employee.LastName AS Employeename , 
				   CTE.DepartmentName, 
				   dbo.FnUmAlquraYMD(Managements.Gatepass.GatepassBegDate) AS UmBegDate, 
				   dbo.FnUmAlquraYMD(Managements.Gatepass.GatepassEndDate) AS UmEndDate, 
				   Employees.fnGetEmployeeName(Managements.Gatepass.ApprovedEmployeeId) AS ApprovedName  	
				FROM Managements.Gatepass INNER JOIN Employees.Employee 
				ON Managements.Gatepass.EmployeeId = Employees.Employee.EmployeeId          
				   INNER JOIN CTE 
				ON Employees.Employee.DepartmentId = CTE.DepartmentId
				   INNER JOIN Managements.GatepassType 
				ON Managements.Gatepass.GatepassTypeId = Managements.GatepassType.GatepassTypeId            
			 WHERE [GatepassBegDate] >= @BegDate
			   AND [GatepassEndDate] <= @EndDate     
			 END	  
	   ELSE	
	   BEGIN
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

	   
			SELECT Managements.Gatepass.GatepassId, 
				   Managements.Gatepass.EmployeeId, 
				   Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + ' ' + Employees.Employee.LastName AS Employeename , 
				   Employees.Employee.BadgeNo AS EmployeeCode,
				   Managements.Gatepass.GatepassTypeId, 
				   Managements.Gatepass.GatepassBegDate, 
				   Managements.Gatepass.GatepassEndDate, 
				   Convert(char,dbo.FnUmAlquraYMD(Managements.Gatepass.[GatepassBegDate])) + ' -- ' + SUBSTRING(CAST(Managements.Gatepass.[GatepassBegTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(Managements.Gatepass.[GatepassBegTime] AS NVARCHAR(30)),18 ,2) AS GatepassBegTime ,
				   Convert(char,dbo.FnUmAlquraYMD(Managements.Gatepass.[GatepassBegDate])) + ' -- ' + SUBSTRING(CAST(Managements.Gatepass.[GatepassEndTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(Managements.Gatepass.[GatepassEndTime] AS NVARCHAR(30)),18 ,2) AS GatepassEndTime ,
				   --Managements.Gatepass.GatepassBegTime, 
				   --Managements.Gatepass.GatepassEndTime, 
				   Managements.Gatepass.ApprovedEmployeeId,  
				   Managements.GatepassType.GatepassTypeName,
				   Managements.Gatepass.IsApproved,
				   Managements.Gatepass.IsRejected,
				   CASE 
				   WHEN Managements.Gatepass.IsApproved=1 
				   THEN 'تم الموافقة'
				   WHEN Managements.Gatepass.IsRejected=1 
				   THEN 'رفض الطلب'
				   Else 'تحت الدراسة' END AS AprovalStatus,
				   Managements.Gatepass.ApprovalNotes,
				   Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + ' ' + Employees.Employee.LastName AS Employeename , 
				   CTE.DepartmentName, 
				   dbo.FnUmAlquraYMD(Managements.Gatepass.GatepassBegDate) AS UmBegDate, 
				   dbo.FnUmAlquraYMD(Managements.Gatepass.GatepassEndDate) AS UmEndDate, 
				   Employees.fnGetEmployeeName(Managements.Gatepass.ApprovedEmployeeId) AS ApprovedName  	
				FROM Managements.Gatepass INNER JOIN Employees.Employee 
				ON Managements.Gatepass.EmployeeId = Employees.Employee.EmployeeId          
				   INNER JOIN CTE 
				ON Employees.Employee.DepartmentId = CTE.DepartmentId
				   INNER JOIN Managements.GatepassType 
				ON Managements.Gatepass.GatepassTypeId = Managements.GatepassType.GatepassTypeId            
			 WHERE [GatepassBegDate] >= @BegDate
			   AND [GatepassEndDate] <= @EndDate     
			 END	  	  		    
	   RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
