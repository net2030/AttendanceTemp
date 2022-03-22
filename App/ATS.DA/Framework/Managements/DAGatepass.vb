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

    Public Class DAGatepass
        Inherits DataAccessBase(Of Gatepass)

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
                    Dim cmd As New SqlCommand("Managements.SpGatepass_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassId", SqlDbType.Int).Value = FieldToint32(Id)

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
        Public Function GetGatepasssDataset( _
        ByVal UserAccountId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
                            .Add("@PageNumber", SqlDbType.Int).Value = Int32ToField(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = Int32ToField(PageSize)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetGatepasssByDateDataset( _
        ByVal UserAccountId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetByDate")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@PageNumber", SqlDbType.Int).Value = Int32ToField(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = Int32ToField(PageSize)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetGatepasssByEmployeeIdDataset( _
        ByVal EmployeeId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetByEmployeeId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@PageNumber", SqlDbType.Int).Value = 1
                            .Add("@PageSize", SqlDbType.Int).Value = 50
                            .Add("@Lang", SqlDbType.Char, 2).Value = Lang

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As Gatepass, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Return Nothing
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of Gatepass)
            Dim List As New List(Of Gatepass)
            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As Gatepass, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of Gatepass)
            Dim List As New List(Of Gatepass)
            Return List
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As Gatepass
            Dim oEmployee As New Gatepass

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassId", SqlDbType.Int).Value = Int32ToField(Id)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With oEmployee
                                .GatepassId = FieldToint32(oReader, 0)
                                .EmployeeId = FieldToint32(oReader, 1)
                                .IsApproved = FieldToBoolean(oReader, 2)
                                .GatepassTypeId = FieldToint32(oReader, 3)
                                .GatepassBegDate = FieldToDate(oReader, 4)
                                .GatepassEndDate = FieldToDate(oReader, 5)
                                .GatepassBegTime = FieldToDate(oReader, 6)
                                .GatepassEndTime = FieldToDate(oReader, 7)
                                .RequestedDate = FieldToDate(oReader, 8)
                                .ApprovedDate = FieldToDate(oReader, 9)
                                .ApprovedEmployeeId = FieldToint32(oReader, 10)
                                .Notes = FieldToString(oReader, 11)


                                .AddedUserAccountId = FieldToint32(oReader, 13)
                                .UpdatedUserAccountId = FieldToint32(oReader, 14)
                                .AddedDate = FieldToDate(oReader, 15)
                                .UpdatedDate = FieldToDate(oReader, 16)
                                .VersionNo = FieldToByte(oReader, 17)
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
        Protected Overrides Function SqlSelect(ByVal Entity As Gatepass) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString
        End Function

        Protected Overrides Function SqlWhere(ByVal Entity As Gatepass) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As Gatepass) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal EmployeeId As Integer,
        ByVal GatepassTypeId As Integer,
        ByVal GatepassBegDate As Date,
        ByVal GatepassEndDate As Date,
        ByVal GatepassBegTime As Date,
        ByVal GatepassEndTime As Date,
        ByVal ApprovedEmployeeId As Integer,
        ByVal Notes As String,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@GatepassTypeId", SqlDbType.Int).Value = Int32ToField(GatepassTypeId)
                            .Add("@GatepassBegDate", SqlDbType.Date).Value = DateToField(GatepassBegDate)
                            .Add("@GatepassEndDate", SqlDbType.Date).Value = DateToField(GatepassEndDate)
                            .Add("@GatepassBegTime", SqlDbType.DateTime).Value = DateToField(GatepassBegTime)
                            .Add("@GatepassEndTime", SqlDbType.DateTime).Value = DateToField(GatepassEndTime)
                            .Add("@ApprovedEmployeeId", SqlDbType.Int).Value = Int32ToField(ApprovedEmployeeId)
                            .Add("@Notes", SqlDbType.NVarChar, 200).Value = StringToField(Notes)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@GatepassId", SqlDbType.Int))
                            .Item("@GatepassId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@GatepassId").Value)
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
        ByVal GatepassId As Integer,
        ByVal EmployeeId As Integer,
        ByVal GatepassTypeId As Integer,
        ByVal GatepassBegDate As Date,
        ByVal GatepassEndDate As Date,
        ByVal GatepassBegTime As Date,
        ByVal GatepassEndTime As Date,
        ByVal ApprovedEmployeeId As Integer,
        ByVal Notes As String,
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassId", SqlDbType.Int).Value = Int32ToField(GatepassId)
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@GatepassTypeId", SqlDbType.Int).Value = Int32ToField(GatepassTypeId)
                            .Add("@GatepassBegDate", SqlDbType.Date).Value = DateToField(GatepassBegDate)
                            .Add("@GatepassEndDate", SqlDbType.Date).Value = DateToField(GatepassEndDate)
                            .Add("@GatepassBegTime", SqlDbType.DateTime).Value = DateToField(GatepassBegTime)
                            .Add("@GatepassEndTime", SqlDbType.DateTime).Value = DateToField(GatepassEndTime)
                            .Add("@ApprovedEmployeeId", SqlDbType.Int).Value = Int32ToField(ApprovedEmployeeId)
                            .Add("@Notes", SqlDbType.NVarChar, 200).Value = StringToField(Notes)
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

        Public Function Approve( _
        ByVal GatepassId As Integer, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_Approve")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassId", SqlDbType.Int).Value = Int32ToField(GatepassId)
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

        Public Function GetGatepasssByIdDateDataset(ByVal GatepassId As Integer) As System.Data.DataSet

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassId", SqlDbType.Int).Value = Int32ToField(GatepassId)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetEmployeeGatepasssDateDataset( _
        ByVal EmployeeId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetByEmployeeId")
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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetDepartmentGatepassDateDataset( _
        ByVal DepartmentId As Integer, _
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal OptionNo As Integer = 1,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetDepartmentGatepass")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@OptionNo", SqlDbType.Int).Value = 1 'Int32ToField(OptionNo)
                            .Add("@Lang", SqlDbType.Char, 2).Value = Lang
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetGatepassTypesList() As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatePassType_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                       
                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "GatepassType")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetGatepassByEmployeeManagerForApprovalDataset( _
        ByVal EmployeeId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetForApproval")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetGatepassByEmployeeManagerForApprovalDataset1( _
        ByVal EmployeeId As Integer, Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetForApproval1")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@Option", SqlDbType.Int).Value = 1
                            .Add("@Lang", SqlDbType.Char, 2).Value = Lang
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetGatepassBySpecificRoleForApprovalDataset( _
        ByVal EmployeeId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetForApproval1")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@Option", SqlDbType.Int).Value = 2
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function AddGatePassApproval(ByVal GatepassId As Integer,
                                            ByVal ApprovalPolicyId As Integer,
                                            ByVal ApprovalPathId As Integer,
                                            ByVal IsApproved As Boolean,
                                            ByVal IsRejected As Boolean,
                                            ByVal Comments As String,
                                            ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_ApprovalInsert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassId", SqlDbType.Int).Value = Int32ToField(GatepassId)
                            .Add("@ApprovalPolicyId", SqlDbType.Int).Value = Int32ToField(ApprovalPolicyId)
                            .Add("@ApprovalPathId", SqlDbType.Int).Value = Int32ToField(ApprovalPathId)
                            .Add("@IsApproved", SqlDbType.Bit).Value = BooleanToField(IsApproved)
                            .Add("@IsRejected", SqlDbType.Bit).Value = BooleanToField(IsRejected)
                            .Add("@Comments", SqlDbType.NVarChar, 200).Value = StringToField(Comments)
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

        Public Function UpdateGatepassApprovalStatus(ByVal GatepassId As Integer,
                                                     ByVal IsApproved As Boolean,
                                                     ByVal IsRejected As Boolean,
                                                     ByVal UserId As Integer,
                                                     ByVal Notes As String) As Boolean

            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_ApproveStatus")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassID", SqlDbType.Int).Value = GatepassId
                            .Add("@IsApproved", SqlDbType.Bit).Value = IsApproved
                            .Add("@IsRejected", SqlDbType.Bit).Value = IsRejected
                            .Add("@Notes", SqlDbType.NVarChar, 200).Value = Notes
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@GatepassId").Value)
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


        Public Function GetGatepassApprovalLogByGatepassId(ByVal GatepassId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpGatepass_GetApprovalRecord")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@GatepassId", SqlDbType.Int).Value = Int32ToField(GatepassId)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Gatepass")
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

