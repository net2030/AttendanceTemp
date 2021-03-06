USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpDepartment_Update]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Update Department 
-- =============================================
CREATE PROCEDURE [Employees].[SpDepartment_Update]
(
 @DepartmentId    int = NULL, 
 @DepartmentName  nvarchar(100) = NULL,
 @DepartmentNameEN  nvarchar(100) = '',
 @ParentId        int = NULL,
 
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
    
    
    BEGIN TRY
        IF NOT EXISTS(SELECT DepartmentId 
                        FROM  [Employees].[Department]
                       WHERE DepartmentId = @DepartmentId)                    
        BEGIN
		  SET @RMsgId = 116 -- رقم القسم/الإدارة غير صحيح أو غير موجود 
		  SET @FieldInError = 'DepartmentId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END

                
        IF (@DepartmentName IS NULL OR @DepartmentName = '')
        BEGIN
		  SET @RMsgId = 117 -- يجب إدخال أسم الإدارة أو القسم 
		  SET @FieldInError = 'DepartmentName'
		  SET @RMessage = (SELECT MsgText 
		                   FROM  [common].[CustomMessage] 
		                   WHERE MsgId = @RMsgId)
		  RETURN        
        END    
        
        IF EXISTS(SELECT DepartmentName 
                    FROM  [Employees].[Department]
                   WHERE DepartmentId <> @DepartmentId
                     AND DepartmentName = @DepartmentName)                    
        BEGIN
		  SET @RMsgId = 114 -- أسم الإدارة/القسم مكرر 
		  SET @FieldInError = 'DepartmentName'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END

        IF @DepartmentId <> 1 AND NOT EXISTS(SELECT DepartmentId 
                    FROM  [Employees].[Department] 
                   WHERE DepartmentId = @ParentId)
        BEGIN
		  SET @RMsgId = 115 --  رقم الإدارة أو القسم التابع لها الإدارة أو القسم المدخل غير صحيح أو غير موجود 
		  SET @FieldInError = 'ParentId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
          
		  
		 IF  EXISTS(SELECT DepartmentId 
                        FROM  [Employees].[Department]
                       WHERE DepartmentId = @DepartmentId and IsProtected=1)                    
  
		 UPDATE  [Employees].[Department]
		   SET [DepartmentName]       = @DepartmentName,
		       [DepartmentNameEN]     = @DepartmentNameEN,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate]          = GETDATE()
	     WHERE [DepartmentId] = @DepartmentId
	       AND [VersionNo]    = @VersionNo 		  
     
		 ELSE          
		UPDATE  [Employees].[Department]
		   SET [DepartmentName]       = @DepartmentName,
		       [DepartmentNameEN]     = @DepartmentNameEN,
			   [ParentId]             = @ParentId,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate]          = GETDATE()
	     WHERE [DepartmentId] = @DepartmentId
	       AND [VersionNo]    = @VersionNo 		   

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
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [DepartmentId] 
		                                      FROM  [Employees].[Department]
		                                     WHERE [DepartmentId] = @DepartmentId
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
