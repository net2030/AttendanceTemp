USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_GetFingerPrintNotRegistered]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Employees who didn't  
--              Register thier Finger Printt
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_GetFingerPrintNotRegistered]
(
 @DepartmentId  int = NULL,
 @Op            int =0,
 @Lang         char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected  int
        
    -- Initializations              
    SET @RowEffected = 0
    
     
    BEGIN TRY

    
	 BEGIN
	    WITH CTE(DepartmentId, DepartmentName, ParentId)
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
		     Employees.Employee.FirstName + ' ' +  Employees.Employee.MiddleName + ' ' +  Employees.Employee.LastName AS EmployeeName ,
			 Employees.Employee.BadgeNo, 
			 CASE WHEN @Lang='en' THEN  Employees.Department.DepartmentNameEn ELSE  Employees.Department.DepartmentName END AS  DepartmentName, 
			 Employees.PositionType.PositionTypeName, 
			 Employees.Employee.JobTitle, 
			 Employees.Position.PositionName, 
			 Employees.Nationality.NationalityName,
			 Common.FnToUmAlqura(Employees.Employee.HireDate, 2) AS UmHireDate,
			 CASE  Employees.Employee.IsFingerRegistered WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END as FingerRegistered
		FROM Employees.Employee INNER JOIN Employees.Department 
		  ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId 
			 
			 INNER JOIN Employees.Position 
		  ON Employees.Employee.PositionId = Employees.Position.PositionId 
			 INNER JOIN Employees.PositionType 
		  ON Employees.Employee.PositionTypeId = Employees.PositionType.PositionTypeId 
			 INNER JOIN Employees.Nationality 
		  ON Employees.Employee.NationalityId = Employees.Nationality.NationalityId   		     		      
		     INNER JOIN CTE
		  ON Employees.Employee.DepartmentId = CTE.DepartmentId    

		  WHERE (Employees.Employee.IsFingerRegistered=1 AND @op=1) 
		  OR (Employees.Employee.IsFingerRegistered=0 AND @Op=2)
		  OR (@op=0)
      
      ORDER BY Employees.Employee.EmployeeId
	 END
	 		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
