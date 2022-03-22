Imports Microsoft.VisualBasic
Imports System.Web.Caching

Public Class CacheManager
    Shared CacheKeys As New ArrayList
    Public Shared Sub AddStaticDataInCache(ByVal ObjectToCache As Object, ByVal Key As String)
        CacheManager.AddInCache(ObjectToCache, Key, CacheItemPriority.NotRemovable, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20))
    End Sub
    Public Shared Sub AddAccountDataInCache(ByVal ObjectToCache As Object, ByVal Key As String)
        CacheManager.AddInCache(ObjectToCache, Key, CacheItemPriority.Normal, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20))
    End Sub
    Public Shared Sub AddAccountEmployeeDataInCache(ByVal ObjectToCache As Object, ByVal Key As String)
        If DBUtilities.IsApplicationContext Then
            CacheManager.AddInCache(ObjectToCache, Key, CacheItemPriority.Normal, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20))
        End If
    End Sub
    Public Shared Sub AddInCache(ByVal ObjectToCache As Object, ByVal Key As String, ByVal Priority As System.Web.Caching.CacheItemPriority, ByVal absoluteExpiration As Date, ByVal slidingExpiration As TimeSpan)
        Try
            If System.Web.HttpContext.Current.Session.Item("CacheKeys") Is Nothing Then
                System.Web.HttpContext.Current.Session.Add("CacheKeys", CacheKeys)
            End If
            System.Web.HttpContext.Current.Session.Add(Key, ObjectToCache)
            GetCacheKey.Add(Key)
        Catch ex As Exception
            Exit Sub
        End Try
        ''System.Web.HttpContext.Current.Cache.Add(Key, ObjectToCache, Nothing, absoluteExpiration, slidingExpiration, Priority, Nothing)
    End Sub
    Public Shared Function GetCacheKey() As ArrayList
        Return CType(System.Web.HttpContext.Current.Session.Item("CacheKeys"), ArrayList)
    End Function
    Public Shared Function GetItemFromCache(ByVal Key As String) As Object
        Try
            Return System.Web.HttpContext.Current.Session(Key)
        Catch ex As Exception
            Exit Function
        End Try

        ''Return System.Web.HttpContext.Current.Cache.Get(Key)
    End Function
    Public Shared Function GetCacheKeyForAccountsData(ByVal strDataTableName As String, ByVal strFunctionName As String, ByVal strParameters As String, Optional ByVal SessionAccountId As Integer = -1) As Object
        Return GetCacheKeyForAccountEmployeeData(strDataTableName, strFunctionName, strParameters, SessionAccountId)
    End Function
    Public Shared Function GetCacheKeyForAccountEmployeeData(ByVal strDataTableName As String, ByVal strFunctionName As String, ByVal strParameters As String, Optional ByVal SessionAccountId As Integer = -1) As Object
        Dim strAccountEmployeeId As String = CStr(DBUtilities.GetSessionAccountEmployeeId)
        Dim strAccountId As String
        If SessionAccountId > 0 Then
            strAccountId = CStr(SessionAccountId)
        Else
            strAccountId = CStr(DBUtilities.GetSessionAccountId)
        End If
        Return strDataTableName & "__" & strFunctionName & "__" & "SessionAccountEmployee=" & strAccountEmployeeId & "__" & "SessionAccount=" & strAccountId & "__" & strParameters
    End Function
    
    Public Shared Sub ClearCache(ByVal TableToDelete As String, Optional ByVal strParameters As String = "No parameter", Optional ByVal IsDeleteByAccountId As Boolean = False, Optional ByVal IsDeleteForAccountEmployeeId As Boolean = False)

        Dim strAccountKey As String = "SessionAccount=" & DBUtilities.GetSessionAccountId
        Dim strAccountEmployeeKey As String = "SessionAccountEmployee=" & DBUtilities.GetSessionAccountEmployeeId
        Dim strCurrentKey As String

        'Dim cacheContents As System.Collections.IEnumerator = System.Web.HttpContext.Current.Session.GetEnumerator



        Dim CollectionKeys As New ArrayList
        For Each strCurrentKey In GetCacheKey()
            '    strCurrentKey = System.Web.HttpContext.Current.Session.Keys(i)
            Dim Keys() As String = Split(strCurrentKey, "__")

            If Keys.Length >= 4 Then
                If Keys(0).IndexOf(TableToDelete) >= 0 And Keys(4).IndexOf(strParameters) >= 0 Then
                    CollectionKeys.Add(strCurrentKey)
                End If
                If IsDeleteByAccountId = True And Keys(0).IndexOf(TableToDelete) >= 0 And Keys(3) = strAccountKey Then
                    CollectionKeys.Add(strCurrentKey)
                End If
                If IsDeleteForAccountEmployeeId = True And Keys(0).IndexOf(TableToDelete) >= 0 And Keys(2) = strAccountEmployeeKey Then
                    CollectionKeys.Add(strCurrentKey)
                End If
            End If
        Next
        For i As Integer = 0 To CollectionKeys.Count - 1
            RemoveFromCache(CStr(CollectionKeys(i)))
        Next
        'While cacheContents.MoveNext
        '    strCurrentKey = cacheContents.Current.ToString()
        '    Dim Keys() As String = Split(strCurrentKey, "__")
        '    If Keys.Length >= 4 Then
        '        If Keys(0).IndexOf(TableToDelete) >= 0 And Keys(4).IndexOf(strParameters) >= 0 Then
        '            System.Web.HttpContext.Current.Session.Remove(strCurrentKey)
        '        End If
        '        If IsDeleteByAccountId = True And Keys(0).IndexOf(TableToDelete) >= 0 And Keys(3) = strAccountKey Then
        '            System.Web.HttpContext.Current.Session.Remove(strCurrentKey)
        '        End If
        '        If IsDeleteForAccountEmployeeId = True And Keys(0).IndexOf(TableToDelete) >= 0 And Keys(2) = strAccountEmployeeKey Then
        '            System.Web.HttpContext.Current.Session.Remove(strCurrentKey)
        '        End If
        '    End If
        'End While
    End Sub
    Public Shared Sub ClearSession(ByVal TableToDelete As String, Optional ByVal strParameters As String = "No parameter", Optional ByVal IsDeleteByAccountId As Boolean = False, Optional ByVal IsDeleteForAccountEmployeeId As Boolean = False)

        Dim strAccountKey As String = "SessionAccount=" & DBUtilities.GetSessionAccountId
        Dim strAccountEmployeeKey As String = "SessionAccountEmployee=" & DBUtilities.GetSessionAccountEmployeeId
        Dim strCurrentKey As String
        Dim cacheContents As System.Collections.IDictionaryEnumerator = CType(System.Web.HttpContext.Current.Session.GetEnumerator, IDictionaryEnumerator)

        While cacheContents.MoveNext
            strCurrentKey = cacheContents.Key.ToString()
            Dim Keys() As String = Split(strCurrentKey, "__")
            If Keys.Length >= 4 Then
                If Keys(0).IndexOf(TableToDelete) >= 0 And Keys(4).IndexOf(strParameters) >= 0 Then
                    System.Web.HttpContext.Current.Cache.Remove(strCurrentKey)
                End If
                If IsDeleteByAccountId = True And Keys(0).IndexOf(TableToDelete) >= 0 And Keys(3) = strAccountKey Then
                    System.Web.HttpContext.Current.Cache.Remove(strCurrentKey)
                End If
                If IsDeleteForAccountEmployeeId = True And Keys(0).IndexOf(TableToDelete) >= 0 And Keys(2) = strAccountEmployeeKey Then
                    System.Web.HttpContext.Current.Cache.Remove(strCurrentKey)
                End If
            End If
        End While
    End Sub
    Public Shared Sub RemoveFromCache(ByVal KeyToDelete As String)
        System.Web.HttpContext.Current.Session.Remove(KeyToDelete)
        If Not GetCacheKey() Is Nothing Then
            GetCacheKey.Remove(KeyToDelete)
        End If
    End Sub
End Class
