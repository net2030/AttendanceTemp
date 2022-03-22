Imports System.Security.Principal
Imports System.Web.Security
Imports System.Data.SqlClient
Imports System.Management
Imports System.Net
Imports System.Net.Sockets
Imports ATS.BO.Framework

Partial Class Auth_Controls_ctlLogin
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EncryptConnectionString()
        'DataProtectionConfigurationProvider
        'RsaProtectedConfigurationProvider

        Me.SetImageUrl()
        CType(Me.Login1.FindControl("VersionLabel"), Label).Text = "Version " & SystemUtilitiesBLL.GetApplicationVersion.ToString
        If UIUtilities.IsSignUpOnLogin Then
            CType(Me.Login1.FindControl("Button2"), Button).Visible = True
        Else
            CType(Me.Login1.FindControl("Button2"), Button).Visible = False
        End If


    End Sub


    Protected Sub Login1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate
        Dim Username As String = CType(Me.Login1.FindControl("UserName"), TextBox).Text
        Dim Password As String = CType(Me.Login1.FindControl("Password"), TextBox).Text
        Dim BLL As New AuthenticateBLL

        Try
            If BLL.AuthenticateLogin(Username, Password) = True Then
                e.Authenticated = True
            Else
                e.Authenticated = False
            End If
        Catch ex As Exception
            CType(Me.Login1.FindControl("ErrorText"), Label).Visible = True
            CType(Me.Login1.FindControl("ErrorText"), Label).Text = "Configuration Error: " & ex.Message
        End Try

    End Sub

    Protected Sub Login1_LoggedIn(ByVal sender As Object, ByVal e As System.EventArgs) Handles Login1.LoggedIn
        Dim Username As String = CType(Me.Login1.FindControl("UserName"), TextBox).Text
        Dim Password As String = CType(Me.Login1.FindControl("Password"), TextBox).Text
        Dim BLL As New AuthenticateBLL

        BLL.LoggedIn(Username, Password)

        If New BOEmployee().CheckIsthereNotificationsNotReceipt(CInt(Session("AccountEmployeeId"))) Then
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg("·œÌﬂ «‰–«— €Ì«» Ì—ÃÏ „—«Ã⁄… «·„Ê«—œ «·»‘—Ì…"), True)

            Login1.DestinationPageUrl = "~/Employee/NoPermission1.aspx"
            Return
        End If

        If DBUtilities.IsTimeLiveMobileLogin Then
            Login1.DestinationPageUrl = UIUtilities.RedirectToMobileHomePage()
        Else
            Login1.DestinationPageUrl = UIUtilities.RedirectToHomePage()
        End If



        ' The orginal Work
        '************************************
        'If (Session("AccountRoleId") = 1 Or Session("AccountRoleId") = 11 Or Session("AccountRoleId") = 12) And Session("username").ToString.ToLower <> "pro_watch" Then
        '    Dim worker As New BackgroundWorker()
        '    AddHandler worker.DoWork, AddressOf worker_DoWork
        '    worker.RunWorker({"Test"})

        '    ' It needs Session Mode is "InProc"
        '    ' to keep the Background Worker working.
        '    Session("worker") = worker

        'End If
        '******************************************


    End Sub

    Private Sub worker_DoWork(ByRef progress As Integer, ByRef result As Object, ByVal ParamArray arguments As Object())
        CheckDevicesConnectivity()
    End Sub

    Protected Sub CheckDevicesConnectivity()


        Dim dt As DataTable = GetDevices()
        If dt.Rows.Count > 20 Then
            Response.Redirect("http://mastechnology.net/")
        End If


        Try

            For Each row1 In dt.Rows
                If PortScan((row1("IPAddress")), 10001) = False Then
                    SaveDeviceConnectionLose(row1("Name").ToString, row1("IPAddress").ToString)
                End If
            Next

        Catch ex As Exception

        End Try


    End Sub

    Protected Function GetDevices() As DataTable

        Dim dt As DataTable = New DataTable
        Dim ds As DataSet = New DataSet
        Try
            Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("L1ConnectionString").ConnectionString
            Dim query As String = "SELECT * FROM vDevices "

            Dim da As New SqlDataAdapter(query, constr)
            Dim table As New DataTable()

            da.Fill(ds)
            dt = ds.Tables(0)

        Catch ex As Exception

        End Try
        Return dt
    End Function

    'Protected Function IsConnected(ByVal IP As String) As Boolean
    '    Dim connected As Boolean


    '    Try

    '        Dim timout As Integer = 5000
    '        Dim port As Integer = 10001
    '        Dim localAddr As IPAddress = IPAddress.Parse(IP)

    '        Dim remoteEndPoint As New IPEndPoint(localAddr, port)
    '        Dim NetworkClient As TcpClient = TimeOutSocket.Connect(remoteEndPoint, timout)

    '        If TimeOutSocket.IsConnectionSuccessful Then
    '            connected = True
    '        Else
    '            connected = False
    '        End If
    '    Catch ex As Exception

    '    End Try
    '    Return connected
    'End Function

    Public Function PortScan(ByVal IP As String, ByVal port As Integer) As Boolean
        Dim connected As Boolean
        Try

            If ScanPort(System.Net.IPAddress.Parse(IP), port) = True Then
                connected = True
            Else
                connected = False
            End If

        Catch

        End Try
        Return connected
    End Function

    ReadOnly Property ScanPort(ByVal IP As System.Net.IPAddress, ByVal Port As Integer) As Boolean
        Get
            Dim TCP As New System.Net.Sockets.TcpClient
            Try
                TCP.Connect(IP, Port)
            Catch
            End Try
            If TCP.Connected = True Then
                ScanPort = True
                TCP.Close()
            Else
                ScanPort = False
                TCP.Close()
            End If

        End Get
    End Property

    Protected Sub SaveDeviceConnectionLose(ByVal DeviceName As String, ByVal DeviceIP As String)

        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "Managements.SpDeviceFault_Insert"
        Try
            Dim con As New SqlConnection(constr)
            Dim com As New SqlCommand(query, con)
            com.CommandType = CommandType.StoredProcedure

            com.Parameters.Add("@DeviceName", SqlDbType.NVarChar, 100).Value = DeviceName
            com.Parameters.Add("@DeviceIP", SqlDbType.NChar, 20).Value = DeviceIP


            con.Open()
            com.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub SaveAccountDetails()
        Dim table As DataTable = GetAccountDetails()
        If Not IsNothing(table) AndAlso table.Rows.Count > 0 Then
            System.Web.HttpContext.Current.Session.Add("Readers", table.Rows(0)("LicenseId"))
            System.Web.HttpContext.Current.Session.Add("ActivationLicenseKey", table.Rows(0)("ActivationLicenseKey"))
            System.Web.HttpContext.Current.Session.Add("MachineId", table.Rows(0)("MachineId"))
        End If

    End Sub
    Public Shared Function GetAccountDetails() As DataTable

        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString

        Dim table As New DataTable()


        Try
            Dim oCon As SqlConnection = New SqlConnection(constr)
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("SELECT * FROM [MASTMS].[dbo].[Account] WHERE AccountID=1")
                Using cmd
                    'cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon


                    Dim Adapter As New SqlDataAdapter
                    ds = New DataSet
                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds, "Account")
                    table = ds.Tables(0)
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try

        Return table
    End Function

    Public Sub SetImageUrl()
        If System.Configuration.ConfigurationManager.AppSettings("SHOW_HELP_ICON") = "No" Then
            CType(Me.Login1.FindControl("Literal1"), Literal).Visible = False
        End If
        If UIUtilities.GetApplicationMode = "Installed" Then
            If Membership.Provider.Name = "AspNetActiveDirectoryMembershipProvider" Then
                CType(Me.Login1.FindControl("Literal5"), Literal).Visible = False
            End If
            ' CType(Me.Login1.FindControl("TimeLiveLogin"), Literal).Text = " ”ÃÌ· «·œŒÊ·"

            Me.Page.Title = ResourceHelper.GetFromResource(Me.Page.Title)
            If LocaleUtilitiesBLL.IsLogoFileExist = True Then
                CType(Me.Login1.FindControl("imgCompanyOwnLogo"), System.Web.UI.WebControls.Image).ImageUrl = ("~/Uploads/" & DBUtilities.GetInstalledAccountId & "/Logo/CompanyLogo.gif")
            Else
                If System.Configuration.ConfigurationManager.AppSettings("BugTracking") = "Yes" Then
                    CType(Me.Login1.FindControl("imgCompanyOwnLogo"), System.Web.UI.WebControls.Image).ImageUrl = "~/Images/MAS.png"
                Else
                    CType(Me.Login1.FindControl("imgCompanyOwnLogo"), System.Web.UI.WebControls.Image).ImageUrl = "~/Images/MAS.png"
                End If
            End If
        Else
            If System.Configuration.ConfigurationManager.AppSettings("BugTracking") = "Yes" Then
                CType(Me.Login1.FindControl("imgCompanyOwnLogo"), System.Web.UI.WebControls.Image).ImageUrl = "~/Images/MAS.png"
            Else
                CType(Me.Login1.FindControl("imgCompanyOwnLogo"), System.Web.UI.WebControls.Image).ImageUrl = "~/Images/MAS.png"
            End If
        End If
    End Sub

    Protected Sub Button2_Click()
        Response.Redirect(SignUpURL, False)
    End Sub
    Public Shared Function SignUpURL() As String
        Return "~/Home/SignUp.aspx"
    End Function

    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function


#Region " Protect Connection String "

    Public Shared Sub EncryptConnectionString(Optional protect As Boolean = True)
        Call UnprotectConnectionString()
        Call ProtectConnectionString()
    End Sub

    Public Shared Sub ProtectConnectionString()
        ToggleConnectionStringProtection(System.Web.HttpContext.Current.Server.MapPath("~/web.config"), True)
    End Sub
    Public Shared Sub UnprotectConnectionString()
        ToggleConnectionStringProtection(System.Web.HttpContext.Current.Server.MapPath("~/web.config"), False)
    End Sub
    Private Shared Sub ToggleConnectionStringProtection(pathName As String, protect As Boolean)
        ' Define the DPAPI provider name.
        Dim strProvider As String = "DataProtectionConfigurationProvider"
        Dim oConfiguration As System.Configuration.Configuration = Nothing
        Dim oSection As System.Configuration.ConnectionStringsSection = Nothing

        Try
            ' Open the configuration file and retrieve the connectionStrings section.

            ' For Web!
            ' oConfiguration = System.Web.Configuration.
            '                  WebConfigurationManager.OpenWebConfiguration("~");

            ' For Windows!
            ' Takes the executable file name without the config extension.
            oConfiguration = System.Configuration.ConfigurationManager.OpenExeConfiguration(pathName)

            If oConfiguration IsNot Nothing Then
                Dim blnChanged As Boolean = False

                oSection = TryCast(oConfiguration.GetSection("connectionStrings"), System.Configuration.ConnectionStringsSection)

                If oSection IsNot Nothing Then
                    If (Not (oSection.ElementInformation.IsLocked)) AndAlso (Not (oSection.SectionInformation.IsLocked)) Then
                        If protect Then
                            If Not (oSection.SectionInformation.IsProtected) Then
                                blnChanged = True

                                ' Encrypt the section.
                                oSection.SectionInformation.ProtectSection(strProvider)
                            End If
                        Else
                            If oSection.SectionInformation.IsProtected Then
                                blnChanged = True

                                ' Remove encryption.
                                oSection.SectionInformation.UnprotectSection()
                            End If
                        End If
                    End If

                    If blnChanged Then
                        ' Indicates whether the associated configuration section o
                        ' will be saved even if it has not been modified.
                        oSection.SectionInformation.ForceSave = True

                        ' Save the current configuration.
                        oConfiguration.Save()
                    End If
                End If
            End If
        Catch ex As System.Exception
            Throw (ex)
        Finally
        End Try
    End Sub
#End Region
End Class
