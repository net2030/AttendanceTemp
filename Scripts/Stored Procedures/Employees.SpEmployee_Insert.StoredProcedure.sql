USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_Insert]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert a new Employee record
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_Insert]
(
 @EmployeeId          int = NULL OUTPUT, 
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
        IF NOT EXISTS(SELECT DepartmentId 
                        FROM  [Employees].[Department]
                       WHERE DepartmentId = @DepartmentId)                    
        BEGIN
		  SET @RMsgId = 116 -- رقم القسم/الإدارة غير صحيح أو غير موجود 
		  SET @FieldInError = 'DepartmentId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 

        IF (@EmployeeName = '' OR @EmployeeName IS NULL)            
        BEGIN
		  SET @RMsgId = 121 -- يجب إدخال أسم الموظف باللغة العربية 
		  SET @FieldInError = 'EmployeeNameArabic'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 

        IF EXISTS(SELECT EmployeeId 
                    FROM  [Employees].[Employee]
                   WHERE EmployeeName = @EmployeeName)                    
        BEGIN
		  SET @RMsgId = 142 -- أسم الموظف مكرر  
		  SET @FieldInError = 'EmployeeId'
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
                        
		INSERT INTO  [Employees].[Employee]
				   ([EmployeeName],
				    [EmployeeNameEnglish],
				    [ManagerId],
				    [JobTitle],
				    [PositionTypeId],
				    [PositionId],
				    [BadgeNo],
				    [DepartmentId],
				    [NationalityId],
				    [HireDate],
				    [IsActive],
				    [IsFingerRegistered],
				    [Picture],
				    [ImageFileName],
				    [ActionId],
				    [IsGatepassApproval],
				    [AddedUserAccountId],
				    [UpdatedUserAccountId],
				    [AddedDate],
				    [UpdatedDate])
			 VALUES
				   (@EmployeeName, 
				    @EmployeeNameEnglish, 
				    @ManagerId,
				    @JobTitleId,
				    @PositionTypeId,
				    @PositionId,
				    @BadgeNo,
				    @DepartmentId,
				    @NationalityId,
				    @HireDate,
				    @IsActive,
				    @IsFingerRegistered,
				    @Picture,
				    @ImageFileName,
				    @ActionId,
				    @IsGatepassApproval,
				    @UserAccountId,
				    @UserAccountId,
				    GETDATE(),				    				    
				    GETDATE())      		        
				        
		-- Save The New ID Value
		SET @EmployeeId = SCOPE_IDENTITY()
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
		END
		ELSE
		BEGIN -- Failed
		  SET @RMsgId = 2 --  فشلت عملية تحديث البيانات
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
       END      
    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         SET @RMsgId = 3  --  حدث خطاء في إجراء قاعدة البيانات - الرجاء الإتصال بمسئول النظام للمساعدة
		 SET @RMessage = (SELECT MsgText 
		                    FROM  [common].[CustomMessage] 
		                   WHERE [MsgId] = @RMsgId)
        RETURN    
    END CATCH
END




GO
