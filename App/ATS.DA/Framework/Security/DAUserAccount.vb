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
    ''' <summary>
    '''  
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DAUserAccount
        Inherits DataAccessBase(Of UserAccount)

#Region " Constructors "
        Public Sub New()
            MyBase.new()
        End Sub
#End Region

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpUserAccount_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = FieldToint32(Id)

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
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As UserAccount, _
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
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Users.UserAccount"
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
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "UserAccount")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If MyBase.RowsEffected > 0 Then
                MyBase.GetPagesTotal("Users.UserAccount", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return MyBase.Dataset
        End Function

        Public Overloads Overrides Function Load( _
        ByVal DataEntity As UserAccount, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of UserAccount)
            Dim List As New List(Of UserAccount)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpCommon_GetDataPage")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Users.UserAccount"
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = SqlSelect(DataEntity)
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = SqlOrderBy(DataEntity)
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = SqlWhere(DataEntity)
                            .Add("@Group", SqlDbType.NVarChar, -1).Value = String.Empty
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim oUserAccount As UserAccount
                        While oReader.Read
                            oUserAccount = New UserAccount
                            With oUserAccount
                                .UserAccountId = FieldToint32(oReader, 1)
                                .windowsAccountName = FieldToString(oReader, 2)
                                .UserName = FieldToString(oReader, 3)
                                .Email = FieldToString(oReader, 4)
                                .IsActive = FieldToBoolean(oReader, 5)

                                .AddedUserAccountId = FieldToint32(oReader, 6)
                                .UpdatedUserAccountId = FieldToint32(oReader, 7)
                                .AddedDate = FieldToDateTime(oReader, 8)
                                .UpdatedDate = FieldToDateTime(oReader, 9)
                                .VersionNo = FieldToByte(oReader, 10)
                            End With
                            List.Add(oUserAccount)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If List.Count > 0 Then
                MyBase.GetPagesTotal("Users.UserAccount", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return List
        End Function


        Public Overrides Function Find(ByVal Id As Integer) As UserAccount
            Dim oUserAccount As New UserAccount

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpUserAccount_GetUserAccountById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With oUserAccount
                                .UserAccountId = FieldToint32(oReader, 0)
                                .windowsAccountName = FieldToString(oReader, 1)
                                .UserName = FieldToString(oReader, 2)
                                .IsActive = FieldToBoolean(oReader, 3)
                                .Email = FieldToString(oReader, 4)
                                .EmployeeId = FieldToint32(oReader, 5)

                                .AddedUserAccountId = FieldToint32(oReader, 6)
                                .UpdatedUserAccountId = FieldToint32(oReader, 7)
                                .AddedDate = FieldToDateTime(oReader, 8)
                                .UpdatedDate = FieldToDateTime(oReader, 9)
                                .VersionNo = FieldToByte(oReader, 10)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return oUserAccount
        End Function
        Public Function FindByEmployeeId(ByVal EmployeeId As Integer) As UserAccount
            Dim oUserAccount As New UserAccount

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpUserAccount_GetUserByEmployeeId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With oUserAccount
                                .UserAccountId = FieldToint32(oReader, 0)
                                .windowsAccountName = FieldToString(oReader, 1)
                                .UserName = FieldToString(oReader, 2)
                                .IsActive = FieldToBoolean(oReader, 3)
                                .Email = FieldToString(oReader, 4)
                                .EmployeeId = FieldToint32(oReader, 5)

                                .AddedUserAccountId = FieldToint32(oReader, 6)
                                .UpdatedUserAccountId = FieldToint32(oReader, 7)
                                .AddedDate = FieldToDateTime(oReader, 8)
                                .UpdatedDate = FieldToDateTime(oReader, 9)
                                .VersionNo = FieldToByte(oReader, 10)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return oUserAccount
        End Function
        Public Overloads Overrides Function Load() _
        As System.Collections.Generic.List(Of UserAccount)
            Dim List As New List(Of UserAccount)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WindowsAccountName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UserName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "EMail, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsActive, ", Environment.NewLine)

            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.UserAccount ", Environment.NewLine)
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim eUserAccount As UserAccount
                        While oReader.Read
                            eUserAccount = New UserAccount
                            With eUserAccount
                                .UserAccountId = FieldToint32(oReader, 0)
                                .windowsAccountName = FieldToString(oReader, 1)
                                .UserName = FieldToString(oReader, 2)
                                .Email = FieldToString(oReader, 3)
                                .IsActive = FieldToBoolean(oReader, 4)

                                .AddedUserAccountId = FieldToint32(oReader, 5)
                                .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                .AddedDate = FieldToDateTime(oReader, 7)
                                .UpdatedDate = FieldToDateTime(oReader, 8)
                                .VersionNo = FieldToByte(oReader, 9)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(eUserAccount)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Public Function LoadDepartmentUsers(ByVal UserAccountId As Integer) _
        As System.Collections.Generic.List(Of UserAccount)
            Dim List As New List(Of UserAccount)

           
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpUserAccount_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim eUserAccount As UserAccount
                        While oReader.Read
                            eUserAccount = New UserAccount
                            With eUserAccount
                                .UserAccountId = FieldToint32(oReader, 0)
                                .windowsAccountName = FieldToString(oReader, 1)
                                .UserName = FieldToString(oReader, 2)
                                .Email = FieldToString(oReader, 3)
                                .IsActive = FieldToBoolean(oReader, 4)
                                .AddedUserAccountId = FieldToint32(oReader, 5)
                                .UpdatedUserAccountId = FieldToint32(oReader, 6)
                                .AddedDate = FieldToDateTime(oReader, 7)
                                .UpdatedDate = FieldToDateTime(oReader, 8)
                                .VersionNo = FieldToByte(oReader, 9)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(eUserAccount)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As UserAccount) As String
            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "UserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WindowsAccountName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UserName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "EMail, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsActive, ", Environment.NewLine)

            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As UserAccount) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.WhereFilter
                Case UserAccount.FilterWhere.PrimaryKey
                    sb.AppendFormat("{0}{1}", "UserAccountId = " & Entity.UserAccountId.ToString, Environment.NewLine)

                Case UserAccount.FilterWhere.UserName


                Case UserAccount.FilterWhere.EMail

                Case UserAccount.FilterWhere.IsActive

            End Select

            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As UserAccount) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.OrderByFilter
                Case UserAccount.FilterOrderBy.None
                    sb.AppendFormat("{0}{1}", "UserAccountId = " & Entity.UserAccountId.ToString, Environment.NewLine)

            End Select

            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "

        Public Function Add( _
        ByVal WindowsAccountName As String, _
        ByVal UserName As String, _
        ByVal Email As String, _
        ByVal IsActive As Boolean, _
        ByVal EmployeeId As Integer, _
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpUserAccount_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@WindowsAccountName", SqlDbType.NVarChar, 50).Value = StringToField(WindowsAccountName)
                            .Add("@UserName", SqlDbType.VarChar, 50).Value = StringToField(UserName)
                            .Add("@Email", SqlDbType.NVarChar, 100).Value = StringToField(Email)
                            .Add("@IsActive", SqlDbType.Bit).Value = BooleanToField(IsActive)
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@AddedUserAccountId", SqlDbType.Int).Value = Int32ToField(AddedUserAccountId)

                            .Add(New SqlParameter("@UserAccountId", SqlDbType.Int))
                            .Item("@UserAccountId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@UserAccountId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function

        Public Function Update( _
        ByVal UserAccountId As Integer, _
        ByVal WindowsAccountName As String, _
        ByVal UserName As String, _
        ByVal Email As String, _
        ByVal IsActive As Boolean, _
        ByVal UpdatedUserAccountId As Integer, _
        ByVal version As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpUserAccount_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = FieldToint32(UserAccountId)
                            .Add("@WindowsAccountName", SqlDbType.NVarChar, 50).Value = StringToField(WindowsAccountName)
                            .Add("@UserName", SqlDbType.VarChar, 50).Value = StringToField(UserName)
                            .Add("@Email", SqlDbType.NVarChar, 100).Value = StringToField(Email)
                            .Add("@IsActive", SqlDbType.Bit).Value = BooleanToField(IsActive)
                            .Add("@UpdatedUserAccountId", SqlDbType.Int).Value = Int32ToField(UpdatedUserAccountId)
                            .Add("@VersionNo", SqlDbType.Timestamp).Value = version

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
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function
        Public Function GetUserName(ByVal Id As Integer) As String
            Dim strUserName As String = String.Empty

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UserName ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.UserAccount ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE UserAccountId = " & Id.ToString, Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        strUserName = FieldToString(cmd.ExecuteScalar())
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return strUserName
        End Function
        Public Function FindByWindowsAccountName(ByVal WindowsAccountName As String) As UserAccount
            Dim oUserAccount As New UserAccount
            oUserAccount.IsFound = False
            oUserAccount.IsConnectionSucceeded = True

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WindowsAccountName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UserName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "EMail, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsActive, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "EmployeeId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.UserAccount ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE WindowsAccountName = " & Str2Field(WindowsAccountName), Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With oUserAccount
                                .UserAccountId = FieldToint32(oReader, 0)
                                .windowsAccountName = FieldToString(oReader, 1)
                                .UserName = FieldToString(oReader, 2)
                                .Email = FieldToString(oReader, 3)
                                .IsActive = FieldToBoolean(oReader, 4)
                                .EmployeeId = FieldToint32(oReader, 5)

                                .AddedUserAccountId = FieldToint32(oReader, 6)
                                .UpdatedUserAccountId = FieldToint32(oReader, 7)
                                .AddedDate = FieldToDateTime(oReader, 8)
                                .UpdatedDate = FieldToDateTime(oReader, 9)
                                .VersionNo = FieldToByte(oReader, 10)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                                .IsFound = True
                                .IsConnectionSucceeded = True
                            End With
                        End If
                    End Using
                End Using
            Catch ex As SqlException
                Dim SQLExp As SqlException = CType(ex, SqlException)
                If SQLExp.Errors(0).Number = 53 Then
                    oUserAccount.IsConnectionSucceeded = False
                End If
                Dim strEx As String = ex.Message
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return oUserAccount
        End Function
        Public Function GetUsersByDepartmentIdDataset( _
        ByVal DepartmentId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpUserAccount_GetByDepartment")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize

                            .Add(New SqlParameter("@PagesTotal", SqlDbType.Int))
                            .Item("@PagesTotal").Direction = ParameterDirection.Output
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "UserAccount")

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
