USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpUserAccount_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All users 
-- =============================================
CREATE PROCEDURE [Users].[SpUserAccount_GetAll]
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
			                                       
		SELECT u.UserAccountId,
			   u.WindowsAccountName,
			   u.UserName,
			   u.EMail,
			   u.IsActive,			   			   			   
			   u.UpdatedUserAccountId,
			   u.AddedDate,
			   u.UpdatedDate,
			   u.VersionNo			   
		FROM Employees.Employee e 
		     INNER JOIN Users.UserAccount u
		  ON e.EmployeeId = u.EmployeeId 		 	     		       
		     INNER JOIN CTE
		  ON e.DepartmentId = CTE.DepartmentId    
	 ORDER BY u.UserName	  
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
