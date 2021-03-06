USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpUpdateOutTimeStatus]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 23/09/2008
-- Description:	Update Out Time Status
-- =============================================
CREATE PROCEDURE [Logs].[SpUpdateOutTimeStatus]
(
 @LogId          int = NULL,
 @AttendanceDate date = NULL,
 @EmployeeId     int = NULL,
 @InTime         datetime = NULL,
 @OutTime        datetime = NULL, 
 @WorkStartTime  datetime = NULL,
 @WorkEndTime    datetime = NULL,
 @WBegTime       datetime = NULL,
 @WEndTime       datetime = NULL,
 @MarkObsentDuration int = 0 
)
WITH EXECUTE AS 'dbo'
AS 
BEGIN
	SET NOCOUNT ON;

    -- Declariations
    DECLARE @RowEffected int
    DECLARE @CurTime datetime
    DECLARE @IsWorkExceptionFound bit = 0
    DECLARE @WorkExceptionTypeName nvarchar (50) = ''
    DECLARE @StatusId int 
    DECLARE @TodayDate date = GETDATE()
    DECLARE @ExceptionStatusId int = 0
    DECLARE @ExBegTime       datetime = NULL
    DECLARE @ExEndTime       datetime = NULL
    DECLARE @IsOnVacation    bit = 0
    DECLARE @IsOutOfRange bit = 0
    DECLARE @OutExtraMinutes int = 0
    
    DECLARE @OutLateTime int = 0 
    DECLARE @InLateTime int = 0
    DECLARE @ActualMinutes int = 0
    DECLARE @WorkMinutes int = 0
    DECLARE @LateMinutes int = 0
    
    -- Set Current Time Value
    SET @CurTime = SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),18 ,2)
    
    SET @StatusId = (SELECT StatusId 
                       FROM Logs.AttendanceLog
                      WHERE LogId = @LogId)   
                      
    BEGIN TRY
		IF @InTime IS NOT NULL	
	   /*****************************************************
		 InTime is not Null 
		*****************************************************/		
		BEGIN 
		  IF @OutTime IS NOT NULL
	      /*****************************************************
		    OutTime is not Null 
		   *****************************************************/		  	
		  BEGIN
		    IF @OutTime >= @WEndTime
            /*****************************************************
	          Calculate Extera time
	         *****************************************************/			    
		    BEGIN 		   
			/*****************************************************
			  Calculate Extera time
			 *****************************************************/	
			  IF @OutTime > @WorkEndTime		   
			  BEGIN
			    SET @OutExtraMinutes = DATEDIFF(mi, @WorkEndTime, @OutTime)
			  END
			  ELSE 
			  BEGIN
			    SET @OutExtraMinutes = 0
			  END

             /*****************************************************
	           Evaluate In Time Status
	          *****************************************************/              
              IF @StatusId = 15 -- على رأس العمل
              BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 55, -- حاضر
				       Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),					 
					   Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),
					   Logs.AttendanceLog.OutExtraMinutes  = @OutExtraMinutes,					  
					   Logs.AttendanceLog.ExtraTimeMinutes  = Logs.AttendanceLog.ExtraTimeMinutes + @OutExtraMinutes,					   
					   Logs.AttendanceLog.LateMinutes = 0,
					   Logs.AttendanceLog.InLateMinutes = 0,
					   Logs.AttendanceLog.OutLateMinutes = 0,
					   Logs.AttendanceLog.OvertimeMinutes = 0,	  
					   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
					   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
					   Logs.AttendanceLog.UpdateVersion = 2,
					   Logs.AttendanceLog.IsProcessCompleted = 1					                              
				 WHERE Logs.AttendanceLog.LogId = @LogId              
              END
              ELSE                                                         
              IF @StatusId = 20
              BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.WorkingMinutes  = DATEDIFF(mi, @WorkStartTime, @WorkEndTime),					 
					   Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),
					   Logs.AttendanceLog.OutExtraMinutes  = @OutExtraMinutes,					  
					   Logs.AttendanceLog.ExtraTimeMinutes  = Logs.AttendanceLog.ExtraTimeMinutes + @OutExtraMinutes,					     
					   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
					   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
					   Logs.AttendanceLog.UpdateVersion = 2,
					   Logs.AttendanceLog.IsProcessCompleted = 1					                              
				 WHERE Logs.AttendanceLog.LogId = @LogId              
              END
              ELSE
              BEGIN
                SET @InLateTime = DATEDIFF(mi, @WorkStartTime, @InTime)
                SET @OutLateTime = DATEDIFF(mi, @OutTime, @WorkEndTime)
                SET @LateMinutes = @InLateTime + @OutLateTime
                                
                IF @MarkObsentDuration > 0 AND @LateMinutes >= @MarkObsentDuration 
                BEGIN
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.StatusId = 75, -- غائب
				      Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),
					   Logs.AttendanceLog.OutExtraMinutes  = @OutExtraMinutes,					  
					   Logs.AttendanceLog.ExtraTimeMinutes  = Logs.AttendanceLog.ExtraTimeMinutes + @OutExtraMinutes,
					   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
					   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
					   Logs.AttendanceLog.UpdateVersion = 2,
					   Logs.AttendanceLog.IsProcessCompleted = 1                           
				 WHERE Logs.AttendanceLog.LogId = @LogId                 
                END
                ELSE
                BEGIN
              /*****************************************************
	            OutTime status remains as InTime status
	           *****************************************************/	              
				UPDATE Logs.AttendanceLog
				   SET Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),
					   Logs.AttendanceLog.OutExtraMinutes  = @OutExtraMinutes,					  
					   Logs.AttendanceLog.ExtraTimeMinutes  = Logs.AttendanceLog.ExtraTimeMinutes + @OutExtraMinutes,
					   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
					   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
					   Logs.AttendanceLog.UpdateVersion = 2,
					   Logs.AttendanceLog.IsProcessCompleted = 1                           
				 WHERE Logs.AttendanceLog.LogId = @LogId
				END              
              END                                         
            END
            ELSE
		    /*****************************************************
			  Leaving before the end of Work time
		     *****************************************************/ 		   
		    IF (@OutTime < @WEndTime)             
            BEGIN
		       /*****************************************************
			    Check for any exception 
		       *****************************************************/ 		    
               EXECUTE Logs.SpGetWorkException @AttendanceDate,
                                               @OutTime,
                                               @EmployeeId, 
                                               @IsWorkExceptionFound OUTPUT,
                                               @IsOnVacation OUTPUT,
                                               @ExceptionStatusId OUTPUT,
                                               @ExBegTime OUTPUT,
                                               @ExEndTime OUTPUT,
                                               @IsOutOfRange OUTPUT
		
		       IF @IsWorkExceptionFound = 1
		       /*****************************************************
			     exception found
		       *****************************************************/ 		        
		       BEGIN 
                 IF @IsOnVacation = 0
                 BEGIN
                   IF @IsOutOfRange = 1
                   BEGIN
						IF @OutTime <= @ExEndTime
						BEGIN
							UPDATE Logs.AttendanceLog
							   SET Logs.AttendanceLog.StatusId = @ExceptionStatusId, 
					               Logs.AttendanceLog.OutExtraMinutes  = 0,					  
					               Logs.AttendanceLog.ExtraTimeMinutes  = 0,
								   Logs.AttendanceLog.OutLateMinutes = 0,
								   Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime), 
								   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								   Logs.AttendanceLog.UpdateVersion = 1,
								   Logs.AttendanceLog.IsProcessCompleted = 1                           
							 WHERE Logs.AttendanceLog.LogId = @LogId			        
						END
						ELSE
						IF @OutTime > @ExEndTime
						BEGIN
						    SET @OutLateTime = DATEDIFF(mi, @ExEndTime, @OutTime)
							UPDATE Logs.AttendanceLog
							   SET Logs.AttendanceLog.StatusId = 110, -- متأخر 
								   Logs.AttendanceLog.OutLateMinutes = @OutLateTime, 
								   Logs.AttendanceLog.LateMinutes = Logs.AttendanceLog.LateMinutes + @OutLateTime,
					               Logs.AttendanceLog.OutExtraMinutes  = 0,
					               Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),					  								   
								   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								   Logs.AttendanceLog.UpdateVersion = 1,
								   Logs.AttendanceLog.IsProcessCompleted = 1                           
							 WHERE Logs.AttendanceLog.LogId = @LogId			        
						END                   
                   END
                   ELSE
                   BEGIN
					  UPDATE Logs.AttendanceLog
					     SET Logs.AttendanceLog.StatusId  = @ExceptionStatusId,					       
					 	     Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),
					         Logs.AttendanceLog.OutExtraMinutes  = 0,					  
							 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
							 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
							 Logs.AttendanceLog.UpdateVersion = 2,
							 Logs.AttendanceLog.IsProcessCompleted = 1                           
					   WHERE Logs.AttendanceLog.LogId = @LogId                    
                   END
                 END               
		       END
		       ELSE
		       /*****************************************************
			     exception not found
		       *****************************************************/ 
		       BEGIN
		           SET @OutLateTime = DATEDIFF(mi, @OutTime, @WorkEndTime)
		           SET @InLateTime = (SELECT InLateMinutes 
		                                FROM Logs.AttendanceLog
		                               WHERE LogId = @LogId)
		                                
                   IF (@InLateTime + @OutLateTime) >= @MarkObsentDuration AND @MarkObsentDuration > 0
                   BEGIN
					   UPDATE Logs.AttendanceLog
						  SET Logs.AttendanceLog.StatusId = 45, -- Late 						  
							  Logs.AttendanceLog.OutLateMinutes = @OutLateTime,
							  Logs.AttendanceLog.LateMinutes = Logs.AttendanceLog.LateMinutes + @OutLateTime,
							  Logs.AttendanceLog.OutExtraMinutes  = 0,
							  Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),					                            
							  Logs.AttendanceLog.UpdatedUserAccountId  = 1,
							  Logs.AttendanceLog.UpdatedDate  = GETDATE(),
							  Logs.AttendanceLog.UpdateVersion = 2,
							  Logs.AttendanceLog.IsProcessCompleted = 1                        
						WHERE Logs.AttendanceLog.LogId = @LogId                    
                   END
                   ELSE
                   BEGIN
					   UPDATE Logs.AttendanceLog
						  SET Logs.AttendanceLog.StatusId = 110, -- متأخر 						  
							  Logs.AttendanceLog.OutLateMinutes = @OutLateTime,
							  Logs.AttendanceLog.LateMinutes = Logs.AttendanceLog.LateMinutes + @OutLateTime,
							  Logs.AttendanceLog.OutExtraMinutes  = 0,
							  Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),					                            
							  Logs.AttendanceLog.UpdatedUserAccountId  = 1,
							  Logs.AttendanceLog.UpdatedDate  = GETDATE(),
							  Logs.AttendanceLog.UpdateVersion = 2,
							  Logs.AttendanceLog.IsProcessCompleted = 1                        
						WHERE Logs.AttendanceLog.LogId = @LogId                    
                   END
 		       
		       END		                
            END
          END  
          ELSE
	     /*****************************************************
		   OutTime is Null 
		  *****************************************************/          
          BEGIN
            IF @AttendanceDate <> @TodayDate
            BEGIN
			  EXECUTE Logs.SpGetWorkException @AttendanceDate,
											  @WEndTime,
											  @EmployeeId, 
											  @IsWorkExceptionFound OUTPUT,
											  @IsOnVacation OUTPUT,
											  @ExceptionStatusId OUTPUT,
											  @ExBegTime OUTPUT,
											  @ExEndTime OUTPUT,
											  @IsOutOfRange OUTPUT	
											  
			  IF @IsWorkExceptionFound = 1 
			  BEGIN
                IF @IsOnVacation = 0
                BEGIN
                   IF @IsOutOfRange = 1
                   BEGIN
						IF @OutTime <= @ExEndTime
						BEGIN
							UPDATE Logs.AttendanceLog
							   SET Logs.AttendanceLog.StatusId = @ExceptionStatusId, 
					               Logs.AttendanceLog.OutExtraMinutes  = 0,					  
								   Logs.AttendanceLog.OutLateMinutes = 0, 
								   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								   Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @ExBegTime),
								   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								   Logs.AttendanceLog.UpdateVersion = 1,
								   Logs.AttendanceLog.IsProcessCompleted = 1                           
							 WHERE Logs.AttendanceLog.LogId = @LogId			        
						END
						ELSE
						IF @OutTime > @ExEndTime
						BEGIN
							UPDATE Logs.AttendanceLog
							   SET Logs.AttendanceLog.StatusId = 110, -- متأخر 
								   Logs.AttendanceLog.OutLateMinutes = DATEDIFF(mi, @ExEndTime, @WorkEndTime), 
								   Logs.AttendanceLog.LateMinutes = (Logs.AttendanceLog.InLateMinutes + Logs.AttendanceLog.OutLateMinutes),
					               Logs.AttendanceLog.OutExtraMinutes  = 0,
					               Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @WorkEndTime),					  								   
								   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								   Logs.AttendanceLog.UpdateVersion = 1,
								   Logs.AttendanceLog.IsProcessCompleted = 1                          
							 WHERE Logs.AttendanceLog.LogId = @LogId			        
						END
						ELSE
						BEGIN
						  UPDATE Logs.AttendanceLog
							 SET Logs.AttendanceLog.StatusId  = @ExceptionStatusId,					       
					 			 Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @ExBegTime),
					             Logs.AttendanceLog.OutExtraMinutes  = 0,					  
					 			 Logs.AttendanceLog.OutLateMinutes = 0,
								 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								 Logs.AttendanceLog.UpdateVersion = 2,
								 Logs.AttendanceLog.IsProcessCompleted = 1                           
						   WHERE Logs.AttendanceLog.LogId = @LogId 						
						END                   
                   END                
                   ELSE
                   BEGIN
					  UPDATE Logs.AttendanceLog
						 SET Logs.AttendanceLog.StatusId  = @ExceptionStatusId,					       
				 			 Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @ExBegTime),
				             Logs.AttendanceLog.OutExtraMinutes  = 0,					  
				 			 Logs.AttendanceLog.OutLateMinutes = 0,
							 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
							 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
							 Logs.AttendanceLog.UpdateVersion = 2,
							 Logs.AttendanceLog.IsProcessCompleted = 1                           
					   WHERE Logs.AttendanceLog.LogId = @LogId                   
                   END
                END
			  END 											              
              ELSE
              BEGIN
				  UPDATE Logs.AttendanceLog
					 SET Logs.AttendanceLog.StatusId = 65,  -- لم يوقع خروج 				       
					     Logs.AttendanceLog.OutExtraMinutes  = 0,					  	 				
		 				 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
						 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
						 Logs.AttendanceLog.UpdateVersion = 2                        
				   WHERE Logs.AttendanceLog.LogId = @LogId                 
              END        
            END
            ELSE
            IF @CurTime > @WEndTime
            BEGIN
			  EXECUTE Logs.SpGetWorkException @AttendanceDate,
											  @WEndTime,
											  @EmployeeId, 
											  @IsWorkExceptionFound OUTPUT,
											  @IsOnVacation OUTPUT,
											  @ExceptionStatusId OUTPUT,
											  @ExBegTime OUTPUT,
											  @ExEndTime OUTPUT,
											  @IsOutOfRange OUTPUT
			  IF @IsWorkExceptionFound = 1 
			  BEGIN
                IF @IsOnVacation = 0
                BEGIN
                   IF @IsOutOfRange = 1
                   BEGIN
						IF @OutTime <= @ExEndTime
						BEGIN
							UPDATE Logs.AttendanceLog
							   SET Logs.AttendanceLog.StatusId = @ExceptionStatusId, 
					               Logs.AttendanceLog.OutExtraMinutes  = 0,					  
								   Logs.AttendanceLog.OutLateMinutes = 0, 
								   Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),
								   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								   Logs.AttendanceLog.UpdateVersion = 1,
								   Logs.AttendanceLog.IsProcessCompleted = 1                           
							 WHERE Logs.AttendanceLog.LogId = @LogId			        
						END
						ELSE
						IF @OutTime > @ExEndTime
						BEGIN
							UPDATE Logs.AttendanceLog
							   SET Logs.AttendanceLog.StatusId = 110, -- متأخر 
								   Logs.AttendanceLog.OutLateMinutes = DATEDIFF(mi, @ExEndTime, @OutTime), 
								   Logs.AttendanceLog.LateMinutes = (Logs.AttendanceLog.InLateMinutes + Logs.AttendanceLog.OutLateMinutes),
					               Logs.AttendanceLog.OutExtraMinutes  = 0,
					               Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),					  								   
								   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								   Logs.AttendanceLog.UpdateVersion = 1,
								   Logs.AttendanceLog.IsProcessCompleted = 1                          
							 WHERE Logs.AttendanceLog.LogId = @LogId			        
						END
						ELSE
						BEGIN
						  UPDATE Logs.AttendanceLog
							 SET Logs.AttendanceLog.StatusId  = @ExceptionStatusId,					       
					 			 Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),
					             Logs.AttendanceLog.OutExtraMinutes  = 0,					  
					 			 Logs.AttendanceLog.OutLateMinutes = 0,
								 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								 Logs.AttendanceLog.UpdateVersion = 2,
								 Logs.AttendanceLog.IsProcessCompleted = 1                           
						   WHERE Logs.AttendanceLog.LogId = @LogId 						
						END                   
                   END                
                END
			  END 											              
              ELSE
              BEGIN
				  UPDATE Logs.AttendanceLog
					 SET Logs.AttendanceLog.StatusId = 65,  -- لم يوقع خروج 				       
					     Logs.AttendanceLog.OutExtraMinutes  = 0,					  	 				
		 				 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
						 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
						 Logs.AttendanceLog.UpdateVersion = 2                        
				   WHERE Logs.AttendanceLog.LogId = @LogId                 
              END  											                 
            END  
	      END		     
        END
        ELSE
	    /*****************************************************
		 InTime is Null 
		 *****************************************************/
		IF (@InTime IS NULL AND @OutTime IS NULL AND @AttendanceDate <> @TodayDate)   
		BEGIN
			  EXECUTE Logs.SpGetWorkException @AttendanceDate,
											  @WEndTime,
											  @EmployeeId, 
											  @IsWorkExceptionFound OUTPUT,
											  @IsOnVacation OUTPUT,
											  @ExceptionStatusId OUTPUT,
											  @ExBegTime OUTPUT,
											  @ExEndTime OUTPUT,
											  @IsOutOfRange OUTPUT		
		
			  IF @IsWorkExceptionFound = 1 
			  BEGIN
				  UPDATE Logs.AttendanceLog
					 SET Logs.AttendanceLog.StatusId  = @ExceptionStatusId,					       
						 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
						 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
						 Logs.AttendanceLog.UpdateVersion = 2,
						 Logs.AttendanceLog.IsProcessCompleted = 1                           
				   WHERE Logs.AttendanceLog.LogId = @LogId 
			  END
			  ELSE
			  BEGIN
				  UPDATE Logs.AttendanceLog
					 SET Logs.AttendanceLog.StatusId = 75,  -- غائب 
						 Logs.AttendanceLog.InLateMinutes = 0,
						 Logs.AttendanceLog.OutLateMinutes = 0,
                         Logs.AttendanceLog.LateMinutes = (Logs.AttendanceLog.InLateMinutes + Logs.AttendanceLog.OutLateMinutes),						 
						 Logs.AttendanceLog.ActualWorkMinutes = 0,
				         Logs.AttendanceLog.OutExtraMinutes  = 0,					  		       
	 					 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
						 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
						 Logs.AttendanceLog.UpdateVersion = 2,
						 Logs.AttendanceLog.IsProcessCompleted = 1                        
				   WHERE Logs.AttendanceLog.LogId = @LogId				  
			  END 			  	
		END
		ELSE
		IF (@InTime IS NULL AND @OutTime IS NULL AND @AttendanceDate = @TodayDate)
		BEGIN
		  IF @CurTime > @WorkEndTime
		  BEGIN
			  EXECUTE Logs.SpGetWorkException @AttendanceDate,
											  @WEndTime,
											  @EmployeeId, 
											  @IsWorkExceptionFound OUTPUT,
											  @IsOnVacation OUTPUT,
											  @ExceptionStatusId OUTPUT,
											  @ExBegTime OUTPUT,
											  @ExEndTime OUTPUT,
											  @IsOutOfRange OUTPUT		
		
			  IF @IsWorkExceptionFound = 1 
			  BEGIN
                IF @IsOnVacation = 0
                BEGIN
                   IF @IsOutOfRange = 1
                   BEGIN
						IF @OutTime <= @ExEndTime
						BEGIN
							UPDATE Logs.AttendanceLog
							   SET Logs.AttendanceLog.StatusId = @ExceptionStatusId, 
					               Logs.AttendanceLog.OutExtraMinutes  = 0,					  
								   Logs.AttendanceLog.OutLateMinutes = 0, 
								   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								   Logs.AttendanceLog.UpdateVersion = 1,
								   Logs.AttendanceLog.IsProcessCompleted = 1                           
							 WHERE Logs.AttendanceLog.LogId = @LogId			        
						END
						ELSE
						IF @OutTime > @ExEndTime
						BEGIN
							UPDATE Logs.AttendanceLog
							   SET Logs.AttendanceLog.StatusId = 110, -- متأخر 
								   Logs.AttendanceLog.OutLateMinutes = DATEDIFF(mi, @ExEndTime, @OutTime), 
								   Logs.AttendanceLog.LateMinutes = (Logs.AttendanceLog.InLateMinutes + Logs.AttendanceLog.OutLateMinutes),
					               Logs.AttendanceLog.OutExtraMinutes  = 0,					  								   
								   Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								   Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								   Logs.AttendanceLog.UpdateVersion = 1,
								   Logs.AttendanceLog.IsProcessCompleted = 1                           
							 WHERE Logs.AttendanceLog.LogId = @LogId			        
						END
						ELSE
						BEGIN
						  UPDATE Logs.AttendanceLog
							 SET Logs.AttendanceLog.StatusId  = @ExceptionStatusId,					       
					 			 Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),
					             Logs.AttendanceLog.OutExtraMinutes  = 0,					  
					 			 Logs.AttendanceLog.OutLateMinutes = 0,
								 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								 Logs.AttendanceLog.UpdateVersion = 2,
								 Logs.AttendanceLog.IsProcessCompleted = 1                           
						   WHERE Logs.AttendanceLog.LogId = @LogId 						
						END                   
                   END                
                END
			  END
			  ELSE
			  BEGIN
				  UPDATE Logs.AttendanceLog
					 SET Logs.AttendanceLog.StatusId = 75,  -- غائب 
						 Logs.AttendanceLog.InLateMinutes = 0,
						 Logs.AttendanceLog.OutLateMinutes = 0,
                         Logs.AttendanceLog.LateMinutes = (Logs.AttendanceLog.InLateMinutes + Logs.AttendanceLog.OutLateMinutes),						 
						 Logs.AttendanceLog.ActualWorkMinutes = 0,
				         Logs.AttendanceLog.OutExtraMinutes  = 0,					  		       
	 					 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
						 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
						 Logs.AttendanceLog.UpdateVersion = 2,
						 Logs.AttendanceLog.IsProcessCompleted = 1                        
				   WHERE Logs.AttendanceLog.LogId = @LogId				  
			  END 			    
		  END
		END
		 		
          		       
	  RETURN    
    END TRY
    BEGIN CATCH
      INSERT INTO  [dbo].[CodeErrors]
           ([ErrorText]) VALUES ('EmployeeId ' + CAST(@EmployeeId as nvarchar(10)) + ' Date ' + CAST(@AttendanceDate as nvarchar(10)))
      EXECUTE [Common].[SpCommon_LogError];
      RETURN      
    END CATCH
END






GO
