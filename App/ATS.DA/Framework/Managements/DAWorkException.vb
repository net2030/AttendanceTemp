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
    Public Class DAWorkException
        Inherits DataAccessBase(Of WorkException)

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
                    Dim cmd As New SqlCommand("Managements.SpWorkException_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkExceptionId", SqlDbType.Int).Value = FieldToint32(Id)

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
        ByVal DataEntity As WorkException, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Return Nothing
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of WorkException)
            Dim List As New List(Of WorkException)
            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As WorkException, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of WorkException)
            Dim List As New List(Of WorkException)
            Return List
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As WorkException
            Dim oEmployee As New WorkException

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_GetById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkExceptionId", SqlDbType.Int).Value = Int32ToField(Id)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With oEmployee
                                .WorkExceptionId = FieldToint32(oReader, 0)
                                .EmployeeId = FieldToint32(oReader, 1)
                                .WorkExceptionTypeId = FieldToint32(oReader, 2)
                                .WorkExceptionBegDate = FieldToDate(oReader, 3)
                                .WorkExceptionEndDate = FieldToDate(oReader, 4)
                                .DepartmentId = FieldToint32(oReader, 5)
                                .Notes = FieldToString(oReader, 6)

                                .AddedUserAccountId = FieldToint32(oReader, 7)
                                .UpdatedUserAccountId = FieldToint32(oReader, 8)
                                .AddedDate = FieldToDate(oReader, 9)
                                .UpdatedDate = FieldToDate(oReader, 10)
                                .VersionNo = FieldToByte(oReader, 11)
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
        Protected Overrides Function SqlSelect(ByVal Entity As WorkException) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As WorkException) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As WorkException) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal EmployeeId As Integer,
        ByVal WorkExceptionTypeId As Integer,
        ByVal WorkExceptionBegDate As Date,
        ByVal WorkExceptionEndDate As Date,
        ByVal DepartmentId As Integer,
        ByVal Notes As String,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@WorkExceptionTypeId", SqlDbType.Int).Value = Int32ToField(WorkExceptionTypeId)
                            .Add("@WorkExceptionBegDate", SqlDbType.Date).Value = DateToField(WorkExceptionBegDate)
                            .Add("@WorkExceptionEndDate", SqlDbType.Date).Value = DateToField(WorkExceptionEndDate)
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@Notes", SqlDbType.NVarChar, 200).Value = StringToField(Notes)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@WorkExceptionId", SqlDbType.Int))
                            .Item("@WorkExceptionId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@WorkExceptionId").Value)
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
        ByVal WorkExceptionId As Integer,
        ByVal EmployeeId As Integer,
        ByVal WorkExceptionTypeId As Integer,
        ByVal WorkExceptionBegDate As Date,
        ByVal WorkExceptionEndDate As Date,
        ByVal DepartmentId As Integer,
        ByVal Notes As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkExceptionId", SqlDbType.Int).Value = Int32ToField(WorkExceptionId)
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@WorkExceptionTypeId", SqlDbType.Int).Value = Int32ToField(WorkExceptionTypeId)
                            .Add("@WorkExceptionBegDate", SqlDbType.Date).Value = DateToField(WorkExceptionBegDate)
                            .Add("@WorkExceptionEndDate", SqlDbType.Date).Value = DateToField(WorkExceptionEndDate)
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
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
        Public Function GetWorkExceptionsDataset(
        ByVal UserAccountId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_GetAll")
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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkException")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetWorkExceptionsByDepartmentDataset(
        ByVal DepartmentId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50,
         Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_GetByDepartmentId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)
                            .Add("@Lang", SqlDbType.Char, 2).Value = Lang

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkException")

                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetWorkExceptionsByEmployeeIdDataset(
        ByVal EmployeeId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_GetByEmployeeId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)
                            .Add("@Lang", SqlDbType.Char, 2).Value = Lang

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkException")

                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetWorkExceptionsByByTypeDataset(
        ByVal UserAccountId As Integer,
        ByVal ExceptionTypeId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_GetByType")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
                            .Add("@ExceptionTypeId", SqlDbType.Int).Value = Int32ToField(ExceptionTypeId)
                            .Add("@BegDate", SqlDbType.Date).Value = DateToField(BegDate)
                            .Add("@EndDate", SqlDbType.Date).Value = DateToField(EndDate)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkException")

                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function



        Public Function GetWorkExceptionByEmployeeManagerForApprovalDataset(ByVal EmployeeId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_GetForApproval")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeManagerId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkException")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetWorkExceptionByEmployeeManagerForApprovalDataset1(ByVal EmployeeId As Integer, Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_GetForApproval1")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeManagerId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@Lang", SqlDbType.Char, 2).Value = Lang

                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkException")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetWorkExceptionBySpecificRoleForApprovalDataset( _
     ByVal EmployeeId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_GetForApproval1")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeManagerId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@Option", SqlDbType.Int).Value = 2
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkException")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function AddWorkExceptionApproval(ByVal WorkExceptionId As Integer,
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
                    Dim cmd As New SqlCommand("Managements.SpWorkException_ApprovalInsert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkExceptionId", SqlDbType.Int).Value = Int32ToField(WorkExceptionId)
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


        Public Function UpdateWorkExceptionApprovalStatus(ByVal WorkExceptionId As Integer,
                                                     ByVal IsApproved As Boolean,
                                                     ByVal IsRejected As Boolean,
                                                     ByVal UserId As Integer,
                                                     ByVal Notes As String) As Boolean

            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_ApproveStatus")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkExceptionID", SqlDbType.Int).Value = WorkExceptionId
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@WorkExceptionId").Value)
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


        Public Function GetWorkExceptionApprovalLogByWorkExceptionId(ByVal WorkExceptionId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpWorkException_GetApprovalRecord")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WorkExceptionId", SqlDbType.Int).Value = Int32ToField(WorkExceptionId)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "WorkException")
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

