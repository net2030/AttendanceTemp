USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpApprovalPolicy_GetEmployees]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Get Leave Policy Employees
-- =============================================
CREATE PROCEDURE [Managements].[SpApprovalPolicy_GetEmployees]
(
 @ApprovalPolicyId   int = NULL,
 @PageNumber     int = 1,
 @PageSize       int = 50,
 
 @PagesTotal     int = 0 OUTPUT
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
     SET NOCOUNT ON;
     
    -- Declariations
    DECLARE @RowEffected  int

    DECLARE @Sort         nvarchar(500)    
    DECLARE @PolicyType int=0
    DECLARE @EmployeeId int
        
    -- Initializations              
    SET @RowEffected = 0
    
    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END
                          

	
	SELECT @PolicyType =TimeOffApprovalType 
	FROM Managements.ApprovalPolicy 
	WHERE ApprovalPolicyId=@ApprovalPolicyId

	
    BEGIN TRY
	
		IF @PolicyType=1	
		 SELECT  Employees.Employee.EmployeeId, 
						 Employees.Employee.FirstName + ' ' +  Employees.Employee.MiddleName +' ' +  Employees.Employee.LastName AS Employeename
		FROM Employees.Employee  
		WHERE VacationApprovalTypeId=@ApprovalPolicyId
		ELSE
		IF @PolicyType=2	
		 SELECT  Employees.Employee.EmployeeId, 
						 Employees.Employee.FirstName + ' ' +  Employees.Employee.MiddleName +' ' +  Employees.Employee.LastName AS Employeename
		FROM Employees.Employee  
		WHERE  WorkExceptionApprovalTypeId=@ApprovalPolicyId
		ELSE
		
		 SELECT  Employees.Employee.EmployeeId, 
						 Employees.Employee.FirstName + ' ' +  Employees.Employee.MiddleName +' ' +  Employees.Employee.LastName AS Employeename
		FROM Employees.Employee  
		WHERE GatepassApprovalTypeId=@ApprovalPolicyId AND VacationApprovalTypeId = @ApprovalPolicyId AND WorkExceptionApprovalTypeId = @ApprovalPolicyId
	
		  		  		  		    
     RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END








GO
