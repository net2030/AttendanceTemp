USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpTimeOffPolicy_GetEmployees]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2012
-- Description:	Get Leave Policy Employees
-- =============================================
CREATE PROCEDURE [Managements].[SpTimeOffPolicy_GetEmployees]
(
 @TimeOffPolicyId   int = NULL,
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
    DECLARE @strPageSize  nvarchar(50)
    DECLARE @strStartRow  nvarchar(50)
    DECLARE @strFilter    nvarchar(max)
    DECLARE @strGroup     nvarchar(max)
    DECLARE @Group        nvarchar(max)
    DECLARE @Tables       nvarchar(max)
    DECLARE @Fields       nvarchar(max) 
    DECLARE @Filter       nvarchar(max)  
    DECLARE @Sort         nvarchar(500)    
    DECLARE @TypeId int
    DECLARE @EmployeeId int
        
    -- Initializations              
    SET @RowEffected = 0
    
    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END
                          
	-- Set paging variables
	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	SELECT @TypeId =TypeId FROM Managements.TimeOffPolicy WHERE TimeOffPolicyId=@TimeOffPolicyId

	
    BEGIN TRY
		
     SELECT Distinct Employees.Employee.EmployeeId, 
                    Employees.Employee.FirstName + ' ' +  Employees.Employee.MiddleName +' ' +  Employees.Employee.LastName AS Employeename
    FROM Employees.Employee  
	INNER JOIN Managements.TimeOffBalance
	ON Managements.TimeOffBalance.EmployeeId = Employees.Employee.EmployeeId
	INNER JOIN Managements.TimeOffPolicyDetail
	ON Managements.TimeOffPolicyDetail.TimeOffPolicyDetailId = Managements.TimeOffBalance.TimeOffPolicyDetailId
	
	WHERE Managements.TimeOffPolicyDetail.TimeOffPolicyId=@TimeOffPolicyId
	
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END







GO
