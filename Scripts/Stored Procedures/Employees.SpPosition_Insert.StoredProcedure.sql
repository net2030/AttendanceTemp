USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpPosition_Insert]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Insert a new position record
-- =============================================
CREATE PROCEDURE [Employees].[SpPosition_Insert]
(
 @PositionId      int = NULL OUTPUT, 
 @PositionName    nvarchar(50) = NULL,
 @PositionTypeId  int = NULL, 
 @SortIndex      int = NULL,
 @IsOfficer      bit = 0, 
 
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
    
    IF @PositionTypeId > 1
    BEGIN
      SET @IsOfficer = 0
    END
        
    BEGIN TRY
        IF NOT EXISTS(SELECT PositionTypeId 
                        FROM  [Employees].[PositionType]
                       WHERE PositionTypeId = @PositionTypeId)                    
        BEGIN
		  SET @RMsgId = 109 -- رقم نوع الرتبة/المرتبة غير صحيح أو غير موجود 
		  SET @FieldInError = 'PositionTypeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
            
        IF EXISTS(SELECT PositionName 
                    FROM  [Employees].[Position] 
                   WHERE PositionName   = @PositionName
                     AND PositionTypeId = @PositionTypeId)
        BEGIN
		  SET @RMsgId = 110 -- أسم الرتبة/المرتبة مكرر 
		  SET @FieldInError = 'PositionName'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
                
		INSERT INTO  [Employees].[Position]
				   ([PositionName],
				    [PositionTypeId],
				    [SortIndex],
				    [IsOfficer],
                    [AddedUserAccountId],
                    [AddedDate],
                    [UpdatedUserAccountId],
                    [UpdatedDate])
			 VALUES
				   (@PositionName,
				    @PositionTypeId,
				    @SortIndex,
				    @IsOfficer,				    
				    @UserAccountId,
				    GETDATE(),
				    @UserAccountId,
				    GETDATE())				    

		-- Save The New ID Value
		SET @PositionId = SCOPE_IDENTITY()

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
