USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Security].[SpRole_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah (saad alqarni)
-- Create date: 7/2/2012
-- Description:	Insert a new Direction record
-- =============================================
CREATE PROCEDURE [Security].[SpRole_Insert]
(
 @RoleId	    int = NULL OUTPUT, 
 @RoleName		nvarchar(50) = NULL,
 @LDAPRole  nvarchar(50) = NULL, 
 @IsApprover   bit=0 ,
 @IsMasterApprover bit=0,
 
 @AddedUserAccountId  int = NULL,
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
		IF (@RoleName IS NULL OR @RoleName = '')
        BEGIN
		  SET @RMsgId = 131 -- يجب إدخال أسم المجموعة 
		  SET @FieldInError = 'RoleName'
		  SET @RMessage = (SELECT MsgText 
		                   FROM  [common].[CustomMessage] 
		                   WHERE MsgId = @RMsgId)
		  RETURN        
        END    

       
        
		INSERT INTO  [Security].[Role] 
				   ([RoleName],
				    [LDAPRole],
					[IsApprover],
					[IsMasterApprover] ,
				    [AddedUserAccountId],
				    [AddedDate],
				    [UpdatedUserAccountId],
				    [UpdatedDate])				    
			 VALUES
				   (@RoleName,
				    @LDAPRole, 
					@IsApprover ,
					@IsMasterApprover ,
				    @AddedUserAccountId,
				    GETDATE(),
				    @AddedUserAccountId,
				    GETDATE())				    

		-- Save The New ID Value
		SET @RoleId = SCOPE_IDENTITY()

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
