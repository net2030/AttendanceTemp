USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffPolicyEmployee_Delete]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Delete Employee from timeoff policy
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffPolicyEmployee_Delete]
(
 @TimeOffPolicyId int = NULL,
 @EmployeeId     int = NULL,
 
 @UserAccountId  int=1,
 @FieldInError   nvarchar(50) = '' OUTPUT,
 @RMsgId         int = NULL OUTPUT,
 @RMessage       nvarchar(200) = '' OUTPUT, 
 @RC             int = NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
	DECLARE @EmpId int
	
	DECLARE @TimeOffPolicyDetailId int
	DECLARE @Earned decimal(18,6)=0.0
	DECLARE @Available decimal(18,6)=0.0
	DECLARE @CarryForward decimal(18,6)=0.0
	DECLARE @LastEarnedDate Date
	DECLARE @LastResetDate Date
	

	DECLARE @HireDate Date
    DECLARE @EffectiveDate Date
	DECLARE @InitialSet decimal(18,3)
	DECLARE @EarnPerDay decimal(18,3)
	DECLARE @EarnPerTime decimal(18,3)
	--DECLARE @periodDays DECIMAL(18,3)
	DECLARE @Today Date =GETDATE()
	DECLARE @EarnPeriodId int
	DECLARE @Count int=0
	DECLARE @TotalDays DECIMAL(6,3)=0.000

    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0


    BEGIN TRY
		IF NOT EXISTS(SELECT TimeOffPolicyId 
						FROM  [Managements].[TimeOffPolicy]
					   WHERE TimeOffPolicyId = @TimeOffPolicyId)                    
		BEGIN
		  SET @RMsgId = 165 -- رقم السياسة غير صحيح أو غير موجود 
		  SET @FieldInError = 'TimeOffPolicyId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END
	

	   DELETE  FROM Managements.TimeOffBalance WHERE EmployeeId=@EmployeeId AND  TimeOffPolicyDetailId IN (SELECT TimeOffPolicyDetailId FROM Managements.TimeOffPolicyDetail WHERE TimeOffPolicyId = @TimeOffPolicyId)

	  
		
		
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
		  SET @RMsgId = 2 -- 
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
