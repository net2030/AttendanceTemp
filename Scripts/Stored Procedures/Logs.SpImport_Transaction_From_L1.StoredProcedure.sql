USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpImport_Transaction_From_L1]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Logs].[SpImport_Transaction_From_L1]
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

INSERT INTO Logs.[MachineLog] ([LID],[EmployeeId],[LocationId],[LogDate],[LogTime])
Select TransactionId,ID,UnitName,convert(varchar(10),[DateTime],111),convert(datetime,convert(varchar(8),[dateTime],108))
from SADB.dbo.TransactionLog
     Where TAndAMsg  IN ('F1','F2','F3','F4') and ID <>0 and TransactionId not in (select LID from Logs.[MachineLog] )
     
         

    -- تسجيل رقم الموظف المعتمد
     
--update Logs.MachineLog set logs.machinelog.EmployeeId=t1.EmployeeId
--from Employees.Employee as t1 JOIN logs.MachineLog  t2 on t1.BadgeNo=t2.EID
--where t2.EmployeeId=0


----تحديث اسم جهاز البصمه في جدول الحركات

update Logs.MachineLog set logs.machinelog.MachineId=t1.MachineId
from logs.Machine as t1 JOIN logs.MachineLog  t2 on t1.MachineName=t2.LocationId

----تعديل نوع الحركه في جدول الحركات

UPDATE logs.MachineLog SET  LogTypeId = 1  FROM  logs.MachineLog t1 JOIN  SADB.dbo.TransactionLog t2 ON t1.LID=t2.TransactionId WHERE    t2.TAndAMsg in('F1','F3')
UPDATE logs.MachineLog SET  LogTypeId = 2  FROM  logs.MachineLog t1 JOIN  SADB.dbo.TransactionLog t2 ON t1.LID=t2.TransactionId WHERE    t2.TAndAMsg in ('F2','F4')

END






GO
