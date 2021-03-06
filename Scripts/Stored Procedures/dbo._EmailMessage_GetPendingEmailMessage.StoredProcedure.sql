USE [MASAttendance]
GO
/****** Object:  StoredProcedure [dbo].[_EmailMessage_GetPendingEmailMessage]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =======================================================
-- ALTER PROCEDURE [dbo].[_EmailMessage_GetPendingEmailMessage]
-- =======================================================
CREATE PROCEDURE [dbo].[_EmailMessage_GetPendingEmailMessage]
AS
SELECT  [ID], [ChangeStamp], [Priority], [Status], 
             [NumberOfRetry], [RetryTime], [MaximumRetry], 
             [ExpiryDatetime], [ArrivedDateTime], [SenderInfo], 
             [EmailTo], [EmailFrom], [EmailSubject], 
             [EmailBody], [EmailCC], [EmailBCC], [IsHtml]
FROM dbo.[EmailMessage]
WHERE  Status = 0 
ORDER BY Priority,RetryTime




GO
