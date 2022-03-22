Imports ATS.BO

Partial Class Masters_MasterPageBase
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



            ' Session.Add("CultureInfo", "ar-SA")

            If Me.Page.Request.Url.ToString.Contains("Default") Then
                ' Me.H2.NavigateUrl = Request.Url.AbsoluteUri.Replace("Reports/AbsentsByDate.aspx", "") + "/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo1.gif"
                Dim meta As New HtmlMeta
                meta.Name = "keywords"
                meta.Content = "‰Ÿ«„ Õ÷Ê— Ê«‰’—«› „‰ ‘—ﬂ… „«” «· ﬁ‰Ì…"
                Page.Header.Controls.Add(meta)
            End If
        Me.SetImageUrl()

        If ATS.BO.UIUtilities.GetCurrentLanguage = "en" Then

            Me.name.Text = Session("AccountEmployeeFullName")
            'Me.nametop.Text = "„—Õ»«, " & Session("Role") & " - " & Session("AccountEmployeeFullName")
            'Me.nametop2.Text = "„—Õ»«, " & Session("Role") & " - " & Session("AccountEmployeeFullName")
            ' Me.RoleLabel.Text = Session("Role")
            'EncodeNewMenuURL()


        Else
            Me.name.Text = Session("AccountEmployeeFullNameAr")
            'Me.nametop.Text = "„—Õ»«, " & Session("Role") & " - " & Session("AccountEmployeeFullName")
            'Me.nametop2.Text = "„—Õ»«, " & Session("Role") & " - " & Session("AccountEmployeeFullName")
            ' Me.RoleLabel.Text = Session("RoleAr")
            'EncodeNewMenuURL()


        End If

 

    End Sub

    Public Function IsLogoFileExist() As Boolean

        Dim strUploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadFilesPath")

        Dim strRoot As String = System.Web.HttpContext.Current.Server.MapPath(strUploadPath)
        If Not System.IO.Directory.Exists(strRoot) Then
            Return False
        End If
        Dim strAccountPath As String = strRoot & DBUtilities.GetSessionAccountId & "\"
        If Not System.IO.Directory.Exists(strAccountPath) Then
            Return False
        End If
        Dim strLogoPath As String = strAccountPath & "Logo" & "\"
        If Not System.IO.Directory.Exists(strLogoPath) Then
            Return False
        End If
        Dim strFilePath As String = strLogoPath & "CompanyLogo1.gif"
        If Not System.IO.File.Exists(strFilePath) Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub SetImageUrl()
        'Dim EmployeeBll As New AccountEmployeeBLL
        'If LocaleUtilitiesBLL.IsShowEmployeeProfilePicture Then
        '    Me.I.ImageUrl = EmployeeBll.GetProfilePictureUrl
        'End If

        HI.ImageUrl = ("~/Uploads/" & DBUtilities.GetSessionAccountId & "/Logo/CompanyLogo1.gif")

    End Sub
    ''' <summary>
    ''' Manage site menu home URL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EncodeSiteMenuTitle() As String
        Dim pbll As New BOPagePermission
        If BOPagePermission.IsPageHasPermissionOf(25, 1) = True Then
            Return "~/Employee/Default.aspx"
        Else
            'H1.Enabled = False
        End If
    End Function
    ''' <summary>
    ''' Return title
    ''' </summary>
    ''' <param name="PageTitle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EncodeMenuTitle(ByVal PageTitle As String) As String
        Return ResourceHelper.GetFromResource(PageTitle)
    End Function
    ''' <summary>
    ''' return dayview and period view
    ''' </summary>
    ''' <param name="URL"></param>
    ''' <param name="PageTitle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EncodeMenuURL(ByVal URL As String, ByVal PageTitle As String) As String
        If PageTitle = "My Timesheet" Then
            If DBUtilities.GetDefaultTimeEntryMode = "Day View" Then
                Return "~/Employee/AccountEmployeeTimeEntryDayView.aspx"
            Else
                Return "~/Employee/AccountEmployeeTimeEntryPeriodView.aspx"
            End If
        End If
        Return URL
    End Function
    'Public Function EncodeNewMenuURL() As String
    '    If DBUtilities.GetDefaultTimeEntryMode = "Day View" Then
    '        TimesheetLink.HRef = "../Employee/AccountEmployeeTimeEntryDayView.aspx"
    '        TimesheetLink1.HRef = "../Employee/AccountEmployeeTimeEntryDayView.aspx"
    '    Else
    '        TimesheetLink.HRef = "../Employee/AccountEmployeeTimeEntryPeriodView.aspx"
    '        TimesheetLink1.HRef = "../Employee/AccountEmployeeTimeEntryPeriodView.aspx"
    '    End If
    'End Function
    Public Function IsMenuPage(Id As Integer) As Boolean
        If Id = 4 Or Id = 13 Or Id = 100 Or Id = 167 Or Id = 174 Or Id = 173 Or Id = 195 Or Id = 203 Then
            Return True
        End If
        Return False
    End Function

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Me.Page.Header.Controls.Add(New LiteralControl(String.Format("<meta http-equiv='refresh' content='{0};url={1}'>", (System.Web.HttpContext.Current.Session.Timeout * 60), UIUtilities.GetSessionExpiredURL(Me.Page))))

    End Sub
End Class

