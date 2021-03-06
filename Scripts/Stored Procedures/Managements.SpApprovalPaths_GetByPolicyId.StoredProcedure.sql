USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpApprovalPaths_GetByPolicyId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Schedule Day
-- =============================================
CREATE PROCEDURE [Managements].[SpApprovalPaths_GetByPolicyId]
(
@ApprovalPolicyId int=NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     

    BEGIN TRY



	 SELECT  AP.[ApprovalPathId]
      ,AP.[ApprovalPolicyId]
      ,AP.[ApproverTypeId]
	  ,R.RoleName AS ApproverTypeName
      ,AP.[ExternalUserId]
      ,AP.[EmployeeId]
      ,AP.[ApprovalPathSequence]
      ,AP.[MasterApprovalPathId]
	  ,AP.VersionNo
	  
    FROM [Managements].[ApprovalPath] AP
	JOIN Security.Role R ON AP.ApproverTypeId=R.RoleId
	WHERE AP.ApprovalPolicyId=@ApprovalPolicyId
		           
	     
	

	  RETURN     
    END TRY
    BEGIN CATCH
       EXECUTE [Common].[SpCommon_LogError];
       RETURN    
    END CATCH
END







GO
