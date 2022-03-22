USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpMachineLog_Change]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Insert a new machine log
-- =============================================
Create PROCEDURE [Logs].[SpMachineLog_Change]
(
 @LogId         int = NULL,

 @FieldInError  nvarchar(50) = '' OUTPUT,
 @RMsgId        int = NULL OUTPUT,
 @RMessage      nvarchar(200) = '' OUTPUT, 
 @RC            int = NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

    -- Declariations
    DECLARE @RowEffected int


    DECLARE @EmployeeId  int 
    DECLARE @LogDate date
    
    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0
    
    
    BEGIN TRY
 
            
        IF NOT EXISTS(SELECT logid 
                    FROM  [Logs].[MachineLog]
                   WHERE LogId = @LogId)
        BEGIN
		  SET @RMsgId = 161 -- رقم الحركة غير موجود 
		  SET @FieldInError = 'PositionName'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END

		SELECT @EmployeeId=EmployeeId,@LogDate=LogDate 
		FROM Logs.MachineLog 
		WHERE LogId=@LogId


       Update Logs.MachineLog 
	      Set LogTypeId = Case When LogTypeId = 1 Then 2 Else 1 End 
		  Where LogId=@LogId

		
		SET @RowEffected = @@RowCount

		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		 EXEC logs.SpProcessAttendanceByEmployee @logdate,@EmployeeId
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
