USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployeeAuthorization]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	Insert a new gatepass
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployeeAuthorization]
(
 @Authorizor   int = NULL, 
 @Authorized   int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations

    DECLARE @RoleId as int
    DECLARE @AuthorizedOldRoleId as int
	DECLARE @DepartmentScope as int
	DECLARE @DepartmentScopeOld as int

    SELECT @RoleId=RoleId
    FROM Employees.Employee 
    WHERE EmployeeId=@Authorizor
   

   IF @RoleId=2 --The Authorizor Is User
   RETURN

    SELECT @AuthorizedOldRoleId=RoleId
	      
    FROM Employees.Employee 
    WHERE EmployeeId=@Authorized

    BEGIN TRY	            
	
	INSERT INTO EmployeeAuthorization(AuthourizorID,AuthorizedID,AuthorizedOldRoleId)
	VALUES(@Authorizor,@Authorized,@AuthorizedOldRoleId)

	Update Employees.Employee 
	       SET RoleId=@RoleId 
		   Where EmployeeId=@Authorized
	

	INSERT INTO Employees.EmployeeDepartmentLink(DepartmentId,EmployeeId,IsAuthorization,Authorizor)
	SELECT DepartmentId,@Authorized,1,@Authorizor FROM Employees.EmployeeDepartmentLink
	WHERE EmployeeId =@Authorizor 
	AND DepartmentId Not in (SELECT DepartmentId FROM Employees.EmployeeDepartmentLink WHERE EmployeeId=@Authorized )

    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
  
        RETURN    
    END CATCH
END









GO
