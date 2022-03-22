
Partial Class Employee_EmployeeProfile
    Inherits BasePage



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("AccountRoleId") <> "2" Then
                Me.ctlAuthorization1.Visible = True
            End If
        End If
    End Sub
End Class
