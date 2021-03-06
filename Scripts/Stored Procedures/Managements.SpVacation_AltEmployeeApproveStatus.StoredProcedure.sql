USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_AltEmployeeApproveStatus]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get gatepass by id
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_AltEmployeeApproveStatus]
(
@VacationID   int,
@IsApproved   bit,
@IsRejected   bit,
@Notes        nvarchar(200),

@UserAccountId  int = NULL,
 @FieldInError   nvarchar(50) = '' OUTPUT,
 @RMsgId         int = NULL OUTPUT,
 @RMessage       nvarchar(200) = '' OUTPUT, 
 @RC             int = NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RowEffected int
    BEGIN TRY
		            

		UPDATE Managements.Vacation 
		       SET IsAltEmpApproved=@IsApproved,
			       IsAltEmpRejected=@IsRejected,
				   ApprovalNotes=@Notes
         WHERE VacationId=@VacationID

		 SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  
		END
		ELSE
		BEGIN -- Failed
		  SET @RMsgId = 2 --  فشلت عملية تحديث البيانات
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
       END      

		RETURN

    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END











GO
