USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpUpdateInTimeStatusFlex]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 23/09/2008
-- Description:	Update In Time Status
-- =============================================
CREATE PROCEDURE [Logs].[SpUpdateInTimeStatusFlex]
(
 @LogId          int = NULL,
 @AttendanceDate date = NULL,
 @EmployeeId     int = NULL,
 @InTime         datetime = NULL,
 @OutTime        datetime = NULL, 
 @WorkBegTime    datetime = NULL,
 @WorkEndTime    datetime = NULL,
 @WorkingMinutes INT=480,
 @WBegTime       datetime = NULL,
 @WEndTime       datetime = NULL ,
 @IsAllowOvertime bit = 1
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

    -- Declariations
    DECLARE @RowEffected int
    DECLARE @CurTime datetime
    DECLARE @ExtraTimeMinutes int = 0
    DECLARE @IsWorkExceptionFound bit = 0
    DECLARE @WorkExceptionTypeName nvarchar (50) = ''
    DECLARE @StatusId int
    DECLARE @TodayDate date = GETDATE() 
    DECLARE @ExceptionStatusId int
    DECLARE @ExBegTime       datetime = NULL
    DECLARE @ExEndTime       datetime = NULL
    DECLARE @IsOnVacation    bit = 0
    DECLARE @IsOutOfRange bit = 0
    DECLARE @InExtraMinutes int = 0
    DECLARE @InLateTime int = 0 
  
    -- Set Current Time Value
   SET @CurTime = SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),18 ,2)
   --SET @WorkingMinutes=DATEDIFF(mi,@WorkBegTime,@WorkEndTime)

    BEGIN TRY  
		IF @InTime IS NOT NULL
		BEGIN
		 IF (@InTime <= @WBegTime) 
		/*****************************************************
		  Coming before the Work Beginning time Or On Time
		 *****************************************************/		 
		 BEGIN
		    IF (@InTime <= @WorkBegTime)  
		    BEGIN
		      SET @InExtraMinutes  = DATEDIFF(mi, @InTime, @WorkBegTime)
		    END
		    
			UPDATE Logs.AttendanceLog
			   SET Logs.AttendanceLog.StatusId = 15, -- على رأس العمل
				   Logs.AttendanceLog.WorkStartTime=@InTime,
				--   Logs.AttendanceLog.WorkEndTime=DATEADD(mi,@WorkingMinutes,@InTime),
				   Logs.AttendanceLog.InExtraMinutes  = @InExtraMinutes,				   
				   Logs.AttendanceLog.ExtraTimeMinutes  = @InExtraMinutes,
				   Logs.AttendanceLog.InLateMinutes = 0, 				   				   				   
				   Logs.AttendanceLog.LateMinutes = 0, 				   
				   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
				   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
				   Logs.AttendanceLog.UpdateVersion = 1                           
			 WHERE Logs.AttendanceLog.LogId = @LogId
		 END  
		 ELSE IF (@InTime > @WBegTime)
		   BEGIN
		   
		     
		 SELECT @ExceptionStatusId = GatepassTypeId,
				@ExBegTime = GatepassBegTime,
				@ExEndTime =  GatepassEndTime
		   FROM Managements.Gatepass
		   WHERE EmployeeId =  @EmployeeId
		   AND GatepassBegDate = @AttendanceDate
		   AND GatepassBegTime = @WorkBegTime
		   AND IsApproved = 1   
           
		   IF  @ExceptionStatusId IS NOT NULL 
		   BEGIN

		     IF @InTime <= @ExEndTime -- OK Came before gatepass ends 
				  UPDATE Logs.AttendanceLog
					SET Logs.AttendanceLog.StatusId = @ExceptionStatusId,
						--Logs.AttendanceLog.WorkEndTime=DATEADD(mi,@WorkingMinutes,@ExBegTime),
						Logs.AttendanceLog.InExtraMinutes  = 0,
						Logs.AttendanceLog.ExtraTimeMinutes  = 0,
						Logs.AttendanceLog.InLateMinutes = 0,
						Logs.AttendanceLog.LateMinutes = 0, 							   							    
						Logs.AttendanceLog.UpdatedUserAccountId  = 1,
						Logs.AttendanceLog.UpdatedDate  = GETDATE(),
						Logs.AttendanceLog.UpdateVersion = 1                           
					WHERE Logs.AttendanceLog.LogId = @LogId
             ELSE -- LATE Came after gatepass ends
			   BEGIN
					SET @InLateTime = DATEDIFF(mi, @ExEndTime, @InTime)  
					UPDATE Logs.AttendanceLog     --Here Is proplem because the employee login out of  Gatepass Range
						SET Logs.AttendanceLog.StatusId = 45, -- متأخر 
							Logs.AttendanceLog.InLateMinutes  = @InLateTime,
							--Logs.AttendanceLog.WorkEndTime=DATEADD(mi,@WorkingMinutes,@ExBegTime),
							Logs.AttendanceLog.LateMinutes = @InLateTime, 
							Logs.AttendanceLog.InExtraMinutes  = 0,
							Logs.AttendanceLog.ExtraTimeMinutes  = 0, 							   
							Logs.AttendanceLog.UpdatedUserAccountId  = 1,
							Logs.AttendanceLog.UpdatedDate  = GETDATE(),
							Logs.AttendanceLog.UpdateVersion = 1                           
						WHERE Logs.AttendanceLog.LogId = @LogId			        
				END
		   END
		   ELSE -- NO Gatepass
		   BEGIN 
			    SET @InLateTime = DATEDIFF(mi, @WorkBegTime, @InTime) 
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 45, -- متأخر 					   
					   Logs.AttendanceLog.InLateMinutes = @InLateTime,
					   --Logs.AttendanceLog.WorkEndTime=DATEADD(mi,@WorkingMinutes,@InTime),
					   Logs.AttendanceLog.LateMinutes =  @InLateTime,
					   Logs.AttendanceLog.InExtraMinutes = 0,
					   Logs.AttendanceLog.ExtraTimeMinutes = 0,
					   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
					   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
					   Logs.AttendanceLog.UpdateVersion = 1                        
				 WHERE Logs.AttendanceLog.LogId = @LogId
			END  
	    END   		
		 END
		ELSE IF @InTime IS NULL --No InTime Log Found
		BEGIN
		  IF @AttendanceDate < @TodayDate OR (@AttendanceDate = @TodayDate AND @CurTime >=@WorkBegTime )--The processing for Past Day
		    BEGIN
			  /**********************************************************
			   Check for Gatepass
			  ***********************************************************/ 
			  SELECT @ExceptionStatusId = GatepassTypeId,
				@ExBegTime = GatepassBegTime,
				@ExEndTime =  GatepassEndTime
			   FROM Managements.Gatepass
			   WHERE EmployeeId =  @EmployeeId
				 AND GatepassBegDate = @AttendanceDate
				 AND GatepassBegTime = @WorkBegTime
			     AND IsApproved = 1             
			  IF @ExceptionStatusId IS NOT NULL
				BEGIN
						UPDATE Logs.AttendanceLog
							SET Logs.AttendanceLog.StatusId = @ExceptionStatusId,
								Logs.AttendanceLog.InExtraMinutes  = 0,
								--Logs.AttendanceLog.WorkEndTime=DATEADD(mi,@WorkingMinutes,@ExBegTime),
								Logs.AttendanceLog.ExtraTimeMinutes  = 0,
								Logs.AttendanceLog.InLateMinutes = 0,
								Logs.AttendanceLog.LateMinutes = 0, 							   							    
								Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								Logs.AttendanceLog.UpdateVersion = 1                           
							WHERE Logs.AttendanceLog.LogId = @LogId			   
				       END
					   ELSE
					   UPDATE Logs.AttendanceLog
						 SET Logs.AttendanceLog.StatusId = 70, -- لم يوقع دخول 
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

               END 
			     
		  ELSE IF @AttendanceDate>@TodayDate  --The processing for Future
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
			  END

      END
	  RETURN    
    END TRY
    BEGIN CATCH
      INSERT INTO Common.DBErrorsLog
           (ErrorMessage) VALUES ('InTimeUpdate ' + CAST(@LogId as nvarchar(100)))    
      EXECUTE [Common].[SpCommon_LogError];
      RETURN      
    END CATCH
END









GO
