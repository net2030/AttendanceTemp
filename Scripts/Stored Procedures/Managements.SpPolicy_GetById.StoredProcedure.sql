USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpPolicy_GetById]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Get Policy
-- =============================================
CREATE PROCEDURE [Managements].[SpPolicy_GetById]
(
 @PolicyId                 int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
    
    BEGIN TRY	
		SELECT [PolicyId]
			  ,[PolicyName]
			  ,[PolicyNameEN]
			  ,[MarkObsentDuration]
			  ,[LateInMinutes]
			  ,[LateOutMinutes]
			  ,[EarlyInMinutes]
			  ,[EarlyOutMinutes]
			  ,[LateLimitPerMonthMinutes]
			  ,[DepartmentId]
			  ,[AddedUserAccountId]
			  ,[UpdatedUserAccountId]
			  ,[AddedDate]
			  ,[UpdatedDate]
			  ,[VersionNo]
		  FROM  [Managements].[Policy]
	     WHERE PolicyId = @PolicyId
		  		        				       	
       RETURN      
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
