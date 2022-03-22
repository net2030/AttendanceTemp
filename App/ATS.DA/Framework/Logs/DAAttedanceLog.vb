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

    Public Class DAAttedanceLog
        Inherits DataAccessBase(Of AttendanceLog)

#Region " Constructors "
        Public Sub New()
            MyBase.new()
        End Sub
#End Region

#Region " Overrides Methods "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False
            Return boolSeccessed
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As AttendanceLog, _
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
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of AttendanceLog)
            Dim List As New List(Of AttendanceLog)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsProtected, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "JobTitleId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "JobTitleName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Employees.AttendanceLog ", Environment.NewLine)

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
                        Dim eJobTitle As AttendanceLog
                        While oReader.Read
                            If Not FieldToBoolean(oReader, 0) Then
                                eJobTitle = New AttendanceLog
                                With eJobTitle
                                    '.JobTitleId = FieldToint32(oReader, 1)
                                    '.JobTitleName = FieldToString(oReader, 2)

                                    .AddedUserAccountId = FieldToint32(oReader, 3)
                                    .UpdatedUserAccountId = FieldToint32(oReader, 4)
                                    .AddedDate = FieldToDateTime(oReader, 5)
                                    .UpdatedDate = FieldToDateTime(oReader, 6)
                                    .VersionNo = FieldToByte(oReader, 7)
                                    .AddedUserName = GetUserName(.AddedUserAccountId)
                                    .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                                End With
                                List.Add(eJobTitle)
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
        ByVal DataEntity As AttendanceLog, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of AttendanceLog)
            Dim List As New List(Of AttendanceLog)

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

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim oJobTitle As AttendanceLog
                        While oReader.Read
                            oJobTitle = New AttendanceLog
                            With oJobTitle
                                '.JobTitleId = FieldToint32(oReader, 1)
                                '.JobTitleName = FieldToString(oReader, 2)

                                .AddedUserAccountId = FieldToint32(oReader, 3)
                                .UpdatedUserAccountId = FieldToint32(oReader, 4)
                                .AddedDate = FieldToDate(oReader, 5)
                                .UpdatedDate = FieldToDate(oReader, 6)
                                .VersionNo = FieldToByte(oReader, 7)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(oJobTitle)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If List.Count > 0 Then
                MyBase.GetPagesTotal("Employees.AttendanceLog", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return List
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As AttendanceLog
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As AttendanceLog) As String
             Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As AttendanceLog) As String
             Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As AttendanceLog) As String
             Throw New NotImplementedException
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal JobTitleName As String, _
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpJobTitle_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@JobTitleName", SqlDbType.VarChar, 100).Value = StringToField(JobTitleName)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(AddedUserAccountId)

                            .Add(New SqlParameter("@JobTitleId", SqlDbType.Int))
                            .Item("@JobTitleId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@JobTitleId").Value)
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
        Public Function UpdateAttendance(
        ByVal OptionNo As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        ByVal EmployeeId As Integer,
        ByVal DepartmentId As Integer) As Boolean

            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpProcessAttendanceByDate")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        cmd.CommandTimeout = 300
                        With cmd.Parameters
                            .Add("@OptionNo", SqlDbType.Int).Value = Int32ToField(OptionNo)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@EmpId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()
                        MyBase.InfoMessage = "تمت عملية تحديث البيانات بنجاح"
                        boolSeccessed = True
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function

        Public Function ChangeAttendanceStatus(ByVal LogId As Integer,
        ByVal StatusId As Integer,
        ByVal UserAccountId As Integer) As Boolean

            Dim boolSeccessed As Boolean = False
            Dim SQLStr As String = ""

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()


                    Dim cmd As New SqlCommand("Logs.SpAttendanceLog_Change")

                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@StatusId", SqlDbType.Int).Value = Int32ToField(StatusId)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
                            .Add("@LogId", SqlDbType.Int).Value = Int32ToField(LogId)

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output

                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()
                        MyBase.InfoMessage = "تمت عملية تحديث البيانات بنجاح"
                        boolSeccessed = True
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function

        Public Function UpdateDepartmentAttendance(ByVal optioNo As Integer, ByVal begDate As Date, ByVal endDate As Date, ByVal DepartmentId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpProcessAttendanceByDate")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Option", SqlDbType.Int).Value = Int32ToField(optioNo)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(begDate)
                            .Add("@endDate", SqlDbType.Date).Value = DateToField(endDate)
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()
                        MyBase.InfoMessage = "تمت عملية تحديث البيانات بنجاح"
                        boolSeccessed = True
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function
        Public Function UpdateEmployeeAttendance(ByVal optionNo As Integer, ByVal begDate As Date, ByVal endDate As Date, ByVal employeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpProcessAttendanceByDate")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Option", SqlDbType.Int).Value = Int32ToField(optionNo)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(begDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(endDate)
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(employeeId)

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()
                        MyBase.InfoMessage = "تمت عملية تحديث البيانات بنجاح"
                        boolSeccessed = True
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function
        Public Function GetEmployeeAttendanceByDate(
        ByVal EmployeeId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar", Optional ByVal Op As Integer = 0) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendanceLog_GetEmployeeAttendanceByDate")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(500)
                            .Add("@Lang", SqlDbType.Char, 2).Value = FieldToString(Lang)
                            .Add("@Op", SqlDbType.Int).Value = FieldToint32(Op)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetEmployeeAttendanceDatesByDate(
        ByVal EmployeeId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendanceLog_GetEmployeeAttendanceDatesByDate")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(500)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetDepartmentAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date, _
          ByVal Employer As Integer,
        ByVal Contracttype As Integer,
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
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
                            .Add("@Employer", SqlDbType.Int).Value = FieldToint32(Employer)
                            .Add("@ContractType", SqlDbType.Int).Value = FieldToint32(Contracttype)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)
                            .Add("@Lang", SqlDbType.Char, 2).Value = FieldToString(Lang)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function



        Public Function GetDepartmentAttendanceDatasetRange( _
        ByVal DepartmentId As Integer, _
         ByVal StartDate As Date, _
           ByVal EndDate As Date, _
          ByVal Employer As Integer,
        ByVal Contracttype As Integer,
        ByVal WorkScheduleId As Integer,
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendanceLog_GetAttendanceRange")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@StartDate", SqlDbType.Date).Value = DateToField(StartDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@Employer", SqlDbType.Int).Value = FieldToint32(Employer)
                            .Add("@ContractType", SqlDbType.Int).Value = FieldToint32(Contracttype)
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = FieldToint32(WorkScheduleId)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)
                            .Add("@Lang", SqlDbType.Char, 2).Value = Lang

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function



        Public Function GetAttendanceByUserIdDataset( _
        ByVal UserAccountId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendanceLog_GetEmployeeAttendance")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetEmployeeMilitaryAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendanceLog_GetAttendanceMilitary")
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


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetEmployeeCiviliansAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendanceLog_GetAttendanceCivilians")
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


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetCiviliansAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date) As System.Data.DataSet
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
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetMilitaryAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendanceLog_GetMilitaryAttendance")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@Attendancedate", SqlDbType.Date).Value = DateToField(AttendanceDate)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetUncompleteStatusDataset(
        UserAccountId As Integer,
        FilterOption As enumFilterOption,
        SortOption As enumSortOption,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetUncompleteStatus")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = FieldToint32(UserAccountId)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)
                            .Add("@FilterOptionNo", SqlDbType.Int).Value = CType(FilterOption, Integer)
                            .Add("@SortOptionNo", SqlDbType.Int).Value = CType(SortOption, Integer)

                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "AttendanceLog")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetAttendanceStatusList(Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("SELECT StatusId,CASE WHEN @Lang = 'en' THEN StatusNameEN ELSE StatusName END AS StatusName FROM Logs.AttendanceStatus")
                    cmd.Parameters.Add("@Lang", SqlDbType.Char, 2).Value = Lang
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = oCon


                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
#End Region

#Region "Data Retrive For Charts"

        Public Function GetAttendanceChartData(EmployeeId As Integer, SDate As Date, EDate As Date, Optional ByVal Lang As String = "ar") As DataSet


            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetChartData")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeID", SqlDbType.Int).Value = EmployeeId
                            .Add("@Lang", SqlDbType.NVarChar, 2).Value = Lang
                            .Add("@BegDate", SqlDbType.Date).Value = SDate
                            .Add("@EndDate", SqlDbType.Date).Value = EDate
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Attendance")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return MyBase.Dataset
        End Function

        Public Function GetEmployeeAttendanceSummuryChartDataForReport(EmployeeId As Integer, SDate As Date, EDate As Date) As DataSet


            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("SELECT * FROM Logs.vAttendanceChart WHERE EmployeeId=@EmployeeId AND AttendanceDate>=@BegDate AND AttendanceDate<=@EndDate ORDER By AttendanceDate ")
                    Using cmd
                        'cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeID", SqlDbType.Int).Value = EmployeeId
                            .Add("@BegDate", SqlDbType.Date).Value = SDate
                            .Add("@EndDate", SqlDbType.Date).Value = EDate
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Attendance")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return MyBase.Dataset
        End Function

        Public Function GetAttendanceChartDataByDepartment(EmployeeId As Integer, SDate As Date, EDate As Date, Optional ByVal Lang As String = "ar") As DataSet


            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetChartDataByDepartment")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId
                            .Add("@Lang", SqlDbType.NVarChar, 2).Value = Lang
                            .Add("@BegDate", SqlDbType.Date).Value = SDate
                            .Add("@EndDate", SqlDbType.Date).Value = EDate
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Attendance")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return MyBase.Dataset
        End Function

        Public Function GetAttendanceChartDataForReportComparssion(DepartmentId As Integer, SDate As Date, EDate As Date, ByVal StatusId As Integer, Optional ByVal Lang As String = "ar") As DataSet


            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetChartDataByDepartmentForComparssionReport")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@ParentId", SqlDbType.Int).Value = DepartmentId
                            .Add("@StatusID", SqlDbType.Int).Value = StatusId
                            .Add("@BegDate", SqlDbType.Date).Value = SDate
                            .Add("@EndDate", SqlDbType.Date).Value = EDate
                            .Add("@Lang", SqlDbType.Char, 2).Value = Lang
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "AttendanceChart")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return MyBase.Dataset
        End Function

        Public Function GetAttendanceSummuryChartDataForReport(DepartmentId As Integer, SDate As Date, EDate As Date) As DataSet


            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendanceSummury_GetChartDataByDepartmentForReport")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId
                            .Add("@BegDate", SqlDbType.Date).Value = SDate
                            .Add("@EndDate", SqlDbType.Date).Value = EDate
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet

                        Adapter.SelectCommand = cmd
                        Adapter.Fill(MyBase.Dataset, "AttendanceChart")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return MyBase.Dataset
        End Function
#End Region

#Region " Reports Data "
        Public Function GetLateComersByDateDataset(DepartmentId As Integer, BegDate As Date, EndDate As Date, OptionNo As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetLateComersByDate")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@OptionNo", SqlDbType.Int).Value = Int32ToField(OptionNo)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "vwAttendanceLog")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetLateComersByEmployeeIdDataset(EmployeeId As Integer, BegDate As Date, EndDate As Date) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetLateComersByEmployeeId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "vwAttendanceLog")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetAbsentsByDateDataset(DepartmentId As Integer, BegDate As Date, EndDate As Date, OptionNo As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetAbsentsByDate")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@OptionNo", SqlDbType.Int).Value = Int32ToField(OptionNo)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "vwAttendanceLog")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetAbsentsByEmployeeIdDataset(EmployeeId As Integer, BegDate As Date, EndDate As Date) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetAbsentsByEmployeeId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "vwAttendanceLog")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetEmployeeAttendanceByIdDataset(ByVal EmployeeId As Integer, ByVal BegDate As Date, ByVal EndDate As Date) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetEmployeeAttendanceById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "vwAttendanceLog")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetEmployeesStatisticsDataset(
        ByVal DepartmentId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        ByVal SelectOptionNo As Integer,
        ByVal SortOptionNo As Integer,
        ByVal FilterOptionNo As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetEmployeesStatistics")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@SelectOptionNo", SqlDbType.Int).Value = Int32ToField(SelectOptionNo)
                            .Add("@SortOptionNo", SqlDbType.Int).Value = Int32ToField(SortOptionNo)
                            .Add("@FilterOptionNo", SqlDbType.Int).Value = Int32ToField(FilterOptionNo)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "AttendanceSummary")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetUncompleteTransDataset(DepartmentId As Integer, BegDate As Date, EndDate As Date, OptionNo As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Logs.SpAttendance_GetUncompleteTrans")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@OptionNo", SqlDbType.Int).Value = Int32ToField(OptionNo)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "AttendanceSummary")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function


        Public Function GetSanctionNotification(DepartmentId As Integer, BegDate As Date, EndDate As Date, OptionNo As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("[Notifications].[SpGetsanctionNotification]")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@Op", SqlDbType.Int).Value = Int32ToField(OptionNo)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "AttendanceSummary")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

#End Region

#Region " Miscellaneous "
        Private intFilterOption As enumFilterOption
        Public Enum enumFilterOption As Integer
            AllEmployees = 1
            ALLCivilians = 2
            AllMilitaryEmployees = 3
            MilitaryOfficersOnly = 4
            MilitaryNoneOfficersOnly = 5
        End Enum
        Private intSortOption As enumSortOption
        Public Enum enumSortOption As Integer
            OrderByEmployeeName = 1
            OrderByRankPostion = 2
            OrderByEmployeeId = 3
        End Enum
        Public Property FilterOption() As enumFilterOption
            Get
                Return intFilterOption
            End Get
            Set(ByVal value As enumFilterOption)
                intFilterOption = value
            End Set
        End Property
        Public Property SortOption() As enumSortOption
            Get
                Return intSortOption
            End Get
            Set(ByVal value As enumSortOption)
                intSortOption = value
            End Set
        End Property
#End Region

    End Class

End Namespace

