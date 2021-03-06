USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpLocation_Update]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Update Nationality 
-- =============================================
CREATE PROCEDURE [Employees].[SpLocation_Update]
(
 @LocationId	    int = NULL , 
 @LocationName		nvarchar(50) = NULL,


 @UserAccountId  int = NULL,
 @VersionNo            timestamp = NULL,
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
        IF NOT EXISTS(SELECT LocationId 
                      FROM  Employees.Location 
                      WHERE LocationId = @LocationId)                    
        BEGIN
		  SET @RMsgId = 132 -- رقم المجموعة غير صحيح أو غير موجود 
		  SET @FieldInError = 'RoleId'
		  SET @RMessage = (SELECT MsgText 
		                   FROM  [common].[CustomMessage] 
		                   WHERE MsgId = @RMsgId)
		  RETURN        
        END

    
                
         IF (@LocationName IS NULL OR @LocationName = '')
        BEGIN
		  SET @RMsgId = 133 -- يجب إدخال أسم المجموعة 
		  SET @FieldInError = 'RoleName'
		  SET @RMessage = (SELECT MsgText 
		                   FROM  [common].[CustomMessage] 
		                   WHERE MsgId = @RMsgId)
		  RETURN        
        END    

                    
      
                
        UPDATE  Employees.Location
		   SET LocationName         = @LocationName,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate]          = GETDATE()
	     WHERE LocationId   = @LocationId
	       AND [VersionNo]  = @VersionNo
	     
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
		IF (@RowEffected = 0 AND Not EXISTS(SELECT LocationId 
		                                      FROM  Employees.Location 
		                                     WHERE LocationId    = @LocationId
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
		  SET @RMsgId = 2  --  فشلت عملية تحديث البيانات
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
