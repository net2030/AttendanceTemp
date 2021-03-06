USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Notifications].[SpEmailsGetAttendanceLogsForNotification]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Adnan Salah
-- Create date: 28/01/2020
-- Description:	Send Attendance Notification
-- =============================================
CREATE PROCEDURE [Notifications].[SpEmailsGetAttendanceLogsForNotification]

WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

    BEGIN TRY

	DECLARE @AttendanceDate AS Date
	DECLARE @EmployeeCode NvarChar(10)
	DECLARE @EmployeeName  NvarChar(50)
	DECLARE @EmployeeNameEn  NvarChar(50)
	DECLARE @EmailAddress NvarChar(50)

	DECLARE @CCEmailAddress NvarChar(50)
	DECLARE @BCCEmailAddress NvarChar(50)
	
	DECLARE @StatusId NvarChar(50)
	DECLARE @StatusName   NvarChar(30)
	DECLARE @StatusNameEn   NvarChar(30)



	DECLARE @InTime   NvarChar(30)
	DECLARE @OutTime   NvarChar(30)


	DECLARE @LateMinutes   NvarChar(30)

	CREATE TABLE #Logs(LogDate NvarChar(50) ,
	                   EmployeeCode  NvarChar(50),
	                   EmployeeName  NvarChar(50),
	          
	                   EmployeeNameEnglish  NvarChar(50),
					   EMailAddress  NvarChar(50),
					   CCEmailAddress  NvarChar(50),
					   BCCEmailAddress  NvarChar(50),
					   StatusName  NvarChar(50),
					   StatusNameEn  NvarChar(50),
					   InTime  NvarChar(50),
					   OutTime  NvarChar(50),
					   LateMinutes  NvarChar(50),
					   StatusId  int)


	DECLARE Curs_EmpAttendance CURSOR
    LOCAL FOR     
	                                     
    SELECT LogDate,
		BadgeNo,
		FirstName + '  ' + MiddleName + '  ' + LastName AS EmployeeName,
		EmployeeNameEnglish,
		EMailAddress,
		'' AS CCEmailAddress,
		'' AS BCCEmailAddress,
		StatusName ,
		StatusNameEn,
		InTime,
		OutTime,
		LateMinutes,
		StatusId

	FROM logs.vwAttendanceLog  
	WHERE EmployeeId=5-- StatusId  IN(45,65,70,75,110,111,120) 
    AND LogDate=Convert(Date,GETDate())

    FOR READ ONLY


	OPEN Curs_EmpAttendance
	FETCH NEXT FROM Curs_EmpAttendance INTO 
	@AttendanceDate,@EmployeeCode,@EmployeeName,@EmployeeNameEn,@EMailAddress,@CCEmailAddress,@BCCEmailAddress,@StatusName,@StatusNameEn,@InTime,@OutTime,@LateMinutes,@StatusId
	WHILE @@Fetch_Status = 0 
	BEGIN


	IF NOT EXISTS (SELECT StatusId 
	               FROM Notifications.Email 
				   WHERE IsEmailSent=1 
				   AND ToEmailAddress=@EmailAddress 
				   AND StatusId=@StatusId 
				   AND Convert(DATE,@AttendanceDate) = Convert(DATE,AddedDate) )
	
	INSERT INTO #Logs
	VALUES(dbo.FnUmAlquraYMD(@AttendanceDate),@EmployeeCode,@EmployeeName,@EmployeeNameEn,@EMailAddress,@CCEmailAddress,@BCCEmailAddress,@StatusName,@StatusNameEn,CONVERT(varchar(15),CAST(@InTime AS TIME),100),CONVERT(varchar(15),CAST(@OutTime AS TIME),100),@LateMinutes,@StatusId)
	

	FETCH NEXT FROM Curs_EmpAttendance INTO 
	@AttendanceDate,@EmployeeCode,@EmployeeName,@EmployeeNameEn,@EMailAddress,@CCEmailAddress,@BCCEmailAddress,@StatusName,@StatusNameEn,@InTime,@OutTime,@LateMinutes,@StatusId
	END
	

	SELECT * FROM #Logs


    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         
        RETURN    
    END CATCH
END




GO
