USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkSchedule_GetById]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Find Work Schedule
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkSchedule_GetById]
(
 @WorkScheduleId      int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY            
		SELECT [WorkScheduleId],
			   [WorkScheduleTypeId],
			   [DepartmentId],
			   [WorkScheduleName],
			   [ScheduleBegDate],
			   [ScheduleEndDate],
			   [IsEffectiveDuringHoliday],
			   [IsPolicyApplied],
			   [PolicyId],
			   [ShiftsCount],
			   [IsPeriodic],
			   [PeriodLength],
			   [FirstPeriodStartDate],
			   [AddedUserAccountId],
			   [UpdatedUserAccountId],
			   [AddedDate],
			   [UpdatedDate],
			   [VersionNo],
			   [WorkScheduleNameEN]
		  FROM  [Managements].[WorkSchedule]
         WHERE WorkScheduleId = @WorkScheduleId     		      
		 
         RETURN   
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
