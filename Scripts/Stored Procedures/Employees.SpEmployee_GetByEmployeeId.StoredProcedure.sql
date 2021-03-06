USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_GetByEmployeeId]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Find Employee record
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_GetByEmployeeId]
(
 @EmployeeId          int = NULL, 
 
 @FieldInError        nvarchar(50) = '' OUTPUT,
 @RMsgId              int = NULL OUTPUT,
 @RMessage            nvarchar(200) = '' OUTPUT, 
 @RC                  int = NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
    
    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0


    BEGIN TRY
        IF NOT EXISTS(SELECT EmployeeId 
                        FROM  [Employees].[Employee]
                       WHERE EmployeeId = @EmployeeId)                    
        BEGIN
		  SET @RMsgId = 120 -- رقم الموظف غير صحيح أو غير موجود 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 
                        
         SELECT EmployeeId, --0
               FirstName, 
               MiddleName, 
			   LastName,
			   EmployeeNameEnglish,
               ManagerId, 
               JobTitle, 
               PositionTypeId, 
               PositionId,
               BadgeNo,
               DepartmentId, 
               NationalityId,
               HireDate,
               IsActive,
               IsFingerRegistered,
               Picture,
               ImageFileName,
               ActionId,
			   IsAllowOvertime,
			   IsExempt,
    

			   LocationId, --19
			   RoleId,
			   UserName,
			   EmailAddress,
			   IsForcePasswordChange,
			   WorkScheduleId,

			   GovId , --25
			   EmployeeNo,
			   MobileNo,
			   Sex,
			   Employer,
			   ContractType,

               AddedUserAccountId,  --31
               UpdatedUserAccountId,
               AddedDate, 
               UpdatedDate,
               VersionNo  
          FROM Employees.Employee 
         WHERE EmployeeId = @EmployeeId		        		      
		
		 SET @RC = 0
		 
         RETURN   
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         SET @RMsgId = 3  --  حدث خطاء في إجراء قاعدة البيانات - الرجاء الإتصال بمسئول النظام للمساعدة
		 SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE [MsgId] = @RMsgId)
        RETURN    
    END CATCH
END






GO
