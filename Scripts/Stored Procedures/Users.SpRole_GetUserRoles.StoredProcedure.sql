USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpRole_GetUserRoles]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah (saad alqarni)
-- Create date: 7/2/2012
-- Description:	Get Roles
-- =============================================
CREATE PROCEDURE [Users].[SpRole_GetUserRoles]
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
    
    SET @DepartmentId = (SELECT DepartmentId
                           FROM Employees.EmployeeDepartmentLink
                          WHERE DepartmentId = (SELECT EmployeeId 
                                                  FROM Users.UserAccount
                                                 WHERE UserAccountId = @UserAccountId))
                          
	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	SET @Tables = '#Roles'
	
	SET @Fields = 'RoleId, 
	               RoleName, 
	               DepartmentId, 
	               DepartmentName' 		 	
	               
	SET @strFilter = ''
	SET @strGroup = ''
	SET @Sort = 'RoleId'

	-- Create A temporary Cards Result Table	
    CREATE TABLE #Roles
    (
     [RoleId]         int, 
     [RoleName]       nvarchar(50), 
     [DepartmentId]   int, 
     [DepartmentName] nvarchar(100)  
     )
     
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
			                        
        INSERT INTO #Roles
        (RoleId, 
		 RoleName, 
		 DepartmentId, 
		 DepartmentName)                 
		SELECT Users.Role.RoleId, 
			 Users.Role.RoleName, 
			 Users.Role.DepartmentId, 
			 Employees.Department.DepartmentName
		FROM Users.Role INNER JOIN Employees.Department 
		  ON Users.Role.DepartmentId = Employees.Department.DepartmentId   		     		      
		     INNER JOIN CTE
		  ON Users.Role.DepartmentId = CTE.DepartmentId    

		  --*******************************************************************
		  -- Generate & Execute Dynamic query
		  --*******************************************************************	
		  EXEC(
		  '

		  WITH RowsResult AS 
		  (
		   		SELECT     ROW_NUMBER() OVER ( ORDER BY ' + @Sort+ ') AS RowSerailID,'
		  +@Fields+ ' FROM ' + @Tables + @strFilter + ' ' + @strGroup + ') 
		  SELECT     *
		    FROM   RowsResult
		   WHERE  RowSerailID BETWEEN '+@strStartRow+' AND ('+@strStartRow+'+'+@strPageSize+'-1);'
		  )

          -- Set Total Pages and Page No
		SET @PagesTotal = (SELECT COUNT(*) from #Roles)
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
