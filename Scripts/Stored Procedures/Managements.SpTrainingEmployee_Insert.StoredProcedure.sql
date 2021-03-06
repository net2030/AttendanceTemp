USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTrainingEmployee_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	add new employee to Training
-- =============================================
CREATE PROCEDURE [Managements].[SpTrainingEmployee_Insert]
(
 @TrainingId      int = NULL, 
 @EmployeeId      int = NULL,

 @UserAccountId   int = NULL,
 @FieldInError    nvarchar(50) = '' OUTPUT,
 @RMsgId          int = NULL OUTPUT,
 @RMessage        nvarchar(200) = '' OUTPUT, 
 @RC              int = NULL OUTPUT
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

    IF NOT EXISTS(SELECT TrainingId 
                    FROM  [Managements].[Training]
                   WHERE TrainingId = @TrainingId)                    
    BEGIN
	  SET @RMsgId = 148 -- رقم الدورة التدريبية غير صحيح أو غير موجود 
	  SET @FieldInError = 'TrainingId'
	  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END

    IF EXISTS(SELECT TrainingId 
                FROM  [Managements].[TrainingEmployeeLink]
               WHERE TrainingId = @TrainingId
                 AND EmployeeId = @EmployeeId)                    
    BEGIN
	  SET @RMsgId = 149 -- الموظف مدخل مسبقاً في هذة الدورة 
	  SET @FieldInError = 'EmployeeId'
	  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END
        
    BEGIN TRY	
		INSERT INTO  [Managements].[TrainingEmployeeLink]
				   ([TrainingId]
				   ,[EmployeeId]
				   ,[AddedUserAccountId]
				   ,[UpdatedUserAccountId]
				   ,[AddedDate]
				   ,[UpdatedDate])
			 VALUES
				   (@TrainingId,
				    @EmployeeId,
				    @UserAccountId,
				    @UserAccountId,
				    GETDATE(),
				    GETDATE())				    				        
		
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
