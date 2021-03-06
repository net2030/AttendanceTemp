USE [MASAttendance]
GO
/****** Object:  UserDefinedFunction [Logs].[funIsOnFullDayWorkException]    Script Date: 22/3/2022 1:38:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Adnan Salah
-- Create date: 27/03/2011
-- Description:	Check Employee Vacation
-- =============================================
CREATE FUNCTION [Logs].[funIsOnFullDayWorkException]
(
 @AttendanceDate datetime,
 @EmployeeId int
)
RETURNS INT
AS
BEGIN
	-- return variable here
	DECLARE @StatusId AS INT
    SET @StatusId  = 0

	/*****************************************************
		  Check Work Exception
		 *****************************************************/          
		IF EXISTS(SELECT w.WorkExceptionId
				    FROM Managements.WorkException w
				   --      INNER JOIN MASTMS.Managements.WorkExceptionEmployeeLink l
					  --ON w.WorkExceptionId = l.WorkExceptionId
				   WHERE  (@AttendanceDate >= w.WorkExceptionBegDate
			         AND  @AttendanceDate <= w.WorkExceptionEndDate
                     AND  W.EmployeeId = @EmployeeId
					 AND W.IsApproved=1))
	    BEGIN

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
			   WHEN 10 THEN 40 -- حضور إجتماع عمل - مهمة عمل
			 END		 							   			                       		                       		                       		                       
  			FROM Managements.WorkException w
				 --INNER JOIN 
				 --MASTMS.Managements.WorkExceptionEmployeeLink l
			  --ON w.WorkExceptionId = l.WorkExceptionId
				 INNER JOIN
				 Managements.WorkExceptionType t
			  ON w.WorkExceptionTypeId = t.WorkExceptionTypeId   
		   WHERE @AttendanceDate >= w.WorkExceptionBegDate
			 AND @AttendanceDate <= w.WorkExceptionEndDate
			 AND w.EmployeeId       = @EmployeeId 
			ORDER BY w.WorkExceptionId DESC    		  		  	    
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
		  SET @StatusId = 40 -- دورة تدريبية		  
		END	

	-- Return the result of the function
	RETURN @StatusId
END








GO
