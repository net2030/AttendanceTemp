USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_Update]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Update Employee record
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_Update]
(
 @EmployeeId          int = NULL, 
 @EmployeeName        nvarchar(100) = NULL,
 @EmployeeNameEnglish nvarchar(100) = NULL,
 @ManagerId            int = NULL,
 @JobTitleId          int = NULL,
 @PositionTypeId      int = NULL,
 @PositionId          int = NULL,
 @BadgeNo             nvarchar(50) = NULL,
 @DepartmentId        int = NULL,
 @NationalityId       int = NULL,
 @HireDate            date = NULL,
 @IsActive            bit = NULL,
 @IsFingerRegistered  bit = 0, 
 @Picture             varbinary(max) = NULL,
 @ImageFileName       nvarchar(100) = NULL,
 @ActionId            int = 1,  
 @IsGatepassApproval  bit = 0,
 @VersionNo           Timestamp = NULL,
 @UserAccountId       int = NULL,
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

        IF (@EmployeeName = '' OR @EmployeeName IS NULL)            
        BEGIN
		  SET @RMsgId = 121 -- يجب إدخال أسم الموظف باللغة العربية 
		  SET @FieldInError = 'EmployeeName'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 

        IF EXISTS(SELECT EmployeeId 
                    FROM  [Employees].[Employee]
                   WHERE EmployeeId <> @EmployeeId
                     AND EmployeeName = @EmployeeName)                    
        BEGIN
		  SET @RMsgId = 142 -- أسم الموظف مكرر  
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
                        
        IF NOT EXISTS(SELECT DepartmentId 
                        FROM  [Employees].[Department]
                       WHERE DepartmentId = @DepartmentId)                    
        BEGIN
		  SET @RMsgId = 116 -- رقم القسم/الإدارة غير صحيح أو غير موجود 
		  SET @FieldInError = 'DepartmentId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 
		
        IF NOT EXISTS(SELECT JobTitleId 
                        FROM  [Employees].[JobTitle]
                       WHERE JobTitleId = @JobTitleId)                    
        BEGIN
		  SET @RMsgId = 101 -- رقم وصف الوظيفة غير صحيح أو غير موجود 
		  SET @FieldInError = 'JobTitleId'
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE MsgId = @RMsgId)
		  RETURN        
        END  
        
        IF NOT EXISTS(SELECT NationalityId 
                        FROM  [Employees].[Nationality]
                       WHERE NationalityId = @NationalityId)                    
        BEGIN
		  SET @RMsgId = 106 -- رقم الجنسية غير صحيح أو غير موجود 
		  SET @FieldInError = 'NationalityId'
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE MsgId = @RMsgId)
		  RETURN        
        END

        IF NOT EXISTS(SELECT PositionId 
                        FROM  [Employees].[Position]
                       WHERE PositionId = @PositionId)                    
        BEGIN
		  SET @RMsgId = 109 -- رقم الرتبة/المرتبة غير صحيح أو غير موجود 
		  SET @FieldInError = 'PositionId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
                        
        IF (@ManagerId IS NOT NULL AND @ManagerId <> 0 AND @ManagerId <> -1)
        BEGIN
			IF NOT EXISTS(SELECT EmployeeId 
							FROM  [Employees].[Employee]
						   WHERE EmployeeId = @ManagerId)                    
			BEGIN
			  SET @RMsgId = 119 -- رقم المدير المباشر غير صحيح أو غير موجود 
			  SET @FieldInError = 'ManagerId'
			  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
			  RETURN           
			END			                   
        END

        IF @ManagerId = 0
        BEGIN
          SET @ManagerId = NULL
        END

        IF NOT EXISTS(SELECT PositionTypeId 
                        FROM  [Employees].[PositionType]
                       WHERE PositionTypeId = @PositionTypeId)                    
        BEGIN
		  SET @RMsgId = 109 -- رقم نوع الرتبة/المرتبة غير صحيح أو غير موجود 
		  SET @FieldInError = 'PositionTypeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END

        IF NOT EXISTS(SELECT ActionId 
                        FROM  [Managements].[BehaviorAction]
                       WHERE ActionId = @ActionId)                    
        BEGIN
		  SET @RMsgId = 139 -- رقم قاعدة تطبيق الدوام غير صحيح أو غير موجود 
		  SET @FieldInError = 'PositionTypeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
                        
		UPDATE  [Employees].[Employee]
		   SET [EmployeeName]   = @EmployeeName,
			   [EmployeeNameEnglish]  = @EmployeeNameEnglish,
			   [ManagerId]             = @ManagerId,
			   [JobTitle]           = @JobTitleId,
			   [PositionTypeId]       = @PositionTypeId,
			   [PositionId]           = @PositionId,
			   [BadgeNo]              = @BadgeNo,
			   [DepartmentId]         = @DepartmentId,
			   [NationalityId]        = @NationalityId,
			   [HireDate]             = @HireDate,
			   [IsActive]             = @IsActive,
			   [IsFingerRegistered]   = @IsFingerRegistered,
			   [Picture]              = @Picture,
			   [ImageFileName]        = @ImageFileName,
			   [ActionId]             = @ActionId,
			   [IsGatepassApproval]  = @IsGatepassApproval,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate]          = GETDATE()
	     WHERE EmployeeId = @EmployeeId
	       AND VersionNo  = @VersionNo		   		     		      
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE [MsgId] = @RMsgId)
		  RETURN
		END
		ELSE
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [EmployeeId] 
		                                      FROM  [Employees].[Employee]
		                                     WHERE [EmployeeId] = @EmployeeId
	                                           AND [VersionNo]  = @VersionNo))
		BEGIN
		  SET @RMsgId = 5  -- تم تحديث السجل من قبل مستخدم آخر
		  SET @RMessage = (SELECT MsgText 
		                   FROM  [common].[CustomMessage] 
		                   WHERE [MsgId] = @RMsgId)
		  RETURN		
		END			
		ELSE
		BEGIN -- Failed
		  SET @RMsgId = 2  --  فشلت عملية تحديث البيانات
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE [MsgId] = @RMsgId)
		  RETURN
       END      
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         SET @RMsgId = 3  --  حدث خطاء في إجراء قاعدة البيانات - الرجاء الإتصال بمسئول النظام للمساعدة
		 SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE [MsgId] = @RMsgId)
        RETURN    
    END CATCH
END






GO
