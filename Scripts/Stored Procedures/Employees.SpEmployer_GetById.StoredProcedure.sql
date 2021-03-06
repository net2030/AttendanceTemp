USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployer_GetById]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Exception Type
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployer_GetById]
(
 @EmployerId   int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
        
       
    BEGIN TRY	
		SELECT [EmployerId]
			  ,[EmployerName]
			  ,[EmployerNameEN]
			  ,[AddedUserAccountId]
			  ,[UpdatedUserAccountId]
			  ,[AddedDate]
			  ,[UpdatedDate]
			  ,[VersionNo]
		  FROM  [Employees].[Employer]
	     WHERE [EmployerId] = @EmployerId			   			        
		
	   RETURN
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
