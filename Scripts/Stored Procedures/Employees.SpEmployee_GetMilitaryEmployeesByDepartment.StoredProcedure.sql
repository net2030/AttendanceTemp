USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_GetMilitaryEmployeesByDepartment]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Military Employees by department
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_GetMilitaryEmployeesByDepartment]
(
 @DepartmentId  int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected  int
     
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
			             
		SELECT Employees.Employee.EmployeeId, 
		  	   Employees.Employee.EmployeeName 			 
		  FROM Employees.Employee INNER JOIN CTE
		    ON Employees.Employee.DepartmentId = CTE.DepartmentId
		 WHERE Employees.Employee.PositionTypeId = 1
      ORDER BY Employees.Employee.EmployeeName		 	    
   		        
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END




GO
