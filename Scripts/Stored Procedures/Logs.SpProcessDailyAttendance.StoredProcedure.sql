USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpProcessDailyAttendance]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Logs].[SpProcessDailyAttendance]
AS
BEGIN
DECLARE @MAxDate as Date=NULL


EXEC [Logs].[SpImport_Transaction_From_PWNT]

set @MAxDate = convert(date,GETDATE(),105)

exec logs.SpProcessAttendance @MAxDate

END






GO
