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

    Public Class DAWorkSchedule
        Inherits DataAccessBase(Of WorkSchedule)

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
                    Dim cmd As New SqlCommand("Managements.SpWorkSchedule_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = FieldToint32(Id)

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
        ByVal DataEntity As WorkSchedule, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of WorkSchedule)
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As WorkSchedule, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of WorkSchedule)
            Throw New NotImplementedException
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As WorkSchedule
            Dim oEmployee As New WorkSchedule

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkSchedule_GetById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = Int32ToField(Id)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With oEmployee
                                .WorkScheduleId = FieldToint32(oReader, 0)
                                .WorkScheduleTypeId = FieldToint32(oReader, 1)
                                .DepartmentId = FieldToint32(oReader, 2)
                                .WorkScheduleName = FieldToString(oReader, 3)
                                .ScheduleBegDate = FieldToDate(oReader, 4)
                                .ScheduleEndDate = FieldToDate(oReader, 5)
                                .IsEffectiveDuringHoliday = FieldToBoolean(oReader, 6)
                                .IsPolicyApplied = FieldToBoolean(oReader, 7)
                                .PolicyId = FieldToint32(oReader, 8)
                                .ShiftsCount = FieldToint32(oReader, 9)
                                .IsPeriodic = FieldToBoolean(oReader, 10)
                                .PeriodLength = FieldToint32(oReader, 11)
                                .FirstPeriodStartDate = FieldToDate(oReader, 12)

                                .AddedUserAccountId = FieldToint32(oReader, 13)
                                .UpdatedUserAccountId = FieldToint32(oReader, 14)
                                .AddedDate = FieldToDate(oReader, 15)
                                .UpdatedDate = FieldToDate(oReader, 16)
                                .VersionNo = FieldToByte(oReader, 17)
                                .WorkScheduleNameEN = FieldToString(oReader, 18)
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
        Protected Overrides Function SqlSelect(ByVal Entity As WorkSchedule) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As WorkSchedule) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As WorkSchedule) As String
            Throw New NotImplementedException
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal WorkScheduleName As String,
        ByVal WorkScheduleTypeId As Integer,
        ByVal DepartmentId As Integer,
        ByVal ScheduleBegDate As Date,
        ByVal ScheduleEndDate As Date,
        ByVal IsEffectiveDuringHoliday As Boolean,
        ByVal IsPolicyApplied As Boolean,
        ByVal PolicyId As Integer,
        ByVal ShiftsCount As Integer,
        ByVal IsPeriodic As Boolean,
        ByVal PeriodLength As Integer,
        ByVal FirstPeriodStartDate As Date,
        ByVal UserAccountId As Integer,
        Optional ByVal WorkScheduleNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkSchedule_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkScheduleName", SqlDbType.NVarChar, 100).Value = StringToField(WorkScheduleName)
                            .Add("@WorkScheduleNameEN", SqlDbType.NVarChar, 100).Value = StringToField(WorkScheduleNameEN)
                            .Add("@WorkScheduleTypeId", SqlDbType.NVarChar, 100).Value = Int32ToField(WorkScheduleTypeId)
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@ScheduleBegDate", SqlDbType.Date).Value = DateToField(ScheduleBegDate)
                            .Add("@ScheduleEndDate", SqlDbType.Date).Value = DateToField(ScheduleEndDate)
                            .Add("@IsEffectiveDuringHoliday", SqlDbType.Bit).Value = BooleanToField(IsEffectiveDuringHoliday)
                            .Add("@IsPolicyApplied", SqlDbType.Bit).Value = BooleanToField(IsPolicyApplied)
                            .Add("@PolicyId", SqlDbType.Int).Value = Int32ToField(PolicyId)

                            .Add("@ShiftsCount", SqlDbType.Int).Value = Int32ToField(ShiftsCount)
                            .Add("@IsPeriodic", SqlDbType.Bit).Value = BooleanToField(IsPeriodic)
                            .Add("@PeriodLength", SqlDbType.Int).Value = Int32ToField(PeriodLength)
                            .Add("@FirstPeriodStartDate", SqlDbType.Date).Value = DateToField(FirstPeriodStartDate)

                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@WorkScheduleId", SqlDbType.Int))
                            .Item("@WorkScheduleId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@WorkScheduleId").Value)
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
        ByVal WorkScheduleName As String,
        ByVal WorkScheduleTypeId As Integer,
        ByVal DepartmentId As Integer,
        ByVal ScheduleBegDate As Date,
        ByVal ScheduleEndDate As Date,
        ByVal IsEffectiveDuringHoliday As Boolean,
        ByVal IsPolicyApplied As Boolean,
        ByVal PolicyId As Integer,
        ByVal ShiftsCount As Integer,
        ByVal IsPeriodic As Boolean,
        ByVal PeriodLength As Integer,
        ByVal FirstPeriodStartDate As Date,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte(),
        Optional ByVal WorkScheduleNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkSchedule_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = Int32ToField(WorkScheduleId)
                            .Add("@WorkScheduleName", SqlDbType.NVarChar, 100).Value = StringToField(WorkScheduleName)
                            .Add("@WorkScheduleNameEN", SqlDbType.NVarChar, 100).Value = StringToField(WorkScheduleNameEN)
                            .Add("@WorkScheduleTypeId", SqlDbType.NVarChar, 100).Value = Int32ToField(WorkScheduleTypeId)
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@ScheduleBegDate", SqlDbType.Date).Value = DateToField(ScheduleBegDate)
                            .Add("@ScheduleEndDate", SqlDbType.Date).Value = DateToField(ScheduleEndDate)
                            .Add("@IsEffectiveDuringHoliday", SqlDbType.Bit).Value = BooleanToField(IsEffectiveDuringHoliday)
                            .Add("@IsPolicyApplied", SqlDbType.Bit).Value = BooleanToField(IsPolicyApplied)
                            .Add("@PolicyId", SqlDbType.Int).Value = Int32ToField(PolicyId)

                            .Add("@ShiftsCount", SqlDbType.Int).Value = Int32ToField(ShiftsCount)
                            .Add("@IsPeriodic", SqlDbType.Bit).Value = BooleanToField(IsPeriodic)
                            .Add("@PeriodLength", SqlDbType.Int).Value = Int32ToField(PeriodLength)
                            .Add("@FirstPeriodStartDate", SqlDbType.Date).Value = DateToField(FirstPeriodStartDate)

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
        Public Function GetWorkSchedulesDataset(
        ByVal DepartmentId As Integer,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkScedule_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = FieldToint32(DepartmentId)

                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkSchedule")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetWorkSchedulesListDataset(Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkSchedule_GetList")
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

        Public Function GetWorkScheduleTypesList() As System.Data.DataSet

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WorkScheduleTypeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WorkScheduleTypeName ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Managements.WorkScheduleType ", Environment.NewLine)

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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkScheduleType")
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

