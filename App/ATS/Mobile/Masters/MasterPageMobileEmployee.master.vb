Imports Telerik.Web.UI
Imports DevExpress.Web.ASPxPanel

Partial Class Masters_MasterPageMobileEmployee
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'If AuthenticateBLL.IsNewSession() Then
        '    AuthenticateBLL.DoLogout(Me.Page, True)
        'End If

        'LocaleUtilitiesBLL.SetPageCultureSetting(Me.Page)

    End Sub

    Public Function IsMenuPage(Id As Integer) As Boolean
        If Id = 4 Or Id = 13 Or Id = 100 Or Id = 167 Or Id = 174 Then
            Return True
        End If
        Return False
    End Function

    Protected Sub RadMenu1_ItemClick(sender As Object, e As RadMenuEventArgs)
        If e.Item.Text.Trim = " ”ÃÌ· Œ—ÊÃ" Then
            System.Web.Security.FormsAuthentication.SignOut()
            System.Web.HttpContext.Current.Session.Abandon()
            UIUtilities.RedirectToLoginPage(, 1)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '   CheckSessionTimeout()
        ' Session.Add("CultureInfo", "en-US")
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        'Try
        '    Me.Page.Header.Controls.Add(New LiteralControl(String.Format("<meta http-equiv='refresh' content='{0};url={1}'>", (System.Web.HttpContext.Current.Session.Timeout * 60), UIUtilities.GetSessionExpiredURL(Me.Page))))

        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub CheckSessionTimeout()

        Dim msgSession As String = "Warning: Within next 3 minutes, if you do not do anything, our system will redirect to the login page. Please save changed data."
        'time to remind, 3 minutes before session ends
        Dim int_MilliSecondsTimeReminder As Integer = (Me.Session.Timeout * 60000) - 3 * 60000
        'time to redirect, 5 milliseconds before session ends
        Dim int_MilliSecondsTimeOut As Integer = (Me.Session.Timeout * 60000) - 5

        Dim str_Script As String = "var myTimeReminder, myTimeOut; "

        str_Script = str_Script + "clearTimeout(myTimeReminder);"
        str_Script = str_Script + " clearTimeout(myTimeOut); "
        str_Script = str_Script + "var sessionTimeReminder = "
        str_Script = str_Script + int_MilliSecondsTimeReminder.ToString() + "; "
        str_Script = str_Script + "var sessionTimeout = " + int_MilliSecondsTimeOut.ToString() + ";"
        str_Script = str_Script + "function doReminder(){ alert('" + msgSession + "'); }"
        str_Script = str_Script + "function doRedirect(){ window.location.href=/KB/session/login.aspx }"
        str_Script = str_Script + " myTimeReminder=setTimeout('doReminder()', sessionTimeReminder); "
        str_Script = str_Script + "  myTimeOut=setTimeout('doRedirect()', sessionTimeout); "

     ScriptManager.RegisterClientScriptBlock(me.Page, me.GetType(), "CheckSessionOut", str_Script, true)
    End Sub

End Class

