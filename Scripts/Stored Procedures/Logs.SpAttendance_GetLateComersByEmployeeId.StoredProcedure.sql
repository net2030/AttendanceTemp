USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendance_GetLateComersByEmployeeId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Late Employees By Id & Date
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendance_GetLateComersByEmployeeId]
(
 @EmployeeId       int = NULL,
 @BegDate          date = NULL,
 @EndDate          date = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
    
    BEGIN TRY
		SELECT [EmployeeId],
			   [EmployeeName],
			   [DepartmentName],
			   [InTime],
			   [OutTime],
			   [WorkingMinutes],
			   [InLateMinutes],
			   [OutLateMinutes],
			   [LateMinutes],
			   [OvertimeMinutes],
			   [InExtraMinutes],
			   [OutExtraMinutes],
			   [ExtraTimeMinutes],
			   [ActualWorkMinutes],
			   [LateInMinutes],
			   [LateOutMinutes],
			   [StatusName],       
			   [DepartmentName],
	
			   [Precentage]
		  FROM  [Logs].[vwAttendanceLog]
		  WHERE LogDate >= @BegDate
			AND LogDate <= @EndDate 
			AND StatusId in(45,110,120)
			AND EmployeeId = @EmployeeId
		ORDER BY [EmployeeName]   
		RETURN 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END




GO
