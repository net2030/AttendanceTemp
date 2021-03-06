USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Notifications].[SpGetConsecutiveAbsencesByEmployee]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO














-- =============================================
-- Author:		Adnan Salah
-- Create date: 23/09/2008
-- Description:	Get Consecutive Absences
-- =============================================
CREATE PROCEDURE [Notifications].[SpGetConsecutiveAbsencesByEmployee]
(
@EmployeeId int
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;


	--Local Parameters
    DECLARE @Year Nvarchar(4)
	DECLARE @StartDate DATE
	DECLARE @EndDate DATE
	DECLARE @Group AS INT

    BEGIN TRY

	--Set Local Parameters
	SET @Year =YEAR(GetDate())
	SET @StartDate=(SELECT  DATEADD(yy, DATEDIFF(yy,0,getdate()), 0))
    SET @EndDate=(SELECT  DATEADD(yy, DATEDIFF(yy,0,getdate()) + 1, -1))



	CREATE Table #AllConsecutiveAbsences(N INT NOT NULL IDENTITY(1,1),EmpId INT,Absencedate Date,ConDays INT,GroupNum int)
	CREATE Table #NotificationsTemp(N INT NOT NULL IDENTITY(1,1),EmployeeId INT ,SDate Date,EDate Date,NoDays Int,GroupNo INT)

	
     CREATE TABLE #Calendar( N INT NOT NULL IDENTITY(1,1)
                          ,Dt DATE NOT NULL PRIMARY KEY CLUSTERED)

    --Insert The Weekends
	;WITH cte(Dt ) AS
	(
		SELECT @startDate
		UNION ALL
		SELECT DATEADD(dd,CASE WHEN DATEDIFF(dd,0,Dt)%7 = 3 THEN 3 ELSE 1 END,Dt) FROM cte
		WHERE Dt < @endDate
	)
	INSERT INTO #Calendar SELECT Dt FROM cte OPTION (MAXRECURSION 0);



     		CREATE TABLE #tmp(EMP_ID int, absence_date DATE);
			INSERT INTO #tmp 
			SELECT EmployeeId, LogDate  
			FROM Logs.AttendanceLog 
			WHERE StatusId=75 AND EmployeeId=@EmployeeId
			AND  Convert(NvarChar(4),LogDate,102)=@Year
			--AND LogDate Not Between (SELECT StartDate FROM Notifications WHERE EmployeeId=EmployeeId) 
			--                    AND (SELECT EndDate FROM Notifications)

			;WITH cte AS
			(
				SELECT
					*,
					N-ROW_NUMBER() OVER (PARTITION BY Emp_id ORDER BY absence_date) AS GroupNum
				FROM
					#Calendar c
					INNER JOIN #tmp T ON T.absence_date = c.Dt
			),
			cte2 AS
			(
				SELECT *,COUNT(*) OVER (PARTITION BY Emp_id,GroupNum) AS ConsecutiveAbsences
				FROM cte
			)
			INSERT INTO #AllConsecutiveAbsences(EmpId,Absencedate,ConDays,GroupNum)
			SELECT  EMP_ID,absence_date ,ConsecutiveAbsences,GroupNum 
			FROM cte2 
			WHERE ConsecutiveAbsences >= 5 
				
			--group by EMP_ID,ConsecutiveAbsences,GroupNum
				DECLARE C1 CURSOR LOCAL FOR
				SELECT DISTINCT GroupNum FROM #AllConsecutiveAbsences

				OPEN C1
				FETCH NEXT FROM C1 INTO
				@Group
				WHILE @@FETCH_STATUS = 0
				BEGIN   
				        CREATE Table #ConsecutiveAbsencesPerGroup(N INT NOT NULL IDENTITY(1,1),EmpId INT,Absencedate Date,ConDays INT,GroupNum int)
						INSERT INTO #ConsecutiveAbsencesPerGroup(EmpId,Absencedate,ConDays,GroupNum)
						SELECT EmpId,Absencedate,ConDays,GroupNum FROM #AllConsecutiveAbsences WHERE GroupNum=@Group			

						INSERT INTO #NotificationsTemp(EmployeeId,SDate,EDate,NoDays,GroupNo)
						SELECT EmpId,MIN(Absencedate) AS SDate,MAX(Absencedate) AS EDate ,5 ,GroupNum
						FROM (
							SELECT *, CAST((N-1)/5 AS INT) AS [Rank] 
							FROM #ConsecutiveAbsencesPerGroup
						) d
						WHERE GroupNum=@Group
						GROUP BY [Rank],EmpId,GroupNum,ConDays
				        
						Having count(*) =5
				
				drop table #ConsecutiveAbsencesPerGroup	 		
				FETCH NEXT FROM C1 INTO
					  @Group
				END

				CLOSE C1
				DEALLOCATE C1				
			INSERT INTO Notifications.Notification(EmployeeId,StartDate,EndDate,Grp,NoOfDays,AbsenceType,CurrentYear)
			SELECT EmployeeId,SDate,EDate,GroupNo,NoDays,1,@Year
			 FROM #NotificationsTemp 
			 WHERE SDate Not IN	
			                   (SELECT StartDate 
							     FROM Notifications.Notification 
								 WHERE EmployeeId=@EmployeeId AND AbsenceType=1 AND CurrentYear=@Year)						


drop table #NotificationsTemp
drop table #Calendar
drop table #tmp	 		
drop table #AllConsecutiveAbsences
      		       
	      
    END TRY
    BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN      
    END CATCH
END






/****** Object:  StoredProcedure [Logs].[SpGetSeparatedAbsences]    Script Date: 09/17/2014 09:19:12 ص ******/
SET ANSI_NULLS ON









GO
