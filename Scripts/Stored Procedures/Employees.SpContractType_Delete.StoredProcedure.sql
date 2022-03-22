USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpContractType_Delete]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	delete Work Exception Type
-- =============================================
CREATE PROCEDURE [Employees].[SpContractType_Delete]
(
 @ContractTypeId   int = NULL, 
 @ContractTypeName nvarchar(50) = NULL,
 
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

    IF NOT EXISTS(SELECT ContractTypeId 
                    FROM  [Employees].[ContractType]
                   WHERE ContractTypeId = @ContractTypeId)                    
    BEGIN
	  SET @RMsgId = 153 -- رقم نوع الإستثناء غير صحيح أو غير موجود 
	  SET @FieldInError = 'ContractTypeId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END	

	
               
    BEGIN TRY	
		DELETE  [Employees].[ContractType]
	     WHERE [ContractTypeId] = @ContractTypeId			   			        
		
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
