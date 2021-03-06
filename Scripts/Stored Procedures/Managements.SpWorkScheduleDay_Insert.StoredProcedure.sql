USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkScheduleDay_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert new Work Schedule Day
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkScheduleDay_Insert]
(
 @WorkScheduleId int = NULL,
 @ShiftNo        int = 1,
 @WeekDayNo      int = NULL,
 @IsWeekendDay   bit = NULL,
 @WorkBegTime    datetime = NULL,
 @WorkEndTime    datetime = NULL,

 @UserAccountId  int = NULL, 
 @FieldInError   nvarchar(50) = '' OUTPUT,
 @RMsgId         int = NULL OUTPUT,
 @RMessage       nvarchar(200) = '' OUTPUT, 
 @RC             int = NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
    
    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0


    BEGIN TRY
		IF NOT EXISTS(SELECT WorkScheduleId 
						FROM  [Managements].[WorkSchedule]
					   WHERE WorkScheduleId = @WorkScheduleId)                    
		BEGIN
		  SET @RMsgId = 156 -- رقم جدول العمل غير صحيح أو غير موجود 
		  SET @FieldInError = 'WorkScheduleId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END	         	

		/**
		IF NOT EXISTS(SELECT WeekDayNo 
						FROM  [Managements].[WeekDay]
					   WHERE WeekDayNo = @WeekDayNo)                    
		BEGIN
		  SET @RMsgId = 158 -- رقم يوم الأسبوع غير صحيح أو غير موجود 
		  SET @FieldInError = 'WeekDayNo'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END	
		**/

		SET @WorkBegTime = CAST(CAST('1900-01-01' as nvarchar(10)) + 
						  (SUBSTRING(CAST(@WorkBegTime AS NVARCHAR(30)),12 ,6) + ' ' + 
						   SUBSTRING(CAST(@WorkBegTime AS NVARCHAR(30)),18 ,2)) as datetime)

		SET @WorkEndTime = CAST(CAST('1900-01-01' as nvarchar(10)) + 
						  (SUBSTRING(CAST(@WorkEndTime AS NVARCHAR(30)),12 ,6) + ' ' + 
						   SUBSTRING(CAST(@WorkEndTime AS NVARCHAR(30)),18 ,2)) as datetime)


      
			INSERT INTO  [Managements].[WorkScheduleDay]
					   ([WorkScheduleId],
						[ShiftNo],
						[WeekDayNo],
						[IsWeekendDay],
						[WorkBegTime],
						[WorkEndTime],
						[AddedUserAccountId],
						[UpdatedUserAccountId],
						[AddedDate],
						[UpdatedDate])
				 VALUES
					   (@WorkScheduleId,
						@ShiftNo,
						@WeekDayNo,
						@IsWeekendDay,
						@WorkBegTime,
						@WorkEndTime,
						@UserAccountId,
						@UserAccountId, 
						GETDATE(),
						GETDATE())
           
		  
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
		END
		ELSE
		BEGIN -- Failed
		  SET @RMsgId = 2 --  فشلت عملية تحديث البيانات
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
       END      
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         SET @RMsgId = 3  --  حدث خطاء في إجراء قاعدة البيانات - الرجاء الإتصال بمسئول النظام للمساعدة
		 SET @RMessage = (SELECT MsgText 
		                    FROM  [common].[CustomMessage] 
		                   WHERE [MsgId] = @RMsgId)
        RETURN    
    END CATCH
END






GO
