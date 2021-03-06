USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Security].[SpGetSystemSecurityCategoryPage]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get DefaultPage 
-- =============================================
CREATE PROCEDURE [Security].[SpGetSystemSecurityCategoryPage]
(
@Lang    Char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
       

 
        
   
                            
  
                          
    BEGIN TRY
		                                       
	       SELECT SP.SequenceNumber, 
	              SP.Folder, 
                  SP.SystemCategoryPage, 
				    SP.SystemCategoryPageDescriptionEN,
				  SP.SystemCategoryPageDescription,
				  --CASE WHEN @Lang = 'en' THEN SP.SystemCategoryPageDescriptionEN ELSE SP.SystemCategoryPageDescription END AS SystemCategoryPageDescription, 
                  SP.ParentSystemSecurityCateogoryPageId, 
				  SP.IsSiteMapPage, 
                  SP.IsCustomizeable,
				  SP.IsAllowAdd, 
				  SP.IsAllowEdit, 
                  SP.IsAllowDelete, 
				  SP.IsAllowList, 
                  SP.IsShowDataOptions, 
				  SP.IsShowTillOptions, 
                  SP.IsSystemPage, 
				  SP.ControlLevelPermission, 
                  SP.NotShowInPermission, 
				  SP.SystemSecurityCategoryId, 
                  SP1.Folder AS ParentFolder, 
				  SP1.SystemCategoryPage AS ParentSystemCategoryPage, 
                  SP1.SystemCategoryPageDescription AS ParentSystemCategoryPageDescription, 
                  SP1.IsSiteMapPage AS ParentIsSiteMapPage,
				  SP.SystemFeatureId, 
                  SP.SystemSecurityCategoryPageId
          FROM    Security.SystemSecurityCategoryPage SP LEFT OUTER JOIN
                  Security.SystemSecurityCategoryPage AS SP1 ON 
                  SP.ParentSystemSecurityCateogoryPageId = SP1.SystemSecurityCategoryPageId
          WHERE  (SP.IsSiteMapPage = 1) OR
                  (SP.ControlLevelPermission = 1)
          ORDER BY SP.SequenceNumber		      
		     
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
