Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.Windows.Forms
Imports System.Globalization
Imports ATS.DA.Framework
Imports ATS.BO.Framework

Partial Class Employee_Controls_ctlctlNotification
    Inherits System.Web.UI.UserControl
    Public Shared DT As DataTable
    Dim moAttendanceLog As BOAttendanceLog
    Private DirectorName, a As String
    Private DirectorTitle, b As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            btnView()
            Dim Lang As String = ATS.BO.UIUtilities.GetCurrentLanguage()
            ReportViewer1.LocalReport.ReportPath = "Reports\RDLC\" + Lang + "\Notification.rdlc"
        End If
    End Sub


#Region "Data Binding"
    Private Function GetData(ByVal op As Integer) As DataSet
        Dim ds As New DataSet
        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("[Notifications].[SpGetAbsenceForNotification]")
                Using cmd
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon
                    cmd.Parameters.Add("@Op", SqlDbType.Int).Value = op
                    Dim Adapter As New SqlDataAdapter

                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds)
                    DT = ds.Tables(0)
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
        Return ds
    End Function
#End Region

    Protected Sub btnView()
        Dim op As Integer
        If Me.rdAll.Checked Then
            op = 0
        ElseIf rdRegistered.Checked Then
            op = 1
        ElseIf rdNotRegistered.Checked Then
            op = 2
        End If

        gvTreeNodes.DataSource = GetData(op)
        gvTreeNodes.DataBind()

    End Sub

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTreeNodes.RowCommand
        If (e.CommandName = "View") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            'Dim row As GridViewRow = gvTreeNodes.Rows(index)
            ViewNotification(index)
        ElseIf (e.CommandName = "Issue") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            'Dim row As GridViewRow = gvTreeNodes.Rows(index)
            IssueNotification(index)
        ElseIf (e.CommandName = "Receipt") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            'Dim row As GridViewRow = gvTreeNodes.Rows(index)
            ReceiptNotification(index)
        End If



    End Sub
    Protected Sub ViewNotification(ByVal index As Integer)
        Dim ParmCollection As ReportParameterInfoCollection
        Dim paramList As New Generic.List(Of ReportParameter)
        Dim Str1 As String = ""
        Dim Str2 As String = ""
        Dim Str3 As String = ""


        ' Prepare parameter values
        Dim EmployeeNo As String = gvTreeNodes.DataKeys(index)("EmployeeNo").ToString()
        Dim EmployeeName As String = gvTreeNodes.DataKeys(index)("EmployeeName").ToString()
        Dim DepartmentName As String = gvTreeNodes.DataKeys(index)("DepartmentName").ToString()
        Dim Title As String = gvTreeNodes.DataKeys(index)("Title").ToString()
        Dim StartDate As String = gvTreeNodes.DataKeys(index)("StartDate").ToString()


        Dim EndDate As String = gvTreeNodes.DataKeys(index)("EndDate").ToString()

        'Dim DirectorName As String = gvTreeNodes.DataKeys(e.NewEditIndex)("DirectorName").ToString()
        'Dim DirectorTitle As String = gvTreeNodes.DataKeys(e.NewEditIndex)("DirectorTitle").ToString()

        Dim DirectorName As String = Me.ddlManagers.SelectedItem.Text
        Dim DirectorTitle As String = Me.ddlManagers.SelectedItem.Value

        Dim AbsenceDays As Integer = CInt(gvTreeNodes.DataKeys(index)("Days").ToString())
        Dim AbsenceType As String = gvTreeNodes.DataKeys(index)("AbsenceType").ToString()
        'Dim obj As MdlConvertNoToText = New MdlConvertNoToText
        'Dim strDays As String = obj.NoToText(AbsenceDays, "DAYS")

        'If AbsenceDays >= 5 AndAlso AbsenceType = "„ ’·…" Then
        '    Str1 = " ((   ≈‰–«— ≈‰ﬁÿ«⁄ ⁄‰ «·⁄„· ·„œ… " + strDays + " „ ’·… ))"
        '    Str2 = "†† ‰Ÿ‹‹‹—« · €Ì‹‹‹‹‹»ﬂ„ ⁄‹‹‹‹‹‰ «·⁄„‹‹‹‹‹‹‹· »‹‹‹‹œÊ‰ ⁄‹‹‹‹–— ·„‹‹‹‹œ…  " + strDays + " „ ’‹‹‹‹‹‹‹‹·… Œ‹‹‹‹‹‹‹‹·«· «·› ‹‹‹‹‹‹—…"
        '    Str3 = "·–« ‰ÊÃÂ ·ﬂ„ Â–« «·«‰–«— »÷—Ê—… „»«‘— ﬂ„ «·⁄„· Õ Ï ·« ‰Ÿÿ— ·≈‰Â«¡ Œœ„« ﬂ„ «” ‰«œ« «·Ï «·„«œ… (80) „‰ ‰Ÿ«„ «·⁄„· Ê«·⁄„«· ›Ì Õ«·… «” „—«— €Ì«»ﬂ„ ·„œ… (5) √Ì«„ √Œ—Ï „ ’·… ··√Â„Ì…"
        'ElseIf AbsenceDays >= 10 AndAlso AbsenceType = "„ ›—ﬁ…" Then
        '    Str1 = " ((   ≈‰–«— ≈‰ﬁÿ«⁄ ⁄‰ «·⁄„· ·„œ…  " + strDays + " „ ›—ﬁ… ))"
        '    Str2 = "†† ‰Ÿ‹‹‹—« · €Ì‹‹‹‹‹»ﬂ„ ⁄‹‹‹‹‹‰ «·⁄„‹‹‹‹‹‹‹· »‹‹‹‹œÊ‰ ⁄‹‹‹‹–— ·„‹‹‹‹œ…  " + strDays + " „ ‹‹‹›—ﬁ… Œ‹‹‹‹‹‹‹‹·«· «·› ‹‹‹‹‹‹—…"
        '    Str3 = "·–« ‰ÊÃÂ ·ﬂ„ Â–« «·≈‰–«— »÷—Ê—… «Õ —«„ﬂ„ ·„Ê«⁄Ìœ «·⁄„· Ê«·„Ê«Ÿ»… ⁄·ÌÂ , Õ Ï ·« ‰÷ÿ— ·≈‰Â«¡ Œœ„« ﬂ„ «” ‰«œ« ≈·Ï «·„«œ… (80) „‰ ‰Ÿ«„ «·⁄„· , ›Ì Õ«·… «” „—«— €Ì«»ﬂ„ „œ… √ﬂÀ— „‰ 20 ÌÊ„« Œ·«· «·”‰… «·Õ«·Ì… ."
        'End If

        If AbsenceDays >= 5 AndAlso AbsenceType = "„ ’·…" Then
            Str1 = " ((   ≈‰–«— ≈‰ﬁÿ«⁄ ⁄‰ «·⁄„· ·„œ… Œ„”…  «Ì«„ „ ’·… ))"
            Str2 = "†† ‰Ÿ‹‹‹—« · €Ì‹‹‹‹‹»ﬂ„ ⁄‹‹‹‹‹‰ «·⁄„‹‹‹‹‹‹‹· »‹‹‹‹œÊ‰ ⁄‹‹‹‹–— ·„‹‹‹‹œ… Œ„”‹‹‹‹… «Ì‹‹‹‹‹‹‹«„ „ ’‹‹‹‹‹‹‹‹·… Œ‹‹‹‹‹‹‹‹·«· «·› ‹‹‹‹‹‹—…"
            Str3 = "·–« ‰ÊÃÂ ·ﬂ„ Â–« «·«‰–«— »÷—Ê—… „»«‘— ﬂ„ «·⁄„· Õ Ï ·« ‰Ÿÿ— ·≈‰Â«¡ Œœ„« ﬂ„ «” ‰«œ« «·Ï «·„«œ… (80) „‰ ‰Ÿ«„ «·⁄„· Ê«·⁄„«· ›Ì Õ«·… «” „—«— €Ì«»ﬂ„ ·„œ… (5) √Ì«„ √Œ—Ï „ ’·… ··√Â„Ì…"
        ElseIf AbsenceDays >= 10 AndAlso AbsenceType = "„ ›—ﬁ…" Then
            Str1 = " ((   ≈‰–«— ≈‰ﬁÿ«⁄ ⁄‰ «·⁄„· ·„œ… ⁄‘—…  «Ì«„ „ ›—ﬁ… ))"
            Str2 = "†† ‰Ÿ‹‹‹—« · €Ì‹‹‹‹‹»ﬂ„ ⁄‹‹‹‹‹‰ «·⁄„‹‹‹‹‹‹‹· »‹‹‹‹œÊ‰ ⁄‹‹‹‹–— ·„‹‹‹‹œ… ⁄‹‹‘—… «Ì‹‹‹‹‹‹‹«„ „ ‹‹‹›—ﬁ… Œ‹‹‹‹‹‹‹‹·«· «·› ‹‹‹‹‹‹—…"
            Str3 = "·–« ‰ÊÃÂ ·ﬂ„ Â–« «·≈‰–«— »÷—Ê—… «Õ —«„ﬂ„ ·„Ê«⁄Ìœ «·⁄„· Ê«·„Ê«Ÿ»… ⁄·ÌÂ , Õ Ï ·« ‰÷ÿ— ·≈‰Â«¡ Œœ„« ﬂ„ «” ‰«œ« ≈·Ï «·„«œ… (80) „‰ ‰Ÿ«„ «·⁄„· , ›Ì Õ«·… «” „—«— €Ì«»ﬂ„ „œ… √ﬂÀ— „‰ 20 ÌÊ„« Œ·«· «·”‰… «·Õ«·Ì… ."
        End If

        'Dim LogoURL As String = Request.Url.AbsoluteUri.Replace("Reports/Notification.aspx", "") + "/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo.gif"
        'Me.ReportViewer1.LocalReport.EnableExternalImages = True
        ' Insert parameter list
        Dim strCompanyName As String = Session("AccountName")
        paramList.Add(New ReportParameter("CompanyName", strCompanyName, False))
        'paramList.Add(New ReportParameter("LogoPath", LogoURL))
        paramList.Add(New ReportParameter("Str1", Str1, False))
        paramList.Add(New ReportParameter("Str2", Str2, False))
        paramList.Add(New ReportParameter("Str3", Str3, False))
        paramList.Add(New ReportParameter("EmployeeNo", EmployeeNo, False))
        paramList.Add(New ReportParameter("EmployeeName", EmployeeName, False))
        paramList.Add(New ReportParameter("DepartmentName", DepartmentName, False))
        paramList.Add(New ReportParameter("Title", Title, False))
        paramList.Add(New ReportParameter("StartDate", StartDate, False))
        paramList.Add(New ReportParameter("Enddate", EndDate, False))
        paramList.Add(New ReportParameter("DirectorName", DirectorName, False))
        paramList.Add(New ReportParameter("DirectorTitle", DirectorTitle, False))






        'ReportViewer1.LocalReport.ReportEmbeddedResource = "~\Reports\RDLC\Notification.rdlc"
        ReportViewer1.LocalReport.SetParameters(paramList)

        ParmCollection = ReportViewer1.LocalReport.GetParameters()
        ReportViewer1.LocalReport.DataSources.Clear()
        'ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("EmployeeAttendanceDataset", AttendanceDAL.GetAbsentsByDate(ddlDepartment.SelectedItem.Value, Startdate.SelectedDate, Enddate.SelectedDate)))
        ReportViewer1.LocalReport.Refresh()
        gvTreeNodes.Visible = False
        Div1.Visible = False
        ReportViewer1.Visible = True
    End Sub
    'Protected Sub gvTreeNodes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTreeNodes.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        If (CInt(DataBinder.Eval(e.Row.DataItem, "Days").ToString) >= 10 AndAlso DataBinder.Eval(e.Row.DataItem, "AbsenceType").ToString = "„ ’·…") Or (CInt(DataBinder.Eval(e.Row.DataItem, "Days").ToString) >= 20 AndAlso DataBinder.Eval(e.Row.DataItem, "AbsenceType").ToString = "„ ›—ﬁ…") Then
    '            e.Row.BackColor = System.Drawing.Color.Red
    '        End If
    '    End If

    '    'If CInt(DataBinder.Eval(e.Row.DataItem, "Days").ToString) >= 20 AndAlso DataBinder.Eval(e.Row.DataItem, "AbsenceType").ToString = "„ ›—ﬁ…" Then
    '    'End If
    'End Sub
    Protected Sub IssueNotification(ByVal index As Integer)
        Dim ParmCollection As ReportParameterInfoCollection
        Dim paramList As New Generic.List(Of ReportParameter)
        Dim Str1 As String = ""
        Dim Str2 As String = ""
        Dim Str3 As String = ""

        Try


            ' Prepare parameter values
            Dim EmployeeNo As String = gvTreeNodes.DataKeys(index)("EmployeeNo").ToString()
            Dim EmployeeName As String = gvTreeNodes.DataKeys(index)("EmployeeName").ToString()
            Dim DepartmentName As String = gvTreeNodes.DataKeys(index)("DepartmentName").ToString()
            Dim Title As String = gvTreeNodes.DataKeys(index)("Title").ToString()
            Dim StartDate As Date = gvTreeNodes.DataKeys(index)("StartDate").ToString()
            Dim EndDate As Date = gvTreeNodes.DataKeys(index)("EndDate").ToString()
            'Dim DirectorName As String = gvTreeNodes.DataKeys(e.NewEditIndex)("DirectorName").ToString()
            'Dim DirectorTitle As String = gvTreeNodes.DataKeys(e.NewEditIndex)("DirectorTitle").ToString()

            Dim DirectorName As String = Me.ddlManagers.SelectedItem.Text
            Dim DirectorTitle As String = Me.ddlManagers.SelectedItem.Value

            Dim AbsenceDays As Integer = CInt(gvTreeNodes.DataKeys(index)("Days").ToString())
            Dim AbsenceType As String = gvTreeNodes.DataKeys(index)("AbsenceType").ToString()

            If AbsenceDays >= 5 AndAlso AbsenceType = "„ ’·…" Then
                Str1 = " ((   ≈‰–«— ≈‰ﬁÿ«⁄ ⁄‰ «·⁄„· ·„œ… Œ„”…  «Ì«„ „ ’·… ))"
                Str2 = "†† ‰Ÿ‹‹‹—« · €Ì‹‹‹‹‹»ﬂ„ ⁄‹‹‹‹‹‰ «·⁄„‹‹‹‹‹‹‹· »‹‹‹‹œÊ‰ ⁄‹‹‹‹–— ·„‹‹‹‹œ… Œ„”‹‹‹‹… «Ì‹‹‹‹‹‹‹«„ „ ’‹‹‹‹‹‹‹‹·… Œ‹‹‹‹‹‹‹‹·«· «·› ‹‹‹‹‹‹—…"
                Str3 = "·–« ‰ÊÃÂ ·ﬂ„ Â–« «·«‰–«— »÷—Ê—… „»«‘— ﬂ„ «·⁄„· Õ Ï ·« ‰Ÿÿ— ·≈‰Â«¡ Œœ„« ﬂ„ «” ‰«œ« «·Ï «·„«œ… (80) „‰ ‰Ÿ«„ «·⁄„· Ê«·⁄„«· ›Ì Õ«·… «” „—«— €Ì«»ﬂ„ ·„œ… (5) √Ì«„ √Œ—Ï „ ’·… ··√Â„Ì…"
            ElseIf AbsenceDays >= 10 AndAlso AbsenceType = "„ ›—ﬁ…" Then
                Str1 = " ((   ≈‰–«— ≈‰ﬁÿ«⁄ ⁄‰ «·⁄„· ·„œ… ⁄‘—…  «Ì«„ „ ›—ﬁ… ))"
                Str2 = "†† ‰Ÿ‹‹‹—« · €Ì‹‹‹‹‹»ﬂ„ ⁄‹‹‹‹‹‰ «·⁄„‹‹‹‹‹‹‹· »‹‹‹‹œÊ‰ ⁄‹‹‹‹–— ·„‹‹‹‹œ… ⁄‹‹‘—… «Ì‹‹‹‹‹‹‹«„ „ ‹‹‹›—ﬁ… Œ‹‹‹‹‹‹‹‹·«· «·› ‹‹‹‹‹‹—…"
                Str3 = "·–« ‰ÊÃÂ ·ﬂ„ Â–« «·≈‰–«— »÷—Ê—… «Õ —«„ﬂ„ ·„Ê«⁄Ìœ «·⁄„· Ê«·„Ê«Ÿ»… ⁄·ÌÂ , Õ Ï ·« ‰÷ÿ— ·≈‰Â«¡ Œœ„« ﬂ„ «” ‰«œ« ≈·Ï «·„«œ… (80) „‰ ‰Ÿ«„ «·⁄„· , ›Ì Õ«·… «” „—«— €Ì«»ﬂ„ „œ… √ﬂÀ— „‰ 20 ÌÊ„« Œ·«· «·”‰… «·Õ«·Ì… ."
            End If
            '    Dim LogoURL As String = Request.Url.AbsoluteUri.Replace("Reports/Notification.aspx", "") + "/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo.gif"
            'Me.ReportViewer1.LocalReport.EnableExternalImages = True
            ' Insert parameter list
            '   paramList.Add(New ReportParameter("LogoPath", LogoURL))
            paramList.Add(New ReportParameter("Str1", Str1, False))
            paramList.Add(New ReportParameter("Str2", Str2, False))
            paramList.Add(New ReportParameter("Str3", Str3, False))
            paramList.Add(New ReportParameter("EmployeeNo", EmployeeNo, False))
            paramList.Add(New ReportParameter("EmployeeName", EmployeeName, False))
            paramList.Add(New ReportParameter("DepartmentName", DepartmentName, False))
            paramList.Add(New ReportParameter("Title", Title, False))
            paramList.Add(New ReportParameter("StartDate", StartDate.ToString("yyyy/MM/dd", CultureInfo.CreateSpecificCulture("sa-AR")), False))
            paramList.Add(New ReportParameter("Enddate", EndDate.ToString("yyyy/MM/dd", CultureInfo.CreateSpecificCulture("sa-AR")), False))
            paramList.Add(New ReportParameter("DirectorName", DirectorName, False))
            paramList.Add(New ReportParameter("DirectorTitle", DirectorTitle, False))






            'ReportViewer1.LocalReport.ReportEmbeddedResource = "~\Reports\RDLC\Notification.rdlc"
            ReportViewer1.LocalReport.SetParameters(paramList)

            ParmCollection = ReportViewer1.LocalReport.GetParameters()
            ReportViewer1.LocalReport.DataSources.Clear()
            'ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("EmployeeAttendanceDataset", AttendanceDAL.GetAbsentsByDate(ddlDepartment.SelectedItem.Value, Startdate.SelectedDate, Enddate.SelectedDate)))
            ReportViewer1.LocalReport.Refresh()

            Dim oLocalReport As Microsoft.Reporting.WebForms.LocalReport = ReportViewer1.LocalReport


            Dim renderedBytes As Byte() = Nothing
            Dim reportType As String = "PDF"
            Dim mimeType As String = "application/PDF"
            Dim encoding As String = Nothing
            Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
            Dim streams As String() = Nothing
            Dim deviceInfo As String = "<DeviceInfo><OutputFormat>Pdf</OutputFormat><PageWidth>8.5in</PageWidth><PageHeight>11in</PageHeight></DeviceInfo>"


            'Render the report
            renderedBytes = oLocalReport.Render(reportType, deviceInfo, mimeType, encoding, "PDF", streams, warnings)

            System.Web.HttpContext.Current.Response.Clear()
            System.Web.HttpContext.Current.Response.ContentType = mimeType

            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=≈‰–«—- " & EmployeeName & ".pdf")
            System.Web.HttpContext.Current.Response.BinaryWrite(renderedBytes)
            ' System.Web.HttpContext.Current.Response.[End]()

            SaveIssuedNotification(CInt(gvTreeNodes.DataKeys(index)("EmployeeID").ToString()),
                                   CDate(gvTreeNodes.DataKeys(index)("StartDate1").ToString()),
                                   (gvTreeNodes.DataKeys(index)("EndDate1").ToString()))


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SaveIssuedNotification(ByVal EmployeeId As Integer, ByVal Startdate As Date, ByVal EndDate As Date)

        Dim query As String = "INSERT INTO [Notifications].[NotificationsIssued](EmployeeID,StartDate,EndDate) VALUES (@EmployeeID,@StartDate,@EndDate)"
        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Dim com As New SqlCommand(query, oCon)
            com.CommandType = CommandType.Text

            com.Parameters.Add("@StartDate", SqlDbType.Date).Value = Startdate
            com.Parameters.Add("@EndDate", SqlDbType.Date).Value = EndDate
            com.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmployeeId



            oCon.Open()
            com.ExecuteNonQuery()

            oCon.Close()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ReceiptNotification(ByVal index As Integer)


        Try

            Dim EmployeeID As String = gvTreeNodes.DataKeys(index)("EmployeeID").ToString()

            Dim StartDate As Date = gvTreeNodes.DataKeys(index)("StartDate1").ToString()
            Dim EndDate As Date = gvTreeNodes.DataKeys(index)("EndDate1").ToString()

            Dim query As String = "Update [Notifications].[NotificationsIssued] SET IsReciept=1 WHERE EmployeeID=@EmployeeID AND StartDate=@StartDate AND EndDate=@EndDate"
            Try
                Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
                Dim com As New SqlCommand(query, oCon)
                com.CommandType = CommandType.Text

                com.Parameters.Add("@StartDate", SqlDbType.Date).Value = StartDate
                com.Parameters.Add("@EndDate", SqlDbType.Date).Value = EndDate
                com.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmployeeID
              


                oCon.Open()
                com.ExecuteNonQuery()

                oCon.Close()
            Catch ex As Exception
            End Try


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvTreeNodes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvTreeNodes.PageIndex = e.NewPageIndex
        gvTreeNodes.DataSource = DT
        gvTreeNodes.DataBind()
        'btnView()
    End Sub


    

    Protected Sub gvTreeNodes_Sorting(sender As Object, e As GridViewSortEventArgs)

    End Sub
End Class