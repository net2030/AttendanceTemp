USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpMachineLog_GetManualLogs]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/5/2011
-- Description:	get machine log by id
-- =============================================
CREATE PROCEDURE [Logs].[SpMachineLog_GetManualLogs]
(
 @DepartmentId       int = NULL,
 @BegDate          date = NULL,
 @EndDate          date = NULL
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;
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
		SELECT ML.[LogId]
			  ,ML.[EmployeeId]
			  ,AE.FirstName + ' ' + AE.MiddleName + ' ' + AE.LastName AS EmployeeName	

			   ,dbo.FnUmAlquraYMD(ML.LogDate) AS LogDate
			  ,Convert(time,ML.[LogTime]) as LogTime
			  ,CASE ML.LogTypeId WHEN 1 THEN 'دخول' WHEN 2 THEN 'خروج' END AS LogType
              ,CASE ML.IsManualRecord WHEN 0 THEN 'إضافة آلية' WHEN 1 THEN 'إضافة يدوية' END AS ManualRecord
              ,#Departments.DepartmentName
			  ,ML.[AddedUserAccountId]
			  ,ML.[UpdatedUserAccountId]
			  ,ML.[AddedDate]
			  ,AE1.FirstName + ' ' + AE1.MiddleName + ' ' + AE1.LastName AS AddedBy
			  ,ML.[AddedDate]
		  FROM  [Logs].[MachineLog] ML
		  INNER JOIN Employees.Employee AE ON AE.EmployeeId=ML.EmployeeId
		   INNER JOIN Employees.Employee AE1 ON AE1.EmployeeId=ML.AddedUserAccountId
		   INNER JOIN #Departments ON AE.DepartmentId=#Departments.DepartmentId
		   WHERE ML.LogDate>=@BegDate AND ML.LogDate<=@EndDate
		   AND ML.IsManualRecord=1
		   ORDER BY ML.LogId DESC

		RETURN		 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END








GO
