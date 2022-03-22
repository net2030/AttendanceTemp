Imports Microsoft.VisualBasic
Imports SmartMassEmail.Entities
Imports SmartMassEmail.Providers
Imports SmartMassEmail.ImplementedProviders
Imports System.Threading
Imports System.Net
Imports Microsoft.Exchange.WebServices.Data


Public Class EMailUtilities

    Public Const TASK_NOTIFICATION_DISPLAY_NAME = "Timesheet Notification"
    Public Const TIMESHEET_APPROVED_NOTIFICATION_INFORMATION_FROM = "Timesheet Approved Notification"
    Public Const TIMESHEET_REJECTED_NOTIFICATION_INFORMATION_FROM = "Timesheet Rejected Notification"
    Public Const TIMESHEET_APPROVAL_PENDING_NOTIFICATION_INFORMATION_FROM = "Timesheet Approval Pending Notification"

    Public Const PENDING_TIMEENTRY_NOTIFICATION_INFORMATION_FORM = "Timesheet Notification"
    Public Const PENDING_TIMEENTRY_NOTIFICATION_INFORMATION_FORM_FOR_ADMINISTRATOR = "Timesheet Notification For Administrator"
    Public Const PENDING_TIMEENTRY_NOTIFICATION_INFORMATION_FORM_FOR_EMPLOYEE_MANAGER = "Timesheet Notification For Employee Manager"
    Public Const PENDING_TIMEENTRY_NOTIFICATION_INFORMATION_FORM_FOR_PROJECT_MANAGER = "Timesheet Notification For Project Manager"
    Public Const PENDING_TIMEENTRY_NOTIFICATION_INFORMATION_FORM_FOR_TEAM_LEAD = "Timesheet Notification For Team Lead"

    Public Const TIMESHEET_OVERDUE_NOTIFICATION_INFORMATION_FORM = "Timesheet Overdue Notification"
    Public Const TIMESHEET_OVERDUE_NOTIFICATION_INFORMATION_FORM_FOR_ADMINISTRATOR = "Timesheet Overdue Notification For Administrator"
    Public Const TIMESHEET_OVERDUE_NOTIFICATION_INFORMATION_FORM_FOR_EMPLOYEE_MANAGER = "Timesheet Overdue Notification For Employee Manager"
    Public Const TIMESHEET_OVERDUE_NOTIFICATION_INFORMATION_FORM_FOR_PROJECT_MANAGER = "Timesheet Overdue Notification For Project Manager"
    Public Const TIMESHEET_OVERDUE_NOTIFICATION_INFORMATION_FORM_FOR_TEAM_LEAD = "Timesheet Overdue Notification For Team Lead"

    Public Const EXPENSE_APPROVED_NOTIFICATION_INFORMATION_FROM = "Expense Approved Notification"
    Public Const EXPENSE_REJECTED_NOTIFICATION_INFORMATION_FROM = "Expense Rejected Notification"
    Public Const EXPENSE_APPROVAL_PENDING_NOTIFICATION_INFORMATION_FROM = "Expense Approval Pending Notification"

    Public Const TIME_OFF_APPROVED_NOTIFICATION_INFORMATION_FROM = "Time Off Approved Notification"
    Public Const TIME_OFF_REJECTED_NOTIFICATION_INFORMATION_FROM = "Time Off Rejected Notification"
    Public Const TIME_OFF_REQUEST_APPROVAL_PENDING_NOTIFICATION_INFORMATION_FROM = "Time Off Request Approval Notification"
    Public Const TIMEOFF_APPROVAL_PENDING_NOTIFICATION_INFORMATION_FROM = "Time Off Approval Notification"

    Public Const EMPLOYEE_LOGIN_INFORMATION_FROM = "MASTMS Login Information"
    Public Const TIMELIVE_DOWNLOAD_INFORMATION_FROM = "MASTMS Download Information"
    Public Const TIMELIVE_DEMO_REQUEST_FROM = "MASTMS Demo Request Information"
    Public Const TIMELIVE_PASSWORD_RESET_INFORMATION_FROM = "MASTMS Password Reset Information"
    Public Const TIMELIVE_PASSWORD_CONFIRM_INFORMATION_FROM = "MASTMS Password Reset Verification"

    Public Const EMAIL_FROM_TIMELIVE_SUPPORT = "info@mastechnology.net"
    Public Const EMAIL_FROM_TIMELIVE_SALES = "info@mastechnology.net"
    Public Const EMAIL_TO_TIMELIVE_SALES = "info@mastechnology.net"

    Public Shared DequeueInProgress As Boolean = False
    Public Shared SystemCurrentDateTime As DateTime

    Public Shared Function SendEMail(ByVal Subject As String, ByVal TemplateName As String, ByVal nameValue As StringDictionary, ByVal MailTo As String, Optional ByVal MailToDisplayName As String = "", Optional ByVal MailToCC As String = "", Optional ByVal FromAddressDisplayName As String = "", Optional ByVal FromAddress As String = "", Optional ByVal BCCToSales As Boolean = False) As Boolean

        LoggingBLL.WriteToLog("Before Create the MailMessage instance" & " " & TemplateName)

        FromAddressDisplayName = FromAddressDisplayName.Replace("TimeLive", UIUtilities.GetCompanyNameByApplication())
        'If DBUtilities.IsApplicationContext Then
        '    Subject = ResourceHelper.GetFromResource(Subject)
        '    FromAddressDisplayName = ResourceHelper.GetFromResource(FromAddressDisplayName)
        'End If


        Dim AccountId As Integer




        AccountId = 1
        ' Subject = ResourceHelper.GetFromResource(Subject, AccountId)
        FromAddressDisplayName = "Attendance System" ' ResourceHelper.GetFromResource(FromAddressDisplayName, AccountId)


        If FromAddressDisplayName = "" Then
            FromAddressDisplayName = DBUtilities.GetFromEmailAddressDisplayName
        End If


        FromAddress = DBUtilities.GetFromEmailAddress




        '(1) Create the MailMessage instance
        Dim mm As New SmartMassEmail.Entities.EmailMessage

        With mm
            .ID = Guid.NewGuid
            .Priority = 0
            .EmailSubject = Subject
            .EmailTo = MailTo
            .EmailCC = FromAddressDisplayName
            .EmailFrom = FromAddress
            If BCCToSales = True Then
                .EmailBCC = EMAIL_TO_TIMELIVE_SALES
            End If
            .ArrivedDateTime = DateTime.Now
            .ChangeStamp = DateTime.Now
            .ExpiryDatetime = DateTime.Now.AddDays(1)
            .MaximumRetry = 3
            .NumberOfRetry = 0
            .RetryTime = DateTime.Now
            .SenderInfo = "web"
            .Status = 0
            If TemplateName = "TimeEntryPendingEmployeesTemplate" Or TemplateName = "ExpensePendingTemplate" Or TemplateName = "TimesheetPendingTemplate" Or TemplateName = "TimeEntryPendingTemplate" Or TemplateName = "TimeOffRequestPendingTemplate" Or TemplateName = "TimeOffPendingTemplate" Or TemplateName = "TimesheetOverdueTemplate" Or TemplateName = "TimesheetDueTemplate" Then
                .IsHtml = True
            End If
        End With

        LoggingBLL.WriteToLog("After Create the MailMessage instance" & " " & TemplateName)
        LoggingBLL.WriteToLog("Before GetTemplatedMessage" & " " & TemplateName)

        Dim templatedMessage As SmartMassEmail.Entities.EmailMessage = EmailTemplate.GetTemplatedMessage(TemplateName, mm, nameValue)

        LoggingBLL.WriteToLog("After GetTemplatedMessage" & " " & TemplateName)

        Dim parameters As Object() = New Object() {templatedMessage}
        EMailUtilities.Send(parameters)

    End Function
    Public Shared Sub Send(ByVal Data As Object)
        Dim templatedMessage As SmartMassEmail.Entities.EmailMessage = Data(0)
        Dim MailType As String = ConfigurationManager.AppSettings("MailType").ToString()
        If MailType = "SMTP" Then
            EmailQueue.Send(templatedMessage)
        ElseIf MailType = "EXCHANGE" Then
            SendMailUsingExchange(templatedMessage)
        End If

    End Sub


    Public Shared Function SendMailUsingExchange(ByVal templatedMessage As SmartMassEmail.Entities.EmailMessage) As Boolean
        Try
            Dim MailUser As String = ConfigurationManager.AppSettings("emailsender").ToString()
            Dim MailPass As String = ConfigurationManager.AppSettings("password").ToString()
            Dim subject As String = templatedMessage.EmailSubject
            Dim MailText As String = templatedMessage.EmailBody
            Dim MailTo As String = templatedMessage.EmailTo

            Dim service As ExchangeService = New ExchangeService(ExchangeVersion.Exchange2010_SP1)
            service.Credentials = New NetworkCredential(MailUser, MailPass)
            service.AutodiscoverUrl(MailUser)
            Dim emailMessage As Microsoft.Exchange.WebServices.Data.EmailMessage = New Microsoft.Exchange.WebServices.Data.EmailMessage(service)
            emailMessage.Subject = subject
            emailMessage.Body = New MessageBody(MailText)
            emailMessage.ToRecipients.Add(MailTo)
            emailMessage.SendAndSaveCopy()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Shared Function GetPreparedNameValue(ByVal message As SmartMassEmail.Entities.EmailMessage) As StringDictionary
        Dim dict As New StringDictionary
        dict.Add("[recievername]", "Shahed")
        dict.Add("[sendername]", "Khan")
        dict.Add("[body]", message.EmailBody)
        Return dict

    End Function
    Public Shared Sub DequeueEmail()
        If UIUtilities.EnableDequeueMailSend() Then
            DequeueEmailWithoutThread()
        End If
    End Sub
    Public Shared Sub DequeueEmailWithoutThread()
        LoggingBLL.WriteToLog("DequeueEmailWithoutThread: Initiated")
        LoggingBLL.WriteToLog("DequeueEmailWithoutThread: DequeueInProgress=" & DequeueInProgress)
        DequeueInProgress = True
        Dim list As TList(Of SmartMassEmail.Entities.EmailMessage)
        While True
            list = EmailDeQueue.Recieve
            LoggingBLL.WriteToLog("DequeueEmailWithoutThread: EmailListCount=" & list.Count)
            If list.Count > 0 Then
                EmailDispatchThread(list)
            Else
                Exit While
            End If
        End While
        LoggingBLL.WriteToLog("DequeueEmailWithoutThread: Finished")
        DequeueInProgress = False
    End Sub
    Public Shared Sub EmailDispatchThread(ByVal list As Object)
        LoggingBLL.WriteToLog("EmailDispatchThread: EmailListCount=" & list.Count)
        Dim ParameterizedThreadStart As New ParameterizedThreadStart(AddressOf DispatchThread)
        Dim newThread As New Thread(ParameterizedThreadStart)
        newThread.Priority = ThreadPriority.Highest
        newThread.Start(list)
        Thread.Sleep(10)
    End Sub
    Public Shared Sub DispatchThread(ByVal list As Object)
        EmailDispatch.Dispatch(list)
    End Sub
    Public Function SendEMail(ByVal Subject As String, ByVal Body As String, ByVal MailTo As String, ByVal MailToDisplayName As String, Optional ByVal MailToCC As String = "") As Boolean
        Dim parameters As Object() = New Object() {Subject, Body, MailTo, MailToDisplayName}
        Dim pts As New ParameterizedThreadStart(AddressOf Send)
        Dim t As New Threading.Thread(pts)
        t.Priority = ThreadPriority.Lowest
        t.Name = "Send"
        t.Start(parameters)
    End Function
    'Public Shared Function GetTimeZoneId(ByVal AccountEmployeeID As Integer) As Byte
    '    Dim dt As AccountEmployee.AccountEmployeeDataTable = New AccountEmployeeBLL().GetAccountEmployeesByAccountEmployeeId(AccountEmployeeID)
    '    Dim dr As AccountEmployee.AccountEmployeeRow
    '    If dt.Rows.Count > 0 Then
    '        dr = dt.Rows(0)
    '        If Not IsDBNull(dr.Item("TimeZoneId")) Then
    '            Return dr.TimeZoneId
    '        End If
    '    End If
    '    Return 6
    'End Function
    'Public Shared Function SendScheduledEmail()
    '    SystemCurrentDateTime = SMDateTime.Now
    '    LoggingBLL.WriteToLog("Initiated:SendScheduledEmail: SystemCurrentDateTime " & SystemCurrentDateTime.ToString)
    '    'sending scheduled email
    '    EMailUtilities.SendScheduledEMailForTimesheetApprovalPending()
    '    EMailUtilities.SendScheduledEMailForExpenseApprovalPending()
    '    EMailUtilities.SendScheduledEMailForTimeEntryPending()
    '    EMailUtilities.SendScheduledEMailForTimeEntryPendingForAdministrator()
    '    EMailUtilities.SendScheduledEMailForTimeEntryPendingForEmployeeManager()
    '    EMailUtilities.SendScheduledEMailForTimeEntryPendingForProjectManager()
    '    EMailUtilities.SendScheduledEMailForTimeEntryPendingForTeamLead()
    '    EMailUtilities.SendScheduledEMailForTimesheetApprovalPendingForTimeOff()
    '    EMailUtilities.SendScheduledEMailForTimeOffApprovalPending()
    '    EMailUtilities.SendScheduledEMailForTimesheetDue()
    '    EMailUtilities.SendScheduledEMailForTimesheetOverdue()
    '    EMailUtilities.SendScheduledEMailForTimesheetOverdueAdministrator()
    '    EMailUtilities.SendScheduledEMailForTimesheetOverdueEmployeeManager()
    '    EMailUtilities.SendScheduledEMailForTimesheetOverdueProjectManager()
    '    EMailUtilities.SendScheduledEMailForTimesheetOverdueTeamLead()
    '    'update
    '    EMailUtilities.UpdateScheduledEMailForTimesheetApprovalPending()
    '    EMailUtilities.UpdateScheduledEMailForExpenseApprovalPending()
    '    EMailUtilities.UpdateScheduledEMailForTimeEntryPending()
    '    EMailUtilities.UpdateScheduledEMailForTimeEntryPendingForAdministrator()
    '    EMailUtilities.UpdateScheduledEMailForTimeEntryPendingForEmployeeManager()
    '    EMailUtilities.UpdateScheduledEMailForTimeEntryPendingForProjectManager()
    '    EMailUtilities.UpdateScheduledEMailForTimeEntryPendingForTeamLead()
    '    EMailUtilities.UpdateScheduledEMailForTimeOffApprovalPending()
    '    EMailUtilities.UpdateScheduledEMailForTimesheetApprovalPendingForTimeOff()
    '    EMailUtilities.UpdateScheduledEMailForTimesheetDue()
    '    EMailUtilities.UpdateScheduledEMailForTimesheetOverdue()
    '    EMailUtilities.UpdateScheduledEMailForTimesheetOverdueForAdministrator()
    '    EMailUtilities.UpdateScheduledEMailForTimesheetOverdueForEmployeeManager()
    '    EMailUtilities.UpdateScheduledEMailForTimesheetOverdueForProjectManager()
    '    EMailUtilities.UpdateScheduledEMailForTimesheetOverdueForTeamLead()
    '    LoggingBLL.WriteToLog("Finished:SendScheduledEmail: SystemCurrentDateTime " & SystemCurrentDateTime.ToString & " Now " & Now)
    'End Function
    Public Shared Function IsCurrentDaySetForEmail(ByVal dr As DataRow, ByVal UserDate As DateTime) As Boolean
        If [Enum].Format(GetType(DayOfWeek), UserDate.DayOfWeek, "G") = "Monday" Then
            Return dr.Item("Monday")
        ElseIf [Enum].Format(GetType(DayOfWeek), UserDate.DayOfWeek, "G") = "Tuesday" Then
            Return dr.Item("Tuesday")
        ElseIf [Enum].Format(GetType(DayOfWeek), UserDate.DayOfWeek, "G") = "Wednesday" Then
            Return dr.Item("Wednesday")
        ElseIf [Enum].Format(GetType(DayOfWeek), UserDate.DayOfWeek, "G") = "Thursday" Then
            Return dr.Item("Thursday")
        ElseIf [Enum].Format(GetType(DayOfWeek), UserDate.DayOfWeek, "G") = "Friday" Then
            Return dr.Item("Friday")
        ElseIf [Enum].Format(GetType(DayOfWeek), UserDate.DayOfWeek, "G") = "Saturday" Then
            Return dr.Item("Saturday")
        ElseIf [Enum].Format(GetType(DayOfWeek), UserDate.DayOfWeek, "G") = "Sunday" Then
            Return dr.Item("Sunday")
        End If
        Return False
    End Function
    'Public Shared Function SendScheduledEMailForTimesheetApprovalPending()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL
    '        Dim dt As TimeLiveDataSet.vueAccountEmployeeTimeEntryApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdDataTable = BLL.GetPendingTimeEntryApprovalsWithPreferenceGroupByApproverEmployeeId
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPending: Total Employees are " & dt.Rows.Count)
    '            Dim dr As TimeLiveDataSet.vueAccountEmployeeTimeEntryApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(New AccountEmployeeBLL().GetAccountIdByAccountEmployeeId(dr.ApproverEmployeeId)) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPending: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTime(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPending: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)

    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPending: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetPendingEMail(dr.ApproverEmployeeId)
    '                            LoggingBLL.WriteToLog("SendTimesheetApprovalPendingScheduledEMail Email recive by this employee: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPending Update IsEmailSend = 1: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetPendingEMail(dr.ApproverEmployeeId)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPending Email recive by this employee: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPending Update IsEmailSend = 1: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPending TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimesheetApprovalPending", ex)
    '    End Try
    'End Function
    ''TIme Off Pending Email
    'Public Shared Function SendScheduledEMailForTimesheetApprovalPendingForTimeOff()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeOffRequestBLL
    '        Dim dt As AccountEmployeeTimeOffRequest.vueAccountEmployeeTimeEntryApprovalPendingEmaiWithPreferencelForTimeOffGroupByApproverEmployeeIdDataTable = BLL.GetPendingTimeEntryApprovalsWithPreferenceTimeEntryForTimeOffGroupByApproverEmployeeId
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeOffRequest.vueAccountEmployeeTimeEntryApprovalPendingEmaiWithPreferencelForTimeOffGroupByApproverEmployeeIdRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(New AccountEmployeeBLL().GetAccountIdByAccountEmployeeId(dr.ApproverEmployeeId)) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId)).Date & " " & AccountEmployeeTimeOffRequestBLL.GetDefaultScheduleEmailSendTime(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeOffRequestBLL().SendTimeEntryForTimeOffPendingEMail(dr.ApproverEmployeeId)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff Email recive by this employee: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff Update IsEmailSend = 1: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeOffRequestBLL().SendTimeEntryForTimeOffPendingEMail(dr.ApproverEmployeeId)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff Email recive by this employee: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff Update IsEmailSend = 1: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimesheetApprovalPendingForTimeOff", ex)
    '    End Try
    'End Function
    ''TIme Off Req Pending Email
    'Public Shared Function SendScheduledEMailForTimeOffApprovalPending()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeOffRequestBLL
    '        Dim dt As AccountEmployeeTimeOffRequest.vueAccountEmployeeTimeOffRequestApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdDataTable = BLL.GetPendingTimeOffRequestApprovalsWithPreferenceForGroupByApproverEmployeeId
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeOffApprovalPending: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeOffRequest.vueAccountEmployeeTimeOffRequestApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(New AccountEmployeeBLL().GetAccountIdByAccountEmployeeId(dr.ApproverEmployeeId)) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeOffApprovalPending: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId)).Date & " " & AccountEmployeeTimeOffRequestBLL.GetDefaultScheduleEmailSendTimeForTimeOff(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeOffApprovalPending: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimeOffApprovalPending: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeOffRequestBLL().SendTimeOffPendingEMail(dr.ApproverEmployeeId)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeOffApprovalPending Email recive by this employee: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeOffApprovalPending Update IsEmailSend = 1: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeOffRequestBLL().SendTimeOffPendingEMail(dr.ApproverEmployeeId)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeOffApprovalPending Email recive by this employee: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeOffApprovalPending Update IsEmailSend = 1: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeOffApprovalPending TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimeOffApprovalPending", ex)
    '    End Try
    'End Function

    'Public Shared Function SendScheduledEMailForExpenseApprovalPending()
    '    Try
    '        Dim BLL As New AccountExpenseEntryBLL
    '        Dim dt As TimeLiveDataSet.vueAccountExpenseEntryApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdDataTable = BLL.GetPendingExpenseEntryApprovalsWithPreferenceGroupByApproverEmployeeId
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForExpenseApprovalPending: Total Employees are " & dt.Rows.Count)
    '            Dim dr As TimeLiveDataSet.vueAccountExpenseEntryApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(New AccountEmployeeBLL().GetAccountIdByAccountEmployeeId(dr.ApproverEmployeeId)) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForExpenseApprovalPending: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId)).Date & " " & AccountExpenseEntryBLL.GetDefaultScheduleEmailSendTime(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForExpenseApprovalPending: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForExpenseApprovalPending: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountExpenseEntryBLL().SendExpensePendingEMail(dr.ApproverEmployeeId)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForExpenseApprovalPending Email recive by this employee: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForExpenseApprovalPending Update IsEmailSend = 1: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountExpenseEntryBLL().SendExpensePendingEMail(dr.ApproverEmployeeId)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForExpenseApprovalPending Email recive by this employee: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForExpenseApprovalPending Update IsEmailSend = 1: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForExpenseApprovalPending TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForExpenseApprovalPending", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimeEntryPending()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As TimeLiveDataSet.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceDataTable = BLL.GetPendingTimeEntryWithPreference()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPending: Total Employees are " & dt.Rows.Count)
    '            Dim dr As TimeLiveDataSet.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPending: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntry(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPending: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPending: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMail(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, dr.CultureInfoName, IIf(Not IsDBNull(dr.Item("SystemTimesheetPeriodType")), dr.Item("SystemTimesheetPeriodType"), "Weekly"), IIf(Not IsDBNull(dr.Item("EmployeeWeekStartDay")), dr.Item("EmployeeWeekStartDay"), 1), dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPending Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPending Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMail(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, dr.CultureInfoName, IIf(Not IsDBNull(dr.Item("SystemTimesheetPeriodType")), dr.Item("SystemTimesheetPeriodType"), "Weekly"), IIf(Not IsDBNull(dr.Item("EmployeeWeekStartDay")), dr.Item("EmployeeWeekStartDay"), 1), dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPending Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPending Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPending TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimeEntryPending", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimeEntryPendingForAdministrator()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForAdministratorDataTable = BLL.GetPendingTimeEntryWithPreferenceForAdministrator()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForAdministrator: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForAdministratorRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForAdministrator: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForAdministrator: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForAdministrator: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId & " For Administrator")
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMailForADMINAndPMAndTLAndEM(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "Administrator", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForAdministrator Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForAdministrator Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMailForADMINAndPMAndTLAndEM(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "Administrator", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForAdministrator Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForAdministrator Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForAdministrator TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimeEntryPendingForAdministrator", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimeEntryPendingForEmployeeManager()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForEmployeeManagerDataTable = BLL.GetPendingTimeEntryWithPreferenceForEmployeeManager()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForEmployeeManagerRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId & " For Employee Manager")
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMailForADMINAndPMAndTLAndEM(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "EmployeeManager", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMailForADMINAndPMAndTLAndEM(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "EmployeeManager", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimeEntryPendingForEmployeeManager", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimeEntryPendingForProjectManager()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForProjectManagerDataTable = BLL.GetPendingTimeEntryWithPreferenceForProjectManager()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForProjectManager: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForProjectManagerRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForProjectManager: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForProjectManager: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForProjectManager: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId & "For Project Manager")
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMailForADMINAndPMAndTLAndEM(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "ProjectManager", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForProjectManager Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForProjectManager Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMailForADMINAndPMAndTLAndEM(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "ProjectManager", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForProjectManager Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForProjectManager Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForProjectManager TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimeEntryPendingForProjectManager", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimeEntryPendingForTeamLead()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForTeamLeadDataTable = BLL.GetPendingTimeEntryWithPreferenceForTeamLead()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForTeamLead: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForTeamLeadRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForTeamLead: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForTeamLead: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForTeamLead: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId & " For Team Lead")
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMailForADMINAndPMAndTLAndEM(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "TeamLead", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForTeamLead Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForTeamLead Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And IsCurrentDaySetForEmail(dr, UserCurrentDateTime) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimeEntryPendingEMailForADMINAndPMAndTLAndEM(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "TeamLead", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForTeamLead Email recive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForTeamLead Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimeEntryPendingForTeamLead TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimeEntryPendingForTeamLead", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimesheetDue()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetDueNotificationEmailWithPreferenceDataTable = BLL.GetTimesheetDueNotificationEmailWithPreference
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetDue: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetDueNotificationEmailWithPreferenceRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetDue: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetDue: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetDue: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And BLL.IsCurrentTimesheetdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, dr.CultureInfoName, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth, dr.TimesheetOverduePeriods, dr.MinimumHoursPerWeek)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetDue Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetDue Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And BLL.IsCurrentTimesheetdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, dr.CultureInfoName, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth, dr.TimesheetOverduePeriods, dr.MinimumHoursPerWeek)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetDue Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetDue Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetDue TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimesheetDue", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimesheetOverdue()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceDataTable = BLL.GetTimesheetOverdueWithPreference()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdue: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdue: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdue: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdue: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "EmployeeOwn", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdue Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdue Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "EmployeeOwn", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdue Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdue Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdue TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimesheetOverdue", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimesheetOverdueAdministrator()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForAdministratorDataTable = BLL.GetTimesheetOverdueWithPreferenceForAdministrator()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueAdministrator: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForAdministratorRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueAdministrator: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueAdministrator: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueAdministrator: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId & " For Administrator")
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "Administrator", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueAdministrator Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueAdministrator Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "Administrator", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueAdministrator Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueAdministrator Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueAdministrator TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimesheetOverdueAdministrator", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimesheetOverdueEmployeeManager()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForEmployeeManagerDataTable = BLL.GetTimesheetOverdueWithPreferenceForEmployeeManager()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForEmployeeManagerRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId & " For Employee Manager")
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "EmployeeManager", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "EmployeeManager", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimesheetOverdueEmployeeManager", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimesheetOverdueProjectManager()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForProjectManagerDataTable = BLL.GetTimesheetOverdueWithPreferenceForProjectManager()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueProjectManager: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForProjectManagerRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueProjectManager: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueProjectManager: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueProjectManager: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId & " For Project Manager")
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "ProjectManager", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueProjectManager Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueProjectManager Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "ProjectManager", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueProjectManager Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueProjectManager Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueProjectManager TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimesheetOverdueProjectManager", ex)
    '    End Try
    'End Function
    'Public Shared Function SendScheduledEMailForTimesheetOverdueTeamLead()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForTeamLeadDataTable = BLL.GetTimesheetOverdueWithPreferenceForTeamLead()
    '        If dt.Rows.Count > 0 Then
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueTeamLead: Total Employees are " & dt.Rows.Count)
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForTeamLeadRow = dt.Rows(0)
    '            Dim starttime As DateTime = Now
    '            Dim totalemployees As Integer
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim TodaySendTime As DateTime
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueTeamLead: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)

    '                    TodaySendTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId)).Date & " " & AccountEmployeeTimeEntryBLL.GetDefaultScheduleEmailSendTimeForPendingTimeEntryByDataRow(dr).ToLongTimeString()
    '                    LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueTeamLead: TodaySendTime" & " " & TodaySendTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    If Not IsDBNull(dr.Item("LastScheduledEmailSentTime")) Then
    '                        LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueTeamLead: LastScheduledEmailSentTime" & " " & dr.LastScheduledEmailSentTime.Date & " AccountEmployeeId = " & dr.AccountEmployeeId & " For Team Lead")
    '                        If UserCurrentDateTime > TodaySendTime And dr.LastScheduledEmailSentTime.Date < UserCurrentDateTime.Date And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "TeamLead", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueTeamLead Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueTeamLead Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    Else
    '                        If UserCurrentDateTime > TodaySendTime And BLL.IsCurrentTimesheetOverdue(dr.AccountEmployeeId, UserCurrentDateTime, dr.TimesheetOverdueAfterDays, dr.SystemTimesheetPeriodType, dr.WeekStartDay, dr.SystemInitialDayOfFirstPeriod, dr.SystemInitialDayOfLastPeriod, dr.InitialDayOfTheMonth) Then
    '                            Call New AccountEmployeeTimeEntryBLL().SendTimesheetOverdueNotification(dr.AccountId, dr.AccountEmployeeId, dr.EmployeeName, dr.EMailAddress, "TeamLead", dr.CultureInfoName)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueTeamLead Email receive by this employee: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, True)
    '                            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueTeamLead Update IsEmailSend = 1: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                            totalemployees += 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Dim endtime As DateTime = Now
    '            Dim TotalSecond As Double = (endtime - starttime).TotalSeconds
    '            LoggingBLL.WriteToLog("SendScheduledEMailForTimesheetOverdueTeamLead TotalSeconds = " & TotalSecond & " totalemployees = " & totalemployees)
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("SendScheduledEMailForTimesheetOverdueTeamLead", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimesheetApprovalPending()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL
    '        Dim dt As TimeLiveDataSet.vueAccountEmployeeTimeEntryApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdDataTable = BLL.GetPendingTimeEntryApprovalsWithPreferenceGroupByApproverEmployeeId
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As TimeLiveDataSet.vueAccountEmployeeTimeEntryApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId))
    '                LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetApprovalPending: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                LoggingBLL.WriteToLog("Now" & " " & Now)
    '                Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.ApproverEmployeeId, UserCurrentDateTime)
    '                Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, False)
    '                LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetApprovalPending Update IsEmailSend = 0: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimesheetApprovalPending", ex)
    '    End Try
    'End Function

    'Public Shared Function UpdateScheduledEMailForTimeOffApprovalPending()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeOffRequestBLL
    '        Dim dt As AccountEmployeeTimeOffRequest.vueAccountEmployeeTimeOffRequestApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdDataTable = BLL.GetPendingTimeOffRequestApprovalsWithPreferenceForGroupByApproverEmployeeId
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeOffRequest.vueAccountEmployeeTimeOffRequestApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(New AccountEmployeeBLL().GetAccountIdByAccountEmployeeId(dr.ApproverEmployeeId)) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeOffApprovalPending: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.ApproverEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeOffApprovalPending Update IsEmailSend = 0: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimeOffApprovalPending", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimesheetApprovalPendingForTimeOff()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeOffRequestBLL
    '        Dim dt As AccountEmployeeTimeOffRequest.vueAccountEmployeeTimeEntryApprovalPendingEmaiWithPreferencelForTimeOffGroupByApproverEmployeeIdDataTable = BLL.GetPendingTimeEntryApprovalsWithPreferenceTimeEntryForTimeOffGroupByApproverEmployeeId
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeOffRequest.vueAccountEmployeeTimeEntryApprovalPendingEmaiWithPreferencelForTimeOffGroupByApproverEmployeeIdRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(New AccountEmployeeBLL().GetAccountIdByAccountEmployeeId(dr.ApproverEmployeeId)) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetApprovalPendingForTimeOff: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.ApproverEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetApprovalPendingForTimeOff Update IsEmailSend = 0: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimesheetApprovalPendingForTimeOff", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForExpenseApprovalPending()
    '    Try
    '        Dim BLL As New AccountExpenseEntryBLL
    '        Dim dt As TimeLiveDataSet.vueAccountExpenseEntryApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdDataTable = BLL.GetPendingExpenseEntryApprovalsWithPreferenceGroupByApproverEmployeeId
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As TimeLiveDataSet.vueAccountExpenseEntryApprovalPendingEmailWithPreferenceGroupByApproverEmployeeIdRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(New AccountEmployeeBLL().GetAccountIdByAccountEmployeeId(dr.ApproverEmployeeId)) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.ApproverEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForExpenseApprovalPending: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.ApproverEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.ApproverEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForExpenseApprovalPending Update IsEmailSend = 0: AccountEmployeeId = " & dr.ApproverEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForExpenseApprovalPending", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimeEntryPending()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As TimeLiveDataSet.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceDataTable = BLL.GetPendingTimeEntryWithPreference()
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As TimeLiveDataSet.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPending: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPending Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimeEntryPending", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimeEntryPendingForAdministrator()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForAdministratorDataTable = BLL.GetPendingTimeEntryWithPreferenceForAdministrator()
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForAdministratorRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPendingForAdministrator: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPendingForAdministrator Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimeEntryPendingForAdministrator", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimeEntryPendingForEmployeeManager()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForEmployeeManagerDataTable = BLL.GetPendingTimeEntryWithPreferenceForEmployeeManager()
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForEmployeeManagerRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPendingForEmployeeManager: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPendingForEmployeeManager Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimeEntryPendingForEmployeeManager", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimeEntryPendingForProjectManager()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForProjectManagerDataTable = BLL.GetPendingTimeEntryWithPreferenceForProjectManager()
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForProjectManagerRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPendingForProjectManager: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPendingForProjectManager Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimeEntryPendingForProjectManager", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimeEntryPendingForTeamLead()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForTeamLeadDataTable = BLL.GetPendingTimeEntryWithPreferenceForTeamLead()
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueAccountEmployeeTimeEntryPendingNotificationEmailWithPreferenceForTeamLeadRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPendingForTeamLead: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimeEntryPendingForTeamLead Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimeEntryPendingForTeamLead", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimesheetDue()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetDueNotificationEmailWithPreferenceDataTable = BLL.GetTimesheetDueNotificationEmailWithPreference()
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetDueNotificationEmailWithPreferenceRow = dt.Rows(0)
    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetDue: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetDue Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimesheetDue", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimesheetOverdue()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceDataTable = BLL.GetTimesheetOverdueWithPreference()
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdue: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdue Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimesheetOverdue", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimesheetOverdueForAdministrator()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForAdministratorDataTable = BLL.GetTimesheetOverdueWithPreferenceForAdministrator
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForAdministratorRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdueForAdministrator: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdueForAdministrator Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimesheetOverdueForAdministrator", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimesheetOverdueForEmployeeManager()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForEmployeeManagerDataTable = BLL.GetTimesheetOverdueWithPreferenceForEmployeeManager
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForEmployeeManagerRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdueForEmployeeManager: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdueForEmployeeManager Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimesheetOverdueForEmployeeManager", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimesheetOverdueForProjectManager()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForProjectManagerDataTable = BLL.GetTimesheetOverdueWithPreferenceForProjectManager
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForProjectManagerRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdueForProjectManager: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdueForProjectManager Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimesheetOverdueForProjectManager", ex)
    '    End Try
    'End Function
    'Public Shared Function UpdateScheduledEMailForTimesheetOverdueForTeamLead()
    '    Try
    '        Dim BLL As New AccountEmployeeTimeEntryBLL

    '        Dim dt As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForTeamLeadDataTable = BLL.GetTimesheetOverdueWithPreferenceForTeamLead()
    '        If dt.Rows.Count > 0 Then
    '            Dim dr As AccountEmployeeTimeEntry.vueTimesheetOverdueNotificationWithPreferenceForTeamLeadRow = dt.Rows(0)

    '            For Each dr In dt.Rows
    '                If Not LicensingBLL.IsFreeAccount(dr.AccountId) Then
    '                    Dim UserCurrentDateTime As DateTime = LocaleUtilitiesBLL.GetCurrentDateTimeFromUserTimeZoneForSMTimeZone(SystemCurrentDateTime, GetTimeZoneId(dr.AccountEmployeeId))
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdueForTeamLead: UserCurrentDateTime" & " " & UserCurrentDateTime & " AccountEmployeeId = " & dr.AccountEmployeeId)
    '                    LoggingBLL.WriteToLog("Now" & " " & Now)
    '                    Call New AccountEmployeeBLL().UpdateLastScheduledEmailSentTimeByAccountEmployeeId(dr.AccountEmployeeId, UserCurrentDateTime)
    '                    Call New AccountEmployeeBLL().UpdateIsEmailSendByAccountEmployeeId(dr.AccountEmployeeId, False)
    '                    LoggingBLL.WriteToLog("UpdateScheduledEMailForTimesheetOverdueForTeamLead Update IsEmailSend = 0: AccountEmployeeId = " & dr.AccountEmployeeId)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LoggingBLL.WriteExceptionToLog("UpdateScheduledEMailForTimesheetOverdueForTeamLead", ex)
    '    End Try
    'End Function
End Class
