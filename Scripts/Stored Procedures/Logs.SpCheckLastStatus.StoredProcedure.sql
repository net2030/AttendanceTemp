USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpCheckLastStatus]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Logs].[SpCheckLastStatus]
AS
BEGIN
     DECLARE @LogDate Date
	 DECLARE @EmployeeId INT
	 DECLARE @LogId INT
	 DECLARE @LogType INT
	 DECLARE @LogTime time
     DECLARE Curs_Employees CURSOR
     LOCAL FOR                                          
     SELECT EmployeeId FROM Logs.MachineLog WHERE Logdate=CONVERT(date, GETDATE(), 103)
     FOR READ ONLY

	 SET @LogDate=CONVERT(date, GETDATE(), 103)

	  /*****************************************************
	   Open Cursor
	 *****************************************************/      
     OPEN Curs_Employees
     FETCH NEXT FROM Curs_Employees INTO @EmployeeId
     WHILE @@Fetch_Status = 0 
     BEGIN

		set @LogId=(SELECT TOP(1) Logs.MachineLog.LogId
		from Logs.MachineLog
		where Logs.MachineLog.LogDate = @LogDate and Logs.MachineLog.EmployeeId = @EmployeeId
		order by LogTime desc)
   
	   SELECT @logtime=LogTime,@LogType=LogTypeId FROM Logs.MachineLog WHERE LogId=@LogId

	   IF @LogType=2
	   UPDATE Logs.AttendanceLog SET StatusId=122,OutTime=@logtime WHERE EmployeeId=@EmployeeId AND LogDate=@LogDate

       FETCH NEXT FROM Curs_Employees INTO @EmployeeId
     END
     
     -- close cursor
     CLOSE Curs_Employees
     -- de-allocate cursor
     DEALLOCATE Curs_Employees

END




GO
