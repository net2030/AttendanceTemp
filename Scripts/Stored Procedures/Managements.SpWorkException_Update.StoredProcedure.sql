USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkException_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Update Work Exception
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkException_Update]
(
 @WorkExceptionId      int = NULL, 
 @EmployeeId           int = NULL,
 @WorkExceptionTypeId  int = NULL,
 @DepartmentId         int = NULL, 
 @WorkExceptionBegDate date = NULL,
 @WorkExceptionEndDate date = NULL,
 @Notes                nvarchar(200) = NULL,
 @VersionNo            timestamp = NULL,
 
 @UserAccountId        int = NULL,
 @FieldInError         nvarchar(50) = '' OUTPUT,
 @RMsgId               int = NULL OUTPUT,
 @RMessage             nvarchar(200) = '' OUTPUT, 
 @RC                   int = NULL OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected int
    DECLARE @Available int
	DECLARE @Pending int 
	DECLARE @IsApproved bit=0
	DECLARE @WorkExceptionBegDateOld DATE
	DECLARE @WorkExceptionEndDateOld DATE
	DECLARE @MinValue int
	DECLARE @MaxValue int
	DECLARE @Period int 
	DECLARE @CarryForward decimal(6, 2)
	DECLARE @Used decimal(6, 2)
	DECLARE @BalanceID INT
	DECLARE @BalanceOfUpdatedRecord  decimal(6, 2) 
	DECLARE @WorkExceptionAddedDate AS DATETIME
	DECLARE @PolicyDate DATETIME
	DECLARE @BalanceAddedDate DATETIME

    BEGIN TRY	

    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0

	SELECT @MinValue= Convert(int,TOPD.MinValue),
	        @MaxValue= Convert(int,TOPD.MaxValue),
	        @Available= Convert(int,TOB.Available),
	        @CarryForward = Convert(int,TOB.CarryForward),
			@Used = Used,
	        @PolicyDate = GP.AddedDate,
			@BalanceAddedDate = TOB.AddedDate
	FROM  Managements.TimeOffBalance TOB 
			INNER JOIN Managements.TimeOffPolicyDetail TOPD
			ON TOB.TimeOffPolicyDetailId = TOPD.TimeOffPolicyDetailId 
			INNER JOIN   Managements.WorkExceptionType
			ON TOPD.GatepassTypeId = Managements.WorkExceptionType.WorkExceptionTypeId
			INNER JOIN Managements.TimeOffPolicy GP ON GP.TimeOffPolicyId=TOPD.TimeOffPolicyId
    WHERE TOB.EmployeeId=@EmployeeID AND  GP.TypeId=2 AND Managements.WorkExceptionType.WorkExceptionTypeId=@WorkExceptionTypeId

	--Check If the update after approval
    SELECT @IsApproved =IsApproved,
	       @WorkExceptionBegDateOld=WorkExceptionBegDate,
		   @WorkExceptionEndDateOld=WorkExceptionEndDate
	FROM Managements.WorkException 
	WHERE WorkExceptionId=@WorkExceptionId


	IF @MaxValue IS NOT NULL AND @MaxValue<>0 AND DATEDIFF(day,@WorkExceptionBegDate,@WorkExceptionEndDate)+1  >@MaxValue AND @WorkExceptionAddedDate > @BalanceAddedDate
	  BEGIN
		  SET @RMsgId = 175 --يجب ان يكون الطلب أقل من او يساوي الحد الأقصى 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText + '   (' +  CAST(@MaxValue as nvarchar(3)) + '  يوم)'  FROM  [common].[CustomMessage]   WHERE MsgId = @RMsgId)
		  RETURN        
      END   
	  
	IF @MinValue IS NOT NULL AND @MinValue<>0 AND DATEDIFF(day,@WorkExceptionBegDate,@WorkExceptionEndDate)+1  <@MinValue  AND @WorkExceptionAddedDate > @BalanceAddedDate
	  BEGIN
		  SET @RMsgId = 176 --ايجب أن يكون الطلب أكبر من او يساوي الحد الأدنى
		  SET @FieldInError = 'EmployeeId'
		   SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText + '   (' +  CAST(@MinValue as nvarchar(3)) + '  يوم)'  FROM  [common].[CustomMessage]   WHERE MsgId = @RMsgId)
		  RETURN        
      END   


     IF EXISTS(SELECT WorkExceptionId
		          FROM Managements.WorkException 
				  WHERE(WorkExceptionId<> @WorkExceptionId AND EmployeeId=@EmployeeId AND (@WorkExceptionBegDate<=WorkExceptionBegDate AND @WorkExceptionEndDate>=WorkExceptionBegDate)) --New Will overlap befor
					OR (WorkExceptionId<> @WorkExceptionId AND EmployeeId=@EmployeeId AND (@WorkExceptionBegDate<=WorkExceptionEndDate AND @WorkExceptionEndDate>=WorkExceptionEndDate)) --New Will overlap after
				    OR (WorkExceptionId<> @WorkExceptionId AND EmployeeId=@EmployeeId AND (@WorkExceptionBegDate>=WorkExceptionBegDate AND @WorkExceptionEndDate<=WorkExceptionEndDate)) --New will contained by the old Or equal
					OR (WorkExceptionId<> @WorkExceptionId AND EmployeeId=@EmployeeId AND (@WorkExceptionBegDate<=WorkExceptionBegDate AND @WorkExceptionEndDate>=WorkExceptionEndDate)) --New Will include the old or equal
				   	   )

        BEGIN
		  SET @RMsgId = 172 -- الإجازة مكررة 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 


			IF EXISTS(SELECT VacationId
		          FROM Managements.Vacation 
				  WHERE(EmployeeId=@EmployeeId AND (@WorkExceptionBegDate<=EffectiveDate AND @WorkExceptionEndDate>=EffectiveDate)) --New Will overlap befor
					OR (EmployeeId=@EmployeeId AND (@WorkExceptionBegDate<=DateExpire AND @WorkExceptionEndDate>=DateExpire)) --New Will overlap after
				    OR (EmployeeId=@EmployeeId AND (@WorkExceptionBegDate>=EffectiveDate AND @WorkExceptionEndDate<=DateExpire)) --New will contained by the old Or equal
					OR (EmployeeId=@EmployeeId AND (@WorkExceptionBegDate<=EffectiveDate AND @WorkExceptionEndDate>=DateExpire)) --New Will include the old or equal
				   	   )
				 
        BEGIN
		  SET @RMsgId = 203 -- الإستثناء يتعارض مع اجازة بنفس التاريخ 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 

    IF NOT EXISTS(SELECT WorkExceptionId 
                    FROM  [Managements].[WorkException]
                   WHERE WorkExceptionId = @WorkExceptionId)                    
    BEGIN
	  SET @RMsgId = 152 -- رقم الإستثناء غير صحيح أو غير موجود 
	  SET @FieldInError = 'WorkExceptionId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END	

    IF NOT EXISTS(SELECT EmployeeId 
                    FROM  Employees.Employee
                   WHERE EmployeeId = @EmployeeId)                    
    BEGIN
	  SET @RMsgId = 120 -- رقم الموظف غير صحيح أو غير موجود 
	  SET @FieldInError = 'WorkExceptionTypeId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END	

    IF NOT EXISTS(SELECT WorkExceptionTypeId 
                    FROM  [Managements].[WorkExceptionType]
                   WHERE WorkExceptionTypeId = @WorkExceptionTypeId)                    
    BEGIN
	  SET @RMsgId = 153 -- رقم نوع الإستثناء غير صحيح أو غير موجود 
	  SET @FieldInError = 'WorkExceptionTypeId'
	  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
	  RETURN        
    END	
    
	
	 
	 -- Check Balance
       
	 IF @Available IS NOT NULL
	   BEGIN

	   SELECT @Pending=SUM(DATEDIFF(DAY,WorkExceptionBegDate,WorkExceptionEndDate) + 1) FROM Managements. WorkException WHERE EmployeeId=@EmployeeId AND @WorkExceptionTypeId=@WorkExceptionTypeId AND IsApproved=0 AND IsRejected=0
	   IF @Pending IS  NULL
	   SET  @Pending = 0.00

	    IF @IsApproved = 1 --Include The Updated Record Balance
		 SET @BalanceOfUpdatedRecord=  DATEDIFF(DAY,@WorkExceptionBegDateOld,@WorkExceptionEndDateOld) + 1


       IF @WorkExceptionAddedDate > @BalanceAddedDate -- Check if the gatepass before Policy
	   IF @Available + @CarryForward + @BalanceOfUpdatedRecord < (DATEDIFF(Day,@WorkExceptionBegDate,@WorkExceptionEndDate) + 1) + @Pending

	   BEGIN
	     SET @RMsgId = 167 -- لايوجد رصيد كافي 
		  SET @FieldInError = 'TypeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
	   END
	   END     
    



		UPDATE  [Managements].[WorkException]
		   SET [EmployeeId]=@EmployeeId,
		       [WorkExceptionTypeId] = @WorkExceptionTypeId,
		     --  [DepartmentId] = @DepartmentId,
			   [WorkExceptionBegDate] = @WorkExceptionBegDate,
			   [WorkExceptionEndDate] = @WorkExceptionEndDate,
			   [Notes] = @Notes, 
			   IsRejected=0,
			   IsApproved=0,
			   [UpdatedUserAccountId] = @UserAccountId,
			   [UpdatedDate] =  GETDATE()
		 WHERE WorkExceptionId = @WorkExceptionId
		   AND [VersionNo]    = @VersionNo
				     			        
		
		-- Save Number of Rows Affected
		SET @RowEffected = @@RowCount
		
		IF @RowEffected > 0 AND @@ERROR = 0
		BEGIN -- Succeeded
		  SET @RC = 0 
		  SET @RMsgId = 1  -- تمت عملية التحديث بنجاح
		  SET @RMessage = (SELECT MsgText 
		                     FROM  [common].[CustomMessage] 
		                    WHERE [MsgId] = @RMsgId)
		 --RETURN
		END
		ELSE
		IF (@RowEffected = 0 AND Not EXISTS(SELECT [WorkExceptionId] 
		                                      FROM  [Managements].[WorkException]
		                                     WHERE [WorkExceptionId]   = @WorkExceptionId
	                                           AND [VersionNo]    = @VersionNo))
		BEGIN
		  SET @RMsgId = 5  -- تم تحديث السجل من قبل مستخدم آخر
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

	  DELETE FROM Managements.WorkExceptionApproval
	  WHERE WorkExceptionId=@WorkExceptionId
        -------------------Check If the update after approval
	  If @IsApproved=1
		        BEGIN

				  -- Process Attendance Status
				
				
					  DECLARE @AttendanceDate AS DATE=@WorkExceptionBegDateOld
					  DECLARE @Today as Date=GETDATE()

					  WHILE @AttendanceDate<=@Today  AND @AttendanceDate <= @WorkExceptionEndDateOld  
						BEGIN
							EXEC logs.SpProcessAttendanceByEmployee @AttendanceDate,@EmployeeId
							SET @AttendanceDate=DATEADD(day,1,@AttendanceDate )
						END   
					
				      --No Need To Change Balance
						IF @BalanceAddedDate > @WorkExceptionAddedDate
						RETURN
					   
			         	 SET @Used = @Used - @Period
					     SET @Available = @Available + @Period

						UPDATE  Managements.TimeOffBalance  
						SET     Available = @Available ,
								Used =  @Used,
								CarryForward = @CarryForward
						WHERE TimeOffBalanceId = @BalanceID
					 
					 END
      -------------------------



	 
            
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
