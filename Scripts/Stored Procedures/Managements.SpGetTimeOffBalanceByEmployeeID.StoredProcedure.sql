USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGetTimeOffBalanceByEmployeeID]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Get Leave Policy Employees
-- =============================================
CREATE PROCEDURE [Managements].[SpGetTimeOffBalanceByEmployeeID]
(
 @EmployeeID   int = NULL,
 @TimeOffType  int =NULL,
 @Lang           char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
   
	
    BEGIN TRY
		
    IF @TimeOffType=1

		SELECT TOB.TimeOffBalanceId,
		   TOB.EmployeeId,
		   E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS EmployeeName,
		   TOB.TimeOffPolicyDetailId,
		   TOB.Earned,
		   TOB.Used, 
		   TOB.Available, 
		   TOB.CarryForward, 
		   TOB.LastEarnedDate, 
		   TOB.LastResetDate, 
		   TOPD.IsCarryForward,
		   CASE WHEn @Lang = 'en' THEN Managements.VacationType.[TypeNameEN] ELSE Managements.VacationType.[TypeName] END AS TypeName,
		   Periods.PeriodName,
		   GP.TypeId,
		   TOB.VersionNo
	   FROM Employees.Employee E INNER JOIN Managements.TimeOffBalance TOB 
			 ON E.EmployeeId = TOB.EmployeeId
			 INNER JOIN Managements.TimeOffPolicyDetail TOPD
			 ON TOB.TimeOffPolicyDetailId = TOPD.TimeOffPolicyDetailId 
			 INNER JOIN   Managements.VacationType
			 ON TOPD.GatepassTypeId = Managements.VacationType.TypeId
			 INNER JOIN Periods ON Periods.PeriodId= TOPD.EarnPeriodId
			 INNER JOIN Managements.TimeOffPolicy GP ON GP.TimeOffPolicyId=TOPD.TimeOffPolicyId
       WHERE E.EmployeeId=@EmployeeID AND  GP.TypeId=1

	   ELSE
	   IF @TimeOffType=2

		SELECT TOB.TimeOffBalanceId,
		   TOB.EmployeeId,
		   E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS EmployeeName,
		   TOB.TimeOffPolicyDetailId,
		   TOB.Earned,
		   TOB.Used, 
		   Convert(int,TOB.Available) AS Available, 
		   TOB.CarryForward, 
		   TOB.LastEarnedDate, 
		   TOB.LastResetDate, 
		   Managements.TimeOffPolicyDetail.IsCarryForward,
		   Managements.WorkExceptionType.WorkExceptionTypeName AS TypeName,
		   CASE WHEn @Lang = 'en' THEN Managements.WorkExceptionType.WorkExceptionTypeNameEN ELSE Managements.WorkExceptionType.WorkExceptionTypeName END AS TypeName,
		   Periods.PeriodName,
		    GP.TypeId,
		   TOB.VersionNo
	   FROM Employees.Employee E INNER JOIN Managements.TimeOffBalance TOB 
			 ON E.EmployeeId = TOB.EmployeeId
			 INNER JOIN Managements.TimeOffPolicyDetail 
			 ON TOB.TimeOffPolicyDetailId = Managements.TimeOffPolicyDetail.TimeOffPolicyDetailId 
			 INNER JOIN   Managements.WorkExceptionType
			 ON Managements.TimeOffPolicyDetail.GatepassTypeId = Managements.WorkExceptionType.WorkExceptionTypeId
             INNER JOIN Periods ON Periods.PeriodId= Managements.TimeOffPolicyDetail.EarnPeriodId
			 INNER JOIN Managements.TimeOffPolicy GP ON GP.TimeOffPolicyId=Managements.TimeOffPolicyDetail.TimeOffPolicyId
	   WHERE E.EmployeeId=@EmployeeID AND  GP.TypeId=2

	   ELSE
	   IF @TimeOffType=3

		SELECT TOB.TimeOffBalanceId,
		   TOB.EmployeeId,
		   E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS EmployeeName,
		   TOB.TimeOffPolicyDetailId,
		   TOB.Earned,
		   TOB.Used, 
		   Convert(int,TOB.Available) AS Available, 
		   TOB.CarryForward, 
		   TOB.LastEarnedDate, 
		   TOB.LastResetDate, 
		   Managements.TimeOffPolicyDetail.IsCarryForward,
		   CASE WHEn @Lang = 'en' THEN Managements.GatepassType.GatepassTypeNameEN ELSE Managements.GatepassType.GatepassTypeName END AS TypeName,
		   Periods.PeriodName,
		     GP.TypeId,
		   TOB.VersionNo
	   FROM Employees.Employee E INNER JOIN Managements.TimeOffBalance TOB 
			 ON E.EmployeeId = TOB.EmployeeId
			 INNER JOIN Managements.TimeOffPolicyDetail 
			 ON TOB.TimeOffPolicyDetailId = Managements.TimeOffPolicyDetail.TimeOffPolicyDetailId 
			 INNER JOIN   Managements.GatepassType
			 ON Managements.TimeOffPolicyDetail.GatepassTypeId = Managements.GatepassType.GatepassTypeId
              INNER JOIN Periods ON Periods.PeriodId= Managements.TimeOffPolicyDetail.EarnPeriodId
			  INNER JOIN Managements.TimeOffPolicy GP ON GP.TimeOffPolicyId=Managements.TimeOffPolicyDetail.TimeOffPolicyId
	   WHERE E.EmployeeId=@EmployeeID AND  GP.TypeId=3
	
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
