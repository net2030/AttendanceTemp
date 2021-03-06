USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffBalance_Reset]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	update Work Exception Balance
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffBalance_Reset]

WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
    
	
    DECLARE @TimeOffBalanceId int
	DECLARE @Available DECIMAL(6,3)=0.00
	DECLARE @CarryForward DECIMAL(6,3)=0.00
	DECLARE @IsCarryForward bit=0
	DECLARE @ResetToMinutes DECIMAL(6,3)=0.00
	DECLARE @LastResetDate Date
	DECLARE @DueDate Date
	DECLARE @ResetToZeroPeriodId int=0
	DECLARE @Today DATE=GetDATE()
    
           
    BEGIN TRY	
	      

		    /*****************************************************
			   Cursor Declaration
			 *****************************************************/      
			 DECLARE Curs_Employees CURSOR
			 LOCAL FOR   
			                                        
			 SELECT Distinct TOB.TimeOffBalanceId, TOPD.ResetToMinutes, TOPD.ResetToZeroPeriodId,TOB.LastResetDate,TOB.Available,TOB.CarryForward,TOPD.IsCarryForward
	         From Managements.TimeOffBalance TOB
		     JOIN  Managements.TimeOffPolicyDetail TOPD ON TOB.TimeOffPolicyDetailId=TOPD.TimeOffPolicyDetailId
			 
		
			 

			 FOR READ ONLY

			 /*****************************************************
			   Open Cursor
			 *****************************************************/      
			 OPEN Curs_Employees
			 FETCH NEXT FROM Curs_Employees INTO @TimeOffBalanceId,@ResetToMinutes,@ResetToZeroPeriodId,@LastResetDate,@Available,@CarryForward,@IsCarryForward
			 WHILE @@Fetch_Status = 0 
			 BEGIN

			 IF @ResetToZeroPeriodId=1
			 BEGIN
			  SELECT DATEADD(MONTH,1,@LastResetDate)
			  SET @DueDate=DATEADD(MONTH,1,@LastResetDate)
			 END
			 ELSE IF @ResetToZeroPeriodId=2
			 BEGIN
			  SELECT DATEADD(YEAR,1,@LastResetDate)
			   SET @DueDate=DATEADD(YEAR,1,@LastResetDate)
		     END
			 
			 IF @IsCarryForward=1
			   SET @CarryForward=@CarryForward+@Available
             ELSE
			   SET @CarryForward=0.00


			 IF @Today=@DueDate

			 UPDATE Managements.TimeOffBalance 
			        SET  CarryForward=@CarryForward,
					     Available=0.00,
						 Used=0,
						 LastResetDate=@Today
		     WHERE TimeOffBalanceId=@TimeOffBalanceId
			 AND EmployeeId NOT IN(SELECT EmployeeId FROM Employees.Employee WHERE IsActive=0)



			  SET @CarryForward=0.00

			 FETCH NEXT FROM Curs_Employees INTO @TimeOffBalanceId,@ResetToMinutes,@ResetToZeroPeriodId,@LastResetDate,@Available,@CarryForward,@IsCarryForward
			 END

    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
       
        RETURN    
    END CATCH
END







GO
