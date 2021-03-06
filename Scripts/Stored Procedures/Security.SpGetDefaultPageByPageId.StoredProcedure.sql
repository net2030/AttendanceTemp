USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Security].[SpGetDefaultPageByPageId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get DefaultPage 
-- =============================================
CREATE PROCEDURE [Security].[SpGetDefaultPageByPageId]
(
@PageId int
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
       

 
        
   
                            
  
                          
    BEGIN TRY
		                                       
	SELECT [SystemSecurityCategoryPageId]
		  ,[SystemSecurityCategoryId]
		  ,[SequenceNumber]
		  ,[Folder]
		  ,[SystemCategoryPage]
		  ,[SystemCategoryPageDescription]
		  ,[ParentSystemSecurityCateogoryPageId]
		  ,[IsSiteMapPage]
		  ,[IsCustomizeable]
		  ,[IsAllowAdd]
		  ,[IsAllowEdit]
		  ,[IsAllowDelete]
		  ,[IsAllowList]
		  ,[IsShowDataOptions]
		  ,[IsShowTillOptions]
		  ,[IsSystemPage]
		  ,[ControlLevelPermission]
		  ,[NotShowInPermission]
		  ,[NotShowInStartupPage]
		  ,[SystemFeatureId]
	  FROM  [Security].[SystemSecurityCategoryPage] 
	  WHERE   [SystemSecurityCategoryPageId]=@PageId		      
		     
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
