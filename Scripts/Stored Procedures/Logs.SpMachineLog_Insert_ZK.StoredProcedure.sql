USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpMachineLog_Insert_ZK]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Adnan Salah
-- Create date: 6/2/2012
-- Description:	Insert a new machine log
-- =============================================
CREATE PROCEDURE [Logs].[SpMachineLog_Insert_ZK]
(
 @UserNo            NvarChar(10)= NULL,
 @LogDateTime       datetime= NULL,
 @LogType           INT=1,
 @DeviceId          NvarChar(20)= NULL,
 @IPAddress          NvarChar(20)= NULL,
 @Location           NvarChar(100)= NULL,
 @IsRealTime        bit=0
 
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

    BEGIN TRY

 --   INSERT INTO Test
	--VALUES (Convert(Time,@LogDateTime),@UserNo)
	   
     
    DECLARE @EmployeeId INT
    DECLARE @LogDate Date
    DECLARE @IsValidRecord bit  = 1
    DECLARE @IsManualRecord bit  = 0
    DECLARE @LogTime datetime 


	 IF @IsRealTime=0 
	BEGIN
	 IF EXISTS(SELECT LogId FROM Logs.MachineLog WHERE EID=@UserNo AND LogDateTime=@LogDateTime)
	 RETURN
   END

    SET @LogDate=Convert(DATE,@LogDateTime)
    SET @LogTime =  Convert(Time,@LogDateTime)
	SET @UserNo=REPLACE(@UserNo, ' ', '') 

	SELECT @EmployeeId =EmployeeId FROM Employees.Employee WHERE BadgeNo=@UserNo
  
		INSERT INTO [Logs].[MachineLog]
				   ([EmployeeId],
				    EID,
				    [LogDate],
				    [LogTime],
					[LogDateTime],
				    [LogTypeId],
				    [IPAddress],
				    [MachineId],
					LocationId,
				    [IsValidRecord],
				    [IsManualRecord],
				    [AddedUserAccountId],
				    [UpdatedUserAccountId],
				    [AddedDate],
				    [UpdateDate])
			 VALUES
				   (@EmployeeId,
				    @UserNo,
				    @LogDate,
				    @LogTime,
					@LogDateTime,
				    @LogType,
				    @IPAddress,
				    @DeviceId,
					@Location,
				    @IsValidRecord,
				    @IsManualRecord,
				    1,
				    1,				    
				    GETDATE(),				    
				    GETDATE())			
					
					
			UPDATE Logs.Machine SET LastLogTime=@LogDateTime WHERE MachineId=@DeviceId	
			
			EXEC Logs.SpProcessAttendanceByEmployee @LogDate,@EmployeeId


    END TRY
    BEGIN CATCH
         EXECUTE [Common].[SpCommon_LogError];
         
        RETURN    
    END CATCH
END






GO
