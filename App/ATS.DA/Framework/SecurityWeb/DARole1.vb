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

    Public Class DARole1
        Inherits DataAccessBase(Of Role1)



#Region " Constructors "
        Public Sub New()
            MyBase.New()
        End Sub
#End Region

#Region " Overrides Methods "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Security.SpRole_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleId", SqlDbType.Int).Value = FieldToint32(Id)

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
        Public Overrides Function GetDataset( _
        ByVal DataEntity As Role1, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpGetDataRows")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Security.Role"
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = SqlSelect(DataEntity)
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = SqlOrderBy(DataEntity)
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = SqlWhere(DataEntity)
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
                MyBase.GetPagesTotal("Security.Role", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return MyBase.Dataset
        End Function
        Public Function GetUserRolesDataset(ByVal UserAccountId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Security.SpRole_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        'With cmd.Parameters
                        '    .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
                        'End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Roles")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Overloads Overrides Function Load( _
        ByVal DataEntity As Role1, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of Role1)
            Dim List As New List(Of Role1)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpGetDataRows")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Security.Role"
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = SqlSelect(DataEntity)
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = SqlOrderBy(DataEntity)
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = SqlWhere(DataEntity)
                            .Add("@Group", SqlDbType.NVarChar, -1).Value = String.Empty
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim oRole As Role1
                        While oReader.Read
                            oRole = New Role1
                            With oRole
                                .RoleId = FieldToint32(oReader, 1)
                                .RoleName = FieldToString(oReader, 2)
                                .LDAPRole = FieldToString(oReader, 3)

                                .AddedUserAccountId = FieldToint32(oReader, 4)
                                .UpdatedUserAccountId = FieldToint32(oReader, 5)
                                .AddedDate = FieldToDateTime(oReader, 6)
                                .UpdatedDate = FieldToDateTime(oReader, 7)
                                .VersionNo = FieldToByte(oReader, 8)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(oRole)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If List.Count > 0 Then
                MyBase.GetPagesTotal("Security.Role", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return List
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As Role1
            Dim oRole As New Role1

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "LDAPRole, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsApprover, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsMasterApprover, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Security.Role ", Environment.NewLine)
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
                            With oRole
                                .RoleId = FieldToint32(oReader, 0)
                                .RoleName = FieldToString(oReader, 1)
                                .LDAPRole = FieldToString(oReader, 2)
                                .IsApprover = FieldToBoolean(oReader, 3)
                                .IsMasterApprover = FieldToBoolean(oReader, 4)
                                .AddedUserAccountId = FieldToint32(oReader, 5)
                                .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                .AddedDate = FieldToDateTime(oReader, 7)
                                .UpdatedDate = FieldToDateTime(oReader, 8)
                                .VersionNo = FieldToByte(oReader, 9)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return oRole
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As Role1) As String
            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "LDAPRole, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsProtected, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)

            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As Role1) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.WhereFilter
                Case Role1.FilterWhere.PrimaryKey
                    sb.AppendFormat("{0}{1}", "RoleId = " & Entity.RoleId.ToString, Environment.NewLine)
            End Select

            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As Role1) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.OrderByFilter
                Case Role1.FilterOrderBy.None
                    sb.AppendFormat("{0}{1}", "RoleId ", Environment.NewLine)

            End Select

            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "

        Public Function Add(ByVal RoleName As String,
                            ByVal LDAPRole As String,
                            ByVal IsApprover As Boolean,
                            ByVal IsMasterApprover As Boolean,
                            ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Security.SpRole_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleName", SqlDbType.NVarChar, 50).Value = StringToField(RoleName)
                            .Add("@LDAPRole", SqlDbType.NVarChar, 50).Value = StringToField(LDAPRole)
                            .Add("@IsApprover", SqlDbType.Bit).Value = BooleanToField(IsApprover)
                            .Add("@IsMasterApprover", SqlDbType.Bit).Value = BooleanToField(IsMasterApprover)

                            .Add("@AddedUserAccountId", SqlDbType.Int).Value = Int32ToField(AddedUserAccountId)

                            .Add(New SqlParameter("@RoleId", SqlDbType.Int))
                            .Item("@RoleId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@RoleId").Value)
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

        Public Function Update(ByVal RoleId As Integer, _
                                ByVal RoleName As String, _
                                ByVal LDAPRole As String, _
                                ByVal IsApprover As Boolean,
                                ByVal IsMasterApprover As Boolean,
                                ByVal UpdatedUserAccountId As Integer, _
                                ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Security.SpRole_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleId", SqlDbType.Int).Value = Int32ToField(RoleId)
                            .Add("@RoleName", SqlDbType.NVarChar, 50).Value = StringToField(RoleName)
                            .Add("@LDAPRole", SqlDbType.NVarChar, 50).Value = StringToField(LDAPRole)
                            .Add("@IsApprover", SqlDbType.Bit).Value = BooleanToField(IsApprover)
                            .Add("@IsMasterApprover", SqlDbType.Bit).Value = BooleanToField(IsMasterApprover)
                            .Add("@UpdatedUserAccountId", SqlDbType.Int).Value = Int32ToField(UpdatedUserAccountId)
                            .Add("@VersionNo", SqlDbType.Timestamp).Value = VersionNo

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

        Public Function LoadByUserAccountId(ByVal UserAccountId As Integer) _
        As System.Collections.Generic.List(Of Role1)
            Dim List As New List(Of Role1)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Role.RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Role.RoleName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Role.LDAPRole, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Role.AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Role.AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Role.UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Role.UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Role.VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Security.Role ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "INNER JOIN Employees.Employee ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "ON Role.RoleId = Employees.Employee.RoleId ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE EmployeeId = " & UserAccountId.ToString, Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim eRole As Role1
                        While oReader.Read
                            eRole = New Role1
                            With eRole
                                .RoleId = FieldToint32(oReader, 0)
                                .RoleName = FieldToString(oReader, 1)
                                .LDAPRole = FieldToString(oReader, 2)

                                .AddedDate = FieldToDateTime(oReader, 3)
                                .AddedUserAccountId = FieldToint32(oReader, 4)
                                .UpdatedUserAccountId = FieldToint32(oReader, 5)
                                .UpdatedDate = FieldToDateTime(oReader, 6)
                                .VersionNo = FieldToByte(oReader, 7)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(eRole)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function

        Public Overloads Overrides Function Load() _
        As System.Collections.Generic.List(Of Role1)
            Dim List As New List(Of Role1)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "LDAPRole, ", Environment.NewLine)

            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Security.Role ", Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim oRole As Role1
                        While oReader.Read
                            oRole = New Role1
                            With oRole
                                .RoleId = FieldToint32(oReader, 0)
                                .RoleName = FieldToString(oReader, 1)
                                .LDAPRole = FieldToString(oReader, 2)

                                .AddedUserAccountId = FieldToint32(oReader, 3)
                                .UpdatedUserAccountId = FieldToint32(oReader, 4)
                                .AddedDate = FieldToDateTime(oReader, 5)
                                .UpdatedDate = FieldToDateTime(oReader, 6)
                                .VersionNo = FieldToByte(oReader, 7)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(oRole)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function

        Public Function GetList() As System.Data.DataSet

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleName ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Security.Role ", Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Role")
                        Adapter.Dispose()
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "Retrive List Failed"
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetDefaultPage(ByVal RoleId As Integer) As System.Data.DataSet


            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Security.SpGetDefaultPageByRole", oCon)
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Page")
                        Adapter.Dispose()
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "Retrive List Failed"
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetApprovalRoles() As System.Data.DataSet

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleName ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Security.Role WHERE IsApprover=1 ", Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Role")
                        Adapter.Dispose()
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "Retrive List Failed"
            End Try

            Return MyBase.Dataset
        End Function

#End Region

      
    End Class
End Namespace

