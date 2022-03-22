
Imports ATS.BO.Framework
Imports System.Data.SqlClient
Imports ATS.DA.Framework
Imports System.IO

Partial Class ctlAccountEmployeeLogsByDepartment
    Inherits System.Web.UI.UserControl
    Private moMachineLog As New BOMachineLog
    Dim Lang As String = "ar"
    Public Shared LogsDT As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            GrdEmployeeAttendance.DataSource = New List(Of String)
            GrdEmployeeAttendance.DataBind()
            Startdate.SelectedDate = Now.Date
            Enddate.SelectedDate = Now.Date
            'ctlDepartmentTree1.SelectedValue = Session("Department1")

            Dim EmployeeID As Integer

            If Request.QueryString("EmployeeID") Is Nothing Then
                EmployeeID = Session("AccountEmployeeID")
            Else
                EmployeeID = Me.Request.QueryString("EmployeeID")
            End If

            Dim ds As DataSet
            ds = GetData()
            If ds.Tables.Count > 0 Then
                LogsDT = GetData().Tables(0)
                Me.GrdEmployeeAttendance.DataSource = LogsDT
            End If

            Me.GrdEmployeeAttendance.DataBind()

            'Me.dsEmployeeAttendance.SelectParameters.Item("EmployeeID").DefaultValue = EmployeeID
            'Me.dsEmployeeAttendance.SelectParameters.Item("BegDate").DefaultValue = SDate
            'Me.dsEmployeeAttendance.SelectParameters.Item("EndDate").DefaultValue = EDate

        End If


    End Sub


#Region "Data Binding"

    Private Function GetData() As DataSet


        Dim ds As New DataSet

        Lang = ATS.BO.UIUtilities.GetCurrentLanguage()

        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("Logs.SpGetLogsByDepartment")
                Using cmd
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon
                    cmd.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = 1
                    cmd.Parameters.Add("@BegDate", SqlDbType.Date).Value = Me.Startdate.SelectedDate
                    cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = Me.Enddate.SelectedDate
                    cmd.Parameters.Add("@Lang", SqlDbType.Char, 2).Value = Lang

                    Dim Adapter As New SqlDataAdapter

                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds)

                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
        Return ds
    End Function
#End Region



    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("console.log('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function

    Protected Sub viewData_Click(sender As Object, e As EventArgs) Handles viewData.Click
        Dim ds As DataSet
        ds = GetData()
        If ds.Tables.Count > 0 Then
            LogsDT = GetData().Tables(0)
            Me.GrdEmployeeAttendance.DataSource = LogsDT
            Me.GridView1.DataSource = LogsDT
        End If

        If LogsDT.Rows.Count > 0 Then
            Me.GridView1.DataBind()
            Me.GrdEmployeeAttendance.DataBind()
            Me.Button1.Enabled = True
        Else
            Me.Button1.Enabled = False
        End If
    End Sub

    Protected Sub GrdEmployeeAttendance_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrdEmployeeAttendance.PageIndexChanging
        GrdEmployeeAttendance.PageIndex = e.NewPageIndex
        gridviewdatabinding()
    End Sub

    Protected Sub gridviewdatabinding()

        GrdEmployeeAttendance.DataSource = LogsDT
        GrdEmployeeAttendance.DataBind()
    End Sub


    Private Sub ExportGridToExcel()
        Response.Clear()
        Response.Buffer = True
        Response.ClearContent()
        Response.ClearHeaders()
        Response.Charset = ""
        Dim FileName As String = "Attendance_" & ctlDepartmentTree1.Text & "_" & DateTime.Now & ".xls"
        Dim strwritter As StringWriter = New StringWriter()
        Dim htmltextwrtter As HtmlTextWriter = New HtmlTextWriter(strwritter)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & FileName)
        GridView1.GridLines = GridLines.Both
        GridView1.HeaderStyle.Font.Bold = True
        GridView1.RenderControl(htmltextwrtter)
        Response.Write(strwritter.ToString())
        Response.[End]()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        ExportGridToExcel()
    End Sub
End Class
