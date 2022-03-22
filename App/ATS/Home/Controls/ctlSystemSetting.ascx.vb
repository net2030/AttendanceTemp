Imports System.DirectoryServices
Partial Class Home_Controls_ctlSystemSetting
    Inherits System.Web.UI.UserControl
    Dim Err As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Title = ResourceHelper.GetFromResource(Me.Page.Title)
        If (System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") Is Nothing Or System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") = "") OrElse System.Web.HttpContext.Current.User.IsInRole("SystemAdmin") Then
            If Not Me.IsPostBack Then
                Try
                    Me.HideAdPreferences()
                    txtSystemSettingPassword.Text = System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword")
                    txtSystemSettingPasswordConfirm.Text = System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword")
                    txtSMTPServer.Text = System.Configuration.ConfigurationManager.AppSettings("SmtpServer")
                    txtSMTPUsername.Text = System.Configuration.ConfigurationManager.AppSettings("SmtpServerUserName")
                    txtSMTPPassword.Text = System.Configuration.ConfigurationManager.AppSettings("SmtpServerPassword")
                    txtSMTPPortNumber.Text = System.Configuration.ConfigurationManager.AppSettings("SmtpPortnumber")
                    If Not System.Configuration.ConfigurationManager.AppSettings("SitePrefix") = "" Then
                        txtApplicationURL.Text = System.Configuration.ConfigurationManager.AppSettings("SitePrefix")
                    Else
                        txtApplicationURL.Text = "http://" & System.Web.HttpContext.Current.Server.MachineName & "/TimeLive/"
                    End If
                    txtConnectionString.Text = System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString


                    If UIUtilities.IsAspNetActiveDirectoryMembershipProvider = True Then
                        ActiveDirectoryAuthenticationRadioButton.Checked = True
                        DefaultAuthenticationRadioButton.Checked = False
                        Me.GetADConnectionSettings()
                    ElseIf UIUtilities.IsOpenLDAPMembershipProvider Then
                        ActiveDirectoryAuthenticationRadioButton.Checked = False
                        DefaultAuthenticationRadioButton.Checked = False
                        RDOpenLDAP.Checked = True
                        Me.GetLDAPSetting()
                    Else
                        DefaultAuthenticationRadioButton.Checked = True
                        ActiveDirectoryAuthenticationRadioButton.Checked = False
                    End If
                    'Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
                    'Dim ConSec As ConnectionStringsSection = config.GetSection("connectionStrings")
                    'If ConSec.SectionInformation.IsProtected Then
                    '    chkencrypt.Checked = True
                    'End If
                    Dim EnableSSl = UIUtilities.GetEnableSSL
                    If EnableSSl = False Then
                        chkEnableSSL.Checked = False
                    Else
                        chkEnableSSL.Checked = True
                    End If
                    '& " " & System.Configuration.ConfigurationManager.AppSettings("SitePrefix")

                Catch ex As Exception
                    Me.ShowErrorMessage(ex)
                End Try


            End If
        Else

            UIUtilities.RedirectToLoginPageAfterSystemSettings(txtSystemSettingPassword.Text)

        End If
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If txtSystemSettingPassword.Text <> "" Then
                UIUtilities.ModifyAppSettings("SystemSettingPassword", DBUtilities.EncryptPasswordInHash(txtSystemSettingPassword.Text))
            End If
            If Not System.Configuration.ConfigurationManager.AppSettings("SmtpServer") Is Nothing Then
                UIUtilities.ModifyAppSettings("SmtpServer", txtSMTPServer.Text)
            End If

            If Not System.Configuration.ConfigurationManager.AppSettings("SmtpServerUserName") Is Nothing Then
                UIUtilities.ModifyAppSettings("SmtpServerUserName", txtSMTPUsername.Text)
            End If

            If Not System.Configuration.ConfigurationManager.AppSettings("SmtpServerPassword") Is Nothing Then
                UIUtilities.ModifyAppSettings("SmtpServerPassword", txtSMTPPassword.Text)
            End If

            If Not System.Configuration.ConfigurationManager.AppSettings("SmtpPortnumber") Is Nothing Then
                UIUtilities.ModifyAppSettings("SmtpPortnumber", txtSMTPPortNumber.Text)
            End If

            If Not System.Configuration.ConfigurationManager.AppSettings("SitePrefix") Is Nothing Then
                UIUtilities.ModifyAppSettings("SitePrefix", txtApplicationURL.Text)
            End If

            If ActiveDirectoryAuthenticationRadioButton.Checked = True Then
                If Not System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString Is Nothing Then
                    UIUtilities.ModifyConnectionString("ATSConnectionString", txtConnectionString.Text)
                End If

                If Not System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString Is Nothing Then
                    UIUtilities.ModifyConnectionString("ATSConnectionString", txtConnectionString.Text)
                End If

                If Not System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString Is Nothing Then
                    UIUtilities.ModifyConnectionString("ADService", txtADConnectionString.Text)
                End If

                UIUtilities.ModifyAppSettings("IntegratedAuthentication", GetIntegratedAuthenticationValue)

                Dim DomainName As String = ADDomainNameTextBox.Text
                Dim Username As String = ADUsernameTextBox.Text
                Dim ADUsername As String = DomainName & "\" & Username
                Dim ADPassword As String = ADPasswordTextBox.Text
                Dim ConnectionProtection As String = Me.GetConnectionProtection

                UIUtilities.ChangeMembershipProviderForADAuthentication(ADUsername, ADPassword, ConnectionProtection)
                Me.TestAD(DomainName, ADUsername, ADPassword)
            ElseIf RDOpenLDAP.Checked = True Then
                If Not System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString Is Nothing Then
                    UIUtilities.ModifyConnectionString("ATSConnectionString", txtConnectionString.Text)
                End If

                If Not System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString Is Nothing Then
                    UIUtilities.ModifyConnectionString("ATSConnectionString", txtConnectionString.Text)
                End If
                If Not System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString Is Nothing Then
                    UIUtilities.ModifyConnectionString("ADService", GetADServiceForOpenLDAPAuthentication())
                End If
                UIUtilities.ChangeMembershipProviderForOpenLDAPAuthentication(txtLDAPUsername.Text, txtLDAPPassword.Text, "None")
                TestOpenLDAP(txtLDAPUsername.Text, txtLDAPPassword.Text)
            Else

                If Not System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString Is Nothing Then
                    UIUtilities.ModifyConnectionString("ATSConnectionString", txtConnectionString.Text)
                End If

                If Not System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString Is Nothing Then
                    UIUtilities.ModifyConnectionString("ATSConnectionString", txtConnectionString.Text)
                End If

                EncryptConnectionString()
                Me.EnableSSL()
                UIUtilities.ChangeMembershipProviderForDefaultAuthentication()
            End If

            Me.TestDB(txtConnectionString.Text)


            Me.RedirectOnUpdate(txtSystemSettingPassword.Text)

        Catch ex As Threading.ThreadAbortException
            Me.RedirectOnUpdate(txtSystemSettingPassword.Text)
        Catch ex As Exception
            Me.ShowErrorMessage(ex)
            Me.RedirectAfterUpdateOnError(ex)
        End Try


    End Sub
    Public Function GetADServiceForOpenLDAPAuthentication() As String
        Return "LDAP://" & txtLDAPHost.Text & ":" & txtLDAPPort.Text & "/" & txtLDAPBaseDN.Text
    End Function
    Public Sub EncryptConnectionString()
        'Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        'Dim ConSec As ConnectionStringsSection = config.GetSection("connectionStrings")
        'If chkencrypt.Checked = True Then
        '    If Not ConSec.SectionInformation.IsProtected Then
        '        ConSec.SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
        '        config.Save()
        '    End If
        'ElseIf chkencrypt.Checked = False Then
        '    If ConSec.SectionInformation.IsProtected Then
        '        ConSec.SectionInformation.UnprotectSection()
        '        config.Save()
        '    End If
        'End If
    End Sub
    Public Sub EnableSSL()
        'Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        'Dim ConSec As ConnectionStringsSection = config.GetSection("connectionStrings")
        ''If Not ConSec.C Then
        'UIUtilities.ModifyAppSettings("EnableSSL", chkEnableSSL.Checked)
        ''End If
    End Sub
    Public Sub RedirectOnUpdate(ByVal SystemSettingPassword As String)
        System.Web.HttpContext.Current.Session.Abandon()
        System.Web.Security.FormsAuthentication.SignOut()
        If Request.QueryString("ApplicationMode") = "Installed" Then
            UIUtilities.RedirectToAccountAddForInstalled()
        Else
            If (System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") Is Nothing Or System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") = "") OrElse System.Web.HttpContext.Current.User.IsInRole("SystemAdmin") Then
                UIUtilities.RedirectToLoginPage()

            Else
                UIUtilities.RedirectToLoginPageAfterSystemSettings(SystemSettingPassword)
            End If
        End If
    End Sub
    Public Sub TestAD(ByVal DomainName As String, ByVal ADUsername As String, ByVal ADPassword As String)
        Try
            If UIUtilities.IsAspNetActiveDirectoryMembershipProvider = True Then
                Dim DE As New DirectoryEntry(System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString, ADUsername, ADPassword)
                Dim Name As String
                Name = DE.Name
            End If
        Catch ex As Exception
            Throw New Exception("Active Directory Connection Failed: <br>" & ex.Message)
        End Try
    End Sub
    Public Sub TestOpenLDAP(ByVal LDAPUsername As String, ByVal LDAPPassword As String)
        Try
            Dim DE As New DirectoryEntry(System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString, LDAPUsername, LDAPPassword, AuthenticationTypes.None)
            DE.RefreshCache()
        Catch ex As Exception
            Throw New Exception("Open LDAP Connection Failed: <br>" & ex.Message)
        End Try
    End Sub
    Public Sub TestDB(ByVal ConnectionString As String)
        Try
            Dim conTest As New System.Data.SqlClient.SqlConnection(ConnectionString)
            conTest.Open()
            conTest.Close()
        Catch ex As Exception
            Throw New Exception("Database Connection Failed: <br>" & ex.Message)
        End Try
    End Sub

    Protected Sub DefaultAuthenticationRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If (System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") Is Nothing Or System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") = "") OrElse System.Web.HttpContext.Current.User.IsInRole("SystemAdmin") Then
            Me.HideAdPreferences()
        Else
            UiUtilities.RedirectToLoginPageAfterSystemSettings(txtSystemSettingPassword.Text)
        End If
    End Sub

    Protected Sub ActiveDirectoryAuthenticationRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.GetADConnectionSettings()
    End Sub
    Public Sub GetADConnectionSettings()
        Try
            If (System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") Is Nothing Or System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") = "") OrElse System.Web.HttpContext.Current.User.IsInRole("SystemAdmin") Then


                If ActiveDirectoryAuthenticationRadioButton.Checked = True And Not ATS.BO.LDAPUtilitiesBLL.IsOpenLDAPMembershipProvider() Then
                    txtADConnectionString.Text = System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString
                    txtConnectionString.Text = System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString


                    Dim values As String
                    values = UIUtilities.GetADConnectionUsername

                    Dim sites As String() = Nothing

                    Dim sep(2) As Char
                    sep(0) = "\"
                    If Not values Is Nothing Then
                        sites = values.Split(sep)
                    End If
                    If Not sites Is Nothing Then
                        If Not sites(0) Is Nothing Then
                            ADDomainNameTextBox.Text = sites(0)
                        End If
                        If Not sites(1) Is Nothing Then
                            ADUsernameTextBox.Text = sites(1)
                        End If
                    End If
                    ADPasswordTextBox.Text = UIUtilities.GetADConnectionPassword

                    Dim ConnectionProtecion As String = UIUtilities.GetADConnectionProtection
                    If ConnectionProtecion = "Secure" Then
                        chkSecured.Checked = True
                    End If
                    Dim IntegratedAuthentication As String = UIUtilities.GetIntegratedAuthentication
                    If IntegratedAuthentication = "Yes" Then
                        chkIntegratedAuthentication.Checked = True
                    End If
                    Dim EnableSSl = UIUtilities.GetEnableSSL
                    If EnableSSl = False Then
                        chkEnableSSL.Checked = False
                    Else
                        chkEnableSSL.Checked = True
                    End If
                    Dim LDAP As New ATS.BO.LDAPUtilitiesBLL
                    LDAP.IsAspNetActiveDirectoryMembershipProvider()
                End If
            Else
                UIUtilities.RedirectToLoginPageAfterSystemSettings(txtSystemSettingPassword.Text)
            End If
        Catch ex As Exception
            Me.ShowErrorMessage(ex)
        End Try

    End Sub
    Public Sub HideAdPreferences()
        If (System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") Is Nothing Or System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") = "") OrElse System.Web.HttpContext.Current.User.IsInRole("SystemAdmin") Then
            If DefaultAuthenticationRadioButton.Checked = True Then
                txtConnectionString.Text = System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString
            End If
        Else
            UiUtilities.RedirectToLoginPageAfterSystemSettings(txtSystemSettingPassword.Text)
        End If
    End Sub

    Public Sub ShowErrorMessage(ByVal Ex As Exception)
        Me.lblErrorMessage.Text = "<br>" & Ex.Message
        Me.lblErrorMessage.Visible = True
    End Sub
    Public Sub RedirectAfterUpdateOnError(ByVal Ex As Exception)
        UIUtilities.RedirectToSystemSetting()
    End Sub
    Public Function GetConnectionProtection() As String
        If chkSecured.Checked = True Then
            Return "Secure"
        End If
        Return "None"
    End Function
    Public Function GetIntegratedAuthenticationValue() As String
        If chkIntegratedAuthentication.Checked = True Then
            Return "Yes"
        End If
        Return "No"
    End Function
    Public Sub GetLDAPSetting()
        Try
            If (System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") Is Nothing Or System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") = "") OrElse System.Web.HttpContext.Current.User.IsInRole("SystemAdmin") Then
                If RDOpenLDAP.Checked = True And ATS.BO.LDAPUtilitiesBLL.IsOpenLDAPMembershipProvider() Then
                    txtLDAPHost.Text = UIUtilities.GetLDAPHost
                    txtConnectionString.Text = System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString
                    txtLDAPUsername.Text = UIUtilities.GetADConnectionUsername
                    txtLDAPPassword.Text = UIUtilities.GetADConnectionPassword
                    txtLDAPBaseDN.Text = UIUtilities.GetLDAPBaseDN
                    txtLDAPPort.Text = UIUtilities.GetLDAPPort
                    TestOpenLDAP(txtLDAPUsername.Text, txtLDAPPassword.Text)
                End If
            Else
                UIUtilities.RedirectToLoginPageAfterSystemSettings(txtSystemSettingPassword.Text)
            End If
        Catch ex As Exception
            Me.ShowErrorMessage(ex)
        End Try
    End Sub
    Protected Sub RDOpenLDAP_CheckedChanged(sender As Object, e As System.EventArgs) Handles RDOpenLDAP.CheckedChanged
        GetLDAPSetting()
    End Sub
End Class
