USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkScheduleEmployee_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert Employee into Work Schedule
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkScheduleEmployee_Insert]
(
 @WorkScheduleId int = NULL,
 @EmployeeId     int = NULL,
 
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
    
    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0


    BEGIN TRY
		IF NOT EXISTS(SELECT WorkScheduleId 
						FROM  [Managements].[WorkSchedule]
					   WHERE WorkScheduleId = @WorkScheduleId)                    
		BEGIN
		  SET @RMsgId = 120 -- رقم جدول العمل غير صحيح أو غير موجود 
		  SET @FieldInError = 'WorkScheduleId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END
		    
		--IF EXISTS(SELECT WorkScheduleId 
		--			FROM  [Managements].[WorkScheduleEmployeeLink]
		--		   WHERE WorkScheduleId = @WorkScheduleId
		--		     AND [EmployeeId] = @EmployeeId)                    
		--BEGIN
		--  SET @RMsgId = 0 -- الموظف مسجل في هذا الجدول مسبقاً 
		--  SET @FieldInError = 'EmployeeId'
		--  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		--  RETURN        
		--END	         	
		 
		 
		--IF NOT EXISTS(SELECT EmployeeId 
		--              FROM Managements.WorkScheduleEmployeeLink
		--			  WHERE EmployeeId=@EmployeeId)        
		--INSERT INTO  [Managements].[WorkScheduleEmployeeLink]  
		--		   ([WorkScheduleId],
		--		    [EmployeeId])
		--	 VALUES
		--		   (@WorkScheduleId,
		--		    @EmployeeId)
  --       ELSE
		-- UPDATE  [Managements].[WorkScheduleEmployeeLink]
		--       SET WorkScheduleId=@WorkScheduleId
		--	   WHERE EmployeeId=@EmployeeId
		
		Update Employees.Employee 
			    SET WorkScheduleId=@WorkScheduleId 
				WHERE EmployeeId=@EmployeeId

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
