USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendance_GetChartDataByDepartment]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Attendance Team
-- Create date: 02/07/2011
-- Description:	Get absent Employees By Id & Date
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendance_GetChartDataByDepartment]
(
 @EmployeeId     int = null,
 @BegDate          date='2010-01-01',
 @EndDate          date='2020-01-01',
 @Lang             char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int

	DECLARE @Persent Float
	DECLARE @Absent Float
	DECLARE @Late Float
	DECLARE @Early Float
	DECLARE @LateEarly Float
	DECLARE @Exception Float
	DECLARE @UncompleteTran Float


    SET @BegDate= DATEADD(month,-1,GETDATE())--DATEADD(month, DATEDIFF(month, 0, GetDate()), 0) 

	DECLARE @All int

	 CREATE TABLE #Departments
    (
     [DepartmentId]   int, 
     [DepartmentName] nvarchar(100), 
     [ParentId]       int
     )
          
    BEGIN TRY
	    ;WITH CTE(DepartmentId, DepartmentName, ParentId)
		AS
		(
		SELECT DepartmentId, DepartmentName, ParentId
		  From Employees.Department
		 WHERE DepartmentId in(SELECT DepartmentId FROM Employees.EmployeeDepartmentLink WHERE Employeeid =@EmployeeId)
		 UNION ALL
		SELECT e.DepartmentId, e.DepartmentName, e.ParentId
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
		        
   SELECT @All=COUNT(StatusId) FROM Logs.vwAttendanceLog WHERE DepartmentId in(SELECT DepartmentId FROM #Departments) AND LogDate>=@BegDate  AND LogDate<=@EndDate AND StatusId Not In (70)
   IF @All=0 
   Return

	SELECT @Persent=COUNT(StatusId) FROM  Logs.vwAttendanceLog  WHERE DepartmentId in(SELECT DepartmentId FROM #Departments) AND StatusId in(55,15) AND LogDate>=@BegDate AND LogDate<=@EndDate
	SELECT @Absent=COUNT(StatusId) FROM  Logs.vwAttendanceLog  WHERE DepartmentId in(SELECT DepartmentId FROM #Departments) AND StatusId=75 AND LogDate>=@BegDate AND LogDate<=@EndDate
	SELECT @Late=COUNT(StatusId) FROM  Logs.vwAttendanceLog  WHERE DepartmentId in(SELECT DepartmentId FROM #Departments) AND StatusId=45 AND LogDate>=@BegDate AND LogDate<=@EndDate
	SELECT @Early=COUNT(StatusId) FROM  Logs.vwAttendanceLog  WHERE DepartmentId in(SELECT DepartmentId FROM #Departments) AND StatusId=110 AND LogDate>=@BegDate AND LogDate<=@EndDate
	SELECT @LateEarly=COUNT(StatusId) FROM  Logs.vwAttendanceLog  WHERE DepartmentId in(SELECT DepartmentId FROM #Departments) AND StatusId=120 AND LogDate>=@BegDate AND LogDate<=@EndDate
	SELECT @Exception=COUNT(StatusId) FROM  Logs.vwAttendanceLog  WHERE DepartmentId in(SELECT DepartmentId FROM #Departments) AND (StatusId IN(20,25,30,40,60,100) OR  StatusId > 1000) AND LogDate>=@BegDate AND LogDate<=@EndDate
	SELECT @UncompleteTran=COUNT(StatusId) FROM  Logs.vwAttendanceLog  WHERE DepartmentId in(SELECT DepartmentId FROM #Departments) AND StatusId IN(65,70) AND LogDate>=@BegDate AND LogDate<=@EndDate
  
	

	SET @Persent=CEILING(@Persent/@All *100)
    SET @Absent=CEILING(@Absent/@All *100)
	SET @Late=CEILING(@Late/@All *100)
	SET @Early=CEILING(@Early/@All *100)
    SET @LateEarly=CEILING(@LateEarly/@All *100)
	SET @Exception=CEILING(@Exception/@All *100)
	SET @UncompleteTran=CEILING(@UncompleteTran/@All *100)


		

	IF @Lang = 'en'
		BEGIN
			SELECT 'حاضر'  AS Key2,'Present' AS Key1,@Persent AS Value UNION
			SELECT 'إستثناء دوام'  AS Key2,'Time-Off' AS Key1,@Exception AS Value UNION
			SELECT 'غائب' AS  Key2,'Absent' AS Key1,@Absent AS Value UNION 
			SELECT 'متأخر' AS  Key2,'Late' AS Key1,@Late AS Value UNION 
			SELECT 'خروج مبكر' AS Key2,'Early Leave' AS Key1,@Early AS Value UNION 
			SELECT 'دخول متأخر وخروج مبكر' AS  Key2,'Late & Early Lave' AS Key1,@LateEarly AS Value UNION
			SELECT 'حركات غير مكتملة' AS Key2,'Uncomplete Trans.' AS Key1,@UncompleteTran AS Value 
	    END
		ELSE
		BEGIN
			SELECT 'حاضر'  AS Key1,'Present' AS Key2,@Persent AS Value UNION
			SELECT 'إستثناء دوام'  AS Key1,'Time-Off' AS Key2,@Exception AS Value UNION
			SELECT 'غائب' AS  Key1,'Absent' AS Key2,@Absent AS Value UNION 
			SELECT 'متأخر' AS  Key1,'Late' AS Key2,@Late AS Value UNION 
			SELECT 'خروج مبكر' AS Key1,'Early Leave' AS Key2,@Early AS Value UNION 
			SELECT 'دخول متأخر وخروج مبكر' AS  Key1,'Late & Early Lave' AS Key2,@LateEarly AS Value UNION
			SELECT 'حركات غير مكتملة' AS Key1,'Uncomplete Trans.' AS Key2,@UncompleteTran AS Value 
	    END

	--SELECT StatusName AS Key2,COUNT(StatusId) AS Value
	--FROM vwAttendanceLog  
	--WHERE StatusId NOT IN(10,15,50,100) AND EmployeeId=5  
	--GROUP BY StatusName
		RETURN 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END





GO
