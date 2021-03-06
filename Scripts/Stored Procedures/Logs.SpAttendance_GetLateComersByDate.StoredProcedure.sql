USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendance_GetLateComersByDate]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Late Employees By Date
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendance_GetLateComersByDate]
(
 @DepartmentId     int = NULL,
 @BegDate          date = NULL,
 @EndDate          date = NULL,
 @OptionNo         int = 1 
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
    
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
			SELECT Logs.AttendanceLog.LogId, 
				   dbo.FnUmAlquraYMD( Logs.AttendanceLog.LogDate) as LogDate, 
				   Logs.AttendanceLog.StatusId, 
				   Logs.AttendanceLog.EmployeeId, 
				   Logs.AttendanceLog.InLogId, 
				   Logs.AttendanceLog.InTime, 
				   Logs.AttendanceLog.OutLogId, 
				   Logs.AttendanceLog.OutTime, 
				   Logs.AttendanceLog.IsInPunchManual, 
				   Logs.AttendanceLog.IsOutPunchManual, 
				   Logs.AttendanceLog.WorkingMinutes, 
				   Logs.AttendanceLog.InLateMinutes, 
				   Logs.AttendanceLog.OutLateMinutes, 
				   Logs.AttendanceLog.LateMinutes, 
				   Logs.AttendanceLog.OvertimeMinutes,
				   Logs.AttendanceLog.InExtraMinutes,
				   Logs.AttendanceLog.OutExtraMinutes,                                       
				   Logs.AttendanceLog.ExtraTimeMinutes, 
				   Logs.AttendanceLog.ActualWorkMinutes, 
				   Logs.AttendanceLog.WorkScheduleId, 
				   Logs.AttendanceLog.WorkStartTime, 
				   Logs.AttendanceLog.WorkEndTime, 
				   Logs.AttendanceLog.PolicyId, 
				   Logs.AttendanceLog.LateInMinutes, 
				   Logs.AttendanceLog.LateOutMinutes, 
				   Logs.AttendanceLog.EarlyInMinutes, 
				   Logs.AttendanceLog.EarlyOutMinutes, 
				   Logs.AttendanceLog.MarkObsentDuration, 
				   Logs.AttendanceLog.IsProcessCompleted, 
				   Logs.AttendanceLog.IsReapplyPolicy, 
				   Logs.AttendanceLog.ActionId, 
				   Logs.AttendanceLog.UpdateVersion, 
				   Logs.AttendanceLog.AddedUserAccountId, 
				   Logs.AttendanceLog.UpdatedUserAccountId, 
				   Logs.AttendanceLog.AddedDate, 
				   Logs.AttendanceLog.UpdatedDate, 
				   Logs.AttendanceLog.VersionNo, 
				   Logs.AttendanceStatus.StatusName,
				   Employees.Employee.FirstName + ' ' +  Employees.Employee.MiddleName + ' ' +  Employees.Employee.LastName AS EmployeeName,
				   Employees.Employee.BadgeNo,
				   Employees.Department.DepartmentName, 
				   CASE Logs.AttendanceLog.IsInPunchManual 
					 WHEN 0 THEN 'لا' 
					 WHEN 1 THEN 'نعم'
				   END AS InPunchManual,
				   CASE Logs.AttendanceLog.IsOutPunchManual 
					 WHEN 0 THEN 'لا'
					 WHEN 1 THEN 'نعم'
				   END AS OutPunchManual, 
				   CASE Logs.AttendanceLog.IsProcessCompleted 
					 WHEN 0 THEN 'لا'
					 WHEN 1 THEN 'نعم'
				   END AS ProcessCompleted, 
				   CASE Logs.AttendanceLog.IsReapplyPolicy 
					 WHEN 0 THEN 'لا'
					 WHEN 1 THEN 'نعم'
				   END AS ReapplyPolicy,
				   Employees.Department.DepartmentId, 
				   dbo.FnUmAlquraYMD(Logs.AttendanceLog.LogDate) AS UmLogDate, 
				   Logs.funGetPrecentage(Logs.AttendanceLog.ActualWorkMinutes, Logs.AttendanceLog.WorkingMinutes) AS Precentage 
			  FROM Logs.AttendanceLog INNER JOIN Logs.AttendanceStatus 
				ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
				   INNER JOIN Employees.Employee 
				ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
				   INNER JOIN Employees.Department 
				ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
				
				   INNER JOIN CTE
				ON Employees.Employee.DepartmentId = CTE.DepartmentId 
			  WHERE Logs.AttendanceLog.LogDate >= @BegDate
				AND Logs.AttendanceLog.LogDate <= @EndDate 
				AND Logs.AttendanceLog.StatusId in(45,110,120)
		   ORDER BY Employees.Employee.EmployeeName			
		
				    
		
		    
		RETURN 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
