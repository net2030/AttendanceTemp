Imports System.Data.SqlClient
Imports System.IO.File
Partial Class Home_Controls_ctlSetupDataBase
    Inherits System.Web.UI.UserControl
    Dim ConnectionStringForSALogin As String
    Dim ConnectionStringForTimeLiveLogin As String

    Protected Sub btnCreateDatabase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateDatabase.Click
        lblexception.Visible = False
        Try
            Me.CheckLoginAndPasswordTextBox()
            Me.CheckTimeLiveUserNameAndPassword()
            Me.CreateDataBase()
            Me.RenameExecuteScript()
            Me.PopulatedDatabase()
            If Exists(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_RENAME)) And Exists(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH)) Then
                Delete(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_RENAME))
            End If
            Rename(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH), Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_RENAME))
            Me.UpdateConnectionString()
            UIUtilities.RedirectToLoginPage()
        Catch ex As Exception
            lblexception.Visible = True
            lblexception.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnPopulateSchema_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopulateSchema.Click
        lblexception.Visible = False
        Try
            Me.CheckTimeLiveUserNameAndPassword()
            VerifyNewTimeliveConnection()
            Me.UpdateConnectionString()
            Me.PopulatedDatabase()
            Me.RenameExecuteScript()
            Me.ShowDatabaseCreatedSuccessfully()
        Catch ex As Exception
            lblexception.Visible = True
            lblexception.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExistingDatabase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExistingDatabase.Click
        lblexception.Visible = False
        Try
            CheckTimeLiveUserNameAndPassword()
            GetNewConnectionStringForTimeLiveLogin()
            TestDB()
            Me.UpdateConnectionString()
            Me.RenameExecuteScript()
            ShowDatabaseCreatedSuccessfully(True)
        Catch ex As Exception
            lblexception.Visible = True
            lblexception.Text = ex.Message
        End Try

    End Sub

    Public Function GetNewConnectionStringForSALogin() As SqlConnection
        If ddlAuthenticationType.SelectedValue <> 0 Then
            If txtInstanceName.Text <> "" Then
                ConnectionStringForSALogin = "Data Source=" & txtServerName.Text & "\" & txtInstanceName.Text & ";Initial Catalog=" & txtDataBaseName.Text & ";Persist Security Info=True;User ID=" & txtLogin.Text & ";Password=" & txtPassword.Text & ""
            Else
                ConnectionStringForSALogin = "Data Source=" & txtServerName.Text & ";Initial Catalog=" & txtDataBaseName.Text & ";Persist Security Info=True;User ID=" & txtLogin.Text & ";Password=" & txtPassword.Text & ""
            End If
        Else
            If txtInstanceName.Text <> "" Then
                ConnectionStringForSALogin = "Data Source=" & txtServerName.Text & "\" & txtInstanceName.Text & ";Initial Catalog=" & txtDataBaseName.Text & ";Integrated Security=True"
            Else
                ConnectionStringForSALogin = "Data Source=" & txtServerName.Text & ";Initial Catalog=" & txtDataBaseName.Text & ";Integrated Security=True"

            End If
        End If
        Dim CN As New SqlConnection(ConnectionStringForSALogin)
        Return CN
    End Function
    Public Function GetNewConnectionStringForTimeLiveLogin() As SqlConnection
        If ddlAuthenticationType.SelectedValue <> 0 Then
            If txtInstanceName.Text <> "" Then
                ConnectionStringForTimeLiveLogin = "Data Source=" & txtServerName.Text & "\" & txtInstanceName.Text & ";Initial Catalog=" & txtDataBaseName.Text & ";Persist Security Info=True;User ID=" & txtTimeLiveUserName.Text & ";Password=" & txtTimeLivePassword.Text & ""
            Else
                ConnectionStringForTimeLiveLogin = "Data Source=" & txtServerName.Text & ";Initial Catalog=" & txtDataBaseName.Text & ";Persist Security Info=True;User ID=" & txtTimeLiveUserName.Text & ";Password=" & txtTimeLivePassword.Text & ""
            End If
        Else
            If txtInstanceName.Text <> "" Then
                ConnectionStringForTimeLiveLogin = "Data Source=" & txtServerName.Text & "\" & txtInstanceName.Text & ";Initial Catalog=" & txtDataBaseName.Text & ";Integrated Security=True"
            Else
                ConnectionStringForTimeLiveLogin = "Data Source=" & txtServerName.Text & ";Initial Catalog=" & txtDataBaseName.Text & ";Integrated Security=True"

            End If
        End If
        Dim CN As New SqlConnection(ConnectionStringForTimeLiveLogin)
        Return CN
    End Function
    Public Sub CheckLoginAndPasswordTextBox()
        If ddlAuthenticationType.SelectedValue <> 0 Then
            If txtLogin.Text = "" Or txtPassword.Text = "" Then
                Throw New Exception("SA Login/Password Required")
            End If
        End If
    End Sub
    Public Sub ShowDatabaseCreatedSuccessfully(Optional ByVal IsExistingDataBase As Boolean = False)
        Dim strMessage As String = ""
        If IsExistingDataBase Then
            strMessage = "Configuration File Updated."
        Else
            strMessage = "Database Created Successfully."
        End If
        Dim strScript As String = "alert('" & strMessage & "'); window.location.href = '../Default.aspx';"
        If (Not Me.Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "clientScript", strScript, True)
        End If
    End Sub

    Public Sub CheckTimeLiveUserNameAndPassword()
        If ddlAuthenticationType.SelectedValue <> 0 Then
            If txtTimeLiveUserName.Text = "" Or txtTimeLivePassword.Text = "" Then
                Throw New Exception(ResourceHelper.GetFromResource("TimeLive Username/Password Required"))
            End If
        End If
    End Sub
    Public Sub RenameExecuteScript()
        If Exists(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH)) Then
            Exit Sub
        ElseIf Exists(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_RENAME)) Then
            Rename(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_RENAME), Server.MapPath(DBUtilities.SQL_SCRIPT_PATH))
        End If
    End Sub

    Public Sub CreateDataBase()
        If ddlAuthenticationType.SelectedValue <> 0 Then
            DBUtilities.CreateUserDatabaseLoginPassword(txtTimeLiveUserName.Text, txtTimeLivePassword.Text, txtDataBaseName.Text, GetNewConnectionStringForSALogin)
        End If
        VerifyNewTimeliveConnection()
        DBUtilities.CreateUserDatabase(txtDataBaseName.Text, GetNewConnectionStringForSALogin)
        If ddlAuthenticationType.SelectedValue <> 0 Then
            DBUtilities.ChangeDefaultDatabaseForLogin(txtTimeLiveUserName.Text, txtDataBaseName.Text, GetNewConnectionStringForSALogin)
            DBUtilities.CreateDatabaseSecurityUser(txtTimeLiveUserName.Text, GetNewConnectionStringForSALogin)
        End If
    End Sub
    Public Sub RunUpgradeScript()
        DBUtilities.RunSQLScript(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH), GetNewConnectionStringForTimeLiveLogin)
    End Sub
    Public Sub PopulatedDatabase()
        DBUtilities.RunCreateDatabaseScript(GetNewConnectionStringForTimeLiveLogin)
        Me.RunUpgradeScript()
    End Sub
    Public Sub UpdateConnectionString()
        GetNewConnectionStringForTimeLiveLogin()
        UIUtilities.UpdateConnectionString(ConnectionStringForTimeLiveLogin)
    End Sub
    Public Sub ChangeLabelCaption()
        Label1.Text = ResourceHelper.GetFromResource("This utility will create TimeLive database specified SQL Server Database Engine. This utility will create a blank TimeLive database with TimeLive schema.  Administrator can create TimeLive database on their own corporate server.  Server can be SQL Server 2000, 2005 and 2008.")
        If Not Me.IsPostBack Then
            txtDataBaseName.Text = ResourceHelper.GetFromResource("TimeLive")
            txtTimeLiveUserName.Text = ResourceHelper.GetFromResource("TimeLiveUser")
            txtTimeLivePassword.Text = ResourceHelper.GetFromResource("#TimeLivePassword")
        End If
        If rdCreateDatabase.Checked = True Then
            Label8.Text = ResourceHelper.GetFromResource("(New TimeLive database username which will be created for TimeLive database)")
            Label9.Text = ResourceHelper.GetFromResource("(New TimeLive database user's password which will be created for TimeLive database)")
            btnCreateDatabase.Visible = True
            btnPopulateSchema.Visible = False
            btnExistingDatabase.Visible = False
        ElseIf rdPopulateSchema.Checked = True Then
            Label8.Text = ResourceHelper.GetFromResource("(TimeLive database username)")
            Label9.Text = ResourceHelper.GetFromResource("(TimeLive database user's password)")
            btnCreateDatabase.Visible = False
            btnPopulateSchema.Visible = True
            btnExistingDatabase.Visible = False
        ElseIf rdExistingDatabase.Checked = True Then
            Label8.Text = ResourceHelper.GetFromResource("(TimeLive database username)")
            Label9.Text = ResourceHelper.GetFromResource("(TimeLive database user's password)")
            btnCreateDatabase.Visible = False
            btnPopulateSchema.Visible = False
            btnExistingDatabase.Visible = True
        End If
    End Sub

    Public Sub TestDB()
        Try
            Dim conTest As New System.Data.SqlClient.SqlConnection(ConnectionStringForTimeLiveLogin)
            conTest.Open()
            conTest.Close()
        Catch ex As Exception
            Throw New Exception("Database Connection Failed: <br>" & ex.Message)
        End Try
    End Sub
    Protected Sub ddlAuthenticationType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAuthenticationType.SelectedIndexChanged
        If ddlAuthenticationType.SelectedValue <> 0 Then
            txtLogin.Enabled = True
            txtPassword.Enabled = True
            txtLogin.Text = "sa"
            txtLogin.Focus()
        Else
            txtLogin.Enabled = False
            txtPassword.Enabled = False
            txtLogin.Text = ""
            txtPassword.Text = ""
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Title = ResourceHelper.GetFromResource(Me.Page.Title)
        If Not Me.IsPostBack Then
            lblexception.Visible = False
            rdCreateDatabase.Checked = True
        End If
        lblexception.Visible = False
        ChangeLabelCaption()
    End Sub
    Public Sub VerifyNewTimeliveConnection()
        GetNewConnectionStringForTimeLiveLogin()
        Dim cn As New SqlConnection(GetCNWithOutDatabaseName)
        cn.Open()
        cn.Close()
    End Sub
    Public Function GetNewConnectionString() As String
        Return ConnectionStringForTimeLiveLogin
    End Function
    Public Function GetCNWithOutDatabaseName() As String
        Dim con As New SqlConnection(ConnectionStringForTimeLiveLogin)
        Return GetNewConnectionString().Replace("Initial Catalog=" & con.Database.ToString & ";", "")
    End Function
End Class
