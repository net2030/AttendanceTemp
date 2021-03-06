USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpRole_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Roles 
-- =============================================
CREATE PROCEDURE [Users].[SpRole_GetAll]
(
 @UserAccountId  int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
       
    DECLARE @DepartmentId int
    DECLARE @EmployeeId int
        
    SET @EmployeeId = (SELECT EmployeeId 
                         FROM Users.UserAccount
                        WHERE UserAccountId = @UserAccountId)
                            
    SET @DepartmentId = (SELECT DepartmentId
                           FROM Employees.EmployeeDepartmentLink
                          WHERE EmployeeId = @EmployeeId)
                          
    BEGIN TRY
	    ;WITH CTE(DepartmentId, DepartmentName, ParentId)
		AS
		(
		SELECT DepartmentId, DepartmentName, ParentId
		  From Employees.Department 
		 WHERE DepartmentId = @DepartmentId
		 UNION ALL
		SELECT e.DepartmentId, e.DepartmentName, e.ParentId
		  FROM Employees.Department e
		       INNER JOIN CTE c
		    ON e.ParentId = c.DepartmentId
			)
			                                       
		SELECT Users.Role.RoleId, 
			   Users.Role.RoleName,
               Users.Role.DepartmentId,
			   Users.Role.AddedUserAccountId,
			   Users.Role.UpdatedUserAccountId,
			   Users.Role.AddedDate,
			   Users.Role.UpdatedDate,
			   Users.Role.VersionNo			   
		FROM Users.Role INNER JOIN Employees.Department 
		  ON Users.Role.DepartmentId = Employees.Department.DepartmentId 		 	     		      
		     INNER JOIN CTE
		  ON Users.Role.DepartmentId = CTE.DepartmentId    
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
