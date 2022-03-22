Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Collections.Generic
Imports System.Configuration.Provider
Imports System.Security.Permissions
Imports System.Data.Common
Imports System.Data
Imports System.Web.Caching
Imports ATS.BO

Namespace CustomProviders
    ''' <summary>
    ''' TimeLive SQL SiteMap Provider provide role based permission to TimeLive application
    ''' </summary>
    <SqlClientPermission(SecurityAction.Demand, Unrestricted:=True)> _
    Public Class LiveSiteMapProvider
        Inherits StaticSiteMapProvider
        Private Const _errmsg1 As String = "Missing node ID"
        Private Const _errmsg2 As String = "Duplicate node ID"
        Private Const _errmsg3 As String = "Missing parent ID"
        Private Const _errmsg4 As String = "Invalid parent ID"
        Private Const _errmsg5 As String = "Empty or missing connectionStringName"
        Private Const _errmsg6 As String = "Missing connection string"
        Private Const _errmsg7 As String = "Empty connection string"
        Private Const _errmsg8 As String = "Invalid sqlCacheDependency"
        Private Const _cacheDependencyName As String = "__SiteMapCacheDependency"
        Private ReadOnly _lock As New Object()
        Private _connect As String
        ' Database connection string
        Private _database As String, _table As String
        ' Database info for SQL Server 7/2000 cache dependency
        ' Database info for SQL Server 2005 cache dependency
        Private SiteMapRootNode As SiteMapNode
        Private _indexID As Integer, _indexTitle As Integer, _indexUrl As Integer, _indexDesc As Integer, _indexRoles As Integer, _indexParent As Integer
        Public Overloads Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
            ' Verify that config isn't null
            If config Is Nothing Then
                Throw New ArgumentNullException("config")
            End If

            ' Assign the provider a default name if it doesn't have one
            If [String].IsNullOrEmpty(name) Then
                name = "LiveSiteMapProvider"
            End If

            ' Add a default "description" attribute to config if the
            ' attribute doesn’t exist or is empty
            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", "SQL site map provider")
            End If

            ' Call the base class's Initialize method
            MyBase.Initialize(name, config)

            ' Initialize _connect
            Dim connect As String = config("connectionStringName")
            If [String].IsNullOrEmpty(connect) Then
                Throw New ProviderException(_errmsg5)
            End If
            config.Remove("connectionStringName")

            If WebConfigurationManager.ConnectionStrings(connect) Is Nothing Then
                Throw New ProviderException(_errmsg6)
            End If

            _connect = WebConfigurationManager.ConnectionStrings(connect).ConnectionString
            If [String].IsNullOrEmpty(_connect) Then
                Throw New ProviderException(_errmsg7)
            End If

            ' Initialize SQL cache dependency info
            Dim dependency As String = config("sqlCacheDependency")
            config.Remove("sqlCacheDependency")

            ' SiteMapProvider processes the securityTrimmingEnabled
            ' attribute but fails to remove it. Remove it now so we can
            ' check for unrecognized configuration attributes.
            If config("securityTrimmingEnabled") IsNot Nothing Then
                config.Remove("securityTrimmingEnabled")
            End If

            ' Throw an exception if unrecognized attributes remain
            If config.Count > 0 Then
                Dim attr As String = config.GetKey(0)
                If Not [String].IsNullOrEmpty(attr) Then
                    Throw New ProviderException("Unrecognized attribute: " + attr)
                End If
            End If
        End Sub
        ''' <summary>
        ''' Implementation of BuildSiteMap function of SiteMap class
        ''' </summary>
        ''' <remarks></remarks>
        Public Overloads Overrides Function BuildSiteMap() As SiteMapNode
            If System.Web.HttpContext.Current.Session("AccountEmployeeId") Is Nothing Then
                Return Nothing
            End If
            SyncLock _lock
                If Not Me.SiteMapRootNode Is Nothing Then
                    Return SiteMapRootNode
                End If
                Dim AllNodes As New Dictionary(Of Integer, SiteMapNode)(16)
                Dim objPagePermission As New BOPagePermission
                Dim dtPermissions As DataTable
                Try
                    'Dim aRoles() As String = System.Web.Security.Roles.Provider.GetRolesForUser(System.Web.HttpContext.Current.Session("UserName"))
                    dtPermissions = objPagePermission.GetAccountPageSecurityOfRoles(ATS.BO.UIUtilities.GetCurrentLanguage())
                    Me.Clear()
                    ' Create site map node of every page of TimeLive
                    For Each objPermission As DataRow In dtPermissions.Rows
                        If SiteMapRootNode Is Nothing Then
                            SiteMapRootNode = CreateRootSiteMapNode(AllNodes, "")
                            AddNode(SiteMapRootNode, Nothing)
                        End If
                        If IsDBNull(objPermission("ControlLevelPermission")) OrElse objPermission("ControlLevelPermission") = False Then
                            ' Create another site map node and add it to the site map
                            Dim node As SiteMapNode = CreateSiteMapNodeFromDataReader(objPermission, AllNodes)
                            Dim parentNode As SiteMapNode = Nothing

                            If Not node Is Nothing Then
                                parentNode = GetAndAddParentNodeFromDataReader(objPermission, AllNodes, RootNode)
                                AddNode(node, parentNode)
                            End If
                        End If
                    Next
                Catch ex As Exception
                    'Throw ex
                End Try
                'System.Web.HttpContext.Current.Session(key) = SiteMapRootNode
                Return SiteMapRootNode
            End SyncLock
        End Function

        Public Function GetFullPageName(ByVal strFolderName As String, ByVal strPageName As String)
            Return "~/" & strFolderName & IIf(strFolderName <> "", "/", "") & strPageName
        End Function

        Protected Overloads Overrides Function GetRootNodeCore() As SiteMapNode
            If System.Web.HttpContext.Current.Session("AccountEmployeeId") Is Nothing Then
                Return Nothing
            End If
            SyncLock _lock
                Dim key As String = "RootNode" & System.Web.HttpContext.Current.Session("AccountEmployeeId")
                Return BuildSiteMap()
                'Return System.Web.HttpContext.Current.Session(key)
            End SyncLock
        End Function

        Private Function CreateRootSiteMapNode(ByVal AllNodes As Dictionary(Of Integer, SiteMapNode), ByVal Role As String) As SiteMapNode
            ' Get the node ID from the DataReader
            Dim id As Integer = 25
            ' Make sure the node ID is unique
            If AllNodes.ContainsKey(id) Then
                Return Nothing
            End If

            ' Get title, URL, description, and roles from the DataReader
            Dim title As String = "ÇáÑÆíÓíÉ"
            Dim url As String = "~/Employee/Default.aspx"
            Dim description As String = "ÇáÑÆíÓíÉ"
            Dim roles As String = Role

            ' If roles were specified, turn the list into a string array
            Dim rolelist As String() = Nothing
            If Not [String].IsNullOrEmpty(roles) Then
                rolelist = roles.Split(New Char() {","c, ";"c}, 512)
            End If

            ' Create a SiteMapNode
            Dim node As New SiteMapNode(Me, id.ToString(), url, title, description, rolelist, _
             Nothing, Nothing, Nothing)
            ' Record the node in the _nodes dictionary
            AllNodes.Add(id, node)
            ' Return the node        
            Return AllNodes(id)
        End Function

        Private Function CreateSiteMapNodeFromDataReader(ByVal Row As DataRow, ByVal AllNodes As Dictionary(Of Integer, SiteMapNode)) As SiteMapNode
            ' Get the node ID from the DataReader
            Dim id As Integer = Row("SystemSecurityCategoryPageId")
            ' Make sure the node ID is unique
            If AllNodes.ContainsKey(id) Then
                Return Nothing
            End If

            ' Get title, URL, description, and roles from the DataReader
            Dim title As String = Row("SystemCategoryPageDescriptionEN")
            Dim url As String = Me.GetFullPageName(Row("Folder"), Row("SystemCategoryPage"))
            Dim description As String = Row("SystemCategoryPageDescription")

            ' If roles were specified, turn the list into a string array
            Dim rolelist As String() = Nothing

            ' Create a SiteMapNode
            Dim node As New SiteMapNode(Me, id.ToString(), url, title, description, rolelist, _
             Nothing, Nothing, Nothing)

            ' Record the node in the _nodes dictionary
            AllNodes.Add(id, node)

            ' Return the node        
            Return AllNodes(id)
        End Function

        Private Function GetAndAddParentNodeFromDataReader(ByVal Row As DataRow, ByVal AllNodes As Dictionary(Of Integer, SiteMapNode), ByVal ParentNode As SiteMapNode) As SiteMapNode
            ' Make sure the parent ID is present
            If IsDBNull(Row("ParentSystemSecurityCateogoryPageId")) Then
                Throw New ProviderException(_errmsg3)
            End If

            ' Get the parent ID from the DataReader
            Dim pid As Integer = CInt(Row("ParentSystemSecurityCateogoryPageId"))
            ' Make sure the node ID is unique
            If AllNodes.ContainsKey(pid) Then
                Return AllNodes(pid)
            End If

            ' Add parent node in sitemap if its permissions are exist.
            If Not Me.IsDependentChildPage(Row("SystemSecurityCategoryPageId")) Then
                Dim title As String = Row("ParentSystemCategoryPageDescription")
                Dim url As String = Me.GetFullPageName(Row("ParentFolder"), Row("ParentSystemCategoryPage"))
                Dim description As String = Row("ParentSystemCategoryPageDescription")
                'Dim roles As String = Row.Roles '"Administrator"
                Dim rolelist As String() = Nothing
                'If Not [String].IsNullOrEmpty(roles) Then
                'rolelist = Roles.Split(New Char() {","c, ";"c}, 512)
                'End If

                Dim node As New SiteMapNode(Me, pid.ToString(), url, title, description, rolelist, _
                 Nothing, Nothing, Nothing)

                ' Make sure the parent ID is valid
                If Not AllNodes.ContainsKey(pid) Then
                    AllNodes.Add(pid, node)
                End If

                Dim objPagePermission As New BOPagePermission
                Dim dtParentPermissions As DataTable
                dtParentPermissions = objPagePermission.GetAccountPageSecurityByAccountIdAndSystemSecurityCategoryPageId(pid)
                Dim drParentPermissions As DataRow

                'Dim NewParentNode As SiteMapNode
                If dtParentPermissions.Rows.Count > 0 Then
                    drParentPermissions = dtParentPermissions.Rows(0)
                    ParentNode = Me.GetAndAddParentNodeFromDataReader(drParentPermissions, AllNodes, ParentNode)
                End If
                AddNode(node, ParentNode)
                ' Return the parent SiteMapNode
                Return AllNodes(pid)
            End If

        End Function
        ''' <summary>
        ''' Return true if provided page is a dependent child
        ''' </summary>
        ''' <remarks></remarks>
        Public Function IsDependentChildPage(ByVal PageId As Integer) As Boolean
            If PageId = 108 Then Return True
            If PageId = 96 Then Return True
            If PageId = 97 Then Return True
            If PageId = 95 Then Return True
            If PageId = 24 Then Return True
            If PageId = 21 Then Return True
            If PageId = 35 Then Return True
            If PageId = 95 Then Return True
            If PageId = 32 Then Return True
            If PageId = 37 Then Return True
            If PageId = 86 Then Return True
            If PageId = 88 Then Return True
            If PageId = 99 Then Return True
            If PageId = 100 Then Return True
        End Function
        ''' <summary>
        ''' Returns True if supplied page id is allowed to current user
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Function IsAccessibleToUser(ByVal context As HttpContext, ByVal node As SiteMapNode) As Boolean
            If node.Key = "70" Then
                Return True
            End If
            If BOPagePermission.IspageHasRights(node.Key, True) Then
                Return True
            End If

            Return False
        End Function
    End Class
End Namespace