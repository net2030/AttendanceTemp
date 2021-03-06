USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepass_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	update gatepass
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepass_Update]
(
 @GatepassId       int = NULL, 
 @EmployeeId       int = NULL,
 @GatepassTypeId   int = NULL,
 @GatepassBegDate  date = NULL,
 @GatepassEndDate  date = NULL,
 @GatepassBegTime  datetime = NULL,
 @GatepassEndTime  datetime = NULL,
 @ApprovedEmployeeId int = NULL,
 @Notes            nvarchar(200) = NULL,
 
 @VersionNo        timestamp = NULL,
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
	DECLARE @DayNo int
    DECLARE @Days int
    DECLARE @RowEffected int
    DECLARE @STime as datetime
    DECLARE @ETime as datetime
	DECLARE @ActualGatepassSTime as datetime
	DECLARE @ActualGatepassETime as datetime
    DECLARE @Available int  
	DECLARE @Pending int   
	DECLARE @MinValue int
	DECLARE @MaxValue int
	DECLARE @IsApproved bit=0
	DECLARE @STimeOld DATETIME
	DECLARE @ETimeOld DATETIME


	--Checking If the gatepass Is Out Of WorkingTime


    DECLARE @IsWorkingDay bit  
    DECLARE @WorkScheduleId int
    DECLARE @WorkBegTime datetime 
    DECLARE @WorkEndTime datetime
    DECLARE @PolicyId int
    DECLARE @WorkingMinutes int
    DECLARE @LateInMinutes int
    DECLARE @LateOutMinutes int
    DECLARE @MarkObsentDuration int
    DECLARE @PolicyVersionNo timestamp
    DECLARE @WorkScheduleVersionNo timestamp
    DECLARE @EarlyInMinutes int = 0
    DECLARE @EarlyOutMinutes int = 0
    DECLARE @IsShiftWorkSchedule bit


	

	DECLARE @GatePassMinutes INT

	DECLARE @ShiftNo INT
	DECLARE @GatePassShiftType INT

	DECLARE @Period int 
	DECLARE @CarryForward decimal(6, 2)
	DECLARE @Used decimal(6, 2)
	DECLARE @BalanceOfUpdatedRecord  decimal(6, 2)  = 00.00
	DECLARE @BalanceID INT
	DECLARE @GatepassAddedDate AS DATETIME
	DECLARE @PolicyDate DATETIME
	DECLARE @BalanceAddedDate DATETIME

  BEGIN TRY	

	 -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0

	SELECT    @MinValue= Convert(int,TOPD.MinValue),
	          @MaxValue= Convert(int,TOPD.MaxValue),
	          @Available= Convert(int,TOB.Available),
	          @CarryForward = Convert(int,TOB.CarryForward),
			  @Used = Used,
	          @PolicyDate = GP.AddedDate,
			  @BalanceAddedDate = TOB.AddedDate
	FROM  Managements.TimeOffBalance TOB 
			INNER JOIN Managements.TimeOffPolicyDetail TOPD
			ON TOB.TimeOffPolicyDetailId = TOPD.TimeOffPolicyDetailId 
			INNER JOIN   Managements.GatepassType
			ON TOPD.GatepassTypeId = Managements.GatepassType.GatepassTypeId
			INNER JOIN Managements.TimeOffPolicy GP ON GP.TimeOffPolicyId=TOPD.TimeOffPolicyId
    WHERE TOB.EmployeeId=@EmployeeID AND  GP.TypeId=3 AND Managements.GatepassType.GatepassTypeId=@GatepassTypeId

	    --Set @IsApproved to Check If the update after approval
    SELECT @IsApproved =IsApproved ,
	       @STimeOld=GatepassBegTime,
		   @ETimeOld=GatepassEndTime,
		   @GatepassAddedDate = AddedDate
	FROM Managements.Gatepass 
	WHERE GatepassId=@GatepassId  
	   
	SET @STime = SUBSTRING(CAST(@GatepassBegTime AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(@GatepassBegTime AS NVARCHAR(30)),18 ,2)
    SET @ETime = SUBSTRING(CAST(@GatepassEndTime AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(@GatepassEndTime AS NVARCHAR(30)),18 ,2)    

	SET @ActualGatepassSTime = CONVERT(DATETIME, CONVERT(CHAR(10), @GatepassBegDate, 112) + ' ' + CONVERT(CHAR(12),  CAST( @STime AS time), 108))
	SET @ActualGatepassETime =CONVERT(DATETIME, CONVERT(CHAR(10), @GatepassBegDate, 112) + ' ' + CONVERT(CHAR(12),  CAST( @ETime AS time), 108))
	
	IF @MaxValue IS NOT NULL AND @MaxValue <> 0 AND DATEDIFF(MI,@STime,@ETime) + 1  > @MaxValue AND @GatepassAddedDate > @BalanceAddedDate
	BEGIN
		  SET @RMsgId = 175 --يجب ان يكون الطلب أقل من او يساوي الحد الأقصى 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText + '   (' +  CAST(@MaxValue as nvarchar(3)) + '  دقيقة)'  FROM  [common].[CustomMessage]   WHERE MsgId = @RMsgId)
		  RETURN        
        END   
	  
	IF @MinValue IS NOT NULL AND @MinValue<>0 AND DATEDIFF(MI,@STime,@ETime)+1  <@MinValue AND @GatepassAddedDate > @BalanceAddedDate
	BEGIN
		  SET @RMsgId = 176 --ايجب أن يكون الطلب أكبر من او يساوي الحد الأدنى
		  SET @FieldInError = 'EmployeeId'
		   SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText + '   (' +  CAST(@MinValue as nvarchar(3)) + '  دقيقة)'  FROM  [common].[CustomMessage]   WHERE MsgId = @RMsgId)
		  RETURN        
    END   

	-- Check Gatepass overlap

		IF EXISTS(SELECT GatepassId
		          FROM Managements.Gatepass 
				  WHERE(GatepassId<>@GatepassId AND EmployeeId=@EmployeeId AND GatepassBegDate=@GatepassBegDate AND (@ActualGatepassSTime<=GatepassBegTime AND @ActualGatepassETime>=GatepassBegTime))  --New Will overlap befor
				    OR (GatepassId<>@GatepassId AND EmployeeId=@EmployeeId AND GatepassBegDate=@GatepassBegDate AND (@ActualGatepassSTime<=GatepassEndTime AND @ActualGatepassETime>=GatepassEndTime))--New Will overlap after
					OR (GatepassId<>@GatepassId AND EmployeeId=@EmployeeId AND GatepassBegDate=@GatepassBegDate AND (@ActualGatepassSTime>=GatepassBegTime AND @ActualGatepassETime<=GatepassEndTime))--New will contained by the old Or equal
				    OR (GatepassId<>@GatepassId AND EmployeeId=@EmployeeId AND GatepassBegDate=@GatepassBegDate AND (@ActualGatepassSTime<=GatepassBegTime AND @ActualGatepassETime>=GatepassEndTime)) --New Will include the old or equal
			  )

      BEGIN
		  SET @RMsgId = 173 --  مكررة 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 

	 -- Check if overlap with vacation

	    IF EXISTS(SELECT VacationId
		          FROM Managements.Vacation 
				  WHERE EmployeeId=@EmployeeId AND (@GatepassBegDate >= EffectiveDate AND @GatepassBegDate <=DateExpire ) )
				
        BEGIN
		  SET @RMsgId = 200 -- الإستئذان يتعارض مع اجازة 
		  SET @FieldInError = 'GatepassBegDate'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 


		-- Check if overlaps with work exception


	    IF EXISTS(SELECT WorkExceptionId
		          FROM Managements.WorkException 
				  WHERE EmployeeId=@EmployeeId AND (@GatepassBegDate>= WorkExceptionBegDate AND  @GatepassBegDate<= WorkExceptionEndDate) )
				
        BEGIN
		  SET @RMsgId = 201 -- الإستئذان يتعارض مع إستثناء 
		  SET @FieldInError = 'GatepassBegDate'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 


	 /*****************************************************
		Get Work Schedule 
	  *****************************************************/         
     EXECUTE Logs.SpGetEmployeeWorkSchedule @GatepassBegDate, 
                                            @EmployeeId, 
                                            @WorkScheduleId OUTPUT,
											@PolicyId  OUTPUT,
											@LateInMinutes OUTPUT,
											@LateOutMinutes OUTPUT,
											@MarkObsentDuration OUTPUT,
											@EarlyInMinutes OUTPUT,
											@EarlyOutMinutes OUTPUT ,
										    @IsShiftWorkSchedule OUTPUT													                                                 
	 /*****************************************************
		Check Day Of week
	  *****************************************************/ 
	 IF @IsShiftWorkSchedule=0    
	 
	 BEGIN  
	                               
     SET DATEFIRST 6
	 SET @DayNo = DATEPART(dw, @GatepassBegDate)  

	 IF NOT EXISTS(
	 SELECT WeekDayNo 
	 FROM Managements.WorkScheduleDay 
	 WHERE WeekDayNo =  @DayNo
	   AND IsWeekendDay = 0 )

	BEGIN
	      SET @RMsgId = 181 -- يجب ان يكون الإستئذان في يوم عمل
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
	END

	/***************************************************************************************************************************************
	 SELECT @ShiftNo = ShiftNo 
	 FROM Managements.WorkScheduleDay 
	 WHERE WeekDayNo =  @DayNo
	   AND WorkBegTime = @STime 

    IF @ShiftNo IS NULL
	SELECT @ShiftNo = ShiftNo 
		 FROM Managements.WorkScheduleDay 
		 WHERE WeekDayNo =  @DayNo
		   AND WorkEndTime = @ETime 

    IF @ShiftNo IS NULL
	BEGIN
		SET @RMsgId = 183 -- Please check gatepass Intime and gatepass out time 
		SET @FieldInError = 'EmployeeId'
		SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		RETURN       
	END


   DECLARE @ShiftMinutes INT
   SELECT @ShiftMinutes = DATEDIFF(MINUTE,WorkBegTime,WorkEndTime) 
   FROM Managements.WorkScheduleDay
   WHERE WeekDayNo = @DayNo
   AND ShiftNo = @ShiftNo 


   IF @GatePassMinutes > @ShiftMinutes
   BEGIN
		SET @RMsgId = 184 -- Gatepass minutes must not exceeds shift minutes 
		SET @FieldInError = 'EmployeeId'
		SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		RETURN      
   END

   ********************************************************************************************************************************/
	END
	

	IF NOT EXISTS(SELECT EmployeeId 
                        FROM  [Employees].[Employee]
                       WHERE EmployeeId = @EmployeeId)                    
        BEGIN
		  SET @RMsgId = 120 -- رقم الموظف غير صحيح أو غير موجود 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 

    IF NOT EXISTS(SELECT GatepassTypeId 
                    FROM  [Managements].[GatepassType]
                   WHERE GatepassTypeId = @GatepassTypeId)                    
    BEGIN
	  SET @RMsgId = 159 -- رقم نوع الإستئذان غير صحيح أو غير موجود 
	  SET @FieldInError = 'GatepassTypeId'
	  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END
    
	 IF NOT EXISTS(SELECT * 
	              FROM Managements.WorkScheduleDay
				  WHERE WorkScheduleId = @WorkScheduleId AND WeekDayNo = @DayNo AND (@STime >= WorkBegTime AND @STime <= WorkEndTime) AND (@ETime >= WorkBegTime AND @ETime <= WorkEndTime))
      BEGIN
		SET @RMsgId = 185 -- يجب أن يكون الإستئذان ضمن وقت الدوام
		SET @FieldInError = 'EmployeeId'
		SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		RETURN      
     END


   -- Check Balance
   IF @Available IS NOT NULL
	   BEGIN

		   SELECT @Pending=SUM(DATEDIFF(MINUTE,GatepassBegTime,GatepassEndTime)) FROM Managements.Gatepass WHERE EmployeeId=@EmployeeId AND GatepassTypeId=@GatepassTypeId AND IsApproved=0 AND IsRejected=0 AND Gatepassid<>@GatepassId
		   IF @Pending IS  NULL
		   SET @Pending = 0

		   IF @IsApproved=1 --Include The Updated Record Balance
		     SET @BalanceOfUpdatedRecord= DATEDIFF(MINUTE,@STimeOld,@ETimeOld)
		   ELSE
			 SET @BalanceOfUpdatedRecord= 0.00

		        
			   IF @GatepassAddedDate > @BalanceAddedDate -- Check if the gatepass before Policy
			   IF @Available + @CarryForward + @BalanceOfUpdatedRecord < DATEDIFF(MINUTE,@STime,@ETime) + @Pending  
			   BEGIN
				 SET @RMsgId = 167 -- لايوجد رصيد كافي 
				  SET @FieldInError = 'TypeId'
				  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
				  RETURN        
			   END
	    END

           
		UPDATE  [Managements].[Gatepass]
		   SET [EmployeeId] = @EmployeeId,
			   [GatepassBegDate] = @GatepassBegDate,
			   [GatepassEndDate] = @GatepassEndDate,
			   [GatepassBegTime] = @ActualGatepassSTime,
			   [GatepassEndTime] = @ActualGatepassETime,
			   [GatepassTypeId] = @GatepassTypeId,
			   [ApprovedEmployeeId] = @ApprovedEmployeeId,
			   [Notes] = @Notes,
			   IsRejected=0,
			   IsApproved=0,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate] = GETDATE()
         WHERE GatepassId = @GatepassId
           AND VersionNo = @VersionNo		        				       
		
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
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [GatepassId] 
		                                      FROM  [Managements].[Gatepass]
		                                     WHERE [GatepassId] = @GatepassId
	                                           AND [VersionNo]  = @VersionNo))
		BEGIN
		  SET @RMsgId = 5  -- تم تحديث السجل من قبل مستخدم آخر
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
	   
	   --DELETE Old Approval Records
	   DELETE FROM Managements.GatepassApproval 
	   WHERE GatepassId=@GatepassId

	   -------------------Check If the update after approval
	  If @IsApproved=1
		        BEGIN
				 -- Process Attendance Status
				    EXEC logs.SpProcessAttendanceByEmployee @GatepassBegDate,@EmployeeId

					
              --------------------------------
					
					SET @Period  = DATEDIFF(MINUTE,@STimeOld,@ETimeOld)



					 --No Need To Change Balance
					IF @BalanceAddedDate > @GatepassAddedDate
					RETURN
					 
		
					SET @Used = @Used - @Period
					SET @Available = @Available + @Period

					UPDATE  Managements.TimeOffBalance  
					SET     Available = @Available ,
							Used =  @Used,
							CarryForward = @CarryForward
                    WHERE TimeOffBalanceId = @BalanceID
			---------------------------------------------------------
			

		 END
      ----------------------------------------------------

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
