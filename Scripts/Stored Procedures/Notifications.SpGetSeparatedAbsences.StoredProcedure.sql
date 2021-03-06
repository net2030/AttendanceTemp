USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Notifications].[SpGetSeparatedAbsences]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Adnan Salah
-- Create date: 23/09/2008
-- Description:	Get Consecutive Absences
-- =============================================
CREATE PROCEDURE [Notifications].[SpGetSeparatedAbsences]

WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

	 DECLARE @StartDate DATE
     DECLARE @EndDate DATE
     DECLARE @EmployeeID INT

	 CREATE Table #NotificationsTemp(N INT NOT NULL IDENTITY(1,1),EmployeeId INT ,SDate Date,EDate Date,NoDays Int)
     CREATE TABLE #AllConsecutiveAbsences(EMP_ID int, absence_date DATE)
    BEGIN TRY
               DECLARE C1 CURSOR LOCAL FOR
			   SELECT EmployeeId,StartDate,EndDate FROM Notifications.Notification WHERE CurrentYear=YEAR(GetDate()) AND AbsenceType=1

				OPEN C1
				FETCH NEXT FROM C1 INTO
				@EmployeeID,@StartDate,@EndDate
				WHILE @@FETCH_STATUS = 0
				BEGIN  
				INSERT INTO #AllConsecutiveAbsences
				
				SELECT @EmployeeID,* FROM dbo.ExplodeDates(@StartDate,@EndDate) 
				

				FETCH NEXT FROM C1 INTO
				@EmployeeID,@StartDate,@EndDate
				END


                DECLARE C2 CURSOR LOCAL FOR
				SELECT DISTINCT EMP_ID FROM #AllConsecutiveAbsences

				OPEN C2
				FETCH NEXT FROM C2 INTO
				@EmployeeID
				WHILE @@FETCH_STATUS = 0
				BEGIN   
				        CREATE Table #SepratedAbsencesPerEmployee(N INT NOT NULL IDENTITY(1,1),EmpId INT,Absencedate Date,NoDays INT)
						INSERT INTO #SepratedAbsencesPerEmployee(EmpId,Absencedate)
						SELECT EmployeeId,LogDate FROM Logs.AttendanceLog 
						WHERE EmployeeId=@EmployeeID
						AND StatusId=75
						AND  Convert(NvarChar(4),LogDate,102)=YEAR(GetDate())
				        AND LogDate NOT IN (
				        SELECT absence_date FROM #AllConsecutiveAbsences WHERE EMP_ID=@EmployeeID)	

						INSERT INTO #NotificationsTemp(EmployeeId,SDate,EDate,NoDays)
						SELECT EmpId,MIN(Absencedate) AS SDate,MAX(Absencedate) AS EDate ,10
						FROM (
							SELECT *, CAST((N-1)/10 AS INT) AS [Rank] 
							FROM #SepratedAbsencesPerEmployee
						) d
						WHERE EmpId=@EmployeeID
						GROUP BY [Rank],EmpId
				        
						Having count(*) =10
				
				drop table #SepratedAbsencesPerEmployee	 		
				FETCH NEXT FROM C2 INTO
					  @EmployeeID
				END

		

               INSERT INTO Notifications.Notification(EmployeeId,StartDate,EndDate,Grp,NoOfDays,AbsenceType,CurrentYear)
			    SELECT EmployeeId,SDate,EDate,0,NoDays,2,YEAR(GetDate())
			    FROM #NotificationsTemp 
			    WHERE SDate Not IN	
			                   (SELECT StartDate 
							     FROM Notifications.Notification 
								 WHERE EmployeeId=EmployeeId AND AbsenceType=2 AND CurrentYear=YEAR(GetDate()))		
                





CLOSE C1
DEALLOCATE C1
CLOSE C2
DEALLOCATE C2
drop table #AllConsecutiveAbsences
drop table #NotificationsTemp
 
    END TRY
    BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN      
    END CATCH
END














GO
