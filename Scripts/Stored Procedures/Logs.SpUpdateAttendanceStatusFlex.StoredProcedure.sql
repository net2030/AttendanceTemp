USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpUpdateAttendanceStatusFlex]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Update Attendance Status
-- =============================================
CREATE PROCEDURE [Logs].[SpUpdateAttendanceStatusFlex]
(
 @AttendanceDate     datetime = NULL,
 @LogId              int = NULL
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

    /*****************************************************
     Local Variables Declaration
    *****************************************************/	
	DECLARE @EmployeeId         int = NULL
	DECLARE @InTime             datetime = NULL
	DECLARE @OutTime            datetime = NULL
	DECLARE @WorkStartTime      datetime = NULL
	DECLARE @WorkEndTime        datetime = NULL
	DECLARE @WorkEndTimeFlex        datetime = NULL
	DECLARE @LateInMinutes      int = 0
	DECLARE @LateOutMinutes     int = 0
	DECLARE @MarkObsentDuration int = 0
	DECLARE @StatusId           int = NULL
	DECLARE @ActionId           int = NULL
    DECLARE @EarlyInMinutes     int = 0
    DECLARE @EarlyOutMinutes    int = 0

    DECLARE @IsExempt int
	DECLARE @IsAllowOvertime bit
	
	DECLARE @WBegTime datetime
	DECLARE @WEndTime datetime
    
    DECLARE @ActualTime as time
	DECLARE @WorkingMinuts as int
    DECLARE @ActualMinuts as int
	DECLARE @OutMinutes as int
    DECLARE @LogDate as Date
	DECLARE @CountIn   int=0
	DECLARE @CountOut  int=0

    
 	DECLARE @EarlyInTime datetime
	DECLARE @EarlyOutTime datetime
	
    DECLARE @IsEarlyCheckRequired bit = 0
    DECLARE @IsProcessed bit = 0
    
    DECLARE @TodayDate date = GETDATE()
    DECLARE @CurTime datetime

	DECLARE @ExtraMinuts int
    
   -- SET @CurTime = SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),18 ,2)
	SET @CurTime = GETDATE()
    
    /*****************************************************
     Set local Variable values
    *****************************************************/    
    SELECT @EmployeeId = EmployeeId,
           @LogDate=Logdate,
           @InTime = CONVERT(VARCHAR(16),InTime, 121) ,
           @OutTime = OutTime,
           @WorkStartTime = WorkStartTime,
           @WorkEndTime = WorkEndTime,
		   @WorkingMinuts=WorkingMinutes,
           @LateInMinutes = LateInMinutes,
           @LateOutMinutes = LateOutMinutes,
           @MarkObsentDuration = MarkObsentDuration,
           @StatusId = StatusId,
           @ActionId = ActionId,
		   @IsExempt = IsExempt,
		   @IsAllowOvertime = IsAllowOvertime,
           @EarlyInMinutes = EarlyInMinutes,
           @EarlyOutMinutes = EarlyOutMinutes           
      FROM Logs.AttendanceLog
     WHERE LogId = @LogId
    BEGIN TRY		
	
	  IF @TodayDate<@AttendanceDate OR( @TodayDate=@AttendanceDate AND @CurTime < @WorkStartTime)  
	   BEGIN
		UPDATE Logs.AttendanceLog
			SET Logs.AttendanceLog.StatusId = 50, -- لم يبدأ الدوام بعد
				Logs.AttendanceLog.LateMinutes = 0,
				Logs.AttendanceLog.InLateMinutes = 0,
				Logs.AttendanceLog.OutLateMinutes = 0,					   
				Logs.AttendanceLog.InExtraMinutes = 0,
				Logs.AttendanceLog.OutExtraMinutes = 0,					   
				Logs.AttendanceLog.ExtraTimeMinutes = 0,					  
				Logs.AttendanceLog.UpdatedUserAccountId  = 1,
				Logs.AttendanceLog.UpdatedDate  = GETDATE(),
				Logs.AttendanceLog.UpdateVersion = 1                       
			WHERE Logs.AttendanceLog.LogId = @LogId	


		    RETURN	  
	   END		  
	 
	  
	  					    		
	  SET @WBegTime = @WorkStartTime
	  SET @WEndTime = @WorkEndTime

	  SET @EarlyInTime = @WorkStartTime
	  SET @EarlyOutTime = @WorkEndTime


	  	if @AttendanceDate >='2019-05-05' AND @AttendanceDate <='2019-06-05' --Ramadan
	   BEGIN
			if @InTime <@WorkStartTime 
			SET @WorkEndTime=DATEADD(mi,@WorkingMinuts,@InTime)
			--SET @WorkStartTime = @InTime
	   END
 

    

	/********************
	if @AttendanceDate >='2017-05-28' AND @AttendanceDate <='2017-06-28' --Ramadan
		if @InTime <@WorkStartTime 
		SET @WorkEndTime=DATEADD(mi,@WorkingMinuts,@InTime)
    else


	IF @InTime Is Not NULL and Datediff(minute,@WorkEndTime ,DATEADD(mi,@WorkingMinuts,@InTime))<=30 AND (@StatusId IS NOT NULL AND @StatusId<>20)
	  BEGIN
	  IF Datediff(minute,@WorkEndTime ,DATEADD(mi,@WorkingMinuts,@InTime))>15 OR Datediff(minute,@WorkEndTime ,DATEADD(mi,@WorkingMinuts,@InTime))<=0
	    SET @WorkEndTimeFlex=DATEADD(mi,@WorkingMinuts,@InTime)
	  END
    ELSE
	  SET @WorkEndTimeFlex=DATEADD(mi,30,@WorkEndTime)


	  if @WorkEndTimeFlex is NULL
	  SET @WorkEndTimeFlex=@WorkEndTime
	  ****************************/
	
      /*****************************************************
       Re-Calculate work Start & End times according to 
       work schedule policy
      *****************************************************/        
	  IF (@LateInMinutes IS NOT NULL AND @LateInMinutes > 0)
	  BEGIN
	    SET @WBegTime =  DATEADD(mi, @LateInMinutes, @WorkStartTime)
	  END
	  IF (@LateOutMinutes IS NOT NULL AND @LateOutMinutes > 0)
	  BEGIN
	    SET @WEndTime  =  DATEADD(mi, -@LateOutMinutes, @WorkEndTime)
	    --SET @WEndTime  =  DATEADD(mi, -@LateOutMinutes, @WorkEndTimeFlex)
	  END   		

	  IF (@EarlyInMinutes IS NOT NULL AND @EarlyInMinutes > 0)
	  BEGIN
	    SET @EarlyInTime =  DATEADD(mi, -@EarlyInMinutes, @WorkStartTime)
	    SET @IsEarlyCheckRequired = 1
	  END
	  IF (@EarlyOutMinutes IS NOT NULL AND @EarlyOutMinutes > 0)
	  BEGIN
	  SET @EarlyOutTime  =  DATEADD(mi, @EarlyOutMinutes, @WorkEndTime)
	    --SET @EarlyOutTime  =  DATEADD(mi, @EarlyOutMinutes, @WorkEndTimeFlex)
	    SET @IsEarlyCheckRequired = 1
	  END  
	  

	   /*****************************************************
       Check if Holiday
      *****************************************************/  
	  DECLARE @HolidyId INT  
	  SELECT @HolidyId=HolidayId FROM Managements.Holiday WHERE @AttendanceDate>= EffectiveDate AND @AttendanceDate <= DateExpire 
	  IF @HolidyId IS NOT NULL --There is a Holidy
	  IF NOT EXISTS(SELECT * FROM Managements.HolidayExceptionEmployeeLink WHERE HolidayId=@HolidyId AND EmployeeId=@EmployeeId) -- If the employee not excepted from holiday
	   BEGIN 
		UPDATE Logs.AttendanceLog
		   SET Logs.AttendanceLog.StatusId = 1, -- عطلة رسمية
			   Logs.AttendanceLog.LateMinutes = 0,
			   Logs.AttendanceLog.InLateMinutes = 0,
			   Logs.AttendanceLog.OutLateMinutes = 0,
			   Logs.AttendanceLog.ActualWorkMinutes = 0,
			   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
			   Logs.AttendanceLog.UpdatedDate = GETDATE(),
			   Logs.AttendanceLog.IsProcessCompleted = 1                     
		 WHERE Logs.AttendanceLog.LogId = @LogId
		 
		 SET @IsProcessed = 1	

		 RETURN		     
	  END

      /*****************************************************
       Check if employee on vacation
      *****************************************************/ 
	  DECLARE @VacationType INT  
	  SELECT @VacationType=TypeId FROM Managements.Vacation WHERE EmployeeId=@EmployeeId AND IsApproved=1 AND @AttendanceDate>= EffectiveDate AND @AttendanceDate <= DateExpire 
	  IF @VacationType IS NOT NULL --There is a Vacation      
	    BEGIN 
			UPDATE Logs.AttendanceLog
			   SET Logs.AttendanceLog.StatusId = @VacationType, -- إجازة
				   Logs.AttendanceLog.LateMinutes = 0,
				   Logs.AttendanceLog.InLateMinutes = 0,
				   Logs.AttendanceLog.OutLateMinutes = 0,
				   Logs.AttendanceLog.ActualWorkMinutes = 0,
				   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
				   Logs.AttendanceLog.UpdatedDate = GETDATE(),
				   Logs.AttendanceLog.IsProcessCompleted = 1                     
			 WHERE Logs.AttendanceLog.LogId = @LogId
		 
		 SET @IsProcessed = 1	
		 
		 RETURN		     
	   END

		DECLARE @WorkExceptionType INT  
		SELECT @WorkExceptionType=WorkExceptionTypeId FROM Managements.WorkException WHERE  EmployeeId=@EmployeeId AND IsApproved=1 AND @AttendanceDate>= WorkExceptionBegDate AND @AttendanceDate <= WorkExceptionEndDate 
		IF @WorkExceptionType IS NOT NULL --There is a WorkException      
		BEGIN
		UPDATE Logs.AttendanceLog
			SET Logs.AttendanceLog.StatusId = @WorkExceptionType,--WorkException 
				Logs.AttendanceLog.LateMinutes = 0,
				Logs.AttendanceLog.InLateMinutes = 0,
				Logs.AttendanceLog.OutLateMinutes = 0,
				Logs.AttendanceLog.ActualWorkMinutes = 0,
				Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
				Logs.AttendanceLog.UpdatedDate = GETDATE(),
				Logs.AttendanceLog.IsProcessCompleted = 1                     
			WHERE Logs.AttendanceLog.LogId = @LogId
		 
			SET @IsProcessed = 1	
			
			RETURN		     
		END
		  
        IF @ActionId = 3 OR @IsExempt = 1
		BEGIN
			UPDATE Logs.AttendanceLog
			   SET Logs.AttendanceLog.StatusId = 123, -- معفي
				   Logs.AttendanceLog.LateMinutes = 0,
				   Logs.AttendanceLog.InLateMinutes = 0,
				   Logs.AttendanceLog.OutLateMinutes = 0,
				   Logs.AttendanceLog.ActualWorkMinutes = 0,
				   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
				   Logs.AttendanceLog.UpdatedDate = GETDATE(),
				   Logs.AttendanceLog.IsProcessCompleted = 1                     
			 WHERE Logs.AttendanceLog.LogId = @LogId

			SET @IsProcessed = 1

			RETURN	          
		END
    /************************************************* OLD Exempt ********************************
	  IF @IsProcessed = 0
		  BEGIN
				IF @ActionId = 3 OR @IsExempt = 1
				BEGIN
					IF @AttendanceDate = @TodayDate
					BEGIN
					  IF @CurTime >= @WorkStartTime AND @CurTime < @WorkEndTime
					  BEGIN
						UPDATE Logs.AttendanceLog
						   SET Logs.AttendanceLog.StatusId = 123, -- على رأس العمل- معفي
							   Logs.AttendanceLog.LateMinutes = 0,
							   Logs.AttendanceLog.InLateMinutes = 0,
							   Logs.AttendanceLog.OutLateMinutes = 0,
							   Logs.AttendanceLog.InExtraMinutes  = 0,				   
							   Logs.AttendanceLog.OutExtraMinutes  = 0, 
							   Logs.AttendanceLog.ExtraTimeMinutes  = 0,			       			   
							    Logs.AttendanceLog.InTime = NULL,-- @WorkStartTime,
							   Logs.AttendanceLog.OutTime = NULL , -- @WorkEndTime,
							   Logs.AttendanceLog.ActualWorkMinutes =  0,--DATEDIFF(mi, @WorkStartTime, @WorkEndTime),				   
							   --Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),  
							   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
						 WHERE Logs.AttendanceLog.LogId = @LogId
						 SET @IsProcessed = 1	          
					  END
					  ELSE
					  IF @CurTime >= @WorkEndTime
					  BEGIN
						UPDATE Logs.AttendanceLog
						   SET Logs.AttendanceLog.StatusId = 123, -- حاضر- معفي
							   Logs.AttendanceLog.LateMinutes = 0,
							   Logs.AttendanceLog.InLateMinutes = 0,
							   Logs.AttendanceLog.OutLateMinutes = 0,
							   Logs.AttendanceLog.InExtraMinutes  = 0,				   
							   Logs.AttendanceLog.OutExtraMinutes  = 0, 
							   Logs.AttendanceLog.ExtraTimeMinutes  = 0,			       			   
							    Logs.AttendanceLog.InTime = NULL,-- @WorkStartTime,
							   Logs.AttendanceLog.OutTime = NULL, -- @WorkEndTime,
							   Logs.AttendanceLog.ActualWorkMinutes =  0,--DATEDIFF(mi, @WorkStartTime, @WorkEndTime),
							   --Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime), 
							   Logs.AttendanceLog.IsProcessCompleted = 1,  
							   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
						 WHERE Logs.AttendanceLog.LogId = @LogId
						 SET @IsProcessed = 1	          
					  END
					END
					ELSE
					BEGIN
						UPDATE Logs.AttendanceLog
						   SET Logs.AttendanceLog.StatusId = 123, -- حاضر- معفي
							   Logs.AttendanceLog.LateMinutes = 0,
							   Logs.AttendanceLog.InLateMinutes = 0,
							   Logs.AttendanceLog.OutLateMinutes = 0,
							   Logs.AttendanceLog.InExtraMinutes  = 0,				   
							   Logs.AttendanceLog.OutExtraMinutes  = 0, 
							   Logs.AttendanceLog.ExtraTimeMinutes  = 0,			       			   
							   Logs.AttendanceLog.InTime = NULL,-- @WorkStartTime,
							   Logs.AttendanceLog.OutTime = NULL , -- @WorkEndTime,
							   Logs.AttendanceLog.ActualWorkMinutes =  0,--DATEDIFF(mi, @WorkStartTime, @WorkEndTime),
							   --Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime), 
							   Logs.AttendanceLog.IsProcessCompleted = 1,  
							   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
						 WHERE Logs.AttendanceLog.LogId = @LogId
						 SET @IsProcessed = 1	        
					END	    
				END
	        END
	  ***************************************************************************************************************************/	  	  
	  IF @IsProcessed = 0
	     BEGIN
		  IF (@IsEarlyCheckRequired = 1 AND (@ActionId < 3 OR @IsExempt = 0) AND @InTime IS NOT NULL AND @InTime < @EarlyInTime)
		  BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 80, -- توقيع قبل وقت الدوام
					   --Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   --Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.ActualWorkMinutes = 0,
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1
		  END

		  ELSE IF (@IsEarlyCheckRequired = 1 AND (@ActionId < 3 OR @IsExempt = 0) AND @OutTime IS NOT NULL AND @OutTime > @EarlyOutTime)
			BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 85, -- توقيع بعد وقت الدوام
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.ActualWorkMinutes = 0,
					   --Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1	    
			END
			
		   ELSE IF (@IsEarlyCheckRequired = 1 AND (@ActionId < 3 OR @IsExempt = 0) AND @OutTime IS NOT NULL AND @InTime IS NOT NULL AND @OutTime > @EarlyOutTime AND @InTime > @EarlyInTime )
			BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 90, -- توقيع دخول وخروج في غير الوقت المحدد
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.ActualWorkMinutes =0,
					   --Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1				
			END
			--ELSE
			--BEGIN
			--	UPDATE Logs.AttendanceLog
			--	   SET Logs.AttendanceLog.StatusId = 85, -- توقيع بعد وقت الدوام
			--		   --Logs.AttendanceLog.LateMinutes = 0,
			--		   --Logs.AttendanceLog.InLateMinutes = 0,
			--		   Logs.AttendanceLog.OutLateMinutes = 0,
			--		   Logs.AttendanceLog.ActualWorkMinutes =  0,
			--		   --Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
			--		   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
			--	 WHERE Logs.AttendanceLog.LogId = @LogId
			--	 SET @IsProcessed = 1	    
			--END		  
	  
	  END



      IF @IsProcessed = 0
	  BEGIN
        /*****************************************************
         Update In Status
        *****************************************************/	  
	    EXECUTE Logs.SpUpdateInTimeStatus @LogId,
										  @AttendanceDate,
										  @EmployeeId,
										  @InTime,
										  @OutTime,
										  @WorkStartTime,
										  @WorkEndTime,
										  @WorkingMinuts,
										  @WBegTime,
										  @WEndTime,
										  @IsAllowOvertime

        /*****************************************************
         Update Out Status
        *****************************************************/


		IF (@CurTime>@WorkEndTime AND @AttendanceDate=@TodayDate) OR @TodayDate>@AttendanceDate  
	    EXECUTE Logs.SpUpdateOutTimeStatusFlex @LogId,
										   @AttendanceDate,
										   @EmployeeId,
										   @InTime,
										   @OutTime,											 
										   @WorkStartTime,
										   @WorkEndTime,
										   @WBegTime,
										   @WEndTime,
										   @MarkObsentDuration,
										   @IsAllowOvertime											 
	  END 
	  
	  -----------------------Calculate Actual Work Minutes-------------------------
	  
	  --if (SUBSTRING(CONVERT(VARCHAR(20),getdate(), 113), 13, 5)<SUBSTRING(CONVERT(VARCHAR(20),@WorkEndTime, 113), 13, 5)) and( convert(date,CONVERT(varchar(10),getdate(),103),103)=@AttendanceDate)
   --    BEGIN
   --    SET @OutMinutes=0
	  -- END
	   
	 
	  
	  --UPDATE Logs.AttendanceLog 
	  --SET OutMinutes =@OutMinutes
	  --WHERE LogId=@LogId
   -- 	 AND StatusId not IN (10,15,20,25,30,35,40,50,60,100)
    	
	  -----------------------------------------------------------------------------		
      
	  --UPDATE Logs.AttendanceLog
   --   SET StatusId=45
   --   WHERE LogId =@LogId
   --   AND InLateMinutes>0 
   --   AND OutLateMinutes =0
      
	  --UPDATE Logs.AttendanceLog
   --   SET StatusId=110
   --   WHERE LogId =@LogId
   --   AND InLateMinutes=0 
   --   AND OutLateMinutes >0
      
   --   ---------------------------------If Came Late And Leave Early-----------------
   --   UPDATE Logs.AttendanceLog
   --   SET StatusId=120
   --   WHERE LogId =@LogId
   --   AND InLateMinutes>0 
   --   AND OutLateMinutes >0
      

      -------------------------------------------------------------------------------


	   -----------------------Calculate Extra Work Minutes-------------------------
	   SELECT @ExtraMinuts= logs.AttendanceLog.ActualWorkMinutes- logs.AttendanceLog.WorkingMinutes
	   FROM logs.AttendanceLog
	   WHERE LogId=@LogId

	   IF @ExtraMinuts>0
	   BEGIN
	  UPDATE Logs.AttendanceLog 
	  SET ExtraTimeMinutes =@ExtraMinuts
	  WHERE LogId=@LogId
      AND StatusId NOT IN (10,15,20,25,30,35,40,50,60,100)
	  END
    	
	  -----------------------------------------------------------------------------		

      RETURN     
    END TRY
    BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN    
    END CATCH
END







GO
