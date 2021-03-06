USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[spInsertEmployeeFromLDAP]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 08/02/2018
-- Description:	Insert Employee From Active Directory
-- =============================================
CREATE PROCEDURE [Employees].[spInsertEmployeeFromLDAP]
(
 @EmployeeId   int = NULL OUTPUT, 
 @SAMAccountName nvarchar(100) = NULL,
 @EnglishName nvarchar(100) = NULL,
 @ArabicName nvarchar(100) = NULL,
 @GovId nvarchar(100) = NULL,
 @EmployeeNo nvarchar(100) = NULL,
 @FirstName  nvarchar(100) = NULL,
 @JobTitle nvarchar(100) = NULL,
 @LastName nvarchar(100) = NULL,
 @homePhone nvarchar(100) = NULL,
 @mobile nvarchar(100) = NULL,
 @mail nvarchar(100) = NULL,
 @ParentDepartment nvarchar(100) = NULL,
 @Department nvarchar(100) = NULL,
 @manager nvarchar(100) = NULL

)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations

	DECLARE @DepartmentId int =0
	DECLARE @ParentDepartmentId int =0
    
   
       
    BEGIN TRY	

  IF @EmployeeNo IS NULL 
   RETURN

 INSERT INTO [dbo].[EmployeeTemp]
           ([SAMAccountName]
           ,[EnglishName]
           ,[ArabicName]
           ,[GovId]
           ,[EmployeeNo]
           ,[FirstName]
           ,[JobTitle]
           ,[LastName]
           ,[homePhone]
           ,[mobile]
           ,[mail]
           ,[ParentDepartment]
           ,[Department]
           ,[manager])
     VALUES
           (@SAMAccountName,
		    @EnglishName,
			@ArabicName,
			@GovId,
			@EmployeeNo,
			@FirstName,
			@JobTitle,
			@LastName,
			@homePhone,
			@mobile,
			@mail,
			@ParentDepartment,
			@Department,
			@manager)




    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
       
        RETURN    
    END CATCH
END







GO
