USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpBehaviorAction_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Behavior Actions 
-- =============================================
CREATE PROCEDURE [Managements].[SpBehaviorAction_GetAll]
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected  int

    BEGIN TRY
		SELECT [ActionId],
			   [ActionName]
		  FROM  [Managements].[BehaviorAction]	   
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
