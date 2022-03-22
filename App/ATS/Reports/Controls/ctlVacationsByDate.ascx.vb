Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class Employee_Controls_ctlVacationsByDate
    Inherits System.Web.UI.UserControl
    Dim Lang As String
    Dim moVacation As New BOVacation

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
         
            Me.Startdate.SelectedDate = DateAdd(DateInterval.Month, -1, Now.Date)
            Me.Enddate.SelectedDate = Now.Date

           
        End If

        Lang = ATS.BO.UIUtilities.GetCurrentLanguage()
        ReportViewer1.LocalReport.ReportPath = "Reports\RDLC\" + Lang + "\EmployeesVacationReport.rdlc"
    End Sub

    Protected Sub btnView()
        Try

          

            ' Setup parameter collection
            Dim ParmCollection As ReportParameterInfoCollection
            Dim paramList As New Generic.List(Of ReportParameter)

            ' Prepare parameter values
            Dim strDepartmentName As String = ctlDepartmentTree1.text
            'strDepartmentName = strDepartmentName.Replace("|__", "")
            Dim strStartDate As String = Startdate.Text
            Dim strEndDate As String = Enddate.Text
            Dim strPrintDate As String = Now
            Dim DeptId = CInt(ctlDepartmentTree1.SelectedValue)
            Dim ds As System.Data.DataSet = moVacation.GetDepartmentVacationsDataset(DeptId, Startdate.SelectedDate, Enddate.SelectedDate, 1, 1, Lang)
            Dim strEmployeesInVacation As String = ds.Tables(0).Rows.Count.ToString
            'Dim LogoURL As String = Request.Url.AbsoluteUri.Replace("Reports/VacationsByDate.aspx", "") + "/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo.gif"
            Me.ReportViewer1.LocalReport.EnableExternalImages = True
            ' Insert parameter list
            Dim strCompanyName As String = Session("AccountName")
            paramList.Add(New ReportParameter("CompanyName", strCompanyName, False))
            ' paramList.Add(New ReportParameter("LogoPath", LogoURL))
            paramList.Add(New ReportParameter("DepartmentName", strDepartmentName, False))
            paramList.Add(New ReportParameter("ReportBegDate", strStartDate, False))
            paramList.Add(New ReportParameter("ReportEndDate", strEndDate, False))
            paramList.Add(New ReportParameter("PrintDate", strPrintDate, False))
            paramList.Add(New ReportParameter("EmployeesInVacation", strEmployeesInVacation, False))

            ' Add parameters list to the report
            Me.ReportViewer1.LocalReport.SetParameters(paramList)

            ParmCollection = Me.ReportViewer1.LocalReport.GetParameters()
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DepartmentVacations", ds.Tables(0)))
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
        End Try

    End Sub



    '#Region "ComboBox Tree View"


    '    Private level As Integer = -1
    '    Private Sub ShowTreeNodes(ByVal EmployeeId As Integer)
    '        ddlDepartment.Items.Clear()
    '        Dim dtNodes As DataTable = DepartmentDAL.GetDepartmentList(EmployeeId)
    '        'select all the nodes
    '        RecursiveFillTree(dtNodes, 0)
    '        ddlDepartment.Items.Insert(0, New ListItem("-ลฮสั-", "0"))
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


End Class