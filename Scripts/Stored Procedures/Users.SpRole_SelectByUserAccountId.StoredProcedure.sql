USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Users].[SpRole_SelectByUserAccountId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Users].[SpRole_SelectByUserAccountId]
(
	@UserAccountId int
)
AS
	SET NOCOUNT ON
	
	SELECT Role.RoleId, 
	       RoleName, 
	       Role.AddedDate, 
	       Role.AddedUserAccountId, 
	       Role.UpdatedDate, 
	       Role.UpdatedUserAccountId, 
	       Role.VersionNo
	  FROM Users.Role
INNER JOIN Users.RoleUserAccount
 	    ON Role.RoleId = RoleUserAccount.RoleId
	 WHERE UserAccountId = @UserAccountId
	
	RETURN






GO
