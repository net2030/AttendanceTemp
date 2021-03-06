USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpGetGatepass]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Check Work Exception
-- =============================================
CREATE PROCEDURE [Logs].[SpGetGatepass]
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
		  Check Gatepass permissions
		 *****************************************************/ 
		IF @AttendanceTime IS NOT NULL

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
		  SET @StatusId=20
		  SET @IsOutOfRange=0
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
			  SET @StatusId=20
			  SET @IsOutOfRange=1

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
