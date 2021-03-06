USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpApprovalPolicy_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Schedule Day
-- =============================================
CREATE PROCEDURE [Managements].[SpApprovalPolicy_GetAll]

WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    BEGIN TRY
		SELECT ApprovalPolicyId,
			   ApprovalPolicyName,
			   CASE TimeOffApprovalType WHEN 1 THEN 'الإجازات' WHEN 2 THEN 'الإستثناءات' WHEN 3 THEN 'الإستئذان' ELSE 'الجميع' END AS TimeOffApprovalType ,
			   [AddedUserAccountId],
			   [UpdatedUserAccountId],
			   [AddedDate],
			   [UpdateDate],
			   [VersionNo]      
	      FROM [Managements].ApprovalPolicy
	     
	  RETURN     
    END TRY
    BEGIN CATCH
       EXECUTE [Common].[SpCommon_LogError];
       RETURN    
    END CATCH
END







GO
