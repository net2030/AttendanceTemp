USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpUserAccount_GetUserByEmployeeId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Users].[SpUserAccount_GetUserByEmployeeId]
(
 @EmployeeId    int = NULL,
 
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
    
    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0
       
    BEGIN TRY    
		SELECT [UserAccountId]
			  ,[WindowsAccountName]
			  ,[UserName]
			  ,[IsActive]
			  ,[EMail]
			  ,[EmployeeId]
			  ,[AddedUserAccountId]
			  ,[UpdatedUserAccountId]
			  ,[AddedDate]
			  ,[UpdatedDate]
			  ,[VersionNo]
		  FROM  [Users].[UserAccount]
		 WHERE EmployeeId = @EmployeeId
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		RETURN
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         SET @RMsgId = 8  --  حدث خطاء في إجراء قاعدة البيانات - الرجاء الإتصال بمسئول النظام للمساعدة
		 SET @RMessage = (SELECT MsgText 
		                    FROM  [common].[CustomMessage] 
		                   WHERE [MsgId] = @RMsgId)
        RETURN    
    END CATCH	
END






GO
