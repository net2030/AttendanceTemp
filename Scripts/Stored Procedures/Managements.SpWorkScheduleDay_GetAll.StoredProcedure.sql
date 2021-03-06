USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkScheduleDay_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Schedule Day
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkScheduleDay_GetAll]
(
 @WorkScheduleId int = NULL,
 @WeekDayNo      int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    BEGIN TRY
		SELECT [WorkScheduleId],
			   [WeekDayNo],
			   [IsWeekendDay],
			   [WorkBegTime],
			   [WorkEndTime],
			   [AddedUserAccountId],
			   [UpdatedUserAccountId],
			   [AddedDate],
			   [UpdatedDate],
			   [VersionNo]      
	      FROM  [Managements].[WorkScheduleDay]
	     WHERE WorkScheduleId = @WorkScheduleId
	       AND WeekDayNo = @WeekDayNo	    
	  RETURN     
    END TRY
    BEGIN CATCH
       EXECUTE [Common].[SpCommon_LogError];
       RETURN    
    END CATCH
END






GO
