Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports Telerik.Web.UI

Partial Class Employee_Controls_ctlAttendanceChartByDate
    Inherits System.Web.UI.UserControl

    Dim moAttendanceLog As New ATS.BO.Framework.BOAttendanceLog

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim DeptId As Integer = CInt(Session("AccountDepartmentId1"))
            Me.Startdate.SelectedDate = DateAdd(DateInterval.Month, -1, Now.Date)
            Me.Enddate.SelectedDate = Now.Date

            Dim Lang As String = ATS.BO.UIUtilities.GetCurrentLanguage()
            ReportViewer1.LocalReport.ReportPath = "Reports\RDLC\" + Lang + "\Charts\rptDepartmentComparassionByDate.rdlc"

        End If
    End Sub

    Protected Sub btnView()
        Try


            ' Setup parameter collection
            Dim ParmCollection As ReportParameterInfoCollection
            Dim paramList As New Generic.List(Of ReportParameter)

            ' Prepare parameter values
            Dim strDepartmentName As String = ctlDepartmentTree1.Text

            'strDepartmentName = strDepartmentName.Replace("|__", "")
            Dim strStartDate As String = Startdate.Text
            Dim strEndDate As String = Enddate.Text
            Dim strPrintDate As String = Now


            ' Dim LogoURL As String = Request.Url.AbsoluteUri.Replace("Reports/AttendanceChartComparassionByDepartment.aspx", "") + "/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo.gif"
            Me.ReportViewer1.LocalReport.EnableExternalImages = True
            ' Insert parameter list
            Dim strCompanyName As String = Session("AccountName")
            paramList.Add(New ReportParameter("CompanyName", strCompanyName, False))
            ' paramList.Add(New ReportParameter("LogoPath", LogoURL))
            paramList.Add(New ReportParameter("DepartmentName", strDepartmentName, False))
            paramList.Add(New ReportParameter("BegDate", strStartDate, False))
            paramList.Add(New ReportParameter("EndDate", strEndDate, False))
            paramList.Add(New ReportParameter("PrintDate", strPrintDate, False))


            ' Add parameters list to the report
            Me.ReportViewer1.LocalReport.SetParameters(paramList)

            ParmCollection = Me.ReportViewer1.LocalReport.GetParameters()
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("AttendanceChart", moAttendanceLog.GetAttendanceChartDataForReportComparssion(CInt(ctlDepartmentTree1.SelectedValue), Startdate.SelectedDate, Enddate.SelectedDate, ddlStatus.SelectedValue, ATS.BO.UIUtilities.GetCurrentLanguage()).Tables(0)))
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
        End Try

    End Sub

End Class