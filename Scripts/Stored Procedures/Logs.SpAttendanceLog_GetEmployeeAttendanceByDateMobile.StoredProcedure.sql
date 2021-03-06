USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendanceLog_GetEmployeeAttendanceByDateMobile]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Employee Attendance
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendanceLog_GetEmployeeAttendanceByDateMobile]
(
 @id   int = NULL,
 @BegDate      date = NULL,
 @EndDate      date = NULL, 
 @PageNumber   int = 1,
 @PageSize     int = 50,
 
 @PagesTotal   int = 0 OUTPUT 
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected  int = 0
    
    DECLARE @strPageSize  nvarchar(50)
    DECLARE @strStartRow  nvarchar(50)
    DECLARE @strFilter    nvarchar(max)
    DECLARE @strGroup     nvarchar(max)
    DECLARE @Group        nvarchar(max)
    DECLARE @Tables       nvarchar(max)
    DECLARE @Fields       nvarchar(max) 
    DECLARE @Filter       nvarchar(max)  
    DECLARE @Sort         nvarchar(500)  
    
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
	
	SET @Tables = 'Logs.AttendanceLog INNER JOIN Logs.AttendanceStatus 
	               ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
	               INNER JOIN Employees.Employee 
	               ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
	               INNER JOIN Employees.Department 
	               ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
                   INNER JOIN Employees.Position 
                   ON Employees.Employee.PositionId = Employees.Position.PositionId '
	
	SET @Fields = 'dbo.FnUmAlquraYMD( Logs.AttendanceLog.LogDate) as LogDate,
	                Logs.AttendanceLog.StatusId, 
				   SUBSTRING(CAST(Logs.AttendanceLog.InTime AS NVARCHAR(30)),12 ,6) + '' '' + SUBSTRING(CAST(Logs.AttendanceLog.InTime AS NVARCHAR(30)),18 ,2) AS InTime,
				    SUBSTRING(CAST(Logs.AttendanceLog.OutTime AS NVARCHAR(30)),12 ,6) + '' '' + SUBSTRING(CAST(Logs.AttendanceLog.OutTime AS NVARCHAR(30)),18 ,2) AS OutTime,
                   Logs.AttendanceLog.LateMinutes,  
				   Logs.AttendanceLog.ExtraTimeMinutes,            
                   Logs.AttendanceLog.ActualWorkMinutes, 
            	
	                Logs.AttendanceStatus.StatusNameEn AS StatusName'

	SET @strFilter = ' WHERE Logs.AttendanceLog.LogDate >= ' + '''' + CAST(@BegDate as nvarchar(10)) + '''' +
	                 ' AND Logs.AttendanceLog.LogDate <= ' + '''' + CAST(@EndDate as nvarchar(10)) + '''' + 
	                 ' AND Logs.AttendanceLog.EmployeeId = ' + CAST(@id as nvarchar(10))		                     
	
	SET @strGroup = ''
	SET @Sort = 'Logs.AttendanceLog.LogDate desc '
          
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
		SET @PagesTotal = (SELECT COUNT(Logs.AttendanceLog.LogId) 
		                     FROM Logs.AttendanceLog INNER JOIN Logs.AttendanceStatus 
	                           ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
	                              INNER JOIN Employees.Employee 
	                           ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
	                              INNER JOIN Employees.Department 
	                           ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
                                  INNER JOIN Employees.Position 
                               ON Employees.Employee.PositionId = Employees.Position.PositionId                                   
	                        WHERE Logs.AttendanceLog.LogDate >= @BegDate
	                          AND Logs.AttendanceLog.LogDate <= @EndDate
	                          AND Logs.AttendanceLog.EmployeeId = @id)	                           
		              		  		  		  		    
	   RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
