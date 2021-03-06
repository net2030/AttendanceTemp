USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpRoleCapability_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert RoleCapability 
-- =============================================
CREATE PROCEDURE [Users].[SpRoleCapability_Insert]
(
 @RoleCapabilityId		[int] = NULL OUTPUT,
 @RoleId				[int] = NULL,
 @CapabilityId			[int] = NULL,
 @AccessFlag			[int] = NULL, 
 
 @AddedUserAccountId	[int] = NULL,
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
    
    IF NOT EXISTS(SELECT RoleId 
                  FROM  [Users].[Role]
                  WHERE RoleId = @RoleId)                    
    BEGIN
	  SET @RMsgId = 76 -- رقم المجموعة غير صحيح أو غير موجود 
	  SET @FieldInError = 'RoleId'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END
    
    IF NOT EXISTS(SELECT CapabilityId 
                  FROM  [Users].[Capability]
                  WHERE CapabilityId = @CapabilityId)                    
    BEGIN
	  SET @RMsgId = 81 -- رقم الصلاحية غير صحيح أو غير موجود 
	  SET @FieldInError = 'RoleId'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END

    IF (@AccessFlag IS NULL OR @AccessFlag < 0 OR @AccessFlag  > 2 )
    BEGIN
	  SET @RMsgId = 82 -- يجب إدخال رمز التحكم بالصصلاحية 
	  SET @FieldInError = 'RoleName'
	  SET @RMessage = (SELECT MsgText 
	                   FROM  [common].[CustomMessage] 
	                   WHERE MsgId = @RMsgId)
	  RETURN        
    END            
    
    BEGIN TRY
        IF EXISTS(SELECT RoleId
                    FROM  [Users].[RoleCapability] 
                   WHERE RoleId = @RoleId
                     AND CapabilityId = @CapabilityId)
        BEGIN
            SET @RoleCapabilityId = (SELECT RoleCapabilityId
                                       FROM  [Users].[RoleCapability] 
                                      WHERE RoleId = @RoleId
                                        AND CapabilityId = @CapabilityId)
			UPDATE  [Users].[RoleCapability]
				SET AccessFlag = @AccessFlag, 
					UpdatedDate = GetDate(), 
					UpdatedUserAccountId = @AddedUserAccountId
			 WHERE [RoleCapabilityId]   = @RoleCapabilityId                                     
        END
        ELSE
        BEGIN
			INSERT INTO  [Users].[RoleCapability]
				(RoleId, 
				 CapabilityId,
				 AccessFlag,
				 AddedUserAccountId,
				 UpdatedUserAccountId,			 
				 AddedDate, 			 
				 UpdatedDate)
			VALUES
				(@RoleId, 
				 @CapabilityId, 
				 @AccessFlag,  
				 @AddedUserAccountId,
				 @AddedUserAccountId,
				 GetDate(),			 
				 GetDate())
				 
			 -- Save The New ID Value
			SET @RoleCapabilityId = SCOPE_IDENTITY()        
        END                         
		
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
