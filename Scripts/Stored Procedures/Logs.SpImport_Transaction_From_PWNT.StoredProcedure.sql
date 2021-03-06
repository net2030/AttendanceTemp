USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpImport_Transaction_From_PWNT]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Logs].[SpImport_Transaction_From_PWNT]
AS
BEGIN
	

-------------------------------------------------------------------------
-- سحب بيانات الموظفين 
--set IDENTITY_INSERT  employees.employee on



--insert into Employees.Employee(EmployeeId, BadgeNo,EmployeeName,DepartmentId)

--select t1.ID,t2.TemplateID, t1.FirstName+' '+t1.MiddleName +' ' + t1.LastName,t1.DepartmentId from SADB.dbo.EnterpriseUser as t1 join SADB.dbo.Template  as t2 on(t1.Id=t2.EnterpriseUserId)

--------------------تسجيل حسابات الموظفين من جدول الموظفين

--insert into users.UserAccount(EmployeeId,UserName)
--select  EmployeeId,EmployeeName from Employees.Employee 
--where EmployeeName not in (select UserName from Users.UserAccount) 

-----------------------------------------------------------------------------
-- سحب حركات البصمات للموظفين

  INSERT INTO [Logs].[MachineLog] ([LId],EmployeeId ,[LogDate],[LogTime],[LogDateTime],LogTypeId,IPAddress)
     Select no1,convert(int,LNAME),CONVERT(date, EVNT_DAT, 103),convert(datetime,convert(varchar(8),EVNT_DAT,108)),EVNT_DAT,CASE  WHEN convert(datetime,convert(varchar(8),EVNT_DAT,108)) > '1900-01-01 13:36:54.000' THEN 2 ELSE 1 END,'192.168.1.183'
     FROM [PWNT].[dbo].[EV_LOG]
     Where  NO1 not IN (select LId from [Logs].[MachineLog] ) and (BADGENO is not null) --and (LOCATION like 'Turnstile%')
         



    -- تسجيل رقم الموظف المعتمد
     
--update Logs.MachineLog set logs.machinelog.EmployeeId=t1.EmployeeId
--from Employees.Employee as t1 JOIN logs.MachineLog  t2 on t1.ID=t2.EID
--where t2.EmployeeId=0



----تحديث اسم جهاز البصمه في جدول الحركات

--update Logs.MachineLog set logs.machinelog.MachineId=t1.MachineId
--from logs.Machine as t1 JOIN logs.MachineLog  t2 on t1.MachineName=t2.LocationId

----تعديل نوع الحركه في جدول الحركات

--UPDATE logs.MachineLog SET  LogTypeId = 1  FROM  logs.MachineLog t1 JOIN  SADB.dbo.TransactionLog t2 ON t1.LID=t2.TransactionId WHERE    t2.TAndAMsg in('F1','F3')
--UPDATE logs.MachineLog SET  LogTypeId = 2  FROM  logs.MachineLog t1 JOIN  SADB.dbo.TransactionLog t2 ON t1.LID=t2.TransactionId WHERE    t2.TAndAMsg in ('F2','F4')

END






GO
