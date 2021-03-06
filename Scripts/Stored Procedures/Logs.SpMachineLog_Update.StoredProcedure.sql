USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpMachineLog_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Insert a new machine log
-- =============================================
CREATE PROCEDURE [Logs].[SpMachineLog_Update]
(
 @LogId         int = NULL, 
 @EmployeeId    int= NULL,
 @LogDate       date= NULL,
 @LogTime       datetime= NULL,
 @LogTypeId     int= NULL,

 @VersionNo                timestamp = NULL,
 @UserAccountId int = NULL,
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
    DECLARE @IsValidRecord bit  = 1
    DECLARE @IsManualRecord bit  = 1
    DECLARE @IPAddress nvarchar(20)
    DECLARE @MachineId     int = 1
    DECLARE @NewTime datetime
    
    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0
    
    
    BEGIN TRY
        IF NOT EXISTS(SELECT LogTypeId 
                        FROM  [Logs].[LogType]
                       WHERE LogTypeId = @LogTypeId)                    
        BEGIN
		  SET @RMsgId = 140 -- رقم نوع الحركة غير صحيح أو غير موجود 
		  SET @FieldInError = 'PositionTypeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
            
        IF NOT EXISTS(SELECT MachineId 
                    FROM  [Logs].[Machine]
                   WHERE MachineId   = @MachineId)
        BEGIN
		  SET @RMsgId = 141 -- رقم جهاز البصمة غير صحيح أو غير موجود 
		  SET @FieldInError = 'PositionName'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
        
        SELECT @IPAddress = IPAddress
          FROM Logs.Machine
         WHERE MachineId = @MachineId

		IF @LogTypeId = 1
		BEGIN
			SET @NewTime = CAST(CAST('1900-01-01' as nvarchar(10)) + 
						  (SUBSTRING(CAST(@LogTime AS NVARCHAR(30)),12 ,6) + ' ' + 
						   SUBSTRING(CAST(@LogTime AS NVARCHAR(30)),18 ,2)) as datetime)
			SET @LogTime =  @NewTime
		END     
		     
		UPDATE  [Logs].[MachineLog]
				   SET [EmployeeId]=@EmployeeId,
				    [LogDate]=@LogDate,
				    [LogTime]=@LogTime,
				    [LogTypeId]=@LogTypeId,
				    [IPAddress]=@IPAddress,
				    [MachineId]=@MachineId,
				    [IsValidRecord]=@IsValidRecord,
				    [IsManualRecord]=@IsManualRecord,
				    [UpdatedUserAccountId]=@UserAccountId,
				    [UpdateDate]=GETDATE()
					WHERE LogID=@LogId
			 		    

		-- Save The New ID Value
		SET @LogId = SCOPE_IDENTITY()

		-- Save Number of Rows Affected
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
