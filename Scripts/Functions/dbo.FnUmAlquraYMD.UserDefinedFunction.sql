USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [dbo].[FnUmAlquraYMD]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 21 July 2007
-- Description:	Formate Date 
-- =============================================
CREATE FUNCTION [dbo].[FnUmAlquraYMD]
(
 @GregorianDate date = NULL
)
RETURNS nvarchar(10)
AS
BEGIN
	-- Return Value
	IF EXISTS(SELECT * FROM Account WHERE culture='en-US')
	RETURN @GregorianDate


	DECLARE @UmAlquraDate nvarchar(10)
    DECLARE @strDate nvarchar(20)
    
    SET @strDate = CAST(@GregorianDate as nvarchar(20))
    
    IF @GregorianDate IS NULL
    BEGIN
      SET @UmAlquraDate = ''
      RETURN @UmAlquraDate
    END

    IF ISDATE(@strDate) <> 1
    BEGIN
      SET @UmAlquraDate = ''
      RETURN @UmAlquraDate
    END
        	
	DECLARE @d UmAlQuraDateTime
    DECLARE @YearNo  int
    DECLARE @MonthNo int 
    DECLARE @DayNo   int 
 
    SET @d = UmAlQuraDateTime::FromSqlDateTime(@GregorianDate)
	SET @YearNo  = @d.Year
	SET @MonthNo = @d.Month
	SET @DayNo   = @d.Day
	
	DECLARE @strMonth nvarchar(2) = ''
	IF @MonthNo < 10
	BEGIN
	  SET @strMonth = RTRIM(LTRIM('0' + CAST(@MonthNo as nvarchar(2))))
	END
	ELSE
	BEGIN
	  SET @strMonth = CAST(@MonthNo as nvarchar(2))
	END
	
	DECLARE @strDay nvarchar(2) = ''	
	IF @DayNo < 10
	BEGIN
	  SET @strDay = RTRIM(LTRIM('0' + CAST(@DayNo as nvarchar(2))))
	END
	ELSE
	BEGIN
	  SET @strDay = CAST(@DayNo as nvarchar(2))
	END	
	
	SET @UmAlquraDate = CAST(@YearNo as nvarchar(4)) +'/'+ @strMonth +'/'+ @strDay
	 
	-- Return the result 
	RETURN @UmAlquraDate
	--RETURN @GregorianDate
END




GO
