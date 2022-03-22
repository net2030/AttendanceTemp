USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpRole_ReportRoleCapability]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Users].[SpRole_ReportRoleCapability]	
AS
	SET NOCOUNT ON
	
	SELECT Role.RoleName, 
	       RoleCapability.AccessFlag, 
	       Capability.CapabilityName
	  FROM Users.Capability 
INNER JOIN Users.RoleCapability 
		ON Capability.CapabilityId = RoleCapability.CapabilityId 
INNER JOIN Users.Role 
		ON RoleCapability.RoleId = Role.RoleId
                      
	RETURN






GO
