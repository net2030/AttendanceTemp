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
    ''' 
    Public Class DAPosition
        Inherits DataAccessBase(Of Position)

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
                    Dim cmd As New SqlCommand("Employees.SpPosition_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@PositionId", SqlDbType.Int).Value = FieldToint32(Id)

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
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As Position, _
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
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Employees.Position"
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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Position")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If MyBase.RowsEffected > 0 Then
                MyBase.GetPagesTotal("Employees.Position", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return MyBase.Dataset
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of Position)
            Dim List As New List(Of Position)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionTypeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "SortIndex, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsOfficer, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Employees.Position ", Environment.NewLine)
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim ePosition As Position
                        While oReader.Read
                            If Not FieldToBoolean(oReader, 0) Then
                                ePosition = New Position
                                With ePosition
                                    .PositionId = FieldToint32(oReader, 0)
                                    .PositionName = FieldToString(oReader, 1)
                                    .PositionTypeId = FieldToint32(oReader, 2)
                                    .SortIndex = FieldToint32(oReader, 3)
                                    .IsOfficer = FieldToBoolean(oReader, 4)

                                    .AddedUserAccountId = FieldToint32(oReader, 5)
                                    .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                    .AddedDate = FieldToDateTime(oReader, 7)
                                    .UpdatedDate = FieldToDateTime(oReader, 8)
                                    .VersionNo = FieldToByte(oReader, 9)
                                    .AddedUserName = GetUserName(.AddedUserAccountId)
                                    .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                                End With
                                List.Add(ePosition)
                            End If
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Public Function LoadByPositionType(ByVal PositionTypeId As Integer) As System.Collections.Generic.List(Of Position)
            Dim List As New List(Of Position)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionTypeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "SortIndex, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsOfficer, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Employees.Position ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE PositionTypeId = " & PositionTypeId.ToString, Environment.NewLine)
            sb.AppendFormat("{0}{1}", "ORDER BY SortIndex ", Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim ePosition As Position
                        While oReader.Read
                            If Not FieldToBoolean(oReader, 0) Then
                                ePosition = New Position
                                With ePosition
                                    .PositionId = FieldToint32(oReader, 0)
                                    .PositionName = FieldToString(oReader, 1)
                                    .PositionTypeId = FieldToint32(oReader, 2)
                                    .SortIndex = FieldToint32(oReader, 3)
                                    .IsOfficer = FieldToBoolean(oReader, 4)

                                    .AddedUserAccountId = FieldToint32(oReader, 5)
                                    .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                    .AddedDate = FieldToDateTime(oReader, 7)
                                    .UpdatedDate = FieldToDateTime(oReader, 8)
                                    .VersionNo = FieldToByte(oReader, 9)
                                    .AddedUserName = GetUserName(.AddedUserAccountId)
                                    .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                                End With
                                List.Add(ePosition)
                            End If
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As Position, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of Position)
            Dim List As New List(Of Position)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpCommon_GetDataPage")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Employees.Position"
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = SqlSelect(DataEntity)
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = SqlOrderBy(DataEntity)
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = SqlWhere(DataEntity)
                            .Add("@Group", SqlDbType.NVarChar, -1).Value = String.Empty
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim oPosition As Position
                        While oReader.Read
                            oPosition = New Position
                            With oPosition
                                .PositionId = FieldToint32(oReader, 1)
                                .PositionName = FieldToString(oReader, 2)
                                .PositionTypeId = FieldToint32(oReader, 3)
                                .SortIndex = FieldToint32(oReader, 4)
                                .IsOfficer = FieldToBoolean(oReader, 5)

                                .AddedUserAccountId = FieldToint32(oReader, 6)
                                .UpdatedUserAccountId = FieldToint32(oReader, 7)
                                .AddedDate = FieldToDateTime(oReader, 8)
                                .UpdatedDate = FieldToDateTime(oReader, 9)
                                .VersionNo = FieldToByte(oReader, 10)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(oPosition)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If List.Count > 0 Then
                MyBase.GetPagesTotal("Employees.Position", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return List
        End Function


        Public Overrides Function Find(ByVal Id As Integer) As Position
            Dim oPosition As New Position

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionTypeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "SortIndex, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsOfficer, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Employees.Position ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE PositionId = " & Id.ToString, Environment.NewLine)

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
                            With oPosition
                                .PositionId = FieldToint32(oReader, 0)
                                .PositionName = FieldToString(oReader, 1)
                                .PositionTypeId = FieldToint32(oReader, 2)
                                .SortIndex = FieldToint32(oReader, 3)
                                .IsOfficer = FieldToBoolean(oReader, 4)

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

            Return oPosition
        End Function

        Protected Overrides Function SqlSelect(ByVal Entity As Position) As String
            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "PositionId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionTypeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "SortIndex, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsOfficer, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As Position) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As Position) As String
            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "PositionId ", Environment.NewLine)

            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "

        Public Function Add(
        ByVal PositionName As String,
        ByVal PositionTypeId As Integer,
        ByVal SortIndex As Integer,
        ByVal IsOfficer As Boolean,
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpPosition_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@PositionName", SqlDbType.NVarChar, 50).Value = StringToField(PositionName)
                            .Add("@PositionTypeId", SqlDbType.Int).Value = Int32ToField(PositionTypeId)
                            .Add("@SortIndex", SqlDbType.Int).Value = Int32ToField(SortIndex)
                            .Add("@IsOfficer", SqlDbType.Bit).Value = BooleanToField(IsOfficer)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(AddedUserAccountId)

                            .Add(New SqlParameter("@PositionId", SqlDbType.Int))
                            .Item("@PositionId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@PositionId").Value)
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
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function


        Public Function Update(
        ByVal PositionId As Integer,
        ByVal PositionName As String,
        ByVal PositionTypeId As Integer,
        ByVal SortIndex As Integer,
        ByVal IsOfficer As Boolean,
        ByVal UpdatedUserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpPosition_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@PositionId", SqlDbType.Int).Value = Int32ToField(PositionId)
                            .Add("@PositionName", SqlDbType.NVarChar, 50).Value = StringToField(PositionName)
                            .Add("@PositionTypeId", SqlDbType.Int).Value = Int32ToField(PositionTypeId)
                            .Add("@SortIndex", SqlDbType.Int).Value = Int32ToField(SortIndex)
                            .Add("@IsOfficer", SqlDbType.Bit).Value = BooleanToField(IsOfficer)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UpdatedUserAccountId)
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
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function
        Public Function LoadCivilian() As System.Collections.Generic.List(Of Position)
            Dim List As New List(Of Position)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionTypeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "SortIndex, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsOfficer, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Employees.Position ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", " PositionTypeId = 2 ", Environment.NewLine)
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim ePosition As Position
                        While oReader.Read
                            ePosition = New Position
                            With ePosition
                                .PositionId = FieldToint32(oReader, 0)
                                .PositionName = FieldToString(oReader, 1)
                                .PositionTypeId = FieldToint32(oReader, 2)
                                .SortIndex = FieldToint32(oReader, 3)
                                .IsOfficer = FieldToBoolean(oReader, 4)

                                .AddedUserAccountId = FieldToint32(oReader, 5)
                                .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                .AddedDate = FieldToDateTime(oReader, 7)
                                .UpdatedDate = FieldToDateTime(oReader, 8)
                                .VersionNo = FieldToByte(oReader, 9)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(ePosition)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Public Function LoadMilitary() As System.Collections.Generic.List(Of Position)
            Dim List As New List(Of Position)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionTypeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "SortIndex, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsOfficer, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Employees.Position ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", " PositionTypeId = 1 ", Environment.NewLine)
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim ePosition As Position
                        While oReader.Read
                            ePosition = New Position
                            With ePosition
                                .PositionId = FieldToint32(oReader, 0)
                                .PositionName = FieldToString(oReader, 1)
                                .PositionTypeId = FieldToint32(oReader, 2)
                                .SortIndex = FieldToint32(oReader, 3)
                                .IsOfficer = FieldToBoolean(oReader, 4)

                                .AddedUserAccountId = FieldToint32(oReader, 5)
                                .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                .AddedDate = FieldToDateTime(oReader, 7)
                                .UpdatedDate = FieldToDateTime(oReader, 8)
                                .VersionNo = FieldToByte(oReader, 9)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(ePosition)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function

        Public Function GetList(ByVal PositionTypeId As Integer) As System.Data.DataSet

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Employees.Position ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE PositionTypeId = " & PositionTypeId.ToString, Environment.NewLine)
            sb.AppendFormat("{0}{1}", "ORDER BY PositionId ", Environment.NewLine)


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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Position")
                        Adapter.Dispose()
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "Retrive List Failed"
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetPositionTypesList() As System.Data.DataSet

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionTypeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PositionTypeName ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Employees.PositionType ", Environment.NewLine)

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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "PositionType")
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
