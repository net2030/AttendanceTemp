USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployeeUnAuthorization]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	Insert a new gatepass
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployeeUnAuthorization]
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


    SELECT @RoleId=AuthorizedOldRoleId
    FROM Employees.EmployeeAuthorization 
    WHERE AuthorizedID=@Authorized
   
    BEGIN TRY	            
	
    DELETE FROM Employees.EmployeeAuthorization 
				WHERE AuthorizedID=@Authorized 
				AND AuthourizorID=@Authorizor

	UPDATE  Employees.Employee 
	SET RoleId=@RoleId 
	WHERE EmployeeId=@Authorized 


	DELETE FROM Employees.EmployeeDepartmentLink
	WHERE EmployeeId=@Authorized
	AND IsAuthorization=1
	AND Authorizor=@Authorizor

    END TRY


    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
  
        RETURN    
    END CATCH
END









GO
