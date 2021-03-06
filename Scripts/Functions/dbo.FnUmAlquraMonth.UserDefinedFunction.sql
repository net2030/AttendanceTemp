USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [dbo].[FnUmAlquraMonth]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 21 July 2007
-- Description:	Formate Date 
-- =============================================
CREATE FUNCTION [dbo].[FnUmAlquraMonth]
(
 @GregorianDate date = NULL
)
RETURNS nvarchar(10)
AS
BEGIN
	-- Return Value
	DECLARE @UmAlquraMonth nvarchar(10)
    DECLARE @strDate nvarchar(20)
    
    SET @strDate = CAST(@GregorianDate as nvarchar(20))
    
    IF @GregorianDate IS NULL
    BEGIN
      SET @UmAlquraMonth = ''
      RETURN @UmAlquraMonth
    END

    IF ISDATE(@strDate) <> 1
    BEGIN
      SET @UmAlquraMonth = ''
      RETURN @UmAlquraMonth
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
	
--	SET @UmAlquraMonth = CAST(@YearNo as nvarchar(4)) +'/'+ @strMonth +'/'+ @strDay
	 
    SET @UmAlquraMonth= CASE @strMonth 
	                  WHEN 1 THEN 'محرم'
					  WHEN 2 THEN 'صفر'
					  WHEN 3 THEN 'ربيع اول'
					  WHEN 4 THEN 'ربيع ثاني'
					  WHEN 5 THEN 'جماد أول'
					  WHEN 6 THEN 'جماد ثاني'
					  WHEN 7 THEN 'رجب'
					  WHEN 8 THEN 'شعبان'
					  WHEN 9 THEN 'رمضان'
					  WHEN 10 THEN 'شوال'
					  WHEN 11 THEN 'ذوالقعدة'
					  WHEN 12 THEN 'ذوالحجة'
					  END
	-- Return the result 
	RETURN @UmAlquraMonth
	--RETURN @GregorianDate
END



GO
