USE [MASAttendance]
GO
/****** Object:  StoredProcedure [dbo].[testsend]    Script Date: 22/3/2022 1:37:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[testsend]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		EXEC msdb.dbo.sp_send_dbmail @profile_name='MAS',
						@recipients='adnan@mastechnology.net',
						@subject='Test',
						@body='Test Email',
						@body_format ='HTML'

   
END





GO
