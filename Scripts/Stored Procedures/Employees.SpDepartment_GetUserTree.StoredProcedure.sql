USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpDepartment_GetUserTree]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Employee departments tree 
-- ===============================================
CREATE PROCEDURE [Employees].[SpDepartment_GetUserTree]
(
 @EmployeeId int = NULL
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
	 ParentId int
    )
        
    BEGIN TRY
		SET @DepartmentId = (SELECT DepartmentId 
							   FROM Employees.EmployeeDepartmentLink
							  WHERE EmployeeId = @EmployeeId)    
                        

	    ;WITH CTE(DepartmentId, DepartmentName, ParentId)
		AS
		(
		SELECT DepartmentId, DepartmentName, ParentId
		  From Employees.Department 
		 WHERE DepartmentId =@DepartmentId
		 UNION ALL
		SELECT e.DepartmentId, e.DepartmentName, e.ParentId
		  FROM Employees.Department e INNER JOIN CTE c
		    ON e.ParentId = c.DepartmentId
		)
		INSERT INTO #UserTree
		(DepartmentId, DepartmentName, ParentId)
		SELECT DepartmentId, DepartmentName, ParentId 
		FROM CTE  
        
        -- check if tree root is exists
        IF NOT EXISTS(SELECT DepartmentId FROM #UserTree WHERE ParentId IS NULL)
        BEGIN
          -- create a tree root
          UPDATE #UserTree SET ParentId = -1 WHERE DepartmentId = @DepartmentId
        END
        
        SELECT DepartmentId, DepartmentName, ParentId FROM  #UserTree
        		   		     		      
		RETURN    
    END TRY
    BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN    
    END CATCH
END






GO
