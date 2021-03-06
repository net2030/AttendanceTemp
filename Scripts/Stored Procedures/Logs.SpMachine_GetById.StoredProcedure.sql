USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpMachine_GetById]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Machine By Id
-- =============================================
CREATE PROCEDURE [Logs].[SpMachine_GetById]
(
 @MachineId int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;       
          
    BEGIN TRY
		SELECT [MachineId]
			  ,[MachineName]
			  ,[Location]
			  ,[DepartmentId]
			  ,[IPAddress]
			  ,[MachineTypeId]
			  ,[AddedUserAccountId]
			  ,[UpdatedUserAccountId]
			  ,[AddedDate]
			  ,[UpdatedDate]
			  ,[VersionNo]
		  FROM  [Logs].[Machine]
		 WHERE MachineId = @MachineId                        	                                         
				    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
