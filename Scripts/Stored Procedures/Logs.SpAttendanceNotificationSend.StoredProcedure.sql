USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendanceNotificationSend]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Attendance Team
-- Create date: 25/12/2014
-- Description:	Send Attendance Notification
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendanceNotificationSend]

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

	DECLARE @StatusName   NvarChar(30)
	DECLARE @StatusNameEn   NvarChar(30)
	DECLARE @MSGTitle VARCHAR(100)='Attendance Notification'
	DECLARE	@ManagersEmail NVARCHAR(50)=''
	DECLARE	@MSGBody NVARCHAR(2000)
	DECLARE	@MSGBodyAr NVARCHAR(2000)
	DECLARE	@MSGBodyEn NVARCHAR(2000)

	DECLARE Curs_Department CURSOR
    LOCAL FOR                                          
    SELECT LogDate,EmployeeId,FirstName + '  ' + MiddleName + '  ' + LastName AS EmployeeName,EmployeeNameEnglish,EMailAddress,StatusName ,StatusNameEn
	FROM logs.vwAttendanceLog  
	WHERE StatusId  IN(45,75,110,120,70,65) AND EmployeeId not in(1,2,24)
	AND LogDate=Convert(Date,GETDate())
    FOR READ ONLY


	OPEN Curs_Department
	FETCH NEXT FROM Curs_Department INTO 
	@AttendanceDate,@EmployeeCode,@EmployeeName,@EmployeeNameEn,@EMailAddress,@StatusName,@StatusNameEn
	WHILE @@Fetch_Status = 0 
	BEGIN

	SET @MSGBodyAr='<html><body><table dir="rtl"> <tr> <td dir="ltr"> <div dir="rtl" ><span  style="color:#17365d"><H5>'
	SET @MSGBodyAr=@MSGBodyAr + 'الموظف / ـة/' + @EmployeeName
	SET	@MSGBodyAr=@MSGBodyAr	 +'<BR><BR>'
	SET @MSGBodyAr=@MSGBodyAr + ' نفيدكم بأن حالة دوامكم ليوم   ( ' + CONVERT(NVARCHAR(10),@AttendanceDate,105)
	SET @MSGBodyAr=@MSGBodyAr +')  هي:-  ' + @StatusName 
	SET @MSGBodyAr=@MSGBodyAr + '</Span></H5></Font>'
	
	SET	@MSGBodyAr=@MSGBodyAr	 +'<BR><BR> <BR><BR> <BR><BR> <BR>'
	SET	@MSGBodyAr=@MSGBodyAr + '<font color="red"> <H4> هذا الايميل تم انشاءه الياً , يرجى عدم الرد عليه.</H4></font>'
	SET	@MSGBodyAr=@MSGBodyAr + '</div> </td>'

	SET	@MSGBodyAr=@MSGBodyAr + ' <td dir="ltr"> <div Style="float:left;margin-right:100px">'

	SET @MSGBodyAr=@MSGBodyAr + 'Dear/' + @EmployeeNameEn
	SET	@MSGBodyAr=@MSGBodyAr	 +'<BR><BR>'
	SET @MSGBodyAr=@MSGBodyAr + ' Your Attendance Status on   ( ' + CONVERT(NVARCHAR(10),@AttendanceDate,105)
	SET @MSGBodyAr=@MSGBodyAr +')  Is:-  ' + @StatusNameEn 
	SET @MSGBodyAr=@MSGBodyAr + '</Span></H5></Font>'
	
	SET	@MSGBodyAr=@MSGBodyAr	 +'<BR><BR> <BR><BR> <BR><BR> <BR>'
	SET	@MSGBodyAr=@MSGBodyAr + '<font color="red"> <H4> This is system Email , please do not reply.</H4></font>'
	SET	@MSGBodyAr=@MSGBodyAr + '</div>'

	SET	@MSGBodyAr=@MSGBodyAr + '</td></tr></table></body></html>'

	SET @EmailAddress=@EmailAddress +';'+ @ManagersEmail

	EXEC msdb.dbo.sp_send_dbmail @profile_name='MAS',
						@recipients=@EmailAddress,
						@subject=@MSGTitle,
						@body=@MSGBodyAr,
						@body_format ='HTML'

	FETCH NEXT FROM Curs_Department INTO 
	@AttendanceDate,@EmployeeCode,@EmployeeName,@EmployeeNameEn,@EMailAddress,@StatusName,@StatusNameEn
	END

    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         
        RETURN    
    END CATCH
END




GO
