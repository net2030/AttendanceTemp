USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_GetApprovalRecord]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get gatepass by id
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_GetApprovalRecord]
(
 @VacationId       int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
	

    BEGIN TRY	

	   SELECT  0 AS ApprovalPathSequence
			  ,E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS ApprovedBy
			  ,'تمت الموافقة' AS ApprovalStatus
			  ,'' AS Comments
			  ,'الموظف البديل' AS ApprovalPolicyName
			  ,R.RoleName
			  ,dbo.FnUmAlquraYMD(V.AltApprovedOn) AS ApprovedOn
			  from Managements.Vacation V
			  Join Employees.Employee E ON V.AltEmployeeId =E.EmployeeId 
			  JOIN [Security].[Role] R ON  E.RoleId=R.RoleId 
			   WHERE  V.VacationId=@VacationId AND V.EmployeeId <> V.AltEmployeeId 
           UNION ALL
		 	SELECT 
			   P.ApprovalPathSequence
			  ,E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS ApprovedBy
			  ,'تمت الموافقة' AS ApprovalStatus
			  ,VA.Comments
			  ,AP.ApprovalPolicyName
			  ,R.RoleName
			  ,dbo.FnUmAlquraYMD(VA.ApprovedOn) AS ApprovedOn
		  FROM [Managements].VacationApproval  VA
		  JOIN Managements.ApprovalPolicy AP ON VA.ApprovalPolicyId=AP.ApprovalPolicyId
		  JOIN Employees.Employee E ON VA.ApprovedByEmployeeId=E.EmployeeId
		  JOIN Managements.ApprovalPath P ON VA.ApprovalPathId=P.ApprovalPathId
		  JOIN [Security].[Role] R ON  P.ApproverTypeId=R.RoleId
	      WHERE  VA.VacationId=@VacationId

		  ORDER BY ApprovalPathSequence
	
		 
			   


		RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END








GO
