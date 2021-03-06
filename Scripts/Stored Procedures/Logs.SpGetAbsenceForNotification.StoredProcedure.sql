USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Logs].[SpGetAbsenceForNotification]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2012
-- Description:	get Training by id
-- =============================================
CREATE PROCEDURE [Logs].[SpGetAbsenceForNotification]
(
@Op INT=0
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
	
    BEGIN TRY	


   
	DELETE FROM Notifications.Notifications
	EXEC Notifications.SpGetConsecutiveAbsences
	EXEC Notifications.SpGetSeparatedAbsences


    IF @Op=0  --All Notifications 
   
	    SELECT AE.FirstName + ' ' + AE.MiddleName + ' ' + AE.LastName AS EmployeeName	
			  ,AE.[EMailAddress]
			  ,AE.EmployeeNo
			  ,AE.[JobTitle] As Title
			  ,MIN(convert(nvarchar(10),N.StartDate,105)) AS StartDate
			  ,MAX(convert(nvarchar(10),N.EndDate,105)) AS EndDate
			  ,SUM(N.NoOfDays) as Days
			  , CASE AbsenceType
					   WHEN 1 THEN 'متصلة'
					   WHEN 2 THEN 'متفرقة'
				 END   AS AbsenceType 
				, AD.DepartmentName 
			
		  FROM Employees.Employee AE 
		  JOIN  Notifications.Notifications N ON AE.EmployeeId=N.EmployeeId
		  JOIN Employees.Department AD ON AE.DepartmentId=AD.DepartmentId
		  GROUP BY AE.FirstName,AE.MiddleName,AE.LastName,AE.[EMailAddress],AE.EmployeeNo,AE.[JobTitle],N.AbsenceType, AD.DepartmentName

		
     ELSE IF @Op=1 --Issued Notifications

	     SELECT AE.FirstName + ' ' + AE.MiddleName + ' ' + AE.LastName AS EmployeeName	
			  ,AE.[EMailAddress]
			  ,AE.EmployeeNo
			  ,AE.[JobTitle] As Title
			  ,MIN(convert(nvarchar(10),N.StartDate,105)) AS StartDate
			  ,MAX(convert(nvarchar(10),N.EndDate,105)) AS EndDate
			  ,SUM(N.NoOfDays) as Days
			  , CASE AbsenceType
					   WHEN 1 THEN 'متصلة'
					   WHEN 2 THEN 'متفرقة'
				 END   AS AbsenceType 
				, AD.DepartmentName 
				
		  FROM Employees.Employee AE 
		  JOIN Notifications.Notifications N ON AE.EmployeeId=N.EmployeeId
		  JOIN Employees.Department AD ON AE.DepartmentId=AD.DepartmentId
		  WHERE  EXISTS (SELECT ID FROM Notifications.NotificationsIssued WHERE EmployeeID=AE.EmployeeNo AND StartDate=N.StartDate AND EndDate=N.EndDate)
		  GROUP BY AE.FirstName,AE.MiddleName,AE.LastName,AE.[EMailAddress],AE.EmployeeNo,AE.[JobTitle],N.AbsenceType, AD.DepartmentName
		  
		
		  
	ELSE IF @Op=2 -- NOT Issued Notifications

	       SELECT AE.FirstName + ' ' + AE.MiddleName + ' ' + AE.LastName AS EmployeeName	
			  ,AE.[EMailAddress]
			  ,AE.EmployeeNo
			  ,AE.[JobTitle] As Title
			  ,MIN(convert(nvarchar(10),N.StartDate,105)) AS StartDate
			  ,MAX(convert(nvarchar(10),N.EndDate,105)) AS EndDate
			  ,SUM(N.NoOfDays) as Days
			  , CASE AbsenceType
					   WHEN 1 THEN 'متصلة'
					   WHEN 2 THEN 'متفرقة'
				 END   AS AbsenceType 
				, AD.DepartmentName 
			
		  FROM Employees.Employee AE 
		  JOIN  Notifications.Notifications N ON AE.EmployeeId=N.EmployeeId
		  JOIN Employees.Department AD ON AE.DepartmentId=AD.DepartmentId
		  WHERE NOT EXISTS (SELECT ID FROM Notifications.NotificationsIssued WHERE EmployeeID=AE.EmployeeNo AND StartDate=N.StartDate AND EndDate=N.EndDate)
		  GROUP BY AE.FirstName,AE.MiddleName,AE.LastName,AE.[EMailAddress],AE.EmployeeNo,AE.[JobTitle],N.AbsenceType, AD.DepartmentName
	

      
		RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN     
    END CATCH
END

 




GO
