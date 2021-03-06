USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpContractType_GetList]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Exception Type list
-- =============================================
CREATE PROCEDURE [Employees].[SpContractType_GetList]
(
@Lang         char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
        
       
    BEGIN TRY	
		  if @lang = 'en'
			SELECT [ContractTypeId],
				   [ContractTypeNameEN] AS ContractTypeName
			  FROM [Employees].[ContractType]		
			  UNION 
			  SELECT 0,'Choose'
		else
		  SELECT [ContractTypeId],
			   [ContractTypeName]
		  FROM  [Employees].[ContractType]		
		  UNION 
		  SELECT 0,'إختر'
		     			        
		
	   RETURN
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END








GO
