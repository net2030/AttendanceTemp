
Partial Class Employee_Controls_ctlLoginStatus
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub LoginStatus1_LoggingOut(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LoginCancelEventArgs) Handles S.LoggingOut
        Dim IsTimeLiveMobileLogin As Boolean = DBUtilities.IsTimeLiveMobileLogin()
        System.Web.Security.FormsAuthentication.SignOut()
        System.Web.HttpContext.Current.Session.Abandon()
        UIUtilities.RedirectToLoginPage(, IsTimeLiveMobileLogin)
    End Sub
End Class
