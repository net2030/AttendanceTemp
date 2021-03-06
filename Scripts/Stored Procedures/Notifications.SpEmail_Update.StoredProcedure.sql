USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Notifications].[SpEmail_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	update a email
-- =============================================
CREATE PROCEDURE [Notifications].[SpEmail_Update]
(
 @EmailId          int = NULL, 
 @ToEmailAddress   nvarchar(max) = NULL,
 @CCEmailAddress   nvarchar(max) = NULL, 
 @BCCEmailAddress  nvarchar(max) = NULL, 
 @FromEmailAddress nvarchar(255) = NULL,
 @EmailSubject     nvarchar(max) = NULL,
 @EmailBody        nvarchar(max) = NULL,
 @IsEmailSent      bit = 0,
 
 @VersionNo        timestamp = NULL, 
 @UserAccountId    int = NULL,
 @FieldInError     nvarchar(50) = '' OUTPUT,
 @RMsgId           int = NULL OUTPUT,
 @RMessage         nvarchar(200) = '' OUTPUT, 
 @RC               int = NULL OUTPUT
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
		UPDATE  [Notifications].[Email]
		   SET [ToEmailAddress] = @ToEmailAddress,
			   [CCEmailAddress] = @CCEmailAddress,
			   [BCCEmailAddress] = @BCCEmailAddress,
			   [FromEmailAddress] = @FromEmailAddress,
			   [EmailSubject] = @EmailSubject,
			   [EmailBody] = @EmailBody,
			   [IsEmailSent] = @IsEmailSent,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate] = GETDATE()
		 WHERE EmailId = @EmailId
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
		IF (@RowEffected = 0 AND Not EXISTS(SELECT EmailId 
		                                      FROM  [Notifications].[Email]
		                                     WHERE EmailId = @EmailId
	                                           AND [VersionNo] = @VersionNo))
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
