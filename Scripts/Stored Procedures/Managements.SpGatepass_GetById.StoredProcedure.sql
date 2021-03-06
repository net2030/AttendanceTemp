USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepass_GetById]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get gatepass by id
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepass_GetById]
(
 @GatepassId       int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY	            
			SELECT [GatepassId]
			  ,[EmployeeId]
			  ,[IsApproved]
			  ,[GatepassTypeId]
			  ,[GatepassBegDate]
			  ,[GatepassEndDate]
			  ,[GatepassBegTime]
			  ,[GatepassEndTime]
			  ,[RequestedDate]
			  ,[ApprovedDate]
			  ,[ApprovedEmployeeId]
			  ,[Notes]
			  ,ApprovalNotes
			  ,[AddedUserAccountId]
			  ,[UpdatedUserAccountId]
			  ,[AddedDate]
			  ,[UpdatedDate]
			  ,[VersionNo]
		  FROM  [Managements].[Gatepass]
         WHERE GatepassId = @GatepassId		        				       
		
		RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
