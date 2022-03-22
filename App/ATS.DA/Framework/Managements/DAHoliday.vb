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
    Public Class DAHoliday
        Inherits DataAccessBase(Of Holiday)

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
                    Dim cmd As New SqlCommand("Managements.SpHoliday_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@HolidayId", SqlDbType.Int).Value = FieldToint32(Id)

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
        ByVal DataEntity As Holiday, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Return Nothing
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of Holiday)
            Dim List As New List(Of Holiday)
            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As Holiday, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of Holiday)
            Dim List As New List(Of Holiday)
            Return List
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As Holiday
            Dim eHoliday As New Holiday

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpHoliday_GetById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@HolidayId", SqlDbType.Int).Value = Int32ToField(Id)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With eHoliday
                                .HolidayId = FieldToint32(oReader, 0)
                                .HolidayName = FieldToString(oReader, 1)
                                .EffectiveDate = FieldToDate(oReader, 2)
                                .DateExpire = FieldToDate(oReader, 3)
                                .Note = FieldToString(oReader, 4)

                                .AddedUserAccountId = FieldToint32(oReader, 5)
                                .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                .AddedDate = FieldToDate(oReader, 7)
                                .UpdatedDate = FieldToDate(oReader, 8)
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

            Return eHoliday
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As Holiday) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As Holiday) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As Holiday) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal HolidayName As String,
        ByVal EffectiveDate As Date,
        ByVal DateExpire As Date,
        ByVal Note As String,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpHoliday_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@HolidayName", SqlDbType.NVarChar, 100).Value = StringToField(HolidayName)
                            .Add("@EffectiveDate", SqlDbType.Date).Value = DateToField(EffectiveDate)
                            .Add("@DateExpire", SqlDbType.Date).Value = DateToField(DateExpire)
                            .Add("@Note", SqlDbType.NVarChar, 300).Value = StringToField(Note)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@HolidayId", SqlDbType.Int))
                            .Item("@HolidayId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@HolidayId").Value)
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
        ByVal HolidayId As Integer, _
        ByVal HolidayName As String,
        ByVal EffectiveDate As Date,
        ByVal DateExpire As Date,
        ByVal Note As String,
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpHoliday_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@HolidayId", SqlDbType.Int).Value = Int32ToField(HolidayId)
                            .Add("@HolidayName", SqlDbType.NVarChar, 100).Value = StringToField(HolidayName)
                            .Add("@EffectiveDate", SqlDbType.Date).Value = DateToField(EffectiveDate)
                            .Add("@DateExpire", SqlDbType.Date).Value = DateToField(DateExpire)
                            .Add("@Note", SqlDbType.NVarChar, 300).Value = StringToField(Note)
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
        Public Function GetHolidaysDataset(Optional ByVal PageNo As Integer = 1, Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpHoliday_GetAll")
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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Holiday")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
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

