USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Notifications].[SpEmail_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert a new email
-- =============================================
CREATE PROCEDURE [Notifications].[SpEmail_Insert]
(
 @EmailId          int = '' OUTPUT, 
 @ToEmailAddress   nvarchar(max) = NULL,
 @CCEmailAddress   nvarchar(max) = NULL, 
 @BCCEmailAddress  nvarchar(max) = NULL, 
 @FromEmailAddress nvarchar(255) = NULL,
 @EmailSubject     nvarchar(max) = NULL,
 @EmailBody        nvarchar(max) = NULL,
 @StatusID         int           = NULL,
 @IsEmailSent      bit = 0,
 
 @UserAccountId    int = 1,
 @FieldInError     nvarchar(50) = '' OUTPUT,
 @RMsgId           int = '' OUTPUT,
 @RMessage         nvarchar(200) = '' OUTPUT, 
 @RC               int = '' OUTPUT
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
		INSERT INTO  [Notifications].[Email]
				   ([ToEmailAddress]
				   ,[CCEmailAddress]
				   ,[BCCEmailAddress]
				   ,[FromEmailAddress]
				   ,[EmailSubject]
				   ,[EmailBody]
				   ,[StatusId]
				   ,[IsEmailSent]
				   ,[AddedUserAccountId]
				   ,[UpdatedUserAccountId]
				   ,[AddedDate]
				   ,[UpdatedDate])
			 VALUES
				   (@ToEmailAddress,
				    @CCEmailAddress,
				    @BCCEmailAddress,
				    @FromEmailAddress,
				    @EmailSubject,
				    @EmailBody,
					@StatusID,
				    @IsEmailSent,
				    @UserAccountId,
				    @UserAccountId,				    
				    GETDATE(),
				    GETDATE())				        
				        
		-- Save The New ID Value
		SET @EmailId = SCOPE_IDENTITY()
		
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
