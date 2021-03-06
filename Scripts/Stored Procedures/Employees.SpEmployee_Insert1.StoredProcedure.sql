USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Employees].[SpEmployee_Insert1]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert a new Employee record
-- =============================================
CREATE PROCEDURE [Employees].[SpEmployee_Insert1]
(
 @EmployeeId          int = NULL OUTPUT, 
 @Firstname        nvarchar(100) = NULL,
 @MiddleName        nvarchar(100) = NULL,
 @LastName nvarchar(100) = NULL,
 @EmployeeNameEnglish nvarchar(100) = '',
 @ManagerId            int = NULL,
 @JobTitle            nvarchar(100) = NULL,

 @GovId              NVarchar(15)='',
 @EmployeeNo nvarchar(15)='',
 @MobileNo     nvarchar(15) ='',
 @Sex        smallint=1,
 @Employer   int=1,
 @ContractType int=1,


 @BadgeNo             nvarchar(50) = NULL,
 @DepartmentId        int = NULL,
 @NationalityId       int = NULL,
 @HireDate            date = NULL,
 @IsActive            bit = NULL,
 @IsFingerRegistered  bit = 0, 


 @ActionId            int = 1,
 @LocationId          int=1,
 @RoleId              int=2,
 @UserName            Nvarchar(100),
 @EmailAddress            Nvarchar(100),
 @Password          nvarchar(50),
 @IsForcePasswordChange  bit,
 @WorkScheduleId      int=1,

 @PositionTypeId      int = NULL,
 @PositionId          int = NULL,
 @IsAllowOvertime     bit = 0,
 @IsExempt            bit = 0,
  
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

      

        IF EXISTS(SELECT EmployeeId 
                    FROM  [Employees].[Employee]
                   WHERE BadgeNo = @BadgeNo)                    
        BEGIN
		  SET @RMsgId = 206 -- الرقم الوظيفي مكرر
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
                		
       
        IF EXISTS(SELECT EmployeeId 
                    FROM  [Employees].[Employee]
                   WHERE EmployeeNo = @EmployeeNo)                    
        BEGIN
		  SET @RMsgId = 205 -- رقم البصمة مكرر  
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END
        

		IF EXISTS(SELECT EmployeeId 
                    FROM  [Employees].[Employee]
                   WHERE UserName = @UserName)                    
        BEGIN
		  SET @RMsgId = 128 -- أسم المستخدم مكرر  
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END


		
		IF EXISTS(SELECT EmployeeId 
                    FROM  [Employees].[Employee]
                   WHERE EmailAddress = @EmailAddress)                    
        BEGIN
		  SET @RMsgId = 204 -- البريد الالكتروني مكرر  
		  SET @FieldInError = 'EmployeeId'
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
				   ( Firstname ,
					 MiddleName ,
					 LastName ,
					 EmployeeNameEnglish,
					 ManagerId,
					 JobTitle ,
					 GovId,
					 EmployeeNo,
					 BadgeNo ,
					 DepartmentId,
					 NationalityId,
					 HireDate,
					 IsActive,
					 IsFingerRegistered, 
					 IsAllowOvertime,
					 IsExempt,

					 MobileNo,
					 ActionId,
					 LocationId,
					 RoleId,
					 UserName,
					 EmailAddress,
					 [Password],
					 IsForcePasswordChange  ,
					 WorkScheduleId,
					 PositionTypeId,
					 PositionId,
				    [AddedUserAccountId],
				    [UpdatedUserAccountId],
				    [AddedDate],
				    [UpdatedDate])
			 VALUES
				   (@Firstname ,
					 @MiddleName ,
					 @LastName ,
					 @EmployeeNameEnglish,
					 @ManagerId,
					 @JobTitle ,
					 @GovId,
					 @EmployeeNo,
					 @BadgeNo ,
					 @DepartmentId,
					 @NationalityId,
					 @HireDate,
					 @IsActive,
					 @IsFingerRegistered, 
					 @IsAllowOvertime,
					 @IsExempt,

					 @MobileNo,
					 @ActionId,
					 @LocationId,
					 @RoleId,
					 @UserName,
					 @EmailAddress,
					 @Password,
					 @IsForcePasswordChange ,
					 @WorkScheduleId,
					 1,
					 1,
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
