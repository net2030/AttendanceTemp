USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffPolicyDetail_GetById]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Schedule Day
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffPolicyDetail_GetById]
(
@TimeOffPolicyDetailId int=NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    BEGIN TRY
		            SELECT [TimeOffPolicyDetailId]
						  ,[TimeOffPolicyId]
						  ,[GatepassTypeId]
						  ,[EarnPeriodId]
						  ,[ResetToZeroPeriodId]
						  ,[InitialSetToMinutes]
						  ,[ResetToMinutes]
						  ,[EarnMinutes]
						  ,EffectiveDate
						  ,IsCarryForward
						  ,MinValue
						  ,MaxValue 
						  ,[AddedUserAccountId]
						  ,[UpdatedUserAccountId]
						  ,[AddedDate]
						  ,[UpdatedDate]
					  FROM [Managements].[TimeOffPolicyDetail]
					  WHERE TimeOffPolicyDetailId=@TimeOffPolicyDetailId
	     
	  RETURN     
    END TRY
    BEGIN CATCH
       EXECUTE [Common].[SpCommon_LogError];
       RETURN    
    END CATCH
END







GO
