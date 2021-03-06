USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Security].[SpPermission_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Update Permission
-- =============================================
CREATE PROCEDURE [Security].[SpPermission_Update]
(
 @AccountId                      int = 1 , 
 @AccountRoleId                  int = NULL,
 @SystemSecurityCategoryPageId   int = NULL, 
 @SystemPermissionId             int = NULL, 
 @IsDeleted                      bit = NULL,

 
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
	    
		IF NOT EXISTS(SELECT RoleId 
                      FROM  [Security].[Role] 
                      WHERE RoleId = @AccountRoleId)                    
        BEGIN
		  SET @RMsgId = 132 -- رقم المجموعة غير صحيح أو غير موجود 
		  SET @FieldInError = 'RoleId'
		  SET @RMessage = (SELECT MsgText 
		                   FROM  [common].[CustomMessage] 
		                   WHERE MsgId = @RMsgId)
		  RETURN        
         END

		IF @IsDeleted=0 
		BEGIN
			IF EXISTS (SELECT AccountPagePermissionId FROM Security.AccountPagePermission 
					   WHERE AccountRoleId=@AccountRoleId 
					   And SystemSecurityCategoryPageId=@SystemSecurityCategoryPageId
					   And SystemPermissionId=@SystemPermissionId)
				BEGIN
				SET @RC = 0
				RETURN
				END
			ELSE

				INSERT INTO Security.AccountPagePermission
			                     (AccountId,
								 AccountRoleId,
								 SystemSecurityCategoryPageId,
								 SystemPermissionId,
								 AddedUserAccountId,
								 UpdatedUserAccountId,
								 AddedDate,
								 UpdatedDate)
                          Values( @AccountId,
						          @AccountRoleId,
						         @SystemSecurityCategoryPageId,
								 @SystemPermissionId,
								 @UserAccountId,
								 @UserAccountId,
								 GetDate(),
								 Getdate())
		END

		ELSE IF @IsDeleted=1
	    BEGIN
		IF NOT EXISTS (SELECT AccountPagePermissionId FROM Security.AccountPagePermission 
					   WHERE AccountRoleId=@AccountRoleId 
					   And SystemSecurityCategoryPageId=@SystemSecurityCategoryPageId
					   And SystemPermissionId=@SystemPermissionId)
			RETURN
			ELSE
			DELETE  FROM Security.AccountPagePermission 
					   WHERE AccountRoleId=@AccountRoleId 
					   And SystemSecurityCategoryPageId=@SystemSecurityCategoryPageId
					   And SystemPermissionId=@SystemPermissionId
		END
		            
				        
				        
		-- Save The New ID Value
	
		
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
