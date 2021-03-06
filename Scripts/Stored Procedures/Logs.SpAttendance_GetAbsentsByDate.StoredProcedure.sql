USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendance_GetAbsentsByDate]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Absent Employees By Date
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendance_GetAbsentsByDate]
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
				  dbo.FnUmAlquraYMD(Logs.AttendanceLog.LogDate) AS LogDate, 
				   Logs.AttendanceLog.InTime, 
				   Logs.AttendanceLog.OutTime, 
				   Logs.AttendanceLog.WorkingMinutes, 
				   Logs.AttendanceLog.InLateMinutes, 
				   Logs.AttendanceLog.OutLateMinutes, 
				   Logs.AttendanceLog.LateMinutes, 
				   Logs.AttendanceLog.OvertimeMinutes,
				   Logs.AttendanceLog.InExtraMinutes,
				   Logs.AttendanceLog.OutExtraMinutes,                                       
				   Logs.AttendanceLog.ExtraTimeMinutes, 
				   Logs.AttendanceLog.ActualWorkMinutes, 
				   Logs.AttendanceStatus.StatusName, 
				   Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + ' ' + Employees.Employee.LastName AS EmployeeName, 
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
				   Employees.Department.DepartmentId
				 
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
				AND Logs.AttendanceLog.StatusId = 75
		
		   ORDER BY Employees.Employee.EmployeeName			
		END
			    
		
		    
		RETURN 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
