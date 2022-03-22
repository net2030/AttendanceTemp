
Partial Class Authenticate_SessionExpired
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoginURL.HRef = IIf(Me.Request.QueryString("ReturnPage") Is Nothing, "../Default.aspx", "../Default.aspx?ReturnPage=" & HttpUtility.UrlEncode(Me.Request.QueryString("ReturnPage")))
        AuthenticateBLL.DoLogoutForSessionExpired()
    End Sub
End Class
