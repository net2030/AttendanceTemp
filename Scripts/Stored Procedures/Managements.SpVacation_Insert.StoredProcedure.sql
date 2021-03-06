USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpVacation_Insert]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Insert a new vacation
-- =============================================
CREATE PROCEDURE [Managements].[SpVacation_Insert]
(
 @VacationId     int = NULL OUTPUT, 
 @EmployeeId     int = NULL,
 @TypeId         int = NULL,
 @AltEmployeeId  int=0,
 @EffectiveDate  date = NULL,
 @DateExpire     date = NULL,
 @DateOfReturn   date = NULL,
 @Note           nvarchar(500) = NULL,
  @Address           nvarchar(200) = NULL, 
 @Contact           nvarchar(200) = NULL, 
 
 
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
    DECLARE @Available InT
	DECLARE @Pending int 
	DECLARE @MinValue int
	DECLARE @MaxValue int

	DECLARE @IsAltEmployeeApproved  bit=0

	DECLARE @Period int 
	DECLARE @CarryForward decimal(6, 2)
	DECLARE @Used decimal(6, 2)
	DECLARE @BalanceID INT
	DECLARE @PolicyDate DATETIME
	DECLARE @BalanceAddedDate DATETIME

    BEGIN TRY

    -- Initializations        
    SET @RC = 1 -- Assume Falure Run     
    SET @RowEffected = 0
    SET @RMessage = ''
    SET @RMsgId = 0


	SELECT    @MinValue= Convert(int,TOPD.MinValue),
	          @MaxValue= Convert(int,TOPD.MaxValue),
	          @Available= Convert(int,TOB.Available),
	          @CarryForward = Convert(int,TOB.CarryForward),
			  @Used = Used,
	          @PolicyDate = GP.AddedDate,
			  @BalanceAddedDate = TOB.AddedDate
	 FROM  Managements.TimeOffBalance TOB 
			 INNER JOIN Managements.TimeOffPolicyDetail TOPD
			 ON TOB.TimeOffPolicyDetailId = TOPD.TimeOffPolicyDetailId 
			 INNER JOIN   Managements.VacationType
			 ON TOPD.GatepassTypeId = Managements.VacationType.TypeId
			 INNER JOIN Managements.TimeOffPolicy GP ON GP.TimeOffPolicyId=TOPD.TimeOffPolicyId
     WHERE TOB.EmployeeId=@EmployeeID AND  GP.TypeId=1 AND Managements.VacationType.TypeId=@TypeId

	

	if @AltEmployeeId=0
	BEGIN
	SET @AltEmployeeId=@EmployeeId
	SET @IsAltEmployeeApproved=1
	END

	IF @DateExpire<@EffectiveDate

	BEGIN
		  SET @RMsgId = 174 --يحب ان يكون تاريخ النهاية اكبر من او يساوي
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
     END 
    

	IF @MaxValue IS NOT NULL AND @MaxValue <> 0 AND DATEDIFF(day,@EffectiveDate,@DateExpire) + 1  > @MaxValue 
	BEGIN
		  SET @RMsgId = 175 --يجب ان يكون الطلب أقل من او يساوي الحد الأقصى 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText + '   (' +  CAST(@MaxValue as nvarchar(3)) + '  يوم)'  FROM  [common].[CustomMessage]   WHERE MsgId = @RMsgId)
		  RETURN        
        END   
	  
	IF @MinValue IS NOT NULL AND @MinValue<>0 AND DATEDIFF(day,@EffectiveDate,@DateExpire)+1  <@MinValue 
	BEGIN
		  SET @RMsgId = 176 --ايجب أن يكون الطلب أكبر من او يساوي الحد الأدنى
		  SET @FieldInError = 'EmployeeId'
		   SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText + '   (' +  CAST(@MinValue as nvarchar(3)) + '  يوم)'  FROM  [common].[CustomMessage]   WHERE MsgId = @RMsgId)
		  RETURN        
        END   

        IF EXISTS(SELECT VacationId
		          FROM Managements.Vacation 
				  WHERE(EmployeeId=@EmployeeId AND (@EffectiveDate<=EffectiveDate AND @DateExpire>=EffectiveDate)) --New Will overlap befor
					OR (EmployeeId=@EmployeeId AND (@EffectiveDate<=DateExpire AND @DateExpire>=DateExpire)) --New Will overlap after
				    OR (EmployeeId=@EmployeeId AND (@EffectiveDate>=EffectiveDate AND @DateExpire<=DateExpire)) --New will contained by the old Or equal
					OR (EmployeeId=@EmployeeId AND (@EffectiveDate<=EffectiveDate AND @DateExpire>=DateExpire)) --New Will include the old or equal
				  )
        BEGIN
		  SET @RMsgId = 171 -- الإجازة مكررة 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 


	  IF EXISTS(SELECT WorkExceptionId
		          FROM Managements.WorkException 
				  WHERE(EmployeeId=@EmployeeId AND (@EffectiveDate<=WorkExceptionBegDate AND @DateExpire>=WorkExceptionBegDate)) --New Will overlap befor
					OR (EmployeeId=@EmployeeId AND (@EffectiveDate<=WorkExceptionEndDate AND @DateExpire>=WorkExceptionEndDate)) --New Will overlap after
				    OR (EmployeeId=@EmployeeId AND (@EffectiveDate>=WorkExceptionBegDate AND @DateExpire<=WorkExceptionEndDate)) --New will contained by the old Or equal
					OR (EmployeeId=@EmployeeId AND (@EffectiveDate<=WorkExceptionBegDate AND @DateExpire>=WorkExceptionEndDate)) --New Will include the old or equal
				  )
        BEGIN
		  SET @RMsgId = 202 -- الإجازة تتعارض مع استثناء في نفس التاريخ 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 
	

        IF NOT EXISTS(SELECT EmployeeId 
                        FROM  [Employees].[Employee]
                       WHERE EmployeeId = @EmployeeId)                    
        BEGIN
		  SET @RMsgId = 120 -- رقم الموظف غير صحيح أو غير موجود 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 

        IF NOT EXISTS(SELECT TypeId 
                        FROM  [Managements].[VacationType]
                       WHERE TypeId = @TypeId)                    
        BEGIN
		  SET @RMsgId = 136 -- رقم نوع الإجازة غير صحيح أو غير موجود 
		  SET @FieldInError = 'TypeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END		


        -- Check Balance

	   IF @Available IS NOT NULL
	   BEGIN
		   SELECT @Pending=SUM(DATEDIFF(DAY,EffectiveDate,DateExpire) +  1) FROM Managements.Vacation WHERE EmployeeId=@EmployeeId AND TypeId=@TypeId AND IsApproved=0 AND IsRejected=0 AND AddedDate > @BalanceAddedDate 
		   IF @Pending IS NULL
		      SET @Pending = 0.00
		   IF @Available + @CarryForward - @Pending <DATEDIFF(Day,@EffectiveDate,@DateExpire) +1
		   BEGIN
			 SET @RMsgId = 167 -- لايوجد رصيد كافي 
			  SET @FieldInError = 'TypeId'
			  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
			  RETURN        
		   END
	   END

	   -- Check The Alternative Employy if busy
	   IF EXISTS(SELECT VacationId
		          FROM Managements.Vacation 
				  WHERE((EmployeeId=@AltEmployeeId OR AltEmployeeId=@AltEmployeeId) AND (@EffectiveDate<=EffectiveDate AND @DateExpire>=EffectiveDate)) --New Will overlap befor
					OR ((EmployeeId=@AltEmployeeId OR AltEmployeeId=@AltEmployeeId) AND (@EffectiveDate<=DateExpire AND @DateExpire>=DateExpire)) --New Will overlap after
				    OR ((EmployeeId=@AltEmployeeId OR AltEmployeeId=@AltEmployeeId) AND (@EffectiveDate>=EffectiveDate AND @DateExpire<=DateExpire)) --New will contained by the old Or equal
					OR ((EmployeeId=@AltEmployeeId OR AltEmployeeId=@AltEmployeeId) AND (@EffectiveDate<=EffectiveDate AND @DateExpire>=DateExpire)) --New Will include the old or equal
				  )
        BEGIN
		  SET @RMsgId = 178 --  الموظف البديل مشغول 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 

		 -- Check The  Employee is Alternative
	   IF EXISTS(SELECT VacationId
		          FROM Managements.Vacation 
				  WHERE(AltEmployeeId=@EmployeeId AND (@EffectiveDate<=EffectiveDate AND @DateExpire>=EffectiveDate)) --New Will overlap befor
					OR (AltEmployeeId=@EmployeeId AND (@EffectiveDate<=DateExpire AND @DateExpire>=DateExpire)) --New Will overlap after
				    OR (AltEmployeeId=@EmployeeId AND (@EffectiveDate>=EffectiveDate AND @DateExpire<=DateExpire)) --New will contained by the old Or equal
					OR (AltEmployeeId=@EmployeeId AND (@EffectiveDate<=EffectiveDate AND @DateExpire>=DateExpire)) --New Will include the old or equal
				  )
        BEGIN
		  SET @RMsgId = 179 --  الموظف محجوز كموظف بديل 
		  SET @FieldInError = 'EmployeeId'
		  SET @RMessage = CAST(@RMsgId as nvarchar(10)) + ' - ' + (SELECT MsgText FROM  [common].[CustomMessage] WHERE MsgId = @RMsgId)
		  RETURN        
        END 


      INSERT INTO	 [Managements].[Vacation]
                       ([EmployeeId],
                        [TypeId],
						AltEmployeeId,
                        [EffectiveDate],
                        [DateExpire],
                        [DateOfReturn],
                        [Note],
						[Address] ,
						[Contact] ,
						[IsAltEmpApproved],
                        [AddedUserAccountId],
                        [AddedDate],
                        [UpdatedUserAccountId],
                        [UpdatedDate])
               VALUES
                       (@EmployeeId,
                        @TypeId,
						@AltEmployeeId,
                        @EffectiveDate,
                        @DateExpire,
                        @DateOfReturn,
                        @Note,
						@Address ,
						@Contact ,
						@IsAltEmployeeApproved,
				        @UserAccountId,
				        GETDATE(),
				        @UserAccountId,
				        GETDATE())				              
				        
		-- Save The New ID Value
		SET @VacationId = SCOPE_IDENTITY()
		
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
