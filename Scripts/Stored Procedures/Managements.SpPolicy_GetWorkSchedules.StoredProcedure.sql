USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpPolicy_GetWorkSchedules]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Get Policy Work Schedules
-- =============================================
CREATE PROCEDURE [Managements].[SpPolicy_GetWorkSchedules]
(
 @PolicyId     int = NULL,
 @PageNumber   int = 1,
 @PageSize     int = 50,
 
 @PagesTotal int = 0 OUTPUT 
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
	
	SET @Tables = 'Managements.WorkSchedule w
	               INNER JOIN Managements.WorkScheduleType t
                   ON w.WorkScheduleTypeId = t.WorkScheduleTypeId 
                   INNER JOIN Employees.Department d
                   ON w.DepartmentId = d.DepartmentId '
	
	SET @Fields = ' w.WorkScheduleId, 
                    w.WorkScheduleName, 
                    t.WorkScheduleTypeName, 
                    d.DepartmentName  ' 		 	
	               
	SET @strFilter = 'WHERE w.PolicyId = ' + CAST(@PolicyId as nvarchar(10))
	SET @strGroup = ''
	SET @Sort = 'w.WorkScheduleId'

         
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
		      	
		SET @PagesTotal = (SELECT COUNT(w.WorkScheduleId)
							 FROM Managements.WorkSchedule w
							      INNER JOIN Managements.WorkScheduleType t
							   ON w.WorkScheduleTypeId = t.WorkScheduleTypeId 
							      INNER JOIN Employees.Department d
							   ON w.DepartmentId = d.DepartmentId
							WHERE w.PolicyId = @PolicyId)
		  		        				       	
       RETURN      
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
