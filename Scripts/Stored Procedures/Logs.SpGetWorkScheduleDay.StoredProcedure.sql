USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpGetWorkScheduleDay]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Check Work Schedule Day
-- =============================================
CREATE PROCEDURE [Logs].[SpGetWorkScheduleDay]
(
 @AttendanceDate date = NULL,
 @WorkScheduleId int = NULL,
 @ShiftNo        int = 1,
 @IsPeriodic     bit = 0,

 @IsWorkingDay   bit = 0 OUTPUT, 
 @WorkBegTime    datetime = NULL OUTPUT,
 @WorkEndTime    datetime = NULL OUTPUT,
 @WorkingMinutes int = NULL OUTPUT 
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

	DECLARE @TodayDayNo int

	--DECLARE @IsYesterdayOff Bit 
	--DECLARE @YesterdayDate DATE
	--DECLARE @YesterdayDayNo int

	

    -- set saturday as the first day of the week
    SET DATEFIRST 6

    -- Initialize output parameters
	SET @IsWorkingDay = 0 
	SET	@WorkBegTime  = NULL 
	SET @WorkEndTime  = NULL 
    SET @WorkingMinutes = 420
	--SET @YesterdayDate = DATEADD(DAY,-1,@AttendanceDate)
     
    BEGIN TRY     

	  IF  @IsPeriodic  = 1
	  BEGIN

		  DECLARE @PeriodFirstDate DATE 
		  DECLARE @PeriodLength INT

		  SELECT  @PeriodFirstDate = FirstPeriodStartDate,
				  @PeriodLength = PeriodLength
		  FROM Managements.WorkSchedule
		  WHERE WorkScheduleId =  @WorkScheduleId

		  SET @TodayDayNo =  (DATEDIFF(DAY,@PeriodFirstDate,@AttendanceDate) % @PeriodLength) + 1
		  --SET @YesterdayDayNo =  (DATEDIFF(DAY,@PeriodFirstDate,@YesterdayDate) % @PeriodLength) + 1

	  END
	  ELSE
	  BEGIN
		  SET @TodayDayNo =  DATEPART(dw, @AttendanceDate)
		  --SET @YesterdayDayNo =  DATEPART(dw, @YesterdayDate)
	  END


      IF @WorkScheduleId > -1
      -- when a valid WorkScheduleId is passed 
      BEGIN
        -- check if its a Weekend day 
		IF EXISTS(SELECT WeekDayNo 
			        FROM Managements.WorkScheduleDay 
			       WHERE WorkScheduleId = @WorkScheduleId 
			         AND WeekDayNo =  @TodayDayNo
			         AND IsWeekendDay = 1)   
        BEGIN
          -- set day as a weekend day 
		  SET @IsWorkingDay = 0    
        END
        ELSE
        BEGIN
          -- Set day as a working day 
          SET @IsWorkingDay = 1
        END 
      END
      ELSE
      BEGIN
        -- when there is no valid WorkScheduleId
        SELECT @IsWorkingDay =
          CASE  @TodayDayNo
               WHEN 1 THEN 0
               WHEN 2 THEN 1 
               WHEN 3 THEN 1
               WHEN 4 THEN 1
               WHEN 5 THEN 1                                             
               WHEN 6 THEN 1
               WHEN 7 THEN 0
         END                                    
      END

      IF (@IsWorkingDay = 1 AND @WorkScheduleId = -1)
      BEGIN
         -- if there is no work schedule and its a working day set default parameters value
		 SET @WorkBegTime = CONVERT(DATETIME, CONVERT(CHAR(10), @AttendanceDate, 112) + ' ' + CONVERT(CHAR(12),  CAST( '08:00 AM' AS time), 108))--'08:00 AM'
		 SET @WorkEndTime = CONVERT(DATETIME, CONVERT(CHAR(10), @AttendanceDate, 112) + ' ' + CONVERT(CHAR(12),  CAST( '05:00 PM' AS time), 108))--'05:00 PM'   
		 SET @WorkingMinutes = DATEDIFF(mi, @WorkBegTime, @WorkEndTime)              
      END
      ELSE
      IF (@IsWorkingDay = 1 AND @WorkScheduleId > -1)
      BEGIN 
        -- if there its a valid work schedule and its a working day set parameters value
		SELECT @WorkBegTime = CONVERT(DATETIME, CONVERT(CHAR(10), @AttendanceDate, 112) + ' ' + CONVERT(CHAR(12),  CAST( WorkBegTime AS time), 108)),
			   @WorkEndTime = CONVERT(DATETIME, CONVERT(CHAR(10), @AttendanceDate, 112) + ' ' + CONVERT(CHAR(12),  CAST( WorkEndTime AS time), 108)),
			   @WorkingMinutes = DATEDIFF(mi, WorkBegTime, WorkEndTime) 
		 FROM Managements.WorkScheduleDay 
		WHERE WorkScheduleId = @WorkScheduleId AND ShiftNo =@ShiftNo
		  AND WeekDayNo =  @TodayDayNo    
      END

      -- Check Official Holiday
	  IF EXISTS(SELECT HolidayId 
	              FROM Managements.Holiday 
	             WHERE @Attendancedate >= EffectiveDate
	               AND @Attendancedate <= DateExpire)
	  BEGIN
	    IF (@WorkScheduleId > -1)
	    -- if there its a valid work schedule
	    BEGIN
	         --Check if work Schedule override holiday
			IF EXISTS(SELECT WorkScheduleId 
						FROM Managements.WorkSchedule 
					   WHERE WorkScheduleId  = @WorkScheduleId 
						 AND IsEffectiveDuringHoliday = 1)
			BEGIN 
		     SET @IsWorkingDay = 1
			END
			ELSE
			BEGIN
			  SET @IsWorkingDay = 0			
			END	    
	    END
	  END
	  
	  --Make Work Begin Time Real  ---------------------------------------------------------------------- For Night Shifts

		IF @WorkBegTime>@WorkEndTime  --Shift Overlap Between Two Dates
		BEGIN

		
		--SELECT @IsYesterdayOff = IsWeekendDay
		--FROM Managements.WorkScheduleDay 
		--WHERE WorkScheduleId = @WorkScheduleId 
		--  AND ShiftNo =@ShiftNo
		--  AND WeekDayNo = @YesterdayDayNo

			--IF @IsYesterdayOff = 1 
			--BEGIN 
			--	SET @WorkBegTime=DATEADD(DAY,-1,@WorkBegTime)
			--	--SET @IsWorkingDay=1
			--END
			--ELSE 
			--BEGIN
			    SET @WorkBegTime=DATEADD(DAY,-1,@WorkBegTime)
				--SET @WorkEndTime=DATEADD(DAY,0,@WorkEndTime)
				
			--END
		END
        SET @WorkingMinutes = DATEDIFF(mi, @WorkBegTime, @WorkEndTime) 	  
	             
      RETURN
    END TRY
    BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN    
    END CATCH
END






GO
