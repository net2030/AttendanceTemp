USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpNationality_Insert]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert a new Nationality record
-- =============================================
CREATE PROCEDURE [Employees].[SpNationality_Insert]
(
 @NationalityId    int = NULL OUTPUT, 
 @NationalityName  nvarchar(50) = NULL,
 
 @UserAccountId    int = NULL,
 @FieldInError     nvarchar(50) = '' OUTPUT,
 @RMsgId           int = NULL OUTPUT,
 @RMessage         nvarchar(200) = '' OUTPUT, 
 @RC               int = NULL OUTPUT
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
        IF EXISTS(SELECT NationalityName 
                    FROM  [Employees].[Nationality] 
                   WHERE NationalityName = @NationalityName)
        BEGIN
		  SET @RMsgId = 105 -- إسم الجنسية مكرر 
		  SET @FieldInError = 'NationalityName'
		  SET @RMessage = (SELECT MsgText 
		                   FROM  [common].[CustomMessage] 
		                   WHERE MsgId = @RMsgId)
		  RETURN        
        END	
		
        
        INSERT INTO	 [Employees].[Nationality]
                       ([NationalityName],
                        [AddedUserAccountId],
                        [AddedDate],
                        [UpdatedUserAccountId],
                        [UpdatedDate])
               VALUES
				       (@NationalityName,
				        @UserAccountId,
				        GETDATE(),
				        @UserAccountId,
				        GETDATE())				        
				        
		-- Save The New ID Value
		SET @NationalityId = SCOPE_IDENTITY()
		
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
