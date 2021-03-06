USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpRoleUserAccount_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Users].[SpRoleUserAccount_Insert]
(
 @RoleUserAccountId int = NULL OUTPUT,
 @RoleId int = NULL,
 @UserAccountId int = NULL,

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


    BEGIN TRY
	
	  INSERT INTO Users.RoleUserAccount
	              (RoleId, 
	               UserAccountId, 
	               AddedDate, 
	               AddedUserAccountId, 
	               UpdatedDate, 
	               UpdatedUserAccountId)
		   VALUES (@RoleId, 
		  	  	  @UserAccountId, 
				  GetDate(),
				  @AddedUserAccountId,
				  GetDate(), 
				  @AddedUserAccountId)
				        
		-- Save The New ID Value
		SET @RoleUserAccountId = SCOPE_IDENTITY()
		
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
