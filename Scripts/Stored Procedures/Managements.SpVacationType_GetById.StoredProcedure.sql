USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacationType_GetById]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Exception Type
-- =============================================
CREATE PROCEDURE [Managements].[SpVacationType_GetById]
(
 @TypeId   int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
        
       
    BEGIN TRY	
		SELECT [TypeId]
			  ,[TypeName]
			  ,[TypeNameEN]
			  ,[AddedUserAccountId]
			  ,[UpdatedAccountId]
			  ,[AddedDate]
			  ,[UpdatedDate]
			  ,[VersionNo]
		  FROM  [Managements].[VacationType]
	     WHERE [TypeId] = @TypeId			   			        
		
	   RETURN
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
