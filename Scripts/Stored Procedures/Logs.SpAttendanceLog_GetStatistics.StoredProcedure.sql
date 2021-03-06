USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendanceLog_GetStatistics]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Attendance Statistics
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendanceLog_GetStatistics]
(
 @EmployeeId           int = NULL,
 @BegDate              date = NULL,
 @EndDate              date = NULL,
 
 @WorkingDays          decimal(10,2) = 0 OUTPUT, 
 @AbsentDays           decimal(10,2) = 0 OUTPUT,
 @LateDays             decimal(10,2) = 0 OUTPUT,
 @UncompleteDays       decimal(10,2) = 0 OUTPUT,
 @ExceptionDays        decimal(10,2) = 0 OUTPUT,
 @ActualWorkdays       decimal(10,2) = 0 OUTPUT, 
 @LateHours            decimal(10,2) = 0 OUTPUT,
 @InExteraHours        decimal(10,2) = 0 OUTPUT,
 @OutExteraHours       decimal(10,2) = 0 OUTPUT, 
 @ExteraHours          decimal(10,2) = 0 OUTPUT,
 @WorkingHours         decimal(10,2) = 0 OUTPUT, 
 @ActualWorkHours      decimal(10,2) = 0 OUTPUT,
 @GatepassCount        decimal(10,2) = 0 OUTPUT, 
 @DisciplinePercentage decimal(10,2) = 0 OUTPUT,
 @UmBegDate            nvarchar(10) OUTPUT,
 @UmEndDate            nvarchar(10) OUTPUT
 
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;       
          
    DECLARE @UserAccountId nvarchar(30) = ''
    --DECLARE @EmployeeId    int = 0
    DECLARE @TodayDate date = GETDATE()
   	DECLARE @chrValue as nvarchar(20)
   	DECLARE @intValue int = 0
   	  
	  
	  CREATE TABLE #Calendar( N INT NOT NULL IDENTITY(1,1)
                          ,Dt DATE NOT NULL PRIMARY KEY CLUSTERED)                            
    BEGIN TRY

	



		-- Get UmAlqura Start Date
		--SET @UmBegDate  = (SELECT UmDate
		--					 FROM [Common].[UmAlquraCalendar]
		--					WHERE GregorianDate = @BegDate)

		-- Get UmAlqura end Date
		--SET @UmEndDate  = (SELECT UmDate
		--					 FROM [Common].[UmAlquraCalendar]
		--					WHERE GregorianDate = @EndDate)

        /* 
          Days Calculation  
        */
        																                                                  
		-- Get Working days	
		SET @WorkingDays  = (SELECT COUNT(LogId) 
							   FROM Logs.AttendanceLog
							  WHERE LogDate >= @BegDate
							    AND LogDate <= @EndDate  
								AND EmployeeId = @EmployeeId)
								                    	                                         

        -- Get Absent Days
		SET @AbsentDays = (SELECT COUNT(LogId) 
								   FROM [Logs].[AttendanceLog]
								  WHERE StatusId = 75  -- ????
									AND LogDate >= @BegDate
							        AND LogDate <= @EndDate     
									AND EmployeeId = @EmployeeId) 			    
        IF @AbsentDays IS NULL
        BEGIN
          SET @AbsentDays = 0
        END
        
        -- Get Late Days
		SET @LateDays = (SELECT COUNT(LogId) 
						   FROM [Logs].[AttendanceLog]
						    WHERE StatusId in( 45,110,120) -- ?????  
							AND LogDate >= @BegDate
							AND LogDate <= @EndDate
							AND EmployeeId = @EmployeeId)
        IF @LateDays IS NULL
        BEGIN
          SET @LateDays = 0
        END

        -- Get Exeption days Days
		SET @ExceptionDays = (SELECT COUNT(LogId)
						        FROM [Logs].[AttendanceLog]
						       WHERE LogDate >= @BegDate
							     AND LogDate <= @EndDate
							     AND (StatusId IN  (25, 30, 35, 40, 60) OR StatusId > 2999)
							     AND EmployeeId = @EmployeeId)
        IF @ExceptionDays IS NULL
        BEGIN
          SET @ExceptionDays = 0
        END
        
        -- Get Uncomplete Days
		SET @UncompleteDays = (SELECT COUNT(LogId)
						        FROM [Logs].[AttendanceLog]
						       WHERE LogDate >= @BegDate
							     AND LogDate <= @EndDate
							     AND StatusId IN (10, 65, 70, 80, 85, 90)
							     AND EmployeeId = @EmployeeId)
        IF @UncompleteDays IS NULL
        BEGIN
          SET @UncompleteDays = 0
        END


        -- Get Actual Work Days
		SET @ActualWorkdays = (SELECT COUNT(LogId)
						        FROM [Logs].[AttendanceLog]
						       WHERE LogDate >= @BegDate
							     AND LogDate <= @EndDate
							     AND StatusId IN (15, 55,45,110,120) -- ??? ??? ????? / ????
							     AND EmployeeId = @EmployeeId)
        IF @ActualWorkdays IS NULL
        BEGIN
          SET @ActualWorkdays = 0
        END
        
        /* 
          Hours Calculation  
        */

        -- Get Working Minutes
        DECLARE @WorkingMin decimal(10,2) = 0
        SET @chrValue = ''
        SET @intValue = 0
		SET @WorkingMin = (SELECT SUM(WorkingMinutes) 
						       FROM [Logs].[AttendanceLog]
						      WHERE LogDate >= @BegDate
							    AND LogDate <= @EndDate
						        AND EmployeeId = @EmployeeId)

        IF (@WorkingMin IS NULL OR @WorkingMin = 0)
        BEGIN
          SET @WorkingMin = 0
        END
                
		-- Get Working Hours	
		SET @WorkingHours = (SELECT SUM(WorkingMinutes) /60
						       FROM [Logs].[AttendanceLog]
						      WHERE LogDate >= @BegDate
							    AND LogDate <= @EndDate
						        AND EmployeeId = @EmployeeId)
								


  --      -- Get In Extera Hours
  --      DECLARE @InExteraMin decimal(10,2) = 0
  --      SET @chrValue = ''
  --      SET @intValue = 0
		--SET @InExteraMin = (SELECT SUM(InExtraMinutes)
		--				      FROM [Logs].[AttendanceLog]
		--				     WHERE LogDate >= @BegDate
		--					   AND LogDate <= @EndDate
		--				       AND EmployeeId = @EmployeeId)
  --      IF (@InExteraMin IS NULL OR @InExteraMin = 0)
  --      BEGIN
  --        SET @InExteraHours = 0
  --      END
  --      ELSE
  --      IF @InExteraMin >= 60
  --      BEGIN
  --        SET @InExteraHours = @InExteraMin / 60
  --      END
  --      ELSE
  --      BEGIN
  --        SET @intValue = CAST(@InExteraMin AS INT)
  --        SET @chrValue = CAST('0.' as nvarchar(5)) + CAST(@intValue as nvarchar(10))
  --        SET @InExteraHours = CAST(CAST(@chrValue AS nvarchar(20)) AS decimal(10,2))         
  --      END

  --      -- Get Out Extera Hours
  --      DECLARE @OutExteraMin decimal(10,2) = 0
  --      SET @chrValue = ''
  --      SET @intValue = 0
		--SET @OutExteraMin = (SELECT SUM(OutExtraMinutes) 
		--				       FROM [Logs].[AttendanceLog]
		--				       WHERE LogDate >= @BegDate
		--					    AND LogDate <= @EndDate
		--				        AND StatusId IN (15, 20, 25, 30, 35, 40, 45, 55)
		--				        AND EmployeeId = @EmployeeId)
  --      IF (@OutExteraMin IS NULL OR @OutExteraMin = 0)
  --      BEGIN
  --        SET @OutExteraHours = 0
  --      END
  --      ELSE
  --      IF @OutExteraMin >= 60
  --      BEGIN
  --        SET @OutExteraHours = @OutExteraMin / 60
  --      END
  --      ELSE
  --      BEGIN
  --        SET @intValue = CAST(@OutExteraMin AS INT)
  --        SET @chrValue = CAST('0.' as nvarchar(5)) + CAST(@intValue as nvarchar(10))
  --        SET @OutExteraHours = CAST(CAST(@chrValue AS nvarchar(20)) AS decimal(10,2))         
  --      END
                
                        
        -- Get Extera Hours
        DECLARE @ExteraMin decimal(10,2) = 0
        SET @chrValue = ''
        SET @intValue = 0
		SET @ExteraMin = (SELECT SUM(ExtraTimeMinutes)
						    FROM [Logs].[AttendanceLog]
						   WHERE LogDate >= @BegDate
						     AND LogDate <= @EndDate
						     AND EmployeeId = @EmployeeId)

        IF (@ExteraMin IS NULL OR @ExteraMin = 0)
        BEGIN
          SET @ExteraHours = 0
        END
        ELSE
        IF @ExteraMin >= 60
        BEGIN
          SET @ExteraHours = @ExteraMin / 60
        END
        ELSE
        BEGIN
          SET @intValue = CAST(@ExteraMin AS INT)
          SET @chrValue = CAST('0.' as nvarchar(5)) + CAST(@intValue as nvarchar(10))
          SET @ExteraHours = CAST(CAST(@chrValue AS nvarchar(20)) AS decimal(10,2))         
        END
        
        
        -- Get Actual Working Hours
        DECLARE @ActualWorMin decimal(10,2) = 0
		DECLARE @ExceptionMinutes int
		SET @ExceptionMinutes=(SELECT SUM(WorkingMinutes)
						        FROM [Logs].[AttendanceLog]
						       WHERE LogDate >= @BegDate
							     AND LogDate <= @EndDate
							     AND (StatusId IN (20, 25, 30, 35, 40, 60) OR  StatusId > 999)
							     AND EmployeeId = @EmployeeId)
        SET @chrValue = ''
        SET @intValue = 0
		SET @ActualWorMin = (SELECT SUM(ActualWorkMinutes) 
						       FROM [Logs].[AttendanceLog]
						      WHERE LogDate >= @BegDate
							    AND LogDate <= @EndDate
								AND StatusId NOT IN (20, 25, 30, 35, 40, 60) 
						        AND EmployeeId = @EmployeeId)
       IF @ExceptionMinutes IS NULL
	   BEGIN
	   SET @ExceptionMinutes=0
	   END

        IF (@ActualWorMin IS NULL OR @ActualWorMin = 0)
        BEGIN
          SET @ActualWorkHours = 0
        END
        ELSE
        IF @ActualWorMin >= 60
        BEGIN
          SET @ActualWorkHours = (@ActualWorMin + @ExceptionMinutes) / 60
        END
        ELSE
        BEGIN
          SET @intValue = CAST(@ActualWorMin AS INT)
          SET @chrValue = CAST('0.' as nvarchar(5)) + CAST(@intValue as nvarchar(10))
          SET @ActualWorkHours = CAST(CAST(@chrValue AS nvarchar(20)) AS decimal(10,2))         
        END

        -- Get Late Hours
        DECLARE @LateMin decimal(10,2) = 0
        SET @chrValue = ''
        SET @intValue = 0
		SET @LateMin = (SELECT SUM(LateMinutes) 
					      FROM [Logs].[AttendanceLog]
						 WHERE LogDate >= @BegDate
						 AND LogDate <= @EndDate
						 AND EmployeeId = @EmployeeId)						     
        IF (@LateMin IS NULL OR @LateMin = 0)
        BEGIN
          SET @LateHours = 0
        END
        ELSE
        IF @LateMin >= 60
        BEGIN
          SET @LateHours = @LateMin / 60
        END
        ELSE
        BEGIN
          SET @intValue = CAST(@LateMin AS INT)
          SET @chrValue = CAST('0.' as nvarchar(5)) + CAST(@intValue as nvarchar(10))
          SET @LateHours = CAST(CAST(@chrValue AS nvarchar(20)) AS decimal(10,2))         
        END

        SET @ActualWorMin=@ActualWorMin + @ExceptionMinutes
		       
        SET @DisciplinePercentage = (@ActualWorMin / @WorkingMin) * 100
        
        IF @DisciplinePercentage IS NULL OR @DisciplinePercentage < 0 
        BEGIN
          SET @DisciplinePercentage = 0
        END
        
        -- Get Gatepass Permissions Count
		SET @GatepassCount = (SELECT COUNT(GatepassId)
						        FROM Managements.Gatepass
						       WHERE GatepassBegDate >= @BegDate
						             AND GatepassEndDate <= @EndDate
						             AND EmployeeId = @EmployeeId
									 AND IsApproved=1)
        IF @GatepassCount IS NULL
        BEGIN
          SET @GatepassCount = 0
        END
                                                 																		
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
