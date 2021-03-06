USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpUserAccount_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Users].[SpUserAccount_Update]
(
 @UserAccountId        int = NULL,
 @WindowsAccountName   varchar(50) = NULL,
 @UserName             varchar(50) = NULL,
 @Email                varchar(100) = NULL,
 @IsActive             bit = 1,	
 @VersionNo            timestamp = NULL,
 
 @UpdatedUserAccountId int,	
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
    
    -- Validate Parameters  
    IF NOT EXISTS(SELECT UserAccountId 
                    FROM Users.UserAccount
                    WHERE UserAccountId = @UserAccountId)
    
    BEGIN
	  SET @RMsgId = 126 -- رقم المستخدم غير صحيح أو غير موجود 
	  SET @FieldInError = 'WindowsAccountName'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END

    IF EXISTS(SELECT UserAccountId 
                FROM Users.UserAccount
               WHERE UserAccountId = @UserAccountId
                 AND IsProtected = 1)
    
    BEGIN
	  SET @RMsgId = 127 -- لايمكن تعديل هذا الحساب 
	  SET @FieldInError = 'WindowsAccountName'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END
               	                
    IF (@WindowsAccountName = NULL OR @WindowsAccountName = '')
    BEGIN
	  SET @RMsgId = 123 -- يجب إدخال اسم الحساب 
	  SET @FieldInError = 'WindowsAccountName'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END	

    IF (@UserName = NULL OR @UserName = '')
    BEGIN
	  SET @RMsgId = 124 -- يجب إدخال اسم المستخدم 
	  SET @FieldInError = 'UserName'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END	
    
    IF EXISTS(SELECT UserAccountId 
                FROM Users.UserAccount
               WHERE UserName = @UserName
                 AND UserAccountId <> @UserAccountId)
    
    BEGIN
	  SET @RMsgId = 128 -- اسم المستخدم مكرر 
	  SET @FieldInError = 'UserName'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END	

    IF EXISTS(SELECT UserAccountId 
                FROM Users.UserAccount
               WHERE WindowsAccountName = @WindowsAccountName
                 AND UserAccountId <> @UserAccountId)
    
    BEGIN
	  SET @RMsgId = 129 -- أسم حساب المستخدم مكرر 
	  SET @FieldInError = 'WindowsAccountName'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END	
                       
    BEGIN TRY    
		UPDATE Users.UserAccount 
		   SET WindowsAccountName = @WindowsAccountName,
			   UserName = @UserName,
			   Email = @Email,
			   IsActive = @IsActive,
			   UpdatedDate = GetDate(),
			   UpdatedUserAccountId = @UpdatedUserAccountId
		 WHERE UserAccountId = @UserAccountId
		   AND VersionNo = @VersionNo
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 6  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
		END
		ELSE
		IF (@RowEffected = 0 AND Not EXISTS(SELECT UserAccountId 
		                                      FROM Users.UserAccount 
		                                     WHERE UserAccountId = @UserAccountId
	                                           AND [VersionNo] = @VersionNo))
        BEGIN
          SET @RMsgId = 100  -- تم تعديل السجل من قبل مستخدم آخر
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN           
        END          
        ELSE		
		BEGIN -- Failed
		  SET @RMsgId = 7  --  فشلت عملية تحديث البيانات
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
       END      
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         SET @RMsgId = 8  --  حدث خطاء في إجراء قاعدة البيانات - الرجاء الإتصال بمسئول النظام للمساعدة
		 SET @RMessage = (SELECT MsgText 
		                    FROM  [common].[CustomMessage] 
		                   WHERE [MsgId] = @RMsgId)
        RETURN    
    END CATCH	
END






GO
