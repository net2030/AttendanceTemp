USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkException_GetById]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	get Work Exception by id
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkException_GetById]
(
 @WorkExceptionId      int = NULL
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
		SELECT [WorkExceptionId]
		      ,EmployeeId
			  ,[WorkExceptionTypeId]
			  ,[WorkExceptionBegDate]
			  ,[WorkExceptionEndDate]
			  ,[DepartmentId]
			  ,[Notes]
			  ,[AddedUserAccountId]
			  ,[UpdatedUserAccountId]
			  ,[AddedDate]
			  ,[UpdatedDate]
			  ,[VersionNo]
		  FROM  [Managements].[WorkException]
		 WHERE [WorkExceptionId] = @WorkExceptionId 		
		RETURN  
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
