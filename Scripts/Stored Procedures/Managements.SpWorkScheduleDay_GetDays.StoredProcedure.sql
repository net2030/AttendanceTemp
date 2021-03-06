USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkScheduleDay_GetDays]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Work Scedule Days
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkScheduleDay_GetDays]
(
 @WorkScheduleId  int = NULL,
 @ShiftNo         int = 1
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
     
    BEGIN TRY
		SELECT [WorkScheduleId],
			   [Managements].[WorkScheduleDay].[WeekDayNo],
			   [WeekDayName],
			   [IsWeekendDay],
			   [WorkBegTime],
			   [WorkEndTime],
			   [VersionNo]
		  FROM  [Managements].[WorkScheduleDay]
		  LEFT JOIN Managements.[WeekDay] ON [Managements].[WorkScheduleDay].WeekDayNo = Managements.[WeekDay].WeekDayNo
		 WHERE [WorkScheduleId] = @WorkScheduleId 
		 AND   ShiftNo= @ShiftNo
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
