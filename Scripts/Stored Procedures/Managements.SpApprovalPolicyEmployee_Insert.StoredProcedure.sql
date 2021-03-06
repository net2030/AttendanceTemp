USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpApprovalPolicyEmployee_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert New Exception Employee
-- =============================================
CREATE PROCEDURE [Managements].[SpApprovalPolicyEmployee_Insert]
(
 @ApprovalPolicyId int = NULL, 
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
	DECLARE @PolicyType int
    
    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0

	SELECT @PolicyType =TimeOffApprovalType 
	FROM Managements.ApprovalPolicy 
	WHERE ApprovalPolicyId=@ApprovalPolicyId


  
    
    BEGIN TRY	
		IF @PolicyType=1
		UPDATE Employees.Employee SET VacationApprovalTypeId=@ApprovalPolicyId WHERE EmployeeId=@EmployeeId		    	
		ELSE
		IF @PolicyType=2
		UPDATE Employees.Employee SET WorkExceptionApprovalTypeId=@ApprovalPolicyId WHERE EmployeeId=@EmployeeId		    	
		ELSE
		IF @PolicyType=3
		UPDATE Employees.Employee SET GatepassApprovalTypeId=@ApprovalPolicyId WHERE EmployeeId=@EmployeeId		    		        
		ELSE
		UPDATE Employees.Employee SET GatepassApprovalTypeId=@ApprovalPolicyId, WorkExceptionApprovalTypeId=@ApprovalPolicyId,VacationApprovalTypeId=@ApprovalPolicyId WHERE EmployeeId=@EmployeeId		

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
