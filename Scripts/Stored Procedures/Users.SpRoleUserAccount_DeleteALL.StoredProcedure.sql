USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpRoleUserAccount_DeleteALL]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Users].[SpRoleUserAccount_DeleteALL]
(
 @RoleId int = NULL,
	
 @FieldInError       nvarchar(50) = '' OUTPUT,
 @RMsgId             int = NULL OUTPUT,
 @RMessage           nvarchar(200) = '' OUTPUT, 
 @RC                 int = NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON
	
    -- Local Variables Declaration
    DECLARE @RwosEffected int, @LangeId int
  
    -- Initialize Fields  
    SET @RC = 1 -- Assume Failure Run 
    SET @RwosEffected = 0
    SET @LangeId = 2

        IF NOT EXISTS(SELECT RoleId 
                      FROM  [Users].[Role] 
                      WHERE RoleId = @RoleId)                    
        BEGIN
		  SET @RMsgId = 322 -- Invalid Role Id Or Its not exists 
		  SET @FieldInError = 'RoleId'
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN       
        END
            
    BEGIN TRY		    
	  DELETE 
	    FROM Users.RoleUserAccount
	   WHERE RoleId = @RoleId
	 
		-- Save Number of Rows Affected
		SET @RwosEffected = @@RowCount 	        

		IF @RwosEffected > 0 AND @@ERROR = 0
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
