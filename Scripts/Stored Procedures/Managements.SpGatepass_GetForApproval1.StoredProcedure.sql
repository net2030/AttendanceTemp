USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepass_GetForApproval1]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get gatepass by id
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepass_GetForApproval1]
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
		 	SELECT G.[GatepassId]
		      ,G.EmployeeId
			  ,G.FullName
			   ,CASE WHEn @Lang = 'en' THEN G.GatepassTypeNameEN ELSE G.GatepassTypeName END AS GatepassTypeName
			  ,Convert(nvarchar(10),G.[GatepassBegDate],105) + ' ' + SUBSTRING(CAST(G.[GatepassBegTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(G.[GatepassBegTime] AS NVARCHAR(30)),18 ,2) AS GatepassBegTime
			  ,Convert(nvarchar(10),G.[GatepassEndDate],105) + ' ' + SUBSTRING(CAST(G.[GatepassEndTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(G.[GatepassEndTime] AS NVARCHAR(30)),18 ,2) AS GatepassEndTime
			  ,G.[Approved]
			  ,G.Rejected
			  ,G.ApprovalPathId
			  ,G.ApproverTypeId
			  ,G.ApprovalPathSequence
			  ,G.MaxApprovalPathSequence
			  ,G.ApprovalPolicyId
			  ,G.ApprovalPathId
			  ,G.ManagerId
			  ,G.[Notes]
		  FROM [Managements].vueGatepassApprovalPending  G
	      WHERE G.IsApproved=0 AND G.IsRejected=0
		  RETURN
		 END
		         
	    IF @Option=1 --Employee Manager (direct manager)  
		SELECT G.[GatepassId]
		      ,G.EmployeeId
			  ,G.FullName
			  ,CASE WHEn @Lang = 'en' THEN G.GatepassTypeNameEN ELSE G.GatepassTypeName END AS GatepassTypeName
			  ,Convert(nvarchar(10),G.[GatepassBegDate],105) + ' ' + SUBSTRING(CAST(G.[GatepassBegTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(G.[GatepassBegTime] AS NVARCHAR(30)),18 ,2) AS GatepassBegTime
			  ,Convert(nvarchar(10),G.[GatepassEndDate],105) + ' ' + SUBSTRING(CAST(G.[GatepassEndTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(G.[GatepassEndTime] AS NVARCHAR(30)),18 ,2) AS GatepassEndTime
			  ,G.[Approved]
			  ,G.Rejected
			  ,G.ApprovalPathId
			  ,G.ApproverTypeId
			  ,G.ApprovalPathSequence
			  ,G.MaxApprovalPathSequence
			  ,G.ApprovalPolicyId
			  ,G.ApprovalPathId
			  ,G.ManagerId
			  ,G.[Notes]
		  FROM [Managements].vueGatepassApprovalPending  G
	      WHERE G.ApproverTypeId=3 AND G.IsApproved=0 AND G.IsRejected=0
			 AND (G.ManagerId=@EmployeeID  OR G.ManagerId IN(SELECT AuthourizorID FROM Employees.EmployeeAuthorization 
																					   WHERE AuthorizedID=@EmployeeID))
        
		UNION ALL
		SELECT G.[GatepassId]
		      ,G.EmployeeId
			  ,G.FullName
			   ,CASE WHEn @Lang = 'en' THEN G.GatepassTypeNameEN ELSE G.GatepassTypeName END AS GatepassTypeName
			  ,Convert(nvarchar(10),G.[GatepassBegDate],105) + ' ' + SUBSTRING(CAST(G.[GatepassBegTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(G.[GatepassBegTime] AS NVARCHAR(30)),18 ,2) AS GatepassBegTime
			  ,Convert(nvarchar(10),G.[GatepassEndDate],105) + ' ' + SUBSTRING(CAST(G.[GatepassEndTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(G.[GatepassEndTime] AS NVARCHAR(30)),18 ,2) AS GatepassEndTime
			  ,G.[Approved]
			  ,G.Rejected
			  ,G.ApprovalPathId
			  ,G.ApproverTypeId
			  ,G.ApprovalPathSequence
			  ,G.MaxApprovalPathSequence
			  ,G.ApprovalPolicyId
			  ,G.ApprovalPathId
			  ,G.ManagerId
			  ,G.[Notes]
		  FROM [Managements].vueGatepassApprovalPending  G
	      WHERE  G.IsApproved=0 AND G.IsRejected=0
		       AND G.ApproverTypeId=@RoleId 
			   AND G.ApproverTypeId<>3

       	        				       
		
		RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END








GO
