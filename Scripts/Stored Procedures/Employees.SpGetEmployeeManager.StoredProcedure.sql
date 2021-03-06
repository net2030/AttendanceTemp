USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpGetEmployeeManager]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	get employeeId manager name
-- =============================================
CREATE PROCEDURE [Employees].[SpGetEmployeeManager]
(
 @ManagerId     int = NULL,
 @EmployeeName  nvarchar(100) = '' OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
    
    BEGIN TRY
      SET @EmployeeName = (SELECT EmployeeName 
                             FROM  [Employees].[Employee]
	                        WHERE EmployeeId = @ManagerId)	
	  RETURN     
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
