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
    Public Class DAEmployee
        Inherits DataAccessBase(Of Employee)


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
                    Dim cmd As New SqlCommand("Employees.SpEmployee_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = FieldToint32(Id)

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
        ByVal DataEntity As Employee, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Return Nothing
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of Employee)
            Dim List As New List(Of Employee)
            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As Employee, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of Employee)
            Dim List As New List(Of Employee)
            Return List
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As Employee
            Dim oEmployee As New Employee

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetByEmployeeId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(Id)

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
                            With oEmployee
                                .EmployeeId = FieldToint32(oReader, 0)

                                .FirstName = FieldToString(oReader, 1)
                                .MiddleName = FieldToString(oReader, 2)
                                .LastName = FieldToString(oReader, 3)
                                ' .EmployeeName = .FirstName + " " + .MiddleName + " " + .LastName
                                .ManagerId = FieldToint32(oReader, 4)
                                .JobTitle = FieldToString(oReader, 5)
                                .PositionTypeId = FieldToint32(oReader, 6)
                                .PositionId = FieldToint32(oReader, 7)
                                .BadgeNo = FieldToString(oReader, 8)
                                .DepartmentId = FieldToint32(oReader, 9)
                                .NationalityId = FieldToint32(oReader, 10)
                                .HireDate = FieldToDate(oReader, 11)
                                .IsActive = FieldToBoolean(oReader, 12)
                                .IsFingerRegistered = FieldToBoolean(oReader, 13)
                                .Picture = FieldToByte(oReader, 14)
                                .ImageFileName = FieldToString(oReader, 15)
                                .ActionId = FieldToint32(oReader, 16)
                                .IsGatepassApproval = FieldToBoolean(oReader, 17)

                                .LocationId = FieldToint32(oReader, 18)
                                .RoleId = FieldToint32(oReader, 19)
                                .UserName = FieldToString(oReader, 20)
                                .EmailAddress = FieldToString(oReader, 21)
                                .IsForcePasswordChange = FieldToBoolean(oReader, 22)
                                .WorkScheduleId = FieldToint32(oReader, 23)

                                .AddedUserAccountId = FieldToint32(oReader, 24)
                                .UpdatedUserAccountId = FieldToint32(oReader, 25)
                                .AddedDate = FieldToDate(oReader, 26)
                                .UpdatedDate = FieldToDate(oReader, 27)
                                .VersionNo = FieldToByte(oReader, 28)




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
        Protected Overrides Function SqlSelect(ByVal Entity As Employee) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As Employee) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As Employee) As String
            Dim sb As New System.Text.StringBuilder
            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal FirstName As String,
        ByVal MiddleName As String,
        ByVal LastName As String,
        ByVal ManagerId As Integer,
        ByVal JobTitle As String,
        ByVal PositionTypeId As Integer,
        ByVal PositionId As Integer,
        ByVal BadgeNo As String,
        ByVal DepartmentId As Integer,
        ByVal NationalityId As Integer,
        ByVal HireDate As Date,
        ByVal IsActive As Boolean,
        ByVal IsFingerRegistered As Boolean,
        ByVal Picture As Byte(),
        ByVal ImageFileName As String,
        ByVal ActionId As Integer,
        ByVal IsGatepassApproval As Boolean,
        ByVal LocationId As Integer,
        ByVal RoleId As Integer,
        ByVal UserName As String,
        ByVal Password As String,
        ByVal EmailAddress As String,
        ByVal IsForcePasswordChange As Boolean,
        ByVal WorkScheduleId As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_Insert1")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Firstname", SqlDbType.NVarChar, 100).Value = StringToField(FirstName)
                            .Add("@MiddleName", SqlDbType.NVarChar, 100).Value = StringToField(MiddleName)
                            .Add("@LastName", SqlDbType.NVarChar, 100).Value = StringToField(LastName)
                            .Add("@ManagerId", SqlDbType.Int).Value = Int32ToField(ManagerId)
                            .Add("@JobTitle", SqlDbType.NVarChar, 100).Value = StringToField(JobTitle)
                            '.Add("@PositionTypeId", SqlDbType.Int).Value = Int32ToField(PositionTypeId)
                            '.Add("@PositionId", SqlDbType.Int).Value = Int32ToField(PositionId)
                            .Add("@BadgeNo", SqlDbType.NVarChar, 50).Value = StringToField(BadgeNo)
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@NationalityId", SqlDbType.Int).Value = Int32ToField(NationalityId)
                            .Add("@HireDate", SqlDbType.Date).Value = DateToField(HireDate)
                            .Add("@IsActive", SqlDbType.Bit).Value = BooleanToField(IsActive)
                            .Add("@IsFingerRegistered", SqlDbType.Bit).Value = BooleanToField(IsFingerRegistered)
                            '.Add("@Picture", SqlDbType.VarBinary, -1).Value = ByteToField(Picture)
                            '.Add("@ImageFileName", SqlDbType.NVarChar, 100).Value = StringToField(ImageFileName)
                            '.Add("@ActionId", SqlDbType.Int).Value = Int32ToField(ActionId)
                            '.Add("@IsGatepassApproval", SqlDbType.Bit).Value = BooleanToField(IsGatepassApproval)

                            .Add("@LocationId", SqlDbType.Int).Value = Int32ToField(LocationId)
                            .Add("@RoleId", SqlDbType.Int).Value = Int32ToField(RoleId)
                            .Add("@UserName", SqlDbType.NVarChar, 100).Value = StringToField(UserName)
                            .Add("@Password", SqlDbType.NVarChar, 100).Value = StringToField(Password)
                            .Add("@EmailAddress", SqlDbType.NVarChar, 100).Value = StringToField(EmailAddress)
                            .Add("@IsForcePasswordChange", SqlDbType.Int).Value = BooleanToField(IsForcePasswordChange)
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = Int32ToField(WorkScheduleId)

                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@EmployeeId", SqlDbType.Int))
                            .Item("@EmployeeId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@EmployeeId").Value)
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
        ByVal EmployeeId As Integer,
        ByVal FirstName As String,
        ByVal MiddleName As String,
        ByVal LastName As String,
        ByVal ManagerId As Integer,
        ByVal JobTitle As String,
        ByVal PositionTypeId As Integer,
        ByVal PositionId As Integer,
        ByVal BadgeNo As String,
        ByVal DepartmentId As Integer,
        ByVal NationalityId As Integer,
        ByVal HireDate As Date,
        ByVal IsActive As Boolean,
        ByVal IsFingerRegistered As Boolean,
        ByVal Picture As Byte(),
        ByVal ImageFileName As String,
        ByVal ActionId As Integer,
        ByVal IsGatepassApproval As Boolean,
        ByVal LocationId As Integer,
        ByVal RoleId As Integer,
        ByVal UserName As String,
        ByVal Password As String,
        ByVal EmailAddress As String,
        ByVal IsForcePasswordChange As Boolean,
        ByVal WorkScheduleId As Integer,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_Update1")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
                            .Add("@Firstname", SqlDbType.NVarChar, 100).Value = StringToField(FirstName)
                            .Add("@MiddleName", SqlDbType.NVarChar, 100).Value = StringToField(MiddleName)
                            .Add("@LastName", SqlDbType.NVarChar, 100).Value = StringToField(LastName)
                            .Add("@ManagerId", SqlDbType.Int).Value = Int32ToField(ManagerId)
                            .Add("@JobTitle", SqlDbType.NVarChar, 100).Value = StringToField(JobTitle)
                            '.Add("@PositionTypeId", SqlDbType.Int).Value = Int32ToField(PositionTypeId)
                            '.Add("@PositionId", SqlDbType.Int).Value = Int32ToField(PositionId)
                            .Add("@BadgeNo", SqlDbType.NVarChar, 50).Value = StringToField(BadgeNo)
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@NationalityId", SqlDbType.Int).Value = Int32ToField(NationalityId)
                            .Add("@HireDate", SqlDbType.Date).Value = DateToField(HireDate)
                            .Add("@IsActive", SqlDbType.Bit).Value = BooleanToField(IsActive)
                            .Add("@IsFingerRegistered", SqlDbType.Bit).Value = BooleanToField(IsFingerRegistered)
                            '.Add("@Picture", SqlDbType.VarBinary, -1).Value = ByteToField(Picture)
                            '.Add("@ImageFileName", SqlDbType.NVarChar, 100).Value = StringToField(ImageFileName)
                            '.Add("@ActionId", SqlDbType.Int).Value = Int32ToField(ActionId)
                            '.Add("@IsGatepassApproval", SqlDbType.Bit).Value = BooleanToField(IsGatepassApproval)

                            .Add("@LocationId", SqlDbType.Int).Value = Int32ToField(LocationId)
                            .Add("@RoleId", SqlDbType.Int).Value = Int32ToField(RoleId)
                            .Add("@UserName", SqlDbType.NVarChar, 100).Value = StringToField(UserName)
                            .Add("@Password", SqlDbType.NVarChar, 100).Value = StringToField(Password)
                            .Add("@EmailAddress", SqlDbType.NVarChar, 100).Value = StringToField(EmailAddress)
                            .Add("@IsForcePasswordChange", SqlDbType.Int).Value = BooleanToField(IsForcePasswordChange)
                            .Add("@WorkScheduleId", SqlDbType.Int).Value = Int32ToField(WorkScheduleId)

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

        Public Function UpdateEmployeeManager(ByVal EmployeeId As Integer,
                                              ByVal ManagerId As Integer,
                                              ByVal UserId As Integer) As Boolean


            Dim query As String = "Update Employees.Employee Set ManagerId=@ManagerId,UpdatedUserAccountId=@UpdatedUserAccountId,UpdatedDate=Getdate() Where EmployeeId=@EmployeeId"
            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)

                oCon.Open()
                Dim com As New SqlCommand(query, oCon)


                com.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId
                com.Parameters.Add("@ManagerId", SqlDbType.Int).Value = ManagerId
                com.Parameters.Add("@UpdatedUserAccountId", SqlDbType.Int).Value = UserId


                MyBase.RowsEffected = com.ExecuteNonQuery()


                oCon.Close()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        Public Function GetAllEmployeesDataset(
        ByVal UserAccountId As Integer,
        ByVal FilterOption As enumFilterOption,
        ByVal SortOption As enumSortOption,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = FieldToint32(UserAccountId)
                            .Add("@PageNumber", SqlDbType.Int).Value = FieldToint32(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = FieldToint32(PageSize)
                            .Add("@FilterOptionNo", SqlDbType.Int).Value = FieldToint32(FilterOption)
                            .Add("@SortOptionNo", SqlDbType.Int).Value = CType(SortOption, Integer)

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
        Public Function GetEmployeesByNameDataset( _
        ByVal UserAccountId As Integer, _
        ByVal EmployeeName As String, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetEmployeesByName")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = FieldToint32(UserAccountId)
                            .Add("@EmployeeName", SqlDbType.NVarChar, 100).Value = FieldToString(EmployeeName)
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
        Public Function GetEmployeesByBadgeDataset( _
        ByVal UserAccountId As Integer, _
        ByVal BadgeNo As String, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetEmployeesByBadgeNo")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = FieldToint32(UserAccountId)
                            .Add("@BadgeNo", SqlDbType.NVarChar, 50).Value = FieldToString(BadgeNo)
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
        Public Function GetEmployeesByIdDataset( _
        ByVal UserAccountId As Integer, _
        ByVal EmployeeId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetEmployeesByEmployeeId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = FieldToint32(UserAccountId)
                            .Add("@EmployeeId", SqlDbType.Int).Value = FieldToint32(EmployeeId)
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
        Public Function GetEmployeesByDepartmentDataset(ByVal DepartmentId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetEmployeesByDepartment")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = FieldToint32(DepartmentId)
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
        Public Function GetMilitaryEmployeesByDepartmentDataset(ByVal DepartmentId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetMilitaryEmployeesByDepartment")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = FieldToint32(DepartmentId)
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
        Public Function GetCivilianEmployeesByDepartmentDataset(ByVal DepartmentId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetCiviliansEmployeesByDepartment")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = FieldToint32(DepartmentId)
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
        Public Function GetEmployeeId(ByVal UserAccountId As Integer) As Integer
            Dim intEmployeeId As Integer = 0

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpGetEmployeeByUserId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@EmployeeId", SqlDbType.Int))
                            .Item("@EmployeeId").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        intEmployeeId = FieldToint32(cmd.Parameters("@EmployeeId").Value)
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية الوصول للبيانات"
            End Try

            Return intEmployeeId
        End Function
        Public Function GetEmployeeManager(ByVal ManagerId As Integer) As String
            Dim strManagerName As String = String.Empty

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpGetEmployeeManager")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@ManagerId", SqlDbType.Int).Value = Int32ToField(ManagerId)

                            .Add(New SqlParameter("@EmployeeName", SqlDbType.NVarChar, 100))
                            .Item("@EmployeeName").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        strManagerName = FieldToString(cmd.Parameters("@EmployeeName").Value)
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية الوصول للبيانات"
            End Try

            Return strManagerName
        End Function
        Public Function GetGatepassApprovals(ByVal UserAccountId As Integer) As DataSet
            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetApprovals")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "Employee")
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية الوصول للبيانات"
            End Try

            Return MyBase.Dataset
        End Function
        Private Function GetEmployeeScope(ByVal EmployeeId As Integer) As Integer
            Dim intDepartmentId As Integer = 0

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetDepartmentScopeId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)

                            .Add(New SqlParameter("@DepartmentId", SqlDbType.NVarChar, 100))
                            .Item("@DepartmentId").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        intDepartmentId = FieldToint32(cmd.Parameters("@DepartmentId").Value)
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية الوصول للبيانات"
            End Try

            Return intDepartmentId
        End Function
        Public Function GetAllBehaviorActionsDataset() As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Managements.SpBehaviorAction_GetAll")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "BehaviorAction")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetUnregisteredEmployees(ByVal DepartmentId As Integer, ByVal Op As Integer) As System.Data.DataSet
            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetFingerPrintNotRegistered")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@Op", SqlDbType.Int).Value = Int32ToField(Op)
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "vwEmployees")
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية الوصول للبيانات"
            End Try

            Return MyBase.Dataset
        End Function
        Public Function GetDepartmentEmployeesDataset(
        ByVal DepartmentId As Integer,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetDepartmentEmployees")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@DepartmentId", SqlDbType.Int).Value = Int32ToField(DepartmentId)
                            .Add("@PageNumber", SqlDbType.Int).Value = Int32ToField(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = Int32ToField(PageSize)

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

        Public Function GetEmployeeByUserName(ByVal UserName As String) As System.Data.DataSet
            Dim oEmployee As New Employee

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetByUserName")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserName", SqlDbType.NVarChar, 100).Value = UserName

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
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

        Public Function GetEmployeeByUserNameAndPassword(ByVal UserName As String, ByVal Password As String) As System.Data.DataSet

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetByUserNameAndPassword")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserName", SqlDbType.NVarChar, 100).Value = UserName
                            .Add("@Password", SqlDbType.NVarChar, 100).Value = Password

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
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

        Public Function GetEmployeeByUserNameAndPassword1(ByVal UserName As String, ByVal Password As String) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetByUserNameAndPassword")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@UserName", SqlDbType.NVarChar, 100).Value = UserName
                            .Add("@Password", SqlDbType.NVarChar, 100).Value = Password

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
                        End With
                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "BehaviorAction")
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return MyBase.Dataset
        End Function

        Public Function GetEmployeesByManagerDataset(
        ByVal ManagerId As Integer,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployee_GetEmployeesByManager")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@ManagerId", SqlDbType.Int).Value = Int32ToField(ManagerId)
                            .Add("@PageNumber", SqlDbType.Int).Value = Int32ToField(PageNo)
                            .Add("@PageSize", SqlDbType.Int).Value = Int32ToField(PageSize)

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

        Public Function GetAuthorizedEmployeeById(ByVal EmployeeId As Integer) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Employees.SpEmployeeGetAuthuriedByEmployeeId")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = Int32ToField(EmployeeId)
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

        Public Function AuthorizeEmployee(ByVal AuthorizorEmployeeId As Integer, ByVal AuthorizedEmployeeId As Integer) As Boolean

            If AuthorizedEmployeeId = 0 Then
                AuthorizedEmployeeId = AuthorizorEmployeeId
            End If

            Dim query As String = "Employees.SpEmployeeAuthorization"
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Dim com As New SqlCommand(query, oCon)

                com.CommandType = CommandType.StoredProcedure

                com.Parameters.Add("@Authorized", SqlDbType.Int).Value = AuthorizedEmployeeId
                com.Parameters.Add("@Authorizor", SqlDbType.Int).Value = AuthorizorEmployeeId
                oCon.Open()
                com.ExecuteNonQuery()
                oCon.Close()
            Catch ex As Exception
            End Try
            Return True
        End Function

        Public Function UnAuthorizeEmployee(ByVal AuthorizorEmployeeId As Integer, ByVal AuthorizedEmployeeId As Integer) As Boolean

            If AuthorizedEmployeeId = 0 Then
                AuthorizedEmployeeId = AuthorizorEmployeeId
            End If

            Dim query As String = "Employees.SpEmployeeUnAuthorization"
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Dim com As New SqlCommand(query, oCon)

                com.CommandType = CommandType.StoredProcedure


                com.Parameters.Add("@Authorized", SqlDbType.Int).Value = AuthorizedEmployeeId
                com.Parameters.Add("@Authorizor", SqlDbType.Int).Value = AuthorizorEmployeeId


                oCon.Open()
                com.ExecuteNonQuery()

                oCon.Close()
            Catch ex As Exception
            End Try
            Return True
        End Function


        Public Function ChangePassword(ByVal EmployeeId As Integer,
                                       ByVal Password As String,
                                       Optional ByVal EmailAddress As String = "") As Boolean

            Dim sql As String = "Update Employees.Employee Set Password=@Password"

            If EmailAddress <> "" Then
                sql = sql + ",EmailAddress=@EmailAddress"
            End If

            sql = sql + ",IsForcePasswordChange=0,UpdatedUserAccountId=@EmployeeId,UpdatedDate=Getdate() Where EmployeeId=@EmployeeId"

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)

                oCon.Open()
                Dim com As New SqlCommand(sql, oCon)
                com.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId
                com.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = Password

                If EmailAddress <> "" Then
                    com.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = EmailAddress
                End If

                com.Parameters.Add("@UpdatedUserAccountId", SqlDbType.Int).Value = EmployeeId
                MyBase.RowsEffected = com.ExecuteNonQuery()
                oCon.Close()
            Catch ex As Exception
                Return False
            End Try
            Return True

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

