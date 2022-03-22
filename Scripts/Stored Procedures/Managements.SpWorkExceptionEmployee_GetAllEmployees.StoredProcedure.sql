USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkExceptionEmployee_GetAllEmployees]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get All employees 
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkExceptionEmployee_GetAllEmployees]
(
 @WorkExceptionId  int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
     BEGIN TRY
       SELECT [WorkExceptionId],
              [EmployeeId]
         FROM  [Managements].[WorkExceptionEmployeeLink]
  	    WHERE WorkExceptionId = @WorkExceptionId	  		  		    
  	  
	    RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
