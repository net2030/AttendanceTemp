USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkException_GetForApproval]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2011
-- Description:	Get Vacations by Department id 
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkException_GetForApproval]
(
@EmployeeManagerId INT=NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
   
    BEGIN TRY
          
		  SELECT WE.WorkExceptionId,
		 E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS EmployeeName ,
		 AD.DepartmentName,
		 WET.WorkExceptionTypeName,
		Convert(nvarchar(10),  WorkExceptionBegDate,105) AS WorkExceptionBegDate,
		Convert(nvarchar(10),   WE.WorkExceptionEndDate,105) AS WorkExceptionEndDate,
		 DATEDIFF(dd, WE.WorkExceptionBegDate, WE.WorkExceptionEndDate) + 1 AS Period,
		 WE.Notes
         FROM Managements.WorkException WE
         JOIN Managements.WorkExceptionType WET ON WE.WorkExceptionTypeId=WET.WorkExceptionTypeId
		 JOIN Employees.Employee E ON WE.EmployeeId=E.EmployeeId
		 JOIN Employees.Department AD ON E.DepartmentId=AD.DepartmentId
		 WHERE WE.IsApproved=0 AND WE.IsRejected=0
			 AND (E.ManagerId=@EmployeeManagerId  OR E.ManagerId IN(SELECT AuthourizorID 
			                                                                           FROM [Managements].EmployeeAuthorization 
																					   WHERE AuthorizedID=@EmployeeManagerId) OR @EmployeeManagerId=5)
			
				  
	    
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
       
        RETURN    
    END CATCH
END











GO
