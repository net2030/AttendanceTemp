Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports Telerik.Web.UI

Partial Class Employee_Controls_ctlDailyAttendance
    Inherits System.Web.UI.UserControl

    Dim moAttendanceLog As New ATS.BO.Framework.BOAttendanceLog
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.fromDate.SelectedDate = Now.Date
            Me.toDate.SelectedDate = Now.Date

            Dim Lang As String = ATS.BO.UIUtilities.GetCurrentLanguage()
            ReportViewer1.LocalReport.ReportPath = "Reports\RDLC\" + Lang + "\rptGetAttendanceByDepartment.rdlc"

        End If

    End Sub

    Protected Sub btnView()
        Try


            ' Setup parameter collection
            Dim ParmCollection As ReportParameterInfoCollection
            Dim paramList As New Generic.List(Of ReportParameter)

            ' Prepare parameter values
            Dim strDepartmentName As String = ctlDepartmentTree1.Text

            Dim strReportDate As String = fromDate.Text
            Dim strEndDate As String = toDate.Text

            Dim strPrintDate As String = Now
            Dim strDayName As String = GetDayName(fromDate.SelectedDate)

            Dim strEmployer As String = ddlEmployer.SelectedItem.Text
            Dim strContractType As String = ddlContractType.SelectedItem.Text
            Dim strWorkSchedule As String = ddlWorkSchedule.SelectedItem.Text

            ' Dim LogoURL As String = Request.Url.AbsoluteUri.Replace("Reports/DailyAttendance.aspx", "") + "/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo.gif"
            Me.ReportViewer1.LocalReport.EnableExternalImages = True
            ' Insert parameter list
            Dim strCompanyName As String = Session("AccountName")
            paramList.Add(New ReportParameter("CompanyName", strCompanyName, False))
            'paramList.Add(New ReportParameter("LogoPath", LogoURL))
            paramList.Add(New ReportParameter("DepartmentName", strDepartmentName, False))
            paramList.Add(New ReportParameter("Employer", strEmployer, False))
            paramList.Add(New ReportParameter("ContractType", strContractType, False))
            paramList.Add(New ReportParameter("WorkSchedule", strWorkSchedule, False))
            paramList.Add(New ReportParameter("AttendanceDate", strReportDate, False))
            paramList.Add(New ReportParameter("EndDate", strEndDate, False))
            paramList.Add(New ReportParameter("PrintDate", strPrintDate, False))
            'paramList.Add(New ReportParameter("DayName", strDayName, False))


            ' Add parameters list to the report
            Me.ReportViewer1.LocalReport.SetParameters(paramList)

            ParmCollection = Me.ReportViewer1.LocalReport.GetParameters()
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("EmployeeAttendanceDataset", moAttendanceLog.GetDepartmentAttendanceDatasetRange(CInt(ctlDepartmentTree1.SelectedValue), fromDate.SelectedDate, toDate.SelectedDate, ddlEmployer.SelectedValue, ddlContractType.SelectedValue, CInt(ddlWorkSchedule.SelectedValue), 1, 1000, ATS.BO.UIUtilities.GetCurrentLanguage()).Tables(0)))
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
        End Try

    End Sub

    Public Shared Function GetDayName(ByVal GregorainDate As Date) As String
        Dim strDayName As String = String.Empty

        Select Case GregorainDate.DayOfWeek
            Case DayOfWeek.Saturday
                strDayName = "«·”» "
            Case DayOfWeek.Sunday
                strDayName = "«·√Õœ"
            Case DayOfWeek.Monday
                strDayName = "«·√À‰Ì‰"
            Case DayOfWeek.Tuesday
                strDayName = "«·À·«À«¡"
            Case DayOfWeek.Wednesday
                strDayName = "«·√—»⁄«¡"
            Case DayOfWeek.Thursday
                strDayName = "«·Œ„Ì”"
            Case DayOfWeek.Friday
                strDayName = "«·Ã„⁄…"
        End Select
        Return strDayName
    End Function

    '#Region "ComboBox Tree View"


    '    Private level As Integer = -1
    '    Private Sub ShowTreeNodes(ByVal EmployeeId As Integer)
    '        ddlDepartment.Items.Clear()
    '        Dim dtNodes As DataTable = DepartmentDAL.GetDepartmentList(EmployeeId)
    '        'select all the nodes
    '        RecursiveFillTree(dtNodes, 0)
    '        ddlDepartment.Items.Insert(0, New ListItem("-≈Œ —-", "0"))
    '    End Sub

    '    Private Sub RecursiveFillTree(dtParent As DataTable, parentID As Integer)
    '        level += 1
    '        'on the each call level increment 1
    '        Dim appender As New StringBuilder()

    '        For j As Integer = 0 To level - 1

    '            appender.Append("&nbsp;&nbsp;&nbsp;&nbsp;")
    '        Next
    '        If level > 0 Then
    '            appender.Append("|__")
    '        End If

    '        Dim dv As New DataView(dtParent)

    '        dv.RowFilter = String.Format("ParentId = {0}", parentID)

    '        Dim i As Integer

    '        If dv.Count > 0 Then
    '            For i = 0 To dv.Count - 1
    '                ddlDepartment.Items.Add(New ListItem(Server.HtmlDecode(appender.ToString() & dv(i)("DepartmentName").ToString()), dv(i)("DepartmentId").ToString()))
    '                RecursiveFillTree(dtParent, Integer.Parse(dv(i)("DepartmentId").ToString()))
    '            Next
    '        End If

    '        level -= 1
    '        'on the each function end level will decrement by 1
    '    End Sub

    '#End Region


    'Protected Sub ddlWorkSchedule_DataBound(sender As Object, e As EventArgs) Handles ddlWorkSchedule.DataBound
    '    ddlWorkSchedule.Items.Insert(0, New ListItem("≈Œ —", "0"))
    'End Sub
End Class