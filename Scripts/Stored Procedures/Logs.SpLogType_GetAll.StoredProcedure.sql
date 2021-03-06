USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpLogType_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/5/2011
-- Description:	get Log Types
-- =============================================
CREATE PROCEDURE [Logs].[SpLogType_GetAll]
(
 @LogId         int = NULL
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;
    
    BEGIN TRY               
		SELECT [LogTypeId]
			  ,[LogTypeName]
		  FROM  [Logs].[LogType]
		 WHERE IsValidType = 1

		RETURN		 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
