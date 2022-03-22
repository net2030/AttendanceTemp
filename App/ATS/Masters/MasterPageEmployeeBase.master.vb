Imports System.Management
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO

Partial Class MasterPageEmployeeBase
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init '
        'If LicensingBLL.IsFreeAccount(Session("AccountId")) And Me.Page.ToString.Contains("accountlicenseactivation") = False Then
        '    System.Web.HttpContext.Current.Response.Redirect("~/AccountAdmin/AccountLicenseActivation.aspx", False)
        'End If
        If AuthenticateBLL.IsNewSession() Then
            If Me.Page.IsCallback Then
                AuthenticateBLL.DoLogoutForCallBack(Me.Page)
            Else
                AuthenticateBLL.DoLogout(Me.Page)
            End If
        End If

        LocaleUtilitiesBLL.SetPageCultureSetting(Me.Page)
        If Not ATS.BO.BOPagePermission.IsPageHasRightsByPage(Me.Page) Then
            If Me.Page.IsCallback Then
                'DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/Employee/NoPermission.aspx")
            Else
                Response.Redirect("~/Employee/NoPermission.aspx", False)
            End If
        End If

        ' Return

        'Dim dat As Date = New DateTime(2018, 11, 11, 0, 0, 0)

        If System.Web.HttpContext.Current.Session("username").ToString.ToLower <> "pro_watch" And System.Web.HttpContext.Current.Session("username").ToString.ToLower <> "adnan" And System.Web.HttpContext.Current.Session("username").ToString.ToLower <> "admin" Then
            Dim SystemID As String = Session("MachineId")
            Dim ExpireDate As String = Session("AccountExpiryActivation")
            Dim NoOfReaders As String = Session("LicenseId")
            Dim BL As programLic = New programLic
            If BL.IsAllowedToUseApplication(SystemID, ExpireDate, NoOfReaders) = False And NeedForCheckingLicense(Me.Page) = False Then
                System.Web.HttpContext.Current.Response.Redirect("~/AccountAdmin/AccountPreferences.aspx", False)
            End If

        End If
    End Sub
   


    Public Shared Function NeedForCheckingLicense(ByVal objPage As Page) As Boolean



        Dim ThisPage As String = objPage.AppRelativeVirtualPath
        Dim SlashPos As Integer = InStrRev(ThisPage, "/")
        Dim PageName As String = Right(ThisPage, Len(ThisPage) - SlashPos)


        If PageName = "AccountPreferences.aspx" Or PageName = "LicenseActivation.aspx" Or PageName = "NoPermission.aspx" Then
            Return True
        End If

        Return False

    End Function


End Class

