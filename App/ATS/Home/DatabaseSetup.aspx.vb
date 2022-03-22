
Partial Class Home_DatabaseSetup
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DBUtilities.RunSQLScript("D:\Temp\TimeLiveWebForHosting\Script\CreateDatabase.sql")

    End Sub
End Class
