
Partial Class Employee_NoPermission1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Session.Abandon()
    End Sub
End Class
