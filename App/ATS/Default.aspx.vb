Imports System.IO.File
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ips As String

        ips = System.Web.HttpContext.Current.Request.ServerVariables("LOCAL_ADDR")

        Dim LDAP As New ATS.BO.LDAPUtilitiesBLL
        If Not Me.IsPostBack Then
            ' Me.RunUpdatedScript()

            If isMobileBrowser() Then
                Response.Redirect(UIUtilities.RedirectToMobileHomePage())
            End If

            Me.CheckInstalled()
            If Not Me.Request.QueryString("AutoLogin") Is Nothing Then
                LoggingBLL.WriteToLog("Default.aspx: AutoLogin")
                Dim BLL As New AuthenticateBLL
                BLL.AuthenticateLogin(Me.Request.QueryString("Username"), Me.Request.QueryString("Password"), False)
                BLL.LoggedIn(Me.Request.QueryString("Username"), Me.Request.QueryString("Password"))
            ElseIf UIUtilities.GetIntegratedAuthentication = "Yes" And LDAP.IsAspNetActiveDirectoryMembershipProvider = True Then
                Dim Username As String

                Dim UsernameArray() As String = Request.ServerVariables("LOGON_USER").Split("\")
                If UsernameArray.Length < 2 Then
                    Dim Login As Login
                    Login = CType(Me.CtlLogin1.FindControl("Login1"), Login)
                    CType(Login.FindControl("Username"), TextBox).Text = Me.Request.QueryString("Username")
                    CType(Login.FindControl("Password"), TextBox).Attributes.Add("value", Me.Request.QueryString("Password"))
                Else
                    Username = UsernameArray(1)
                    AuthenticateLogin(Username)
                End If
            Else
                Dim Login As Login
                Login = CType(Me.CtlLogin1.FindControl("Login1"), Login)
                CType(Login.FindControl("Username"), TextBox).Text = Me.Request.QueryString("Username")
                CType(Login.FindControl("Password"), TextBox).Attributes.Add("value", Me.Request.QueryString("Password"))
            End If
        End If

    End Sub
    Public Sub RunUpdatedScript()
        If Exists(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH)) Then
            DBUtilities.CreateDefaultDatabase()
            Response.Redirect("~\Home\ExecuteScript.aspx")
            LoggingBLL.WriteToLog("RunUpdatedScript")
        End If
    End Sub

    Public Sub CheckInstalled()
        Dim IsRedirectedFromSystemSettings As Boolean

        If System.Configuration.ConfigurationManager.AppSettings("ApplicationMode") = "Installed" Then
            ' Dim dtAccount As TimeLiveDataSet.AccountDataTable
            'dtAccount = New AccountBLL().GetAccounts()
            'If dtAccount.Rows.Count = 0 Then
            '    If System.Configuration.ConfigurationManager.AppSettings("SHOW_LOGIN_WITH_ACCOUNT_PAGE") = "Yes" Then
            '        Exit Sub
            '    Else
            '        UIUtilities.RedirectToAccountAddForInstalled()
            '    End If
            If Me.Request.QueryString("Username") = "systemadmin" And IsRedirectedFromSystemSettings = False Then
                IsRedirectedFromSystemSettings = True
                LoggingBLL.WriteToLog("Default.aspx: CheckInstalled: IsRedirectedFromSystemSettings = True")
            ElseIf Me.Request.QueryString("Username") <> "systemadmin" And System.Web.HttpContext.Current.User.Identity.Name = "systemadmin" Then
                LoggingBLL.WriteToLog("Default.aspx: Siging out and session adandon.")
                System.Web.HttpContext.Current.Session.Abandon()
                System.Web.Security.FormsAuthentication.SignOut()
            End If
        End If
    End Sub
    Public Sub AuthenticateLogin(Username As String)
        LoggingBLL.WriteToLog("Default.aspx: AuthenticateLogin")
        Dim BLL As New AuthenticateBLL
        If BLL.AuthenticateLogin(Username, "", False) = True Then
            BLL.LoggedIn(Username, "")
            If DBUtilities.IsTimeLiveMobileLogin Then
                Response.Redirect(UIUtilities.RedirectToMobileHomePage())
            Else
                Response.Redirect(UIUtilities.RedirectToHomePage())
            End If
        Else
            Dim Login As Login
            Login = CType(Me.CtlLogin1.FindControl("Login1"), Login)
            CType(Login.FindControl("ErrorText"), Label).Visible = True
            CType(Login.FindControl("ErrorText"), Label).Font.Size = 12
            CType(Login.FindControl("ErrorText"), Label).Text = "Your login attempt was not successful. Please try again."
        End If
    End Sub


#Region "Check Mobile Login"

    Public Shared Function isMobileBrowser() As Boolean
        'GETS THE CURRENT USER CONTEXT
        Dim context As HttpContext = HttpContext.Current

        'FIRST TRY BUILT IN ASP.NT CHECK
        If context.Request.Browser.IsMobileDevice Then
            Return True
        End If
        'THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
        If context.Request.ServerVariables("HTTP_X_WAP_PROFILE") IsNot Nothing Then
            Return True
        End If
        'THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
        If context.Request.ServerVariables("HTTP_ACCEPT") IsNot Nothing AndAlso context.Request.ServerVariables("HTTP_ACCEPT").ToLower().Contains("wap") Then
            Return True
        End If
        'AND FINALLY CHECK THE HTTP_USER_AGENT 
        'HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
        If context.Request.ServerVariables("HTTP_USER_AGENT") IsNot Nothing Then
            'Create a list of all mobile types
            Dim mobiles() As String = {"midp", "j2me", "avant", "docomo", "novarra", "palmos", _
            "palmsource", "240x320", "opwv", "chtml", "pda", "windows ce", _
            "mmp/", "blackberry", "mib/", "symbian", "wireless", "nokia", _
            "hand", "mobi", "phone", "cdm", "up.b", "audio", _
            "SIE-", "SEC-", "samsung", "HTC", "mot-", "mitsu", _
            "sagem", "sony", "alcatel", "lg", "eric", "vx", _
            "NEC", "philips", "mmm", "xx", "panasonic", "sharp", _
            "wap", "sch", "rover", "pocket", "benq", "java", _
            "pt", "pg", "vox", "amoi", "bird", "compal", _
            "kg", "voda", "sany", "kdd", "dbt", "sendo", _
            "sgh", "gradi", "jb", "dddi", "moto", "iphone"}

            'Loop through each item in the list created above 
            'and check if the header contains that text
            For Each s As String In mobiles
                If context.Request.ServerVariables("HTTP_USER_AGENT").ToLower().Contains(s.ToLower()) Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

#End Region
End Class
