USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffPolicyDetail_GetByPolicyId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Schedule Day
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffPolicyDetail_GetByPolicyId]
(
@TimeOffPolicyId int=NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
	 DECLARE @TypeId INT=0

	 SELECT @TypeId =TypeId FROM Managements.TimeOffPolicy WHERE TimeOffPolicyId=@TimeOffPolicyId

    BEGIN TRY

	IF @TypeId=1
	BEGIN

		            SELECT GPD.[TimeOffPolicyDetailId]
						  ,GPD.[TimeOffPolicyId]
						  ,GPD.[GatepassTypeId]
						  ,VT.TypeName
						  ,GPD.[EarnPeriodId]
						  ,P1.PeriodName AS EarnPeriod
						  ,GPD.[ResetToZeroPeriodId]
						  ,P2.PeriodName AS ResetToZeroPeriod
						  ,GPD.[InitialSetToMinutes]
						  ,GPD.[ResetToMinutes]
						  ,GPD.[EarnMinutes]
						  ,GPD.EffectiveDate
						  ,GPD.IsCarryForward
						  ,GPD.MinValue
						  ,GPD.MaxValue
						  ,Case GPD.IsCarryForward WHEN 0 THEN 'لا' WHEN 1 THEN 'نعم' END AS IsCarryForwardName
						  ,dbo.FnUmAlquraYMD(GPD.EffectiveDate) AS strEffectiveDate
						  ,GPD.[AddedUserAccountId]
						  ,GPD.[UpdatedUserAccountId]
						  ,GPD.[AddedDate]
						  ,GPD.[UpdatedDate]
						  ,GPD.VersionNo
					  FROM [Managements].[TimeOffPolicyDetail] GPD
					  JOIN Periods P1 ON GPD.EarnPeriodId=P1.PeriodId
					  JOIN Periods P2 ON GPD.ResetToZeroPeriodId=P2.PeriodId
					  JOIN Managements.VacationType VT ON GPD.GatepassTypeId=VT.TypeId
					  WHERE TimeOffPolicyId=@TimeOffPolicyId
	     
	  END
	  ELSE IF @TypeId =2
	  BEGIN

		            SELECT GPD.[TimeOffPolicyDetailId]
						  ,GPD.[TimeOffPolicyId]
						  ,GPD.[GatepassTypeId]
						  ,WT.WorkExceptionTypeName AS TypeName
						  ,GPD.[EarnPeriodId]
						  ,P1.PeriodName AS EarnPeriod
						  ,GPD.[ResetToZeroPeriodId]
						  ,P2.PeriodName AS ResetToZeroPeriod
						  ,GPD.[InitialSetToMinutes]
						  ,GPD.[ResetToMinutes]
						  ,GPD.[EarnMinutes]
						  ,GPD.EffectiveDate
						  ,GPD.IsCarryForward
						  ,GPD.MinValue
						  ,GPD.MaxValue
						  ,Case GPD.IsCarryForward WHEN 0 THEN 'لا' WHEN 1 THEN 'نعم' END AS IsCarryForwardName
						  ,dbo.FnUmAlquraYMD(GPD.EffectiveDate) AS strEffectiveDate
						  ,GPD.[AddedUserAccountId]
						  ,GPD.[UpdatedUserAccountId]
						  ,GPD.[AddedDate]
						  ,GPD.[UpdatedDate]
						  ,GPD.VersionNo
					  FROM [Managements].[TimeOffPolicyDetail] GPD
					  JOIN Periods P1 ON GPD.EarnPeriodId=P1.PeriodId
					  JOIN Periods P2 ON GPD.ResetToZeroPeriodId=P2.PeriodId
					  JOIN Managements.WorkExceptionType WT ON GPD.GatepassTypeId=WT.WorkExceptionTypeId
					  WHERE TimeOffPolicyId=@TimeOffPolicyId
	     
	  END
	  ELSE IF @TypeId=3
	  BEGIN

		            SELECT GPD.[TimeOffPolicyDetailId]
						  ,GPD.[TimeOffPolicyId]
						  ,GPD.[GatepassTypeId]
						  ,GT.GatepassTypeName AS TypeName
						  ,GPD.[EarnPeriodId]
						  ,P1.PeriodName AS EarnPeriod
						  ,GPD.[ResetToZeroPeriodId]
						  ,P2.PeriodName AS ResetToZeroPeriod
						  ,GPD.[InitialSetToMinutes]
						  ,GPD.[ResetToMinutes]
						  ,GPD.[EarnMinutes]
						  ,GPD.EffectiveDate
						   ,GPD.IsCarryForward
						   ,GPD.MinValue
						  ,GPD.MaxValue
						  ,Case GPD.IsCarryForward WHEN 0 THEN 'لا' WHEN 1 THEN 'نعم' END AS IsCarryForwardName
						  ,dbo.FnUmAlquraYMD(GPD.EffectiveDate) AS strEffectiveDate
						  ,GPD.[AddedUserAccountId]
						  ,GPD.[UpdatedUserAccountId]
						  ,GPD.[AddedDate]
						  ,GPD.[UpdatedDate]
						  ,GPD.VersionNo
					  FROM [Managements].[TimeOffPolicyDetail] GPD
					  JOIN Periods P1 ON GPD.EarnPeriodId=P1.PeriodId
					  JOIN Periods P2 ON GPD.ResetToZeroPeriodId=P2.PeriodId
					  JOIN Managements.GatepassType GT ON GPD.GatepassTypeId=GT.GatepassTypeId
					  WHERE TimeOffPolicyId=@TimeOffPolicyId
	     
	  END

	  RETURN     
    END TRY
    BEGIN CATCH
       EXECUTE [Common].[SpCommon_LogError];
       RETURN    
    END CATCH
END







GO
