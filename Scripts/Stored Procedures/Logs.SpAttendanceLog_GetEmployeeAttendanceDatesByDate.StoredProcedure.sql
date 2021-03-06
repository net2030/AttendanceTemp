USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendanceLog_GetEmployeeAttendanceDatesByDate]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Employee Attendance
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendanceLog_GetEmployeeAttendanceDatesByDate]
(
 @EmployeeId   int = NULL,
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
	SET @PageSize = 500
   -- Default Page Number

	

          
    BEGIN TRY
		  --*******************************************************************
		  -- Generate & Execute Dynamic query
		  --*******************************************************************	
	SELECT DISTINCT EmployeeId,LogDate
	FROM Logs.AttendanceLog
	WHERE EmployeeId = @EmployeeId
	AND LogDate>=@BegDate AND LogDate<=@EndDate
		                           
		              		  		  		  		    
	   RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
