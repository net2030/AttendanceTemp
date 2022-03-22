Imports Telerik.Web.UI

Partial Class Employee_AttendanceByEmployee
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If CInt(Session("AccountRoleId")) = 2 Then
                Dim Step2Tab As RadTab = RadTabStrip1.Controls(1)
                Step2Tab.Visible = False

            End If
           
        End If
    End Sub
End Class
