Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class BOPagePermission

    Public Shared Function GetDefaultPage() As DataRow

        Dim dt As DataTable = New BORole1().GetDefaultPage(DBUtilities.GetSessionAccountRoleId).Tables(0)
        Dim dr As DataRow
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
            Dim DefaultPageId As Integer = 0

            If Not IsDBNull(dr.Item("DefaultAccountPageId")) Then
                DefaultPageId = CInt(dr.Item("DefaultAccountPageId"))
            End If

            If DefaultPageId = 0 Then
                Return Nothing
            End If

            Return dr
        Else
            Return Nothing
        End If
    End Function

    Public Function GetAccountPageSecurityOfRoles(Optional ByVal lang As String = "ar") As DataTable
        GetAccountPageSecurityOfRoles = New DAPagePermession().GetSystemSecurityCategoryPage(lang).Tables(0)
        'ChangeURLsFromPreferencesUpdates(GetAccountPageSecurityOfRoles)
    End Function

    Public Function GetAccountPageSecurityByAccountIdAndSystemSecurityCategoryPageId(ByVal SystemSecurityCategoryPageId As Integer) As DataTable
        Return New DAPagePermession().GetDataBySystemSecurityCategoryPageId(SystemSecurityCategoryPageId).Tables(0)
    End Function

    'Public Shared Function GetSystemSecurityCategoryPage() As DataTable
    '    Dim ds As DataSet = New DataSet
    '    Dim dt As DataTable = New DataTable
    '    Dim sql As String

    '    sql = "SELECT * FROM View_1"




    '    Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
    '    Try
    '        Dim oCon As SqlConnection = New SqlConnection(constr)
    '        Using oCon
    '            oCon.Open()
    '            Dim cmd As New SqlCommand(sql)
    '            Using cmd
    '                'cmd.CommandType = CommandType.StoredProcedure
    '                cmd.Connection = oCon


    '                Dim Adapter As New SqlDataAdapter

    '                Adapter.SelectCommand = cmd
    '                Adapter.Fill(ds, "SystemSecurityCategoryPage")
    '                dt = ds.Tables(0)
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        Dim strEx As String = ex.Message
    '    End Try
    '    Return dt
    'End Function

    'Public Shared Function GetDataBySystemSecurityCategoryPageId(ByVal SystemSecurityCategoryPageId As Integer) As DataTable
    '    Dim ds As DataSet = New DataSet
    '    Dim dt As DataTable = New DataTable
    '    Dim sql As String

    '    sql = "SELECT * FROM View_1"
    '    sql = sql & " WHERE SystemSecurityCategoryPageId=" & SystemSecurityCategoryPageId




    '    Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
    '    Try
    '        Dim oCon As SqlConnection = New SqlConnection(constr)
    '        Using oCon
    '            oCon.Open()
    '            Dim cmd As New SqlCommand(sql)
    '            Using cmd
    '                'cmd.CommandType = CommandType.StoredProcedure
    '                cmd.Connection = oCon


    '                Dim Adapter As New SqlDataAdapter

    '                Adapter.SelectCommand = cmd
    '                Adapter.Fill(ds, "Attendance")
    '                dt = ds.Tables(0)
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        Dim strEx As String = ex.Message
    '    End Try
    '    Return dt
    'End Function

    Public Shared Function IsPageHasPermissionOf(ByVal SystemSecurityCategoryPageId As Integer, ByVal SystemPermissionId As Integer) As Boolean

        If IsDependentChildPage(SystemSecurityCategoryPageId) Then
            Return True
        End If

        Dim dtPermissions As DataTable = GetAccountPermissionsOfCurrentRoles()

        Dim dr() As DataRow = dtPermissions.Select("SystemSecurityCategoryPageId = " & SystemSecurityCategoryPageId & " and SystemPermissionId = " & SystemPermissionId)

        If dr.Length > 0 Then
            Return True
        End If

    End Function

    Public Shared Function IsDependentChildPage(ByVal PageId As Integer) As Boolean
        If PageId = 108 Then Return True
        If PageId = 95 Then Return True
        If PageId = 24 Then Return True
        If PageId = 21 Then Return True
        If PageId = 35 Then Return True
        If PageId = 95 Then Return True
        'If PageId = 32 Then Return True
        'If PageId = 37 Then Return True
        If PageId = 86 Then Return True
        'If PageId = 88 Then Return True
        If PageId = 99 Then Return True
        If PageId = 100 Then Return True
        If PageId = 126 Then Return True
    End Function

    Public Shared Function GetAccountPermissionsOfCurrentRoles() As DataTable
        If Not System.Web.HttpContext.Current.Session("PermissionTable") Is Nothing Then
            GetAccountPermissionsOfCurrentRoles = CType(System.Web.HttpContext.Current.Session("PermissionTable"), DataTable)
        Else
            Dim RoleId As Integer = CInt(System.Web.HttpContext.Current.Session("AccountRoleId"))
            ' Dim AccountRoles() As String = System.Web.Security.Roles.Provider.GetRolesForUser(CStr(System.Web.HttpContext.Current.Session("UserName")))
            System.Web.HttpContext.Current.Session("PermissionTable") = New DAPagePermession().GetpermissionsTable(DBUtilities.GetSessionAccountId, RoleId)
            GetAccountPermissionsOfCurrentRoles = CType(System.Web.HttpContext.Current.Session("PermissionTable"), DataTable)
        End If
        'ChangeURLsFromPreferencesUpdates(GetAccountPermissionsOfCurrentRoles)
    End Function



    Public Shared Function IsPageHasRightsByPage(ByVal objPage As Page) As Boolean

        If System.Web.HttpContext.Current.Session("PermissionTable") Is Nothing Then
            Return True
        End If

        Dim ThisPage As String = objPage.AppRelativeVirtualPath
        Dim SlashPos As Integer = InStrRev(ThisPage, "/")
        Dim PageName As String = Right(ThisPage, Len(ThisPage) - SlashPos)

        If PageName = "MyReports.aspx" Then
            Return True
        End If

        Dim ThisFolder As String = objPage.AppRelativeTemplateSourceDirectory
        Dim SlashPos1 As String = Right(ThisFolder, Len(ThisFolder) - 2)
        Dim FolderName As String = Left(SlashPos1, Len(SlashPos1) - 1)

        Dim PageId As Integer = GetPageIdByPageName(PageName, FolderName)

        If IsSystemPage(PageId) Then
            Return True
        End If


        Return IspageHasRights(PageId)


    End Function

    Public Shared Function GetPageIdByPageName(ByVal strPageName As String, ByVal strFolderName As String) As Integer
        Try


            Dim dtPermissions As DataTable
            dtPermissions = New DAPagePermession().GetPageIdByPageName(strPageName, strFolderName).Tables(0)

            Dim dr As DataRow = dtPermissions.Rows(0)

            Return CInt(dr.Item("SystemSecurityCategoryPageId"))
        Catch ex As Exception

        End Try

    End Function

    Public Shared Function IsSystemPage(ByVal SystemSecurityCategoryPageId As Integer) As Boolean
        Try


            Dim dtPermissions As DataTable
            dtPermissions = New DAPagePermession().GetDataBySystemSecurityCategoryPageId(SystemSecurityCategoryPageId).Tables(0)
            Dim dr As DataRow = dtPermissions.Rows(0)


            If IsDBNull(dr.Item("IsSystemPage")) Then
                Return False
            ElseIf CBool(dr.Item("IsSystemPage")) = True Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

        End Try
    End Function



    Public Shared Function GetSystemSecurityCategoryPages() As DataTable
        If Not GetItemFromCache("SystemSecurityCategoryPages") Is Nothing Then
            Return CType(CacheManager.GetItemFromCache("SystemSecurityCategoryPages"), DataTable)
        Else
            Dim _SystemSecurityCategoryPageTableAdapter As New DAPagePermession()
            GetSystemSecurityCategoryPages = _SystemSecurityCategoryPageTableAdapter.GetSystemSecurityCategoryPage().Tables(0)
            CacheManager.AddStaticDataInCache(GetSystemSecurityCategoryPages, "SystemSecurityCategoryPages")
            Return GetSystemSecurityCategoryPages
        End If
    End Function

    Public Shared Function IspageHasRights(ByVal SystemSecurityCategoryPageId As Integer, Optional ByVal CheckParent As Boolean = False) As Boolean

        If IsDependentChildPage(SystemSecurityCategoryPageId) Then
            Return True
        End If
        If SystemSecurityCategoryPageId =120 Then

        End If
        Dim dtPermissions As DataTable = GetAccountPermissionsOfCurrentRoles()

        Dim drPermission As DataRow

        If dtPermissions.Rows.Count > 0 Then
            drPermission = dtPermissions.Rows(0)
            If Not dtPermissions Is Nothing Then
                Dim dr() As DataRow = dtPermissions.Select("SystemSecurityCategoryPageId = " & SystemSecurityCategoryPageId)
                If dr.Length > 0 Then
                    Return True
                End If
            End If
            If CheckParent = True Then
                Return IsParentPageHasRights(SystemSecurityCategoryPageId)
            End If
        End If

    End Function

    Public Shared Function IsParentPageHasRights(ByVal ParentSystemSecurityCateogoryPageId As Integer) As Boolean
        Dim dtPermissions As DataTable = GetAccountPermissionsOfCurrentRoles()
        Dim drPermission As DataRow

        If dtPermissions.Rows.Count > 0 Then
            drPermission = dtPermissions.Rows(0)
            If Not dtPermissions Is Nothing Then
                Dim dr() As DataRow = dtPermissions.Select("ParentSystemSecurityCateogoryPageId = " & ParentSystemSecurityCateogoryPageId)
                If IsDependentChildOfRows(dr) Then
                    Return False
                End If
                If dr.Length > 0 Then
                    Return True
                End If
            End If
        End If


    End Function

    Public Shared Function IsDependentChildOfRows(ByVal objRows() As DataRow) As Boolean
        For n As Integer = 0 To objRows.Length - 1
            If IsDependentChild(CInt(objRows(n)("SystemSecurityCategoryPageId"))) Then
                Return True
            End If
        Next
    End Function

    Public Shared Function IsDependentChild(ByVal PageId As Integer) As Boolean
        If PageId = 108 Or PageId = 24 Or PageId = 116 Or PageId = 120 Then
            Return True
        End If
    End Function

    Public Shared Function GetItemFromCache(ByVal Key As String) As Object
        Try
            Return System.Web.HttpContext.Current.Session(Key)
        Catch ex As Exception
            Exit Function
        End Try

        ''Return System.Web.HttpContext.Current.Cache.Get(Key)
    End Function

    Public Shared Function GetSystemSecurityCategoryPageBySystemSecurityCategoryPageId(ByVal SystemSecurityCategoryPageId As Integer) As DataRow
        Return GetSystemSecurityCategoryPages().Select("SystemSecurityCategoryPageId=" & SystemSecurityCategoryPageId)(0)
    End Function

    Public Shared Function GetSystemSecurityCategoryPageView() As DataTable
        Return New DAPagePermession().GetSystemSecurityCategoryPageView()
    End Function

    Public Function GetDataForAccountPagePermission(ByVal RoleId As Integer, ByVal PageId As Integer) As DataTable
        Return New DAPagePermession().GetDataForAccountPagePermission(RoleId, PageId)
    End Function

    Public Shared Function SetPagePermissionForGridView(ByVal SystemSecurityCategoryPageId As Integer, ByVal objGridView As GridView, ByVal EditColumnNo As Integer, ByVal DeleteColumnNo As Integer) As Boolean

        objGridView.Visible = IsPageHasPermissionOf(SystemSecurityCategoryPageId, 1)
        If Not EditColumnNo = Nothing Then
            objGridView.Columns(EditColumnNo).Visible = IsPageHasPermissionOf(SystemSecurityCategoryPageId, 3)
        End If
        If Not DeleteColumnNo = Nothing Then
            objGridView.Columns(DeleteColumnNo).Visible = IsPageHasPermissionOf(SystemSecurityCategoryPageId, 4)
        End If
        Return True
    End Function

    Public Shared Function SetPagePermission(ByVal SystemSecurityCategoryPageId As Integer, ByVal objGridView As GridView, ByVal objFormView As FormView, ByVal AddButtonName As String, ByVal EditColumnNo As Integer, ByVal DeleteColumnNo As Integer) As Boolean

        objGridView.Visible = IsPageHasPermissionOf(SystemSecurityCategoryPageId, 1)

        If objFormView.CurrentMode = FormViewMode.Insert Then
            CType(objFormView.FindControl(AddButtonName), Button).Enabled = IsPageHasPermissionOf(SystemSecurityCategoryPageId, 2)
            'objFormView.Visible = AccountPagePermissionBLL.IsPageHasPermissionOf(SystemSecurityCategoryPageId, 2)
        End If

        objGridView.Columns(EditColumnNo).Visible = IsPageHasPermissionOf(SystemSecurityCategoryPageId, 3)
        objGridView.Columns(DeleteColumnNo).Visible = IsPageHasPermissionOf(SystemSecurityCategoryPageId, 4)

        Return True
    End Function

    Public Shared Function SetPagePermissionForGridViewAndButton(ByVal SystemSecurityCategoryPageId As Integer, ByVal objGridView As GridView, ByVal objButton As Button) As Boolean
        objGridView.Visible = IsPageHasPermissionOf(SystemSecurityCategoryPageId, 1)
        objButton.Visible = IsPageHasPermissionOf(SystemSecurityCategoryPageId, 3)
        Return True
    End Function

    Public Function SetAccountPagePermission(ByVal AccountId As Integer,
                                           ByVal AccountRoleId As Integer,
                                           ByVal SystemSecurityCategoryPageId As Integer,
                                           ByVal SystemPermissionId As System.Nullable(Of Integer),
                                           ByVal IsDeleted As Boolean,
                                           ByVal AddedUserAccountId As Integer) As Boolean
        ' Create a new ProductRow instance


        Dim boolSeccessed As Boolean = False

        Dim oPagePermession As New DAPagePermession
        If oPagePermession.SetAccountPagePermission(AccountId, AccountRoleId, SystemSecurityCategoryPageId, SystemPermissionId, IsDeleted, AddedUserAccountId) Then
            boolSeccessed = True
        Else
            boolSeccessed = False
        End If

        Return boolSeccessed

    End Function
End Class
