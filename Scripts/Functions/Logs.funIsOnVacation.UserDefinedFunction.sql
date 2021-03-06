USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [Logs].[funIsOnVacation]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 27/03/2011
-- Description:	Check Employee Vacation
-- =============================================
CREATE FUNCTION [Logs].[funIsOnVacation]
(
 @AttendanceDate datetime,
 @EmployeeId int
)
RETURNS bit
AS
BEGIN
	-- return variable here
	DECLARE @IsOnVacation bit
    SET @IsOnVacation  = 0

	IF EXISTS(SELECT VacationId 
                FROM Managements.Vacation 
               WHERE @AttendanceDate >= EffectiveDate 
                 AND @AttendanceDate <= DateExpire 
                 AND EmployeeId = @EmployeeId
				 AND IsApproved=1)
    BEGIN
      SET @IsOnVacation  = 1
    END

	-- Return the result of the function
	RETURN @IsOnVacation
END




GO
