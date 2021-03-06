USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkSchedule_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Update WorkSchedule
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkSchedule_Update]
(
 @WorkScheduleId           int = NULL, 
 @WorkScheduleName         nvarchar(100) = NULL,
 @WorkScheduleNameEN       nvarchar(100) = '',
 @WorkScheduleTypeId       int = NULL,
 @DepartmentId             int = NULL,  
 @ScheduleBegDate          date = NULL,
 @ScheduleEndDate          date = NULL,
 @IsEffectiveDuringHoliday bit = NULL,
 @IsPolicyApplied          bit = NULL,
 @PolicyId                 int = NULL,
 @ShiftsCount              int = 1,
 @IsPeriodic               bit = 0,
 @PeriodLength             int = 7,
 @FirstPeriodStartDate     date = '2019-01-05',
 @VersionNo                timestamp = NULL, 
 
 @UserAccountId            int = NULL,
 @FieldInError             nvarchar(50) = '' OUTPUT,
 @RMsgId                   int = NULL OUTPUT,
 @RMessage                 nvarchar(200) = '' OUTPUT, 
 @RC                       int = NULL OUTPUT
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

	IF NOT EXISTS(SELECT WorkScheduleId 
					FROM  [Managements].[WorkSchedule]
				   WHERE WorkScheduleId = @WorkScheduleId)                    
	BEGIN
	  SET @RMsgId = 156 -- رقم جدول العمل غير صحيح أو غير موجود 
	  SET @FieldInError = 'WorkScheduleId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
	END
	IF @PolicyId = 0 
	BEGIN
	  SET @PolicyId = NULL
	END
		
    IF @PolicyId IS NOT NULL
    BEGIN
		IF NOT EXISTS(SELECT PolicyId 
						FROM  [Managements].[Policy]
					   WHERE PolicyId = @PolicyId)                    
		BEGIN
		  SET @RMsgId = 151 -- رقم السياسة غير صحيح أو غير موجود 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END
		ELSE
		BEGIN
		  SET @IsPolicyApplied = 1
		END
	END	
	ELSE
	BEGIN
      SET @IsPolicyApplied = 0
      SET @PolicyId = NULL
	END	
    
    BEGIN TRY
		UPDATE  [Managements].[WorkSchedule]
		   SET [WorkScheduleName] = @WorkScheduleName,
		       [WorkScheduleNameEN] = @WorkScheduleNameEN,
		       [WorkScheduleTypeId] = @WorkScheduleTypeId,
		       [DepartmentId] = @DepartmentId,
		       [ScheduleBegDate] = @ScheduleBegDate,
			   [ScheduleEndDate] = @ScheduleEndDate,
			   [IsEffectiveDuringHoliday] = @IsEffectiveDuringHoliday,
			   [IsPolicyApplied] = @IsPolicyApplied,
			   [PolicyId] = @PolicyId, 
			   [ShiftsCount] = @ShiftsCount,
			   [IsPeriodic] = @IsPeriodic,
			   [PeriodLength] = @PeriodLength,
			   [FirstPeriodStartDate] = @FirstPeriodStartDate,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate] = GETDATE()				        
	     WHERE WorkScheduleId = @WorkScheduleId
	       AND VersionNo = @VersionNo
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
           -- Delete WorkSchedule Days

		   DELETE FROM Managements.WorkScheduleDay WHERE WorkScheduleId = @WorkScheduleId

		  RETURN
		END
		ELSE
		IF (@RowEffected = 0 AND Not EXISTS(SELECT WorkScheduleId 
		                                      FROM  [Managements].[WorkSchedule]
		                                     WHERE WorkScheduleId   = @WorkScheduleId
	                                           AND [VersionNo]    = @VersionNo))
		BEGIN
		  SET @RMsgId = 5  -- تم تحديث السجل من قبل مستخدم آخر
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
