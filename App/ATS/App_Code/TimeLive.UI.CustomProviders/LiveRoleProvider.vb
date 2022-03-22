Imports System
Imports System.Web.Security
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Configuration.Provider
Imports System.Web.Hosting
Imports System.Xml
Imports System.Security.Permissions
Imports System.Web
Namespace CustomProviders

    Public Class LiveRoleProvider
        Inherits RoleProvider


        ' RoleProvider properties
        Public Overrides Property ApplicationName() As String
            Get
                Throw New NotSupportedException()
            End Get
            Set(ByVal value As String)
                Throw New NotSupportedException()
            End Set
        End Property

        ' RoleProvider methods
        Public Overrides Sub Initialize(ByVal name As String, _
                                ByVal config As NameValueCollection)
            ' Verify that config isn't null
            If config Is Nothing Then
                Throw New ArgumentNullException("config")
            End If

            ' Assign the provider a default name if it doesn't have one
            If String.IsNullOrEmpty(name) Then
                name = "LiveRoleProvider"
            End If

            ' Add a default "description" attribute to config if the
            ' attribute doesn't exist or is empty
            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", "Live Role Provider")
            End If

            ' Call the base class's Initialize method
            MyBase.Initialize(name, config)


        End Sub

        Public Overrides Function IsUserInRole(ByVal username As String, _
                                    ByVal roleName As String) As Boolean

            Throw New NotSupportedException()
        End Function
        Public Overrides Function GetRolesForUser( _
                                    ByVal username As String) As String()

            ' Validate input parameters
            If username Is Nothing Then
                Throw New ArgumentNullException()
            End If
            If username = String.Empty Then
                Throw New ArgumentException()
            End If

            ' Make sure the user name is valid
            If username.ToLower = "systemadmin" Then
                Dim roles(0) As String
                roles(0) = "SystemAdmin"
                Return roles
            Else
                Dim roles As String() = Nothing
                'Dim objAccountEmployeeBLL As New AccountEmployeeBLL

                'Dim AccountEmployeeId = System.Web.HttpContext.Current.Session("AccountEmployeeId")
                'roles = objAccountEmployeeBLL.GetRoles(AccountEmployeeId)


                ' Return role names
                Return roles
            End If

        End Function

        Public Overrides Function GetUsersInRole( _
                                    ByVal roleName As String) As String()
            Throw New NotSupportedException()
        End Function

        Public Overrides Function GetAllRoles() As String()
            Throw New NotSupportedException()
        End Function

        Public Overrides Function RoleExists( _
                                ByVal roleName As String) As Boolean
            ' Validate input parameters
            Throw New NotSupportedException()
        End Function

        Public Overrides Sub CreateRole(ByVal roleName As String)
            Throw New NotSupportedException()
        End Sub

        Public Overrides Function DeleteRole(ByVal roleName As String, _
                        ByVal throwOnPopulatedRole As Boolean) As Boolean
            Throw New NotSupportedException()
        End Function

        Public Overrides Sub AddUsersToRoles( _
                ByVal usernames As String(), ByVal roleNames As String())
            Throw New NotSupportedException()
        End Sub

        Public Overrides Function FindUsersInRole( _
                ByVal roleName As String, _
                ByVal usernameToMatch As String) As String()
            Throw New NotSupportedException()
        End Function

        Public Overrides Sub RemoveUsersFromRoles( _
                ByVal usernames As String(), ByVal roleNames As String())
            Throw New NotSupportedException()
        End Sub



    End Class
End Namespace
