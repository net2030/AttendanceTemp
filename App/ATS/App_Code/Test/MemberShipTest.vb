Imports Microsoft.VisualBasic
Imports System
Imports System.Xml
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Configuration.Provider
Imports System.Web.Security
Imports System.Web.Hosting
Imports System.Web.Management
Imports System.Security.Permissions
Imports System.Web
Imports System.Security.Cryptography
Imports ATS.BO.Framework

Public Class MemberShipTest
    Inherits MembershipProvider

    Public Overrides Property ApplicationName As String

    Public Overrides Function ChangePassword(username As String, oldPassword As String, newPassword As String) As Boolean

    End Function

    Public Overrides Function ChangePasswordQuestionAndAnswer(username As String, password As String, newPasswordQuestion As String, newPasswordAnswer As String) As Boolean

    End Function

    Public Overrides Function CreateUser(username As String, password As String, email As String, passwordQuestion As String, passwordAnswer As String, isApproved As Boolean, providerUserKey As Object, ByRef status As MembershipCreateStatus) As MembershipUser

    End Function

    Public Overrides Function DeleteUser(username As String, deleteAllRelatedData As Boolean) As Boolean

    End Function

    Public Overrides ReadOnly Property EnablePasswordReset As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property EnablePasswordRetrieval As Boolean
        Get

        End Get
    End Property

    Public Overrides Function FindUsersByEmail(emailToMatch As String, pageIndex As Integer, pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection

    End Function

    Public Overrides Function FindUsersByName(usernameToMatch As String, pageIndex As Integer, pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection

    End Function

    Public Overrides Function GetAllUsers(pageIndex As Integer, pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection

    End Function

    Public Overrides Function GetNumberOfUsersOnline() As Integer

    End Function

    Public Overrides Function GetPassword(username As String, answer As String) As String

    End Function

    Public Overloads Overrides Function GetUser(providerUserKey As Object, userIsOnline As Boolean) As MembershipUser

    End Function

    Public Overloads Overrides Function GetUser(username As String, userIsOnline As Boolean) As MembershipUser

    End Function

    Public Overrides Function GetUserNameByEmail(email As String) As String

    End Function

    Public Overrides ReadOnly Property MaxInvalidPasswordAttempts As Integer
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property MinRequiredNonAlphanumericCharacters As Integer
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property MinRequiredPasswordLength As Integer
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property PasswordAttemptWindow As Integer
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property PasswordFormat As MembershipPasswordFormat
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property PasswordStrengthRegularExpression As String
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property RequiresQuestionAndAnswer As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property RequiresUniqueEmail As Boolean
        Get

        End Get
    End Property

    Public Overrides Function ResetPassword(username As String, answer As String) As String

    End Function

    Public Overrides Function UnlockUser(userName As String) As Boolean

    End Function

    Public Overrides Sub UpdateUser(user As MembershipUser)

    End Sub

    Public Overrides Function ValidateUser(username As String, password As String) As Boolean
        Return New BOEmployee().ValidateLogin(username, password)
    End Function
End Class
