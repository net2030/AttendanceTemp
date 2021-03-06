USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpMachine_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	update Machine
-- =============================================
CREATE PROCEDURE [Logs].[SpMachine_Update]
(
 @MachineId      int = NULL , 
 @MachineName    nvarchar(100) = NULL,
 @Location       nvarchar(100) = NULL,
 @IPAdress       nvarchar(100) = NULL,
 @MachineTypeId      int = NULL,
 @DepartmentId   int,
 
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
    
    IF NOT EXISTS(SELECT MachineId 
                    FROM  Logs.Machine
                   WHERE MachineId = @MachineId)                    
    BEGIN
	  SET @RMsgId = 153 -- رقم الجهاز غير صحيح أو غير موجود 
	  SET @FieldInError = 'MachineId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END	

		if EXISTS(SELECT MachineId FROM Logs.Machine WHERE IPAddress=@IPAdress AND MachineId <> @MachineId)
	  	BEGIN -- Dupplicate IP
		 
		  SET @RMsgId = 207  -- عنوان الشبكة مكرر
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
		END
           
    BEGIN TRY	
		UPDATE  Logs.Machine
		   SET [MachineName] = @MachineName,
		       Location = @Location,
			   IPAddress =  @IPAdress,
			   MachineTypeId = @MachineTypeId,
			   DepartmentId = @DepartmentId,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate] = GETDATE()
	     WHERE [MachineId] = @MachineId
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
		  RETURN
		END
		ELSE
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [MachineId] 
		                                      FROM  Logs.Machine
		                                     WHERE [MachineId] = @MachineId
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
