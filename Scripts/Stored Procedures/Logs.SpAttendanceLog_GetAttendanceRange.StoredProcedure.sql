USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendanceLog_GetAttendanceRange]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Attendance
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendanceLog_GetAttendanceRange]
(
 @DepartmentId   int = NULL,
 @StartDate date = NULL,
 @Enddate date = NULL,
 @Employer       int=1,
 @ContractType   int=1,
 @WorkScheduleId  int=1,
 @Lang         char(2) = 'ar',
 @PageNumber     int = 1,
 @PageSize       int = 50,
 
 @PagesTotal     int = 0 OUTPUT 
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
                   ON Employees.Employee.PositionId = Employees.Position.PositionId 
                   INNER JOIN #Departments
	               ON Employees.Employee.DepartmentId = #Departments.DepartmentId '
	
	SET @Fields = 'Logs.AttendanceLog.LogId, 
	               dbo.FnUmAlquraYMD(Logs.AttendanceLog.LogDate) AS LogDate, 
	               Logs.AttendanceLog.StatusId, 
	               Logs.AttendanceLog.EmployeeId, 
	               Logs.AttendanceLog.InLogId, 
                   SUBSTRING(CAST(Logs.AttendanceLog.InTime AS NVARCHAR(30)),12 ,6) + '' '' + SUBSTRING(CAST(Logs.AttendanceLog.InTime AS NVARCHAR(30)),18 ,2) AS InTime,
                   Logs.AttendanceLog.OutLogId, 
                  
				   SUBSTRING(CAST(Logs.AttendanceLog.OutTime AS NVARCHAR(30)),12 ,6) + '' '' + SUBSTRING(CAST(Logs.AttendanceLog.OutTime AS NVARCHAR(30)),18 ,2) AS OutTime,
                   Logs.AttendanceLog.IsInPunchManual, 
                   Logs.AttendanceLog.IsOutPunchManual, 
                   Logs.AttendanceLog.WorkingMinutes, 
                   Logs.AttendanceLog.InLateMinutes, 
                   Logs.AttendanceLog.OutLateMinutes, 
                   Logs.AttendanceLog.LateMinutes, 
                   Logs.AttendanceLog.OvertimeMinutes,
                   Logs.AttendanceLog.InExtraMinutes,
                   Logs.AttendanceLog.OutExtraMinutes,                                       
                   Logs.AttendanceLog.ExtraTimeMinutes, 
                   Logs.AttendanceLog.OutMinutes,
                   Logs.AttendanceLog.ActualWorkMinutes, 
                   Logs.AttendanceLog.WorkScheduleId, 
                   Logs.AttendanceLog.WorkStartTime, 
                   Logs.AttendanceLog.WorkEndTime, 
                   Logs.AttendanceLog.PolicyId, 
                   Logs.AttendanceLog.LateInMinutes, 
                   Logs.AttendanceLog.LateOutMinutes, 
                   Logs.AttendanceLog.EarlyInMinutes, 
                   Logs.AttendanceLog.EarlyOutMinutes, 
                   Logs.AttendanceLog.MarkObsentDuration, 
                   Logs.AttendanceLog.IsProcessCompleted, 
                   Logs.AttendanceLog.IsReapplyPolicy, 
                   Logs.AttendanceLog.ActionId, 
                   Logs.AttendanceLog.UpdateVersion, 
                   Logs.AttendanceLog.AddedUserAccountId, 
                   Logs.AttendanceLog.UpdatedUserAccountId, 
                   Logs.AttendanceLog.AddedDate, 
                   Logs.AttendanceLog.UpdatedDate, 
                   Logs.AttendanceLog.VersionNo, 
                   
                   Employees.Employee.FirstName + ''  '' + Employees.Employee.MiddleName + '' '' + Employees.Employee.LastName AS EmployeeName , 
                   Employees.Employee.BadgeNo,
                   Employees.Department.DepartmentName, 
                   CASE Logs.AttendanceLog.IsInPunchManual 
                     WHEN 0 THEN ''لا'' 
                     WHEN 1 THEN ''نعم''
                   END AS InPunchManual,
                   CASE Logs.AttendanceLog.IsOutPunchManual 
                     WHEN 0 THEN ''لا''
                     WHEN 1 THEN ''نعم''
                   END AS OutPunchManual, 
                   CASE Logs.AttendanceLog.IsProcessCompleted 
                     WHEN 0 THEN ''لا''
                     WHEN 1 THEN ''نعم''
                   END AS ProcessCompleted, 
                   CASE Logs.AttendanceLog.IsReapplyPolicy 
                     WHEN 0 THEN ''لا''
                     WHEN 1 THEN ''نعم''
                   END AS ReapplyPolicy,
                   Employees.Department.DepartmentId, 
                   dbo.FnUmAlquraYMD(Logs.AttendanceLog.LogDate) AS UmLogDate, 
                   Employees.Position.SortIndex, 
                   Employees.Position.PositionTypeId, 
                   Employees.Position.PositionName'


	if @Lang = 'en'
	BEGIN
	SET @Fields = @Fields + ',Logs.AttendanceStatus.StatusNameEN AS StatusName'
	SET @Fields = @Fields + ',  CASE Logs.AttendanceLog.VerificationLog 
								 WHEN 0 THEN ''No'' 
								 WHEN 1 THEN ''Yes'' END AS VerificationLog'
	END
	ELSE
	BEGIN
	SET @Fields = @Fields + ',Logs.AttendanceStatus.StatusName' 	
	SET @Fields = @Fields + ', CASE Logs.AttendanceLog.VerificationLog 
                     WHEN 0 THEN ''لا'' 
                     WHEN 1 THEN ''نعم'' END AS VerificationLog'
	END	

    SET @strFilter = ' WHERE Logs.AttendanceLog.LogDate >= ' + '''' + CAST(@StartDate as nvarchar(10)) + '''' +
					 '  AND Logs.AttendanceLog.LogDate <= ' + ''''  + CAST(@Enddate as nvarchar(10)) + '''' 

    IF @ContractType <>0 
	 SET @strFilter =   @strFilter + '  AND Employee.ContractType=' + '''' +   CAST(@ContractType AS CHAR) + ''''

	 IF @Employer <>0 
	 SET @strFilter =   @strFilter + '  AND Employee.Employer= ' + ''''  + CAST(@Employer AS CHAR) + '''' 

	 IF @WorkScheduleId <>0 
	 SET @strFilter =   @strFilter + '  AND Logs.AttendanceLog.WorkScheduleId= ' + ''''  + CAST(@WorkScheduleId AS CHAR) + '''' 
	  




    
					   

	SET @strGroup = ''
	SET @Sort = 'Logs.AttendanceLog.LogId'

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
		SET @PagesTotal = (SELECT COUNT(Logs.AttendanceLog.LogId) 
		                     FROM Logs.AttendanceLog INNER JOIN Logs.AttendanceStatus 
	                           ON Logs.AttendanceLog.StatusId = Logs.AttendanceStatus.StatusId 
	                              INNER JOIN Employees.Employee 
	                           ON Logs.AttendanceLog.EmployeeId = Employees.Employee.EmployeeId 
	                              INNER JOIN Employees.Department 
	                           ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId  
                                  INNER JOIN Employees.Position 
                               ON Employees.Employee.PositionId = Employees.Position.PositionId 
                                  INNER JOIN #Departments
	                           ON Employees.Employee.DepartmentId = #Departments.DepartmentId
	                        WHERE Logs.AttendanceLog.LogDate >= @StartDate AND Logs.AttendanceLog.LogDate <= @Enddate)	                           
		              		  		  		  		    
	   RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
