USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpProcessAttendanceByDepartment]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 18-09-2008
-- Description:	Process Attendance By Department
-- =============================================
CREATE PROCEDURE [Logs].[SpProcessAttendanceByDepartment] 
(
 @AttendanceDate [date] = NULL,
 @DepartmentId   [int] = NULL,
 @RC             [int] = NULL OUTPUT
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

   /*****************************************************
    Cursor Variables
   *****************************************************/     
   DECLARE @EmployeeId int
   DECLARE @ActionId int
         
   /*****************************************************
    Employees temporary table
   *****************************************************/      
   CREATE TABLE #Employees (EmployeeId int, ActionId int)
          
   BEGIN TRY      
	 /*****************************************************
	   Insert Active Employees 
	 *****************************************************/
	    ;WITH CTE(DepartmentId, DepartmentName, ParentId)
		AS
		(
		SELECT DepartmentId, DepartmentName, ParentId
		  From Employees.Department 
		 WHERE DepartmentId = @DepartmentId
		 UNION ALL
		SELECT e.DepartmentId, e.DepartmentName, e.ParentId
		  FROM Employees.Department e
		       INNER JOIN CTE c
		    ON e.ParentId = c.DepartmentId
			)
				       
     INSERT INTO #Employees (EmployeeId, ActionId)
	 SELECT Employees.Employee.EmployeeId, 
			Employees.Employee.ActionId 
	   FROM Employees.Employee INNER JOIN CTE
	     ON Employees.Employee.DepartmentId = CTE.DepartmentId 
    
	 /*****************************************************
	   Cursor Declaration
	 *****************************************************/      
     DECLARE Curs_Employees CURSOR
     LOCAL FOR                                          
     SELECT EmployeeId, ActionId FROM #Employees
     FOR READ ONLY

	 /*****************************************************
	   Open Cursor
	 *****************************************************/      
     OPEN Curs_Employees
     FETCH NEXT FROM Curs_Employees INTO @EmployeeId, @ActionId
     WHILE @@Fetch_Status = 0 
     BEGIN       
       IF NOT EXISTS(SELECT EmployeeId 
                       FROM Logs.AttendanceLog
                      WHERE LogDate    = @AttendanceDate
                        AND EmployeeId = @EmployeeId)
	   /*****************************************************
		 New Attendance log Employee
	   *****************************************************/                         
       BEGIN
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
												@EarlyOutMinutes OUTPUT 
																								                                                 
		 /*****************************************************
			Check Day Of week
		  *****************************************************/                                        
         EXECUTE Logs.SpGetWorkScheduleDay @AttendanceDate,
                                           @WorkScheduleId, 
                                           @IsWorkingDay OUTPUT,
										   @WorkBegTime OUTPUT,
										   @WorkEndTime OUTPUT,
										   @WorkingMinutes OUTPUT  

         IF @IsWorkingDay = 1
         BEGIN
           INSERT INTO  [Logs].[AttendanceLog]
                 ([LogDate],
                  [EmployeeId],
                  [WorkScheduleId],
                  [WorkStartTime],
                  [WorkEndTime],
                  [PolicyId],
                  [WorkingMinutes],
                  [LateInMinutes],
                  [LateOutMinutes],
                  [MarkObsentDuration],
                  [ActionId],
                  [EarlyInMinutes],
                  [EarlyOutMinutes]
                  )
           VALUES (@AttendanceDate,
                   @EmployeeId,
                   @WorkScheduleId,
                   @WorkBegTime,
                   @WorkEndTime,
                   @PolicyId,
                   @WorkingMinutes,
                   @LateInMinutes,
                   @LateOutMinutes,
                   @MarkObsentDuration, 
                   @ActionId,
                   @EarlyInMinutes, 
                   @EarlyOutMinutes)                   
           
           SET @IdentityId = SCOPE_IDENTITY()
           
           IF @@ERROR = 0
           BEGIN
             IF @ActionId = 3
             BEGIN
               EXECUTE Logs.SpUpdateAttendanceStatus @AttendanceDate, @IdentityId 
             END
             ELSE
             BEGIN
               EXECUTE Logs.SpUpdateTimesFromMachineLog @IdentityId, @AttendanceDate, @EmployeeId
               EXECUTE Logs.SpUpdateAttendanceStatus @AttendanceDate, @IdentityId             
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
                               AND EmployeeId = @EmployeeId)
                               
         IF @ActionId = 3
         BEGIN
           EXECUTE Logs.SpUpdateAttendanceStatus @AttendanceDate, @IdentityId 
         END
         ELSE
         BEGIN
           EXECUTE Logs.SpUpdateTimesFromMachineLog @IdentityId, @AttendanceDate, @EmployeeId
           EXECUTE Logs.SpUpdateAttendanceStatus @AttendanceDate, @IdentityId             
         END 
       END
   
       FETCH NEXT FROM Curs_Employees INTO @EmployeeId, @ActionId
     END
     
     -- close cursor
     CLOSE Curs_Employees
     -- de-allocate cursor
     DEALLOCATE Curs_Employees

     RETURN
   END TRY
   BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN
   END CATCH
END






GO
