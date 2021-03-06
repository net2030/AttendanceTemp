USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [dbo].[FnGetFirstOfUmAlquraMonthOrYear]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 21 July 2015
-- Description:	Convert Gregorian Date To Umalqura
-- =============================================
CREATE FUNCTION [dbo].[FnGetFirstOfUmAlquraMonthOrYear]
(
	@GregorianDate date,
	@FormatOption  int = 1
)
RETURNS Date
AS
BEGIN
	-- Return Value
	DECLARE @GregDate DATE=@GregorianDate

	-------------------Georgian----------
	IF @FormatOption=1
	SET @GregDate=DATEADD(month, DATEDIFF(month, 0, @GregorianDate), 0) 
	ELSE IF @FormatOption=2
	SET @GregDate= DATEADD(yy, DATEDIFF(yy,0,getdate()), 0)

	RETURN @GregDate
	-------------------------------------



	DECLARE @d UmAlQuraDateTime
    DECLARE @YearNo  int
    DECLARE @MonthNo int 
    DECLARE @DayNo   int 
    DECLARE @NextDay  DATE

    SET @d = UmAlQuraDateTime::FromSqlDateTime(@GregorianDate)
	SET @YearNo  = @d.Year
	SET @MonthNo = @d.Month
	SET @DayNo   = @d.Day
	
	IF @FormatOption = 1
	WHILE (@DayNo<>1)
	BEGIN
		SET @GregDate=DATEADD(Day,-1,@GregDate)
		SET @d = UmAlQuraDateTime::FromSqlDateTime(@GregDate)
		SET @YearNo  = @d.Year
		SET @MonthNo = @d.Month
		SET @DayNo   = @d.Day
	END

	ELSE 
	IF @FormatOption = 2
	WHILE (@DayNo<>1 OR @MonthNo<>1)
	BEGIN
		SET @GregDate=DATEADD(Day,-1,@GregDate)
		SET @d = UmAlQuraDateTime::FromSqlDateTime(@GregDate)
		SET @YearNo  = @d.Year
		SET @MonthNo = @d.Month
		SET @DayNo   = @d.Day
	END
	
			 
	-- Return the result 
	RETURN @GregDate
END





GO
