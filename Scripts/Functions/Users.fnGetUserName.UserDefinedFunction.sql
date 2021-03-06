USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [Users].[fnGetUserName]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [Users].[fnGetUserName]
(
@UserAccountId int
)
RETURNS NVARCHAR(50)
AS
	BEGIN
	  DECLARE @UserName as nvarchar(50)
	  
	  SET @UserName = (SELECT UserName 
	                     FROM GTV.Users.UserAccount 
	                    WHERE UserAccountId = @UserAccountId)
	RETURN @UserName
	END




GO
