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
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DACapability
        Inherits DataAccessBase(Of Capability)

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
                    Dim cmd As New SqlCommand("Users.SpCapability_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@CapabilityId", SqlDbType.Int).Value = FieldToint32(Id)

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
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As Capability, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpCommon_GetDataPage")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Users.Capability"
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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Capability")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If MyBase.RowsEffected > 0 Then
                MyBase.GetPagesTotal("Users.Capability", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return MyBase.Dataset
        End Function
        Public Overloads Overrides Function Load() _
        As System.Collections.Generic.List(Of Capability)
            Dim List As New List(Of Capability)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "CapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "CapabilityName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MenuItemId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AccessType, ", Environment.NewLine)

            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.Capability ", Environment.NewLine)

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
                        Dim eCapability As Capability
                        While oReader.Read
                            eCapability = New Capability
                            With eCapability
                                .CapabilityId = FieldToint32(oReader, 0)
                                .CapabilityName = FieldToString(oReader, 1)
                                .MenuItemId = FieldToint32(oReader, 2)
                                .AccessType = CType(FieldToint32(oReader, 3), Capability.AccessTypeEnum)

                                .AddedUserAccountId = FieldToint32(oReader, 4)
                                .UpdatedUserAccountId = FieldToint32(oReader, 5)
                                .AddedDate = FieldToDateTime(oReader, 6)
                                .UpdatedDate = FieldToDateTime(oReader, 7)
                                .VersionNo = FieldToByte(oReader, 8)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(eCapability)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As Capability, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of Capability)
            Dim List As New List(Of Capability)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpCommon_GetDataPage")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Users.Capability"
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = SqlSelect(DataEntity)
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = SqlOrderBy(DataEntity)
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = SqlWhere(DataEntity)
                            .Add("@Group", SqlDbType.NVarChar, -1).Value = String.Empty
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim oCapability As Capability
                        While oReader.Read
                            oCapability = New Capability
                            With oCapability
                                .CapabilityId = FieldToint32(oReader, 1)
                                .CapabilityName = FieldToString(oReader, 2)
                                .MenuItemId = FieldToint32(oReader, 3)
                                .AccessType = CType(FieldToint32(oReader, 4), Capability.AccessTypeEnum)

                                .AddedUserAccountId = FieldToint32(oReader, 5)
                                .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                .AddedDate = FieldToDateTime(oReader, 7)
                                .UpdatedDate = FieldToDateTime(oReader, 8)
                                .VersionNo = FieldToByte(oReader, 9)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(oCapability)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If List.Count > 0 Then
                MyBase.GetPagesTotal("Users.Capability", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return List
        End Function



        Public Overrides Function Find(ByVal Id As Integer) As Capability
            Dim oCapability As New Capability

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "CapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "CapabilityName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MenuItemId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AccessType, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.Capability ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE CapabilityId = " & Id.ToString, Environment.NewLine)

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
                            With oCapability
                                .CapabilityId = FieldToint32(oReader, 0)
                                .CapabilityName = FieldToString(oReader, 1)
                                .MenuItemId = FieldToint32(oReader, 2)
                                .AccessType = CType(FieldToint32(oReader, 3), Capability.AccessTypeEnum)

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

            Return oCapability
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As Capability) As String
            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "CapabilityId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "CapabilityName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MenuItemId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AccessType, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As Capability) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.WhereFilter
                Case Capability.FilterWhere.PrimaryKey
                    sb.AppendFormat("{0}{1}", "CapabilityId = " & Entity.CapabilityId.ToString, Environment.NewLine)
            End Select

            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As Capability) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.OrderByFilter
                Case Capability.FilterOrderBy.None
            End Select

            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="CapabilityName"></param>
        ''' <param name="MenuItemId"></param>
        ''' <param name="AccessType"></param>
        ''' <param name="AddedUserAccountId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Add( _
        ByVal CapabilityName As String, _
        ByVal MenuItemId As Integer, _
        ByVal AccessType As Integer, _
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpCapability_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@CapabilityName", SqlDbType.VarChar, 50).Value = StringToField(CapabilityName)
                            .Add("@MenuItemId", SqlDbType.Int).Value = Int32ToField(MenuItemId)
                            .Add("@AccessType", SqlDbType.Int).Value = Int32ToField(AccessType)
                            .Add("@AddedUserAccountId", SqlDbType.Int).Value = Int32ToField(AddedUserAccountId)

                            .Add(New SqlParameter("@CapabilityId", SqlDbType.Int))
                            .Item("@CapabilityId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@CapabilityId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function

        Public Function Update( _
        ByVal CapabilityId As Integer, _
        ByVal CapabilityName As String, _
        ByVal MenuItemId As Integer, _
        ByVal AccessType As Integer, _
        ByVal UpdatedUserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpCapability_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@CapabilityId", SqlDbType.Int).Value = Int32ToField(CapabilityId)
                            .Add("@CapabilityName", SqlDbType.VarChar, 50).Value = StringToField(CapabilityName)
                            .Add("@MenuItemId", SqlDbType.Int).Value = Int32ToField(MenuItemId)
                            .Add("@AccessType", SqlDbType.Int).Value = Int32ToField(AccessType)
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

