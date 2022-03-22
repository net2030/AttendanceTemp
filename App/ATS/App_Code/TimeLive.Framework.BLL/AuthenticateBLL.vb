Imports System.DirectoryServices
Imports Microsoft.VisualBasic
Imports System.Security.Principal
Imports System.Web.Security
Imports ATS.BO.Framework
Imports ATS.BO

Public Class AuthenticateBLL

    Public Function AfterLoggedIn(ByVal Username As String, ByVal Password As String) As Boolean
        If Not Username.ToLower = "systemadmin" Then
            Dim InstalledCustomerId As Integer = Me.GetInstalledAccountId
            Dim moEmployee As New BOEmployee
            Dim dsEmployee As New System.Data.DataSet
            dsEmployee = moEmployee.GetEmployeeByUserName(Username)

            If dsEmployee.Tables.Count > 0 AndAlso dsEmployee.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Else
            Return True
        End If
    End Function



    

    Public Function AuthenticateLogin(ByVal Username As String, ByVal Password As String, Optional ByVal IsAutoLogin As Boolean = True) As Boolean
        Dim LDAP As New LDAPUtilitiesBLL

        If Username.ToLower = "systemadmin" Then
            If New BOEmployee().ValidateLogin(Username.ToLower, Password) = True Then
                Return True
            Else
                Return False
            End If
        Else
            If LDAP.IsAspNetActiveDirectoryMembershipProvider = True Then    'Here checking AD

                Dim BLL As New AuthenticateBLL

                Dim MembershipValidUser As Boolean


                MembershipValidUser = Membership.ValidateUser(Username, Password)

                If MembershipValidUser = True Then
                    If BLL.AfterLoggedIn(Username, Password) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                If Membership.ValidateUser(Username, Password) = True Then
                    Return True
                End If
            End If
          

        End If

    End Function

    Public Function LoggedIn(ByVal Username As String, ByVal Password As String, Optional ByVal IsTimeLiveMobile As Boolean = False) As Boolean

        Dim authTicket As System.Web.Security.FormsAuthenticationTicket
        Dim BLL As New AuthenticateBLL
        If New LDAPUtilitiesBLL().IsAspNetActiveDirectoryMembershipProvider = True Then
            Call New BOEmployee().ValidateLogin(Username, Password)
        End If
        FormsAuthentication.Initialize()

        If Username.ToLower = "systemadmin" Then
            authTicket = New FormsAuthenticationTicket(1, Username.ToLower, Now, Now.AddMinutes(System.Web.HttpContext.Current.Session.Timeout), True, -1, FormsAuthentication.FormsCookiePath)
        Else
            authTicket = New FormsAuthenticationTicket(1, Username, Now, Now.AddMinutes(System.Web.HttpContext.Current.Session.Timeout), True, System.Web.HttpContext.Current.Session("AccountEmployeeId"), FormsAuthentication.FormsCookiePath)

        End If
        Dim encTicket As String = FormsAuthentication.Encrypt(authTicket)
        If Username.ToLower = "systemadmin" Then
            System.Web.HttpContext.Current.Session("UserName") = Username.ToLower

        Else
            System.Web.HttpContext.Current.Session("UserName") = Username
        End If
        System.Web.HttpContext.Current.Response.Cookies.Add(New HttpCookie(FormsAuthentication.FormsCookieName, encTicket))
        Call BOPagePermission.GetAccountPermissionsOfCurrentRoles()
        System.Web.HttpContext.Current.Session.Add("IsTimeLiveMobile", IsTimeLiveMobile)

        'Save License Information
        
        SaveLicenceDetails()

    End Function
    Public Function GetInstalledAccountId() As Integer
        Return 1
    End Function

    Public Shared Sub DoLogout(ByVal CurrentPage As Page, Optional ByVal IsTimeLiveMobileLogin As Boolean = False)

        System.Web.HttpContext.Current.Session.Abandon()
        System.Web.Security.FormsAuthentication.SignOut()
        UIUtilities.RedirectToLoginPage(CurrentPage, IsTimeLiveMobileLogin)
    End Sub
    Public Shared Sub DoLogoutForCallBack(ByVal CurrentPage As Page, Optional ByVal IsTimeLiveMobileLogin As Boolean = False)
        System.Web.HttpContext.Current.Session.Abandon()
        System.Web.Security.FormsAuthentication.SignOut()
        UIUtilities.RedirectToLoginPageForCallBack(CurrentPage, IsTimeLiveMobileLogin)
    End Sub
    Public Shared Function IsNewSession() As Boolean
        If (System.Web.HttpContext.Current.Session.IsNewSession) Or (System.Web.HttpContext.Current.Session.Count = 0) Or (System.Web.HttpContext.Current.User.Identity.Name = "") Then
            Return True
        End If
    End Function
    Public Shared Sub CheckSession(ByVal Page As System.Web.UI.Page)
        If AuthenticateBLL.IsNewSession() Then
            AuthenticateBLL.DoLogout(Page)
        End If
        LocaleUtilitiesBLL.SetPageCultureSetting(Page)
    End Sub
    Public Shared Sub DoLogoutForSessionExpired()
        System.Web.HttpContext.Current.Session.Abandon()
        System.Web.Security.FormsAuthentication.SignOut()
    End Sub

    Public Function SaveLicenceDetails() As Boolean
        Try

            Dim AccBLL As AccountDAL = New AccountDAL
            Dim dtPreference As DataTable = AccBLL.GetDataByAccountId(1).Tables(0)
            Dim drPreference As DataRow

            drPreference = dtPreference.Rows(0)

            ' If System.Web.HttpContext.Current.Session("username").ToString.ToLower <> "pro_watch" And System.Web.HttpContext.Current.Session("username").ToString.ToLower <> "adnan" And System.Web.HttpContext.Current.Session("username").ToString.ToLower <> "admin" Then


            Dim culture As String = drPreference("culture")
            Dim SystemID As String = drPreference("MachineId")
            Dim ExpireDate As String = drPreference("AccountExpiryActivation")
            Dim NoOfReaders As String = drPreference("LicenseId")

            Dim PageSize As String = drPreference("PageSize")

            Dim BL As programLic = New programLic

            System.Web.HttpContext.Current.Session.Add("CultureInfo", culture)
            System.Web.HttpContext.Current.Session.Add("MachineId", SystemID)
            System.Web.HttpContext.Current.Session.Add("AccountExpiryActivation", ExpireDate)
            System.Web.HttpContext.Current.Session.Add("LicenseId", NoOfReaders)
            System.Web.HttpContext.Current.Session.Add("PageSize", PageSize)

        Catch ex As Exception
            Return False
        End Try



        Return True
    End Function
End Class
