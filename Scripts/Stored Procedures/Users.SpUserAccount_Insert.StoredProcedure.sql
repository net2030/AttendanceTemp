USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpUserAccount_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Users].[SpUserAccount_Insert]
(
 @UserAccountId      int = NULL OUTPUT,
 @WindowsAccountName varchar(50) = NULL,
 @UserName           varchar(50) = NULL,
 @Email              varchar(100) = NULL,
 @IsActive           bit = 1,
 @EmployeeId         int = NULL, 	
           	
 @AddedUserAccountId int = NULL,
 @FieldInError       nvarchar(50) = '' OUTPUT,
 @RMsgId             int = NULL OUTPUT,
 @RMessage           nvarchar(200) = '' OUTPUT, 
 @RC                 int = NULL OUTPUT	
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
               WHERE WindowsAccountName = @WindowsAccountName)
    
    BEGIN
	  SET @RMsgId = 125 -- أسم حساب المستخدم  مكرر  
	  SET @FieldInError = 'WindowsAccountName'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END	
                       
    BEGIN TRY    
		INSERT INTO Users.UserAccount
					(WindowsAccountName, 
					 UserName, 
					 Email, 
					 IsActive,
					 EmployeeId, 
					 AddedDate, 
					 AddedUserAccountId, 
					 UpdatedDate, 
					 UpdatedUserAccountId) 
			 VALUES (@WindowsAccountName, 
					 @UserName,
					 @Email,
					 @IsActive,
					 @EmployeeId,
					 GetDate(),
					 @AddedUserAccountId, 
					 GetDate(),
					 @AddedUserAccountId)				        
				        
		-- Save The New ID Value
		SET @UserAccountId = Scope_Identity()
		
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
