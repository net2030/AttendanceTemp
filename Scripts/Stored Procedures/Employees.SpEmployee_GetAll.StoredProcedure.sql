USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_GetAll]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Employees 
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_GetAll]
(
 @UserAccountId  int = NULL,
 @PageNumber     int = 1,
 @PageSize       int = 50,
 @FilterOptionNo int = 1,
 @SortOptionNo int = 1, 
 
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
    DECLARE @EmployeeId int
        
    -- Initializations              
    SET @RowEffected = 0
    
    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END
        
	IF @UserAccountId<>0
	BEGIN						  


	        	-- Create user temporary tree table	
    CREATE TABLE #UserTree 
    (
	 DepartmentId int, 
	 DepartmentName nvarchar(100), 
	 ParentId int
    )

				   ;WITH CTE(DepartmentId, DepartmentName, ParentId)
				AS
				(
				SELECT DepartmentId, DepartmentName, ParentId
				  From Employees.Department 
				 WHERE DepartmentId in (SELECT DepartmentId 
									   FROM Employees.EmployeeDepartmentLink
									  WHERE EmployeeId = @UserAccountId)  
				 UNION ALL
				SELECT e.DepartmentId, e.DepartmentName, e.ParentId
				  FROM Employees.Department e INNER JOIN CTE c
					ON e.ParentId = c.DepartmentId
				)
				INSERT INTO #UserTree
				(DepartmentId, DepartmentName, ParentId)
				SELECT DepartmentId, DepartmentName, ParentId 
				FROM CTE  
		 
        
				-- check if tree root is exists
				IF NOT EXISTS(SELECT DepartmentId FROM #UserTree WHERE ParentId IS NULL)
				BEGIN
				  -- create a tree root
				  UPDATE #UserTree SET ParentId = NULL WHERE DepartmentId in (SELECT DepartmentId 
									   FROM Employees.EmployeeDepartmentLink
									  WHERE EmployeeId = @EmployeeId)  
				END
        
		END
		


    --SET @EmployeeId = (SELECT EmployeeId 
    --                     FROM Users.UserAccount
    --                    WHERE UserAccountId = @UserAccountId)
                            
    --SET @DepartmentId = (SELECT DepartmentId
    --                       FROM Employees.EmployeeDepartmentLink
    --                      WHERE EmployeeId = @EmployeeId)
                          
	---- Set paging variables
	--SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	--SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	--SET @Tables = ' Employees.Employee INNER JOIN #Departments
	--	         ON Employees.Employee.DepartmentId = #Departments.DepartmentId 
	--		        INNER JOIN Employees.JobTitle 
	--	         ON Employees.Employee.JobTitleId = Employees.JobTitle.JobTitleId 
	--		        INNER JOIN Employees.Position 
	--	         ON Employees.Employee.PositionId = Employees.Position.PositionId 
	--		        INNER JOIN Employees.PositionType 
	--	         ON Employees.Employee.PositionTypeId = Employees.PositionType.PositionTypeId 
	--		        INNER JOIN Employees.Nationality 
	--	         ON Employees.Employee.NationalityId = Employees.Nationality.NationalityId '
	
	--SET @Fields = 'Employees.Employee.EmployeeId, 
	--		       Employees.Employee.EmployeeName, 
	--		       Employees.Employee.BadgeNo, 
	--		       #Departments.DepartmentName, 
	--		       Employees.PositionType.PositionTypeName, 
	--		       Employees.JobTitle.JobTitleName, 
	--		       Employees.Position.PositionName, 
	--		       Employees.Nationality.NationalityName,
	--		       Common.FnToUmAlqura(Employees.Employee.HireDate, 2) AS UmHireDate,
	--		       CASE  Employees.Employee.IsFingerRegistered 
	--		        WHEN 0 THEN ''لا'' 
	--		        WHEN 1 THEN ''نعم'' END as FingerRegistered ' 		 	
	               
	--SET @strFilter = ''
	--SET @strGroup = ''	

   /*****************************************************
    Employees filter options
   *****************************************************/ 
   -- IF @FilterOptionNo  = 1 -- جميع الموظفين
   -- BEGIN
   --   SET @strFilter = ''  
   -- END
   -- ELSE
   -- IF @FilterOptionNo  = 2 -- جميع المدنيين
   -- BEGIN
	  --SET @strFilter = ' WHERE Employees.Employee.IsActive = 1
			--			 AND Employees.Employee.PositionTypeId IN (2, 3) '      
   -- END
   -- ELSE
   -- IF @FilterOptionNo  = 3 -- جميع العسكريين
   -- BEGIN
   --   SET @strFilter = ' WHERE Employees.Employee.IsActive = 1
			--             AND Employees.Employee.PositionTypeId = 1 '     
   -- END
   -- ELSE
   -- IF @FilterOptionNo  = 4 -- جميع العسكريين الضباط
   -- BEGIN
   --   SET @strFilter = ' WHERE Employees.Employee.IsActive = 1
			--             AND Employees.Employee.PositionTypeId = 1
			--             AND Employees.Position.IsOfficer = 1 '    
   -- END
   -- ELSE
   -- IF @FilterOptionNo  = 5 -- جميع العسكريين صف الضباط
   -- BEGIN
   --   SET @strFilter = ' WHERE Employees.Employee.IsActive = 1
			--             AND Employees.Employee.PositionTypeId = 1
			--             AND Employees.Position.IsOfficer = 0 '     
   -- END                    

   /*****************************************************
    Employees sort options
   *****************************************************/ 
   --IF @SortOptionNo = 1 -- أسم الموظف
   --BEGIN
   --  SET @Sort = 'Employees.Employee.EmployeeName '
   --END
   --ELSE
   --IF @FilterOptionNo  > 1 AND @SortOptionNo = 2 -- الرتبة أو المرتبة
   --BEGIN
   --  SET @Sort = 'Employees.Position.SortIndex '
   --END
   --ELSE
   --IF @SortOptionNo = 3 -- رقم الموظف
   --BEGIN
   --  SET @Sort = 'Employees.Employee.EmployeeId '
   --END
   --ELSE
   --BEGIN
   --  SET @Sort = 'Employees.Employee.EmployeeName '
   --END
         
	---- Create A temporary Cards Result Table	
    --CREATE TABLE #Departments
    --(
    -- [DepartmentId]   int, 
    -- [DepartmentName] nvarchar(100), 
    -- [ParentId]       int
    -- )
     
    BEGIN TRY

	 --   ;WITH CTE(DepartmentId, DepartmentName, ParentId)
		--AS
		--(
		--SELECT DepartmentId, DepartmentName, ParentId
		--  From Employees.Department 
		-- WHERE DepartmentId in(SELECT DepartmentId
  --                         FROM Employees.EmployeeDepartmentLink
  --                        WHERE EmployeeId = @EmployeeId)
		-- UNION ALL
		--SELECT e.DepartmentId, e.DepartmentName, e.ParentId
		--  FROM Employees.Department e
		--       INNER JOIN CTE c
		--    ON e.ParentId = c.DepartmentId
		--	)
			                        
		--INSERT INTO #Departments
		--       (
  --              [DepartmentId], 
  --              [DepartmentName], 
  --              [ParentId] 
		--       )
		--SELECT  [DepartmentId], 
  --              [DepartmentName], 
  --              [ParentId]
  --         FROM CTE                            		     		      
    

		  --*******************************************************************
		  -- Generate & Execute Dynamic query
		  --*******************************************************************	
		  --EXEC(
		  --'

		  --WITH RowsResult AS 
		  --(
		  -- 		SELECT     ROW_NUMBER() OVER ( ORDER BY ' + @Sort+ ') AS RowSerailID,'
		  --+@Fields+ ' FROM ' + @Tables + @strFilter + ' ' + @strGroup + ') 
		  --SELECT     *
		  --  FROM   RowsResult
		  -- WHERE  RowSerailID BETWEEN '+@strStartRow+' AND ('+@strStartRow+'+'+@strPageSize+'-1);'
		  --)



		   SELECT  
			Employees.Employee.EmployeeId,
			Employees.Employee.BadgeNo,
			Employees.Employee.FirstName,
			Employees.Employee.MiddleName,
			Employees.Employee.LastName,
			Employees.Employee.GovId,
			Employees.Employee.EmailAddress,
			Employees.Department.DepartmentName,
			Employees.Location.LocationName,
			Employees.Employer.EmployerName,
			Employees.Employer.EmployerName,
			Employees.ContractType.ContractTypeName,
			Employees.Employee.IsActive,
			CASE  Employees.Employee.IsFingerRegistered 
		        WHEN 0 THEN 'لا' 
		        WHEN 1 THEN 'نعم' END as FingerRegistered ,
            Employees.Employee.VersionNo

			FROM Employees.Employee INNER JOIN Employees.Department
			ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId 
				INNER JOIN Employees.Location 
			ON Employees.Employee.LocationId = Employees.Location.LocationId 
			INNER JOIN Employees.Employer 
			ON Employees.Employee.Employer = Employees.Employer.EmployerId
			INNER JOIN Employees.ContractType 
			ON Employees.Employee.ContractType =Employees.ContractType.ContractTypeId
			INNER JOIN #UserTree on Employees.Employee.DepartmentId = #UserTree.DepartmentId
	

          -- Set Total Pages and Page No
		--SET @PagesTotal = (SELECT COUNT(*) 
		--                     FROM Employees.Employee INNER JOIN #Departments
		--					   ON Employees.Employee.DepartmentId = #Departments.DepartmentId 
		--						  INNER JOIN Employees.JobTitle 
		--					   ON Employees.Employee.JobTitleId = Employees.JobTitle.JobTitleId 
		--						  INNER JOIN Employees.Position 
		--					   ON Employees.Employee.PositionId = Employees.Position.PositionId 
		--						  INNER JOIN Employees.PositionType 
		--					   ON Employees.Employee.PositionTypeId = Employees.PositionType.PositionTypeId 
		--						  INNER JOIN Employees.Nationality 
		--					   ON Employees.Employee.NationalityId = Employees.Nationality.NationalityId)
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
