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
    Public Class DATrainingEmployee
        Inherits DataAccessBase(Of TrainingEmployee)

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
        ByVal DataEntity As TrainingEmployee, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Return Nothing
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of TrainingEmployee)
            Dim List As New List(Of TrainingEmployee)
            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As TrainingEmployee, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of TrainingEmployee)
            Dim List As New List(Of TrainingEmployee)
            Return List
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As TrainingEmployee
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As TrainingEmployee) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As TrainingEmployee) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As TrainingEmployee) As String
            Throw New NotImplementedException
        End Function
#End Region

#Region " Public Methods "
        Public Function AddEmployee(
        ByVal TrainingId As Integer,
        ByVal EmployeeId As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpTrainingEmployee_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@TrainingId", SqlDbType.Int).Value = FieldToint32(TrainingId)
                            .Add("@EmployeeId", SqlDbType.Int).Value = FieldToint32(EmployeeId)
                            .Add("@UserAccountId", SqlDbType.Int).Value = FieldToint32(UserAccountId)

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
        Public Function DeleteEmployee(ByVal TrainingId As Integer, ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpTrainingEmployee_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@TrainingId", SqlDbType.Int).Value = FieldToint32(TrainingId)
                            .Add("@EmployeeId", SqlDbType.Int).Value = FieldToint32(EmployeeId)

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
        Public Function GetTrainingEmployeesDataset( _
        ByVal TrainingId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpTrainingEmployee_GetEmployees")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@TrainingId", SqlDbType.Int).Value = FieldToint32(TrainingId)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Training")


                        GetPagesTotal(FieldToint32(cmd.Parameters("@PagesTotal").Value), PageSize)
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function LoadEmployees(ByVal TrainingId As Integer) As System.Collections.Generic.List(Of TrainingEmployee)
            Dim List As New List(Of TrainingEmployee)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpTrainingEmployee_GetAllEmployees")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@TrainingId", SqlDbType.Int).Value = FieldToint32(TrainingId)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim eTrainingEmployee As TrainingEmployee
                        While oReader.Read
                            eTrainingEmployee = New TrainingEmployee
                            With eTrainingEmployee
                                .TrainingId = FieldToint32(oReader, 0)
                                .EmployeeId = FieldToint32(oReader, 1)
                            End With
                            List.Add(eTrainingEmployee)
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

