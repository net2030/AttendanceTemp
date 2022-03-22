USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpRoleCapability_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Update RoleCapability 
-- =============================================
CREATE PROCEDURE [Users].[SpRoleCapability_Update]
(
 @RoleCapabilityId		[int] = NULL,
 @RoleId				[int] = NULL,
 @CapabilityId			[int] = NULL,
 @AccessFlag			[int] = NULL,
 @VersionNo				[timestamp] = NULL,
 
 @UpdatedUserAccountId	[int] = NULL,
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
    
    
    
    BEGIN TRY
        IF NOT EXISTS(SELECT RoleCapabilityId 
                      FROM  [Users].[RoleCapability]
                      WHERE RoleCapabilityId = @RoleCapabilityId)                    
        BEGIN
		  SET @RMsgId = 80 -- رقم سجل الصلاحية الممنوحة للمجموعة غير صحيح أو غير موجود 
		  SET @FieldInError = 'RoleCapabilityId'
		  SET @RMessage = (SELECT MsgText 
		                   FROM  [common].[CustomMessage] 
		                   WHERE MsgId = @RMsgId)
		  RETURN        
        END
        
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
        
        UPDATE  [Users].[RoleCapability]
		    SET RoleId = @RoleId, 
				CapabilityId = @CapabilityId, 
				AccessFlag = @AccessFlag, 
				UpdatedDate = GetDate(), 
				UpdatedUserAccountId = @UpdatedUserAccountId
	     WHERE [RoleCapabilityId]   = @RoleCapabilityId
	       AND [VersionNo]    = @VersionNo
	     
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
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [RoleCapabilityId] 
		                                      FROM  [Users].[RoleCapability]
		                                     WHERE [RoleCapabilityId]   = @RoleCapabilityId
	                                           AND [VersionNo]    = @VersionNo))
		BEGIN
		  SET @RMsgId = 100  -- تم تحديث السجل من قبل مستخدم آخر
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
