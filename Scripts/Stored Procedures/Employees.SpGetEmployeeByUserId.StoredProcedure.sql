USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpGetEmployeeByUserId]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	get employeeId 
-- =============================================
CREATE PROCEDURE [Employees].[SpGetEmployeeByUserId]
(
 @UserAccountId  int = NULL,
 @EmployeeId     int = -1 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
    
    BEGIN TRY
      SET @EmployeeId = (SELECT EmployeeId 
                           FROM  [Users].[UserAccount]
	                      WHERE UserAccountId = @UserAccountId)	
	  RETURN     
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
