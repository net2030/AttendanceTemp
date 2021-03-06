USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkException1_ApproveStatus]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get gatepass by id
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkException1_ApproveStatus]
(
@WorkExceptionID   int,
@WorkExceptionBegDate date = NULL,
@WorkExceptionEndDate date = NULL,
@IsApproved        bit,
@IsRejected        bit,
@IsPaid            bit,
@DueAmount        decimal(6,2),
@PaidAmount        decimal(6,2),
@Notes             nvarchar(200),

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
		            

		UPDATE Managements.WorkException1
		       SET [WorkExceptionBegDate] = @WorkExceptionBegDate,
			       [WorkExceptionEndDate] = @WorkExceptionEndDate,
			       IsApproved=@IsApproved,
			       IsRejected=@IsRejected,
				   IsPaid=@IsPaid,
				   DueAmount=@DueAmount,
				   AmountPaid=@PaidAmount,
				   ApprovalNotes=@Notes
         WHERE WorkExceptionId=@WorkExceptionID

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

	   --if @IsApproved=1
	   --EXEC [Managements].[SpWorkException_ApproveStatus] @WorkExceptionID,@IsApproved,@IsRejected,@Notes,@UserAccountId


    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END












GO
