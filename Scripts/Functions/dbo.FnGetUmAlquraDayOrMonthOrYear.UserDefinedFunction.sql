USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [dbo].[FnGetUmAlquraDayOrMonthOrYear]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 21 July 2007
-- Description:	Convert Gregorian Date To Umalqura
-- =============================================
CREATE FUNCTION [dbo].[FnGetUmAlquraDayOrMonthOrYear]
(
	@GregorianDate date,
	@FormatOption  int = 1
)
RETURNS nvarchar(4)
AS
BEGIN
	-- Return Value
	DECLARE @UmAlquraDate nvarchar(4)
	
	DECLARE @d UmAlQuraDateTime
    DECLARE @YearNo  int
    DECLARE @MonthNo int 
    DECLARE @DayNo   int 
 
    SET @d = UmAlQuraDateTime::FromSqlDateTime(@GregorianDate)
	SET @YearNo  = @d.Year
	SET @MonthNo = @d.Month
	SET @DayNo   = @d.Day
	
	DECLARE @strMonth nvarchar(2)
	DECLARE @strDay nvarchar(2)
		
	IF @MonthNo < 10
	BEGIN
	  SET @strMonth = RTRIM(LTRIM('0' + CAST(@MonthNo as nvarchar(2))))
	END
	ELSE
	BEGIN
	  SET @strMonth = CAST(@MonthNo as nvarchar(2))
	END


	IF @DayNo < 10
	BEGIN
	  SET @strDay = RTRIM(LTRIM('0' + CAST(@DayNo as nvarchar(2))))
	END
	ELSE
	BEGIN
	  SET @strDay = CAST(@DayNo as nvarchar(2))
	END
	
	IF @FormatOption = 1
	BEGIN
	  SET @UmAlquraDate = @strDay
	END
	ELSE
	IF @FormatOption = 2
	BEGIN
	  SET @UmAlquraDate = @strMonth
	END
	ELSE
	IF @FormatOption = 3	
	BEGIN
	  SET @UmAlquraDate = CAST(@YearNo as nvarchar(4)) 
	END
	
			 
	-- Return the result 
	RETURN RTRIM(LTRIM(@UmAlquraDate))
END




GO
