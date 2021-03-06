USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpDepartment_GetUserTreeForDropdownTree]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Employee departments tree 
-- ===============================================
CREATE PROCEDURE [Employees].[SpDepartment_GetUserTreeForDropdownTree]
(
 @EmployeeId int = NULL,
 @Lang       Char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;


    -- Declariations
    DECLARE @RowEffected int
    DECLARE @DepartmentId int
    
    -- Initializations            
    SET @RowEffected = 0

	-- Create user temporary tree table	
    CREATE TABLE #UserTree 
    (
	 DepartmentId int, 
	 DepartmentName nvarchar(100), 
	 DepartmentNameEN nvarchar(100), 
	 ParentId int
    )
        
    BEGIN TRY
	IF @EmployeeId<>0
	BEGIN						  

				   ;WITH CTE(DepartmentId, DepartmentName,DepartmentNameEN, ParentId)
				AS
				(
				SELECT DepartmentId, DepartmentName,DepartmentNameEN, ParentId
				  From Employees.Department 
				 WHERE DepartmentId in (SELECT DepartmentId 
									   FROM Employees.EmployeeDepartmentLink
									  WHERE EmployeeId = @EmployeeId)  
				 UNION ALL
				SELECT e.DepartmentId, e.DepartmentName,e.DepartmentNameEN, e.ParentId
				  FROM Employees.Department e INNER JOIN CTE c
					ON e.ParentId = c.DepartmentId
				)
				INSERT INTO #UserTree
				(DepartmentId, DepartmentName,DepartmentNameEN, ParentId)
				SELECT DepartmentId, DepartmentName,DepartmentNameEN, ParentId 
				FROM CTE  
		 
        
				-- check if tree root is exists
				IF NOT EXISTS(SELECT DepartmentId FROM #UserTree WHERE ParentId IS NULL)
				BEGIN
				  -- create a tree root
				  UPDATE #UserTree SET ParentId = NULL WHERE DepartmentId in (SELECT DepartmentId 
									   FROM Employees.EmployeeDepartmentLink
									  WHERE EmployeeId = @EmployeeId)  
				END
        
		END
		ELSE
		BEGIN --Return All Departments
				SET @DepartmentId =1
				  ;WITH CTE(DepartmentId, DepartmentName,DepartmentNameEN, ParentId)
				AS
				(
				SELECT DepartmentId, DepartmentName,DepartmentNameEN, ParentId
				  From Employees.Department 
				 WHERE DepartmentId =@DepartmentId
				 UNION ALL
				SELECT e.DepartmentId, e.DepartmentName,e.DepartmentNameEN, e.ParentId
				  FROM Employees.Department e INNER JOIN CTE c
					ON e.ParentId = c.DepartmentId
				)
				INSERT INTO #UserTree
				(DepartmentId, DepartmentName,DepartmentNameEN, ParentId)
				SELECT DepartmentId, DepartmentName,DepartmentNameEN, ParentId 
				FROM CTE  
		 
        
				-- check if tree root is exists
				IF NOT EXISTS(SELECT DepartmentId FROM #UserTree WHERE ParentId IS NULL)
				BEGIN
				  -- create a tree root
				  UPDATE #UserTree SET ParentId = NULL WHERE DepartmentId =@DepartmentId 
				END
		END 

		if @Lang = 'en'
        SELECT DepartmentId
		, DepartmentNameEN as DepartmentName
		, ParentId 
		FROM  #UserTree
		--UNION SELECT 0,'......إختر......',null
		order by DepartmentNameEN

		else

		 SELECT DepartmentId
		, DepartmentName
		, ParentId 
		FROM  #UserTree
		--UNION SELECT 0,'......إختر......',null
		order by DepartmentName

        		   		     		      
		RETURN    
    END TRY
    BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN    
    END CATCH
END







GO
