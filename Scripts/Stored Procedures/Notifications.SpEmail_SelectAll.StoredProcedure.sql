USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Notifications].[SpEmail_SelectAll]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	select all emails
-- =============================================
CREATE PROCEDURE [Notifications].[SpEmail_SelectAll]
(
 @PageNumber     int = 1,
 @PageSize       int = 50,
 
 @PagesTotal     int = 0 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @strPageSize  nvarchar(50)
    DECLARE @strStartRow  nvarchar(50)

    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END

	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))
	            
    BEGIN TRY 	            
		  EXEC(
		  '

		  WITH RowsResult AS 
		  (
		   SELECT ROW_NUMBER() OVER (ORDER BY EmailId) AS RowSerailID,
		          EmailId,
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
		     FROM  [Notifications].[Email]) 
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
		    FROM   RowsResult
		   WHERE  RowSerailID BETWEEN '+@strStartRow+' AND ('+@strStartRow+'+'+@strPageSize+'-1);'
		  )				       				       

          -- Set Total Pages and Page No
		SET @PagesTotal = (SELECT COUNT(*) from  [Notifications].[Email])
				     
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
