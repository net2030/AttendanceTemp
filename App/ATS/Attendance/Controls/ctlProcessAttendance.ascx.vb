Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Attendance_Controls_ctlProcessAttendance
    Inherits System.Web.UI.UserControl




    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            
         
            Startdate.SelectedDate = Date.Now
            Enddate.SelectedDate = Date.Now
        Else
            Me.IsPost.Value = 1
        End If

    End Sub




    Protected Sub ProcessAttendance()
        Dim moAtt As New ATS.BO.Framework.BOAttendanceLog
        Try
            Dim StartDate As Date = Me.Startdate.SelectedDate
            Dim EndDate As Date
            Dim DepartmentID As Integer
            Dim EmployeeID As Integer
            Dim op As Integer

            If rdAll.Checked Then
                op = 1
                EndDate = StartDate
            ElseIf rdByDepartment.Checked Then
                DepartmentID = Me.ddlDepartment.SelectedValue
                op = 2
            ElseIf rdByEmployee.Checked Then
                ' DepartmentID = Me.ddlDepartment.SelectedValue
                EmployeeID = Me.ddlEmployees.SelectedValue
                EndDate = Me.Enddate.SelectedDate
                op = 3
            End If

            Dim Results As Boolean = moAtt.UpdateEmployeeAttendance(op, Startdate, Enddate, EmployeeID)

            If Results Then
                If EmployeeID <> 0 Then
                    Response.Redirect("AccountEmployeeAttendance.aspx?EmployeeID=" & EmployeeID & "&EmployeeName=" & ddlEmployees.SelectedItem.Text)
                Else
                    Label2.ForeColor = Color.Green
                    Label2.Visible = True
                    Label2.Text = "ÊãÊ ÇáãÚÇáÌÉ"
                    Response.Redirect("AccountEmployeeAttendance.aspx")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Canceling()
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim moEmployee As New ATS.BO.Framework.BOEmployee
        Try
            Dim dt As DataTable = New DataTable
            Dim EmployeeCode As String = txtEmployeeCode.Text
            dt = moEmployee.GetEmployeesByBadgeDataset(0, EmployeeCode, 1, 1).Tables(0)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                ddlEmployees.DataSource = dt
                ddlEmployees.DataBind()
                ddlEmployees.SelectedValue = dt.Rows(0)("EmployeeId")

            Else
                ddlEmployees.SelectedValue = 0
            End If
        Catch ex As Exception

        End Try


    End Sub
    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("ispost=true;")
        sb.Append("Toggle();")
        sb.Append("};")

        Return sb.ToString()

    End Function

End Class
