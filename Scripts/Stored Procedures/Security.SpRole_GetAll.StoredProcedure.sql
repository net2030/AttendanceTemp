USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Security].[SpRole_GetAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All Roles 
-- =============================================
CREATE PROCEDURE [Security].[SpRole_GetAll]

WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
       
    DECLARE @DepartmentId int
    DECLARE @EmployeeId int
        
   
                            
  
                          
    BEGIN TRY
	   
			                                       
		SELECT Security.Role.RoleId, 
			   Security.Role.RoleName,
        
			   Security.Role.AddedUserAccountId,
			   Security.Role.UpdatedUserAccountId,
			   Security.Role.AddedDate,
			   Security.Role.UpdatedDate,
			   Security.Role.VersionNo			   
		FROM Security.Role 	     		      
		     
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
