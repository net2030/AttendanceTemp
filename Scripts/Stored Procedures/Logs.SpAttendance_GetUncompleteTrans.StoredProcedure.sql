USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendance_GetUncompleteTrans]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Uncomplete transactions
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendance_GetUncompleteTrans]
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
		IF @OptionNo = 2
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
			SELECT Logs.AttendanceLog.LogId, 
				   Logs.AttendanceLog.LogDate, 
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
				   Employees.Employee.EmployeeName, 
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
				   Employees.Position.SortIndex, 
				   Employees.Position.PositionTypeId, 
				   Employees.Position.PositionName,
				   Logs.funGetPrecentage(Logs.AttendanceLog.ActualWorkMinutes, Logs.AttendanceLog.WorkingMinutes) AS Precentage 
			  FROM Logs.AttendanceLog INNER JOIN Logs.AttendanceStatus 
				ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
				   INNER JOIN Employees.Employee 
				ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
				   INNER JOIN Employees.Department 
				ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
				   INNER JOIN Employees.Position 
				ON Employees.Employee.PositionId = Employees.Position.PositionId 
				   INNER JOIN CTE
				ON Employees.Employee.DepartmentId = CTE.DepartmentId 
			  WHERE Logs.AttendanceLog.LogDate >= @BegDate
				AND Logs.AttendanceLog.LogDate <= @EndDate 
				AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90)
				AND Employees.Position.PositionTypeId = 1
		   ORDER BY Employees.Employee.EmployeeId	
		END
		ELSE
		IF @OptionNo = 3
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
			SELECT Logs.AttendanceLog.LogId, 
				   Logs.AttendanceLog.LogDate, 
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
				   Employees.Employee.EmployeeName, 
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
				   Employees.Position.SortIndex, 
				   Employees.Position.PositionTypeId, 
				   Employees.Position.PositionName 
			  FROM Logs.AttendanceLog INNER JOIN Logs.AttendanceStatus 
				ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
				   INNER JOIN Employees.Employee 
				ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
				   INNER JOIN Employees.Department 
				ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
				   INNER JOIN Employees.Position 
				ON Employees.Employee.PositionId = Employees.Position.PositionId 
				   INNER JOIN CTE
				ON Employees.Employee.DepartmentId = CTE.DepartmentId 
			  WHERE Logs.AttendanceLog.LogDate >= @BegDate
				AND Logs.AttendanceLog.LogDate <= @EndDate 
				AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90)
				AND Employees.Position.PositionTypeId IN (2, 3)
		   ORDER BY Employees.Department.DepartmentName			
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
			SELECT Logs.AttendanceLog.LogId, 
				   Logs.AttendanceLog.LogDate, 
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
				   Employees.Employee.EmployeeName, 
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
				   Employees.Position.SortIndex, 
				   Employees.Position.PositionTypeId, 
				   Employees.Position.PositionName 
			  FROM Logs.AttendanceLog INNER JOIN Logs.AttendanceStatus 
				ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
				   INNER JOIN Employees.Employee 
				ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
				   INNER JOIN Employees.Department 
				ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
				   INNER JOIN Employees.Position 
				ON Employees.Employee.PositionId = Employees.Position.PositionId 
				   INNER JOIN CTE
				ON Employees.Employee.DepartmentId = CTE.DepartmentId 
			  WHERE Logs.AttendanceLog.LogDate >= @BegDate
				AND Logs.AttendanceLog.LogDate <= @EndDate 
				AND Logs.AttendanceLog.StatusId IN (10, 65, 70, 80, 85, 90)
		   --ORDER BY Employees.Department.DepartmentName	
		      ORDER BY Employees.Employee.EmployeeId		
		END					    
		
		    
		RETURN 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
