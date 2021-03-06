USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendance_GetChartDataByDepartmentForComparssionReport]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Logs].[SpAttendance_GetChartDataByDepartmentForComparssionReport]
(
 @ParentId         int =1,
 @StatusID         int=55,
 @BegDate          date='2010-01-01',
 @EndDate          date='2020-01-01',
 @Lang         char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations

	DECLARE @AllDays AS decimal(10,2)
	DECLARE @StatusDays AS decimal(10,2)
	DECLARE @DeptId AS INT
	DECLARE @StatusName AS NvarChar(20)
	DECLARE @DisciplinePercentage decimal(10,2)

	SELECT @StatusName=CASE WHEN @Lang = 'en' THEN StatusNameEN ELSE StatusName END FROM Logs.AttendanceStatus WHERE StatusId=@StatusID

	CREATE TABLE #ChartData
	([DepartmentId]   int, 
	 [DepartmentName] nvarchar(100), 
     [StatusName] nvarchar(100),
	 [StatusId]   decimal(10,2)
	 )
	  
     INSERT INTO #ChartData(DepartmentId,DepartmentName,StatusName)
	 SELECT DepartmentId, CASE WHEN @Lang='en' THEN DepartmentNameEN ELSE DepartmentName END ,@StatusName FROM Employees.Department
	 WHERE ParentId=@ParentId 
	 --AND AccountDepartmentId<>3


	 DECLARE Curs_Department CURSOR
    LOCAL FOR                                          
    SELECT DepartmentId FROM #ChartData  
    FOR READ ONLY
	 

	 CREATE TABLE #Departments
    (
     [DepartmentId]   int, 
     [DepartmentName] nvarchar(100), 
     [ParentId]       int
     )
          
    BEGIN TRY

	 OPEN Curs_Department
		 FETCH NEXT FROM Curs_Department INTO @DeptId
		 WHILE @@Fetch_Status = 0 
		 BEGIN 
		 --================================================================
		 DELETE FROM #Departments


		 ;WITH CTE(DepartmentId, DepartmentName, ParentId)
		AS
		(
		SELECT DepartmentId, CASE WHEN @Lang='en' THEN DepartmentNameEN ELSE DepartmentName END , ParentId
		  From Employees.Department
		 WHERE DepartmentId = @DeptId
		 UNION ALL
		SELECT e.DepartmentId, CASE WHEN @Lang='en' THEN e.DepartmentNameEN ELSE e.DepartmentName END, e.ParentId
		  FROM Employees.Department e
		       INNER JOIN CTE c
		    ON e.ParentId = c.DepartmentId
			)
		
		INSERT INTO #Departments
		       (
                [DepartmentId], 
                [DepartmentName], 
                [ParentId] 
		       )
		SELECT  [DepartmentId], 
                [DepartmentName], 
                [ParentId]
           FROM CTE        


		  SELECT @AllDays= Count(StatusId) 
		  FROM logs.vAttendanceChart  
		  JOIN #Departments ON logs.vAttendanceChart .DepartmentId=#Departments.DepartmentId
		  
		  IF @AllDays IS NULL OR @AllDays=0
		  GOTO Cont

		  SELECT @StatusDays= Count(StatusId) 
		  FROM logs.vAttendanceChart  
		  JOIN #Departments ON logs.vAttendanceChart .DepartmentId=#Departments.DepartmentId
		  WHERE Logs.vAttendanceChart.StatusId=@StatusID

		  SET @DisciplinePercentage = (@StatusDays / @AllDays) * 100
		  UPDATE #ChartData SET StatusId=@DisciplinePercentage WHERE DepartmentId=@DeptId
		 --================================================================
		 Cont:
		 FETCH NEXT FROM Curs_Department INTO @DeptId
		 END

		 --SELECT @AllDays= Count(StatusId) 
		 --FROM logs.vAttendanceChart
		 --WHERE DepartmentId=@ParentId

		 -- IF @AllDays IS NOT NULL AND @AllDays <> 0
		 --BEGIN
			-- INSERT INTO #ChartData(DepartmentId,DepartmentName,StatusName)
			-- SELECT DepartmentId,DepartmentName,@StatusName FROM Employees.Department
			-- WHERE DepartmentId=@ParentId 
	    
			-- SELECT @StatusDays= Count(StatusId) 
			-- FROM logs.vAttendanceChart  
			-- WHERE DepartmentId=@ParentId AND StatusId=@StatusID 
		
			-- SET @DisciplinePercentage = (@StatusDays / @AllDays) * 100
			-- UPDATE #ChartData SET StatusId=@DisciplinePercentage WHERE DepartmentId=@ParentId
		 --END

      SELECT * FROM #ChartData
	  
		RETURN 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
