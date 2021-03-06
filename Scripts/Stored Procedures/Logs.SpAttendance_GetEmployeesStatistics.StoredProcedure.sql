USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpAttendance_GetEmployeesStatistics]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Generate Employee Statistics List
-- =============================================
CREATE PROCEDURE [Logs].[SpAttendance_GetEmployeesStatistics]
(
 @DepartmentId   int = NULL,     
 @BegDate        date = NULL,
 @EndDate        date = NULL,
 @SelectOptionNo int = 1,
 @SortOptionNo   int = 0,
 @FilterOptionNo int = 0 
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;       
          
    DECLARE @EmployeeId int = 0
    DECLARE @WindowsAccountName nvarchar(20)
	DECLARE @WorkingDays          decimal(10,2) = 0
	DECLARE @AbsentDays           decimal(10,2) = 0
	DECLARE @LateDays             decimal(10,2) = 0
	DECLARE @UncompleteDays       decimal(10,2) = 0
	DECLARE @ExceptionDays        decimal(10,2) = 0
	DECLARE @ActualWorkdays       decimal(10,2) = 0
	DECLARE @LateHours            decimal(10,2) = 0
	DECLARE @InExteraHours        decimal(10,2) = 0
	DECLARE @OutExteraHours       decimal(10,2) = 0 
	DECLARE @ExteraHours          decimal(10,2) = 0
	DECLARE @WorkingHours         decimal(10,2) = 0 
	DECLARE @ActualWorkHours      decimal(10,2) = 0
	DECLARE @GatepassCount        decimal(10,2) = 0 
	DECLARE @DisciplinePercentage decimal(10,2) = 0
	DECLARE @UmBegDate            nvarchar(10) = 0
	DECLARE @UmEndDate            nvarchar(10) = 0
	
   /*****************************************************
    Employees temporary table
   *****************************************************/      
   CREATE TABLE #Employees 
   (
    EmployeeId           int NOT NULL,
    WindowsAccountName   nvarchar(30), 
    EmployeeName         nvarchar(100),
	BadgeNo              nvarchar(20),
    DepartmentId         int,
    DepartmentName       nvarchar(100),
	BegDate              date,
	EndDate              date,	 
	WorkingDays          decimal(10,2), 
	AbsentDays           decimal(10,2),
	LateDays             decimal(10,2),
	UncompleteDays       decimal(10,2),
	ExceptionDays        decimal(10,2),
	ActualWorkdays       decimal(10,2), 
	LateHours            decimal(10,2),
	InExteraHours        decimal(10,2),
	OutExteraHours       decimal(10,2), 
	ExteraHours          decimal(10,2),
	WorkingHours         decimal(10,2), 
	ActualWorkHours      decimal(10,2),
	GatepassCount        decimal(10,2), 
	DisciplinePercentage decimal(10,2),
	UmBegDate            nvarchar(10),
	UmEndDate            nvarchar(10),
	SortIndex            int,    
    )
   
	 /*****************************************************
	   Cursor Declaration
	 *****************************************************/      
    DECLARE Curs_Employees CURSOR
    LOCAL FOR                                          
    SELECT EmployeeId, WindowsAccountName FROM #Employees  
    FOR READ ONLY
        	                            
    BEGIN TRY				       
	    IF @SelectOptionNo  = 2 -- ???????
	    BEGIN
	       IF @FilterOptionNo = 1 -- ???? ???	    
	       BEGIN
				;WITH CTE(DepartmentId, DepartmentName, ParentId)
				AS
				(
				SELECT DepartmentId, DepartmentName, ParentId
				  From Employees.Department 
				 WHERE DepartmentId = @DepartmentId
				 UNION ALL
				SELECT e.DepartmentId, e.DepartmentName, e.ParentId
				  FROM Employees.Department e
					   INNER JOIN CTE c
					ON e.ParentId = c.DepartmentId
					)
					    
				INSERT INTO #Employees 
					   (
						EmployeeId, 
						EmployeeName,
						BadgeNo,
						DepartmentId,
						DepartmentName,
						BegDate,
						EndDate,
						WindowsAccountName,
						SortIndex
						)    
				SELECT e.EmployeeId, 
					    e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName ,
					   e.BadgeNo,
					   e.DepartmentId,
					   d.DepartmentName,
					   @BegDate,
					   @EndDate,
					   (SELECT WindowsAccountName 
						  FROM Users.UserAccount 
						 WHERE EmployeeId = e.EmployeeId), 
					   p.SortIndex 
				  FROM Employees.Employee e
					   INNER JOIN Employees.Position p
					ON e.PositionId = p.PositionId			       
					   INNER JOIN Employees.Department d
					ON e.DepartmentId = d.DepartmentId
					   INNER JOIN CTE c
					On d.DepartmentId = c.DepartmentId
				 WHERE e.IsActive = 1
				   AND e.PositionTypeId = 1
				   AND p.IsOfficer = 1	
				   and IsFingerRegistered=1	  	       
	       END
	       ELSE
	       IF @FilterOptionNo = 2 -- ?? ???? ???	    
	       BEGIN
				;WITH CTE(DepartmentId, DepartmentName, ParentId)
				AS
				(
				SELECT DepartmentId, DepartmentName, ParentId
				  From Employees.Department 
				 WHERE DepartmentId = @DepartmentId
				 UNION ALL
				SELECT e.DepartmentId, e.DepartmentName, e.ParentId
				  FROM Employees.Department e
					   INNER JOIN CTE c
					ON e.ParentId = c.DepartmentId
					)
					    
				INSERT INTO #Employees 
					   (
						EmployeeId, 
						EmployeeName,
						BadgeNo,
						DepartmentId,
						DepartmentName,
						BegDate,
						EndDate,
						WindowsAccountName,
						SortIndex
						)    
				SELECT e.EmployeeId, 
					   e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName ,
					   e.BadgeNo,
					   e.DepartmentId,
					   d.DepartmentName,
					   @BegDate,
					   @EndDate,
					   (SELECT WindowsAccountName 
						  FROM Users.UserAccount 
						 WHERE EmployeeId = e.EmployeeId), 
					   p.SortIndex 
				  FROM Employees.Employee e
					   INNER JOIN Employees.Position p
					ON e.PositionId = p.PositionId			       
					   INNER JOIN Employees.Department d
					ON e.DepartmentId = d.DepartmentId
					   INNER JOIN CTE c
					On d.DepartmentId = c.DepartmentId
				 WHERE e.IsActive = 1
				   AND e.PositionTypeId = 1
				   AND p.IsOfficer = 0	
				   and IsFingerRegistered=1       
	       END
	       ELSE
	       BEGIN
				;WITH CTE(DepartmentId, DepartmentName, ParentId)
				AS
				(
				SELECT DepartmentId, DepartmentName, ParentId
				  From Employees.Department 
				 WHERE DepartmentId = @DepartmentId
				 UNION ALL
				SELECT e.DepartmentId, e.DepartmentName, e.ParentId
				  FROM Employees.Department e
					   INNER JOIN CTE c
					ON e.ParentId = c.DepartmentId
					)
					    
				INSERT INTO #Employees 
					   (
						EmployeeId, 
						EmployeeName,
						BadgeNo,
						DepartmentId,
						DepartmentName,
						BegDate,
						EndDate,
						WindowsAccountName,
						SortIndex
						)    
				SELECT e.EmployeeId, 
					   e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName ,
					   e.BadgeNo,
					   e.DepartmentId,
					   d.DepartmentName,
					   @BegDate,
					   @EndDate,
					   (SELECT WindowsAccountName 
						  FROM Users.UserAccount 
						 WHERE EmployeeId = e.EmployeeId), 
					   p.SortIndex 
				  FROM Employees.Employee e
					   INNER JOIN Employees.Position p
					ON e.PositionId = p.PositionId			       
					   INNER JOIN Employees.Department d
					ON e.DepartmentId = d.DepartmentId
					   INNER JOIN CTE c
					On d.DepartmentId = c.DepartmentId
				 WHERE e.IsActive = 1 and IsFingerRegistered=1
				   AND e.PositionTypeId = 1	    	       
	       END	          
	    END
	    ELSE
	    IF @SelectOptionNo  = 3 -- ??????
	    BEGIN
			/*****************************************************
			  Common Table Exprestion for departments 
			*****************************************************/     
			;WITH CTE(DepartmentId, DepartmentName, ParentId)
			AS
			(
			SELECT DepartmentId, DepartmentName, ParentId
			  From Employees.Department 
			 WHERE DepartmentId = @DepartmentId
			 UNION ALL
			SELECT e.DepartmentId, e.DepartmentName, e.ParentId
			  FROM Employees.Department e
				   INNER JOIN CTE c
				ON e.ParentId = c.DepartmentId
				)
				    
			INSERT INTO #Employees 
				   (
					EmployeeId, 
					EmployeeName,
					BadgeNo,
					DepartmentId,
					DepartmentName,
					BegDate,
					EndDate,
					WindowsAccountName
					)    
			SELECT e.EmployeeId, 
				   e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName ,
				   e.BadgeNo,
				   e.DepartmentId,
				   d.DepartmentName,
				   @BegDate,
				   @EndDate,
				   (SELECT WindowsAccountName 
					  FROM Users.UserAccount 
					 WHERE EmployeeId = e.EmployeeId) 
			  FROM Employees.Employee e
				   INNER JOIN Employees.Department d
				ON e.DepartmentId = d.DepartmentId
				   INNER JOIN CTE c
				On d.DepartmentId = c.DepartmentId
			 WHERE e.IsActive = 1
			  -- AND e.PositionTypeId IN (2, 3)	    
	    END
	    ELSE	    
	    BEGIN -- ???? ????????
			/*****************************************************
			  Common Table Exprestion for departments 
			*****************************************************/     
			;WITH CTE(DepartmentId, DepartmentName, ParentId)
			AS
			(
			SELECT DepartmentId, DepartmentName, ParentId
			  From Employees.Department 
			 WHERE DepartmentId = @DepartmentId
			 UNION ALL
			SELECT e.DepartmentId, e.DepartmentName, e.ParentId
			  FROM Employees.Department e
				   INNER JOIN CTE c
				ON e.ParentId = c.DepartmentId
				)
				    
			INSERT INTO #Employees 
				   (
					EmployeeId, 
					EmployeeName,
					BadgeNo,
					DepartmentId,
					DepartmentName,
					BegDate,
					EndDate,
					WindowsAccountName
					)    
			SELECT e.EmployeeId, 
				   e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName ,
				   e.BadgeNo,
				   e.DepartmentId,
				   d.DepartmentName,
				   @BegDate,
				   @EndDate,
				   NULL
			  FROM Employees.Employee e
				   INNER JOIN Employees.Department d
				ON e.DepartmentId = d.DepartmentId
				   INNER JOIN CTE c
				On d.DepartmentId = c.DepartmentId
			 WHERE e.IsActive = 1 and IsFingerRegistered=1	    
	    END	      

		 /*****************************************************
		   Open Cursor
		 *****************************************************/      
		 OPEN Curs_Employees
		 FETCH NEXT FROM Curs_Employees INTO @EmployeeId, @WindowsAccountName
		 WHILE @@Fetch_Status = 0 
		 BEGIN 
		   EXECUTE Logs.SpAttendanceLog_GetStatistics @EmployeeId,
		                                              @BegDate,
		                                              @EndDate,
		                                              @WorkingDays OUTPUT, 
													  @AbsentDays OUTPUT,
													  @LateDays OUTPUT,
													  @UncompleteDays OUTPUT,
													  @ExceptionDays OUTPUT,
													  @ActualWorkdays OUTPUT, 
													  @LateHours OUTPUT,
													  @InExteraHours OUTPUT,
													  @OutExteraHours OUTPUT, 
													  @ExteraHours OUTPUT,
													  @WorkingHours OUTPUT, 
													  @ActualWorkHours OUTPUT,
													  @GatepassCount OUTPUT, 
													  @DisciplinePercentage OUTPUT,
													  @UmBegDate OUTPUT,
													  @UmEndDate OUTPUT
		                                              
		   UPDATE #Employees
		      SET WorkingDays = @WorkingDays, 
				  AbsentDays = @AbsentDays,
				  LateDays =  @LateDays,
				  UncompleteDays = @UncompleteDays,
				  ExceptionDays =  @ExceptionDays,
				  ActualWorkdays = @ActualWorkdays, 
				  LateHours = @LateHours,
				  InExteraHours = @InExteraHours,
				  OutExteraHours = @OutExteraHours, 
				  ExteraHours = @ExteraHours,
				  WorkingHours = @WorkingHours, 
				  ActualWorkHours = @ActualWorkHours,
				  GatepassCount = @GatepassCount, 
				  DisciplinePercentage = @DisciplinePercentage,
				  UmBegDate = @UmBegDate,
			  	  UmEndDate = @UmEndDate
			WHERE EmployeeId = @EmployeeId
													  
		   FETCH NEXT FROM Curs_Employees INTO @EmployeeId, @WindowsAccountName
		 END
     
		 -- close cursor
		 CLOSE Curs_Employees
		 -- de-allocate cursor
		 DEALLOCATE Curs_Employees

         IF @SelectOptionNo = 2
         BEGIN
           IF @SortOptionNo = 1 -- ??? ?????? ?? ???????
           BEGIN
			 SELECT ROW_NUMBER() OVER ( ORDER BY EmployeeId) AS RowSerailID,
			        EmployeeId,
					WindowsAccountName, 
					EmployeeName,
					DepartmentId,
					DepartmentName,
					BegDate,
					EndDate,	 
					WorkingDays, 
					AbsentDays,
					LateDays,
					UncompleteDays,
					ExceptionDays,
					ActualWorkdays, 
					LateHours,
					InExteraHours,
					OutExteraHours, 
					ExteraHours,
					WorkingHours, 
					ActualWorkHours,
					GatepassCount, 
					DisciplinePercentage,
					UmBegDate,
					UmEndDate
			   FROM #Employees
			  ORDER BY SortIndex
			  RETURN	           
           END
           ELSE
           IF @SortOptionNo = 2 -- ??? ???? ????????
           BEGIN
			 SELECT ROW_NUMBER() OVER ( ORDER BY EmployeeId) AS RowSerailID,
			        EmployeeId,
					WindowsAccountName, 
					EmployeeName,
					DepartmentId,
					DepartmentName,
					BegDate,
					EndDate,	 
					WorkingDays, 
					AbsentDays,
					LateDays,
					UncompleteDays,
					ExceptionDays,
					ActualWorkdays, 
					LateHours,
					InExteraHours,
					OutExteraHours, 
					ExteraHours,
					WorkingHours, 
					ActualWorkHours,
					GatepassCount, 
					DisciplinePercentage,
					UmBegDate,
					UmEndDate
			   FROM #Employees
			  ORDER BY DisciplinePercentage DESC
			  RETURN           
           END
           ELSE
           IF @SortOptionNo = 3 -- ??? ?????
           BEGIN
			 SELECT ROW_NUMBER() OVER ( ORDER BY EmployeeId) AS RowSerailID,
			        EmployeeId,
					WindowsAccountName, 
					EmployeeName,
					DepartmentId,
					DepartmentName,
					BegDate,
					EndDate,	 
					WorkingDays, 
					AbsentDays,
					LateDays,
					UncompleteDays,
					ExceptionDays,
					ActualWorkdays, 
					LateHours,
					InExteraHours,
					OutExteraHours, 
					ExteraHours,
					WorkingHours, 
					ActualWorkHours,
					GatepassCount, 
					DisciplinePercentage,
					UmBegDate,
					UmEndDate
			   FROM #Employees
			  ORDER BY EmployeeName
			  RETURN           
           END
           ELSE
           BEGIN
			 SELECT ROW_NUMBER() OVER ( ORDER BY EmployeeId) AS RowSerailID,
			        EmployeeId,
					WindowsAccountName, 
					EmployeeName,
					DepartmentId,
					DepartmentName,
					BegDate,
					EndDate,	 
					WorkingDays, 
					AbsentDays,
					LateDays,
					UncompleteDays,
					ExceptionDays,
					ActualWorkdays, 
					LateHours,
					InExteraHours,
					OutExteraHours, 
					ExteraHours,
					WorkingHours, 
					ActualWorkHours,
					GatepassCount, 
					DisciplinePercentage,
					UmBegDate,
					UmEndDate
			   FROM #Employees
			  RETURN            
           END                                  
         END
         ELSE
         BEGIN
			 SELECT ROW_NUMBER() OVER ( ORDER BY EmployeeId) AS RowSerailID,
			        EmployeeId,
					WindowsAccountName, 
					EmployeeName,
					badgeNo,
					DepartmentId,
					DepartmentName,
					BegDate,
					EndDate,	 
					WorkingDays, 
					AbsentDays,
					LateDays,
					UncompleteDays,
					ExceptionDays,
					ActualWorkdays, 
					LateHours,
					InExteraHours,
					OutExteraHours, 
					ExteraHours,
					WorkingHours, 
					ActualWorkHours,
					GatepassCount, 
					DisciplinePercentage,
					UmBegDate,
					UmEndDate
			   FROM #Employees
			   
			  RETURN          
         END
	   			     				                                                 																	    
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
