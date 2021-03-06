USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepass_GetDepartmentGatepassBalance]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get department Gatepass
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepass_GetDepartmentGatepassBalance]
(
 @DepartmentId int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;

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

	    SELECT GB.GatepassBalanceId, 
			   GB.EmployeeId, 
			   Employees.Employee.FirstName + ' ' + Employees.Employee.MiddleName + ' ' + Employees.Employee.LastName AS Employeename , 
			   Employees.Employee.BadgeNo AS EmployeeCode,
			   GB.Earned, 
			   GB.Used, 
			   GB.Available, 
			   CTE.DepartmentName
			
			FROM Managements.GatepassBalance GB INNER JOIN Employees.Employee 
            ON GB.EmployeeId = Employees.Employee.EmployeeId          
               INNER JOIN CTE 
            ON Employees.Employee.DepartmentId = CTE.DepartmentId
       
	  
		  		  		    
	   RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
