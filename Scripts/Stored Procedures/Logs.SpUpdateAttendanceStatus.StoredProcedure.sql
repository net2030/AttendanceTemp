USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpUpdateAttendanceStatus]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Update Attendance Status
-- =============================================
CREATE PROCEDURE [Logs].[SpUpdateAttendanceStatus]
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
	DECLARE @LateInMinutes      int = 0
	DECLARE @LateOutMinutes     int = 0
	DECLARE @MarkObsentDuration int = 0
	DECLARE @StatusId           int = NULL
	DECLARE @ActionId           int = NULL
    DECLARE @EarlyInMinutes     int = 0
    DECLARE @EarlyOutMinutes    int = 0
	
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
    
    SET @CurTime = SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),18 ,2)
    
    /*****************************************************
     Set local Variable values
    *****************************************************/    
    SELECT @EmployeeId = EmployeeId,
           @LogDate=Logdate,
           @InTime = InTime,
           @OutTime = OutTime,
           @WorkStartTime = WorkStartTime,
           @WorkEndTime = WorkEndTime,
		   @WorkingMinuts=WorkingMinutes,
           @LateInMinutes = LateInMinutes,
           @LateOutMinutes = LateOutMinutes,
           @MarkObsentDuration = MarkObsentDuration,
           @StatusId = StatusId,
           @ActionId = ActionId,
           @EarlyInMinutes = EarlyInMinutes,
           @EarlyOutMinutes = EarlyOutMinutes           
      FROM Logs.AttendanceLog
     WHERE LogId = @LogId
    BEGIN TRY							    		
	  SET @WBegTime = @WorkStartTime
	  SET @WEndTime = @WorkEndTime

	  SET @EarlyInTime = @WorkStartTime
	  SET @EarlyOutTime = @WorkEndTime
	  
	   ---------------------------------------------Calculate The Actual Time---------------------
/*
SELECT @CountIn=Count(*) from logs.MachineLog 
WHERE Employeeid=@EmployeeId 
AND LogDate=@LogDate
AND LogTypeId=1 

SELECT @CountOut=Count(*) from logs.MachineLog 
WHERE Employeeid=@EmployeeId 
AND LogDate=@LogDate
AND LogTypeId=2 

--Print @countin
--Print @countOut

IF (@CountIn=@CountOut) 

BEGIN

   ;  with cte_test as
(
select [LogTime],[EmployeeId],[LogTypeId],ROW_NUMBER() over (partition by [EmployeeId],[LogTypeId] order by [EmployeeId],[LogTypeId])
 as rn from Logs.MachineLog where LogDate=@LogDate and EmployeeId=@EmployeeId
 )

select @EmployeeId= a.[EmployeeId], @logdate=convert(char(10), a.[LogTime], 111),@ActualTime= convert(varchar(8),dateadd(s,
sum(datepart(hour, b.[LogTime]-a.[LogTime]) * 3600) + sum(datepart(minute, b.[LogTime]-a.[LogTime]) * 60) + sum(datepart(second, b.[LogTime]-a.[LogTime])),0),108)
 from cte_test a
inner join cte_test b on a.[EmployeeId]=b.[EmployeeId]    and a.rn=b.rn
and a.LogTypeId=1 and b.LogTypeId=2
group by a.[EmployeeId],convert(char(10), a.[LogTime], 111)




declare @hms varchar(8)
set @hms = @ActualTime
declare @hours int
declare @minutes decimal
set @hours = datepart(hour, @hms)
set @minutes = datepart(minute, @hms)

 

select @ActualMinuts= @minutes+@hours*60


SELECT @OutMinutes=@WorkingMinuts - (@ActualMinuts + DATEDIFF(mi, @WorkStartTime, @InTime)+DATEDIFF(mi, @OutTime, @WorkEndTime))

   IF @OutMinutes=1
   BEGIN 
     SET @OutMinutes=0
   END

END

IF @CountIn<>@CountOut OR @OutMinutes <0
BEGIN
SET @OutMinutes=9999
END
	*/
	 -------------------------------------------------------------------------------------------
    
	  
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
	  END   		

	  IF (@EarlyInMinutes IS NOT NULL AND @EarlyInMinutes > 0)
	  BEGIN
	    SET @EarlyInTime =  DATEADD(mi, -@EarlyInMinutes, @WorkStartTime)
	    SET @IsEarlyCheckRequired = 1
	  END
	  IF (@EarlyOutMinutes IS NOT NULL AND @EarlyOutMinutes > 0)
	  BEGIN
	    SET @EarlyOutTime  =  DATEADD(mi, @EarlyOutMinutes, @WorkEndTime)
	    SET @IsEarlyCheckRequired = 1
	  END  
	  
      /*****************************************************
       Check if employee on vacation
      *****************************************************/       
	  IF Logs.funIsOnVacation(@AttendanceDate, @EmployeeId) = 1 
	  BEGIN 
		UPDATE Logs.AttendanceLog
		   SET Logs.AttendanceLog.StatusId = 60, -- إجازة
			   Logs.AttendanceLog.LateMinutes = 0,
			   Logs.AttendanceLog.InLateMinutes = 0,
			   Logs.AttendanceLog.OutLateMinutes = 0,
			   Logs.AttendanceLog.ActualWorkMinutes = 0,
			   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
			   Logs.AttendanceLog.UpdatedDate = GETDATE(),
			   Logs.AttendanceLog.IsProcessCompleted = 1                     
		 WHERE Logs.AttendanceLog.LogId = @LogId
		 
		 SET @IsProcessed = 1			     
	  END

	  IF @IsProcessed = 0
	  BEGIN
	    IF @ActionId = 3
	    BEGIN
	        IF @AttendanceDate = @TodayDate
	        BEGIN
	          IF @CurTime >= @WorkStartTime AND @CurTime < @WorkEndTime
	          BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 15, -- على رأس العمل
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.InExtraMinutes  = 0,				   
					   Logs.AttendanceLog.OutExtraMinutes  = 0, 
					   Logs.AttendanceLog.ExtraTimeMinutes  = 0,			       			   
					   Logs.AttendanceLog.InTime = @WorkStartTime,
					   Logs.AttendanceLog.OutTime = NULL,
					   Logs.AttendanceLog.ActualWorkMinutes =  0,					   
					  Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),  
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1	          
	          END
	          ELSE
	          IF @CurTime >= @WorkEndTime
	          BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 55, -- حاضر
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.InExtraMinutes  = 0,				   
					   Logs.AttendanceLog.OutExtraMinutes  = 0, 
					   Logs.AttendanceLog.ExtraTimeMinutes  = 0,			       			   
					   Logs.AttendanceLog.InTime = @WorkStartTime,
					   Logs.AttendanceLog.OutTime = @WorkEndTime,
					   Logs.AttendanceLog.ActualWorkMinutes = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),
					   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime), 
					   Logs.AttendanceLog.IsProcessCompleted = 1,  
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1	          
	          END
	        END
	        ELSE
	        BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 55, -- حاضر
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.InExtraMinutes  = 0,				   
					   Logs.AttendanceLog.OutExtraMinutes  = 0, 
					   Logs.AttendanceLog.ExtraTimeMinutes  = 0,			       			   
					   Logs.AttendanceLog.InTime = @WorkStartTime,
					   Logs.AttendanceLog.OutTime = @WorkEndTime,
					   Logs.AttendanceLog.ActualWorkMinutes =  DATEDIFF(mi, @WorkStartTime, @WorkEndTime),
					   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime), 
					   Logs.AttendanceLog.IsProcessCompleted = 1,  
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1	        
	        END	    
	    END
	  END
	  	  
	  IF @IsProcessed = 0
	  BEGIN
		  IF (@IsEarlyCheckRequired = 1 AND @ActionId < 3 AND @InTime IS NOT NULL AND @InTime < @EarlyInTime)
		  BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 80, -- توقيع قبل وقت الدوام
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.ActualWorkMinutes = 0,
					   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1
		  END

		  IF (@IsEarlyCheckRequired = 1 AND @ActionId < 3 AND @OutTime IS NOT NULL AND @OutTime > @EarlyOutTime)
		  BEGIN
			IF @StatusId = 80
			BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 90, -- توقيع دخول وخروج في غير الوقت المحدد
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.ActualWorkMinutes = 0,
					   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1	    
			END
			ELSE
			IF (@IsEarlyCheckRequired = 1 AND @ActionId < 3 AND @OutTime IS NOT NULL AND @InTime IS NOT NULL AND @OutTime > @EarlyOutTime AND @InTime > @EarlyOutTime )
			BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 90, -- توقيع دخول وخروج في غير الوقت المحدد
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.ActualWorkMinutes =0,
					   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1				
			END
			ELSE
			BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 85, -- توقيع بعد وقت الدوام
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.ActualWorkMinutes =  0,
					   Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),   
					   Logs.AttendanceLog.UpdatedDate = GETDATE()                     
				 WHERE Logs.AttendanceLog.LogId = @LogId
				 SET @IsProcessed = 1	    
			END		  
		  END	  
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
										  @WBegTime,
										  @WEndTime

        /*****************************************************
         Update Out Status
        *****************************************************/
	    EXECUTE Logs.SpUpdateOutTimeStatus @LogId,
										   @AttendanceDate,
										   @EmployeeId,
										   @InTime,
										   @OutTime,											 
										   @WorkStartTime,
										   @WorkEndTime,
										   @WBegTime,
										   @WEndTime,
										   @MarkObsentDuration											 
	  END 
	  
	  -----------------------Calculate Actual Work Minutes-------------------------
	  
	  if (SUBSTRING(CONVERT(VARCHAR(20),getdate(), 113), 13, 5)<SUBSTRING(CONVERT(VARCHAR(20),@WorkEndTime, 113), 13, 5)) and( convert(date,CONVERT(varchar(10),getdate(),103),103)=@AttendanceDate)
       BEGIN
       SET @OutMinutes=0
	   END
	   
	 
	  
	  UPDATE Logs.AttendanceLog 
	  SET OutMinutes =@OutMinutes
	  WHERE LogId=@LogId
    	 AND StatusId not IN (10,15,20,25,30,35,40,50,60,100)
    	
	  -----------------------------------------------------------------------------		
      
      
      ---------------------------------If Came Late And Leave Early-----------------
      UPDATE Logs.AttendanceLog
      SET StatusId=120
      WHERE LogId =@LogId
      AND InLateMinutes>0 
      AND OutLateMinutes >0
      
      
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
