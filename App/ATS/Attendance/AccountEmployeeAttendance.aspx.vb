Imports Telerik.Web.UI

Partial Class Employee_frmAccountEmployeeAttendance
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
          
            If Not Request.QueryString("EmployeeName") Is Nothing Then
                Dim Step2Tab As RadTab = RadTabStrip1.Controls(0)
                If IsNothing(Step2Tab) Then
                    Step2Tab = RadTabStrip1.FindTabByText("My Attendance")

                End If
                Step2Tab.Text = Request.QueryString("EmployeeName")
            End If

            If CInt(Session("AccountRoleId")) = 2 Then
                'Dim Step2Tab As RadTab = RadTabStrip1.Controls(1)
                'Step2Tab.Visible = False
            End If


            'If Not Request.QueryString("EmployeeName") Is Nothing Then

            '    Dim EmployeeName As String = Request.QueryString("EmployeeName")

            '    ctlAccountEmployeeAttendance.Visible = True
            '    ctlAccountEmployeeAttendanceByDepartment.Visible = False
            'End If
       
        End If
    End Sub
End Class
