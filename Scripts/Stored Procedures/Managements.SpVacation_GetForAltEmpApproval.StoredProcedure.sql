USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_GetForAltEmpApproval]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2011
-- Description:	Get Vacations by Department id 
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_GetForAltEmpApproval]
(
@AltEmployeeId INT=NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
   
    BEGIN TRY
          
		   SELECT V.VacationId, 
				  V.EmployeeId,  
			      V.TypeId,  
				   v.Note,
				   VT.TypeName, 
				   E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS EmployeeName ,
				    D.DepartmentName,
			 	   dbo.FnUmAlquraYMD(v.EffectiveDate) AS EffectiveDate,
	               dbo.FnUmAlquraYMD(v.DateExpire) AS DateExpire, 
				  DATEDIFF(dd, V.EffectiveDate, V.DateExpire) + 1 AS Period
			 FROM Managements.Vacation V INNER JOIN Managements.VacationType  VT
			   ON V.TypeId = VT.TypeId
				  INNER JOIN Employees.Employee E
			   ON V.EmployeeId = E.EmployeeId
				  INNER JOIN Employees.Department D
			   ON E.DepartmentId = D.DepartmentId  
			 WHERE V.IsAltEmpApproved=0 AND V.IsAltEmpRejected=0
			 AND V.AltEmployeeId=@AltEmployeeId  
				  
	    
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END











GO
