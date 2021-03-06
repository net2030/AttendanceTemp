USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpApproval_GetPendingsCount]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Vacations 
-- =============================================
CREATE PROCEDURE [Managements].[SpApproval_GetPendingsCount]
(
 @EmployeeId                      int = NULL,

 @VacationsCount                  int = 0 OUTPUT,
 @WorkExceptionsCount     int = 0 OUTPUT,
 @GatepassCount                   int = 0 OUTPUT,
 @VacationAltEmployeeCount        int = 0 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
	DECLARE @RoleId int
	DECLARE @IsMasterApprover bit=0

	SELECT @RoleId=RoleId FROM Employees.Employee WHERE EmployeeId=@EmployeeID
	SELECT @IsMasterApprover =IsMasterApprover FROM [Security].[Role] WHERE RoleId=@RoleId 


    BEGIN TRY

	


	 IF @IsMasterApprover<>1
	 BEGIN
	   SELECT V.VacationId
	   FROM [Managements].vueVacationApprovalPending  V
	   WHERE V.ApproverTypeId=3 AND V.IsApproved=0 AND V.IsRejected=0
			 AND (V.ManagerId=@EmployeeID  OR V.ManagerId IN(SELECT AuthourizorID FROM Employees.EmployeeAuthorization 
																					   WHERE AuthorizedID=@EmployeeID))
        
		UNION ALL
		SELECT V.VacationId
		FROM [Managements].vueVacationApprovalPending  V
	    WHERE  V.IsApproved=0 AND V.IsRejected=0
		       AND V.ApproverTypeId=@RoleId 
			   AND V.ApproverTypeId<>3
       END
	   ELSE
	   SELECT V.VacationId
	   FROM [Managements].vueVacationApprovalPending  V
       WHERE  V.IsApproved=0 AND V.IsRejected=0
		SET @VacationsCount=@@ROWCOUNT
		print @@ROWCOUNT

	
	 IF @IsMasterApprover<>1
	 BEGIN
	   SELECT V.WorkExceptionId
	   FROM [Managements].vueWorkExceptionApprovalPending  V
	   WHERE V.ApproverTypeId=3 AND V.IsApproved=0 AND V.IsRejected=0
			 AND (V.ManagerId=@EmployeeID  OR V.ManagerId IN(SELECT AuthourizorID FROM Employees.EmployeeAuthorization 
																					   WHERE AuthorizedID=@EmployeeID))
		UNION ALL
		SELECT V.WorkExceptionId
		FROM [Managements].vueWorkExceptionApprovalPending  V
	    WHERE  V.IsApproved=0 AND V.IsRejected=0
		       AND V.ApproverTypeId=@RoleId 
			   AND V.ApproverTypeId<>3
    END
	ELSE
	   SELECT V.WorkExceptionId
	   FROM [Managements].vueWorkExceptionApprovalPending  V
       WHERE  V.IsApproved=0 AND V.IsRejected=0

	   SET @WorkExceptionsCount=@@ROWCOUNT
	   print @@ROWCOUNT


	



	IF @IsMasterApprover <>1
	BEGIN
		SELECT G.GatepassId
		FROM [Managements].vueGatepassApprovalPending  G
		WHERE G.ApproverTypeId=3 AND G.IsApproved=0 AND G.IsRejected=0
				 AND (G.ManagerId=@EmployeeID  OR G.ManagerId IN(SELECT AuthourizorID FROM Employees.EmployeeAuthorization 
																						   WHERE AuthorizedID=@EmployeeID))
		UNION ALL
		SELECT G.[GatepassId]
		FROM [Managements].vueGatepassApprovalPending  G
		WHERE  G.IsApproved=0 AND G.IsRejected=0
				   AND G.ApproverTypeId=@RoleId 
				   AND G.ApproverTypeId<>3
      END
	  ELSE
	    SELECT G.GatepassId
		FROM [Managements].vueGatepassApprovalPending  G
		WHERE  G.IsApproved=0 AND G.IsRejected=0

		SET @GatepassCount=@@ROWCOUNT
		print @@ROWCOUNT



	  SELECT VacationId
	  FROM Managements.Vacation
	  WHERE AltEmployeeId=@EmployeeId AND EmployeeId<>AltEmployeeId
    
	  SET @VacationAltEmployeeCount=@@ROWCOUNT
      print @@ROWCOUNT


    RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
