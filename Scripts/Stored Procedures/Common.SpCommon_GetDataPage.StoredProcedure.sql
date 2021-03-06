USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Common].[SpCommon_GetDataPage]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Common].[SpCommon_GetDataPage] 
(
@Tables     nvarchar(max),
@Fields     nvarchar(max) = NULL,
@Sort       nvarchar(500) = NULL,
@Filter     nvarchar(max) = NULL,
@Group      nvarchar(max) = NULL,
@PageNumber int = 1,
@PageSize   int = 10
)
WITH RECOMPILE
AS

DECLARE @strPageSize varchar(50)
DECLARE @strStartRow varchar(50)
DECLARE @strFilter   varchar(max)
DECLARE @strGroup    varchar(max)

IF @PageNumber IS NULL
BEGIN
  SET @PageNumber = 1
END
IF @PageSize IS NULL
BEGIN
  SET @PageSize = 50
END

/*Default Page Number*/
IF @PageNumber < 1
	SET @PageNumber = 1

/*Set paging variables.*/
SET @strPageSize = CAST(@PageSize AS varchar(50))
SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS varchar(50))

/*Set filter & group variables.*/
IF @Filter IS NOT NULL AND @Filter != ''
	SET @strFilter = ' WHERE ' + @Filter + ' '
ELSE
	SET @strFilter = ''
IF @Group IS NOT NULL AND @Group != ''
	SET @strGroup = ' GROUP BY ' + @Group + ' '
ELSE
	SET @strGroup = ''
	
IF @Fields IS NULL
	Begin
	  Set @Fields = '*'
	End

--*******************************************************************
-- Generate & Execute Dynamic query
--*******************************************************************	
EXEC(
'

WITH RowsResult AS 
	(
		SELECT     ROW_NUMBER() OVER ( ORDER BY ' + @Sort+ ') AS RowSerailID,'
+@Fields+ ' FROM ' + @Tables + @strFilter + ' ' + @strGroup + ') 
SELECT     *
     FROM   RowsResult
     WHERE  RowSerailID BETWEEN '+@strStartRow+' AND ('+@strStartRow+'+'+@strPageSize+'-1);'
)






GO
