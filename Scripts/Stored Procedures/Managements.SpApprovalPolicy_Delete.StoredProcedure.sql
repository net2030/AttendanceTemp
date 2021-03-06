USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpApprovalPolicy_Delete]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	delete ApprovalPolicy
-- =============================================
CREATE PROCEDURE [Managements].[SpApprovalPolicy_Delete]
(
 @ApprovalPolicyId           int = NULL, 

 @FieldInError             nvarchar(50) = '' OUTPUT,
 @RMsgId                   int = NULL OUTPUT,
 @RMessage                 nvarchar(200) = '' OUTPUT, 
 @RC                       int = NULL OUTPUT
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


	IF  EXISTS(SELECT ApprovalPolicyId 
					FROM  [Managements].[ApprovalPolicy]
				   WHERE ApprovalPolicyId = @ApprovalPolicyId AND IsProtected=1)                    
	BEGIN
	  SET @RMsgId = 4 -- 
	  SET @FieldInError = 'ApprovalPolicyId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
	END	

	IF NOT EXISTS(SELECT ApprovalPolicyId 
					FROM  [Managements].[ApprovalPolicy]
				   WHERE ApprovalPolicyId = @ApprovalPolicyId)                    
	BEGIN
	  SET @RMsgId = 164 -- 
	  SET @FieldInError = 'ApprovalPolicyId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
	END	



	IF EXISTS(SELECT EmployeeId
	          FROM Employees.Employee
			   WHERE GatepassApprovalTypeId = @ApprovalPolicyId OR VacationApprovalTypeId=@ApprovalPolicyId OR WorkExceptionApprovalTypeId=@ApprovalPolicyId)                    
	BEGIN
	  SET @RMsgId = 199 --لا يمكن حذف السجل لإرتباطه بسجلات اخرى
	  SET @FieldInError = 'ApprovalPolicyId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
	END	
		
    BEGIN TRY
		 DELETE [Managements].ApprovalPath
	     WHERE ApprovalPolicyId = @ApprovalPolicyId     
		        	        
		DELETE  [Managements].[ApprovalPolicy]			        
	     WHERE ApprovalPolicyId = @ApprovalPolicyId
		
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
