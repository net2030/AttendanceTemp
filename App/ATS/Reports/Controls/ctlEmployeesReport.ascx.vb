Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports Telerik.Web.UI

Partial Class Employee_Controls_ctlEmployeesReport
    Inherits System.Web.UI.UserControl

    Dim moEmployee As New ATS.BO.Framework.BOEmployee
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        If Not IsPostBack Then
            Dim Lang As String = ATS.BO.UIUtilities.GetCurrentLanguage()
            ReportViewer1.LocalReport.ReportPath = "Reports\RDLC\" + Lang + "\rptGetEmployeesByDepartment.rdlc"
        End If
    End Sub

    Protected Sub btnView()
        Try


            ' Setup parameter collection
            Dim ParmCollection As ReportParameterInfoCollection
            Dim paramList As New Generic.List(Of ReportParameter)

            ' Prepare parameter values
            Dim strDepartmentName As String = ctlDepartmentTree1.Text

            Dim strEmployer As String = ddlEmployer.SelectedItem.Text
            Dim strContractType As String = ddlContractType.SelectedItem.Text

            Dim strPrintDate As String = Now



            ' Dim LogoURL As String = Request.Url.AbsoluteUri.Replace("Reports/DailyAttendance.aspx", "") + "/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo.gif"
            Me.ReportViewer1.LocalReport.EnableExternalImages = True
            ' Insert parameter list
            Dim strCompanyName As String = Session("AccountName")
            paramList.Add(New ReportParameter("CompanyName", strCompanyName, False))
            'paramList.Add(New ReportParameter("LogoPath", LogoURL))
            paramList.Add(New ReportParameter("DepartmentName", strDepartmentName.ToUpper, False))
            paramList.Add(New ReportParameter("Employer", strEmployer.ToUpper, False))
            paramList.Add(New ReportParameter("ContractType", strContractType.ToUpper, False))

            paramList.Add(New ReportParameter("PrintDate", strPrintDate.ToUpper, False))


            ' Add parameters list to the report
            Me.ReportViewer1.LocalReport.SetParameters(paramList)

            ParmCollection = Me.ReportViewer1.LocalReport.GetParameters()
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("EmployeesDataSet", moEmployee.GetEmployeesByDepartmentAndContractTypeAndEmployerDataset(CInt(ctlDepartmentTree1.SelectedValue), ddlEmployer.SelectedValue, ddlContractType.SelectedValue, ATS.BO.UIUtilities.GetCurrentLanguage()).Tables(0)))
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
        End Try

    End Sub


End Class