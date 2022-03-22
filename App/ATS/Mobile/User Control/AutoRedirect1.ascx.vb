Imports System.Globalization

Partial Class Mobile_User_Control_AutoRedirect1
    Inherits System.Web.UI.UserControl

    Public LoginDate As String
    Public ExpressDate As String
    Public RURL As String = "Default.aspx"


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Check session is expire or timeout.
        If Session("username") Is Nothing Then
            Response.Redirect(RURL)
        End If

        ' Get user login time or last activity time.
        Dim [date] As DateTime = DateTime.Now
        LoginDate = [date].ToString("u", DateTimeFormatInfo.InvariantInfo).Replace("Z", "")
        Dim sessionTimeout As Integer = Session.Timeout
        Dim dateExpress As DateTime = [date].AddMinutes(sessionTimeout)
        ExpressDate = dateExpress.ToString("u", DateTimeFormatInfo.InvariantInfo).Replace("Z", "")
        RURL = GetSessionExpiredURL(Me.Page, True)
    End Sub

    Public Shared Function GetSessionExpiredURL(Optional ByVal CurrentPage As System.Web.UI.Page = Nothing, Optional ByVal IsTimeLiveMobileLogin As Boolean = False) As String
        Dim loginUrl As String = IIf(IsTimeLiveMobileLogin, "../Mobile/Default.aspx", "../Authenticate/SessionExpired.aspx")
        If Not CurrentPage Is Nothing Then
            Dim currentPageName As String = CurrentPage.ResolveClientUrl(HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.RawUrl))
            loginUrl = loginUrl + "?ReturnPage=" + currentPageName
        End If
        Return loginUrl
    End Function
End Class
