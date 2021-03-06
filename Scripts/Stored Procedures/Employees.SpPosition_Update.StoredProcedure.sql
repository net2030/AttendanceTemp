USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpPosition_Update]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Update Position 
-- =============================================
CREATE PROCEDURE [Employees].[SpPosition_Update]
(
 @PositionId     int = NULL, 
 @PositionName   nvarchar(50) = NULL,
 @PositionTypeId int = NULL,
 @SortIndex      int = NULL,
 @IsOfficer      bit = 0,   
 @VersionNo      timestamp,
 
 @UserAccountId  int = NULL,
 @FieldInError   nvarchar(50) = '' OUTPUT,
 @RMsgId         int = NULL OUTPUT,
 @RMessage       nvarchar(200) = '' OUTPUT, 
 @RC             int = NULL OUTPUT
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
    
    IF @PositionTypeId > 1
    BEGIN
      SET @IsOfficer = 0
    END
    
    BEGIN TRY
        IF NOT EXISTS(SELECT PositionId 
                        FROM  [Employees].[Position]
                       WHERE PositionId = @PositionId)                    
        BEGIN
		  SET @RMsgId = 111 -- رقم الرتبة/المرتبة غير صحيح أو غير موجود 
		  SET @FieldInError = 'PositionId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END

        IF NOT EXISTS(SELECT PositionTypeId 
                        FROM  [Employees].[PositionType]
                       WHERE PositionTypeId = @PositionTypeId)                    
        BEGIN
		  SET @RMsgId = 109 -- رقم نوع الرتبة/المرتبة غير صحيح أو غير موجود 
		  SET @FieldInError = 'PositionTypeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
        
        IF EXISTS(SELECT PositionId 
                    FROM  [Employees].[Position]
                   WHERE PositionId = @PositionId
                     AND IsProtected = 1)                    
        BEGIN
		  SET @RMsgId = 6 -- لا يمكن تحديث سجل خاص بالنظام 
		  SET @FieldInError = 'PositionId'
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE MsgId = @RMsgId)
		  RETURN        
        END
                
        IF (@PositionName IS NULL OR @PositionName = '')
        BEGIN
		  SET @RMsgId = 112 -- يجب إدخال أسم الرتبة/المرتبة 
		  SET @FieldInError = 'PositionName'
		  SET @RMessage = (SELECT MsgText 
		                   FROM  [common].[CustomMessage] 
		                   WHERE MsgId = @RMsgId)
		  RETURN        
        END    
        
        IF EXISTS(SELECT PositionName 
                    FROM  [Employees].[Position]
                   WHERE PositionId <> @PositionId
                     AND PositionName = @PositionName
                     AND PositionTypeId = @PositionTypeId)                    
        BEGIN
		  SET @RMsgId = 110 -- أسم الرتبة/المرتبة مكرر 
		  SET @FieldInError = 'PositionName'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
                
		UPDATE  [Employees].[Position]
		   SET [PositionName]          = @PositionName,
			   [PositionTypeId]        = @PositionTypeId,
			   [SortIndex]             = @SortIndex,
			   [IsOfficer]             = @IsOfficer,
			   [UpdatedUserAccountId]  = @UserAccountId,
			   [UpdatedDate]           = GETDATE()
	     WHERE [PositionId]            = @PositionId
	       AND [VersionNo]             = @VersionNo 		   

		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount

		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE [MsgId] = @RMsgId)
		  RETURN
		END
		ELSE
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [PositionId] 
		                                      FROM  [Employees].[Position]
		                                     WHERE [PositionId] = @PositionId
	                                           AND [VersionNo]    = @VersionNo))
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
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE [MsgId] = @RMsgId)
		  RETURN
       END      
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         SET @RMsgId = 3  --  حدث خطاء في إجراء قاعدة البيانات - الرجاء الإتصال بمسئول النظام للمساعدة
		 SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE [MsgId] = @RMsgId)
        RETURN    
    END CATCH
END






GO
