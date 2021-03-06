USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployeeDepartmentLink_GetScope]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get department scope
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployeeDepartmentLink_GetScope]
(
 @EmployeeId          int = NULL, 
 @DepartmentId        int = -1 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
    
    BEGIN TRY
      SET @DepartmentId = (SELECT DepartmentId
                             FROM  [Employees].[EmployeeDepartmentLink]
                            WHERE EmployeeId = @EmployeeId)                        
	  RETURN
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
