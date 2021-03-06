USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpDepartments_GetAll]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get Training by id
-- =============================================
CREATE PROCEDURE [Employees].[SpDepartments_GetAll]

WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @FirstNode AS Nvarchar(200)

	CREATE TABLE #Departments
    (
	 DepartmentId int, 
	 DepartmentName nvarchar(300), 
	 DepartmentNameEN nvarchar(300), 
	 ParentId int,
	 ParentName nvarchar(300), 
	 ParentNameEN nvarchar(300), 
    )

  BEGIN TRY	            
		WITH Hierarchy(ChildId, ChildName,ChildNameEN, ParentId, Parents,ParentsEN)
		AS
		(
		SELECT DepartmentId, DepartmentName,DepartmentNameEN, ParentId, CAST('' AS VARCHAR(MAX)),CAST('' AS VARCHAR(MAX))
			FROM Employees.Department AS FirtGeneration
			WHERE ParentId =0--IS NULL    
		UNION ALL
		SELECT NextGeneration.DepartmentId, NextGeneration.DepartmentName,NextGeneration.DepartmentNameEN, Parent.ChildId,
		CAST(CASE WHEN Parent.Parents = ''
			THEN(CAST(NextGeneration.DepartmentName AS VARCHAR(MAX)))
			ELSE(Parent.Parents + ' ==> ' + CAST(NextGeneration.DepartmentName AS VARCHAR(MAX)))
			END AS VARCHAR(MAX)),
		CAST(CASE WHEN Parent.ParentsEN = ''
			THEN(CAST(NextGeneration.DepartmentNameEN AS VARCHAR(MAX)))
			ELSE(Parent.ParentsEN + ' ==> ' + CAST(NextGeneration.DepartmentNameEN AS VARCHAR(MAX)))
			END AS VARCHAR(MAX))
			FROM Employees.Department AS NextGeneration
			INNER JOIN Hierarchy AS Parent ON NextGeneration.ParentId = Parent.ChildId 
		   
			)
			INSERT INTO #Departments(DepartmentId,DepartmentName,DepartmentNameEN,ParentId,ParentName,ParentNameEN)
			SELECT ChildId,ChildName,ChildNameEN,ParentId,Parents,ParentsEN
				FROM Hierarchy
			OPTION(MAXRECURSION 32767)
			
			SELECT @FirstNode=DepartmentName FROM #Departments WHERE ParentId=0
			UPDATE #Departments SET ParentName=@FirstNode+' ==> '+ParentName WHERE ParentName=DepartmentName	   
			
			SELECT * FROM #Departments  
			ORDER By ParentName  
		
     RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN     
    END CATCH
END







GO
