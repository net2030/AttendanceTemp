USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpUpdateOutTimeStatusFlex]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 23/09/2008
-- Description:	Update Out Time Status
-- =============================================
CREATE PROCEDURE [Logs].[SpUpdateOutTimeStatusFlex]
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
 @MarkObsentDuration int = 0 ,
 @IsAllowOvertime bit = 1
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
    DECLARE @ExceptionTypeId int = NULL
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
    --SET @CurTime = SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),12 ,6) + ' ' + SUBSTRING(CAST(GETDATE() AS NVARCHAR(30)),18 ,2)
    SET @CurTime = GETDATE()

    SET @StatusId = (SELECT StatusId 
                       FROM Logs.AttendanceLog
                      WHERE LogId = @LogId)   

	                 
    BEGIN TRY
	     /*****************************************************
		 InTime is not Null    OutTime is not Null Same Day
		*****************************************************/		
		IF  (@OutTime IS NOT NULL AND @InTime IS NOT NULL) --
			     BEGIN
		           IF @OutTime >= @WEndTime
					BEGIN 
					  /*****************************************************
					  Calculate Extera time
					 *****************************************************/		   
					  IF @OutTime > @WorkEndTime		   
						SET @OutExtraMinutes = DATEDIFF(mi, @WorkEndTime, @OutTime)
					  ELSE 
						SET @OutExtraMinutes = 0

					 /*****************************************************
					   Evaluate In Time Status
					  *****************************************************/              
					  IF @StatusId = 15 -- على رأس العمل
					  BEGIN
						UPDATE Logs.AttendanceLog
						   SET Logs.AttendanceLog.StatusId = 55, -- حاضر			 
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
					
					ELSE
					IF (@OutTime < @WEndTime)   
					  /*****************************************************
					  Leaving before the end of Work time
					 *****************************************************/ 		   
					    BEGIN
					   /*****************************************************
						Check for any exception 
					   *****************************************************/ 		    
					  
					 SELECT @ExceptionTypeId = GatepassTypeId,
							@ExBegTime = GatepassBegTime,
							@ExEndTime =  GatepassEndTime
					   FROM Managements.Gatepass
					   WHERE EmployeeId =  @EmployeeId
					   AND GatepassBegDate = @AttendanceDate
					   AND GatepassEndTime = @WorkEndTime
					   AND IsApproved = 1   
           
		   IF  @ExceptionTypeId IS NOT NULL 
					BEGIN 
							IF @StatusId=15     
							BEGIN
								IF @OutTime >= @ExBegTime --Leave Within Exception End Time
												UPDATE Logs.AttendanceLog																					
												SET Logs.AttendanceLog.StatusId = @ExceptionTypeId, 
													Logs.AttendanceLog.OutExtraMinutes  = 0,					  
													Logs.AttendanceLog.ExtraTimeMinutes  = 0,
													Logs.AttendanceLog.OutLateMinutes = 0,
													Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime), 
													Logs.AttendanceLog.UpdatedUserAccountId  = 1,
													Logs.AttendanceLog.UpdatedDate  = GETDATE(),
													Logs.AttendanceLog.UpdateVersion = 2,
													Logs.AttendanceLog.IsProcessCompleted = 1    
												WHERE Logs.AttendanceLog.LogId = @LogId                       
                                ELSE
								BEGIN
									SET @WEndTime=@WorkEndTime
									SET @OutLateTime = DATEDIFF(mi, @OutTime, @ExBegTime)
										UPDATE Logs.AttendanceLog
											SET Logs.AttendanceLog.StatusId = 110, -- خروج مبكر 						  
												Logs.AttendanceLog.OutLateMinutes = @OutLateTime,
												Logs.AttendanceLog.LateMinutes = Logs.AttendanceLog.InLateMinutes + @OutLateTime,
												Logs.AttendanceLog.OutExtraMinutes  = 0,
												Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),					                            
												Logs.AttendanceLog.UpdatedUserAccountId  = 1,
												Logs.AttendanceLog.UpdatedDate  = GETDATE(),
												Logs.AttendanceLog.UpdateVersion = 2,
												Logs.AttendanceLog.IsProcessCompleted = 1                        
										WHERE Logs.AttendanceLog.LogId = @LogId    
								END	        
							END 
							ELSE IF  @StatusId =45	
							BEGIN
							    IF @OutTime >= @ExBegTime --Leave Within Exception End Time
												UPDATE Logs.AttendanceLog																					
												SET	Logs.AttendanceLog.OutExtraMinutes  = 0,					  
													Logs.AttendanceLog.ExtraTimeMinutes  = 0,
													Logs.AttendanceLog.OutLateMinutes = 0,
													Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime), 
													Logs.AttendanceLog.UpdatedUserAccountId  = 1,
													Logs.AttendanceLog.UpdatedDate  = GETDATE(),
													Logs.AttendanceLog.UpdateVersion = 2,
													Logs.AttendanceLog.IsProcessCompleted = 1                           
													WHERE Logs.AttendanceLog.LogId = @LogId			        
										ELSE  IF @OutTime < @ExBegTime  --Leaft Before Exception Start Time
												BEGIN
													SET @OutLateTime = DATEDIFF(mi,  @OutTime,@ExBegTime)
													UPDATE Logs.AttendanceLog
													   SET  Logs.AttendanceLog.StatusId = 120, --متأخر صباحا وخروج قبل بدء الاستئذان 
															Logs.AttendanceLog.OutLateMinutes = @OutLateTime, 
															Logs.AttendanceLog.LateMinutes = Logs.AttendanceLog.InLateMinutes + @OutLateTime,
															Logs.AttendanceLog.OutExtraMinutes  = 0,
															Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),					  								   
															Logs.AttendanceLog.UpdatedUserAccountId  = 1,
															Logs.AttendanceLog.UpdatedDate  = GETDATE(),
															Logs.AttendanceLog.UpdateVersion = 2,
															Logs.AttendanceLog.IsProcessCompleted = 1                           
														WHERE Logs.AttendanceLog.LogId = @LogId			        
												END  
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
		   ELSE IF @ExceptionTypeId IS NULL
					BEGIN
					     IF @StatusId=15 
						  BEGIN
							   SET @OutLateTime = DATEDIFF(mi, @OutTime, @WEndTime)
							   IF @OutLateTime >= @MarkObsentDuration AND @MarkObsentDuration > 0
							   BEGIN
								   UPDATE Logs.AttendanceLog
									  SET Logs.AttendanceLog.StatusId = 75, -- Absence 						  
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
								   SET @OutLateTime = DATEDIFF(mi, @OutTime, @WorkEndTime)
								   UPDATE Logs.AttendanceLog
									  SET Logs.AttendanceLog.StatusId = 110, -- خروج مبكر 						  
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
                          ELSE IF @StatusId=45
						  BEGIN
							   SET @OutLateTime = DATEDIFF(mi, @OutTime, @WEndTime)
							   SET @InLateTime = (SELECT InLateMinutes 
													FROM Logs.AttendanceLog
												   WHERE LogId = @LogId)
		                                
							   IF (@InLateTime + @OutLateTime) >= @MarkObsentDuration AND @MarkObsentDuration > 0
							   BEGIN
								   UPDATE Logs.AttendanceLog
									  SET Logs.AttendanceLog.StatusId = 75, -- Absence 						  
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
								   SET @OutLateTime = DATEDIFF(mi, @OutTime, @WorkEndTime)
								   UPDATE Logs.AttendanceLog
									  SET Logs.AttendanceLog.StatusId = 120, --دخول متأخر وخروج مبكر						  
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
						   

						  ELSE

						  BEGIN
							SET @OutLateTime = DATEDIFF(mi, @OutTime, @WorkEndTime)
							UPDATE Logs.AttendanceLog
								SET Logs.AttendanceLog.StatusId = 110, -- خروج مبكر 						  
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

        /*****************************************************
		 InTime is not Null    OutTime is  Null 
		*****************************************************/		
		ELSE  IF (@InTime IS NOT NULL	AND @OutTime IS  NULL ) --
            BEGIN
              SELECT @ExceptionTypeId = GatepassTypeId ,
			  @ExBegTime = GatepassBegTime
			  FROM Managements.Gatepass
			  WHERE EmployeeId = @EmployeeId
			  AND IsApproved = 1 
			  AND GatepassBegDate = @AttendanceDate
			  AND GatepassEndTime = @WorkEndTime
			  IF @ExceptionTypeId IS NOT NULL
              BEGIN
			       IF @StatusId=15
				   BEGIN
						UPDATE Logs.AttendanceLog
							SET Logs.AttendanceLog.StatusId  = @ExceptionTypeId,					       
					 			Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @ExBegTime),
								Logs.AttendanceLog.OutExtraMinutes  = 0,					  
					 			Logs.AttendanceLog.OutLateMinutes = 0,
								Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								Logs.AttendanceLog.UpdateVersion = 2,
								Logs.AttendanceLog.IsProcessCompleted = 1                           
						WHERE Logs.AttendanceLog.LogId = @LogId 						                               						
				    END	
					ELSE IF @StatusId >=2000 AND @StatusId <3000

					 BEGIN
						UPDATE Logs.AttendanceLog
							SET Logs.AttendanceLog.StatusId  = 20,					       
								Logs.AttendanceLog.OutExtraMinutes  = 0,					  
					 			Logs.AttendanceLog.OutLateMinutes = 0,
								Logs.AttendanceLog.UpdatedUserAccountId  = 1,
								Logs.AttendanceLog.UpdatedDate  = GETDATE(),
								Logs.AttendanceLog.UpdateVersion = 2,
								Logs.AttendanceLog.IsProcessCompleted = 1                           
						WHERE Logs.AttendanceLog.LogId = @LogId 						                               						
				    END	

				    ELSE 
							  UPDATE Logs.AttendanceLog
					 			SET	 Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @ExBegTime),
									 Logs.AttendanceLog.OutExtraMinutes  = 0,					  
					 				 Logs.AttendanceLog.OutLateMinutes = 0,
									 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
									 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
									 Logs.AttendanceLog.UpdateVersion = 2,
									 Logs.AttendanceLog.IsProcessCompleted = 1                           
							   WHERE Logs.AttendanceLog.LogId = @LogId 						
			  END    						              
              ELSE -- NO Out Gatepass
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


		  /*****************************************************
		 InTime is   Null    OutTime is  Null Same Day
		*****************************************************/		
		ELSE IF (@InTime IS NULL AND @OutTime IS NULL )--
		    BEGIN
			 IF @StatusId <> 70 
			 BEGIN
			  SELECT @ExceptionTypeId = GatepassTypeId 
			  FROM Managements.Gatepass
			  WHERE EmployeeId = @EmployeeId
			  AND IsApproved = 1 
			  AND GatepassBegDate = @AttendanceDate
			  AND GatepassEndTime = @WorkEndTime
			  IF @ExceptionTypeId IS NOT NULL
				  UPDATE Logs.AttendanceLog
					 SET Logs.AttendanceLog.StatusId  = @ExceptionTypeId,					       
						 Logs.AttendanceLog.UpdatedUserAccountId  = 1,
						 Logs.AttendanceLog.UpdatedDate  = GETDATE(),
						 Logs.AttendanceLog.UpdateVersion = 2,
						 Logs.AttendanceLog.IsProcessCompleted = 1                           
				   WHERE Logs.AttendanceLog.LogId = @LogId 
			  END -- @StatusId <> 70  
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


		/*****************************************************
		 InTime is   Null    OutTime is Not  Null Same Day
		*****************************************************/	
		ELSE IF (@InTime IS NULL AND @OutTime IS NOT NULL AND @StatusId<>70)--
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
			  
			  
			                                                      
              IF @StatusId BETWEEN 2000 AND 2999 --There are In gatepass
              BEGIN
				UPDATE Logs.AttendanceLog			 
			      SET Logs.AttendanceLog.OutExtraMinutes  = @OutExtraMinutes,	
					   Logs.AttendanceLog.OutLateMinutes  = 0,		  
					   Logs.AttendanceLog.ExtraTimeMinutes  =  @OutExtraMinutes,					     
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
            ELSE
		    /*****************************************************
			  Leaving before the end of Work time
		     *****************************************************/ 		   
		    IF (@OutTime < @WEndTime)             
            	BEGIN
					   /*****************************************************
						Check for any exception 
					   *****************************************************/ 		    
					   SELECT @ExceptionTypeId = GatepassTypeId,
							@ExBegTime = GatepassBegTime,
							@ExEndTime =  GatepassEndTime
					   FROM Managements.Gatepass
					   WHERE EmployeeId =  @EmployeeId
					   AND GatepassBegDate = @AttendanceDate
					   AND GatepassEndTime = @WorkEndTime
					   AND IsApproved = 1   
		
					   IF @ExceptionTypeId IS NOT NULL
					   /*****************************************************
						 exception found
					   *****************************************************/ 		        
								BEGIN
									IF @OutTime >= @ExBegTime --Leave Within Exception End Time
											BEGIN   --Register The Exception Without Problems
											UPDATE Logs.AttendanceLog																					
											SET Logs.AttendanceLog.StatusId = @ExceptionTypeId, 
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
									ELSE  IF @OutTime < @ExBegTime  --Leaft Before Exception Start Time
											BEGIN
												SET @OutLateTime = DATEDIFF(mi,  @OutTime,@ExBegTime)
												UPDATE Logs.AttendanceLog
												SET Logs.AttendanceLog.StatusId = 110, --  خروج مبكر 	
													Logs.AttendanceLog.OutLateMinutes = @OutLateTime, 
													Logs.AttendanceLog.LateMinutes = Logs.AttendanceLog.InLateMinutes + @OutLateTime,
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
					   /*****************************************************
						 exception not found
					   *****************************************************/ 
					   BEGIN
						   SET @OutLateTime = DATEDIFF(mi, @OutTime, @WorkEndTime)
						   SET @InLateTime = (SELECT InLateMinutes 
												FROM Logs.AttendanceLog
											   WHERE LogId = @LogId)
		                                
						   IF (@InLateTime + @OutLateTime) >= @MarkObsentDuration AND @MarkObsentDuration > 0
							   UPDATE Logs.AttendanceLog
								  SET Logs.AttendanceLog.StatusId = 75, -- Absence 						  
									  Logs.AttendanceLog.OutLateMinutes = @OutLateTime,
									  Logs.AttendanceLog.LateMinutes = Logs.AttendanceLog.LateMinutes + @OutLateTime,
									  Logs.AttendanceLog.OutExtraMinutes  = 0,
									  Logs.AttendanceLog.ActualWorkMinutes  = DATEDIFF(mi, @InTime, @OutTime),					                            
									  Logs.AttendanceLog.UpdatedUserAccountId  = 1,
									  Logs.AttendanceLog.UpdatedDate  = GETDATE(),
									  Logs.AttendanceLog.UpdateVersion = 2,
									  Logs.AttendanceLog.IsProcessCompleted = 1                        
								WHERE Logs.AttendanceLog.LogId = @LogId                    
						   ELSE
						   BEGIN
							   SET @OutLateTime = DATEDIFF(mi, @OutTime, @WorkEndTime)
							   UPDATE Logs.AttendanceLog
								  SET Logs.AttendanceLog.StatusId = 110, -- خروج مبكر 						  
									  Logs.AttendanceLog.OutLateMinutes = @OutLateTime,
									  Logs.AttendanceLog.LateMinutes = Logs.AttendanceLog.InLateMinutes + @OutLateTime,
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
			ELSE
				UPDATE Logs.AttendanceLog
					SET Logs.AttendanceLog.UpdatedUserAccountId  = 1,
						Logs.AttendanceLog.UpdatedDate  = GETDATE(),
						Logs.AttendanceLog.UpdateVersion = 2,
						Logs.AttendanceLog.IsProcessCompleted = 1                           
					WHERE Logs.AttendanceLog.LogId = @LogId        

          END      
	  RETURN    
    END TRY
    BEGIN CATCH
      --INSERT INTO  [dbo].[CodeErrors]
      --     ([ErrorText]) VALUES ('EmployeeId ' + CAST(@EmployeeId as nvarchar(10)) + ' Date ' + CAST(@AttendanceDate as nvarchar(10)))
      EXECUTE [Common].[SpCommon_LogError];
      RETURN      
    END CATCH
END









GO
