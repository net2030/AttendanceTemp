USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpHolidayExceptionEmployee_GetByHolidayId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Get work Exception Employees
-- =============================================
CREATE PROCEDURE [Managements].[SpHolidayExceptionEmployee_GetByHolidayId]
(
 @HolidayId int = NULL, 

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

	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	SET @Tables = '   Employees.Employee 
	                  INNER JOIN Managements.HolidayExceptionEmployeeLink 
	                  ON Employees.Employee.EmployeeId = Managements.HolidayExceptionEmployeeLink.EmployeeId 
	                  INNER JOIN Employees.Department 
	                  ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId '
	
	SET @Fields = ' Managements.HolidayExceptionEmployeeLink.HolidayId, 
	                Managements.HolidayExceptionEmployeeLink.EmployeeId, 
	                Employees.Employee.FirstName + '' '' + Employees.Employee.MiddleName + '' ''  + Employees.Employee.LastName AS EmployeeName , 
                    Employees.Department.DepartmentName ' 		 	
     		     		      		  	               
	SET @strFilter = 'WHERE Managements.HolidayExceptionEmployeeLink.HolidayId = ' + CAST(@HolidayId as nvarchar(10)) 
	                 
	SET @strGroup = ''
	SET @Sort = 'Employees.Employee.EmployeeName '
	
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
		SET @PagesTotal = (SELECT COUNT(Managements.HolidayExceptionEmployeeLink.HolidayId)
                             FROM Employees.Employee 
	                              INNER JOIN Managements.HolidayExceptionEmployeeLink 
	                           ON Employees.Employee.EmployeeId = Managements.HolidayExceptionEmployeeLink.EmployeeId 
	                              INNER JOIN Employees.Department 
	                           ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId
		                    WHERE Managements.HolidayExceptionEmployeeLink.HolidayId = @HolidayId)		                   		  		  		  		    
		RETURN     		    
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
