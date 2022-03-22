USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTraining_GetByEmployeeId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Training 
-- =============================================
CREATE PROCEDURE [Managements].[SpTraining_GetByEmployeeId]
(
 @EmployeeId     int = NULL, 
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
        
    -- Initializations              
    SET @RowEffected = 0
    
    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END
                          
	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	SET @Tables = ' Managements.Training INNER JOIN Managements.TrainingEmployeeLink
	                ON Managements.Training.TrainingId = Managements.TrainingEmployeeLink.TrainingId 	
	                INNER JOIN Employees.Department 
	                ON Managements.Training.DepartmentId = Employees.Department.DepartmentId '
	
	SET @Fields = 'Managements.Training.TrainingId, 
	               Managements.Training.DepartmentId, 
	               Managements.Training.CourseName, 
	               Managements.Training.InstituteName, 
	               Managements.Training.TrainingBegDate, 
	               Managements.Training.TrainingEndDate, 
	               Managements.Training.VersionNo,
	               Employees.Department.DepartmentName,
	               dbo.FnUmAlquraYMD(Managements.Training.TrainingBegDate) AS UmBegDate,
	               dbo.FnUmAlquraYMD(Managements.Training.TrainingEndDate) AS UmEndDate ' 		 	

                           		     		      		  	               
	SET @strFilter = ' WHERE Managements.TrainingEmployeeLink.EmployeeId = ' + CAST(@EmployeeId as nvarchar(10)) + ' AND Managements.Training.TrainingBegDate >= ' + '''' + CAST(@BegDate as nvarchar(10)) + ' AND Managements.Training.TrainingBegDate <= '  +  '''' + CAST(@EndDate as nvarchar(10)) + '''' 
	SET @strGroup = ''
	SET @Sort = 'Managements.Training.TrainingId '

	-- Create A temporary Cards Result Table	
    CREATE TABLE #Departments
    (
     [DepartmentId]   int, 
     [DepartmentName] nvarchar(100), 
     [ParentId]       int
     )
          
    BEGIN TRY                      	                                         
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
		SET @PagesTotal = (SELECT COUNT(Managements.Training.TrainingId)
                             FROM Managements.Training INNER JOIN Managements.TrainingEmployeeLink
	                           ON Managements.Training.TrainingId = Managements.TrainingEmployeeLink.TrainingId 	
	                              INNER JOIN Employees.Department 
	                           ON Managements.Training.DepartmentId = Employees.Department.DepartmentId
	                        WHERE Managements.TrainingEmployeeLink.EmployeeId = @EmployeeId
	                          AND Managements.Training.TrainingBegDate >= @BegDate 
		                      AND Managements.Training.TrainingBegDate <= @EndDate)
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
