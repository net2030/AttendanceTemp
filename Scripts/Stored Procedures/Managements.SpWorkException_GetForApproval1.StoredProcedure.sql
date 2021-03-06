USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkException_GetForApproval1]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get gatepass by id
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkException_GetForApproval1]
(
 @EmployeeManagerId       int = NULL,
 @Option           int=1,
 @Lang           char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RoleId int
	DECLARE @IsMasterApprover bit=0

	SELECT @RoleId=RoleId FROM Employees.Employee WHERE EmployeeId=@EmployeeManagerId
	SELECT @IsMasterApprover =IsMasterApprover FROM [Security].[Role] WHERE RoleId=@RoleId 

    BEGIN TRY	
	     
		 IF @IsMasterApprover=1
		 BEGIN
		 SELECT WE.WorkExceptionId
		      ,WE.EmployeeId
			  ,WE.FullName
			  ,CASE WHEn @Lang = 'en' THEN WE.WorkExceptionTypeNameEN ELSE WE.WorkExceptionTypeName END AS WorkExceptionTypeName
			  ,Convert(nvarchar(10),WE.WorkExceptionBegDate,105) AS WorkExceptionBegDate
			  ,Convert(nvarchar(10),WE.WorkExceptionEndDate,105) AS WorkExceptionEndDate
			  ,WE.[Approved]
			  ,WE.Rejected
			  ,WE.ApprovalPathId
			  ,WE.ApproverTypeId
			  ,WE.ApprovalPathSequence
			  ,WE.MaxApprovalPathSequence
			  ,WE.ApprovalPolicyId
			  ,WE.ApprovalPathId
			  ,WE.ManagerId
			  ,WE.[Notes]
		  FROM [Managements].vueWorkExceptionApprovalPending  WE
	      WHERE  WE.IsApproved=0 AND WE.IsRejected=0
		  
		  RETURN
		  END	
		        
	    IF @Option=1 --Employee Manager (direct manager)  
		SELECT WE.WorkExceptionId
		      ,WE.EmployeeId
			  ,WE.FullName
			  ,CASE WHEn @Lang = 'en' THEN WE.WorkExceptionTypeNameEN ELSE WE.WorkExceptionTypeName END AS WorkExceptionTypeName
			  ,Convert(nvarchar(10),WE.WorkExceptionBegDate,105) AS WorkExceptionBegDate
			  ,Convert(nvarchar(10),WE.WorkExceptionEndDate,105) AS WorkExceptionEndDate
			  ,WE.[Approved]
			  ,WE.Rejected
			  ,WE.ApprovalPathId
			  ,WE.ApproverTypeId
			  ,WE.ApprovalPathSequence
			  ,WE.MaxApprovalPathSequence
			  ,WE.ApprovalPolicyId
			  ,WE.ApprovalPathId
			  ,WE.ManagerId
			  ,WE.[Notes]
		  FROM [Managements].vueWorkExceptionApprovalPending  WE
	      WHERE WE.ApproverTypeId=3 AND WE.IsApproved=0 AND WE.IsRejected=0
			 AND (WE.ManagerId=@EmployeeManagerId  OR WE.ManagerId IN(SELECT AuthourizorID FROM Employees.EmployeeAuthorization 
																					   WHERE AuthorizedID=@EmployeeManagerId))
        
		UNION ALL

		SELECT WE.WorkExceptionId
		      ,WE.EmployeeId
			  ,WE.FullName
			  ,CASE WHEn @Lang = 'en' THEN WE.WorkExceptionTypeNameEN ELSE WE.WorkExceptionTypeName END AS WorkExceptionTypeName
			  ,Convert(nvarchar(10),WE.WorkExceptionBegDate,105) AS WorkExceptionBegDate
			  ,Convert(nvarchar(10),WE.WorkExceptionEndDate,105) AS WorkExceptionEndDate
			  ,WE.[Approved]
			  ,WE.Rejected
			  ,WE.ApprovalPathId
			  ,WE.ApproverTypeId
			  ,WE.ApprovalPathSequence
			  ,WE.MaxApprovalPathSequence
			  ,WE.ApprovalPolicyId
			  ,WE.ApprovalPathId
			  ,WE.ManagerId
			  ,WE.[Notes]
		  FROM [Managements].vueWorkExceptionApprovalPending  WE
	      WHERE  WE.IsApproved=0 AND WE.IsRejected=0
		       AND WE.ApproverTypeId=@RoleId 
			   AND WE.ApproverTypeId<>3

       	        				       
		
		RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END








GO
