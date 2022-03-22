
Partial Class Authenticate_DoLogout
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Abandon()
        UIUtilities.RedirectToLoginPage()
    End Sub
End Class
