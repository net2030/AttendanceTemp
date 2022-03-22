Imports System.Security.Principal
Imports System.Web.Security
Partial Class Mobile_Controls_ctlLogin
    Inherits System.Web.UI.UserControl
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
        BLL.LoggedIn(Username, Password, True)

        If Not Request.QueryString("ReturnPage") Is Nothing Then
            Login1.DestinationPageUrl = Me.Request.QueryString("ReturnPage")
        End If

    End Sub
End Class