USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacationNotificationSend]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Adnan Salah
-- Create date: 25/12/2014
-- Description:	Send Attendance Notification
-- =============================================
CREATE PROCEDURE [Managements].[SpVacationNotificationSend]

WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

    BEGIN TRY
	DECLARE @ManagerId Int
	DECLARE @ManagerName Nvarchar(50)
	DECLARE @EmailAddress NvarChar(50)

	DECLARE @EmployeeName Nvarchar(50)
	DECLARE @VacationType Nvarchar(50)
	DECLARE @VacationBeg Nvarchar(50)
	DECLARE @VacationEnd Nvarchar(50)
	DECLARE @DateOfReturn Nvarchar(50)
	DECLARE @Notes  NvarChar(200)

	DECLARE @MSGBodyAr NvarChar(Max)


	DECLARE Curs_Managers CURSOR
    LOCAL FOR 
	SELECT DISTINCT AE1.[FirstName] + ' ' + AE1.[MiddleName] + ' ' + AE1.[LastName] AS EmployeeName,
		  AE.ManagerId  ,
		  AE1.EMailAddress
		  FROM [Managements].[Vacation]  G
		  INNER JOIN Employees.Employee AE ON G.EmployeeId=AE.EmployeeId
		  JOIN Employees.Employee AE1 ON AE.ManagerId=AE1.EmployeeId
		  WHERE G.IsApproved=0 AND G.IsRejected=0
		  --AND Convert(nvarchar(10),G.[VacationBegDate],102)=Convert(nvarchar(10),GETDATE(),102)	 
	

	OPEN Curs_Managers
		 FETCH NEXT FROM Curs_Managers INTO 
		 @ManagerName,@ManagerId,@EmailAddress
		 

		 WHILE @@Fetch_Status = 0 
		 BEGIN 

		 SET @MSGBodyAr='<html dir="rtl"><body>'
		 SET @MSGBodyAr=@MSGBodyAr + 'الأخ/' + @ManagerName
		 SET @MSGBodyAr=@MSGBodyAr + '</br>' + '<br> نحيطكم علما بوجود إجازات تنتظر إعتمادكم لها  <br> '  
		 SET @MSGBodyAr=@MSGBodyAr	 +'<BR><BR>'
		 SET @MSGBodyAr=@MSGBodyAr + '<table Border="1">'
		 SET @MSGBodyAr=@MSGBodyAr + '<tr>'
		 SET @MSGBodyAr=@MSGBodyAr + '<th> إسم الموظف </td>'
		 SET @MSGBodyAr=@MSGBodyAr + '<th> نوع الإجازة </td>'
		 SET @MSGBodyAr=@MSGBodyAr + '<th> بداية الإجازة </td>'
		 SET @MSGBodyAr=@MSGBodyAr + '<th> نهاية الإجازة </td>'
		 SET @MSGBodyAr=@MSGBodyAr + '<th> تاريخ المباشرة  </td>'
		 SET @MSGBodyAr=@MSGBodyAr + '<th> ملاحظات </td>'
		 SET @MSGBodyAr=@MSGBodyAr + '</tr>'
		 

					     DECLARE Curs_Vacationes CURSOR
					     LOCAL FOR                                          
                          
					     SELECT AE.[FirstName] + ' ' + AE.[MiddleName] + ' ' + AE.[LastName] AS EmployeeName
						  ,GT.[TypeName]
						  , Convert(nvarchar(10),G.EffectiveDate,102)  AS VacationBegDate
						  , Convert(nvarchar(10),G.DateExpire,102)AS VacationEndDate
						  , Convert(nvarchar(10),G.DateExpire,102)AS DateOfReturn
						  ,G.[Note]
					     FROM [Managements].[Vacation]  G
					     INNER JOIN Employees.Employee AE ON G.EmployeeId=AE.EmployeeId
					     INNER JOIN Managements.VacationType GT ON G.TypeId=GT.TypeId
					     WHERE G.IsApproved=0 AND G.IsRejected=0
					     AND AE.ManagerId=@ManagerId
					  --AND Convert(nvarchar(10),G.[VacationBegDate],102)=Convert(nvarchar(10),GETDATE(),102)	 
       				       
						 FOR READ ONLY
						 
						 OPEN Curs_Vacationes
						 FETCH NEXT FROM Curs_Vacationes INTO 
						 @EmployeeName,@VacationType,@VacationBeg,@VacationEnd,@DateOfReturn,@Notes

						 

							 WHILE @@Fetch_Status = 0 
							 BEGIN 

						        SET @MSGBodyAr=@MSGBodyAr + '<tr>'
								SET @MSGBodyAr=@MSGBodyAr + '<td  style="padding:4px 5px 6px 7px">' + @EmployeeName + '</td>'
								SET @MSGBodyAr=@MSGBodyAr + '<td  style="padding:4px 5px 6px 7px">' + @VacationType + '</td>'
								SET @MSGBodyAr=@MSGBodyAr + '<td  style="padding:4px 5px 6px 7px">' + @VacationBeg + '</td>'
								SET @MSGBodyAr=@MSGBodyAr + '<td  style="padding:4px 5px 6px 7px">' + @VacationEnd + '</td>'
								SET @MSGBodyAr=@MSGBodyAr + '<td  style="padding:4px 5px 6px 7px">' + @DateOfReturn + '</td>'
								SET @MSGBodyAr=@MSGBodyAr + '<td  style="padding:4px 5px 6px 7px">' + @Notes + '</td>'
								SET @MSGBodyAr=@MSGBodyAr + '</tr>'

								 FETCH NEXT FROM Curs_Vacationes INTO 
								 @EmployeeName,@VacationType,@VacationBeg,@VacationEnd,@DateOfReturn,@Notes

								 

				            END
         SET @MSGBodyAr=@MSGBodyAr + '</table>'
		 SET @MSGBodyAr=@MSGBodyAr	 +'<BR><BR>'
		 SET @MSGBodyAr=@MSGBodyAr	 +'للموافقة ادخل على الرابط التالي <BR>'
		  SET @MSGBodyAr=@MSGBodyAr + 'http://Attendance.mastechnology.net/Attendance/Attendance/VacationsListForApproval.aspx'
		 SET @MSGBodyAr=@MSGBodyAr	 +'<BR><BR> <BR><BR> <BR>'
	     SET @MSGBodyAr=@MSGBodyAr + '<font color="red"> <H4> هذا الايميل تم انشاءه الياً , يرجى عدم الرد عليه.</H4></font>'
         SET @MSGBodyAr=@MSGBodyAr + '</body></html>'
		 print @MSGBodyAr

	    EXEC msdb.dbo.sp_send_dbmail @profile_name='MAS',
						@recipients=@EmailAddress,
						@subject='',
						@body=@MSGBodyAr,
						@body_format ='HTML'
         

		 CLOSE Curs_Vacationes
		 DEALLOCATE Curs_Vacationes
	     FETCH NEXT FROM Curs_Managers INTO 
		 @ManagerName,@ManagerId,@EmailAddress
         END

    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         
        RETURN    
    END CATCH
END








GO
