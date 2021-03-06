USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkSchedule_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert a new WorkSchedule
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkSchedule_Insert]
(
 @WorkScheduleId           int = NULL OUTPUT, 
 @WorkScheduleName         nvarchar(100) = NULL,
 @WorkScheduleNameEN       nvarchar(100) = '',
 @WorkScheduleTypeId       int = 1,
 @DepartmentId             int = NULL,  
 @ScheduleBegDate          date = NULL,
 @ScheduleEndDate          date = NULL,
 @IsEffectiveDuringHoliday bit = NULL,
 @IsPolicyApplied          bit = 0,
 @PolicyId                 int = NULL,
 @ShiftsCount              int = 1,
 @IsPeriodic               bit = 0,
 @PeriodLength             int = 7,
 @FirstPeriodStartDate     date = '2019-01-05',
 
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

    IF (@IsPolicyApplied = 1 AND @PolicyId IS NOT NULL)
    BEGIN
		IF NOT EXISTS(SELECT PolicyId 
						FROM  [Managements].[Policy]
					   WHERE PolicyId = @PolicyId)                    
		BEGIN
		  SET @RMsgId = 151 -- رقم السياسة غير صحيح أو غير موجود 
		  SET @FieldInError = 'PolicyId'
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
		INSERT INTO  [Managements].[WorkSchedule]
				   ([WorkScheduleName],
				    [WorkScheduleNameEN],
				    [WorkScheduleTypeId],
				    [DepartmentId],				    
				    [ScheduleBegDate],
				    [ScheduleEndDate],
				    [IsEffectiveDuringHoliday],
				    [IsPolicyApplied],
				    [PolicyId],
					[ShiftsCount],
					[IsPeriodic] ,
					[PeriodLength],
					[FirstPeriodStartDate],
				    [AddedUserAccountId],
				    [UpdatedUserAccountId],
				    [AddedDate],
				    [UpdatedDate])
			 VALUES
				   (@WorkScheduleName,
				    @WorkScheduleNameEN,
				    @WorkScheduleTypeId, 
				    @DepartmentId,
				    @ScheduleBegDate,
				    @ScheduleEndDate,
				    @IsEffectiveDuringHoliday,
				    @IsPolicyApplied,
				    @PolicyId,
					@ShiftsCount,
					@IsPeriodic,
					@PeriodLength,
					@FirstPeriodStartDate,
				    @UserAccountId,
				    @UserAccountId,				    
				    GETDATE(),
				    GETDATE())				        
				        
		-- Save The New ID Value
		SET @WorkScheduleId = SCOPE_IDENTITY()
		
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
