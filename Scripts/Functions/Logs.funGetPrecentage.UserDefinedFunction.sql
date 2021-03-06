USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [Logs].[funGetPrecentage]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 27/03/2011
-- Description:	calculate a Precentage
-- =============================================
CREATE FUNCTION [Logs].[funGetPrecentage]
(
 @ActualHours int = 0,
 @WorkingHours int = 0 
)
RETURNS nvarchar(6)
AS
BEGIN
	-- return variable here
	DECLARE @chrPrecentage nvarchar(6) = ''
	DECLARE @decPrecentage decimal(6,2) = 0.0

    IF (@ActualHours IS NULL OR @ActualHours = 0 OR @ActualHours < 0)
    BEGIN
      SET @chrPrecentage = ''
    END
    ELSE
    IF (@WorkingHours IS NULL OR @WorkingHours = 0OR @WorkingHours < 0)
    BEGIN
      SET @chrPrecentage = ''
    END
    ELSE    
    BEGIN
		DECLARE @decActualHours decimal(6,2) =  CAST(@ActualHours as decimal(6,2))
		DECLARE @decWorkingHours decimal(6,2) = CAST(@WorkingHours as decimal(6,2))
	    
		IF (@WorkingHours = 0 OR @WorkingHours IS NULL)
		BEGIN
		  SET @chrPrecentage = ''
		END
		ELSE
		IF (@ActualHours = 0 OR @ActualHours IS NULL)
		BEGIN
		  SET @chrPrecentage = ''
		END
		ELSE
		BEGIN
		  SET @decPrecentage = (@decActualHours / @decWorkingHours) * 100 
		  SET @chrPrecentage = CAST(@decPrecentage as nvarchar(6)) + '%'
		END    
    END    
        
	-- Return the result of the function
	RETURN @chrPrecentage
END




GO
