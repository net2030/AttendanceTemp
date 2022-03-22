Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Web.UI.WebControls
Imports System.Text

Public Class DBUtilities
    Public Const SQL_SCRIPT_PATH As String = "~\App_Data\ExecuteScript.sql"
    Public Const SQL_SCRIPT_PATH_ES2 As String = "~\App_Data\ExecuteScript2.sql"
    Public Const SQL_SCRIPT_PATH_ES3 As String = "~\App_Data\ExecuteScript3.sql"
    Public Const SQL_SCRIPT_PATH_DS1 As String = "~/Scripts/DefragTimeLiveDB.sql"
    Public Const SQL_SCRIPT_PATH_RENAME As String = "~\App_Data\ExecuteScript.run"
    Public Const SQL_SCRIPT_NEW_DATABASE As String = "~/Scripts/CreateDatabase.sql"
    Public Const SQL_NEW_DATABASE_ADDRESS As String = "~/App_Data"
    Public Const DEFAULT_CONNECTION_STRING As String = "Data Source=.\TimeLive;Initial Catalog=TimeLive;User ID=sa;Password=1!Timelive"
    Public Const DEFAULT_DATABASE As String = "TimeLive"
    Public Shared Sub SetRowForInserting(ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs)
        If Not System.Web.HttpContext.Current.Session("AccountEmployeeId") Is Nothing Then
            e.InputParameters("CreatedByEmployeeId") = System.Web.HttpContext.Current.Session("AccountEmployeeId")
            e.InputParameters("ModifiedByEmployeeId") = System.Web.HttpContext.Current.Session("AccountEmployeeId")
        Else
            e.InputParameters("CreatedByEmployeeId") = 1
            e.InputParameters("ModifiedByEmployeeId") = 1

        End If
    End Sub
    Public Shared Sub SetHardcodedSessionValues()
        System.Web.HttpContext.Current.Session.Add("AccountId", 232)
        System.Web.HttpContext.Current.Session.Add("AccountRoleId", 367)
    End Sub
    Public Shared Sub SetRowForUpdating(ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs)
        e.InputParameters("ModifiedByEmployeeId") = DBUtilities.GetModifiedByEmployeeId
    End Sub
    Public Shared Function GetModifiedByEmployeeId() As Integer
        If Not System.Web.HttpContext.Current.Session("AccountEmployeeId") Is Nothing Then
            Return CInt(System.Web.HttpContext.Current.Session("AccountEmployeeId"))
        Else
            Return 1
        End If
    End Function
    Public Shared Function GetSessionAccountRoleId() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("AccountRoleId") Is Nothing Then
            Return CInt(System.Web.HttpContext.Current.Session("AccountRoleId"))
        Else
            Return 39
        End If
    End Function
    Public Shared Function IsApplicationContext() As Boolean
        If System.Web.HttpContext.Current Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function GetSessionAccountEmployeeId() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("AccountEmployeeId") Is Nothing Then
            Return CInt(System.Web.HttpContext.Current.Session("AccountEmployeeId"))
        Else
            Return 39
        End If
    End Function
    Public Shared Function GetCurrentAccountId() As Integer
        If DBUtilities.IsApplicationContext AndAlso System.Web.HttpContext.Current.Session Is Nothing Then
            If UIUtilities.GetApplicationMode = "Installed" Then
                Return DBUtilities.GetInstalledAccountId
            Else
                Return 64
            End If
        ElseIf DBUtilities.IsApplicationContext AndAlso Not System.Web.HttpContext.Current.Session("AccountId") Is Nothing Then
            Return CInt(System.Web.HttpContext.Current.Session("AccountId"))
        Else
            If UIUtilities.GetApplicationMode = "Installed" Then
                Return DBUtilities.GetInstalledAccountId
            Else
                Return 64
            End If
        End If
    End Function
    Public Shared Function GetSessionAccountId() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 64
        ElseIf Not System.Web.HttpContext.Current.Session("AccountId") Is Nothing Then
            Return CInt(System.Web.HttpContext.Current.Session("AccountId"))
        Else
            Return 64
        End If
    End Function

    Public Shared Function GetFromEmailAddressDisplayName() As String
        Dim FromEmailAddressDisplayName As String
        If System.Configuration.ConfigurationManager.AppSettings("APPLICATION_NAME") Is Nothing Then
            FromEmailAddressDisplayName = "TimeLive"
        Else
            FromEmailAddressDisplayName = System.Configuration.ConfigurationManager.AppSettings("APPLICATION_NAME")
        End If
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return FromEmailAddressDisplayName
        ElseIf Not System.Web.HttpContext.Current.Session("FromEmailAddressDisplayName") Is Nothing Then
            Return CStr(System.Web.HttpContext.Current.Session("FromEmailAddressDisplayName"))
        Else
            Return FromEmailAddressDisplayName
        End If
    End Function

    Public Shared Function GetEmployeeNameWithCode() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return ""
        ElseIf Not System.Web.HttpContext.Current.Session("EmployeeNameWithCode") Is Nothing Then
            Return CStr(System.Web.HttpContext.Current.Session("EmployeeNameWithCode"))
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetFromEmailAddress() As String
        Dim FromEmailAddress As String
        If System.Configuration.ConfigurationManager.AppSettings("APPLICATION_NAME") Is Nothing Then
            FromEmailAddress = "no-reply@LiveTecs.com"
        Else
            FromEmailAddress = "no-reply@" & System.Configuration.ConfigurationManager.AppSettings("APPLICATION_NAME") & ".com"
        End If
        If Not System.Web.HttpContext.Current Is Nothing Then
            If System.Web.HttpContext.Current.Session Is Nothing Then
                Return FromEmailAddress
            ElseIf Not System.Web.HttpContext.Current.Session("FromEmailAddress") Is Nothing Then
                Return CStr(System.Web.HttpContext.Current.Session("FromEmailAddress"))
            Else
                Return FromEmailAddress
            End If
        Else
            Return FromEmailAddress
        End If
    End Function

    Public Shared Function IsDefaultConnectionStringSetup() As Boolean
        If DBUtilities.GetConnectionString = DBUtilities.DEFAULT_CONNECTION_STRING Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function IsDefaultDatabaseExist() As Boolean
        Dim sql As String = "SELECT count(name) FROM master.dbo.sysdatabases WHERE [name]=" & "'" & DBUtilities.DEFAULT_DATABASE & "'"
        If DBUtilities.ExecuteCommand(sql, DBUtilities.GetMasterDatabaseConnection, True) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function VerifyMasterDatabaseConnection() As Boolean
        Try
            DBUtilities.GetMasterDatabaseConnection()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function GetMasterDatabaseConnection(Optional ByVal DBConnection As SqlConnection = Nothing) As SqlConnection
        Dim Cn As New SqlConnection(DBUtilities.GetConnectionStringWithoutDatabaseName(DBConnection))
        Cn.Open()
        Return Cn
    End Function
    
    Public Shared Function IsDatabaseInstanceExist() As Boolean
        Try
            DBUtilities.GetMasterDatabaseConnection()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function VerifyTimeLiveConnection() As Boolean
        Try
            DBUtilities.GetTimeLiveDBConnection()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Sub CreateUserDatabase(ByVal DatabaseName As String, ByVal CN As SqlConnection)
        If DatabaseName <> "" Then
            Dim sql As String = "Create Database " & DatabaseName & " COLLATE SQL_Latin1_General_CP1_CI_AS"
            DBUtilities.ExecuteCommand(sql, DBUtilities.GetMasterDatabaseConnection(CN))
        End If
    End Sub
   
    Public Shared Sub CreateUserDatabaseLoginPassword(ByVal Login As String, ByVal Password As String, ByVal DBName As String, ByVal CN As SqlConnection)
        Dim sql1 As String = "Use [master] IF Not EXISTS(SELECT * FROM syslogins WHERE name = '" & Login & "') begin "
        Dim sql As String = sql1 & "EXEC master.dbo.sp_addlogin @loginame = N'" & Login & "', @passwd = N'" & Password & "' End"
        DBUtilities.ExecuteCommand(sql, DBUtilities.GetMasterDatabaseConnection(CN))
    End Sub
    Public Shared Sub ChangeDefaultDatabaseForLogin(ByVal Login As String, ByVal DBName As String, ByVal CN As SqlConnection)
        Dim sql As String = " EXEC master.dbo.sp_defaultdb @loginame= N'" & Login & "', @defdb= N'" & DBName & "'"
        DBUtilities.ExecuteCommand(sql, CN)
    End Sub
    Public Shared Sub CreateDatabaseSecurityUser(ByVal Login As String, ByVal CN As SqlConnection)
        Dim sql As String = "EXEC dbo.sp_grantdbaccess @loginame = N'" & Login & "', @name_in_db = N'" & Login & "' EXEC sp_addrolemember N'db_owner', N'" & Login & "'"
        DBUtilities.ExecuteCommand(sql, CN)
    End Sub
    Public Shared Function GetConnectionString() As String
        Return System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
    End Function
    Public Shared Function GetConnectionStringByDatabaseName(ByVal DatabaseName As String) As String
        Dim con As New SqlConnection(DBUtilities.GetConnectionString)
        Return DBUtilities.GetConnectionString().Replace("Initial Catalog=" & con.Database.ToString & ";", "Initial Catalog=" & DatabaseName & ";")
    End Function
    Public Shared Function GetUserConnectionString(ByVal CN As SqlConnection) As String
        Return CN.ConnectionString
    End Function
    Public Shared Function GetConnectionStringWithoutDatabaseName(Optional ByVal DBConnection As SqlConnection = Nothing) As String
        If Not DBConnection Is Nothing Then
            Dim conn As SqlConnection = DBConnection
            Return DBUtilities.GetUserConnectionString(conn).Replace("Initial Catalog=" & conn.Database.ToString & ";", "")
        Else
            Dim con As New SqlConnection(DBUtilities.GetConnectionString)
            Return DBUtilities.GetConnectionString().Replace("Initial Catalog=" & con.Database.ToString & ";", "")
        End If
    End Function
    Public Shared Function GetConnectionStringDatabaseName() As String
        Dim con As New SqlConnection(DBUtilities.GetConnectionString)
        Return con.Database.ToString
    End Function
    Public Shared Sub BackupDatabase(ByVal FolderName As String)
        Dim cn As New SqlConnection(DBUtilities.GetConnectionString)
        Dim sql As String = "BACKUP DATABASE [" & DBUtilities.GetConnectionStringDatabaseName & "] TO  DISK = '" & FolderName & "TimeLiveDBBackup.bak" & "' WITH FORMAT, INIT,  NAME = N'" & DBUtilities.GetConnectionStringDatabaseName & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
        DBUtilities.ExecuteCommand(sql, cn)
    End Sub
    Public Shared Sub BackupDatabaseUserAccount(ByVal FolderName As String)
        Dim cn As New SqlConnection(DBUtilities.GetConnectionString)
        Dim sql As String = "DBCC SHRINKDATABASE" & "(" & "N'" & DBUtilities.GetSessionAccountId & "'" & "," & 1 & ")"
        DBUtilities.ExecuteCommand(sql, cn)
        Dim sql1 As String = "BACKUP DATABASE [" & DBUtilities.GetSessionAccountId & "] TO  DISK = '" & FolderName & DBUtilities.GetSessionAccountId & ".bak" & "' WITH FORMAT, INIT,  NAME = N'" & DBUtilities.GetSessionAccountId & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
        DBUtilities.ExecuteCommand(sql1, cn)
    End Sub
    Public Shared Sub DeleteDatabaseUserAccount(ByVal FolderName As String)
        Dim cn As New SqlConnection(DBUtilities.GetConnectionString)
        Dim sql As String = "ALTER DATABASE [" & DBUtilities.GetSessionAccountId & "]" & " SET  SINGLE_USER WITH ROLLBACK IMMEDIATE"
        DBUtilities.ExecuteCommand(sql, cn)
        Dim sql1 As String = "DROP DATABASE [" & DBUtilities.GetSessionAccountId & "]"
        DBUtilities.ExecuteCommand(sql1, cn)
    End Sub
    Public Shared Sub CreateDatabaseFromBackupAndRestore(ByVal FolderName As String)
        Dim cn As New SqlConnection(DBUtilities.GetConnectionString)
        Dim sql As String = "Create Database [" & DBUtilities.GetSessionAccountId & "] ON  PRIMARY (NAME = '" & DBUtilities.GetSessionAccountId & "', FILENAME = '" & FolderName & DBUtilities.GetSessionAccountId & ".mdf')  LOG ON (NAME = '" & DBUtilities.GetSessionAccountId & ".log', FILENAME = '" & FolderName & DBUtilities.GetSessionAccountId & "_log.ldf')"
        Dim con As New SqlConnection(DBUtilities.GetConnectionString)

        Dim sqlComm As New SqlCommand("use [" & DBUtilities.GetConnectionStringDatabaseName & "] SELECT name, filename FROM sys.sysfiles", con)
        con.Open()
        Dim r As SqlDataReader = sqlComm.ExecuteReader()
        Dim log1 As Integer = 1
        Dim sql1 As String
        Dim mdfname As String
        Dim ldfname As String
        While r.Read()

            If log1 = 1 Then
                mdfname = CStr(r("name"))
                Dim mdffilename As String = CStr(r("filename"))
            Else
                ldfname = CStr(r("name"))
                Dim ldffilename As String = CStr(r("filename"))
            End If
            sql1 = "RESTORE DATABASE [" & DBUtilities.GetSessionAccountId & "] FROM  DISK = N'" & FolderName & "TimeLiveDBBackup.bak" & _
                    "' WITH  FILE = 1, " & _
                    " MOVE N'" & mdfname & "' TO N'" & FolderName & DBUtilities.GetSessionAccountId & ".mdf', " & _
                    "MOVE N'" & ldfname & "' TO N'" & FolderName & DBUtilities.GetSessionAccountId & "_log.ldf',  NOUNLOAD, Replace, STATS = 10"
            log1 = log1 + 1
        End While
        'Dim sql1 As String = "RESTORE DATABASE [" & DBUtilities.GetSessionAccountId & "] FROM  DISK = N'" & FolderName & "TimeLiveDBBackup.bak" & "' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10"
        sqlComm.CommandTimeout = 90000
        DBUtilities.ExecuteCommand(sql1, cn)
        r.Close()
    End Sub
    Public Shared Sub DeleteBackupFile(ByVal tempfolder As String)
        If Not System.IO.Directory.Exists(tempfolder) Then
            Exit Sub
        Else
            Dim names As String() = System.IO.Directory.GetFiles(tempfolder, "*.bak*", SearchOption.AllDirectories)
            For Each file As String In names
                'If DateDiff("d", FileDateTime(file), Now) >= 1 Then
                System.IO.File.Delete(file)
                'End If
            Next
        End If
    End Sub
    
    Public Shared Function IsTableExistsInDatabase(ByVal TableName As String) As Boolean
        Dim cn As New SqlConnection(DBUtilities.GetConnectionString)
        Dim sql As String = "select Count(*) from information_schema.tables where table_name = '" & TableName & "' and table_type = 'BASE TABLE'"
        If DBUtilities.ExecuteCommand(sql, cn, True) > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function ExecuteCommand(ByVal strSQL As String, Optional ByVal DBConnection As SqlConnection = Nothing, Optional ByVal IsExecuteScalar As Boolean = False) As Integer
        Dim objConnection As SqlConnection
        If DBConnection Is Nothing Then
            objConnection = New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString)
            objConnection.Open()
        Else
            objConnection = DBConnection
            If objConnection.State = ConnectionState.Closed Then
                objConnection.Open()
            End If
        End If
        Dim sqlCommand As New SqlClient.SqlCommand
        sqlCommand.Connection = objConnection
        sqlCommand.CommandText = strSQL
        sqlCommand.CommandTimeout = 1000
        Dim recordsAffected As Integer
        If IsExecuteScalar = False Then
            recordsAffected = sqlCommand.ExecuteNonQuery()
        Else
            recordsAffected = CInt(sqlCommand.ExecuteScalar())
        End If
        objConnection.Close()
        Return recordsAffected
    End Function
    Public Shared Function GetTimeLiveDBConnection() As SqlConnection
        Dim objConnection As SqlConnection
        objConnection = New SqlConnection(DBUtilities.GetConnectionString)
        objConnection.Open()
        Return objConnection
    End Function
    Private Shared Function StrToByte(ByVal textdata As String) As Byte()
        Dim encoding As New System.Text.ASCIIEncoding()
        Return encoding.GetBytes(textdata)
    End Function
    Public Shared Function EncryptPasswordInHash(ByVal password As String) As String
        Dim EncodedPassword As String = password
        Dim Hash As New HMACSHA1()
        Hash.Key = StrToByte("zooooooooooo123")
        EncodedPassword = Convert.ToBase64String(Hash.ComputeHash(Encoding.Unicode.GetBytes(password)))
        Return EncodedPassword
    End Function




    Public Shared Function GetPageSize() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 10
        ElseIf Not System.Web.HttpContext.Current.Session("PageSize") Is Nothing Then
            Return CInt(System.Web.HttpContext.Current.Session("PageSize"))
        Else
            Return 10
        End If
    End Function
    Public Shared Function GetInstalledAccountId() As Integer

        Return 1

    End Function
    Public Shared Function IsInstalledAccountExists() As Boolean

        Return True

    End Function











    Public Shared Function GetPasswordExpiryPeriod() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "0"
        ElseIf Not System.Web.HttpContext.Current.Session("PasswordExpiryPeriod") Is Nothing Then
            Return CStr(System.Web.HttpContext.Current.Session("PasswordExpiryPeriod"))
        Else
            Return "0"
        End If
    End Function
    Public Shared Function IsTimeLiveMobileLogin() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("IsTimeLiveMobile") Is Nothing Then
            Return CBool(System.Web.HttpContext.Current.Session("IsTimeLiveMobile"))
        Else
            Return False
        End If
    End Function
    Public Shared Function IsAccountIdExistInSession() As Boolean
        If DBUtilities.IsApplicationContext Then
            If Not System.Web.HttpContext.Current.Session("AccountId") Is Nothing Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Shared Function GetSessionEmailAddress() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return ""
        ElseIf Not System.Web.HttpContext.Current.Session("EmailAddress") Is Nothing Then
            Return CStr(System.Web.HttpContext.Current.Session("EmailAddress"))
        Else
            Return ""
        End If
    End Function
    Public Shared Function GetSessionEmployeeName() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return ""
        ElseIf Not System.Web.HttpContext.Current.Session("AccountEmployeeFullName") Is Nothing Then
            Return CStr(System.Web.HttpContext.Current.Session("AccountEmployeeFullName"))
        Else
            Return ""
        End If
    End Function









    Public Shared Function EnablePasswordComplexity() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("EnablePasswordComplexity") Is Nothing Then
            Return CBool(System.Web.HttpContext.Current.Session("EnablePasswordComplexity"))
        Else
            Return True
        End If
    End Function

















End Class