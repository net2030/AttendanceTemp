USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[InserRandomLogs]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Logs].[InserRandomLogs]
(
@startdate  date = null,
@enddate  date = null,
@EmployeeId int = null,
@Time nvarchar(8) = null,
@LogType  int = null,
@MachineId int = 1,
@IpAddress nvarchar = '192.168.1.183'
)

WITH EXECUTE AS 'dbo'
AS 
BEGIN

  SET NOCOUNT ON;
	
declare @LogTime as datetime 
declare @NewTime as datetime
declare @randNum as integer

while @startdate <= @enddate
begin

set @randNum = FLOOR(RAND()*(10-5+1)+5);

set @LogTime = dateadd(MINUTE,@randNum,@Time)


SET @NewTime = CAST(CAST('1900-01-01' as nvarchar(10)) + 
						  (SUBSTRING(CAST(@LogTime AS NVARCHAR(30)),12 ,6) + ' ' + 
						   SUBSTRING(CAST(@LogTime AS NVARCHAR(30)),18 ,2)) as datetime)



if DATEPART(dw, @startdate) NOT IN (6,0) 
BEGIN

SET @LogTime = CAST(CAST(@startdate as nvarchar(10)) + 
						  (SUBSTRING(CAST(@LogTime AS NVARCHAR(30)),12 ,6) + ' ' + 
						   SUBSTRING(CAST(@LogTime AS NVARCHAR(30)),18 ,2)) as datetime)

select @LogTime,@NewTime
   
          
		INSERT INTO [Logs].[MachineLog]
				   ([EmployeeId],
				    [LogDate],
				    [LogTime],
					LogDateTime,
				    [LogTypeId],
				    [IPAddress],
				    [MachineId],
				    [IsValidRecord],
				    [IsManualRecord],
				    [AddedUserAccountId],
				    [UpdatedUserAccountId],
				    [AddedDate],
				    [UpdateDate])
			 VALUES
				   (@EmployeeId,
				    @startdate,
					@NewTime,
				    @LogTime,
				    @LogType,
				    @IpAddress,
				    @MachineId,
				    1,
				    0,
				    1,
				    1,				    
				    GETDATE(),				    
				    GETDATE())	


	exec logs.SpProcessAttendanceByEmployee @startdate,@EmployeeId

END
SET @startdate = DATEADD(day,1,@startdate)

END

END


GO
