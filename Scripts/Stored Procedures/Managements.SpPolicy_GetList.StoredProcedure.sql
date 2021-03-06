USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpPolicy_GetList]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Plocies List by department id
-- =============================================
CREATE PROCEDURE [Managements].[SpPolicy_GetList]
(
 @Lang  char(2) = 'ar'
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
       
	   BEGIN TRY

	  
	    
		IF @Lang='en'

		SELECT PolicyId, PolicyNameEN as PolicyName 
		FROM Managements.Policy               
		UNION ALL
		SELECT 0, 'Select policy'	
		ORDER BY PolicyId   

		ELSE
		
		SELECT PolicyId, PolicyName 
		FROM Managements.Policy 	               
		UNION ALL
		SELECT 0, 'أختر سياسة جدول العمل'		
		ORDER BY PolicyId
		    
		
		        
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
