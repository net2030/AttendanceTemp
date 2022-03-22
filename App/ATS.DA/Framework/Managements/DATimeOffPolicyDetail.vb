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

    Public Class DATimeOffPolicyDetail
        Inherits DataAccessBase(Of TimeOffPolicyDetail)

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
                    Dim cmd As New SqlCommand("Managements.SpTimeOffPolicyDetail_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@TimeOffPolicyDetailId", SqlDbType.Int).Value = FieldToint32(Id)

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
        ByVal DataEntity As TimeOffPolicyDetail, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of TimeOffPolicyDetail)
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As TimeOffPolicyDetail, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of TimeOffPolicyDetail)
            Throw New NotImplementedException
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As TimeOffPolicyDetail
            Dim oEmployee As New TimeOffPolicyDetail

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpTimeOffPolicyDetail_GetById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@TimeOffPolicyDetailId", SqlDbType.Int).Value = Int32ToField(Id)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With oEmployee
                                .TimeOffPolicyDetailId = FieldToint32(oReader, 0)
                                .TimeOffPolicyId = FieldToint32(oReader, 1)
                                .GatepassTypeId = FieldToint32(oReader, 2)

                                .EarnPeriodId = FieldToint32(oReader, 3)
                                .ResetToZeroPeriodId = FieldToint32(oReader, 4)
                                .InitialSetToMinutes = FieldToDecimal(oReader, 5)
                                .ResetToMinutes = FieldToDecimal(oReader, 6)
                                .EarnMinutes = FieldToDecimal(oReader, 7)
                                .EffectiveDate = FieldToDate(oReader, 8)
                                .IsCarryForward = FieldToBoolean(oReader, 9)
                                .MinValue = FieldToDecimal(oReader, 10)
                                .MaxValue = FieldToDecimal(oReader, 11)
                                .AddedUserAccountId = FieldToint32(oReader, 12)
                                .UpdatedUserAccountId = FieldToint32(oReader, 13)
                                .AddedDate = FieldToDate(oReader, 14)
                                .UpdatedDate = FieldToDate(oReader, 15)
                                .VersionNo = FieldToByte(oReader, 16)
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
        Protected Overrides Function SqlSelect(ByVal Entity As TimeOffPolicyDetail) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As TimeOffPolicyDetail) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As TimeOffPolicyDetail) As String
            Throw New NotImplementedException
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
                            ByVal TimeOffPolicyId As Integer,
                            ByVal GatepassTypeId As Integer,
                            ByVal EarnPeriodId As Integer,
                            ByVal ResetToZeroPeriodId As Integer,
                            ByVal InitialSetToMinutes As Decimal,
                            ByVal ResetToMinutes As Decimal,
                            ByVal EarnMinutes As Decimal,
                            ByVal EffectiveDate As Date,
                            ByVal IsCarryForward As Boolean,
                            ByVal MinValue As Decimal,
                            ByVal MaxValue As Decimal,
                            ByVal UserAccountId As Integer) As Boolean

            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpTimeOffPolicyDetail_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters

                            .Add("@TimeOffPolicyId", SqlDbType.Int).Value = Int32ToField(TimeOffPolicyId)
                            .Add("@GatepassTypeId", SqlDbType.Int).Value = Int32ToField(GatepassTypeId)
                            .Add("@EarnPeriodId", SqlDbType.Int).Value = Int32ToField(EarnPeriodId)
                            .Add("@ResetToZeroPeriodId", SqlDbType.Int).Value = Int32ToField(ResetToZeroPeriodId)
                            .Add("@InitialSetToMinutes", SqlDbType.Decimal).Value = DecimalToField(InitialSetToMinutes)
                            .Add("@ResetToMinutes", SqlDbType.Decimal).Value = DecimalToField(ResetToMinutes)
                            .Add("@EarnMinutes", SqlDbType.Decimal).Value = DecimalToField(EarnMinutes)
                            .Add("@EffectiveDate", SqlDbType.Date).Value = DateToField(EffectiveDate)
                            .Add("@IsCarryForward", SqlDbType.Bit).Value = BooleanToField(IsCarryForward)
                            .Add("@MinValue", SqlDbType.Decimal).Value = DecimalToField(MinValue)
                            .Add("@MaxValue", SqlDbType.Decimal).Value = DecimalToField(MaxValue)

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
        Public Function Update(ByVal TimeOffPolicyDetailId As Integer,
                            ByVal TimeOffPolicyId As Integer,
                            ByVal GatepassTypeId As Integer,
                            ByVal EarnPeriodId As Integer,
                            ByVal ResetToZeroPeriodId As Integer,
                            ByVal InitialSetToMinutes As Decimal,
                            ByVal ResetToMinutes As Decimal,
                            ByVal EarnMinutes As Decimal,
                            ByVal EffectiveDate As Date,
                            ByVal IsCarryForward As Boolean,
                            ByVal MinValue As Decimal,
                            ByVal MaxValue As Decimal,
                            ByVal UserAccountId As Integer,
                            ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpTimeOffPolicyDetail_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@TimeOffPolicyDetailId", SqlDbType.Int).Value = Int32ToField(TimeOffPolicyDetailId)
                            .Add("@TimeOffPolicyId", SqlDbType.Int).Value = Int32ToField(TimeOffPolicyId)
                            .Add("@GatepassTypeId", SqlDbType.Int).Value = Int32ToField(GatepassTypeId)
                            .Add("@EarnPeriodId", SqlDbType.Int).Value = Int32ToField(EarnPeriodId)
                            .Add("@ResetToZeroPeriodId", SqlDbType.Int).Value = Int32ToField(ResetToZeroPeriodId)
                            .Add("@InitialSetToMinutes", SqlDbType.Decimal).Value = DecimalToField(InitialSetToMinutes)
                            .Add("@ResetToMinutes", SqlDbType.Decimal).Value = DecimalToField(ResetToMinutes)
                            .Add("@EarnMinutes", SqlDbType.Decimal).Value = DecimalToField(EarnMinutes)
                            .Add("@EffectiveDate", SqlDbType.Date).Value = DateToField(EffectiveDate)
                            .Add("@IsCarryForward", SqlDbType.Bit).Value = BooleanToField(IsCarryForward)
                            .Add("@MinValue", SqlDbType.Decimal).Value = DecimalToField(MinValue)
                            .Add("@MaxValue", SqlDbType.Decimal).Value = DecimalToField(MaxValue)

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

        Public Function UpdateBalance(ByVal TimeOffBalanceId As Integer,
                                      ByVal CarryForward As Decimal,
                                      ByVal UserAccountId As Integer,
                                      ByVal VersionNo As Byte()) As Boolean

            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpTimeOffBalance_CarryForward_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@TimeOffBalanceId", SqlDbType.Int).Value = Int32ToField(TimeOffBalanceId)
                            .Add("@CarryForward", SqlDbType.Decimal).Value = DecimalToField(CarryForward)

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

        Public Function GetTimeOffPolicyDetailsByPolicyId(ByVal TimeOffPolicyId As Integer,
                                                            Optional ByVal PageNo As Integer = 1,
                                                            Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpTimeOffPolicyDetail_GetByPolicyId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        With cmd.Parameters
                            .Add("@TimeOffPolicyId", SqlDbType.Int).Value = Int32ToField(TimeOffPolicyId)
                        End With

                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "TimeOffPolicyDetail")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetPeriodsList() As System.Data.DataSet

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PeriodId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "PeriodName ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Periods ", Environment.NewLine)

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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Period")
                        Adapter.Dispose()
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "Retrive List Failed"
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetTypesByLeaveType(ByVal LeaveType As Integer) As System.Data.DataSet

            Dim sb As New System.Text.StringBuilder

            If LeaveType = 1 Then
                sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
                sb.AppendFormat("{0}{1}", "TypeId AS TypeId, ", Environment.NewLine)
                sb.AppendFormat("{0}{1}", "TypeName AS  TypeName", Environment.NewLine)
                sb.AppendFormat("{0}{1}", "FROM Managements.VacationType", Environment.NewLine)
            ElseIf LeaveType = 2 Then
                sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
                sb.AppendFormat("{0}{1}", "WorkExceptionTypeId AS TypeId, ", Environment.NewLine)
                sb.AppendFormat("{0}{1}", "WorkExceptionTypeName AS TypeName ", Environment.NewLine)
                sb.AppendFormat("{0}{1}", "FROM Managements.WorkExceptionType ", Environment.NewLine)
            ElseIf LeaveType = 3 Then
                sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
                sb.AppendFormat("{0}{1}", "GatepassTypeId AS TypeId, ", Environment.NewLine)
                sb.AppendFormat("{0}{1}", "GatepassTypeName AS TypeName ", Environment.NewLine)
                sb.AppendFormat("{0}{1}", "FROM Managements.GatepassType ", Environment.NewLine)
            End If


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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Period")
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

