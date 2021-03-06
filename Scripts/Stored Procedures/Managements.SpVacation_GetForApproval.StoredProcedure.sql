USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_GetForApproval]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2011
-- Description:	Get Vacations by Department id 
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_GetForApproval]
(
@EmployeeManagerId INT=NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
   
    BEGIN TRY
          
		   SELECT V.VacationId, 
				  V.EmployeeId,  
			      V.TypeId,  
				  --V.EffectiveDate, 
				  --V.DateExpire,  
				  --V.DateOfReturn,
				   v.Note,
				   VT.TypeName, 
				   E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS EmployeeName ,
				    D.DepartmentName,
			 	   dbo.FnUmAlquraYMD(v.EffectiveDate) AS EffectiveDate,
	               dbo.FnUmAlquraYMD(v.DateExpire) AS DateExpire, 
	               dbo.FnUmAlquraYMD(v.DateOfReturn) AS DateOfReturn, 
				  DATEDIFF(dd, V.EffectiveDate, V.DateExpire) + 1 AS Period
			 FROM Managements.Vacation V INNER JOIN Managements.VacationType  VT
			   ON V.TypeId = VT.TypeId
				  INNER JOIN Employees.Employee E
			   ON V.EmployeeId = E.EmployeeId
				  INNER JOIN Employees.Department D
			   ON E.DepartmentId = D.DepartmentId  
			 WHERE V.IsApproved=0 AND V.IsRejected=0
			 AND (E.ManagerId=@EmployeeManagerId  OR E.ManagerId IN(SELECT AuthourizorID 
			                                                                           FROM [Managements].EmployeeAuthorization 
																					   WHERE AuthorizedID=@EmployeeManagerId) OR @EmployeeManagerId=5)
				  
	    
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END










GO
