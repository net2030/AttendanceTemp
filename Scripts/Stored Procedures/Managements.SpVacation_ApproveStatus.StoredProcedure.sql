USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_ApproveStatus]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get gatepass by id
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_ApproveStatus]
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
		       SET IsApproved=@IsApproved,
			       IsRejected=@IsRejected,
				   ApprovalNotes=@Notes,
				   ApprovedDate=GetDate()
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


		     If @IsApproved=1
		        BEGIN

				  -- Process Attendance Status
					DECLARE @EmployeeId AS INT
					DECLARE @VacationStartDate AS Date
					DECLARE @VacationENDDate AS DATE
					DECLARE @AttendanceDate AS DATE
					DECLARE @TypeId AS int
					DECLARE @Today as Date=GETDATE()
					DECLARE @BalanceAddedDate DATETIME
					DECLARE @VacationAddedDate AS DATETIME

					SELECT @EmployeeId=EmployeeId,
					       @TypeId=TypeId, 
						   @VacationStartDate=EffectiveDate,
						   @VacationENDDate=DateExpire ,
						   @VacationAddedDate = AddedDate
					FROM Managements.Vacation 
					WHERE VacationId=@VacationID
					
					SET @AttendanceDate = @VacationStartDate

					WHILE @AttendanceDate<=@Today  AND @AttendanceDate <= @VacationENDDate  
					  BEGIN
							EXEC logs.SpProcessAttendanceByEmployee @AttendanceDate,@EmployeeId
							SET @AttendanceDate=DATEADD(day,1,@AttendanceDate )
					   END 
					   
					    
					 DECLARE @Period int 
					 DECLARE @Available decimal(6, 2) 
					 DECLARE @CarryForward decimal(6, 2)
					 DECLARE @Used decimal(6, 2)
					 DECLARE @BalanceID INT

					SELECT @BalanceID = TOB.TimeOffBalanceId,
					        @Available = TOB.Available,
					        @Used =  TOB.Used,
							@CarryForward = TOB.CarryForward,
							@BalanceAddedDate = TOB.AddedDate
					FROM Managements.TimeOffBalance  TOB
					INNER JOIN Managements.TimeOffPolicyDetail 
					ON TOB.TimeOffPolicyDetailId = Managements.TimeOffPolicyDetail.TimeOffPolicyDetailId 
					INNER JOIN   Managements.VacationType
					ON Managements.TimeOffPolicyDetail.GatepassTypeId = Managements.VacationType.TypeId
					INNER JOIN Managements.TimeOffPolicy GP ON GP.TimeOffPolicyId=Managements.TimeOffPolicyDetail.TimeOffPolicyId
					WHERE TOB.EmployeeId=@EmployeeID AND  GP.TypeId=1 AND Managements.VacationType.TypeId=@TypeId
					  
				     --No Need To Change Balance
					 IF @BalanceAddedDate > @VacationAddedDate
					 RETURN

					 SET @Period=DATEDIFF(Day,@VacationStartDate,@VacationENDDate)+1

					 IF @Period <= @Available
					  SET @Available = @Available - @Period
					 ELSE
					 BEGIN
					 SET @CarryForward =  @CarryForward - (@Period - @Available)
					 SET @Available = 0
					 END

					 SET @Used = @Used + @Period

					  UPDATE  Managements.TimeOffBalance  
					  SET     Available = @Available ,
					          Used =  @Used,
							  CarryForward = @CarryForward
                     WHERE TimeOffBalanceId = @BalanceID


			 END


		RETURN

    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END










GO
