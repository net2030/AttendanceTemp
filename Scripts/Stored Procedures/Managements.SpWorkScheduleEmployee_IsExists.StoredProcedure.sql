USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkScheduleEmployee_IsExists]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	check Work Schedule Employee
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkScheduleEmployee_IsExists]
(
 @WorkScheduleId int = NULL,
 @EmployeeId     int = NULL,
 
 @IsFound        bit = 0 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
     
    BEGIN TRY	    	         	        
		IF EXISTS(SELECT EmployeeId
		            FROM  [Managements].[WorkScheduleEmployeeLink]
	               WHERE [WorkScheduleId] = @WorkScheduleId
		             AND [EmployeeId] = @EmployeeId)
		BEGIN
		  SET @IsFound = 1
		END		
	  RETURN    
    END TRY
    BEGIN CATCH
       EXECUTE [Common].[SpCommon_LogError];
       RETURN    
    END CATCH
END






GO
