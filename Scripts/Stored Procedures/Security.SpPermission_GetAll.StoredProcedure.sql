USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Security].[SpPermission_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Update Permission
-- =============================================
CREATE PROCEDURE [Security].[SpPermission_GetAll]
(
 @AccountId                      int = 1 
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;

    BEGIN TRY	
	     SELECT *
		FROM Security.AccountPagePermission
		WHERE AccountId=@AccountId
		
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        
        RETURN    
    END CATCH
END






GO
