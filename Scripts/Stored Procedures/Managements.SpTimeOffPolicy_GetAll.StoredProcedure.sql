USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffPolicy_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Schedule Day
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffPolicy_GetAll]

WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    BEGIN TRY
		SELECT TimeOffPolicyId,
			   TimeOffPolicyName,
			    dbo.FnUmAlquraYMD(EffectiveDate) AS EffectiveDate, 
			   [AddedUserAccountId],
			   [UpdatedUserAccountId],
			   [AddedDate],
			   [UpdatedDate],
			   [VersionNo]      
	      FROM [Managements].TimeOffPolicy
	     
	  RETURN     
    END TRY
    BEGIN CATCH
       EXECUTE [Common].[SpCommon_LogError];
       RETURN    
    END CATCH
END







GO
