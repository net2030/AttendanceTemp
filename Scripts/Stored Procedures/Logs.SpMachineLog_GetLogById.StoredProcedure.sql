USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpMachineLog_GetLogById]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/5/2011
-- Description:	get machine log by id
-- =============================================
CREATE PROCEDURE [Logs].[SpMachineLog_GetLogById]
(
 @LogId         int = NULL
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;
    
    BEGIN TRY               
		SELECT [LogId]
			  ,[EmployeeId]
			  ,[LogDate]
			  ,[LogTime]
			  ,[LogTypeId]
			  ,[IPAddress]
			  ,[MachineId]
			  ,[IsValidRecord]
			  ,[IsManualRecord]
			  ,[AddedUserAccountId]
			  ,[UpdatedUserAccountId]
			  ,[AddedDate]
			  ,[UpdateDate]
			  ,[VersionNo]
		  FROM  [Logs].[MachineLog]
	     WHERE LogId = @LogId

		RETURN		 
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
