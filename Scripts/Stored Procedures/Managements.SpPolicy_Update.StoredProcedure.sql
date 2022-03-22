USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpPolicy_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Update Policy
-- =============================================
CREATE PROCEDURE [Managements].[SpPolicy_Update]
(
 @PolicyId                 int = NULL, 
 @PolicyName               nvarchar(100) = NULL,
 @PolicyNameEN               nvarchar(100) = '',
 @MarkObsentDuration       int = NULL,
 @LateInMinutes            int = NULL,
 @LateOutMinutes           int = NULL,
 @EarlyInMinutes           int = NULL,
 @EarlyOutMinutes          int = NULL,
 @LateLimitPerMonthMinutes int = NULL,
 @DepartmentId             int = NULL, 

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

    IF NOT EXISTS(SELECT PolicyId 
                    FROM  [Managements].[Policy]
                   WHERE PolicyId = @PolicyId)                    
    BEGIN
	  SET @RMsgId = 151 -- رقم سياسة الدوام غير صحيح أو غير موجود 
	  SET @FieldInError = 'DepartmentId'
	  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END
    
   -- IF NOT EXISTS(SELECT DepartmentId 
   --                 FROM  [Employees].[Department]
   --                WHERE DepartmentId = @DepartmentId)                    
   -- BEGIN
	  --SET @RMsgId = 116 -- رقم القسم/الإدارة غير صحيح أو غير موجود 
	  --SET @FieldInError = 'DepartmentId'
	  --SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  --RETURN        
   -- END
        

    BEGIN TRY	
		UPDATE  [Managements].[Policy]
		   SET [PolicyName] = @PolicyName,
		       [PolicyNameEN] = @PolicyNameEN,
			   [MarkObsentDuration] = @MarkObsentDuration,
			   [LateInMinutes] = @LateInMinutes,
			   [LateOutMinutes] = @LateOutMinutes,
			   [EarlyInMinutes] = @EarlyInMinutes,
			   [EarlyOutMinutes] = @EarlyOutMinutes,
			   [LateLimitPerMonthMinutes] = @LateLimitPerMonthMinutes,
			   [DepartmentId] = @DepartmentId,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate] = GETDATE()
	     WHERE PolicyId = @PolicyId
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
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [PolicyId] 
		                                      FROM  [Managements].[Policy]
		                                     WHERE [PolicyId] = @PolicyId
	                                           AND [VersionNo]  = @VersionNo))
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
