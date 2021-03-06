USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_GetDepartmentScopeId]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	get employee department scope
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_GetDepartmentScopeId]
(
 @EmployeeId          int = NULL, 
 @DepartmentId        int = NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
   
    BEGIN TRY          
        SET @DepartmentId = (SELECT DepartmentId
                               FROM Employees.EmployeeDepartmentLink
                              WHERE EmployeeId = @EmployeeId)		     		   
		 
         RETURN   
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
