
Imports ATS.BO.Framework

Partial Class Controls_ctlEmployeeAttendance
    Inherits System.Web.UI.UserControl
    Private moMachineLog As New BOMachineLog
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Dim SDate As Date = DateAdd(DateInterval.Year, -10, Now.Date)
            Dim EDate As Date = Now.Date
            Dim EmployeeID As Integer

            If Request.QueryString("EmployeeID") Is Nothing Then
                EmployeeID = Session("AccountEmployeeID")
            Else
                EmployeeID = Me.Request.QueryString("EmployeeID")
            End If


            Me.dsEmployeeAttendance.SelectParameters.Item("EmployeeID").DefaultValue = EmployeeID
            Me.dsEmployeeAttendance.SelectParameters.Item("BegDate").DefaultValue = SDate
            Me.dsEmployeeAttendance.SelectParameters.Item("EndDate").DefaultValue = EDate
            Me.GrdEmployeeAttendance.DataBind()
        End If


    End Sub

   

    Protected Sub GrdEmployeeAttendance_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GrdEmployeeAttendance.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LogId As Integer = DirectCast(Me.GrdEmployeeAttendance.DataKeys(e.Row.RowIndex)("LogId"), Integer)
            Dim grdViewEmployeeLogs As GridView = DirectCast(e.Row.FindControl("grdViewEmployeeLogs"), GridView)
            grdViewEmployeeLogs.DataSource = moMachineLog.GetLogByAttendanceLogIdDataset(LogId, 1, 50)
            grdViewEmployeeLogs.DataBind()
        End If
    End Sub

    Protected Sub GrdDepartmentAttendance0_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GrdEmployeeAttendance.RowCommand
        Try
            Dim moAttendanceLog As New BOAttendanceLog
            Dim LogId As String = e.CommandArgument
            Dim StatusId As Integer

            If e.CommandName = "Escape" Then
                StatusId = 3
            ElseIf e.CommandName = "CancelEscape" Then
                StatusId = 10
            End If

            If moAttendanceLog.ChangeAttendanceStatus(Convert.ToInt32(LogId), StatusId, CInt(Session("AccountEmployeeId"))) Then
                GrdEmployeeAttendance.DataBind()
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moMachineLog.InfoMessage), True)
            End If

        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try
    End Sub


    Protected Sub grdViewEmployeeLogs_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try

            Dim LogId As String = e.CommandArgument
            If moMachineLog.ChangeLogType(Convert.ToInt32(LogId)) Then
                GrdEmployeeAttendance.DataBind()
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
        sb.Append("console.log('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function
End Class
