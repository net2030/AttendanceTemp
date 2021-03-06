USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpDepartment_Insert]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Insert a new Department
-- =============================================
CREATE PROCEDURE [Employees].[SpDepartment_Insert]
(
 @DepartmentId    int = NULL OUTPUT, 
 @DepartmentName  nvarchar(100) = NULL,
 @DepartmentNameEN  nvarchar(100) = '',
 @ParentId        int = NULL,
 
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
        IF EXISTS(SELECT DepartmentId 
                    FROM  [Employees].[Department] 
                   WHERE DepartmentName = @DepartmentName)
        BEGIN
		  SET @RMsgId = 114 -- أسم الإدارة/الفسم مكرر 
		  SET @FieldInError = 'DepartmentName'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END

        IF NOT EXISTS(SELECT DepartmentId 
                    FROM  [Employees].[Department] 
                   WHERE DepartmentId = @ParentId)
        BEGIN
		  SET @RMsgId = 115 -- رقم الإدارة أو القسم التابع لها الإدارة أو القسم المدخل غير صحيح 
		  SET @FieldInError = 'ParentId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
               
		INSERT INTO  [Employees].[Department]
				   ([DepartmentName],
				    [DepartmentNameEN],
				    [ParentId],
                    [AddedUserAccountId],
                    [AddedDate],
                    [UpdatedUserAccountId],
                    [UpdatedDate])
			 VALUES
				   (@DepartmentName,
				    @DepartmentNameEN,
				    @ParentId,
				    @UserAccountId,
				    GETDATE(),
				    @UserAccountId,
				    GETDATE())				    

		-- Save The New ID Value
		SET @DepartmentId = SCOPE_IDENTITY()

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
