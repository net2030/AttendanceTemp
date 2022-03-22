Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class Employee_Controls_ctlctlFingerprintNotRegistered
    Inherits System.Web.UI.UserControl
    Dim moEmployee As New BOEmployee
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Lang As String = ATS.BO.UIUtilities.GetCurrentLanguage()
            ReportViewer1.LocalReport.ReportPath = "Reports\RDLC\" + Lang + "\FingerPrintRegistrationReport.rdlc"
        End If
    End Sub





    Protected Sub btnView(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
           

            Dim op As Integer
            If Me.rdAll.Checked Then
                op = 0
            ElseIf rdRegistered.Checked Then
                op = 1
            ElseIf rdNotRegistered.Checked Then
                op = 2
            End If
            Dim DeptId = CInt(ctlDepartmentTree1.SelectedValue)
            Dim ds As System.Data.DataSet = moEmployee.GetUnregisteredEmployees(DeptId, op, ATS.BO.UIUtilities.GetCurrentLanguage()) 'EmployeeDAL.GetFingerprintNotRegistered(DeptId, op)

            ' Setup parameter collection
            Dim ParmCollection As ReportParameterInfoCollection
            Dim paramList As New Generic.List(Of ReportParameter)

            ' Dim LogoURL As String = Request.Url.AbsoluteUri.Replace("Reports/FingerprintNotRegistered.aspx", "") + "/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo.gif"
            Me.ReportViewer1.LocalReport.EnableExternalImages = True
            '

            ' Insert parameter list
            'paramList.Add(New ReportParameter("DepartmentName", strDepartmentName, False))
            '  paramList.Add(New ReportParameter("LogoPath", LogoURL))
            ' Add parameters list to the report
            Dim strCompanyName As String = Session("AccountName")
            paramList.Add(New ReportParameter("CompanyName", strCompanyName, False))
            Me.ReportViewer1.LocalReport.SetParameters(paramList)

            ParmCollection = Me.ReportViewer1.LocalReport.GetParameters()
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("EmployeesDataset", ds.Tables(0)))
            ReportViewer1.LocalReport.Refresh()






        Catch ex As Exception
        End Try
    End Sub
End Class