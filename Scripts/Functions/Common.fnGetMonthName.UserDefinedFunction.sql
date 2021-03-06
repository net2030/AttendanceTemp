USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [Common].[fnGetMonthName]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [Common].[fnGetMonthName]
(
@MonthNo int
)
RETURNS NVARCHAR(15)
AS
	BEGIN
	  DECLARE @MonthName as nvarchar(15)
	  
	  IF @MonthNo = 1
	  BEGIN
	    SET @MonthName = 'محرم'
	  END
	  ELSE
	  IF @MonthNo = 2
	  BEGIN
	    SET @MonthName = 'صفر'
	  END
	  ELSE
	  IF @MonthNo = 3
	  BEGIN
	    SET @MonthName = 'ربيع الأول'
	  END
	  ELSE	  
	  IF @MonthNo = 4
	  BEGIN
	    SET @MonthName = 'ربيع الآخر'
	  END
	  ELSE
	  IF @MonthNo = 5
	  BEGIN
	    SET @MonthName = 'جماد الأولى'
	  END
	  ELSE
	  IF @MonthNo = 6
	  BEGIN
	    SET @MonthName = 'جماد الآخرة'
	  END
	  ELSE
	  IF @MonthNo = 7
	  BEGIN
	    SET @MonthName = 'رجب'
	  END
	  ELSE
	  IF @MonthNo = 8
	  BEGIN
	    SET @MonthName = 'شعبان'
	  END
	  ELSE
	  IF @MonthNo = 9
	  BEGIN
	    SET @MonthName = 'رمضان'
	  END
	  ELSE	  
	  IF @MonthNo = 10
	  BEGIN
	    SET @MonthName = 'شوال'
	  END
	  ELSE
	  IF @MonthNo = 11
	  BEGIN
	    SET @MonthName = 'ذو القعدة'
	  END
	  ELSE
	  IF @MonthNo = 12
	  BEGIN
	    SET @MonthName = 'ذوالحجة'
	  END
	 	  	                    
	  RETURN RTRIM(LTRIM(@MonthName))
	END




GO
