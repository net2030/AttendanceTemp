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
    Public Class DAGatepassType
        Inherits DataAccessBase(Of GatepassType)

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
                    Dim cmd As New SqlCommand("Managements.SpGatepassType_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassTypeId", SqlDbType.Int).Value = FieldToint32(Id)

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
        ByVal DataEntity As GatepassType, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of GatepassType)
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As GatepassType, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of GatepassType)
            Throw New NotImplementedException
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As GatepassType
            Dim oEmployee As New GatepassType

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepassType_GetById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassTypeId", SqlDbType.Int).Value = Int32ToField(Id)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With oEmployee
                                .GatepassTypeId = FieldToint32(oReader, 0)
                                .GatepassTypeName = FieldToString(oReader, 1)
                                .GatepassTypeNameEN = FieldToString(oReader, 2)

                                .AddedUserAccountId = FieldToint32(oReader, 3)
                                .UpdatedUserAccountId = FieldToint32(oReader, 4)
                                .AddedDate = FieldToDate(oReader, 5)
                                .UpdatedDate = FieldToDate(oReader, 6)
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

            Return oEmployee
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As GatepassType) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As GatepassType) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As GatepassType) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal GatepassTypeName As String,
        ByVal UserAccountId As Integer,
        Optional ByVal GatepassTypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepassType_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassTypeName", SqlDbType.NVarChar, 100).Value = StringToField(GatepassTypeName)
                            .Add("@GatepassTypeNameEN", SqlDbType.NVarChar, 100).Value = StringToField(GatepassTypeNameEN)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@GatepassTypeId", SqlDbType.Int))
                            .Item("@GatepassTypeId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@GatepassTypeId").Value)
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
        Public Function Update( _
        ByVal GatepassTypeId As Integer, _
        ByVal GatepassTypeName As String,
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte(),
        Optional ByVal GatepassTypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepassType_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassTypeId", SqlDbType.Int).Value = Int32ToField(GatepassTypeId)
                            .Add("@GatepassTypeName", SqlDbType.NVarChar, 100).Value = StringToField(GatepassTypeName)
                            .Add("@GatepassTypeNameEN", SqlDbType.NVarChar, 100).Value = StringToField(GatepassTypeNameEN)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
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
        Public Function GetGatepassTypesDataset( _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepassType_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "GatepassType")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetGatepassTypesListDataset(Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepassType_GetList")
                    cmd.Parameters.Add("@Lang", SqlDbType.Char, 2).Value = FieldToString(Lang)
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkSchedule")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
#End Region

    End Class
End Namespace

