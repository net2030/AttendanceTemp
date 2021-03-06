USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffPolicyEmployee_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert Employee into Work Schedule
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffPolicyEmployee_Insert]
(
 @TimeOffPolicyId int = NULL,
 @EmployeeId     int = NULL,
 
 @UserAccountId  int=1,
 @FieldInError   nvarchar(50) = '' OUTPUT,
 @RMsgId         int = NULL OUTPUT,
 @RMessage       nvarchar(200) = '' OUTPUT, 
 @RC             int = NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
	DECLARE @EmpId int

	DECLARE @PolicyType INT
	
	DECLARE @TimeOffPolicyDetailId int
	DECLARE @Earned decimal(18,6)=0.0
	DECLARE @Available decimal(18,6)=0.0
	DECLARE @CarryForward decimal(18,6)=0.0
	DECLARE @LastEarnedDate Date
	DECLARE @LastResetDate Date
	

	DECLARE @HireDate Date
    DECLARE @EffectiveDate Date
	DECLARE @InitialSet decimal(18,3)
	DECLARE @EarnPerDay decimal(18,3)
	DECLARE @EarnPerTime decimal(18,3)
	--DECLARE @periodDays DECIMAL(18,3)
	DECLARE @Today Date =GETDATE()
	DECLARE @EarnPeriodId int
	DECLARE @Count int=0
	DECLARE @TotalDays DECIMAL(6,3)=0.000

    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0


    BEGIN TRY

	    SELECT @PolicyType = TypeId FROM Managements.TimeOffPolicy WHERE TimeOffPolicyId = @TimeOffPolicyId

		IF NOT EXISTS(SELECT TimeOffPolicyId 
						FROM  [Managements].[TimeOffPolicy]
					   WHERE TimeOffPolicyId = @TimeOffPolicyId)                    
		BEGIN
		  SET @RMsgId = 165 -- رقم السياسة غير صحيح أو غير موجود 
		  SET @FieldInError = 'TimeOffPolicyId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END
	

	   IF NOT EXISTS(SELECT TimeOffPolicyDetailId 
						FROM  [Managements].[TimeOffPolicyDetail]
					   WHERE TimeOffPolicyId = @TimeOffPolicyId)                    
		BEGIN
		  SET @RMsgId = 170 -- يجب اضافة تفاصيل السياسة قبل اضافة الموظفين 
		  SET @FieldInError = 'TimeOffPolicyId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END

		IF  EXISTS(SELECT TB.TimeOffBalanceId 
					FROM  [Managements].[TimeOffBalance] TB
					JOIN Managements.TimeOffPolicyDetail TPD ON TB.TimeOffPolicyDetailId = TPD.TimeOffPolicyDetailId
					JOIN Managements.TimeOffPolicy TP ON TP.TimeOffPolicyId = TPD.TimeOffPolicyId
					WHERE TB.EmployeeId = @EmployeeId AND TP.TypeId = @PolicyType)  
					                  
		BEGIN
		  SET @RMsgId = 208 -- الموظف مسجل من قبل على سياسة أخرى
		  SET @FieldInError = 'TimeOffPolicyId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END
	
	 /*****************************************************
	   Cursor Declaration
	 *****************************************************/      
     DECLARE Curs_Employees CURSOR
     LOCAL FOR                                          
     SELECT TimeOffPolicyDetailId ,InitialSetToMinutes,EarnPeriodId,EarnMinutes,EffectiveDate
	 FROM Managements.TimeOffPolicyDetail 
	 WHERE TimeOffPolicyId=@TimeOffPolicyId
     FOR READ ONLY

	 /*****************************************************
	   Open Cursor
	 *****************************************************/      
     OPEN Curs_Employees
     FETCH NEXT FROM Curs_Employees INTO @TimeOffPolicyDetailId,@InitialSet,@EarnPeriodId,@EarnPerTime,@EffectiveDate
     WHILE @@Fetch_Status = 0 
     BEGIN

	 IF NOT EXISTS(SELECT * FROM Managements.TimeOffBalance WHERE TimeOffPolicyDetailId=@TimeOffPolicyDetailId AND EmployeeId=@EmployeeId)
	   BEGIN

	     IF @EffectiveDate<=@Today

		 BEGIN
		 
			  SELECT @HireDate=HireDate FROM Employees.Employee WHERE EmployeeId=@EmployeeId
			--  SELECT @periodDays=DaysNo FROM Periods WHERE PeriodId=@EarnPeriodId
			  --SELECT @EarnPerDay=@EarnPerTime/@periodDays

			
				  IF @HireDate<=@EffectiveDate
					BEGIN
						 SET @LastEarnedDate=@EffectiveDate
						 SET @LastResetDate=@EffectiveDate
                         WHILE @LastEarnedDate<@Today
							BEGIN
								SET @Count=@Count+1
								IF @EarnPeriodId=1
								SET @LastEarnedDate=DATEADD(MONTH, 1,@LastEarnedDate)
								ELSE 
								IF @EarnPeriodId=2
								SET @LastEarnedDate=DATEADD(YEAR, 1,@LastEarnedDate)
							END
					END
				  ELSE 
				  BEGIN
						 SET @LastEarnedDate=@HireDate
						 SET @LastResetDate=@HireDate
                         WHILE @LastEarnedDate<@Today
							BEGIN
								SET @Count=@Count+1
								IF @EarnPeriodId=1
								SET @LastEarnedDate=DATEADD(MONTH, 1,@LastEarnedDate)
								ELSE 
								IF @EarnPeriodId=2
								SET @LastEarnedDate=DATEADD(YEAR, 1,@LastEarnedDate)
							END

					END     
			
			
			
			IF @Count>0 AND @LastEarnedDate>@Today
			  BEGIN
				 SET @Count=@Count-1
				 IF @EarnPeriodId=1
				 SET @LastEarnedDate=DATEADD(MONTH, -1,@LastEarnedDate)
				 ELSE 
				 IF @EarnPeriodId=2
				 SET @LastEarnedDate=DATEADD(YEAR, -1,@LastEarnedDate)
				  SET @Available=@EarnPerTime * @count
				  SET @Earned =@EarnPerTime
			 END
			 ELSE
			 IF @Count>0 AND @LastEarnedDate<=@Today
             BEGIN
				  SET @Available=@EarnPerTime * @count
				  SET @Earned =@EarnPerTime
			 END
					
			
		END

		IF  @InitialSet IS NOT NULL AND @InitialSet>0
			BEGIN
				SET @Available=@Available + @InitialSet
				SET @Earned =@InitialSet
			END
		

		 INSERT INTO Managements.TimeOffBalance
		 (EmployeeId,
		 TimeOffPolicyDetailId,
		 Earned,
		 Available,
		 LastEarnedDate,
		 LastResetDate, 
		 AddedUserAccountId,
		 UpdatedUserAccountId,
		 AddedDate,
		 UpdatedDate)
		 VALUES   (@EmployeeId,
		           @TimeOffPolicyDetailId,
			       @Earned,
				   @Available,
				   @LastEarnedDate,
				   @LastResetDate,
				   @UserAccountId,
				   @UserAccountId,
				   GetDate(),
				   GetDate())
		 SET @RowEffected = @@RowCount

		

	   END
       FETCH NEXT FROM Curs_Employees INTO @TimeOffPolicyDetailId,@InitialSet,@EarnPeriodId,@EarnPerTime,@EffectiveDate
     END
     
     -- close cursor
     CLOSE Curs_Employees
     -- de-allocate cursor
     DEALLOCATE Curs_Employees

		-- Save Number of Rows Affected
		
		
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
		  SET @RMsgId = 166 -- 
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
