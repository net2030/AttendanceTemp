USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkException1_GetForApproval]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2011
-- Description:	Get Vacations by Department id 
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkException1_GetForApproval]
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
		 WE.DueAmount,
		 WE.IsApproved,
		 WE.IsRejected,
		 WE.IsPaid,
		 WE.AmountPaid,
		 WET.WorkExceptionTypeName,
		 WE.WorkExceptionBegDate,
		  WE.WorkExceptionEndDate,
		--Convert(nvarchar(10),  WE.WorkExceptionBegDate,105) AS WorkExceptionBegDate,
		--Convert(nvarchar(10),   WE.WorkExceptionEndDate,105) AS WorkExceptionEndDate,
		 DATEDIFF(dd, WE.WorkExceptionBegDate, WE.WorkExceptionEndDate) + 1 AS Period,
		 WE.Notes,
		 WE.ApprovalNotes

         FROM Managements.WorkException1 WE
         JOIN Managements.WorkExceptionType WET ON WE.WorkExceptionTypeId=WET.WorkExceptionTypeId
		 JOIN Employees.Employee E ON WE.EmployeeId=E.EmployeeId
		 JOIN Employees.Department AD ON E.DepartmentId=AD.DepartmentId
		 --WHERE @EmployeeManagerId in (2,5,6)
		
			
				  
	    
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
       EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END












GO
