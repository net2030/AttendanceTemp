USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpApprovalPath_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Managements].[SpApprovalPath_Insert]
(
 @ApprovalPathId        int = NULL OUTPUT, 
 @ApprovalPolicyId      int = NULL, 
 @ApproverTypeId        int = NULL,
 @EmployeeId            int=NULL,
 @ApprovalPathSequence  int=NULL,


 @UserAccountId        int = NULL,
 @FieldInError         nvarchar(50) = '' OUTPUT,
 @RMsgId               int = NULL OUTPUT,
 @RMessage             nvarchar(200) = '' OUTPUT, 
 @RC                   int = NULL OUTPUT
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
    
	  IF  EXISTS(SELECT ApprovalPathId 
                    FROM  [Managements].ApprovalPath
                   WHERE ApprovalPolicyId = @ApprovalPolicyId AND ApproverTypeId=@ApproverTypeId)                    
    BEGIN
	  SET @RMsgId = 162 --  مكرر
	  SET @FieldInError = 'GatepassTypeId'
	  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END
	   
    BEGIN TRY	
		INSERT INTO  [Managements].ApprovalPath
				   (ApprovalPolicyId
				   ,ApproverTypeId
				   ,EmployeeId
				   ,ApprovalPathSequence
				   ,[AddedUserAccountId]
				   ,[UpdatedUserAccountId]
				   ,[AddedDate]
				   ,UpdateDate)
			 VALUES
				   (@ApprovalPolicyId,
				    @ApproverTypeId,
					@EmployeeId,
					@ApprovalPathSequence,
				    @UserAccountId,
				    @UserAccountId,
				    GETDATE(),
				    GETDATE())

		-- Save The New ID Value
		SET @ApprovalPathId = SCOPE_IDENTITY()				        
		
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
