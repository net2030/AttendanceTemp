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

    Public Class DAWorkScheduleDay
        Inherits DataAccessBase(Of WorkScheduleDay)

#Region " Constructors "
        Public Sub New()
            MyBase.new()
        End Sub
#End Region

#Region " Overrides Methods "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
          Throw New NotImplementedException
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As WorkScheduleDay, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of WorkScheduleDay)
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As WorkScheduleDay, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of WorkScheduleDay)
            Throw New NotImplementedException
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As WorkScheduleDay
           Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As WorkScheduleDay) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As WorkScheduleDay) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As WorkScheduleDay) As String
          Throw New NotImplementedException
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal WorkScheduleId As Integer,
        ByVal ShiftNo As Integer,
        ByVal WeekDayNo As Integer,
        ByVal IsWeekendDay As Boolean,
        ByVal WorkBegTime As DateTime,
        ByVal WorkEndTime As DateTime,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkScheduleDay_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = Int32ToField(WorkScheduleId)
                            .Add("@ShiftNo", SqlDbType.Int).Value = Int32ToField(ShiftNo)
                            .Add("@WeekDayNo", SqlDbType.Int).Value = Int32ToField(WeekDayNo)
                            .Add("@IsWeekendDay", SqlDbType.Bit).Value = BooleanToField(IsWeekendDay)
                            .Add("@WorkBegTime", SqlDbType.DateTime).Value = DateToField(WorkBegTime)
                            .Add("@WorkEndTime", SqlDbType.DateTime).Value = DateToField(WorkEndTime)
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
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal WorkScheduleId As Integer,
        ByVal ShiftNo As Integer,
        ByVal WeekDayNo As Integer,
        ByVal IsWeekendDay As Boolean,
        ByVal WorkBegTime As DateTime,
        ByVal WorkEndTime As DateTime,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkScheduleDay_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = Int32ToField(WorkScheduleId)
                            .Add("@ShiftNo", SqlDbType.Int).Value = Int32ToField(ShiftNo)
                            .Add("@WeekDayNo", SqlDbType.Int).Value = Int32ToField(WeekDayNo)
                            .Add("@IsWeekendDay", SqlDbType.Bit).Value = BooleanToField(IsWeekendDay)
                            .Add("@WorkBegTime", SqlDbType.DateTime).Value = DateToField(WorkBegTime)
                            .Add("@WorkEndTime", SqlDbType.DateTime).Value = DateToField(WorkEndTime)
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
        Public Function FindDay(ByVal WorkScheduleId As Integer, ByVal WeekDayNo As Integer) As WorkScheduleDay
            Dim eDay As New WorkScheduleDay

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkScheduleDay_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = Int32ToField(WorkScheduleId)
                            .Add("@WeekDayNo", SqlDbType.Int).Value = Int32ToField(WeekDayNo)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With eDay
                                .WorkScheduleId = FieldToint32(oReader, 0)
                                .WeekDayNo = FieldToint32(oReader, 1)
                                .IsWeekendDay = FieldToBoolean(oReader, 2)
                                .WorkBegTime = FieldToDate(oReader, 3)
                                .WorkEndTime = FieldToDate(oReader, 4)

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

            Return eDay
        End Function
        Public Function GetWorkScheduleDaysDataset(ByVal WorkScheduleId As Integer, ByVal ShiftNo As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkScheduleDay_GetDays")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = FieldToint32(WorkScheduleId)
                            .Add("@ShiftNo", SqlDbType.Int).Value = FieldToint32(ShiftNo)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkScheduleDay")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function LoadDays(ByVal WorkScheduleId As Integer) As System.Collections.Generic.List(Of WorkScheduleDay)
            Dim List As New List(Of WorkScheduleDay)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkScheduleDay_GetDays")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = FieldToint32(WorkScheduleId)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim eWorkScheduleDay As WorkScheduleDay
                        While oReader.Read
                            If Not FieldToBoolean(oReader, 0) Then
                                eWorkScheduleDay = New WorkScheduleDay
                                With eWorkScheduleDay
                                    .WorkScheduleId = FieldToint32(oReader, 0)
                                    .WeekDayNo = FieldToint32(oReader, 1)
                                    .IsWeekendDay = FieldToBoolean(oReader, 2)
                                    .WorkBegTime = FieldToDate(oReader, 3)
                                    .WorkEndTime = FieldToDate(oReader, 4)

                                    .AddedUserAccountId = FieldToint32(oReader, 5)
                                    .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                    .AddedDate = FieldToDateTime(oReader, 7)
                                    .UpdatedDate = FieldToDateTime(oReader, 8)
                                    .VersionNo = FieldToByte(oReader, 9)
                                    .AddedUserName = GetUserName(.AddedUserAccountId)
                                    .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                                End With
                                List.Add(eWorkScheduleDay)
                            End If

                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
#End Region

    End Class

End Namespace

