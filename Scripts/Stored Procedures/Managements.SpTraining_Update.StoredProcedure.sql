USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTraining_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	update Training
-- =============================================
CREATE PROCEDURE [Managements].[SpTraining_Update]
(
 @TrainingId      int = NULL, 
 @DepartmentId    int = NULL, 
 @CourseName      nvarchar(100) = NULL,
 @InstituteName   nvarchar(100) = NULL,
 @TrainingBegDate date = NULL,
 @TrainingEndDate date = NULL,
 @Notes           nvarchar(200) = NULL,
 @VersionNo       timestamp = NULL, 
 
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
    
    IF NOT EXISTS(SELECT DepartmentId 
                    FROM  [Employees].[Department]
                   WHERE DepartmentId = @DepartmentId)                    
    BEGIN
	  SET @RMsgId = 116 -- رقم القسم/الإدارة غير صحيح أو غير موجود 
	  SET @FieldInError = 'DepartmentId'
	  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END
    
    BEGIN TRY	
		UPDATE  [Managements].[Training]
		   SET [CourseName] = @CourseName,
		       [DepartmentId] = @DepartmentId,
			   [InstituteName] = @InstituteName,
			   [TrainingBegDate] = @TrainingBegDate,
			   [TrainingEndDate] = @TrainingEndDate,
			   [Notes] = @Notes,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate] = GETDATE() 
         WHERE TrainingId = @TrainingId
           AND VersionNo = @VersionNo				        
				        		
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
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [TrainingId] 
		                                      FROM  [Managements].[Training]
		                                     WHERE [TrainingId]   = @TrainingId
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
