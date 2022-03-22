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

    Public Class DAApprovalPath
        Inherits DataAccessBase(Of ApprovalPath)

#Region " Constructors "
        Public Sub New()
            MyBase.New()
        End Sub
#End Region

#Region " Overrides Methods "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Throw New NotImplementedException
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As ApprovalPath, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of ApprovalPath)
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As ApprovalPath, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of ApprovalPath)
            Throw New NotImplementedException
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As ApprovalPath
            Dim ePath As New ApprovalPath

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpApprovalPath_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@ApprovalPathId", SqlDbType.Int).Value = Int32ToField(Id)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With ePath
                                .ApprovalPathId = FieldToint32(oReader, 0)
                                .ApprovalPolicyId = FieldToint32(oReader, 1)
                                .ApproverTypeId = FieldToint32(oReader, 2)
                                .EmployeeId = FieldToint32(oReader, 3)
                                .ApprovalPathSequence = FieldToint32(oReader, 4)


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

            Return ePath
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As ApprovalPath) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As ApprovalPath) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As ApprovalPath) As String
            Throw New NotImplementedException
        End Function
#End Region

#Region " Public Methods "

        Public Function Add(
        ByVal ApprovalPolicyId As Integer,
        ByVal ApproverTypeId As Integer,
        ByVal EmployeeId As Integer,
        ByVal ApprovalPathSequence As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpApprovalPath_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@ApprovalPolicyId", SqlDbType.Int).Value = Int32ToField(ApprovalPolicyId)
                            .Add("@ApproverTypeId", SqlDbType.Int).Value = Int32ToField(ApproverTypeId)
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@ApprovalPathSequence", SqlDbType.Int).Value = Int32ToField(ApprovalPathSequence)
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
                            ByVal ApprovalPathId As Integer,
                            ByVal ApprovalPolicyId As Integer,
                            ByVal ApproverTypeId As Integer,
                            ByVal EmployeeId As Integer,
                            ByVal ApprovalPathSequence As Integer,
                            ByVal UserAccountId As Integer,
                            ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpApprovalPath_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@ApprovalPathId", SqlDbType.Int).Value = Int32ToField(ApprovalPathId)
                            .Add("@ApprovalPolicyId", SqlDbType.Int).Value = Int32ToField(ApprovalPolicyId)
                            .Add("@ApproverTypeId", SqlDbType.Int).Value = Int32ToField(ApproverTypeId)
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@ApprovalPathSequence", SqlDbType.Int).Value = Int32ToField(ApprovalPathSequence)
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
        Public Function GetApprovalPathsByPolicyId(ByVal ApprovalPolicyId As Integer,
                                                            Optional ByVal PageNo As Integer = 1,
                                                            Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpApprovalPaths_GetByPolicyId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        With cmd.Parameters
                            .Add("@ApprovalPolicyId", SqlDbType.Int).Value = Int32ToField(ApprovalPolicyId)
                        End With

                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "ApprovalPath")
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

