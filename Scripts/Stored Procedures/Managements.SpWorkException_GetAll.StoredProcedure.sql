USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkException_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Work Exceptions 
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkException_GetAll]
(
 @UserAccountId  int = NULL,
 @BegDate        date = NULL,
 @EndDate        date = NULL, 
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
    DECLARE @EmployeeId int
        
    -- Initializations              
    SET @RowEffected = 0
    
    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END
        
    SET @EmployeeId = (SELECT EmployeeId 
                         FROM Users.UserAccount
                        WHERE UserAccountId = @UserAccountId)
                            
    SET @DepartmentId = (SELECT DepartmentId
                           FROM Employees.EmployeeDepartmentLink
                          WHERE EmployeeId = @EmployeeId)
                          
	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	SET @Tables = ' Managements.WorkException 
       INNER JOIN Managements.WorkExceptionType 
    ON Managements.WorkException.WorkExceptionTypeId = Managements.WorkExceptionType.WorkExceptionTypeId 
       INNER JOIN Employees.Department 
    ON Managements.WorkException.DepartmentId = Employees.Department.DepartmentId
       INNER JOIN #Departments
	ON Employees.Department.DepartmentId = #Departments.DepartmentId '
	
	SET @Fields = ' Managements.WorkException.WorkExceptionId, 
       Managements.WorkException.WorkExceptionTypeId, 
       Managements.WorkException.WorkExceptionBegDate, 
       Managements.WorkException.WorkExceptionEndDate,  
       Managements.WorkException.DepartmentId, 
       Managements.WorkExceptionType.WorkExceptionTypeName, 
       Employees.Department.DepartmentName, 
       dbo.FnUmAlquraYMD(Managements.WorkException.WorkExceptionBegDate) AS UmBegDate, 
       dbo.FnUmAlquraYMD(Managements.WorkException.WorkExceptionEndDate) AS UmEndDate ' 		 	
     		     		      		  	               
	SET @strFilter = 'WHERE Managements.WorkException.WorkExceptionBegDate >= ' + '''' + CAST(@BegDate as nvarchar(10)) + '''' + 
	                 ' AND Managements.WorkException.WorkExceptionBegDate <= ' + '''' + CAST(@EndDate as nvarchar(10)) + ''''
	                  
	SET @strGroup = ''
	SET @Sort = 'Managements.WorkException.WorkExceptionBegDate DESC '

	-- Create A temporary Cards Result Table	
    CREATE TABLE #Departments
    (
     [DepartmentId]   int, 
     [DepartmentName] nvarchar(100), 
     [ParentId]       int
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
		
		INSERT INTO #Departments
		       (
                [DepartmentId], 
                [DepartmentName], 
                [ParentId] 
		       )
		SELECT  [DepartmentId], 
                [DepartmentName], 
                [ParentId]
           FROM CTE                       	                                         

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
		SET @PagesTotal = (SELECT COUNT(Managements.WorkException.WorkExceptionId)
                             FROM Managements.WorkException 
                                  INNER JOIN Managements.WorkExceptionType 
                               ON Managements.WorkException.WorkExceptionTypeId = Managements.WorkExceptionType.WorkExceptionTypeId 
                                  INNER JOIN Employees.Department 
                               ON Managements.WorkException.DepartmentId = Employees.Department.DepartmentId 		     		      
		                          INNER JOIN #Departments
		                       ON Employees.Department.DepartmentId = #Departments.DepartmentId
		                    WHERE Managements.WorkException.WorkExceptionBegDate >= @BegDate
		                      AND Managements.WorkException.WorkExceptionBegDate <= @EndDate)		                   		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
