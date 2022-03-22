#Region " Imports "
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports ATS.DA.Framework
Imports ATS.DA.Framework.DataHelper
#End Region

Namespace Framework
    Public Class DARoleUserAccount
        Inherits DataAccessBase(Of RoleUserAccount)

#Region " Overrides Methods "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpRoleUserAccount_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleUserAccountId", SqlDbType.Int).Value = FieldToint32(Id)

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        If CInt(cmd.Parameters("@RC").Value) = 0 Then
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            MyBase.FieldInError = FieldToString(cmd.Parameters("@FieldInError").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function

        Public Overrides Function Find(ByVal Id As Integer) As RoleUserAccount
            Dim eRoleUserAccount As New RoleUserAccount

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "RoleUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.RoleUserAccount ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE RoleId = " & Id.ToString, Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With eRoleUserAccount
                                .RoleUserAccountId = FieldToint32(oReader, 0)
                                .RoleId = FieldToint32(oReader, 1)
                                .UserAccountId = FieldToint32(oReader, 2)

                                .AddedUserAccountId = FieldToint32(oReader, 3)
                                .UpdatedUserAccountId = FieldToint32(oReader, 4)
                                .AddedDate = FieldToDateTime(oReader, 5)
                                .UpdatedDate = FieldToDateTime(oReader, 6)
                                .VersionNo = FieldToByte(oReader, 7)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return eRoleUserAccount
        End Function

        Public Overrides Function GetDataset(ByVal DataEntity As RoleUserAccount, Optional ByVal PageNo As Integer = 1, Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function

        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of RoleUserAccount)
            Throw New NotImplementedException
        End Function

        Public Overloads Overrides Function Load(ByVal DataEntity As RoleUserAccount, Optional ByVal PageNo As Integer = 1, Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of RoleUserAccount)
            Throw New NotImplementedException
        End Function
#End Region

#Region " Public Methods "
        Public Function GetRoleUsersDataset(
        ByVal RoleId As Integer,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpCommon_GetDataPage")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        Dim strTableName As String = " Users.UserAccount INNER JOIN Users.RoleUserAccount " &
                                                     " ON Users.UserAccount.UserAccountId = Users.RoleUserAccount.UserAccountId"
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = strTableName
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = "Users.RoleUserAccount.RoleUserAccountId, Users.RoleUserAccount.RoleId, Users.RoleUserAccount.UserAccountId, Users.RoleUserAccount.VersionNo, Users.UserAccount.UserName "
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = "Users.RoleUserAccount.RoleUserAccountId "
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = "Users.RoleUserAccount.RoleId = " & RoleId.ToString
                            .Add("@Group", SqlDbType.NVarChar, -1).Value = String.Empty
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Role")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If MyBase.RowsEffected > 0 Then
                MyBase.GetPagesTotal("Users.RoleUserAccount", "RoleId = " & RoleId.ToString, PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return MyBase.Dataset
        End Function
        Public Function DeleteALL(ByVal RoleId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpRoleUserAccount_DeleteALL")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleId", SqlDbType.Int).Value = FieldToint32(RoleId)

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        If CInt(cmd.Parameters("@RC").Value) = 0 Then
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            MyBase.FieldInError = FieldToString(cmd.Parameters("@FieldInError").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function
        Public Function Add(
        ByVal RoleId As Integer,
        ByVal UserAccountId As Integer,
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpRoleUserAccount_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleId", SqlDbType.Int).Value = Int32ToField(RoleId)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
                            .Add("@AddedUserAccountId", SqlDbType.Int).Value = Int32ToField(AddedUserAccountId)

                            .Add(New SqlParameter("@RoleUserAccountId", SqlDbType.Int))
                            .Item("@RoleUserAccountId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        If CInt(cmd.Parameters("@RC").Value) = 0 Then
                            MyBase.Identity = FieldToint32(cmd.Parameters("@RoleUserAccountId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            MyBase.FieldInError = FieldToString(cmd.Parameters("@FieldInError").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function
        Public Function LoadRoleUsers( _
        ByVal RoleId As Integer) _
        As System.Collections.Generic.List(Of RoleUserAccount)
            Dim List As New List(Of RoleUserAccount)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.RoleUserAccount ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE RoleId = " & RoleId.ToString, Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = oCon

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim eRoleUserAccount As RoleUserAccount
                        While oReader.Read
                            eRoleUserAccount = New RoleUserAccount
                            With eRoleUserAccount
                                .RoleUserAccountId = FieldToint32(oReader, 0)
                                .RoleId = FieldToint32(oReader, 1)
                                .UserAccountId = FieldToint32(oReader, 2)

                                .AddedUserAccountId = FieldToint32(oReader, 3)
                                .UpdatedUserAccountId = FieldToint32(oReader, 4)
                                .AddedDate = FieldToDateTime(oReader, 5)
                                .UpdatedDate = FieldToDateTime(oReader, 6)
                                .VersionNo = FieldToByte(oReader, 7)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                                .UserName = GetUserName(.UserAccountId)
                            End With
                            List.Add(eRoleUserAccount)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Public Function AddAllUserAccountsToRole( _
        ByVal RoleId As Integer, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpRoleUserAccount_InsertALL")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleId", SqlDbType.Int).Value = Int32ToField(RoleId)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        If CInt(cmd.Parameters("@RC").Value) = 0 Then
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            MyBase.FieldInError = FieldToString(cmd.Parameters("@FieldInError").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function
#End Region

    End Class
End Namespace

