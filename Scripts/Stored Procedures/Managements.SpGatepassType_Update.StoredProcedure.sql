USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepassType_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	update Work Exception Type
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepassType_Update]
(
 @GatepassTypeId   int = NULL, 
 @GatepassTypeName nvarchar(50) = NULL,
 @GatepassTypeNameEN nvarchar(50) = '',
 
 @VersionNo             timestamp = NULL,
 @UserAccountId         int = NULL,
 @FieldInError          nvarchar(50) = '' OUTPUT,
 @RMsgId                int = NULL OUTPUT,
 @RMessage              nvarchar(200) = '' OUTPUT, 
 @RC                    int = NULL OUTPUT
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
    
    IF NOT EXISTS(SELECT GatepassTypeId 
                    FROM  [Managements].[GatepassType]
                   WHERE GatepassTypeId = @GatepassTypeId)                    
    BEGIN
	  SET @RMsgId = 153 -- رقم نوع الإستثناء غير صحيح أو غير موجود 
	  SET @FieldInError = 'GatepassTypeId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END	
           
    BEGIN TRY	
		UPDATE  [Managements].[GatepassType]
		   SET [GatepassTypeName] = @GatepassTypeName,
		       [GatepassTypeNameEN] = @GatepassTypeNameEN,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate] = GETDATE()
	     WHERE [GatepassTypeId] = @GatepassTypeId
	       AND [VersionNo]    = @VersionNo 		
		   
		  		        
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)

           UPDATE Logs.AttendanceStatus 
		          SET StatusName = @GatepassTypeName ,
				      StatusNameEN = @GatepassTypeNameEN
				  WHERE StatusId=@GatepassTypeId  	

		  RETURN
		END
		ELSE
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [GatepassTypeId] 
		                                      FROM  [Managements].[GatepassType]
		                                     WHERE [GatepassTypeId] = @GatepassTypeId
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
