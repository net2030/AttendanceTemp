USE [MASAttendance]
GO
/****** Object:  StoredProcedure [Notifications].[SpGetAbsenceForNotification]    Script Date: 22/3/2022 1:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		MASTMS Team
-- Create date: 02/07/2010
-- Description:	get Training by id
-- =============================================
CREATE PROCEDURE [Notifications].[SpGetAbsenceForNotification]
(
@Op INT=0
)
WITH EXECUTE AS 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
	
    BEGIN TRY	


   
	DELETE FROM Notifications.Notification
	EXEC Notifications.SpGetConsecutiveAbsences
	EXEC Notifications.SpGetSeparatedAbsences


    IF @Op=0  --All Notifications 
   
	    SELECT AE.EmployeeId,
		       AE.FirstName + ' ' + AE.MiddleName + ' ' + AE.LastName AS EmployeeName	
			  ,AE.[EMailAddress]
			  ,AE.EmployeeNo
			  ,AE.[JobTitle] As Title
			  ,dbo.FnUmAlquraYMD(N.StartDate) AS StartDate
			  ,dbo.FnUmAlquraYMD(N.EndDate) AS EndDate
			  ,N.StartDate AS StartDate1
			  ,N.EndDate AS EndDate1
			  ,N.NoOfDays as Days

			  --,dbo.FnUmAlquraYMD(MAX(N.StartDate)) AS StartDate
			  --,dbo.FnUmAlquraYMD(MAX(N.EndDate)) AS EndDate
			  --,SUM(N.NoOfDays) as Days
			  , CASE AbsenceType
					   WHEN 1 THEN 'متصلة'
					   WHEN 2 THEN 'متفرقة'
				 END   AS AbsenceType 
				, AD.DepartmentName 
			
		  FROM Employees.Employee AE 
		  JOIN  Notifications.Notification N ON AE.EmployeeId=N.EmployeeId
		  JOIN Employees.Department AD ON AE.DepartmentId=AD.DepartmentId
		  ORDER BY AE.FirstName ,AE.MiddleName ,AE.LastName
		--  GROUP BY AE.FirstName,AE.MiddleName,AE.LastName,AE.[EMailAddress],AE.EmployeeNo,AE.[JobTitle],N.AbsenceType, AD.DepartmentName

		
     ELSE IF @Op=1 --Issued Notifications

	    SELECT AE.EmployeeId,
		       AE.FirstName + ' ' + AE.MiddleName + ' ' + AE.LastName AS EmployeeName	
			  ,AE.[EMailAddress]
			  ,AE.EmployeeNo
			  ,AE.[JobTitle] As Title
			  ,dbo.FnUmAlquraYMD(N.StartDate) AS StartDate
			  ,dbo.FnUmAlquraYMD(N.EndDate) AS EndDate
			   ,N.StartDate AS StartDate1
			  ,N.EndDate AS EndDate1
			  ,N.NoOfDays as Days
			  --,dbo.FnUmAlquraYMD(MAX(N.StartDate)) AS StartDate
			  --,dbo.FnUmAlquraYMD(MAX(N.EndDate)) AS EndDate
			  --,SUM(N.NoOfDays) as Days
			  , CASE AbsenceType
					   WHEN 1 THEN 'متصلة'
					   WHEN 2 THEN 'متفرقة'
				 END   AS AbsenceType 
				, AD.DepartmentName 
			
		  FROM Employees.Employee AE 
		  JOIN Notifications.Notification N ON AE.EmployeeId=N.EmployeeId
		  JOIN Employees.Department AD ON AE.DepartmentId=AD.DepartmentId
		  JOIN Notifications.NotificationsIssued ISSN ON  ISSN.EmployeeID=N.EmployeeId AND  ISSN.StartDate=N.StartDate AND ISSN.EndDate=N.EndDate
		--  GROUP BY AE.FirstName,AE.MiddleName,AE.LastName,AE.[EMailAddress],AE.EmployeeNo,AE.[JobTitle],N.AbsenceType, AD.DepartmentName
		   ORDER BY AE.FirstName ,AE.MiddleName,AE.LastName
		
		  
	ELSE IF @Op=2 -- NOT Issued Notifications

	    SELECT AE.EmployeeId,
		       AE.FirstName + ' ' + AE.MiddleName + ' ' + AE.LastName AS EmployeeName	
			  ,AE.[EMailAddress]
			  ,AE.EmployeeNo
			  ,AE.[JobTitle] As Title
			  ,dbo.FnUmAlquraYMD(N.StartDate) AS StartDate
			  ,dbo.FnUmAlquraYMD(N.EndDate) AS EndDate
			   ,N.StartDate AS StartDate1
			  ,N.EndDate AS EndDate1
			  ,N.NoOfDays as Days
			  --,dbo.FnUmAlquraYMD(MAX(N.StartDate)) AS StartDate
			  --,dbo.FnUmAlquraYMD(MAX(N.EndDate)) AS EndDate
			  --,SUM(N.NoOfDays) as Days
			  , CASE AbsenceType
					   WHEN 1 THEN 'متصلة'
					   WHEN 2 THEN 'متفرقة'
				 END   AS AbsenceType 
				, AD.DepartmentName 
			
		  FROM Employees.Employee AE 
		  JOIN  Notifications.Notification N ON AE.EmployeeId=N.EmployeeId
		  JOIN Employees.Department AD ON AE.DepartmentId=AD.DepartmentId
		  WHERE NOT EXISTS (SELECT ID FROM Notifications.NotificationsIssued WHERE EmployeeID=AE.EmployeeNo AND StartDate=N.StartDate AND EndDate=N.EndDate)
		--  GROUP BY AE.FirstName,AE.MiddleName,AE.LastName,AE.[EMailAddress],AE.EmployeeNo,AE.[JobTitle],N.AbsenceType, AD.DepartmentName
	       ORDER BY AE.FirstName ,AE.MiddleName,AE.LastName

      
		RETURN
    END TRY
    BEGIN CATCH
        EXECUTE [Common].[SpCommon_LogError];
        RETURN     
    END CATCH
END

 




GO
