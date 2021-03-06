USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffPolicyDetail_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Update Work Schedule Day
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffPolicyDetail_Update]
(
 @TimeOffPolicyDetailId INT,
 @TimeOffPolicyId INT,
 @GatepassTypeId INT,
 @EarnPeriodId INT,
 @ResetToZeroPeriodId INT,
 @InitialSetToMinutes DECIMAL(6,2),
 @ResetToMinutes      DECIMAL(6,2),
 @EarnMinutes         DECIMAL(6,2),
 @EffectiveDate       Date,
 @IsCarryForward      bit=0,
 @MinValue        DECIMAL(6,2),
 @MaxValue        DECIMAL(6,2),
 @VersionNo      timestamp = NULL,

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
		IF NOT EXISTS(SELECT TimeOffPolicyId 
						FROM [Managements].TimeOffPolicy
					   WHERE TimeOffPolicyId = @TimeOffPolicyId)                    
		BEGIN
		  SET @RMsgId = 156 -- رقم جدول العمل غير صحيح أو غير موجود 
		  SET @FieldInError = 'WorkScheduleId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END	         	      	

		
		
		
		        
		UPDATE [Managements].TimeOffPolicyDetail
		   SET [TimeOffPolicyId] = @TimeOffPolicyId,
			   [GatepassTypeId] = @GatepassTypeId,
			   [EarnPeriodId] = @EarnPeriodId,
			   [ResetToZeroPeriodId]=@ResetToZeroPeriodId,
			   [InitialSetToMinutes]=@InitialSetToMinutes,
			   [ResetToMinutes]=@ResetToMinutes,
			   [EarnMinutes]=@EarnMinutes,
			   EffectiveDate=@EffectiveDate,
			   IsCarryForward=@IsCarryForward,
			   MinValue =@MinValue ,
			   MaxValue =@MaxValue ,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate] = GETDATE()
		 WHERE TimeOffPolicyDetailId = @TimeOffPolicyDetailId
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
		  RETURN
		END
		ELSE
		IF (@RowEffected = 0 AND Not EXISTS(SELECT TimeOffPolicyDetailId 
		                                      FROM [Managements].TimeOffPolicyDetail
											 WHERE TimeOffPolicyDetailId = @TimeOffPolicyDetailId
											   AND VersionNo = @VersionNo))
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
