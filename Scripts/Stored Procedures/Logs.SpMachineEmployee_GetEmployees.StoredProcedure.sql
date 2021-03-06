USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpMachineEmployee_GetEmployees]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Device Employees
-- =============================================
CREATE PROCEDURE [Logs].[SpMachineEmployee_GetEmployees]
(
 @MachineId  int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations


    BEGIN TRY	            
	
	SELECT DL.EmployeeId --AS EmployeeId
	,E.FirstName + ' ' + E.MiddleName + ' ' + E.LastName AS EmployeeName	
	FROM Logs.EmployeeDeviceLink  DL
	JOIN Employees.Employee E ON E.EmployeeId=DL.EmployeeId
	WHERE DL.MachineId=@MachineId

    END TRY


    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
  
        RETURN    
    END CATCH
END








GO
