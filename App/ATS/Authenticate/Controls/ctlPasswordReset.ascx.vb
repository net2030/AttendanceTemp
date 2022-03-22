Imports Microsoft.Exchange.WebServices.Data
Imports System.Net
Imports System.Net.Mail
Imports System.Diagnostics
Imports System.IO

Partial Class Authenticate_Controls_ctlPasswordReset
    Inherits System.Web.UI.UserControl

    Protected Sub btnPasswordReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPasswordReset.Click
        Me.Label1.Visible = False
        Me.Label2.Visible = False

        If SendPasswordResetVerificationCode(Me.txtEmailAddress.Text) <> True Then
            Me.Label2.Visible = True
        Else
            Me.Label1.Text = Resources.MulResource.PasswordResetCheckYourEmail & Me.txtEmailAddress.Text
            Me.Label1.Visible = True
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If UIUtilities.GetApplicationMode = "Installed" Then
            Me.Page.Title = ResourceHelper.GetFromResource(Me.Page.Title)
        Else
        End If
    End Sub

    Public Function SendPasswordResetVerificationCode(ByVal EmailAddress As String) As Boolean
        Dim PasswordVerificationCode As New Guid
        PasswordVerificationCode = System.Guid.NewGuid

        Dim link = Request.Url.ToString().Replace("PasswordReset.aspx", "") & "PasswordConfirm.aspx?Code=" & PasswordVerificationCode.ToString & "&EmailAddress=" & EmailAddress
        ' Dim link As String = System.Configuration.ConfigurationManager.AppSettings("SitePrefix") & "Authenticate/PasswordConfirm.aspx?Code=" & PasswordVerificationCode.ToString & "&EmailAddress=" & EmailAddress
        Dim moEmployee As ATS.BO.Framework.BOEmployee = New ATS.BO.Framework.BOEmployee
        Dim ds As System.Data.DataSet = moEmployee.GetEmployeeByEmail(EmailAddress)
        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            moEmployee.UpdatePasswordVerificationCodeByEmployeeId(dr("EmployeeId"), PasswordVerificationCode)


            Dim sendType As String = ConfigurationManager.AppSettings("MailType").ToString()
            If sendType = "SMTP" Then
                SendMailUsingSMTP(dr, EmailAddress, link)
            Else
               ' SendMailUsingConsoleApplication(dr, EmailAddress, link)
                SendMailUsingExchange(EmailAddress, dr("FirstName") & " " & dr("LastName"), dr("EmployeeNameEnglish"), link)
            End If


            'EMailUtilities.SendEMail(Resources.MulResource.PasswordReset, "PasswordVerificationCodeTemplate", GetPreparedNameValueForPasswordConfirm(EmailAddress, link, dr("FirstName") & " " & dr("LastName")), EmailAddress, , , EMailUtilities.TIMELIVE_PASSWORD_CONFIRM_INFORMATION_FROM)
            'EMailUtilities.DequeueEmail()
            Return True
        End If
    End Function

    Public Function GetPreparedNameValueForPasswordConfirm(ByVal EmailAddress As String, ByVal link As String, ByVal FullName As String) As StringDictionary
        Dim dict As New StringDictionary
        dict.Add("[emailaddress]", EmailAddress)
        dict.Add("[fullname]", FullName)
        dict.Add("[link]", link)
        dict.Add("[companyname]", System.Configuration.ConfigurationManager.AppSettings("CompanyName"))
        Return dict
    End Function


    Private Sub SendMailUsingConsoleApplication(ByVal dr As DataRow, ByVal EmailAddress As String, ByVal link As String)

        Dim WorkPath As String = System.Web.Hosting.HostingEnvironment.MapPath("~/bin")
        'ConfigurationManager.AppSettings("ForgetPasswordPath").ToString()

        Dim MailUser As String = ConfigurationManager.AppSettings("emailsender").ToString()
        Dim MailPass As String = ConfigurationManager.AppSettings("password").ToString()

        Dim strCmdText As String = MailUser
        strCmdText += " " & MailPass
        strCmdText += " " & EmailAddress
        strCmdText += " """ & dr("FirstName") & " " & dr("LastName") & """ "
        strCmdText += " """ & dr("EmployeeNameEnglish") & """ "

        strCmdText += " """ & link & """ "
        Dim cmd As Process = New Process()
        cmd.StartInfo.FileName = """" & WorkPath & "\ForgetPasswordEmail.exe"""
        'cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        cmd.StartInfo.Arguments = strCmdText
        cmd.Start()
    End Sub

    Private Shared Sub SendMailUsingExchange(ByVal MailTo As String, ByVal EmployeeName As String, ByVal EmployeeNameEn As String, ByVal link As String)
        Dim MailText As String = ""
        Dim MailUser As String = ConfigurationManager.AppSettings("emailsender").ToString()
        Dim MailPass As String = ConfigurationManager.AppSettings("password").ToString()

        Try
            Dim FilePath As String = AppDomain.CurrentDomain.BaseDirectory & "\bin\ForgetPasswordEmailTemplate.html"
            Dim str As StreamReader = New StreamReader(FilePath)
            MailText = str.ReadToEnd()
            str.Close()
            MailText = MailText.Replace("[EmailAddress]", MailTo)
            MailText = MailText.Replace("[EmployeeName]", EmployeeName)
            MailText = MailText.Replace("[EmployeeNameEn]", EmployeeNameEn)
            MailText = MailText.Replace("[Link]", link)
            Dim service As ExchangeService = New ExchangeService(ExchangeVersion.Exchange2013_SP1)
            service.Credentials = New NetworkCredential(MailUser, MailPass)
            service.Url = New Uri("https://webmail.moh.gov.sa/ews/exchange.asmx")
            service.TraceEnabled = True
            Dim emailMessage As EmailMessage = New EmailMessage(service)
            emailMessage.Subject = "Password Reset - ≈” ⁄«œ… ﬂ·„… «·„—Ê—"
            emailMessage.Body = New MessageBody(MailText)
            emailMessage.ToRecipients.Add(MailTo)
            emailMessage.SendAndSaveCopy()
        Catch ex As Exception
            Dim folder As String = "C:\Temp\"
            Dim fileName As String = "EmailException.txt"
            Dim fullPath As String = folder & fileName
            File.WriteAllText(fullPath, ex.Message)
            Dim readText As String = File.ReadAllText(fullPath)
        End Try
    End Sub

    Public Shared Function SendMailUsingExchange(ByVal dr As DataRow, ByVal EmailAddress As String, ByVal link As String) As Boolean
        Try
            Dim MailUser As String = ConfigurationManager.AppSettings("emailsender").ToString()
            Dim MailPass As String = ConfigurationManager.AppSettings("password").ToString()
            Dim subject As String = "≈” ⁄«œ… ﬂ·„… «·„—Ê—"
            Dim MailText As String = "<div style='direction:rtl'>"
            MailText = MailText & "«·„ÊŸ› / ‹…" & dr("FirstName") & " " & dr("LastName") & "<br/>"
            MailText = MailText & "·≈” ⁄«œ… ﬂ·„… «·„—Ê— «·Œ«’… »«·»—Ìœ «·«·ﬂ —Ê‰Ì  " & EmailAddress & "<br/>"
            MailText = MailText & "Ì—ÃÏ «·÷€ÿ ⁄·Ï «·—«»ÿ «· «·Ì   " & "<br/>"
            MailText = MailText & "<div style='direction:ltr'>" & link & "</div><br/>"
            MailText = MailText & "Ê›Ì Õ«·… ⁄œ„ › Õ «·—«»ÿ Ì—ÃÏ ‰”Œ «·—«»ÿ Ê› ÕÂ ›Ì «·„ ’›Õ «·Œ«’ »ﬂ„   " & vbCrLf

            MailText = MailText & "<br/>" & "<br/>" & "<br/>" & "<br/>" & "<br/>"

            MailText = MailText & "›—Ìﬁ ‰Ÿ«„ «·Õ÷Ê— Ê«·«‰’—«›   "

            MailText = MailText & "</div>"



            Dim service As ExchangeService = New ExchangeService(ExchangeVersion.Exchange2013_SP1)
            service.Credentials = New NetworkCredential(MailUser, MailPass)
            service.AutodiscoverUrl(MailUser)
            Dim emailMessage As Microsoft.Exchange.WebServices.Data.EmailMessage = New Microsoft.Exchange.WebServices.Data.EmailMessage(service)
            emailMessage.Subject = subject
            emailMessage.Body = New MessageBody(MailText)
            emailMessage.ToRecipients.Add(EmailAddress)
            emailMessage.SendAndSaveCopy()
            Return True
        Catch ex As Exception
            ' Throw New System.ArgumentException(ex.Message, "Error")

            Return False
        End Try
    End Function

    Public Shared Function SendMailUsingSMTP(ByVal dr As DataRow, ByVal EmailAddress As String, ByVal link As String) As Boolean
        Try
            Dim MailUser As String = ConfigurationManager.AppSettings("emailsender").ToString()
            Dim MailPass As String = ConfigurationManager.AppSettings("password").ToString()
            Dim subject As String = "≈” ⁄«œ… ﬂ·„… «·„—Ê—"
            Dim MailText As String = "<div style='direction:rtl'>"
            MailText = MailText & "«·„ÊŸ› / ‹…" & dr("FirstName") & " " & dr("LastName") & "<br/>"
            MailText = MailText & "·≈” ⁄«œ… ﬂ·„… «·„—Ê— «·Œ«’… »«·»—Ìœ «·«·ﬂ —Ê‰Ì  " & EmailAddress & "<br/>"
            MailText = MailText & "Ì—ÃÏ «·÷€ÿ ⁄·Ï «·—«»ÿ «· «·Ì   " & "<br/>"
            MailText = MailText & "<div style='direction:ltr'>" & link & "</div><br/>"
            MailText = MailText & "Ê›Ì Õ«·… ⁄œ„ › Õ «·—«»ÿ Ì—ÃÏ ‰”Œ «·—«»ÿ Ê› ÕÂ ›Ì «·„ ’›Õ «·Œ«’ »ﬂ„   " & vbCrLf

            MailText = MailText & "<br/>" & "<br/>" & "<br/>" & "<br/>" & "<br/>"

            MailText = MailText & "›—Ìﬁ ‰Ÿ«„ «·Õ÷Ê— Ê«·«‰’—«›   "

            MailText = MailText & "</div>"



            Dim _mailmsg As MailMessage = New MailMessage()
            _mailmsg.IsBodyHtml = True
            _mailmsg.From = New MailAddress("attendancetrack-riyadh@moh.gov.sa")
            _mailmsg.[To].Add(dr("EMailAddress").ToString())
            _mailmsg.Subject = subject
            _mailmsg.Body = MailText
            Dim _smtp As SmtpClient = New SmtpClient()
            _smtp.Host = "mail.mastechnology.net"
            _smtp.Port = 25
            _smtp.EnableSsl = False
            Dim _network As NetworkCredential = New NetworkCredential("temp@mastechnology.net", "g@Z?Thk4fx2?")
            _smtp.Credentials = _network
            _smtp.Send(_mailmsg)
            Return True
        Catch ex As Exception
            '  Throw New System.ArgumentException(ex.Message, "Error")

            Return False
        End Try
    End Function
End Class
