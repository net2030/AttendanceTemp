USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_Delete]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	delete vacation
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_Delete]
(
 @VacationId     int = NULL, 
 
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
    DECLARE @IsApproved bit
	DECLARE @EmployeeId AS INT
	DECLARE @AttendanceDate AS DATE
	DECLARE @VacationStartDate AS Date
	DECLARE @VacationENDDate AS DATE
	DECLARE @VacationAddedDate AS DATETIME
	DECLARE @TypeId AS int
	DECLARE @Today as Date=GETDATE()

    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0


    BEGIN TRY
        IF NOT EXISTS(SELECT VacationId 
                        FROM  [Managements].[Vacation]
                       WHERE VacationId = @VacationId)                    
        BEGIN
		  SET @RMsgId = 138 -- رقم الإجازة غير صحيح أو غير موجود 
		  SET @FieldInError = 'VacationId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END		
		-- Set Parameters before delete

        SELECT @EmployeeId=EmployeeId,
		       @TypeId=TypeId, 
			   @VacationStartDate=EffectiveDate,
			   @VacationENDDate=DateExpire, 
			   @IsApproved=IsApproved ,
			   @VacationAddedDate = AddedDate
	    FROM Managements.Vacation 
		WHERE VacationId=@VacationID

		DELETE FROM Managements.VacationApproval
		WHERE VacationId = @VacationID

        DELETE  [Managements].[Vacation]
		WHERE VacationId = @VacationId		        				      
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' +  
		                  (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		-- RETURN
		END	
		ELSE
		BEGIN -- Failed
		  SET @RMsgId = 2 --  فشلت عملية تحديث البيانات
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + 
		                  (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
       END     
	   
	   IF @IsApproved=1
	    BEGIN
			-- Process Attendance Status
			SET  @AttendanceDate =@VacationStartDate
			WHILE @AttendanceDate<=@Today  AND @AttendanceDate <= @VacationENDDate  
				BEGIN
					EXEC logs.SpProcessAttendanceByEmployee @AttendanceDate,@EmployeeId
					SET @AttendanceDate=DATEADD(day,1,@AttendanceDate )
				END  

            --------------------------------
			DECLARE @Period int =DATEDIFF(Day,@VacationStartDate,@VacationENDDate)+1	   
			DECLARE @Available decimal(6, 2) 
			DECLARE @CarryForward decimal(6, 2)
			DECLARE @Used decimal(6, 2)
			DECLARE @BalanceID INT
			DECLARE @PolicyDate DATETIME
			DECLARE @BalanceAddedDate DATETIME

			SELECT @BalanceID = TOB.TimeOffBalanceId,
				@Available = TOB.Available,
				@Used =  TOB.Used,
				@CarryForward = TOB.CarryForward,
				@PolicyDate = GP.AddedDate,
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

					 
			IF @PolicyDate < @VacationAddedDate
			BEGIN
			SET @Available = @Available + @Period
			SET @Used = @Used - @Period
			END

			UPDATE  Managements.TimeOffBalance  
			SET     Available = @Available ,
					Used =  @Used,
					CarryForward = @CarryForward
            WHERE TimeOffBalanceId = @BalanceID
			---------------------------------------------------------


	    END
	    
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         SET @RMsgId = 3  --  حدث خطاء في إجراء قاعدة البيانات - الرجاء الإتصال بمسئول النظام للمساعدة
		 SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + 
		                 (SELECT MsgText 
		                    FROM  [common].[CustomMessage] 
		                   WHERE [MsgId] = @RMsgId)
        RETURN    
    END CATCH
END







GO
