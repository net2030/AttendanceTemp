USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTraining_GetById]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Training by id
-- =============================================
CREATE PROCEDURE [Managements].[SpTraining_GetById]
(
 @TrainingId       int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY	            
		SELECT [TrainingId]
			  ,[DepartmentId]
			  ,[CourseName]
			  ,[InstituteName]
			  ,[TrainingBegDate]
			  ,[TrainingEndDate]
			  ,[Notes]
			  ,[AddedUserAccountId]
			  ,[UpdatedUserAccountId]
			  ,[AddedDate]
			  ,[UpdatedDate]
			  ,[VersionNo]
		  FROM  [Managements].[Training]
         WHERE TrainingId = @TrainingId	        				       
		
		RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
