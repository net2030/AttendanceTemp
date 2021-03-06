USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffPolicy_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert New Work Exception Policy
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffPolicy_Update]
(
 @TimeOffPolicyId   int = NULL , 
 @TimeOffPolicyName nvarchar(50) = NULL,
 @TypeId               int=NULL,
 @EffectiveDate      date=NULL,
 @VersionNo          timestamp,
 @UserAccountId        int = NULL,
 @FieldInError         nvarchar(50) = '' OUTPUT,
 @RMsgId               int = NULL OUTPUT,
 @RMessage             nvarchar(200) = '' OUTPUT, 
 @RC                   int = NULL OUTPUT
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
     
	IF Not  EXISTS(SELECT TimeOffPolicyId 
						FROM [Managements].TimeOffPolicy
					   WHERE TimeOffPolicyId = @TimeOffPolicyId)                    
		BEGIN
		  SET @RMsgId = 162 -- السياسة مكررة
		  SET @FieldInError = 'WorkScheduleId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END	    
	   
    BEGIN TRY	
		UPDATE  [Managements].[TimeOffPolicy]
				   SET [TimeOffPolicyName]=@TimeOffPolicyName
				   ,TypeId=@TypeId
				   ,EffectiveDate=@EffectiveDate
				   ,[UpdatedUserAccountId]=@UserAccountId
				   ,[UpdatedDate]=GetDate()
         WHERE TimeOffPolicyId=@TimeOffPolicyId
	

		-- Save The New ID Value
		SET @TimeOffPolicyId = SCOPE_IDENTITY()				        
		
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
