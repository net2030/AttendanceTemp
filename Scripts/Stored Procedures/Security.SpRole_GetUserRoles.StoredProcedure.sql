USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Security].[SpRole_GetUserRoles]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah (saad alqarni)
-- Create date: 7/2/2012
-- Description:	Get Roles
-- =============================================
CREATE PROCEDURE [Security].[SpRole_GetUserRoles]
(
 @UserAccountId  int = NULL,
 @PageNumber     int = 1,
 @PageSize       int = 50,
 
 @PagesTotal     int = 0 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected  int
    DECLARE @strPageSize  nvarchar(50)
    DECLARE @strStartRow  nvarchar(50)
    DECLARE @strFilter    nvarchar(max)
    DECLARE @strGroup     nvarchar(max)
    DECLARE @Group        nvarchar(max)
    DECLARE @Tables       nvarchar(max)
    DECLARE @Fields       nvarchar(max) 
    DECLARE @Filter       nvarchar(max)  
    DECLARE @Sort         nvarchar(500)    
    DECLARE @DepartmentId int
    
    -- Initializations              
    SET @RowEffected = 0
    
    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END
    
	SELECT Security.Role.RoleId,
	       Security.Role.RoleName,
		   Security.Role.DefaultAccountPageId
	FROM Security.Role JOIN Employees.Employee 
	ON Security.Role.RoleId= Employees.Employee.RoleId
	WHERE  Employees.Employee.EmployeeId=@UserAccountId
    
    BEGIN TRY
	   



		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
