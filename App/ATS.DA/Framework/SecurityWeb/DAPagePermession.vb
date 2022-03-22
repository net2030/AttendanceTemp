Imports System.Configuration
Imports ATS.DA.Framework
Imports System.Data.SqlClient


Public Class DAPagePermession


#Region " Public Methods "

    Public Function SetAccountPagePermission(ByVal AccountId As Integer,
                                              ByVal AccountRoleId As Integer,
                                              ByVal SystemSecurityCategoryPageId As Integer,
                                              ByVal SystemPermissionId As System.Nullable(Of Integer),
                                              ByVal IsDeleted As Boolean,
                                              ByVal UserAccountId As Integer) As Boolean



        Dim boolSeccessed As Boolean = False

        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("Security.SpPermission_Update")
                Using cmd
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon
                    With cmd.Parameters
                        .Add("@AccountRoleId", SqlDbType.NVarChar, 50).Value = AccountRoleId
                        .Add("@SystemSecurityCategoryPageId", SqlDbType.Int).Value = SystemSecurityCategoryPageId
                        .Add("@SystemPermissionId", SqlDbType.Int).Value = SystemPermissionId
                        .Add("@IsDeleted", SqlDbType.Int).Value = IsDeleted

                        .Add("@UserAccountId", SqlDbType.Int).Value = UserAccountId

                        .Add(New SqlParameter("@RC", SqlDbType.Int))
                        .Item("@RC").Direction = ParameterDirection.Output
                        .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                        .Item("@RMsgId").Direction = ParameterDirection.Output
                        .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                        .Item("@RMessage").Direction = ParameterDirection.Output
                        .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                        .Item("@FieldInError").Direction = ParameterDirection.Output
                    End With

                    cmd.ExecuteNonQuery()


                End Using
            End Using

        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try

        Return boolSeccessed

    End Function

    

    Public Function GetList() As System.Data.DataSet
        Dim ds As DataSet = New DataSet
        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("Security.SpPermission_GetAll")
                Using cmd
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon

                    Dim Adapter As New SqlDataAdapter
                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds, "PagePermission")
                    Adapter.Dispose()
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message

        End Try

        Return ds
    End Function

    Public Function GetDataBySystemSecurityCategoryPageId(ByVal PageId As Integer) As System.Data.DataSet
        Dim ds As DataSet = New DataSet
        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("SECURITY.SpGetDefaultPageByPageId")
                Using cmd
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@PageId", SqlDbType.Int).Value = PageId
                    cmd.Connection = oCon

                    Dim Adapter As New SqlDataAdapter
                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds, "PagePermission")
                    Adapter.Dispose()
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message

        End Try

        Return ds
    End Function

    Public Function GetSystemSecurityCategoryPage(Optional ByVal lang As String = "ar") As System.Data.DataSet
        Dim ds As DataSet = New DataSet
        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("SECURITY.SpGetSystemSecurityCategoryPage")
                Using cmd
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@Lang", SqlDbType.NVarChar, 2).Value = lang

                    cmd.Connection = oCon

                    Dim Adapter As New SqlDataAdapter
                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds, "PagePermission")
                    Adapter.Dispose()
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message

        End Try

        Return ds
    End Function

    Public Function GetPageIdByPageName(ByVal strPageName As String, ByVal strFolderName As String) As System.Data.DataSet
        Dim ds As DataSet = New DataSet
        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("SECURITY.SpGetSystemSecurityCategoryPageByName")
                Using cmd
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@PageName", SqlDbType.NVarChar, 100).Value = strPageName
                    cmd.Parameters.Add("@FolderName", SqlDbType.NVarChar, 100).Value = strFolderName
                    cmd.Connection = oCon

                    Dim Adapter As New SqlDataAdapter
                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds, "PagePermission")
                    Adapter.Dispose()
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message

        End Try

        Return ds
    End Function

    Public Function GetAccountPermissionsOfCurrentRoles() As System.Data.DataSet
        Dim ds As DataSet = New DataSet
        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("SECURITY.SpGetSystemSecurityCategoryPage")
                Using cmd
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Connection = oCon

                    Dim Adapter As New SqlDataAdapter
                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds, "PagePermission")
                    Adapter.Dispose()
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message

        End Try

        Return ds
    End Function

    Public Function GetpermissionsTable(ByVal AccountId As Integer, ByVal RoleId As Integer) As DataTable
        Dim ds As DataSet = New DataSet
        Dim dt As DataTable = New DataTable
        Dim sql As String

      

        sql = "select * from Security.vueAccountPagePermission where AccountId =  " & AccountId

        sql = sql + " and AccountroleId = " & RoleId & " and (IsSiteMapPage = 1 or ControlLevelPermission = 1) "

        'sql = sql + " and ((SystemFeatureId IN (SELECT SystemFeatureId FROM AccountFeatures WHERE (AccountId = " & AccountId & "))) or SystemFeatureId is null) ORDER BY SequenceNumber"


        Try
            Dim oCon As SqlConnection = New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand(sql)
                Using cmd
                    'cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon


                    Dim Adapter As New SqlDataAdapter

                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds, "Attendance")
                    dt = ds.Tables(0)
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetSystemSecurityCategoryPageView() As DataTable
        Dim ds As DataSet = New DataSet
        Dim dt As DataTable = New DataTable
        Dim sql As String



        sql = "select * from Security.vueSystemSecurityCategoryPage ORDER BY SystemSecurityCategoryId"

        

        Try
            Dim oCon As SqlConnection = New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand(sql)
                Using cmd
                    'cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon


                    Dim Adapter As New SqlDataAdapter

                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds, "SystemSecurityCategoryPage")
                    dt = ds.Tables(0)
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetDataForAccountPagePermission(ByVal RoleId As Integer, ByVal PageId As Integer) As DataTable
        Dim ds As DataSet = New DataSet
        Dim dt As DataTable = New DataTable
        Dim sql As String



        sql = "select AccountPagePermissionId,AccountRoleId,ShowAllData,SystemPermissionId,SystemSecurityCategoryPageId "
        sql = sql + " from Security.AccountPagePermission Where AccountRoleId=" & RoleId
        sql = sql + " And SystemSecurityCategoryPageId=" & PageId

        Try
            Dim oCon As SqlConnection = New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand(sql)
                Using cmd
                    'cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon


                    Dim Adapter As New SqlDataAdapter

                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds, "SystemSecurityCategoryPage")
                    dt = ds.Tables(0)
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
        Return dt
    End Function
    
#End Region

  
End Class
