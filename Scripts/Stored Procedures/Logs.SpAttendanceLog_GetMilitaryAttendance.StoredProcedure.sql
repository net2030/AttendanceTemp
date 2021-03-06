USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendanceLog_GetMilitaryAttendance]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Military Attendance
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendanceLog_GetMilitaryAttendance]
(
 @DepartmentId   int = NULL,
 @Attendancedate date = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected  int
        
    -- Initializations              
    SET @RowEffected = 0
    
     
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
			                                       
		SELECT ROW_NUMBER() OVER ( ORDER BY SortIndex) AS RowSerailID,
		       e.LogId,
			   e.LogDate,
			   e.StatusId,
			   e.EmployeeId,
			   e.InLogId,
			   e.InTime,
			   e.OutLogId,
			   e.OutTime,
			   e.IsInPunchManual,
			   e.IsOutPunchManual,
			   e.WorkingMinutes,
			   e.InLateMinutes,
			   e.OutLateMinutes, 
			   e.LateMinutes,
			   e.InExtraMinutes,
			   e.OutExtraMinutes,
			   e.ExtraTimeMinutes,			   
			   e.OvertimeMinutes,
			   e.ActualWorkMinutes,
			   e.WorkScheduleId,
			   e.WorkStartTime,
			   e.WorkEndTime,
			   e.PolicyId,
			   e.LateInMinutes,
			   e.LateOutMinutes,
		       e.EarlyInMinutes,
		       e.EarlyOutMinutes,			   
			   e.MarkObsentDuration,
			   e.IsProcessCompleted,
			   e.IsReapplyPolicy,
			   e.ActionId,
			   e.UpdateVersion,
			   e.AddedUserAccountId,
			   e.UpdatedUserAccountId,
			   e.AddedDate,
			   e.UpdatedDate,
			   e.VersionNo,
			   e.StatusName,
			   e.EmployeeName,
			   e.DepartmentName,
			   e.InPunchManual,
			   e.OutPunchManual,
			   e.ProcessCompleted,
			   e.ReapplyPolicy,
			   e.PositionTypeId,
			   e.SortIndex,
			   e.PositionName,
			   e.Precentage
		  FROM  [Logs].[vwAttendanceLog] e	     		      
		       INNER JOIN CTE
		  ON e.DepartmentId = CTE.DepartmentId    
       WHERE e.LogDate = @Attendancedate
         AND e.PositionTypeId = 1
      ORDER BY e.SortIndex
              		  		  		  		    
	   RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END




GO
