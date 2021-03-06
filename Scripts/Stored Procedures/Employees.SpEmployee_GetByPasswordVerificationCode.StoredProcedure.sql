USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_GetByPasswordVerificationCode]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Find Employee record
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_GetByPasswordVerificationCode]
(
 @PasswordVerificationCode          uniqueidentifier = NULL, 
 
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
    --    IF NOT EXISTS(SELECT EmployeeId 
    --                    FROM  [Employees].[Employee]
    --                   WHERE UserName = @UserName)                    
    --    BEGIN
		  --SET @RMsgId = 120 -- رقم الموظف غير صحيح أو غير موجود 
		  --SET @FieldInError = 'EmployeeId'
		  --SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  --RETURN        
    --    END 
                        
        SELECT E.EmployeeId, 
               E.FirstName, 
               E.MiddleName, 
			   E.LastName,
               E.ManagerId, 
               E.JobTitle, 
               E.PositionTypeId, 
               E.PositionId,
               E.BadgeNo,
               E.DepartmentId, 
               E.NationalityId,
               E.HireDate,
               E.IsActive,
               E.IsFingerRegistered,
               E.Picture,
               E.ImageFileName,
               E.ActionId,
               E.IsGatepassApproval,

			   E.LocationId,
			   E.RoleId,
			   R.RoleName,
			   E.UserName,
			   e.AllowedAccessFromIP,
			  E.EmailAddress,
			   E.IsForcePasswordChange,
			  E.WorkScheduleId,
			  e.AddedDate,
			   E.Lang
          FROM Employees.Employee E 
		  JOIN Security.Role R ON E.RoleId=R.RoleId
         WHERE E.PasswordVerificationCode = @PasswordVerificationCode	AND IsActive=1	  
	     		      
		
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
