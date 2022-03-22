Imports ATS.BO

Partial Class Menus_Controls_ctlAccountAdminSiteMenu
    Inherits System.Web.UI.UserControl
    ''' <summary>
    ''' return dayview and period view
    ''' </summary>
    ''' <param name="URL"></param>
    ''' <param name="PageTitle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EncodeMenuURL(ByVal URL As String, ByVal PageTitle As String) As String
        If PageTitle = "My Timesheet" Then
            If DBUtilities.GetDefaultTimeEntryMode = "Day View" Then
                Return "~/Employee/AccountEmployeeTimeEntryDayView.aspx"
            Else
                Return "~/Employee/AccountEmployeeTimeEntryPeriodView.aspx"
            End If
        End If
        Return URL
    End Function
    ''' <summary>
    ''' Return title
    ''' </summary>
    ''' <param name="PageTitle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EncodeMenuTitle(ByVal PageTitle As String) As String
        Return ResourceHelper.GetFromResource(PageTitle)
    End Function
    ''' <summary>
    ''' Manage site menu home URL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EncodeSiteMenuTitle() As String

        If BOPagePermission.IsPageHasPermissionOf(25, 1) = True Then
            Return "~/Employee/Default.aspx"
        Else
            Me.H.Enabled = False
        End If
    End Function
    ''' <summary>
    ''' page load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        EncodeSiteMenuTitle()
    End Sub
End Class
