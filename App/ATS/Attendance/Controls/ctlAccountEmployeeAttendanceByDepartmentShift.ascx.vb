Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class ctlAccountEmployeeAttendanceByDepartmentShift
    Inherits System.Web.UI.UserControl

    Private moMachineLog As New BOMachineLog

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            addManual.Visible = True
            processAttendance.Visible = True

            Me.Startdate.SelectedDate = Now.Date
            Me.GrdDepartmentAttendance0.Visible = True
            btnDevices.Visible = True


        End If

    End Sub


    Protected Sub Show_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click

        Try
            Me.dsDepartmentAttendance.SelectParameters.Item("DepartmentID").DefaultValue = ctlDepartmentTree1.SelectedValue
            Me.dsDepartmentAttendance.SelectParameters.Item("AttendanceDate").DefaultValue = Me.Startdate.SelectedDate
            Me.dsDepartmentAttendance.SelectParameters.Item("Employer").DefaultValue = Me.ddlEmployer.SelectedValue
            Me.dsDepartmentAttendance.SelectParameters.Item("ContractType").DefaultValue = Me.ddlContractType.SelectedValue

            Me.GrdDepartmentAttendance0.DataBind()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub GrdDepartmentAttendance0_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GrdDepartmentAttendance0.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LogId As Integer = DirectCast(Me.GrdDepartmentAttendance0.DataKeys(e.Row.RowIndex)("LogId"), Integer)
            Dim grdViewEmployeeLogs As GridView = DirectCast(e.Row.FindControl("grdViewEmployeeLogs"), GridView)
            grdViewEmployeeLogs.DataSource = moMachineLog.GetLogByAttendanceLogIdDataset(LogId, 1, 50)
            grdViewEmployeeLogs.DataBind()
        End If
    End Sub

    'Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
    '    For Each row1 As GridViewRow In GrdDepartmentAttendance0.Rows

    '        'Dim StatusId As Integer = CType(row1.FindControl("ddlAttendanceStatus"), DropDownList).SelectedValue
    '        Dim LogId As Integer = Me.GrdDepartmentAttendance0.DataKeys(row1.RowIndex)("LogId")
    '        Dim comments As String = CType(row1.FindControl("Comments"), TextBox).Text
    '        Dim UserId = CInt(Session("AccountEmployeeId"))
    '        If comments <> "" Then
    '            AttendanceDAL.UpdateAttendanceStatus(LogId, 0, comments, UserId)
    '        End If

    '    Next
    '    GrdDepartmentAttendance0.DataBind()



    'End Sub

    Protected Sub addManual_Click(sender As Object, e As EventArgs) Handles addManual.Click
        Response.Redirect("ManualLog.aspx")
    End Sub

    Protected Sub processAttendance_Click(sender As Object, e As EventArgs) Handles processAttendance.Click
        Response.Redirect("ProcessAttendance.aspx")
    End Sub


    Protected Sub btnDevices_Click(sender As Object, e As EventArgs) Handles btnDevices.Click
        Response.Redirect("~/AccountAdmin/DevicesList.aspx")

    End Sub


    Protected Sub grdViewEmployeeLogs_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try

            Dim LogId As String = e.CommandArgument

            If moMachineLog.ChangeLogType(Convert.ToInt32(LogId)) Then
                GrdDepartmentAttendance0.DataBind()
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moMachineLog.InfoMessage), True)
            End If



            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(LogId), True)
            'Dim id As Integer = GridView2.DataKeys(index)("MachineId").ToString()

        Catch ex As Exception
            Dim message As String = ex.Message
            ' Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try
    End Sub

    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function
End Class
