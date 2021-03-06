USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpUpdateInTimeStatus]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 23/09/2008
-- Description:	Update In Time Status
-- =============================================
CREATE PROCEDURE [Logs].[SpUpdateInTimeStatus]
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
				   Logs.AttendanceLog.InExtraMinutes  = @InExtraMinutes,				   
				   Logs.AttendanceLog.OutExtraMinutes  = 0, 
				   Logs.AttendanceLog.ExtraTimeMinutes  = @InExtraMinutes,
				   Logs.AttendanceLog.InLateMinutes = 0, 
				   Logs.AttendanceLog.OutLateMinutes = 0, 				   				   				   
				   Logs.AttendanceLog.LateMinutes = 0, 				   
				   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
				   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
				   Logs.AttendanceLog.UpdateVersion = 1                           
			 WHERE Logs.AttendanceLog.LogId = @LogId
		 END  --End (@InTime <= @WBegTime) 
		 ELSE -- Came Late
		 IF (@InTime > @WBegTime)		 
		 BEGIN 
		/*****************************************************
			Check for  Gatepass
		*****************************************************/             
            SELECT @ExceptionStatusId = GatepassTypeId,
			       @ExBegTime = GatepassBegTime,
				   @ExEndTime = GatepassEndTime 
			FROM Managements.Gatepass
			WHERE EmployeeId = @EmployeeId AND GatepassBegDate = @AttendanceDate AND IsApproved =1 
			AND GatepassBegTime >=@WorkBegTime AND GatepassBegTime <=@WBegTime 
 
            IF @ExceptionStatusId IS NOT NULL --There is IN Gatepass
			BEGIN        
				IF @InTime <= @ExEndTime --Logged In Before Gatepass Ends
				BEGIN
					UPDATE Logs.AttendanceLog
						SET Logs.AttendanceLog.StatusId = @ExceptionStatusId,
							Logs.AttendanceLog.InExtraMinutes  = 0,
							Logs.AttendanceLog.OutExtraMinutes  = 0, 
							Logs.AttendanceLog.ExtraTimeMinutes  = 0,
							Logs.AttendanceLog.InLateMinutes = 0,
							Logs.AttendanceLog.OutLateMinutes = 0, 
							Logs.AttendanceLog.LateMinutes = 0, 							   							    
							Logs.AttendanceLog.UpdatedUserAccountId  = 1,
							Logs.AttendanceLog.UpdatedDate  = GETDATE(),
							Logs.AttendanceLog.UpdateVersion = 1                           
						WHERE Logs.AttendanceLog.LogId = @LogId			        
				END
				ELSE
				IF @InTime > @ExEndTime --Logged In After Gatepass Ends
				BEGIN
					SET @InLateTime = DATEDIFF(mi, @ExEndTime, @InTime) 
					UPDATE Logs.AttendanceLog
						SET Logs.AttendanceLog.StatusId = 45, -- متأخر 
							Logs.AttendanceLog.InLateMinutes  = @InLateTime,
							Logs.AttendanceLog.OutLateMinutes = 0,
							Logs.AttendanceLog.LateMinutes = @InLateTime, 
							Logs.AttendanceLog.InExtraMinutes  = 0,
							Logs.AttendanceLog.OutExtraMinutes  = 0,
							Logs.AttendanceLog.ExtraTimeMinutes  = 0, 							   
							Logs.AttendanceLog.UpdatedUserAccountId  = 1,
							Logs.AttendanceLog.UpdatedDate  = GETDATE(),
							Logs.AttendanceLog.UpdateVersion = 1                           
						WHERE Logs.AttendanceLog.LogId = @LogId			        
				END
			END --   End There is IN Gatepass
			ELSE -- No IN Gatepass
			BEGIN
			 SET @InLateTime = DATEDIFF(mi, @WorkBegTime, @InTime) 
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 45, -- متأخر 					   
					   Logs.AttendanceLog.InLateMinutes = @InLateTime,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.LateMinutes =  @InLateTime,
					   Logs.AttendanceLog.InExtraMinutes = 0,
					   Logs.AttendanceLog.ExtraTimeMinutes = 0,
					   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
					   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
					   Logs.AttendanceLog.UpdateVersion = 1                        
				 WHERE Logs.AttendanceLog.LogId = @LogId
			END --End No IN Gatepass
			    			          
 
		 END   	--End Came Late	
		END  --End @InTime IS NOT NULL	
		ELSE
		/*****************************************************
		  InTime is not exists
		 *****************************************************/ 		
		IF @InTime IS NULL	 
		BEGIN
			  /**********************************************************
			   Check  Gatepass 
			  ***********************************************************/             
			 SELECT @ExceptionStatusId = GatepassTypeId 
			 FROM Managements.Gatepass
			 WHERE EmployeeId = @EmployeeId AND GatepassBegDate = @AttendanceDate AND IsApproved =1 
			 AND GatepassBegTime >=@WorkBegTime AND GatepassBegTime <=@WBegTime 

			 IF @ExceptionStatusId IS NOT NULL
				  UPDATE Logs.AttendanceLog
					 SET Logs.AttendanceLog.StatusId = @ExceptionStatusId, 
						 Logs.AttendanceLog.LateMinutes = 0,
						 Logs.AttendanceLog.InLateMinutes = 0,
						 Logs.AttendanceLog.OutLateMinutes = 0,
						 Logs.AttendanceLog.InExtraMinutes = 0,
						 Logs.AttendanceLog.OutExtraMinutes = 0, 
						 Logs.AttendanceLog.ExtraTimeMinutes = 0,
						 Logs.AttendanceLog.ActualWorkMinutes = 0,
						 Logs.AttendanceLog.OvertimeMinutes = 0,   
						 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
						 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
						 Logs.AttendanceLog.UpdateVersion = 1,
						 Logs.AttendanceLog.IsProcessCompleted = 1                  
				   WHERE Logs.AttendanceLog.LogId = @LogId				      
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
			   
			  /**********************************************************
			  End Check  Gatepass 
			  ***********************************************************/         
			  			      			  
		    	  		  
		END -- END @InTime IS NULL
      
	  RETURN    
    END TRY
    BEGIN CATCH
      INSERT INTO  [dbo].[CodeErrors]
           ([ErrorText]) VALUES ('InTimeUpdate ' + CAST(@LogId as nvarchar(100)))    
      EXECUTE [Common].[SpCommon_LogError];
      RETURN      
    END CATCH
END





GO
