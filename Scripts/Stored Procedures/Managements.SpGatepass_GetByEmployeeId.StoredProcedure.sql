USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpGatepass_GetByEmployeeId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Gatepass employeeid
-- =============================================
CREATE PROCEDURE [Managements].[SpGatepass_GetByEmployeeId]
(
 @EmployeeId  int = NULL,
 @Lang         char(2) = 'ar',
 @PageNumber  int = 1,
 @PageSize    int = 50,
 
 @PagesTotal  int = 0 OUTPUT
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
    
        
    -- Initializations              
    SET @RowEffected = 0
    
    -- Default Page Number
	IF @PageNumber < 1
    BEGIN
      SET @PageNumber = 1
    END
                           
                          
	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	SET @Tables = ' Managements.Gatepass 
	                INNER JOIN Employees.Employee
	             ON Managements.Gatepass.EmployeeId = Employees.Employee.EmployeeId
	                INNER JOIN Managements.GatepassType 
			     ON Managements.Gatepass.GatepassTypeId = Managements.GatepassType.GatepassTypeId	                 
	                INNER JOIN Employees.Department
	             ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId '
	
	SET @Fields = 'Managements.Gatepass.GatepassId, 
			   Managements.Gatepass.EmployeeId, 
			   Managements.Gatepass.GatepassTypeId, 
			   Managements.Gatepass.GatepassBegDate, 
			   Managements.Gatepass.GatepassEndDate, 

			   Convert(char,dbo.FnUmAlquraYMD(Managements.Gatepass.[GatepassBegDate])) + '' --'' + SUBSTRING(CAST(Managements.Gatepass.[GatepassBegTime] AS NVARCHAR(30)),12 ,6) + '' '' + SUBSTRING(CAST(Managements.Gatepass.[GatepassBegTime] AS NVARCHAR(30)),18 ,2) AS GatepassBegTime ,
			   Convert(char,dbo.FnUmAlquraYMD(Managements.Gatepass.[GatepassEndDate])) + '' --'' + SUBSTRING(CAST(Managements.Gatepass.[GatepassEndTime] AS NVARCHAR(30)),12 ,6) + '' '' + SUBSTRING(CAST(Managements.Gatepass.[GatepassEndTime] AS NVARCHAR(30)),18 ,2) AS GatepassEndTime ,

			   Managements.Gatepass.ApprovedEmployeeId,  
			  
			   Managements.Gatepass.IsApproved,
			 
			   Managements.Gatepass.ApprovalNotes,
			   Employees.Employee.FirstName + '' '' + Employees.Employee.MiddleName + '' '' + Employees.Employee.LastName AS Employeename , 
			   Employees.Department.DepartmentName, 
			   dbo.FnUmAlquraYMD(Managements.Gatepass.GatepassBegDate) AS UmBegDate, 
			   dbo.FnUmAlquraYMD(Managements.Gatepass.GatepassEndDate) AS UmEndDate, 
			   Employees.fnGetEmployeeName(Managements.Gatepass.ApprovedEmployeeId) AS ApprovedName ' 		 	


    IF @Lang = 'en'
	BEGIN
	SET @Fields =  @Fields + ',Managements.GatepassType.GatepassTypeNameEN AS GatepassTypeName'
	SET @Fields =  @Fields + ',CASE 
							   WHEN Managements.Gatepass.IsApproved=1 
							   THEN ''Approved'' 
							   WHEN Managements.Gatepass.IsRejected=1 
							   THEN ''Rejected''
							   Else ''Submitted'' END AS AprovalStatus'
	END
	ELSE
	BEGIN
	SET @Fields =  @Fields + ',Managements.GatepassType.GatepassTypeName'
	SET @Fields =  @Fields + ',CASE 
							   WHEN Managements.Gatepass.IsApproved=1 
							   THEN ''تمت الموافقة'' 
							   WHEN Managements.Gatepass.IsRejected=1 
							   THEN ''مرفوض''
							   Else ''تحت الدراسة'' END AS AprovalStatus'
	END
     		     		      		  	               
	SET @strFilter = ' WHERE Employees.Employee.EmployeeId = ' + CAST(@EmployeeId as nvarchar(10)) 
	                
	                 
	SET @strGroup = ''
	SET @Sort = 'Managements.Gatepass.GatepassBegDate DESC '

          
    BEGIN TRY
		  --*******************************************************************
		  -- Generate & Execute Dynamic query
		  --*******************************************************************	
		  EXEC(
		  '

		  WITH RowsResult AS 
		  (
		   		SELECT     ROW_NUMBER() OVER ( ORDER BY ' + @Sort+ ') AS RowSerailID,'
		  +@Fields+ ' FROM ' + @Tables + @strFilter + ' ' + @strGroup + ') 
		  SELECT     *
		    FROM   RowsResult
		   WHERE  RowSerailID BETWEEN '+@strStartRow+' AND ('+@strStartRow+'+'+@strPageSize+'-1);'
		  )

          -- Set Total Pages and Page No
		SET @PagesTotal = (SELECT COUNT(Managements.Gatepass.GatepassId)
                             FROM Managements.Gatepass 
                                  INNER JOIN Employees.Employee 
                               ON Managements.Gatepass.EmployeeId = Employees.Employee.EmployeeId 
                                  INNER JOIN Employees.Department 
                               ON Employees.Employee.DepartmentId = Employees.Department.DepartmentId
                            WHERE Employees.Employee.EmployeeId = @EmployeeId)
		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
