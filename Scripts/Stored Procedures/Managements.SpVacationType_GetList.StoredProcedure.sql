USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacationType_GetList]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 10/04/2019
-- Description:	get Work VacationType Type list
-- =============================================
CREATE PROCEDURE [Managements].[SpVacationType_GetList]
(
@Lang         char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
        
       
    BEGIN TRY	
	    if @lang = 'en'
			SELECT [TypeId],
				   [TypeNameEN] AS TypeName
			  FROM [Managements].[VacationType]		
			  UNION 
			  SELECT 0,'Choose'
		else
		  SELECT [TypeId],
			   TypeName
		  FROM  [Managements].[VacationType]		
		  UNION 
		  SELECT 0,'إختر'

		     			        
		
	   RETURN
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
