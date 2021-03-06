USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpGetEmployeeWorkSchedule]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 24 3 2011
-- Description:	Get Employee Work schedule Id
-- =============================================
CREATE PROCEDURE [Logs].[SpGetEmployeeWorkSchedule]
(
 @AttendanceDate     date = NULL,
 @EmployeeId         int = NULL,
 @WorkScheduleId     int = -1 OUTPUT,
 @PolicyId           int = NULL OUTPUT,
 @LateInMinutes      int = NULL OUTPUT,
 @LateOutMinutes     int = NULL OUTPUT,
 @MarkObsentDuration int = NULL OUTPUT,
 @EarlyInMinutes     int = NULL OUTPUT,
 @EarlyOutMinutes    int = NULL OUTPUT,
 @IsShiftWorkSchedule Bit=NULL OUTPUT,
 @ShiftsCount        int=NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    BEGIN TRY
         -- Initialize output parameters
		SET @PolicyId              = NULL 
		SET @LateInMinutes         = 0 
		SET @LateOutMinutes        = 0 
		SET @MarkObsentDuration    = 0 
	    
        -- When Employee in Work Schedule
		SELECT @WorkScheduleId = WorkSchedule.WorkScheduleId,
		       @PolicyId = WorkSchedule.PolicyId ,
			   @IsShiftWorkSchedule=IsPeriodic,
			   @ShiftsCount = ShiftsCount
		FROM Managements.WorkSchedule 
			 INNER JOIN Employees.Employee
		  ON Managements.WorkSchedule.WorkScheduleId = Employees.Employee.WorkScheduleId
		WHERE @AttendanceDate >= Managements.WorkSchedule.ScheduleBegDate
		 AND @AttendanceDate <= Managements.WorkSchedule.ScheduleEndDate
		 AND Employees.Employee.EmployeeId   = @EmployeeId
		
		
		IF @WorkScheduleId IS NULL
		BEGIN
		  SET @WorkScheduleId = -1		  
		END
		ELSE
		BEGIN
		    -- Get WorkSchedule Policy Setting if exists                                              
			IF @PolicyId IS NOT NULL
			BEGIN
			  SELECT @LateInMinutes = LateInMinutes,
					 @LateOutMinutes = LateOutMinutes,
					 @MarkObsentDuration = MarkObsentDuration,
					 @EarlyInMinutes = EarlyInMinutes,
					 @EarlyOutMinutes = EarlyOutMinutes 					  
				FROM Managements.Policy 
			   WHERE PolicyId = @PolicyId        
			END
			ELSE
			BEGIN
			  SET @LateInMinutes = 0
			  SET @LateOutMinutes = 0
			  SET @MarkObsentDuration = 0
			  SET @EarlyInMinutes = 0
			  SET @EarlyOutMinutes = 0		      
			END			 		
		END
				
		RETURN 
    END TRY
    BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN    
    END CATCH
END









GO
