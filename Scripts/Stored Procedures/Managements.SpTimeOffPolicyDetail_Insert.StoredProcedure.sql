USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffPolicyDetail_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert new Work Schedule Day
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffPolicyDetail_Insert]
(
 @TimeOffPolicyId INT,
 @GatepassTypeId INT,
 @EarnPeriodId INT,
 @ResetToZeroPeriodId INT,
 @InitialSetToMinutes DECIMAL(6,2),
 @ResetToMinutes      DECIMAL(6,2),
 @EarnMinutes         DECIMAL(6,2),
 @EffectiveDate       Date,
 @IsCarryForward      bit=0,
 @MinValue        DECIMAL(6,2),
 @MaxValue        DECIMAL(6,2),
 @UserAccountId  int = NULL, 
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
    DECLARE @TimeOffPolicyDetailId int

    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0


    BEGIN TRY

	IF  EXISTS(SELECT TimeOffPolicyId 
						FROM [Managements].TimeOffPolicyDetail
					   WHERE TimeOffPolicyId = @TimeOffPolicyId AND GatepassTypeId=@GatepassTypeId)                    
		BEGIN
		  SET @RMsgId = 162 -- نوع السياسة  مكرر 
		  SET @FieldInError = 'WorkScheduleId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END	       

		IF NOT EXISTS(SELECT TimeOffPolicyId 
						FROM [Managements].TimeOffPolicy
					   WHERE TimeOffPolicyId = @TimeOffPolicyId)                    
		BEGIN
		  SET @RMsgId = 164 -- نوع السياسة غير موجود 
		  SET @FieldInError = 'WorkScheduleId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
		END	         	

		
		        
		INSERT INTO [Managements].TimeOffPolicyDetail
				   ([TimeOffPolicyId],
					[GatepassTypeId],
					[EarnPeriodId],
					[ResetToZeroPeriodId],
					[InitialSetToMinutes],
					[ResetToMinutes],
					[EarnMinutes],
					EffectiveDate,
					IsCarryForward,
					MinValue,
					MaxValue ,
				    [AddedUserAccountId],
				    [UpdatedUserAccountId],
				    [AddedDate],
				    [UpdatedDate])
			 VALUES
				   (@TimeOffPolicyId ,
					@GatepassTypeId ,
					@EarnPeriodId ,
					@ResetToZeroPeriodId ,
					@InitialSetToMinutes ,
					@ResetToMinutes,
					@EarnMinutes,
					@EffectiveDate,
					@IsCarryForward,
					@MinValue ,
					@MaxValue ,
				    @UserAccountId,
				    @UserAccountId, 
				    GETDATE(),
				    GETDATE())
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		SET @TimeOffPolicyDetailId=SCOPE_IDENTITY()
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		--  RETURN
		END
		ELSE
		BEGIN -- Failed
		  SET @RMsgId = 2 --  فشلت عملية تحديث البيانات
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		  RETURN
       END     
	   
	   --Apply this detail to employees

	   IF EXISTS (SELECT EmployeeId 
	              From Managements.TimeOffBalance TOB
				  JOIN  Managements.TimeOffPolicyDetail TOPD ON TOB.TimeOffPolicyDetailId=TOPD.TimeOffPolicyDetailId
				  WHERE TOPD.TimeOffPolicyId=@TimeOffPolicyId)
        BEGIN
		    
			DECLARE @EmployeeId int
			DECLARE @HireDate Date
			
			DECLARE @periodDays DECIMAL(6,3)
			DECLARE @Today Date =GETDATE()
			DECLARE @Available  decimal(6,3)=0.000
	        DECLARE @CarryForward  decimal(6,3)=0.000
	        DECLARE @LastEarnedDate Date
			DECLARE @LastResetDate Date
			DECLARE @Earned decimal(6,3)=0.000
		    DECLARE @Count int=0
			DECLARE @TotalDays DECIMAL(6,3)=0.000

			  /*****************************************************
			   Cursor Declaration
			 *****************************************************/      
			 DECLARE Curs_Employees CURSOR
			 LOCAL FOR   
			                                        
			 SELECT Distinct EmployeeId 
	         From Managements.TimeOffBalance TOB
		     JOIN  Managements.TimeOffPolicyDetail TOPD ON TOB.TimeOffPolicyDetailId=TOPD.TimeOffPolicyDetailId
			 WHERE TOPD.TimeOffPolicyId=@TimeOffPolicyId

			 FOR READ ONLY

			 /*****************************************************
			   Open Cursor
			 *****************************************************/      
			 OPEN Curs_Employees
			 FETCH NEXT FROM Curs_Employees INTO @EmployeeId
			 WHILE @@Fetch_Status = 0 
			 BEGIN

			  IF @EffectiveDate<=@Today
				 BEGIN
		 
				  SELECT @HireDate=HireDate FROM Employees.Employee WHERE EmployeeId=@EmployeeId
				  SELECT @periodDays=DaysNo FROM Periods WHERE PeriodId=@EarnPeriodId
				  


			
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
				  SET @Available=@EarnMinutes * @count
				  SET @Earned =@EarnMinutes
			 END
			 ELSE
			 IF @Count>0 AND @LastEarnedDate<=@Today
             BEGIN
				  SET @Available=@EarnMinutes * @count
				  SET @Earned =@EarnMinutes
			 END

		END
		 
		   IF  @InitialSetToMinutes IS NOT NULL AND @InitialSetToMinutes>0
			BEGIN
				SET @Available=@Available + @InitialSetToMinutes
				SET @Earned =@InitialSetToMinutes
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

			 FETCH NEXT FROM Curs_Employees INTO @EmployeeId
			 END

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
