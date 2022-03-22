USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpProcessAttendanceByDate]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Logs].[SpProcessAttendanceByDate]
	-- Add the parameters for the stored procedure here
	@Option int=1,
	@EmployeeId int=0,
	@BegDate   date=NULL,
	@EndDate   date =NULL ,
	@RC  int =0 OUTPUT

AS
BEGIN
	
	SET NOCOUNT ON;

IF @Option=3
delete from logs.AttendanceLog where EmployeeId=@EmployeeId and LogDate>=@BegDate AND LogDate<=@EndDate




    -- Insert statements for procedure here
declare @day date
set @day=@BegDate 
WHILE @day <= @EndDate
BEGIN
IF @Option=1
exec logs.SpProcessAttendance @day
--ELSE IF @Option=2
--exec logs.SpProcessAttendanceByDepartment @day,@DepartmentId 
ELSE IF @Option =3


exec logs.SpProcessAttendanceByEmployee @day,@EmployeeId


set @day=DATEADD(day,1,@day)
END

End







GO
