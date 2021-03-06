USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Notifications].[SpEmail_SelectBySentFlag]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	select email by Sent Flag
-- =============================================
CREATE PROCEDURE [Notifications].[SpEmail_SelectBySentFlag]
(
 @IsEmailSent bit = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
	            
    BEGIN TRY 	            
		  SELECT  EmailId,
		          ToEmailAddress,
		          CCEmailAddress,
		          BCCEmailAddress,
		          FromEmailAddress,
		          EmailSubject,
		          EmailBody,
		          IsEmailSent,
		          AddedUserAccountId,
		          UpdatedUserAccountId,
		          AddedDate,
		          UpdatedDate,
		          VersionNo 		  
		    FROM   [Notifications].[Email]
		   WHERE  IsEmailSent = @IsEmailSent				     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
