USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpHolidayExceptionEmployee_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert New Exception Employee
-- =============================================
CREATE PROCEDURE [Managements].[SpHolidayExceptionEmployee_Insert]
(
 @HolidayId int = NULL, 
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
    
    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0

    IF EXISTS(SELECT HolidayId 
                FROM  [Managements].[HolidayExceptionEmployeeLink]
               WHERE HolidayId = @HolidayId
                 AND EmployeeId = @EmployeeId)                    
    BEGIN
	  SET @RMsgId = 155 -- الموظف مدخل مسبقا في هذا الإستثناء 
	  SET @FieldInError = 'EmployeeId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END
    
    BEGIN TRY	
		INSERT INTO  [Managements].[HolidayExceptionEmployeeLink]
				   ([HolidayId]
				   ,[EmployeeId]
				   ,[AddedUserAccountId]
				   ,[UpdatedUserAccountId]
				   ,[AddedDate]
				   ,[UpdatedDate])
			 VALUES
				   (@HolidayId,
				    @EmployeeId,
				    @UserAccountId,
				    @UserAccountId,
				    GETDATE(),
				    GETDATE())					    		        
		
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
