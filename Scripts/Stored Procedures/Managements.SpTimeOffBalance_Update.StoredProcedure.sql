USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffBalance_Update]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	update Work Exception Balance
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffBalance_Update]

WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
    
	
    DECLARE @TimeOffBalanceId int
	DECLARE @Available DECIMAL(6,3)=0.00
	DECLARE @CarryForward DECIMAL(6,3)=0.00
	DECLARE @IsCarryForward bit=0
	DECLARE @EarnMinutes DECIMAL(6,3)=0.00
	DECLARE @LastEarnedDate Date
	DECLARE @DueDate Date
	DECLARE @EarnPeriodId int=0
	DECLARE @Today DATE=GetDATE()
    
           
    BEGIN TRY	
	      

		    /*****************************************************
			   Cursor Declaration
			 *****************************************************/      
			 DECLARE Curs_Employees CURSOR
			 LOCAL FOR   
			                                        
			 SELECT Distinct TOB.TimeOffBalanceId, TOPD.EarnMinutes, TOPD.EarnPeriodId,TOB.LastEarnedDate,TOB.Available
	         From Managements.TimeOffBalance TOB
		     JOIN  Managements.TimeOffPolicyDetail TOPD ON TOB.TimeOffPolicyDetailId=TOPD.TimeOffPolicyDetailId
			 
		
			 

			 FOR READ ONLY

			 /*****************************************************
			   Open Cursor
			 *****************************************************/      
			 OPEN Curs_Employees
			 FETCH NEXT FROM Curs_Employees INTO @TimeOffBalanceId,@EarnMinutes,@EarnPeriodId,@LastEarnedDate,@Available
			 WHILE @@Fetch_Status = 0 
			 BEGIN

			 IF @EarnPeriodId=1
			 BEGIN
			  SELECT DATEADD(MONTH,1,@LastEarnedDate)
			  SET @DueDate=DATEADD(MONTH,1,@LastEarnedDate)
			 END
			 ELSE IF @EarnPeriodId=2
			 BEGIN
			  SELECT DATEADD(YEAR,1,@LastEarnedDate)
			   SET @DueDate=DATEADD(YEAR,1,@LastEarnedDate)
		     END
			 
			


			 IF @Today=@DueDate

			 UPDATE Managements.TimeOffBalance 
			        SET Available=Available+@EarnMinutes,
					    Earned=@EarnMinutes,
						LastEarnedDate=@Today
		     WHERE TimeOffBalanceId=@TimeOffBalanceId
			 AND EmployeeId NOT IN(SELECT EmployeeId FROM Employees.Employee WHERE IsActive=0)



			  SET @CarryForward=0.00

			 FETCH NEXT FROM Curs_Employees INTO @TimeOffBalanceId,@EarnMinutes,@EarnPeriodId,@LastEarnedDate,@Available
			 END

    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
       
        RETURN    
    END CATCH
END







GO
