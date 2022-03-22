USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployeeDepartmentLink_Delete]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	delete department  scope
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployeeDepartmentLink_Delete]
(
 @EmployeeId          int = NULL, 
 @DepartmentId        int = NULL,
 
 @FieldInError        nvarchar(50) = '' OUTPUT,
 @RMsgId              int = NULL OUTPUT,
 @RMessage            nvarchar(200) = '' OUTPUT, 
 @RC                  int = NULL OUTPUT
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
        IF NOT EXISTS(SELECT EmployeeId
                        FROM  [Employees].[EmployeeDepartmentLink]
                       WHERE EmployeeId = @EmployeeId
                         AND DepartmentId = @DepartmentId)
        BEGIN
		  SET @RMsgId = 130 -- رقم القسم/الإدارة مدخل مسبقا لهذا المستخدم 
		  SET @FieldInError = 'DepartmentId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  --RETURN    
        END

		DELETE  [Employees].[EmployeeDepartmentLink]
		 WHERE EmployeeId   = @EmployeeId
		              
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
