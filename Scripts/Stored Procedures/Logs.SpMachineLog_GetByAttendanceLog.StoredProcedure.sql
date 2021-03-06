USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpMachineLog_GetByAttendanceLog]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Attendance Team
-- Create date: 6/5/2011
-- Description:	get machine log by id
-- =============================================
CREATE PROCEDURE [Logs].[SpMachineLog_GetByAttendanceLog]
(
 @LogId       int 
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;
    DECLARE @EmployeeId INT
	DECLARE @LogDate  Date 
    BEGIN TRY   
     
    SELECT @EmployeeId=EmployeeId,@Logdate=LogDate FROM Logs.AttendanceLog Where LogId=@LogId
		      
       SELECT  LogId,
	          SUBSTRING(CAST(LogTime AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(LogTime AS NVARCHAR(30)),18 ,2) AS LogTime,
	        
	          LogTypeName,
			  MachineName,
			  IPAddress,
			  Location,
			  ManualRecord,
			  ValidRecord
	   FROM Logs.vwMachineLog
	   Where LogDate=@LogDate
	     AND EmployeeId=@EmployeeId
		 order by Convert(time,LogTime)

		RETURN		 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
