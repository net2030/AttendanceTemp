USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepass_Delete]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	delete gatepass
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepass_Delete]
(
 @GatepassId       int = NULL, 
 
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
    DECLARE @IsApproved bit
	DECLARE @EmployeeId AS INT
	DECLARE @GatepassBegTime AS Datetime
	DECLARE @GatepassEndTime AS DATETime
	DECLARE @GatepassDate AS DATE
	DECLARE @GatepassAddedDate AS DATETIME
	DECLARE @TypeId AS int
	DECLARE @Today as Date=GETDATE()

    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0

    BEGIN TRY	
	
	
	    SELECT @EmployeeId=EmployeeId,
		       @TypeId=GatepassTypeId, 
			   @GatepassBegTime=GatepassBegTime,
			   @GatepassEndTime=GatepassEndTime, 
			   @GatepassDate=GatepassBegDate,
			   @GatepassAddedDate = AddedDate,
			   @IsApproved=IsApproved 
	    FROM Managements.Gatepass 
		WHERE GatepassId=@GatepassID
		
		            
		DELETE  [Managements].[Gatepass]
        WHERE GatepassId = @GatepassId		        				       
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		 -- RETURN
		END		
		ELSE
		BEGIN -- Failed
		  SET @RMsgId = 2 --  فشلت عملية تحديث البيانات
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
       END 
	   
	   IF @IsApproved=1
	    BEGIN
			-- Process Attendance Status

		    EXEC logs.SpProcessAttendanceByEmployee @GatepassDate,@EmployeeId


			--------------------------------
			DECLARE @Period int = DATEDIFF(MINUTE,@GatepassBegTime,@GatepassEndTime)
			DECLARE @Available decimal(6, 2) 
			DECLARE @CarryForward decimal(6, 2)
			DECLARE @Used decimal(6, 2)
			DECLARE @BalanceID INT
			DECLARE @BalanceAddedDate DATETIME

			SELECT @BalanceID = TOB.TimeOffBalanceId,
				@Available = TOB.Available,
				@Used =  TOB.Used,
				@CarryForward = TOB.CarryForward,
				@BalanceAddedDate = TOB.AddedDate
			FROM Managements.TimeOffBalance  TOB
					
			INNER JOIN Managements.TimeOffPolicyDetail 
			ON TOB.TimeOffPolicyDetailId = Managements.TimeOffPolicyDetail.TimeOffPolicyDetailId 
			INNER JOIN   Managements.GatepassType
			ON Managements.TimeOffPolicyDetail.GatepassTypeId = Managements.GatepassType.GatepassTypeId
			INNER JOIN Managements.TimeOffPolicy GP ON GP.TimeOffPolicyId=Managements.TimeOffPolicyDetail.TimeOffPolicyId
			WHERE TOB.EmployeeId=@EmployeeID AND  GP.TypeId=3 AND Managements.GatepassType.GatepassTypeId=@TypeId  

			 --No Need To Change Balance
			IF @BalanceAddedDate > @GatepassAddedDate
			RETURN
					 

			SET @Available = @Available + @Period
			SET @Used = @Used - @Period


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
		 SET @RMessage = (SELECT MsgText 
		                    FROM  [common].[CustomMessage] 
		                   WHERE [MsgId] = @RMsgId)
        RETURN    
    END CATCH
END






GO
