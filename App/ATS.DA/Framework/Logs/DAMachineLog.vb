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

    Public Class DAMachineLog
        Inherits DataAccessBase(Of MachineLog)

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
                    Dim cmd As New SqlCommand("Logs.SpMachineLog_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@LogId", SqlDbType.Int).Value = FieldToint32(Id)

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
        Public Function GetEmployeeAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendanceLog_GetAttendance")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@Attendancedate", SqlDbType.Date).Value = DateToField(AttendanceDate)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")


                        MyBase.PageTotal = FieldToint32(cmd.Parameters("@PagesTotal").Value)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As MachineLog, _
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
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Employees.AttendanceLog"
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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "AttendanceLog")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If MyBase.RowsEffected > 0 Then
                MyBase.GetPagesTotal("Employees.AttendanceLog", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return MyBase.Dataset
        End Function
        Public Function GetLogByEmployeeIdDataset( _
        ByVal EmployeeId As Integer, _
        ByVal LogBegDate As Date, _
        ByVal LogEndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "LogId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "EmployeeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "LogDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "LogTime, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "LogTypeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IPAddress, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MachineId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsValidRecord, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsManualRecord, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdateDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "LogTypeName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MachineName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Location, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "DepartmentId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MachineTypeName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsUsedForAttendance, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "EmployeeName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "BadgeNo, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "ManualRecord, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "ValidRecord, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "DepartmentName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UmLogDate ", Environment.NewLine)

            Dim sbWhere As New System.Text.StringBuilder
            sbWhere.AppendFormat("{0}{1}", "EmployeeId = " & EmployeeId, Environment.NewLine)
            sbWhere.AppendFormat("{0}{1}", " AND LogDate >= " & Str2Field(LogBegDate.ToShortDateString), Environment.NewLine)
            sbWhere.AppendFormat("{0}{1}", " AND LogDate <= " & Str2Field(LogEndDate.ToShortDateString), Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpCommon_GetDataPage")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Logs.vwMachineLog"
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = sb.ToString
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = "LogDate, LogTime"
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = sbWhere.ToString
                            .Add("@Group", SqlDbType.NVarChar, -1).Value = String.Empty
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "vwMachineLog")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If MyBase.RowsEffected > 0 Then
                MyBase.GetPagesTotal("Logs.vwMachineLog", sbWhere.ToString, PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return MyBase.Dataset
        End Function

        Public Function GetManualLogsFromMachineLog(DepartmentID As Integer, BegDate As Date, EndDate As Date) As System.Data.DataSet

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()

                    Dim cmd As New SqlCommand("Logs.SpMachineLog_GetManualLogs")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = DepartmentID
                            .Add("@BegDate", SqlDbType.Date).Value = BegDate
                            .Add("@EndDate", SqlDbType.Date).Value = EndDate

                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New System.Data.DataSet
                        Adapter.SelectCommand = cmd
                        Adapter.Fill(MyBase.Dataset, "Attendance")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return MyBase.Dataset
        End Function

        Public Function GetLogByAttendanceLogIdDataset(ByVal LogId As Integer,
                                                   Optional ByVal PageNo As Integer = 1, _
                                                   Optional ByVal PageSize As Integer = 50) As System.Data.DataSet

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()

                    Dim cmd As New SqlCommand("Logs.SpMachineLog_GetByAttendanceLog")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@LogId", SqlDbType.Int).Value = LogId
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New System.Data.DataSet
                        Adapter.SelectCommand = cmd
                        Adapter.Fill(MyBase.Dataset, "Attendance")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return MyBase.Dataset
        End Function

        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of MachineLog)
            Dim List As New List(Of MachineLog)
            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As MachineLog, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of MachineLog)
            Dim List As New List(Of MachineLog)
            Return List
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As MachineLog
            Dim eMachineLog As New MachineLog

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpMachineLog_GetLogById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@LogId", SqlDbType.Int).Value = Int32ToField(Id)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With eMachineLog
                                .LogId = FieldToint32(oReader, 0)
                                .EmployeeId = FieldToint32(oReader, 1)
                                .LogDate = FieldToDate(oReader, 2)
                                .LogTime = FieldToDate(oReader, 3)
                                .LogTypeId = FieldToint32(oReader, 4)
                                .IPAddress = FieldToString(oReader, 5)
                                .MachineId = FieldToint32(oReader, 6)
                                .IsValidRecord = FieldToBoolean(oReader, 7)
                                .IsManualRecord = FieldToBoolean(oReader, 8)

                                .AddedUserAccountId = FieldToint32(oReader, 9)
                                .UpdatedUserAccountId = FieldToint32(oReader, 10)
                                .AddedDate = FieldToDate(oReader, 11)
                                .UpdatedDate = FieldToDate(oReader, 12)
                                .VersionNo = FieldToByte(oReader, 13)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return eMachineLog
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As MachineLog) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As MachineLog) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As MachineLog) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal EmployeeId As Integer, _
        ByVal LogDate As Date, _
        ByVal LogTime As Date, _
        ByVal LogTypeId As Integer, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpMachineLog_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@LogDate", SqlDbType.Date).Value = DateToField(LogDate)
                            .Add("@LogTime", SqlDbType.DateTime).Value = DateToField(LogTime)
                            .Add("@LogTypeId", SqlDbType.Int).Value = Int32ToField(LogTypeId)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@LogId", SqlDbType.Int))
                            .Item("@LogId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@LogId").Value)
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
        ByVal LogId As Integer, _
        ByVal EmployeeId As Integer, _
        ByVal LogDate As Date, _
        ByVal LogTime As Date, _
        ByVal LogTypeId As Integer, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpMachineLog_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@LogId", SqlDbType.Int).Value = Int32ToField(LogId)
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@LogDate", SqlDbType.Date).Value = DateToField(LogDate)
                            .Add("@LogTime", SqlDbType.DateTime).Value = DateToField(LogTime)
                            .Add("@LogTypeId", SqlDbType.Int).Value = Int32ToField(LogTypeId)
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

        Public Function ChangeLogType(ByVal LogId As Integer) As Boolean

            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpMachineLog_Change")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@LogId", SqlDbType.Int).Value = Int32ToField(LogId)
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

        Public Function GetLogTypesList() As System.Data.DataSet

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpLogType_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "LogType")
                        Adapter.Dispose()
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "Retrive List Failed"
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetDevicesChartData() As System.Data.DataSet

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpDevices_GetChartData")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "LogType")
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

#Region " Miscellaneous "

#End Region

    End Class

End Namespace

