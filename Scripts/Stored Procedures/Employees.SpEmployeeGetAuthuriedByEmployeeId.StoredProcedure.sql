USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployeeGetAuthuriedByEmployeeId]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	Insert a new gatepass
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployeeGetAuthuriedByEmployeeId]
(
 @EmployeeId   int = NULL 
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations


    BEGIN TRY	            
	
	SELECT EA.AuthorizedID --AS EmployeeId
	,AE.FirstName + ' ' + AE.MiddleName + ' ' + AE.LastName AS EmployeeName	
	FROM Employees.EmployeeAuthorization  EA
	JOIN Employees.Employee AE ON AE.EmployeeId=EA.AuthorizedID
	WHERE EA.AuthourizorID=@EmployeeId

    END TRY


    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
  
        RETURN    
    END CATCH
END









GO
