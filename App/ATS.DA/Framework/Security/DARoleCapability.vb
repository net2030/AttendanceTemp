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
    Public Class DARoleCapability
        Inherits DataAccessBase(Of RoleCapability)


#Region " Constructors "
        Public Sub New()
            MyBase.new()
        End Sub
#End Region

#Region " Overrides Methods "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpRoleCapability_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleCapabilityId", SqlDbType.Int).Value = FieldToint32(Id)

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
        Public Overrides Function GetDataset(ByVal DataEntity As RoleCapability, Optional ByVal PageNo As Integer = 1, Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Return Nothing
        End Function
        Public Function GetRoleCapabilitiesDataset( _
        ByVal RoleId As Integer, _
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
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Users.RoleCapability "
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = "RoleCapabilityId, RoleId, CapabilityId, AccessFlag, VersionNo "
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = "RoleCapabilityId "
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = "RoleId = " & RoleId.ToString
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
                MyBase.GetPagesTotal("Users.Role", "RoleId = " & RoleId.ToString, PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return MyBase.Dataset
        End Function
        Public Function GetRoleCapabilities(ByVal RoleId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpGetCapabilities")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleId", SqlDbType.NVarChar, -1).Value = FieldToint32(RoleId)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Capabilities")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Overloads Overrides Function Load() _
        As System.Collections.Generic.List(Of RoleCapability)
            Dim List As New List(Of RoleCapability)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleCapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "CapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AccessFlag, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.RoleCapability ", Environment.NewLine)

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
                        Dim eRoleCapability As RoleCapability
                        While oReader.Read
                            eRoleCapability = New RoleCapability
                            With eRoleCapability
                                .RoleCapabilityId = FieldToint32(oReader, 0)
                                .RoleId = FieldToint32(oReader, 1)
                                .CapabilityId = FieldToint32(oReader, 2)
                                .AccessFlag = CType(FieldToBoolean(oReader, 3), RoleCapability.AccessFlagEnum)

                                .AddedUserAccountId = FieldToint32(oReader, 4)
                                .UpdatedUserAccountId = FieldToint32(oReader, 5)
                                .AddedDate = FieldToDateTime(oReader, 6)
                                .UpdatedDate = FieldToDateTime(oReader, 7)
                                .VersionNo = FieldToByte(oReader, 8)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(eRoleCapability)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Public Function LoadByRoleId( _
        ByVal RoleId As Integer) As System.Collections.Generic.List(Of RoleCapability)
            Dim List As New List(Of RoleCapability)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleCapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "CapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AccessFlag, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.RoleCapability ", Environment.NewLine)
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
                        Dim eRoleCapability As RoleCapability
                        While oReader.Read
                            eRoleCapability = New RoleCapability
                            With eRoleCapability
                                .RoleCapabilityId = FieldToint32(oReader, 0)
                                .RoleId = FieldToint32(oReader, 1)
                                .CapabilityId = FieldToint32(oReader, 2)
                                .AccessFlag = CType(FieldToint32(oReader, 3), RoleCapability.AccessFlagEnum)

                                .AddedUserAccountId = FieldToint32(oReader, 4)
                                .UpdatedUserAccountId = FieldToint32(oReader, 5)
                                .AddedDate = FieldToDateTime(oReader, 6)
                                .UpdatedDate = FieldToDateTime(oReader, 7)
                                .VersionNo = FieldToByte(oReader, 8)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(eRoleCapability)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As RoleCapability, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of RoleCapability)
            Dim List As New List(Of RoleCapability)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpGetDataRows")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Users.RoleCapability"
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = SqlSelect(DataEntity)
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = SqlOrderBy(DataEntity)
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = SqlWhere(DataEntity)
                            .Add("@Group", SqlDbType.NVarChar, -1).Value = String.Empty
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim oRoleCapability As RoleCapability
                        While oReader.Read
                            oRoleCapability = New RoleCapability
                            With oRoleCapability
                                .RoleCapabilityId = FieldToint32(oReader, 1)
                                .RoleId = FieldToint32(oReader, 2)
                                .CapabilityId = FieldToint32(oReader, 3)
                                .AccessFlag = CType(FieldToint32(oReader, 4), RoleCapability.AccessFlagEnum)

                                .AddedUserAccountId = FieldToint32(oReader, 5)
                                .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                .AddedDate = FieldToDateTime(oReader, 7)
                                .UpdatedDate = FieldToDateTime(oReader, 8)
                                .VersionNo = FieldToByte(oReader, 9)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(oRoleCapability)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If List.Count > 0 Then
                MyBase.GetPagesTotal("Users.RoleCapability", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return List
        End Function



        Public Overrides Function Find(ByVal Id As Integer) As RoleCapability
            Dim oRoleCapability As New RoleCapability

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleCapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "CapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AccessFlag, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.RoleCapability ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE RoleCapabilityId = " & Id.ToString, Environment.NewLine)

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
                            With oRoleCapability
                                .RoleCapabilityId = FieldToint32(oReader, 0)
                                .RoleId = FieldToint32(oReader, 1)
                                .CapabilityId = FieldToint32(oReader, 2)
                                .AccessFlag = CType(FieldToint32(oReader, 3), RoleCapability.AccessFlagEnum)

                                .AddedUserAccountId = FieldToint32(oReader, 4)
                                .UpdatedUserAccountId = FieldToint32(oReader, 5)
                                .AddedDate = FieldToDateTime(oReader, 6)
                                .UpdatedDate = FieldToDateTime(oReader, 7)
                                .VersionNo = FieldToByte(oReader, 8)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return oRoleCapability
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As RoleCapability) As String
            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "RoleCapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "RoleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "CapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AccessFlag, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As RoleCapability) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.WhereFilter
                Case RoleCapability.FilterWhere.PrimaryKey
                    sb.AppendFormat("{0}{1}", "RoleCapabilityId = " & Entity.RoleCapabilityId.ToString, Environment.NewLine)
            End Select

            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As RoleCapability) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.OrderByFilter
                Case RoleCapability.FilterOrderBy.None
                    sb.AppendFormat("{0}{1}", "RoleCapabilityId ", Environment.NewLine)
            End Select

            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "

        Public Function Add( _
        ByVal RoleId As Integer, _
        ByVal CapabilityId As Integer, _
        ByVal AccessFlag As Integer, _
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpRoleCapability_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleId", SqlDbType.Int).Value = Int32ToField(RoleId)
                            .Add("@CapabilityId", SqlDbType.Int).Value = Int32ToField(CapabilityId)
                            .Add("@AccessFlag", SqlDbType.Int).Value = Int32ToField(AccessFlag)
                            .Add("@AddedUserAccountId", SqlDbType.Int).Value = Int32ToField(AddedUserAccountId)

                            .Add(New SqlParameter("@RoleCapabilityId", SqlDbType.Int))
                            .Item("@RoleCapabilityId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@RoleCapabilityId").Value)
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
        Public Function Update( _
        ByVal RoleCapabilityId As Integer, _
        ByVal RoleId As Integer, _
        ByVal CapabilityId As Integer, _
        ByVal AccessFlag As Integer, _
        ByVal UpdatedUserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpRoleCapability_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@RoleCapabilityId", SqlDbType.Int).Value = Int32ToField(RoleCapabilityId)
                            .Add("@RoleId", SqlDbType.Int).Value = Int32ToField(RoleId)
                            .Add("@CapabilityId", SqlDbType.Int).Value = Int32ToField(CapabilityId)
                            .Add("@AccessFlag", SqlDbType.Int).Value = Int32ToField(AccessFlag)
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
#End Region


    End Class
End Namespace

