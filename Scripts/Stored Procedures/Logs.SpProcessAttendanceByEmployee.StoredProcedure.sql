USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpProcessAttendanceByEmployee]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 18-09-2008
-- Description:	Process Employees Attendance
-- =============================================
CREATE PROCEDURE [Logs].[SpProcessAttendanceByEmployee] 
(
 @AttendanceDate [date] = NULL,
 @EmployeeID     [int] = NULL 
)
AS
BEGIN
   SET NOCOUNT ON;

   /*****************************************************
    Local Variables Declaration
   *****************************************************/     
   DECLARE @IdentityId int = NULL
     
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

   DECLARE @IsPeriodic Bit=0
   DECLARE @ShiftsCount INT=1 
   DECLARE @ShiftNo INT = 1 
   DECLARE @HireDate  DATE
   /*****************************************************
    Cursor Variables
   *****************************************************/     

   DECLARE @ActionId int
   DECLARE @IsExempt int

   BEGIN TRY      
   
   IF EXISTS (SELECT EmployeeId, 
            ActionId
       FROM Employees.Employee
	   WHERE  (IsDeleted=1   or IsActive=0 or HireDate>@AttendanceDate) And EmployeeId=@EmployeeID)
      
	   RETURN

   --SELECT @HireDate=HiredDate FROM AccountEmployee WHERE AccountEmployeeId=@EmployeeID 
   --IF @HireDate>@AttendanceDate 
   --RETURN


     SELECT @ActionId= ActionId,@IsExempt = IsExempt FROM Employees.Employee  WHERE EmployeeId=@EmployeeID
	 
	 /*****************************************************
		Get Work Schedule 
	  *****************************************************/         
     EXECUTE Logs.SpGetEmployeeWorkSchedule @AttendanceDate, 
                                            @EmployeeId, 
                                            @WorkScheduleId OUTPUT,
											@PolicyId  OUTPUT,
											@LateInMinutes OUTPUT,
											@LateOutMinutes OUTPUT,
											@MarkObsentDuration OUTPUT,
											@EarlyInMinutes OUTPUT,
											@EarlyOutMinutes OUTPUT ,
										    @IsPeriodic OUTPUT,
											@ShiftsCount	OUTPUT
											
	WHILE @ShiftNo <= @ShiftsCount
	BEGIN												
																							                                                 
	 /*****************************************************
		Check Day Of week
	  *****************************************************/                                                                         
			 EXECUTE Logs.SpGetWorkScheduleDay @AttendanceDate,
											   @WorkScheduleId, 
											   @ShiftNo,
											   @IsPeriodic,
											   @IsWorkingDay OUTPUT,
											   @WorkBegTime OUTPUT,
											   @WorkEndTime OUTPUT,
											   @WorkingMinutes OUTPUT 
									               
       IF NOT EXISTS(SELECT EmployeeId 
                       FROM Logs.AttendanceLog
                      WHERE LogDate    = @AttendanceDate
                        AND EmployeeId = @EmployeeId AND ShiftNo=@ShiftNo)
                        
	   /*****************************************************
		 New Attendance log Employee
	   *****************************************************/                         
       BEGIN 
         IF @IsWorkingDay = 1
         BEGIN
           INSERT INTO [Logs].[AttendanceLog]
                 ([LogDate],
                  [EmployeeId],
                  [WorkScheduleId],
				  [ShiftNo],
                  [WorkStartTime],
                  [WorkEndTime],
                  [PolicyId],
                  [WorkingMinutes],
                  [LateInMinutes],
                  [LateOutMinutes],
                  [MarkObsentDuration],
                  [ActionId],
				  [IsExempt],
                  [EarlyInMinutes],
                  [EarlyOutMinutes]
                  )
           VALUES (@AttendanceDate,
                   @EmployeeId,
                   @WorkScheduleId,
				   @ShiftNo,
                   @WorkBegTime,
                   @WorkEndTime,
                   @PolicyId,
                   @WorkingMinutes,
                   @LateInMinutes,
                   @LateOutMinutes,
                   @MarkObsentDuration, 
                   @ActionId,
				   @IsExempt,
                   @EarlyInMinutes, 
                   @EarlyOutMinutes)                   
           
           SET @IdentityId = SCOPE_IDENTITY()
           
           IF @@ERROR = 0
           BEGIN
             IF @ActionId = 3 OR @IsExempt =1
             BEGIN
               EXECUTE Logs.SpUpdateAttendanceStatusflex @AttendanceDate, @IdentityId 
             END
             ELSE
             BEGIN
              IF @IsPeriodic=0  
			   BEGIN  
				  EXECUTE Logs.SpUpdateTimesFromMachineLog @IdentityId, @AttendanceDate,@ShiftNo, @EmployeeId
				  EXECUTE Logs.SpUpdateAttendanceStatusFlex @AttendanceDate, @IdentityId  
               END
               ELSE --Shifts
			   BEGIN
			      EXECUTE LogsShifts.SpUpdateTimesFromMachineLogForShifts @IdentityId, @AttendanceDate, @EmployeeId
				  EXECUTE LogsShifts.SpUpdateAttendanceStatus @AttendanceDate, @IdentityId  
			    END           
             END          
           END
         END										    										                                         
       END
       ELSE
       IF EXISTS(SELECT EmployeeId 
                   FROM Logs.AttendanceLog
                  WHERE LogDate    = @AttendanceDate
                    AND EmployeeId = @EmployeeId)
	   /*****************************************************
	     Employee already added to Attendance log
	   *****************************************************/                     
       BEGIN
         SET @IdentityId = (SELECT LogId 
                              FROM Logs.AttendanceLog 
                             WHERE LogDate = @AttendanceDate
                               AND EmployeeId = @EmployeeId AND ShiftNo=@ShiftNo)
                               
         IF @ActionId = 3 OR @IsExempt =1
         BEGIN
           EXECUTE Logs.SpUpdateAttendanceStatusflex @AttendanceDate, @IdentityId 
         END
         ELSE
         BEGIN 
			EXECUTE Logs.SpUpdateTimesFromMachineLog @IdentityId, @AttendanceDate,@ShiftNo, @EmployeeId
			EXECUTE Logs.SpUpdateAttendanceStatusFlex @AttendanceDate, @IdentityId  
         END 
       END
	   SET @ShiftNo = @ShiftNo + 1
	   END
   
       
    

     RETURN
   END TRY
   BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN
   END CATCH
END








GO
