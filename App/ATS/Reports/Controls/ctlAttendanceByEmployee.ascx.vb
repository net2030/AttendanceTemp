Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms

Partial Class Employee_Controls_ctlAttendanceByEmployee
    Inherits System.Web.UI.UserControl
    Dim moAttendanceLog As New ATS.BO.Framework.BOAttendanceLog
    Dim moEmployee As ATS.BO.Framework.BOEmployee
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim Role As Integer = CInt(Session("AccountRoleId"))
            If Role = 2 Then
                ddlEmployees.Items.Insert(0, New ListItem(Session("EmployeeNameWithCode"), "0"))
                ddlEmployees.Enabled = False
                ddlDepartment.Enabled = False
                txtEmployeeCode.Enabled = False
                Button1.Enabled = False
            End If
            Me.Startdate.SelectedDate = DateAdd(DateInterval.Month, -1, Now.Date)
            Me.Enddate.SelectedDate = Now.Date

            Dim Lang As String = ATS.BO.UIUtilities.GetCurrentLanguage()
            ReportViewer1.LocalReport.ReportPath = "Reports\RDLC\" + Lang + "\rptGetAttendanceByEmployee.rdlc"


        End If
    End Sub

    Protected Sub btnView()
        Try
            ' Setup parameter collection
            Dim ParmCollection As ReportParameterInfoCollection
            Dim paramList As New Generic.List(Of ReportParameter)

            ' Prepare parameter values
            Dim strEmployeeName As String = Me.ddlEmployees.SelectedItem.Text
            Dim strStartDate As String = Startdate.Text
            Dim strEndDate As String = Enddate.Text
            Dim strPrintDate As String = Now


            'Dim LogoURL As String = Request.Url.AbsoluteUri.Replace("Reports/AttendanceByEmployee.aspx", "") + "/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo.gif"
            Me.ReportViewer1.LocalReport.EnableExternalImages = True
            ' Insert parameter list
            Dim strCompanyName As String = Session("AccountName")
            paramList.Add(New ReportParameter("CompanyName", strCompanyName, False))
            'paramList.Add(New ReportParameter("LogoPath", LogoURL))
            paramList.Add(New ReportParameter("EmployeeName", strEmployeeName, False))
            paramList.Add(New ReportParameter("BegDate", strStartDate, False))
            paramList.Add(New ReportParameter("EndDate", strEndDate, False))
            paramList.Add(New ReportParameter("PrintDate", strPrintDate, False))


            ' Add parameters list to the report
            Me.ReportViewer1.LocalReport.SetParameters(paramList)

            ParmCollection = Me.ReportViewer1.LocalReport.GetParameters()
            'ObjectDataSource1.SelectParameters.Item("EmployeeId").DefaultValue = 5
            'ObjectDataSource1.SelectParameters.Item("BegDate").DefaultValue = Startdate.SelectedDate
            'ObjectDataSource1.SelectParameters.Item("EndDate").DefaultValue = Enddate.SelectedDate
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            If CInt(Session("AccountRoleId")) = 2 Then
                Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("EmployeeAttendanceDataset", moAttendanceLog.GetEmployeeAttendanceByDate(CInt(Session("AccountEmployeeId")), Startdate.SelectedDate, Enddate.SelectedDate, 1, 500, Request.Cookies("CurrentLanguage").Value.ToString(), ddlStatus.SelectedValue).Tables(0)))
            Else
                Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("EmployeeAttendanceDataset", moAttendanceLog.GetEmployeeAttendanceByDate(ddlEmployees.SelectedValue, Startdate.SelectedDate, Enddate.SelectedDate, 1, 500, Request.Cookies("CurrentLanguage").Value.ToString(), ddlStatus.SelectedValue).Tables(0)))
            End If
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
        End Try

    End Sub

    

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        moEmployee = New ATS.BO.Framework.BOEmployee

        Try
            Dim dt As DataTable = New DataTable
        Dim EmployeeCode As String = txtEmployeeCode.Text

        dt = moEmployee.GetEmployeesByBadgeDataset(Session("AccountEmployeeId"), EmployeeCode, 1, 1).Tables(0)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then


            ddlEmployees.DataSource = dt
            ddlEmployees.DataBind()
            ddlEmployees.SelectedValue = dt.Rows(0)("EmployeeId")
            ddlDepartment.SelectedValue = dt.Rows(0)("DepartmentId")
        Else

            ddlEmployees.SelectedValue = 0

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlDepartment_SelectedNodeChanged() Handles ddlDepartment.SelectedNodeChanged

        moEmployee = New ATS.BO.Framework.BOEmployee
        Dim DepartmentId As Integer = ddlDepartment.SelectedValue
        ddlEmployees.DataSource = moEmployee.GetDepartmentEmployeesDataset(DepartmentId, 1, 100).Tables(0)
        ddlEmployees.DataBind()
    End Sub

    Protected Sub ddlStatus_DataBound(sender As Object, e As EventArgs) Handles ddlStatus.DataBound

        ddlStatus.Items.Insert(0, New ListItem("......≈Œ — Õ«·… «·œÊ«„......", "0"))
    End Sub
End Class