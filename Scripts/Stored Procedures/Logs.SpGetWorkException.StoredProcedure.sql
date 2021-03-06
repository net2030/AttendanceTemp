USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpGetWorkException]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Check Work Exception
-- =============================================
CREATE PROCEDURE [Logs].[SpGetWorkException]
(
 @AttendanceDate date = NULL,
 @AttendanceTime datetime = NULL,
 @EmployeeId     int = NULL,
 
 @IsFound        bit = 0 OUTPUT,
 @IsVacation     bit = 0 OUTPUT,  
 @StatusId       int = NULL OUTPUT,
 @BegTime        datetime = NULL OUTPUT,
 @EndTime        datetime = NULL OUTPUT,
 @IsOutOfRange   bit = 0 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;
      
    BEGIN TRY
		/*****************************************************
		  Check Work Exception
		 *****************************************************/          
		IF EXISTS(SELECT w.WorkExceptionId
				    FROM Managements.WorkException w
				   WHERE  (@AttendanceDate >= w.WorkExceptionBegDate
			         AND  @AttendanceDate <= w.WorkExceptionEndDate
                     AND  W.EmployeeId = @EmployeeId
					 AND W.IsApproved=1))
	    BEGIN
		  SET @IsFound = 1
		  SELECT TOP(1) @StatusId = 
		     CASE t.WorkExceptionTypeId
			   WHEN 1 THEN 25 -- تكليف بالعمل - مكلف
			   WHEN 2 THEN 20 -- مكافئة تشجيعية - مستأذن
			   WHEN 3 THEN 30 -- إنتداب خارجي - منتدب
			   WHEN 4 THEN 30 -- إنتداب داخلي - منتدب
			   WHEN 5 THEN 35 -- حضور منتدى - مهمة عمل
			   WHEN 6 THEN 35 -- حضور محاضرة - مهمة عمل
			   WHEN 7 THEN 35 -- زيارة عمل - مهمة عمل
			   WHEN 8 THEN 35 -- حضور إجتماع عمل - مهمة عمل
			   WHEN 10 THEN 41 --ابتعاث للخارج
			   WHEN 12 THEN 42 -- ايفاد داخلي
			 END		 							   			                       		                       		                       		                       
  			FROM Managements.WorkException w
				
				 INNER JOIN
				 Managements.WorkExceptionType t
			  ON w.WorkExceptionTypeId = t.WorkExceptionTypeId   
		   WHERE @AttendanceDate >= w.WorkExceptionBegDate
			 AND @AttendanceDate <= w.WorkExceptionEndDate
			 AND W.EmployeeId       = @EmployeeId 
			ORDER BY w.WorkExceptionId DESC    		  		  	    
		END
	    ELSE
		/*****************************************************
		  Check Vacation
		 *****************************************************/ 
		IF EXISTS(SELECT VacationId
				    FROM Managements.Vacation
				   WHERE (@AttendanceDate >= EffectiveDate
					 AND  @AttendanceDate <= DateExpire
					 AND  EmployeeId = @EmployeeId
					 AND IsApproved=1))

		BEGIN
		  SET @IsFound = 1
		  SET @IsVacation = 1
		  SET @StatusId = 60 -- إجازة
		END		  
	    ELSE
		/*****************************************************
		  Check Training
		 *****************************************************/ 
		IF EXISTS(SELECT t.TrainingId
				    FROM Managements.Training t
				         INNER JOIN Managements.TrainingEmployeeLink l
					  ON t.TrainingId = l.TrainingId
				   WHERE (@AttendanceDate >= t.TrainingBegDate
					 AND  @AttendanceDate <= t.TrainingEndDate
					 AND  l.EmployeeId = @EmployeeId))

		BEGIN
		  SET @IsFound = 1
		  SET @StatusId = 40 -- دورة تدريبية		  
		END
	    ELSE
		/*****************************************************
		  Check Gatepass permissions
		 *****************************************************/ 
		IF EXISTS(SELECT Managements.Gatepass.GatepassId
				    FROM Managements.Gatepass 				         
				   WHERE (@AttendanceDate >= Managements.Gatepass.GatepassBegDate
					 AND  @AttendanceDate <= Managements.Gatepass.GatepassEndDate
					 AND  @AttendanceTime >= Managements.Gatepass.GatepassBegTime
					 AND  @AttendanceTime <= Managements.Gatepass.GatepassEndTime
					 AND  Managements.Gatepass.EmployeeId = @EmployeeId
					 AND  Managements.Gatepass.IsApproved=1))
		BEGIN
		  SET @IsFound = 1

		  --الحالة الجديدة

		 --    SELECT TOP(1) @StatusId = 
		 --    CASE G.GatepassId
			--   WHEN 1 THEN 20 -- إستئذان خاص
			--   WHEN 2 THEN 35 -- مهمة عمل - مستأذن
			--   WHEN 3 THEN 21 -- إنتداب خارجي - منتدب
			--   WHEN 5 THEN 35 -- مستأذن - مهمة عمل
			-- END
			-- , @BegTime = G.GatepassBegTime	
			-- , @EndTime = G.GatepassEndTime	 							   			                       		                       		                       		                       
  	--		FROM Managements.Gatepass G 
		 --  WHERE @AttendanceDate >= G.GatepassBegDate
			-- AND @AttendanceDate <= G.GatepassEndDate
			-- AND @AttendanceTime>=G.GatepassBegTime
			-- AND @AttendanceTime>=G.GatepassEndTime
			-- AND G.EmployeeId       = @EmployeeId 
			--ORDER BY G.GatepassId DESC    		  		  	    
		


		-- الحالة القديمة
		  SET @StatusId = 20 -- مستأذن	
          SELECT TOP(1)
                 @BegTime = Managements.Gatepass.GatepassBegTime,
                 @EndTime = Managements.Gatepass.GatepassEndTime
			FROM Managements.Gatepass 				         
		   WHERE (@AttendanceDate >= Managements.Gatepass.GatepassBegDate
			 AND  @AttendanceDate <= Managements.Gatepass.GatepassEndDate
			 AND  @AttendanceTime >= Managements.Gatepass.GatepassBegTime
			 AND  @AttendanceTime <= Managements.Gatepass.GatepassEndTime
			 AND  Managements.Gatepass.EmployeeId = @EmployeeId)		  		  
		END
		
				   				 		
		IF @IsFound = 0 
		BEGIN
			/*****************************************************
			  Check Gatepass permissions
			 *****************************************************/ 
			IF EXISTS(SELECT Managements.Gatepass.GatepassId
						FROM Managements.Gatepass 				         
					   WHERE (@AttendanceDate >= Managements.Gatepass.GatepassBegDate
						 AND  @AttendanceDate <= Managements.Gatepass.GatepassEndDate
						 AND  Managements.Gatepass.EmployeeId = @EmployeeId 
						 AND IsApproved=1))
			BEGIN
			  SET @IsFound = 1
			  SET @IsOutOfRange = 1
			  SET @StatusId = 20 -- مستأذن	
			  SELECT TOP(1)
					 @BegTime = Managements.Gatepass.GatepassBegTime,
					 @EndTime = Managements.Gatepass.GatepassEndTime
				FROM Managements.Gatepass 				         
			   WHERE (@AttendanceDate >= Managements.Gatepass.GatepassBegDate
				 AND  @AttendanceDate <= Managements.Gatepass.GatepassEndDate
				 AND  Managements.Gatepass.EmployeeId = @EmployeeId)		  		  
			END			      
		END  
						 	             
        RETURN
    END TRY
    BEGIN CATCH
      EXECUTE [Common].[SpCommon_LogError];
      RETURN    
    END CATCH
END








GO
