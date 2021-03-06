USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpCalculate_OutTime]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	Update Policy
-- =============================================
CREATE PROCEDURE [Logs].[SpCalculate_OutTime]
(
@LogDate as Date=Null
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
	DECLARE @Count  int=0
    DECLARE @LogId int
	DECLARE @EmployeeId int = NULL
	DECLARE @ActualTime as time
	DECLARE @WorkingMinuts as int
	DECLARE @ActualMinuts as int
	DECLARE @OutMinutes as int
	DECLARE @hours int
	DECLARE @minutes decimal
	DECLARE @hms varchar(8)

    DECLARE @testtable TABLE (LogId INT identity, LogTypeId INT, EmployeeId CHAR(4),LogTime dateTime);
    
    DECLARE Curs_Employees CURSOR
    LOCAL FOR                                          
    SELECT EmployeeId
	FROM logs.AttendanceLog  
    WHERE InTime IS NOT NULL AND OutTime IS NOT NULL  AND Logdate=@LogDate
    FOR READ ONLY
    
    BEGIN TRY
       
    /*****************************************************
	   Open Cursor
	 *****************************************************/      
     OPEN Curs_Employees
     FETCH NEXT FROM Curs_Employees INTO @EmployeeId
     WHILE @@Fetch_Status = 0 
     BEGIN

	INSERT INTO @testtable (LogTypeId,EmployeeId,LogTime)
    SELECT LogTypeId,EmployeeId,LogTime FROM Logs.MachineLog Where LogDate=@LogDate and EmployeeId =@EmployeeId  Order by LogDateTime


	--Check irregular logs

	;with t1 as (
		select ROW_NUMBER()over(order by LogId) RowID
		,LogId
		,LogTypeId
		,EmployeeId
		from @testtable
		)
		SELECT @Count= Count(LogId)
		from @testtable a
		where LogId in (
					select t1.LogId
					from t1 
					inner join t1 t2
					on t1.RowID = t2.RowID + 1
					and t1.LogTypeId = t2.LogTypeId
					and t1.EmployeeId = t2.EmployeeId
					and t1.LogTypeId =1
					)
				 OR LogId in (
					select t1.LogId
					from t1 
					inner join t1 t2
					on t1.RowID = t2.RowID - 1
					and t1.LogTypeId = t2.LogTypeId
					and t1.EmployeeId = t2.EmployeeId
					and t1.LogTypeId =2
					)
 --   IF @Count IS NOT NULL AND @Count>0
	--BEGIN
	--UPDATE Logs.AttendanceLog 
	--      SET StatusId=99
 --         WHERE EmployeeId=@EmployeeId 
	--	  And LogDate=@LogDate 
 --   GOTO Next_Employee
	--END

    -- Remove the duplicated rows 
	;with t1 as (
		select ROW_NUMBER()over(order by LogId) RowID
		,LogId
		,LogTypeId
		,EmployeeId
		from @testtable
		)
		delete a
		from @testtable a
		where LogId in (
					select t1.LogId
					from t1 
					inner join t1 t2
					on t1.RowID = t2.RowID + 1
					and t1.LogTypeId = t2.LogTypeId
					and t1.EmployeeId = t2.EmployeeId
					and t1.LogTypeId =1
					)
				 OR LogId in (
					select t1.LogId
					from t1 
					inner join t1 t2
					on t1.RowID = t2.RowID - 1
					and t1.LogTypeId = t2.LogTypeId
					and t1.EmployeeId = t2.EmployeeId
					and t1.LogTypeId =2
					)


    -- Calculate the Actual Time					
    ;WITH cte_test AS
	(
	SELECT [LogTime],[EmployeeId],[LogTypeId],ROW_NUMBER() OVER (PARTITION BY [EmployeeId],[LogTypeId] ORDER BY [EmployeeId],[LogTypeId])
	 AS rn FROM @testtable WHERE EmployeeId=@EmployeeId
								   
	 )

	SELECT @EmployeeId= a.[EmployeeId], 
		   --@logdate=convert(CHAR(10), a.[LogTime], 111),
		   @ActualTime= convert(varchar(8),dateadd(s,sum(datepart(hour, b.[LogTime]-a.[LogTime]) * 3600) + sum(datepart(minute, b.[LogTime]-a.[LogTime]) * 60) + sum(datepart(second, b.[LogTime]-a.[LogTime])),0),108)
	 FROM cte_test a
	inner join cte_test b on a.[EmployeeId]=b.[EmployeeId]    and a.rn=b.rn
	and a.LogTypeId=1 and b.LogTypeId=2
	group by a.[EmployeeId],convert(char(10), a.[LogTime], 111)

   
   SET @hms = @ActualTime
   SET @hours = datepart(hour, @hms)
   SET @minutes = datepart(minute, @hms)
   SET @ActualMinuts= @minutes+@hours*60

    UPDATE Logs.AttendanceLog 
	      SET OutMinutes=DATEDIFF(mi, InTime,OutTime ) - @ActualMinuts 
		--  ActualWorkMinutes=@ActualMinuts
          WHERE EmployeeId=@EmployeeId 
		  And LogDate=@LogDate 
		  AND ActionId<>3
		  --AND @OutMinutes>1

     --Clear temp Table

	 Next_Employee:

     DELETE FROM @testtable


     FETCH NEXT FROM Curs_Employees INTO @EmployeeId
     END
     
     -- close cursor
     CLOSE Curs_Employees
     -- de-allocate cursor
     DEALLOCATE Curs_Employees
		
		
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         
        RETURN    
    END CATCH
END








GO
