USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGetManagersForSignature]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Insert a new Department
-- =============================================
CREATE PROCEDURE [Managements].[SpGetManagersForSignature]

WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

    -- Declariations
    DECLARE @RowEffected int

    -- Initializations        

    
    
    BEGIN TRY
       

       	  SELECT [FirstName] + ' ' + [MiddleName] + ' ' + [LastName] AS EmployeeName 
		  ,JobTitle
		  FROM Employees.Employee
		  WHERE RoleId != 2
    

    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
       
        RETURN    
    END CATCH
END


/****** Object:  StoredProcedure [Managements].[SpWorkException_Update]    Script Date: 10/20/2014 02:45:06 م ******/
SET ANSI_NULLS ON







GO
