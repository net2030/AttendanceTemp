USE [MASAttendance]
GO
/****** Object:  StoredProcedure [LogsShifts].[SpUpdateTimesFromMachineLogForShifts]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 23/09/2008
-- Description:	Update Attendance In & Out Time from Machine Log for Shifts
-- =============================================
CREATE PROCEDURE [LogsShifts].[SpUpdateTimesFromMachineLogForShifts]
(
 @LogId          int = NULL,
 @AttendanceDate datetime = NULL,
 @EmployeeId     int = NULL 
)
AS
BEGIN
	SET NOCOUNT ON;

    -- Declariations
    DECLARE @RowEffected int
    
    DECLARE @InLogId int = NULL
    DECLARE @InTime datetime = NULL
    DECLARE @IsInPunchManual bit = NULL
    DECLARE @OutLogId int = NULL
    DECLARE @OutTime datetime = NULL
    DECLARE @IsOutPunchManual bit = NULL 
	DECLARE @WorkBegTime datetime = NULL  
	DECLARE @WorkEndTime datetime = NULL   
	DECLARE @NextDayDate DATE
	DECLARE @PreviousDayDate DATE

SELECT @WorkBegTime=WorkStartTime,@WorkEndTime= WorkEndTime from Logs.AttendanceLog where LogId=@LogId


SET @NextDayDate=DATEADD(Day,1,@AttendanceDate)
SET @PreviousDayDate=DATEADD(Day,-1,@AttendanceDate)
------------------------------------------






------------------------------------------------------------------------------

set @InLogId=(SELECT TOP(1)  Logs.MachineLog.LogId
								from Logs.MachineLog
								where ( Logs.MachineLog.LogDate = @AttendanceDate) 
								AND (Logs.MachineLog.EmployeeId = @EmployeeId)  
								AND (LogTypeId=1)
								AND (datediff(MINUTE ,LogDateTime,@WorkBegTime)>-400)
								AND (datediff(MINUTE ,LogDateTime,@WorkBegTime)<400)
								order by LogTime)
IF @InLogId IS NULL

set @InLogId=(SELECT TOP(1)  Logs.MachineLog.LogId
								from Logs.MachineLog
								where ( Logs.MachineLog.LogDate = @PreviousDayDate) 
								AND (Logs.MachineLog.EmployeeId = @EmployeeId)  
								AND (LogTypeId=1)
								AND (datediff(MINUTE ,LogDateTime,@WorkBegTime)>-400)
								AND (datediff(MINUTE ,LogDateTime,@WorkBegTime)<400)
								order by LogTime)
IF @InLogId IS NULL

set @InLogId=(SELECT TOP(1)  Logs.MachineLog.LogId
								from Logs.MachineLog
								where ( Logs.MachineLog.LogDate = @NextDayDate) 
								AND (Logs.MachineLog.EmployeeId = @EmployeeId)  
								AND (LogTypeId=1)
								AND (datediff(MINUTE ,LogDateTime,@WorkBegTime)>-400)
								AND (datediff(MINUTE ,LogDateTime,@WorkBegTime)<400)
								order by LogTime)

--========================================================================================

set @OutLogId=(SELECT TOP(1)  Logs.MachineLog.LogId
								from Logs.MachineLog
								where ( Logs.MachineLog.LogDate = @AttendanceDate) 
								AND (Logs.MachineLog.EmployeeId = @EmployeeId)  
								AND (LogTypeId=2)
								AND (datediff(MINUTE ,LogDateTime,@WorkEndTime)>-400)
								AND (datediff(MINUTE ,LogDateTime,@WorkEndTime)<400)
								order by LogTime)
IF @OutLogId IS NULL

set @OutLogId=(SELECT TOP(1)  Logs.MachineLog.LogId
								from Logs.MachineLog
								where ( Logs.MachineLog.LogDate = @PreviousDayDate) 
								AND (Logs.MachineLog.EmployeeId = @EmployeeId)  
								AND (LogTypeId=2)
								AND (datediff(MINUTE ,LogDateTime,@WorkEndTime)>-400)
								AND (datediff(MINUTE ,LogDateTime,@WorkEndTime)<400)
								order by LogTime)
IF @OutLogId IS NULL

set @OutLogId=(SELECT TOP(1)  Logs.MachineLog.LogId
								from Logs.MachineLog
								where ( Logs.MachineLog.LogDate = @NextDayDate) 
								AND (Logs.MachineLog.EmployeeId = @EmployeeId)  
								AND (LogTypeId=2)
								AND (datediff(MINUTE ,LogDateTime,@WorkEndTime)>-400)
								AND (datediff(MINUTE ,LogDateTime,@WorkEndTime)<400)
								order by LogTime)
------------------------------------------------------------------------------

	--Adnan's Update to get First InTime for the employee
	
SET @InTime=(SELECT  convert(datetime,convert(time,Logs.MachineLog.LogTime))
from Logs.MachineLog
where LogId=@InLogId)


--Adnan's Update to get First OutLogId for the employee
		
set @OutTime=(SELECT convert(datetime,convert(time,Logs.MachineLog.LogTime))
from Logs.MachineLog
where LogId=@OutLogId)

---------------------------------------------------------



if (SUBSTRING(CONVERT(VARCHAR(20),getdate(), 113), 13, 5)<SUBSTRING(CONVERT(VARCHAR(20),@WorkEndTime, 113), 13, 5)) and( convert(date,CONVERT(varchar(10),getdate(),103),103)=@AttendanceDate)

begin 
set @OutLogId=null
set @OutTime=null
end




        																																													    
    BEGIN TRY
		UPDATE Logs.AttendanceLog
		   SET InLogId =  @InLogId,
			   InTime = @InTime,
			   IsInPunchManual = 0,                                           
			   OutLogId = @OutLogId,
			   OutTime = @OutTime,
			   IsOutPunchManual = 0,
			   UpdateVersion = UpdateVersion + 1                                                                 	
		 WHERE Logs.AttendanceLog.LogId = @LogId   
		 
		 SET @RowEffected = @@RowCount
		 
		 RETURN    
    END TRY
    BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN      
    END CATCH

END








GO
