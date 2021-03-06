USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Security].[SpGetDefaultPageByRole]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get DefaultPage 
-- =============================================
CREATE PROCEDURE [Security].[SpGetDefaultPageByRole]
(
@RoleId int
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
       
    DECLARE @PageId int
 
        
   
                            
  
                          
    BEGIN TRY
	   
	                                       
	SELECT SSP.[SystemSecurityCategoryPageId]
	      ,R.[DefaultAccountPageId]
		  ,SSP.[SystemSecurityCategoryId]
		  ,SSP.[SequenceNumber]
		  ,SSP.[Folder]
		  ,SSP.[SystemCategoryPage]
		  ,SSP.[SystemCategoryPageDescription]
		  ,SSP.[ParentSystemSecurityCateogoryPageId]
		  ,SSP.[IsSiteMapPage]
		  ,SSP.[IsCustomizeable]
		  ,SSP.[IsAllowAdd]
		  ,SSP.[IsAllowEdit]
		  ,SSP.[IsAllowDelete]
		  ,SSP.[IsAllowList]
		  ,SSP.[IsShowDataOptions]
		  ,SSP.[IsShowTillOptions]
		  ,SSP.[IsSystemPage]
		  ,SSP.[ControlLevelPermission]
		  ,SSP.[NotShowInPermission]
		  ,SSP.[NotShowInStartupPage]
		  ,SSP.[SystemFeatureId]
	  FROM  [Security].[SystemSecurityCategoryPage] SSP
	  JOIN Security.Role R on R.DefaultAccountPageId=SSP.SystemSecurityCategoryPageId
	  WHERE   R.RoleId=@RoleId		      
		     
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
