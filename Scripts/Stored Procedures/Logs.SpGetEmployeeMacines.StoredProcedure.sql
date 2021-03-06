USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpGetEmployeeMacines]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 19/02/2018
-- Description:	Get All Machines Related to an Employee
-- =============================================
CREATE PROCEDURE [Logs].[SpGetEmployeeMacines]
(
@EmployeeId INT = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;       
          
    BEGIN TRY
		SELECT  [Logs].[Machine].MachineId
			  ,[Logs].[Machine].[MachineName]
			  ,[Logs].[Machine].[Location]
			  ,CASE WHEN Logs.EmployeeDeviceLink.EmployeeId IS NOT NULL THEN 1 ELSE 0 END AS Selected
		  FROM  [Logs].[Machine]     
		 LEFT JOIN Logs.EmployeeDeviceLink  ON  [Logs].[Machine].MachineId =  Logs.EmployeeDeviceLink.MachineId  
		  WHERE Logs.EmployeeDeviceLink.EmployeeId = @EmployeeId    OR Logs.EmployeeDeviceLink.EmployeeId  IS NULL                             
				    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END





GO
