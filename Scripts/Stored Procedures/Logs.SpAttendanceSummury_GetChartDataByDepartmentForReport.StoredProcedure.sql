USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendanceSummury_GetChartDataByDepartmentForReport]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Attendance Team
-- Create date: 02/07/2011
-- Description:	Get absent Employees By Id & Date
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendanceSummury_GetChartDataByDepartmentForReport]
(
 @DepartmentId        int =1,
 @BegDate          date='2010-01-01',
 @EndDate          date='2020-01-01'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations


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
		 WHERE DepartmentId = @DepartmentId
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


		 

      SELECT  
   
	   vAC.AttendanceDate

      ,SUM(vAC.[ActualWorkMinutes])  AS ActualWorkMinutes
      ,SUM(vAC.[ExtraTimeMinutes]) AS ExtraTimeMinutes
      ,SUM(vAC.[LateMinutes]) AS LateMinutes
      ,SUM(vAC.[WorkingMinutes]) AS WorkingMinutes
      
      FROM [Logs].[vAttendanceChart] vAC
	  --JOIN #Departments  ON vAC.AccountDepartmentId=#Departments.DepartmentId
	  WHERE vAC.AttendanceDate>=@BegDate AND vAC.AttendanceDate<=@EndDate
	  AND vAC.DepartmentId IN(SELECT DepartmentId FROM #Departments)
	  GROUP BY   vAC.[WorkingMinutes]
			    ,vAC.AttendanceDate
		

	  
		RETURN 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
