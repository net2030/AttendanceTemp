USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [Employees].[fnGetEmployeeName]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [Employees].[fnGetEmployeeName]
(
@EmployeeId int
)
RETURNS NVARCHAR(50)
AS
	BEGIN
	  DECLARE @EmployeeName as nvarchar(50)
	  
	  SET @EmployeeName = (SELECT EmployeeName 
	                         FROM Employees.Employee
	                        WHERE EmployeeId = @EmployeeId)
	RETURN @EmployeeName
	END




GO
