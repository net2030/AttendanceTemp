USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpCheckVerificationLog]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 25-02-2018
-- Description:	Check The Verification Log
-- =============================================
CREATE PROCEDURE [Logs].[SpCheckVerificationLog] 

AS
BEGIN
   SET NOCOUNT ON;
    
	 
	DECLARE @EmployeeId int
	DECLARE @AttendanceDate Date
	DECLARE @LogId INT
    DECLARE @VerificationLogStartTime DATETIME     
	DECLARE @VerificationLogEndTime DATETIME     



	SELECT @VerificationLogStartTime = VerificationLogStartTime , @VerificationLogEndTime= VerificationLogEndTime
	FROM Account WHERE AccountId=1

	SET @AttendanceDate = CONVERT(DATE,GETDATE()) --'2018-02-21'

	SET @VerificationLogStartTime =  CAST(CAST('1900-01-01' as nvarchar(10)) + 
										  (SUBSTRING(CAST(@VerificationLogStartTime AS NVARCHAR(30)),12 ,6) + ' ' + 
										   SUBSTRING(CAST(@VerificationLogStartTime AS NVARCHAR(30)),18 ,2)) as datetime)
    
	SET @VerificationLogEndTime =  CAST(CAST('1900-01-01' as nvarchar(10)) + 
										  (SUBSTRING(CAST(@VerificationLogEndTime AS NVARCHAR(30)),12 ,6) + ' ' + 
										   SUBSTRING(CAST(@VerificationLogEndTime AS NVARCHAR(30)),18 ,2)) as datetime)

    /*****************************************************
	   Cursor Declaration
	 *****************************************************/      
     DECLARE Curs_Logs CURSOR
     LOCAL FOR                                          
     SELECT EmployeeId,LogId FROM Logs.AttendanceLog WHERE LogDate = @AttendanceDate
     FOR READ ONLY

	 /*****************************************************
	   Open Cursor
	 *****************************************************/      
     OPEN Curs_Logs
     FETCH NEXT FROM Curs_Logs INTO @EmployeeId, @LogId
     WHILE @@Fetch_Status = 0 
     BEGIN

	 IF EXISTS(SELECT LogID 
	           FROM Logs.MachineLog 
			   WHERE EmployeeId = @EmployeeId
			   AND LogDate = @AttendanceDate
			   AND LogTime >= @VerificationLogStartTime 
			   AND LogTime <= @VerificationLogEndTime )
	  BEGIN	   
         UPDATE Logs.AttendanceLog SET VerificationLog = 1 WHERE LogId = @LogId
		 -- PRINT 'OK' + @LogId
	  END
	  ELSE
	  BEGIN
	     UPDATE Logs.AttendanceLog SET VerificationLog = 0 WHERE LogId = @LogId
		 --PRINT 'NOT OK '  + CAST(@LogId AS CHAR)
	  END

	

	 FETCH NEXT FROM Curs_Logs INTO @EmployeeId, @LogId
	 END

END

GO
