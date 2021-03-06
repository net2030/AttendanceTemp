USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepass_GetForApproval]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get gatepass by id
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepass_GetForApproval]
(
 @EmployeeID       int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY	            
		SELECT G.[GatepassId]
			  ,E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS EmployeeName 
			  ,G.[IsApproved]
			  ,GT.[GatepassTypeName]
			  --,G.[GatepassBegDate]
			  --,G.[GatepassEndDate]
			  --,G.[GatepassBegTime]
			 ,Convert(char,dbo.FnUmAlquraYMD(G.GatepassBegDate)) + '-- ' + SUBSTRING(CAST(G.[GatepassBegTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(G.[GatepassBegTime] AS NVARCHAR(30)),18 ,2) AS GatepassBegTime
			  --,G.[GatepassEndTime]
			  ,Convert(char,dbo.FnUmAlquraYMD(G.GatepassBegDate)) + ' --' + SUBSTRING(CAST(G.[GatepassEndTime] AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(G.[GatepassEndTime] AS NVARCHAR(30)),18 ,2) AS GatepassEndTime
			  ,G.[RequestedDate]
			  ,G.[Notes],
			   DATEDIFF(MINUTE, G.[GatepassBegTime], G.[GatepassEndTime])  AS Period
		  FROM [Managements].[Gatepass]  G
		  INNER JOIN Employees.Employee E ON G.EmployeeId=E.EmployeeId
		  INNER JOIN Managements.GatepassType GT ON G.GatepassTypeId=GT.GatepassTypeId
	 WHERE  G.IsApproved=0 AND G.IsRejected=0
			 AND ( @EmployeeID=5 OR E.ManagerId=@EmployeeID  OR E.ManagerId IN(SELECT AuthourizorID 
			                                                                           FROM Employees.EmployeeAuthorization 
																					   WHERE AuthorizedID=@EmployeeID))
     ORDER BY G.AddedDate desc
       	        				       
		
		RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END









GO
