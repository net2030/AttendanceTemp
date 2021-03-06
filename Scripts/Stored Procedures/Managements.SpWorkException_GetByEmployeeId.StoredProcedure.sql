USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Managements].[SpWorkException_GetByEmployeeId]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adnan Salah
-- Create date: 02/07/2011
-- Description:	Get Employee Work Exceptions 
-- =============================================
CREATE PROCEDURE [Managements].[SpWorkException_GetByEmployeeId]
(
 @EmployeeId  int = NULL, 
 @BegDate     date = NULL,
 @EndDate     date = NULL,  
 @Lang           char(2) = 'ar',
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
	SET @BegDate = DATEADD(Year,-2,GETDATE())
	SET @EndDate = '2030-01-01'
                          
	-- Set paging variables
	SET @strPageSize = CAST(@PageSize AS nvarchar(50))
	SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS nvarchar(50))	
	
	SET @Tables = 'Managements.WorkException 
                   INNER JOIN Managements.WorkExceptionType 
                   ON Managements.WorkException.WorkExceptionTypeId = Managements.WorkExceptionType.WorkExceptionTypeId 
				   INNER JOIN Employees.Employee 
	               ON Managements.WorkException.EmployeeId = Employees.Employee.EmployeeId '
                   
                 
	
	SET @Fields = ' Managements.WorkException.WorkExceptionId, 
	                Employees.Employee.FirstName + '' '' + Employees.Employee.LastName AS EmployeeName,
					Employees.Employee.BadgeNo As EmployeeCode,
	                Managements.WorkException.WorkExceptionTypeId, 
	                  dbo.FnUmAlquraYMD(Managements.WorkException.WorkExceptionBegDate) AS WorkExceptionBegDate, 
                    dbo.FnUmAlquraYMD(Managements.WorkException.WorkExceptionEndDate) AS WorkExceptionEndDate,  
                    Managements.WorkException.DepartmentId, 
					Managements.WorkException.IsApproved,
				    Managements.WorkException.IsRejected,
					Managements.WorkException.ApprovalNotes'
	                --dbo.FnUmAlquraYMD(Managements.WorkException.WorkExceptionBegDate) AS UmBegDate,
	                --dbo.FnUmAlquraYMD(Managements.WorkException.WorkExceptionEndDate) AS UmEndDate ' 
					
					
					
			IF @Lang = 'en'
			BEGIN
			SET @Fields =  @Fields + ',Managements.WorkExceptionType.WorkExceptionTypeNameEN AS WorkExceptionTypeName'
			SET @Fields =  @Fields + ',CASE 
									   WHEN Managements.WorkException.IsApproved=1 
									   THEN ''Approved'' 
									   WHEN Managements.WorkException.IsRejected=1 
									   THEN ''Rejected''
									   Else ''Submitted'' END AS AprovalStatus'
			END
			ELSE
			BEGIN
			SET @Fields =  @Fields + ',Managements.WorkExceptionType.WorkExceptionTypeName'
			SET @Fields =  @Fields + ',CASE 
									   WHEN Managements.WorkException.IsApproved=1 
									   THEN ''تمت الموافقة'' 
									   WHEN Managements.WorkException.IsRejected=1 
									   THEN ''مرفوض''
									   Else ''تحت الدراسة'' END AS AprovalStatus'
			END		 	

	SET @strFilter = 'WHERE Managements.WorkException.WorkExceptionBegDate >= ' + '''' + CAST(@BegDate as nvarchar(10)) + '''' + 
	                 ' AND Managements.WorkException.WorkExceptionBegDate <= ' + '''' + CAST(@EndDate as nvarchar(10)) + '''' +	                      		     		      		  	               
	                 ' AND Managements.WorkException.EmployeeId = ' +  CAST(@EmployeeId as nvarchar(10))                               
	SET @strGroup = ''
	SET @Sort = 'Managements.WorkException.WorkExceptionBegDate DESC '
          
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
		SET @PagesTotal = (SELECT COUNT(Managements.WorkException.WorkExceptionId)
                             FROM Managements.WorkException 
								  INNER JOIN Managements.WorkExceptionType 
							   ON Managements.WorkException.WorkExceptionTypeId = Managements.WorkExceptionType.WorkExceptionTypeId 
							      INNER JOIN Employees.Department 
							   ON Managements.WorkException.DepartmentId = Employees.Department.DepartmentId 
							      INNER JOIN  Managements.WorkExceptionEmployeeLink 
							   ON Managements.WorkException.WorkExceptionId = Managements.WorkExceptionEmployeeLink.WorkExceptionId 
							      INNER JOIN Employees.Employee 
							   ON Managements.WorkExceptionEmployeeLink.EmployeeId = Employees.Employee.EmployeeId
		                    WHERE Managements.WorkException.WorkExceptionBegDate >= @BegDate
		                      AND Managements.WorkException.WorkExceptionBegDate <= @EndDate
		                      AND Managements.WorkExceptionEmployeeLink.EmployeeId = @EmployeeId)		                   		  		  		  		    
		RETURN     
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN    
    END CATCH
END






GO
