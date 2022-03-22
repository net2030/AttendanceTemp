Imports System.DirectoryServices
Imports Microsoft.VisualBasic
Imports System.Web.Security

Public Class LDAPUtilitiesBLL
    Public Shared Function root() As DirectoryEntry
        Return New DirectoryEntry(System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString, UIUtilities.GetADConnectionUsername, UIUtilities.GetADConnectionPassword, AuthenticationTypes.None)
    End Function
    Public Shared Function OpenLDAPRoot() As DirectoryEntry
        Return New DirectoryEntry(System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString, Nothing, Nothing, AuthenticationTypes.None)
    End Function
    Public Function IsValidUserNameAndPassword(ByVal UserName As String, ByVal Password As String) As Boolean
        If Membership.ValidateUser(UserName, Password) = True Then
            'If Not Membership.GetUser(UserName) Is Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsMemberOfTimeLiveAdministrators() As Boolean
        Return True
    End Function
    Public Function IsAspNetActiveDirectoryMembershipProvider() As Boolean
        If Membership.Provider.Name = "AspNetActiveDirectoryMembershipProvider" Or Membership.Provider.Name = "LiveLDAPMembershipProvider" Or Membership.Provider.Name = "OpenLDAPMembershipProvider" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function IsLDAPMembershipProvider() As Boolean
        If Membership.Provider.Name = "LiveLDAPMembershipProvider" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function IsOpenLDAPMembershipProvider() As Boolean
        If Membership.Provider.Name = "OpenLDAPMembershipProvider" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function IsUserTimeLiveAdministrator(ByVal UserName As String) As Boolean
        Return LDAPUtilitiesBLL.IsUserIsMemberOfGroup("TimeLiveAdministrator", UserName)
    End Function
    'Public Shared Function CheckAllRolesInADGroup(ByVal Username As String, ByVal AccountId As Integer) As Integer
    '    Dim dtRoles As TimeLiveDataSet.AccountRoleDataTable = New AccountRoleBLL().GetAccountRoleByAccountId(AccountId)
    '    Dim drRole As TimeLiveDataSet.AccountRoleRow
    '    For Each drRole In dtRoles.Rows
    '        If Not IsDBNull(drRole("LDAPRole")) Then
    '            If LDAPUtilitiesBLL.IsUserIsMemberOfGroup(drRole.LDAPRole, Username) Then
    '                Return drRole.AccountRoleId
    '            End If
    '        End If
    '    Next
    'End Function
    Public Shared Function IsUserIsMemberOfGroup(ByVal GroupName As String, ByVal Username As String) As Boolean
        Dim GroupClassSearchString = LDAPUtilitiesBLL.GetSearchByNameFormat(GroupName)
        Dim srch As DirectorySearcher
        Dim StartNumber As Integer = 0
        Dim EndNumber As Integer = 1000
        Dim AttributeSearchString As String = ""
        Dim LastResult As Boolean = False
        While True
            AttributeSearchString = String.Format("member;range={0}-{1}", StartNumber, EndNumber)

            srch = New DirectorySearcher(LDAPUtilitiesBLL.root)
            srch.PropertiesToLoad.Add(AttributeSearchString)
            srch.Filter = GroupClassSearchString
            Dim sResult As SearchResultCollection = srch.FindAll()
            If sResult.Count = 0 Then
                Return False
            End If
            If sResult(0).Properties(AttributeSearchString).Count = 0 Then
                LastResult = True
                srch = New DirectorySearcher(LDAPUtilitiesBLL.root)
                AttributeSearchString = String.Format("member;range={0}-*", StartNumber)

                srch.PropertiesToLoad.Add(AttributeSearchString)
                srch.Filter = GroupClassSearchString
                sResult = srch.FindAll()
            End If
            If sResult IsNot Nothing Then
                For i As Integer = 0 To sResult.Count - 1
                    If IsGroupObject(sResult(i).GetDirectoryEntry) And IsUserIsExistInMemberProperty(sResult(i), Username, AttributeSearchString) Then
                        Return True
                    End If
                Next
            End If
            If LastResult Then
                Exit While
            End If
            StartNumber = StartNumber + 1000
            EndNumber = EndNumber + 1000

        End While
    End Function
    Public Shared Function IsUserIsExistInMemberProperty(ByVal objSearchResult As SearchResult, ByVal UserName As String, ByVal AttributeName As String) As Boolean
        Dim UserNameSearchStringWithComma = (LDAPUtilitiesBLL.GetSearchByNameFormatForLDAP(UserName) & ",").ToLower
        Dim UserNameSearchStringWithoutComma = (LDAPUtilitiesBLL.GetSearchByNameFormatForLDAP(UserName) & " ").ToLower

        For n As Integer = 0 To objSearchResult.Properties(AttributeName).Count - 1
            If LDAPUtilitiesBLL.GetDirectoryEntryFromDistinguishName(objSearchResult.Properties(AttributeName).Item(n).ToString).Properties(LDAPUtilitiesBLL.GetSearhFormatAttribute).Item(0).ToString.ToLower = UserName.ToLower Then
                Return True
            End If
        Next
    End Function
    Public Shared Function IsGroupObject(ByVal objDirectoryEntry As DirectoryEntry) As Boolean
        If objDirectoryEntry.SchemaClassName.ToLower.Contains("group") Then
            Return True
        End If
    End Function
    Public Shared Function GetDirectoryEntryFromDistinguishName(ByVal DistinguishedName As String) As SearchResult
        Dim srch As New DirectorySearcher(LDAPUtilitiesBLL.root)
        Dim query As String = "(&(distinguishedName=" + DistinguishedName + " ))"
        srch.Filter = query
        srch.PropertiesToLoad.Add("userPrincipalName")
        srch.PropertiesToLoad.Add("sAMAccountName")
        Dim sResult As SearchResultCollection = srch.FindAll()

        If sResult IsNot Nothing Then
            For i As Integer = 0 To sResult.Count - 1
                Return sResult(i)
            Next
        End If
        Return Nothing
    End Function
    Public Shared Function GetUserGroups(ByVal UserName As String) As Collection
        Dim objDirectoryEntry As DirectoryEntry = LDAPUtilitiesBLL.GetUserByName(UserName)
        Dim col As New Collection
        If Not objDirectoryEntry Is Nothing Then
            For n As Integer = 0 To objDirectoryEntry.Properties.Item("memberof").Count - 1
                Dim path As String = CStr(objDirectoryEntry.Properties.Item("memberof").Item(n))
                Dim groupDirectoryEntry As SearchResult = LDAPUtilitiesBLL.GetDirectoryEntryFromDistinguishName(path)
                col.Add(groupDirectoryEntry)
            Next
            Return col
        End If
        Return Nothing
    End Function
    Public Shared Function GetSearchByNameFormatForLDAP(ByVal SearchValue As String) As String
        Return GetLDAPSearhFormatAttribute() & "=" & SearchValue
    End Function
    Public Shared Function GetSearchByNameFormat(ByVal SearchValue As String) As String
        Return GetSearhFormatAttribute() & "=" & SearchValue
    End Function
    Public Shared Function GetSearhFormatAttribute() As String
        If LDAPUtilitiesBLL.IsLDAPMembershipProvider Then
            Return "cn"
        ElseIf LDAPUtilitiesBLL.IsOpenLDAPMembershipProvider Then
            Return "uid"
        Else
            Return "sAMAccountName"
        End If
    End Function
    Public Shared Function GetLDAPSearhFormatAttribute() As String
        Return "cn"
    End Function
    Public Shared Function GetUserByName(ByVal UserName As String) As DirectoryEntry
        Dim srch As New DirectorySearcher(LDAPUtilitiesBLL.root)
        srch.Filter = GetSearchByNameFormat(UserName)
        If Not LDAPUtilitiesBLL.IsOpenLDAPMembershipProvider Then
            srch.PropertiesToLoad.Add("memberOf")
        End If
        Dim sResult As SearchResultCollection = srch.FindAll()
        If sResult IsNot Nothing Then
            For i As Integer = 0 To sResult.Count - 1
                Return sResult(i).GetDirectoryEntry
            Next
        End If
        Return Nothing
    End Function
    Public Shared Function GetGroupByName(ByVal GroupName As String) As DirectoryEntry
        Dim srch As New DirectorySearcher(LDAPUtilitiesBLL.root)
        srch.Filter = GetSearchByNameFormat(GroupName)
        srch.PropertiesToLoad.Add("members")
        Dim sResult As SearchResultCollection = srch.FindAll()
        If sResult IsNot Nothing Then
            For i As Integer = 0 To sResult.Count - 1
                Return sResult(i).GetDirectoryEntry
            Next
        End If
        Return Nothing
    End Function
    Public Shared Function GetDirectoryEntryFromPath(ByVal path As String) As DirectoryEntry
        Return New DirectoryEntry(path, UIUtilities.GetADConnectionUsername, UIUtilities.GetADConnectionPassword, UIUtilities.GetADAuthenticationType)
    End Function
End Class
