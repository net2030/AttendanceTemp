Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
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
            Return System.Web.HttpContext.Current.Session("AccountEmployeeId")
        Else
            Return 1
        End If
    End Function
    Public Shared Function GetSessionAccountRoleId() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("AccountRoleId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("AccountRoleId")
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
            Return System.Web.HttpContext.Current.Session("AccountEmployeeId")
        Else
            Return 39
        End If
    End Function
    Public Shared Function GetCurrentAccountId() As Integer
        If DBUtilities.IsApplicationContext AndAlso System.Web.HttpContext.Current.Session Is Nothing Then
            If ATS.BO.UIUtilities.GetApplicationMode = "Installed" Then
                Return DBUtilities.GetInstalledAccountId
            Else
                Return 64
            End If
        ElseIf DBUtilities.IsApplicationContext AndAlso Not System.Web.HttpContext.Current.Session("AccountId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("AccountId")
        Else
            If ATS.BO.UIUtilities.GetApplicationMode = "Installed" Then
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
            Return System.Web.HttpContext.Current.Session("AccountId")
        Else
            Return 64
        End If
    End Function
    Public Shared Function GetShowClockStartEnd() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("ShowClockStartEnd") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowClockStartEnd")
        Else
            Return True
        End If
    End Function
    Public Shared Function GetShowDescriptionInWeekView() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("ShowDescriptionInWeekView") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowDescriptionInWeekView")
        Else
            Return True
        End If
    End Function
    Public Shared Function GetShowClientInTimesheet() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowClientInTimesheet") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowClientInTimesheet")
        Else
            Return False
        End If
    End Function
    Public Shared Function GetHoursPerDay() As Double
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 8
        ElseIf Not System.Web.HttpContext.Current.Session("HoursPerDay") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("HoursPerDay")
        Else
            Return 8
        End If
    End Function
    Public Shared Function GetMinimumHoursPerDay() As Double
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("MinimumHoursPerDay") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("MinimumHoursPerDay")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetMaximumHoursPerDay() As Double
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 24
        ElseIf Not System.Web.HttpContext.Current.Session("MaximumHoursPerDay") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("MaximumHoursPerDay")
        Else
            Return 24
        End If
    End Function
    Public Shared Function GetMinimumHoursPerWeek() As Double
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("MinimumHoursPerWeek") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("MinimumHoursPerWeek")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetMaximumHoursPerWeek() As Double
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 60
        ElseIf Not System.Web.HttpContext.Current.Session("MaximumHoursPerWeek") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("MaximumHoursPerWeek")
        Else
            Return 60
        End If
    End Function
    Public Shared Function GetMinimumPercentagePerDay() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("MinimumPercentagePerDay") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("MinimumPercentagePerDay")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetMaximumPercentagePerDay() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 100
        ElseIf Not System.Web.HttpContext.Current.Session("MaximumPercentagePerDay") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("MaximumPercentagePerDay")
        Else
            Return 100
        End If
    End Function
    Public Shared Function GetMinimumPercentagePerWeek() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("MinimumPercentagePerWeek") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("MinimumPercentagePerWeek")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetMaximumPercentagePerWeek() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 500
        ElseIf Not System.Web.HttpContext.Current.Session("MaximumPercentagePerWeek") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("MaximumPercentagePerWeek")
        Else
            Return 500
        End If
    End Function
    Public Shared Function GetTimeZoneId() As Byte
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 6
        ElseIf Not System.Web.HttpContext.Current.Session("TimeZoneId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("TimeZoneId")
        Else
            Return 6
        End If
    End Function
    Public Shared Function GetEmployeeTimeZoneId() As Byte
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 6
        ElseIf Not System.Web.HttpContext.Current.Session("EmployeeTimeZoneId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("EmployeeTimeZoneId")
        Else
            Return 6
        End If
    End Function
    Public Shared Function GetCurrencyId() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 1
        ElseIf Not System.Web.HttpContext.Current.Session("CountryId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("CountryId")
        Else
            Return 1
        End If
    End Function
    Public Shared Function GetTimeEntryFormat() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("TimeEntryFormat") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("TimeEntryFormat")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetAccountCountryId() As Byte
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("CountryId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("CountryId")
        Else
            Return 233
        End If
    End Function
    Public Shared Function GetDefaultCurrencyId() As Byte
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("DefaultCurrencyId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("DefaultCurrencyId")
        Else
            Return 1
        End If
    End Function
    Public Shared Function GetAccountBaseCurrencyId() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 1
        ElseIf Not System.Web.HttpContext.Current.Session("AccountBaseCurrencyId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("AccountBaseCurrencyId")
        Else
            Return 1
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
            Return System.Web.HttpContext.Current.Session("FromEmailAddressDisplayName")
        Else
            Return FromEmailAddressDisplayName
        End If
    End Function
    Public Shared Function GetTimesheetPrintFooter() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return GetDefaultPrintFooter()
        ElseIf Not System.Web.HttpContext.Current.Session("TimesheetPrintFooter") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("TimesheetPrintFooter")
        Else
            Return GetDefaultPrintFooter()
        End If
    End Function
    Public Shared Function GetEmployeeNameWithCode() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return ""
        ElseIf Not System.Web.HttpContext.Current.Session("EmployeeNameWithCode") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("EmployeeNameWithCode")
        Else
            Return ""
        End If
    End Function
    Public Shared Function GetExpenseSheetPrintFooter() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return GetDefaultPrintFooter()
        ElseIf Not System.Web.HttpContext.Current.Session("ExpenseSheetPrintFooter") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ExpenseSheetPrintFooter")
        Else
            Return GetDefaultPrintFooter()
        End If
    End Function
    Public Shared Function GetDefaultPrintFooter() As String
        Dim DefaultExpenseSheetPrintFooter As String = Chr(13) & Chr(13) & Chr(13) & Chr(13) & Chr(13) & Chr(13)
        DefaultExpenseSheetPrintFooter = DefaultExpenseSheetPrintFooter & "________________________" & "                                                         " & "    ______________________" & Chr(13) & _
                      "      Consultant Signature" & "                                                                       " & "            Client Signature"
        Return DefaultExpenseSheetPrintFooter
    End Function
    Public Shared Function GetNumberOfBlankRowsInTimeEntry() As Short
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "2"
        ElseIf Not System.Web.HttpContext.Current.Session("NumberOfBlankRowsInTimeEntry") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("NumberOfBlankRowsInTimeEntry")
        Else
            Return "2"
        End If
    End Function
    Public Shared Function GetFromEmailAddress() As String
        Dim FromEmailAddress As String
        If System.Configuration.ConfigurationManager.AppSettings("APPLICATION_NAME") Is Nothing Then
            FromEmailAddress = "no-reply@masware.com"
        Else
            FromEmailAddress = "no-reply@" & System.Configuration.ConfigurationManager.AppSettings("APPLICATION_NAME") & ".com"
        End If
        If Not System.Web.HttpContext.Current Is Nothing Then
            If System.Web.HttpContext.Current.Session Is Nothing Then
                Return FromEmailAddress
            ElseIf Not System.Web.HttpContext.Current.Session("FromEmailAddress") Is Nothing Then
                Return System.Web.HttpContext.Current.Session("FromEmailAddress")
            Else
                Return FromEmailAddress
            End If
        Else
            Return FromEmailAddress
        End If
    End Function
    Public Shared Sub AfterInsert(ByVal objGridView As GridView)
        objGridView.DataBind()
    End Sub
    Public Shared Sub AfterUpdate(ByVal objGridView As GridView)
        objGridView.DataBind()
    End Sub
    Public Shared Function GetMinutesOfTime(ByVal TimeValue As DateTime) As Integer
        Return (TimeValue.Hour * 60) + TimeValue.Minute
    End Function
    Public Shared Function GetDateTimeOfMinutesAsString(ByVal MinutesValue As System.Nullable(Of Integer)) As String
        'If MinutesValue.HasValue = False Then
        '    Return ""
        'End If
        'Dim nHour As Integer = Math.Floor(MinutesValue.Value / 60)
        'Dim nSeconds As Integer = MinutesValue.Value - (nHour * 60)
        'GetDateTimeOfMinutesAsString = Convert.ToString(nHour).PadLeft(2, "0")
        'If LocaleUtilitiesBLL.GetCurrentTimeEntryFormatForTotalTime.IndexOf("mm") > 0 Then
        '    GetDateTimeOfMinutesAsString = GetDateTimeOfMinutesAsString & ":" & New String(CStr(nSeconds)).PadLeft(2, "0")
        'End If
        'Return GetDateTimeOfMinutesAsString
    End Function
    Public Shared Function GetHoursOfMinutesAsString(ByVal MinutesValue As System.Nullable(Of Integer)) As String
        If MinutesValue.HasValue = False Then
            Return ""
        End If
        Dim nHours As Double = Math.Round(MinutesValue.Value / 60, 2)
        GetHoursOfMinutesAsString = Convert.ToString(nHours)
        Return GetHoursOfMinutesAsString
    End Function
    Public Shared Function GetDateTimeOfMinutesAsStringForEmail(ByVal MinutesValue As System.Nullable(Of Integer)) As String
        If MinutesValue.HasValue = False Then
            Return ""
        End If
        Dim nHour As Integer = Math.Floor(MinutesValue.Value / 60)
        Dim nSeconds As Integer = MinutesValue.Value - (nHour * 60)
        GetDateTimeOfMinutesAsStringForEmail = Convert.ToString(nHour).PadLeft(2, "0")
        GetDateTimeOfMinutesAsStringForEmail = GetDateTimeOfMinutesAsStringForEmail & ":" & New String(CStr(nSeconds)).PadRight(2, "0")
        Return GetDateTimeOfMinutesAsStringForEmail
    End Function
    Public Shared Function GetTimeFromDateTime(ByVal DateTimeValue As System.Nullable(Of DateTime), Optional ByVal Format As String = "hh:mm tt") As String
        If DateTimeValue.HasValue Then
            GetTimeFromDateTime = DateTimeValue.Value.ToString(Format)
            Return GetTimeFromDateTime
        End If
    End Function
    Public Shared Function GetTimeFromDateTimeInUSCulture(ByVal DateTimeValue As System.Nullable(Of DateTime), Optional ByVal Format As String = "hh:mm tt") As String
        If DateTimeValue.HasValue Then
            GetTimeFromDateTimeInUSCulture = DateTimeValue.Value.ToString(Format, System.Globalization.CultureInfo.GetCultureInfo("en-US"))
            Return GetTimeFromDateTimeInUSCulture
        End If
    End Function
    Public Shared Function GetTimeFromDateTime24Hours(ByVal DateTimeValue As System.Nullable(Of DateTime), Optional ByVal Format As String = "hh:mm") As String
        If DateTimeValue.HasValue Then
            GetTimeFromDateTime24Hours = DateTimeValue.Value.ToString(Format)
            Return GetTimeFromDateTime24Hours
        End If
    End Function
    Public Shared Function RunSQLScript(ByVal FilePath As String, Optional ByVal DBConnection As SqlConnection = Nothing)
        Dim createStatement As String
        Dim srSqlStatement As New StreamReader(FilePath)
        createStatement = srSqlStatement.ReadToEnd
        createStatement = createStatement.Replace("GO", "~")
        Dim aCommands() As String = createStatement.Split("~")
        srSqlStatement.Close()
        Dim ErrorString As String
        For n As Integer = 0 To aCommands.Length - 1
            Try
                DBUtilities.ExecuteCommand(aCommands(n), DBConnection)
            Catch ex As Exception
                ErrorString = ErrorString + ex.Message + "</br>"
            End Try
        Next
        If ErrorString <> "" Then
            System.Web.HttpContext.Current.Response.Write("<b>Errors appeared on upgrade!. Please click on [Refresh] to retry database upgrade</b></br>")
            System.Web.HttpContext.Current.Response.Write(ErrorString)
            System.Web.HttpContext.Current.Response.End()
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
    Public Shared Sub CreateDefaultDatabase()
        If DBUtilities.IsDefaultConnectionStringSetup Then
            If DBUtilities.IsDatabaseInstanceExist() AndAlso Not DBUtilities.IsDefaultDatabaseExist Then
                Dim sql As String = "Create Database [" & DBUtilities.DEFAULT_DATABASE & "] ON  PRIMARY (NAME = '" & DBUtilities.DEFAULT_DATABASE & "', FILENAME = '" & System.Web.Hosting.HostingEnvironment.MapPath(DBUtilities.SQL_NEW_DATABASE_ADDRESS) & "\" & DBUtilities.DEFAULT_DATABASE & ".mdf')  LOG ON (NAME = '" & DBUtilities.DEFAULT_DATABASE & ".log', FILENAME = '" & System.Web.Hosting.HostingEnvironment.MapPath(DBUtilities.SQL_NEW_DATABASE_ADDRESS) & "\" & DBUtilities.DEFAULT_DATABASE & "_log.ldf')  COLLATE SQL_Latin1_General_CP1_CI_AS"
                'Dim sql As String = "Create Database " & DBUtilities.DEFAULT_DATABASE
                DBUtilities.ExecuteCommand(sql, DBUtilities.GetMasterDatabaseConnection)
                DBUtilities.RunSQLScript(System.Web.Hosting.HostingEnvironment.MapPath(DBUtilities.SQL_SCRIPT_NEW_DATABASE))
            ElseIf DBUtilities.VerifyTimeLiveConnection() = False Then
                ATS.BO.UIUtilities.RedirectToDatabaseSetupPage()
            End If
        End If
    End Sub
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
    Public Shared Sub RunCreateDatabaseScript(ByVal CN As SqlConnection)
        DBUtilities.RunSQLScript(System.Web.Hosting.HostingEnvironment.MapPath(DBUtilities.SQL_SCRIPT_NEW_DATABASE), CN)
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
    Public Shared Sub DeleteAllDataFromAnotherAccount()
        'Dim cn As New SqlConnection(DBUtilities.GetConnectionStringByDatabaseName(DBUtilities.GetSessionAccountId))
        'Dim ProcedureClass As New StoreProcedureBLL
        'Dim Procedure As String = ProcedureClass.GetDataForDelete(DBUtilities.GetSessionAccountId, cn)
        'DBUtilities.RunSQLScript(System.Web.Hosting.HostingEnvironment.MapPath(DBUtilities.SQL_SCRIPT_PATH_DS1), cn)
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
            recordsAffected = sqlCommand.ExecuteScalar()
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
    Public Shared Function IsShowWorkTypeInTimeSheet() As Boolean
        Return System.Web.HttpContext.Current.Session("ShowWorkTypeInTimeSheet")
    End Function
    Public Shared Function IsShowCostCenterInTimeSheet() As Boolean
        Return System.Web.HttpContext.Current.Session("ShowCostCenterInTimeSheet")
    End Function
    Public Shared Function GetSessionEmployeeWeekStartDay() As Short
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "1"
        ElseIf Not System.Web.HttpContext.Current.Session("EmployeeWeekStartDay") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("EmployeeWeekStartDay")
        Else
            Return "1"
        End If
    End Function
    Public Shared Function GetDefaultTimeEntryMode() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "Period View"
        ElseIf Not System.Web.HttpContext.Current.Session("DefaultTimeEntryMode") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("DefaultTimeEntryMode")
        Else
            Return "Period View"
        End If
    End Function
    Public Shared Function GetPageSize() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "10"
        ElseIf Not System.Web.HttpContext.Current.Session("PageSize") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("PageSize")
        Else
            Return "10"
        End If
    End Function
    Public Shared Function GetInstalledAccountId() As Integer
        'Dim AccountBLL As New AccountBLL
        'Dim dtAccount As TimeLiveDataSet.AccountDataTable = AccountBLL.GetAccounts()
        'Dim drAccount As TimeLiveDataSet.AccountRow
        'If dtAccount.Rows.Count > 0 Then
        '    drAccount = dtAccount.Rows(0)
        '    Return drAccount.AccountId
        'Else
        Return 1
        'End If
    End Function
    Public Shared Function IsInstalledAccountExists() As Boolean
        'Dim AccountBLL As New AccountBLL
        'Dim dtAccount As TimeLiveDataSet.AccountDataTable = AccountBLL.GetAccounts()
        'If dtAccount.Rows.Count > 0 Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function
    Public Shared Function IsValidateLockAccount() As Boolean
        'Dim AccountBLL As New AccountBLL
        'Dim dtAccount As TimeLiveDataSet.AccountDataTable = AccountBLL.GetDataByAccountId(DBUtilities.GetSessionAccountId)
        'Dim drAccount As TimeLiveDataSet.AccountRow
        'If dtAccount.Rows.Count > 0 Then
        '    drAccount = dtAccount.Rows(0)
        '    If IsDBNull(drAccount.Item("IsLock")) Then
        '        Return False
        '    ElseIf drAccount.IsLock = True Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'Else
        '    Return False
        'End If
    End Function
    
    Public Shared Function GetInvoiceNoPrefix() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return ""
        ElseIf Not System.Web.HttpContext.Current.Session("InvoiceNoPrefix") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("InvoiceNoPrefix")
        Else
            Return ""
        End If
    End Function
    Public Shared Function IsNotSupportedCultureFormats() As Boolean
        'If LocaleUtilitiesBLL.GetCurrentCulture = "hu-HU" Or System.Threading.Thread.CurrentThread.CurrentCulture.Name = "hu-HU" Then
        '    Return True
        'End If
        'If LocaleUtilitiesBLL.GetCurrentCulture = "sk-SK" Or System.Threading.Thread.CurrentThread.CurrentCulture.Name = "sk-SK" Then
        '    Return True
        'End If
        'If LocaleUtilitiesBLL.GetCurrentCulture = "it-IT" Or System.Threading.Thread.CurrentThread.CurrentCulture.Name = "it-IT" Then
        '    Return True
        'End If
        'Return False
    End Function
    'Public Shared Sub SetMaskEditExtenderCultureToUS(ByVal MaskEditExtender As AjaxControlToolkit.MaskedEditExtender)
    '    MaskEditExtender.CultureName = "en-US"
    'End Sub
    Public Shared Function GetShowAdditionalTaskInformationType() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("ShowAdditionalTaskInformationType") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowAdditionalTaskInformationType")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetSessionAccountWorkingDayTypeId() As Guid
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return Nothing
        ElseIf Not System.Web.HttpContext.Current.Session("AccountWorkingDayTypeId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("AccountWorkingDayTypeId")
        Else
            Return Nothing
        End If
    End Function
    
    Public Shared Function GetSystemInitialDayOfFirstPeriod() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "1"
        ElseIf Not System.Web.HttpContext.Current.Session("SystemInitialDayOfFirstPeriod") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("SystemInitialDayOfFirstPeriod")
        Else
            Return "1"
        End If
    End Function
    Public Shared Function GetSystemInitialDayOfLastPeriod() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "16"
        ElseIf Not System.Web.HttpContext.Current.Session("SystemInitialDayOfLastPeriod") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("SystemInitialDayOfLastPeriod")
        Else
            Return "16"
        End If
    End Function
    Public Shared Function GetInitialDayOfTheMonth() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "1"
        ElseIf Not System.Web.HttpContext.Current.Session("InitialDayOfTheMonth") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("InitialDayOfTheMonth")
        Else
            Return "1"
        End If
    End Function
    Public Shared Function GetPasswordExpiryPeriod() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "0"
        ElseIf Not System.Web.HttpContext.Current.Session("PasswordExpiryPeriod") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("PasswordExpiryPeriod")
        Else
            Return "0"
        End If
    End Function
    Public Shared Function IsTimeLiveMobileLogin() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("IsTimeLiveMobile") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("IsTimeLiveMobile")
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
            Return System.Web.HttpContext.Current.Session("EmailAddress")
        Else
            Return ""
        End If
    End Function
    Public Shared Function GetSessionEmployeeName() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return ""
        ElseIf Not System.Web.HttpContext.Current.Session("AccountEmployeeFullName") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("AccountEmployeeFullName")
        Else
            Return ""
        End If
    End Function
    Public Shared Function GetSessionInvoiceBillingType() As Guid
        Dim GuidHoursId As New Guid("f06cbf01-a190-4424-bfa9-0e8d58812952")
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return GuidHoursId
        ElseIf Not System.Web.HttpContext.Current.Session("InvoiceBillingTypeId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("InvoiceBillingTypeId")
        Else
            Return GuidHoursId
        End If
    End Function
    Public Shared Function IsInvoiceBillingTypeDaily() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("InvoiceBillingTypeId") Is Nothing Then
            If System.Web.HttpContext.Current.Session("InvoiceBillingTypeId").ToString = "d965607c-02ca-48a4-b681-a50f4ae8ce67" Then
                Return True
            End If
        Else
            Return False
        End If
    End Function
    Public Shared Function GetInvoiceFooter() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return ""
        ElseIf Not System.Web.HttpContext.Current.Session("InvoiceFooter") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("InvoiceFooter")
        Else
            Return ""
        End If
    End Function
    Public Shared Function GetSessionTimesheetOverdueAfterDays() As Short
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "1"
        ElseIf Not System.Web.HttpContext.Current.Session("TimesheetOverdueAfterDays") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("TimesheetOverdueAfterDays")
        Else
            Return "1"
        End If
    End Function
    Public Shared Function GetSessionTimesheetOverduePeriods() As Short
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "4"
        ElseIf Not System.Web.HttpContext.Current.Session("TimesheetOverduePeriods") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("TimesheetOverduePeriods")
        Else
            Return "4"
        End If
    End Function
    Public Shared Function GetProjectCodePrefix() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return ""
        ElseIf Not System.Web.HttpContext.Current.Session("ProjectCodePrefix") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ProjectCodePrefix")
        Else
            Return ""
        End If
    End Function
    Public Shared Function IncludeCurrentYearInProjectCode() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("IncludeCurrentYearInProjectCode") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("IncludeCurrentYearInProjectCode")
        Else
            Return False
        End If
    End Function
    Public Shared Function GetSessionTimeEntryHoursFormat() As Guid
        Dim GuidTimeEntryHoursFormatId As New Guid("074187eb-81d9-4e06-8e70-db7bc0c53784")
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return GuidTimeEntryHoursFormatId
        ElseIf IsDBNull(System.Web.HttpContext.Current.Session("TimeEntryHoursFormatId")) Then
            Return Nothing
        ElseIf Not System.Web.HttpContext.Current.Session("TimeEntryHoursFormatId") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("TimeEntryHoursFormatId")
        Else
            Return GuidTimeEntryHoursFormatId
        End If
    End Function
    Public Shared Function IsTimeEntryHoursFormat() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "Time"            
        ElseIf Not System.Web.HttpContext.Current.Session("TimeEntryHoursFormatId") Is Nothing Then
            If System.Web.HttpContext.Current.Session("TimeEntryHoursFormatId").ToString = "074187eb-81d9-4e06-8e70-db7bc0c53784" Then
                Return "Time"
            ElseIf System.Web.HttpContext.Current.Session("TimeEntryHoursFormatId").ToString = "ee9cb3b1-e1a1-4346-b9fc-a3da1c92a45e" Then
                Return "Decimal"
            Else
                Return "None"
            End If
        Else
            Return "Time"
        End If
    End Function
    Public Shared Function EnablePasswordComplexity() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("EnablePasswordComplexity") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("EnablePasswordComplexity")
        Else
            Return True
        End If
    End Function
    Public Shared Function ShowClientDepartmentInProject() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowClientDepartmentInProject") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowClientDepartmentInProject")
        Else
            Return False
        End If
    End Function
    Public Shared Function ShowEntryDateInInvoiceReport() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowEntryDateInInvoiceReport") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowEntryDateInInvoiceReport")
        Else
            Return False
        End If
    End Function
    Public Shared Function GetSortTimesheet() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "Default"
        ElseIf Not System.Web.HttpContext.Current.Session("TimesheetSort") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("TimesheetSort")
        Else
            Return "Default"
        End If
    End Function
    Public Shared Function GetCostCenterInTimesheetBy() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "Account"
        ElseIf Not System.Web.HttpContext.Current.Session("ShowCostCenterInTimesheetBy") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowCostCenterInTimesheetBy")
        Else
            Return "Account"
        End If
    End Function
    Public Shared Function ShowCopyTimesheetButton() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("ShowCopyTimesheetButton") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowCopyTimesheetButton")
        Else
            Return True
        End If
    End Function
    Public Shared Function ShowCopyActivitiesButtonInTimesheet() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("ShowCopyActivitiesButtonInTimesheet") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowCopyActivitiesButtonInTimesheet")
        Else
            Return True
        End If
    End Function
    Public Shared Function GetLockAllPreviousTimesheets() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("LockAllPreviousTimesheets") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("LockAllPreviousTimesheets")
        Else
            Return False
        End If
    End Function
    Public Shared Function GetLockAllFutureTimesheets() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("LockAllFutureTimesheets") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("LockAllFutureTimesheets")
        Else
            Return False
        End If
    End Function
    Public Shared Function GetLockPreviousTimesheetPeriods() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("LockPreviousTimesheetPeriods") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("LockPreviousTimesheetPeriods")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetLockNextTimsheetPeriods() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("LockFutureTimsheetPeriods") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("LockFutureTimsheetPeriods")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetLockPreviousNextMonthTimesheetOn() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("LockPreviousNextMonthTimesheetOn") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("LockPreviousNextMonthTimesheetOn")
        Else
            Return 0
        End If
    End Function
    Public Shared Function EnableTimeOffValidation() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("EnableBalanceValidationForTimeOff") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("EnableBalanceValidationForTimeOff")
        Else
            Return False
        End If
    End Function
    Public Shared Function GetShowAdditionalProjectInformationType() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("ShowAdditionalProjectInformationType") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowAdditionalProjectInformationType")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetLockAllPeriodExceptPrevious() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("LockAllPeriodExceptPrevious") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("LockAllPeriodExceptPrevious")
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetLockAllPeriodExceptNext() As Integer
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return 0
        ElseIf Not System.Web.HttpContext.Current.Session("LockAllPeriodExceptNext") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("LockAllPeriodExceptNext")
        Else
            Return 0
        End If
    End Function
    Public Shared Function EnableOfflineTimesheet() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("EnableOfflineTimesheet") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("EnableOfflineTimesheet")
        Else
            Return True
        End If
    End Function
  
End Class