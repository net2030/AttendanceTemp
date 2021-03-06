USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpUpdateTimesFromMachineLog]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 23/09/2008
-- Description:	Update Attendance In & Out Time from Machine Log
-- =============================================
CREATE PROCEDURE [Logs].[SpUpdateTimesFromMachineLog]
(
 @LogId          int = NULL,
 @AttendanceDate datetime = NULL,
 @ShiftNo        INT = 1,
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
	DECLARE @WorkingMinute INT
	DECLARE @MachineId INT = NULL
------------------------------------------
--Adnan's Update to get First InLogId for the employee

SELECT @WorkBegTime=WorkStartTime,
       @WorkEndTime= WorkEndTime,
	   @WorkingMinute = WorkingMinutes 
FROM Logs.AttendanceLog 
WHERE LogId=@LogId

SET @InLogId=(SELECT TOP(1) Logs.MachineLog.LogId
			  FROM Logs.MachineLog
			  WHERE ABS(DATEDIFF(MINUTE,Logs.MachineLog.LogDateTime, @WorkBegTime) ) < @WorkingMinute
			    and (Logs.MachineLog.EmployeeId = @EmployeeId) 
				and (Logs.MachineLog.LogTypeId= @ShiftNo + (@ShiftNo - 1) )
			  ORDER BY LogTime)

/*
-- Check If the machine is correct
IF @InLogId IS NOT NULL
SELECT @MachineId = MachineId FROM Logs.MachineLog WHERE LogId=@InLogId

IF @MachineId IS NOT NULL 
IF  EXISTS (SELECT MachineId FROM Logs.EmployeeDeviceLink WHERE MachineId=@MachineId AND EmployeeId=@EmployeeId) OR @MachineId=1
*/
SELECT @InTime= Logs.MachineLog.LogDateTime
FROM Logs.MachineLog
WHERE logid=@InLogId

------------------Original------------------------------
    --SET @InLogId =  (SELECT TOP(1) PERCENT Logs.MachineLog.LogId 
				--	   FROM Logs.Machine 
				--	  	    INNER JOIN Logs.MachineType 
				--	     ON Logs.Machine.MachineTypeId = Logs.MachineType.MachineTypeId 
				--		    INNER JOIN Logs.MachineLog 
				--	     ON Logs.Machine.MachineId = Logs.MachineLog.MachineId                                          
				--      WHERE Logs.MachineType.IsUsedForAttendance = 1
				--	    AND Logs.MachineLog.LogDate = @AttendanceDate
				--	    AND Logs.MachineLog.IsValidRecord = 1
				--	    AND Logs.MachineLog.LogTypeId = 1
				--	    AND Logs.MachineLog.EmployeeId = @EmployeeId
				--   ORDER BY Logs.MachineLog.LogTime)
----------------------------------------------------------------------	


			
	--Adnan's Update to get First InLogId for the employee
	
--SET @InTime=(SELECT  convert(datetime,convert(time,Logs.MachineLog.LogTime))
--from Logs.MachineLog
--where Logs.MachineLog.LogId = @InLogId 
--)

			------------------Original------------------------------	   
    --SET @InTime = (SELECT TOP(1) PERCENT SUBSTRING(CAST(LogTime AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(LogTime AS NVARCHAR(30)),18 ,2) 
				--	 FROM Logs.Machine 
				--		  INNER JOIN Logs.MachineType 
				--	   ON Logs.Machine.MachineTypeId = Logs.MachineType.MachineTypeId 
				--		  INNER JOIN Logs.MachineLog 
				--	   ON Logs.Machine.MachineId = Logs.MachineLog.MachineId 
				--		  INNER JOIN Employees.Employee 
				--	   ON Logs.MachineLog.EmployeeId = Employees.Employee.EmployeeId                                          
				--    WHERE Logs.MachineType.IsUsedForAttendance = 1
				--	  AND Logs.MachineLog.LogDate = @AttendanceDate
				--	  AND Logs.MachineLog.LogTypeId = 1
				--	  AND Logs.MachineLog.IsValidRecord = 1
				--	  AND Logs.MachineLog.EmployeeId = @EmployeeId
				-- ORDER BY Logs.MachineLog.LogTime)

    				
    SET @IsInPunchManual = (SELECT TOP(1)  Logs.MachineLog.IsManualRecord 
							 FROM Logs.Machine 
								  INNER JOIN Logs.MachineType 
							   ON Logs.Machine.MachineTypeId = Logs.MachineType.MachineTypeId 
								  INNER JOIN Logs.MachineLog 
							   ON Logs.Machine.MachineId = Logs.MachineLog.MachineId                                          
						    WHERE Logs.MachineType.IsUsedForAttendance = 1
							  AND Logs.MachineLog.LogDate = @AttendanceDate
							  AND Logs.MachineLog.LogTypeId = 1
							  AND Logs.MachineLog.IsValidRecord = 1
							  AND Logs.MachineLog.EmployeeId = @EmployeeId
						 ORDER BY Logs.MachineLog.LogTime)
				
				
				
------------------------------------------------------------

--Adnan's Update to get First InLogId for the employee
SET @OutLogId=(SELECT TOP(1) Logs.MachineLog.LogId
			   FROM Logs.MachineLog
			  WHERE ABS(DATEDIFF(MINUTE,Logs.MachineLog.LogDateTime, @WorkEndTime) ) < @WorkingMinute
			    and (Logs.MachineLog.EmployeeId = @EmployeeId) 
				and (Logs.MachineLog.LogTypeId=@ShiftNo * 2 )
			   ORDER BY LogTime DESC)

/*
-- Check If the machine is correct
IF @OutLogId IS NOT NULL
SELECT @MachineId = MachineId FROM Logs.MachineLog WHERE LogId=@OutLogId

IF @MachineId IS NOT NULL 
IF  EXISTS (SELECT MachineId FROM Logs.EmployeeDeviceLink WHERE MachineId=@MachineId AND EmployeeId=@EmployeeId) OR @MachineId=1
*/
SELECT @OutTime= Logs.MachineLog.LogDateTime
from Logs.MachineLog
where logid=@OutLogId




------------------------------------------------------------------------------


------------------Original------------------------------
	--SET @OutLogId = (SELECT TOP(1) PERCENT Logs.MachineLog.LogId 
	--				   FROM Logs.Machine 
	--				   INNER JOIN Logs.MachineType 
	--				      ON Logs.Machine.MachineTypeId = Logs.MachineType.MachineTypeId 
	--				   	     INNER JOIN Logs.MachineLog 
	--					  ON Logs.Machine.MachineId = Logs.MachineLog.MachineId                                          
	--				   WHERE Logs.MachineType.IsUsedForAttendance = 1
	--					 AND Logs.MachineLog.LogDate = @AttendanceDate
	--					 AND Logs.MachineLog.LogTypeId = 2
	--					 AND Logs.MachineLog.IsValidRecord = 1
	--					 AND Logs.MachineLog.EmployeeId = @EmployeeId
	--			    ORDER BY Logs.MachineLog.LogTime DESC)
	
	-------------------------------------------------------
	
	
	--Adnan's Update to get First OutLogId for the employee
	
--set @OutTime=(SELECT  convert(datetime,convert(time,Logs.MachineLog.LogTime))
--from Logs.MachineLog
--where logid=@OutLogId)
---------------------------------------------------------
set @WorkEndTime=(select WorkEndTime from Logs.AttendanceLog where LogId=@LogId) 
--if (SUBSTRING(CONVERT(VARCHAR(20),getdate(), 113), 13, 5)<SUBSTRING(CONVERT(VARCHAR(20),@WorkEndTime, 113), 13, 5)) and( convert(date,CONVERT(varchar(10),getdate(),103),103)=@AttendanceDate)
IF GETDATE() < @WorkEndTime
begin 
set @OutLogId=null
set @OutTime=null
end


	--	------------------Original------------------------------	
	
					
	--SET @OutTime = (SELECT TOP(1) PERCENT SUBSTRING(CAST(LogTime AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(LogTime AS NVARCHAR(30)),18 ,2)
	--  			      FROM Logs.Machine 
	--				       INNER JOIN Logs.MachineType 
	--				    ON Logs.Machine.MachineTypeId = Logs.MachineType.MachineTypeId 
	--				       INNER JOIN Logs.MachineLog 
	--					ON Logs.Machine.MachineId = Logs.MachineLog.MachineId                                          
	--			     WHERE Logs.MachineType.IsUsedForAttendance = 1
	--				   AND Logs.MachineLog.LogDate = @AttendanceDate
	--				   AND Logs.MachineLog.LogTypeId = 2
	--				   AND Logs.MachineLog.IsValidRecord = 1
	--				   AND Logs.MachineLog.EmployeeId = @EmployeeId
	--			  ORDER BY Logs.MachineLog.LogTime DESC)

------------------------------------------------------------------------


	SET @IsOutPunchManual = (SELECT TOP(1)  Logs.MachineLog.IsManualRecord 
							   FROM Logs.Machine 
								    INNER JOIN Logs.MachineType 
								 ON Logs.Machine.MachineTypeId = Logs.MachineType.MachineTypeId 
								    INNER JOIN Logs.MachineLog 
								 ON Logs.Machine.MachineId = Logs.MachineLog.MachineId                                         
							  WHERE Logs.MachineType.IsUsedForAttendance = 1
							    AND Logs.MachineLog.LogDate = @AttendanceDate
							    AND Logs.MachineLog.LogTypeId = 2
							    AND Logs.MachineLog.IsValidRecord = 1
							    AND Logs.MachineLog.EmployeeId = @EmployeeId
					       ORDER BY Logs.MachineLog.LogTime DESC)

    IF @IsInPunchManual IS NULL
    BEGIN
      SET @IsInPunchManual = 0
    END
    IF @IsOutPunchManual IS NULL
    BEGIN
      SET @IsOutPunchManual = 0
    END
        																																													    
    BEGIN TRY
		UPDATE Logs.AttendanceLog
		   SET InLogId =  @InLogId,
			   InTime = @InTime,
			   IsInPunchManual = @IsInPunchManual,                                           
			   OutLogId = @OutLogId,
			   OutTime = @OutTime,
			   IsOutPunchManual = @IsOutPunchManual,
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
