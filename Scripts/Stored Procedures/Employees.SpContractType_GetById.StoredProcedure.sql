USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpContractType_GetById]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Exception Type
-- =============================================
CREATE PROCEDURE [Employees].[SpContractType_GetById]
(
 @ContractTypeId   int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
        
       
    BEGIN TRY	
		SELECT [ContractTypeId]
			  ,[ContractTypeName]
			  ,[ContractTypeNameEN]
			  ,[AddedUserAccountId]
			  ,[UpdatedUserAccountId]
			  ,[AddedDate]
			  ,[UpdatedDate]
			  ,[VersionNo]
		  FROM  [Employees].[ContractType]
	     WHERE [ContractTypeId] = @ContractTypeId			   			        
		
	   RETURN
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END








GO
