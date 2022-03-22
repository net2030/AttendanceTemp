Imports System.IO.File
Partial Class Home_ExecuteScript
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DBUtilities.IsTableExistsInDatabase("SystemData") = False And Exists(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_ES2)) Then
            DBUtilities.RunSQLScript(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_ES2))
            Response.Redirect("~\Home\ExecuteScript.aspx")
        End If
        If DBUtilities.IsTableExistsInDatabase("AccountEmployeeExpenseSheet") = False And Exists(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_ES3)) Then
            DBUtilities.RunSQLScript(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_ES3))
            Response.Redirect("~\Home\ExecuteScript.aspx")
        End If
        If Exists(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH)) Then
            DBUtilities.RunSQLScript(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH))
            If Exists(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_RENAME)) Then
                System.IO.File.SetAttributes(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_RENAME), IO.FileAttributes.Normal)
                System.IO.File.Delete(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_RENAME))
            End If
            Rename(Server.MapPath(DBUtilities.SQL_SCRIPT_PATH), Server.MapPath(DBUtilities.SQL_SCRIPT_PATH_RENAME))
            UIUtilities.RedirectToLoginPage()
        End If
        ' Comment for application restart issue 
        'UIUtilities.ChangeGlobalizationSetting()
        'UIUtilities.ChangePagesSetting()

        'UIUtilities.ChangeAssemblySettingForWebConfig()
        'UIUtilities.ChangeConfigSettingForWebConfig()
        ''UIUtilities.ChangeAssemblySetting()
    End Sub
End Class
