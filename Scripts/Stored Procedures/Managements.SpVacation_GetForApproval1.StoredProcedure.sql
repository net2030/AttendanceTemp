USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_GetForApproval1]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get gatepass by id
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_GetForApproval1]
(
 @EmployeeID       int = NULL,
 @Option           int=1,
 @Lang           char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RoleId int
	DECLARE @IsMasterApprover bit=0

	SELECT @RoleId=RoleId FROM Employees.Employee WHERE EmployeeId=@EmployeeID
	SELECT @IsMasterApprover =IsMasterApprover FROM [Security].[Role] WHERE RoleId=@RoleId 

    BEGIN TRY	

	     IF @IsMasterApprover=1
		 BEGIN
		 	SELECT V.VacationId
		      ,V.EmployeeId
			  ,V.FullName

			  ,CASE  
				  WHEN  v.AltEmployeeId = v.EmployeeId THEN ''
				  ELSE
				  V.AltEmployeeName END AS AltEmployeeName 

			  ,CASE WHEn @Lang = 'en' THEN V.[TypeNameEN] ELSE V.[TypeName] END AS TypeName
			  ,Convert(nvarchar(10),V.EffectiveDate,105) AS EffectiveDate
			  ,Convert(nvarchar(10),V.DateExpire,105) AS DateExpire
			  ,V.[Approved]
			  ,V.Rejected
			  ,V.ApprovalPathId
			  ,V.ApproverTypeId
			  ,V.ApprovalPathSequence
			  ,V.MaxApprovalPathSequence
			  ,V.ApprovalPolicyId
			  ,V.ApprovalPathId
			  ,V.ManagerId
			  ,V.[Note]
		  FROM [Managements].vueVacationApprovalPending  V
		   
	      WHERE  V.IsApproved=0 AND V.IsRejected=0

		 RETURN
		 END
	            
	    IF @Option=1 --Employee Manager (direct manager)  
		SELECT V.VacationId
		      ,V.EmployeeId
			  ,V.FullName

			    ,CASE  
				  WHEN  v.AltEmployeeId = v.EmployeeId THEN ''
				  ELSE
				  V.AltEmployeeName END AS AltEmployeeName 

			   ,CASE WHEn @Lang = 'en' THEN V.[TypeNameEN] ELSE V.[TypeName] END AS TypeName
			  ,Convert(nvarchar(10),V.EffectiveDate,105) AS EffectiveDate
			  ,Convert(nvarchar(10),V.DateExpire,105) AS DateExpire
			  ,V.[Approved]
			  ,V.Rejected
			  ,V.ApprovalPathId
			  ,V.ApproverTypeId
			  ,V.ApprovalPathSequence
			  ,V.MaxApprovalPathSequence
			  ,V.ApprovalPolicyId
			  ,V.ApprovalPathId
			  ,V.ManagerId
			  ,V.[Note]
		  FROM [Managements].vueVacationApprovalPending  V
	      WHERE V.ApproverTypeId=3 AND V.IsApproved=0 AND V.IsRejected=0
			 AND (V.ManagerId=@EmployeeID  OR V.ManagerId IN(SELECT AuthourizorID FROM Employees.EmployeeAuthorization 
																					   WHERE AuthorizedID=@EmployeeID))
        
		UNION ALL
		SELECT V.VacationId
		      ,V.EmployeeId
			  ,V.FullName
			
			 ,CASE  
				  WHEN  v.AltEmployeeId = v.EmployeeId THEN ''
				  ELSE
				  V.AltEmployeeName END AS AltEmployeeName 

			   ,CASE WHEn @Lang = 'en' THEN V.[TypeNameEN] ELSE V.[TypeName] END AS TypeName
			  ,Convert(nvarchar(10),V.EffectiveDate,105) AS EffectiveDate
			  ,Convert(nvarchar(10),V.DateExpire,105) AS DateExpire
			  ,V.[Approved]
			  ,V.Rejected
			  ,V.ApprovalPathId
			  ,V.ApproverTypeId
			  ,V.ApprovalPathSequence
			  ,V.MaxApprovalPathSequence
			  ,V.ApprovalPolicyId
			  ,V.ApprovalPathId
			  ,V.ManagerId
			  ,V.[Note]
		  FROM [Managements].vueVacationApprovalPending  V
	      WHERE  V.IsApproved=0 AND V.IsRejected=0
		       AND V.ApproverTypeId=@RoleId 
			   AND V.ApproverTypeId<>3

       	        				       
		
		RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END








GO
