USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpGetCapabilities]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 31 05 2012
-- Description:	Capabilities
-- =============================================
CREATE PROCEDURE [Users].[SpGetCapabilities]
(
 @RoleId int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
  SET NOCOUNT ON;
  
  DECLARE @Name        nvarchar(100)
  DECLARE @ItemId      int
  DECLARE @Id          int
  DECLARE @CapabilitId int
  DECLARE @AccessType  int
  DECLARE @IsTitleOnly  bit
  DECLARE @AccessFlag int = 0
  DECLARE @RoleCapabilityId int
  DECLARE @Version Timestamp
	
  CREATE TABLE #RoleCapabilities
  (
   Id                int IDENTITY(1,1) NOT NULL,
   CapabilityId      int,
   CapabilityName    nvarchar(100),
   IsTitleOnly       bit,
   AccessType        int,
   IsReadOnlyAccess  bit,
   IsNoAccess        bit,
   IsEditAccess      bit,
   RoleCapabilityId  int
   )

   Declare Crs_Items Cursor 
   LOCAL
   For 
   SELECT MenuItemId,
          MenuItemName       
     FROM Users.MenuItem
    WHERE ParentMenuItemId IS NULL
   ORDER BY MenuItemId 
   FOR READ ONLY

   Declare Crs_Capabilities Cursor 
   LOCAL
   For 
   SELECT Id,
          CapabilityId,
          AccessType,
          IsTitleOnly                
     FROM #RoleCapabilities
   ORDER BY Id 
   FOR READ ONLY
   		   
  BEGIN TRY
	Open Crs_Items
	Fetch Next From Crs_Items Into @ItemId, @Name
	WHILE @@Fetch_Status = 0 -- Start Loop
	BEGIN

		  INSERT INTO #RoleCapabilities
		  (CapabilityName, IsTitleOnly, AccessType) VALUES (@Name, 1, 0)
	      
		 INSERT INTO #RoleCapabilities
		  (RoleCapabilityId,
		   IsTitleOnly,
		   IsReadOnlyAccess,
		   IsNoAccess,
		   IsEditAccess,
		   CapabilityId,
		   CapabilityName,        
		   AccessType
		   ) 
		  SELECT 0, 0, 0, 0, 0,
		   CapabilityId,
		   CapabilityName,
		   AccessType                   
		  FROM Users.Capability
		  WHERE MenuItemId IN (SELECT MenuItemId 
								 FROM Users.MenuItem
								WHERE ParentMenuItemId = @ItemId)

	Fetch Next From Crs_Items Into @ItemId, @Name
	End 
	Close Crs_Items
	Deallocate Crs_Items

	Open Crs_Capabilities
	Fetch Next From Crs_Capabilities Into @Id, @CapabilitId, @AccessType, @IsTitleOnly
	WHILE @@Fetch_Status = 0 -- Start Loop
	BEGIN

		IF @IsTitleOnly = 0
	    BEGIN
	     IF @AccessType = 0 -- None/ReadOnly/Edit
	     BEGIN
	       IF EXISTS(SELECT RoleId 
	                   FROM Users.RoleCapability
	                  WHERE RoleId       = @RoleId
	                    AND CapabilityId = @CapabilitId)	                    
	       BEGIN	        
	         SET @AccessFlag = (SELECT AccessFlag 
	                              FROM Users.RoleCapability
	                             WHERE RoleId       = @RoleId
	                               AND CapabilityId = @CapabilitId)
	         SET @RoleCapabilityId = (SELECT RoleCapabilityId 
	                                    FROM Users.RoleCapability
	                                   WHERE RoleId       = @RoleId
	                                     AND CapabilityId = @CapabilitId)                               
	         IF @AccessFlag = 0
	         BEGIN   
	            UPDATE #RoleCapabilities 
	               SET IsNoAccess = 1,
	                   RoleCapabilityId = @RoleCapabilityId
	             WHERE Id = @Id  
	         END
	         ELSE
	         IF @AccessFlag = 1
	         BEGIN   
	            UPDATE #RoleCapabilities 
	               SET IsReadOnlyAccess = 1,
	                   RoleCapabilityId = @RoleCapabilityId
	             WHERE Id = @Id  
	         END
	         ELSE
	         IF @AccessFlag = 2
	         BEGIN   
	            UPDATE #RoleCapabilities 
	               SET IsEditAccess = 1,
	                   RoleCapabilityId = @RoleCapabilityId 
	             WHERE Id = @Id  
	         END       	         	                               
	       END
	       ELSE
	       BEGIN   
	          UPDATE #RoleCapabilities 
	             SET IsNoAccess = 1,
	                 RoleCapabilityId = 0
	           WHERE Id = @Id  
	       END	 	                 
	     END
	     ELSE
	     IF @AccessType = 1 -- None/Readonly
	     BEGIN
	       IF EXISTS(SELECT RoleId 
	                   FROM Users.RoleCapability
	                  WHERE RoleId       = @RoleId
	                    AND CapabilityId = @CapabilitId)	                    
	       BEGIN	        
	         SET @AccessFlag = (SELECT AccessFlag 
	                              FROM Users.RoleCapability
	                             WHERE RoleId       = @RoleId
	                               AND CapabilityId = @CapabilitId)
	         SET @RoleCapabilityId = (SELECT RoleCapabilityId 
	                                    FROM Users.RoleCapability
	                                   WHERE RoleId       = @RoleId
	                                     AND CapabilityId = @CapabilitId) 	                               
	         IF @AccessFlag = 0
	         BEGIN   
	            UPDATE #RoleCapabilities 
	               SET IsNoAccess = 1,
	                   RoleCapabilityId = @RoleCapabilityId
	             WHERE Id = @Id  
	         END
	         ELSE
	         IF @AccessFlag = 1
	         BEGIN   
	            UPDATE #RoleCapabilities 
	               SET IsReadOnlyAccess = 1,
	                   RoleCapabilityId = @RoleCapabilityId
	             WHERE Id = @Id  
	         END
	         ELSE
	         BEGIN   
	            UPDATE #RoleCapabilities 
	               SET IsNoAccess = 1,
	                   RoleCapabilityId = 0
	             WHERE Id = @Id  
	         END	         	         	                               
	       END
	       ELSE
	       BEGIN   
	          UPDATE #RoleCapabilities 
	             SET IsNoAccess = 1
	           WHERE Id = @Id  
	       END		       	     
	     END
	     ELSE
	     IF @AccessType = 2 -- None/Edit
	     BEGIN
	       IF EXISTS(SELECT RoleId 
	                   FROM Users.RoleCapability
	                  WHERE RoleId       = @RoleId
	                    AND CapabilityId = @CapabilitId)	                    
	       BEGIN	        
	         SET @AccessFlag = (SELECT AccessFlag 
	                              FROM Users.RoleCapability
	                             WHERE RoleId       = @RoleId
	                               AND CapabilityId = @CapabilitId)
	         SET @RoleCapabilityId = (SELECT RoleCapabilityId 
	                                    FROM Users.RoleCapability
	                                   WHERE RoleId       = @RoleId
	                                     AND CapabilityId = @CapabilitId)                               
	         IF @AccessFlag = 0
	         BEGIN   
	            UPDATE #RoleCapabilities 
	               SET IsNoAccess = 1,
	                   RoleCapabilityId = @RoleCapabilityId
	             WHERE Id = @Id  
	         END
	         ELSE
	         IF @AccessFlag = 2
	         BEGIN   
	            UPDATE #RoleCapabilities 
	               SET IsEditAccess = 1,
	                   RoleCapabilityId = @RoleCapabilityId
	             WHERE Id = @Id  
	         END
	         ELSE
	         BEGIN   
	            UPDATE #RoleCapabilities 
	               SET IsNoAccess = 1,
	                   RoleCapabilityId = 0
	             WHERE Id = @Id  
	         END	         	                               
	       END	     
	       ELSE
	       BEGIN   
	          UPDATE #RoleCapabilities 
	             SET IsNoAccess = 1,
	                 RoleCapabilityId = 0
	           WHERE Id = @Id  
	       END		       
	     END
	    END
	      
	Fetch Next From Crs_Capabilities Into @Id, @CapabilitId, @AccessType, @IsTitleOnly
	End 
	Close Crs_Capabilities
	Deallocate Crs_Capabilities
  
    SELECT Id,
           CapabilityId,
           CapabilityName,
           IsTitleOnly,
           AccessType,
           IsReadOnlyAccess,
           IsNoAccess,
           IsEditAccess,
           RoleCapabilityId
      FROM #RoleCapabilities  
      
    RETURN
  END TRY
  BEGIN CATCH
    EXECUTE [Common].[SpCommon_LogError];
    RETURN   
  END CATCH    
END






GO
