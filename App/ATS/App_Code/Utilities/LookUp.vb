Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class LookUp
    Public Shared Function GetEmployeesList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT AccountEmployeeId, FirstName + ' ' + MiddleName + ' ' + LastName AS EmployeeName FROM AccountEmployee WHERE IsDeleted<>1 UNION SELECT 0, '' "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

   

    Public Shared Function GetEmployeesListByManager(ByVal ManagerId As Integer) As DataTable

        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString

        Dim table As New DataTable()


        Try
            Dim oCon As SqlConnection = New SqlConnection(constr)
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("SELECT AccountEmployeeId, FirstName + ' ' + MiddleName + ' ' + LastName AS EmployeeName FROM AccountEmployee WHERE IsDeleted<>1 AND EmployeeManagerId=@ManagerId UNION SELECT 0, '' ")
                Using cmd
                    'cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon

                    cmd.Parameters.Add("@ManagerId", SqlDbType.Int).Value = ManagerId

                    Dim Adapter As New SqlDataAdapter
                    ds = New DataSet
                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds)
                    table = ds.Tables(0)
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try

        Return table
    End Function

    Public Shared Function GetManagersList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT AccountEmployeeId, FirstName + ' ' + MiddleName + ' ' + LastName AS EmployeeName FROM AccountEmployee WHERE IsDeleted<>1 And AccountDepartmentId1 <> 0 and AccountRoleId<>2  UNION SELECT 0, '' "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetEmployeesListByDepartment(ByVal DepartmentId As Integer) As DataTable
        Dim ds As DataSet = New DataSet
        Dim dt As DataTable = New DataTable
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Try
            Dim oCon As SqlConnection = New SqlConnection(constr)
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("dbo.SpEmployee_GetEmployeesByDepartment")
                Using cmd
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = oCon
                    With cmd.Parameters
                        .Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId
                    End With

                    Dim Adapter As New SqlDataAdapter

                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds)
                    dt = ds.Tables(0)
                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
        Return dt
    End Function

    Public Shared Function GetEmployeesListByDepartmentold(Departmentid As Integer) As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT AccountEmployeeId, FirstName + ' ' + MiddleName + ' ' + LastName AS EmployeeName FROM AccountEmployee  WHERE IsDeleted<>1 AND AccountDepartmentId=" & Departmentid & " UNION SELECT 0, '' "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetGatePassApproverEmployeesList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT AccountEmployeeId, FirstName + ' ' + MiddleName + ' ' + LastName AS EmployeeName FROM AccountEmployee WHERE IsGatePassAprover=1 UNION SELECT 0, '' "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetDepartmentsList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT AccountDepartmentId, DepartmentName FROM AccountDepartment UNION SELECT 0, '' "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetDepartmentsListByEmployee(ByVal DepartmentId As Integer) As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String
        If DepartmentId = 1 Then
            query = "SELECT AccountDepartmentId, DepartmentName FROM AccountDepartment UNION SELECT 0, '' "
        Else
            query = "SELECT AccountDepartmentId, DepartmentName FROM AccountDepartment WHERE AccountDepartmentId=" & DepartmentId & " UNION SELECT 0, '' ORDER BY  AccountDepartmentId"
        End If

        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetWorkSchedulesList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT WorkScheduleId, WorkScheduleName FROM Managements.WorkSchedule UNION SELECT 0, '' "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetWorkExceptionTypesList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT WorkExceptionTypeId, WorkExceptionTypeName FROM Managements.WorkExceptionType"
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetVacationsTypesList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT TypeId, TypeName FROM Managements.VacationType"
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetGatepassTypesList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT GatepassTypeId, GatepassTypeName FROM Managements.GatepassType"
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetRolesList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT AccountRoleId, Role FROM AccountRole UNION SELECT 0, '' "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetLocationsList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT AccountLocationId, AccountLocation FROM AccountLocation UNION SELECT 0, '' "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function

    Public Shared Function GetCountriesList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT CountryId, Country FROM SystemCountry UNION SELECT 0, '' "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function


    Public Shared Function GetAttendanceStatusList() As DataTable
        Dim ds As DataSet = New DataSet
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "SELECT StatusId, StatusName FROM Logs.AttendanceStatus WHERE StatusId NOT IN(100,15,10,50) "
        Dim da As New System.Data.SqlClient.SqlDataAdapter(query, constr)
        Dim table As New DataTable()

        da.Fill(table)
        Return table
    End Function


    Public Shared Function FillDepartmentddl(ByRef ddl As DropDownList, ByVal UserId As Integer) As Boolean

        Return True
    End Function

End Class
