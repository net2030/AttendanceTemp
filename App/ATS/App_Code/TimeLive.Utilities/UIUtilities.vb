Imports Microsoft.VisualBasic
Imports System.Configuration
Imports ATS.BO
Imports System.Data

Public Class UIUtilities
    Public Shared Sub FixTableForNoRecords(ByVal dt As DataTable)
        If dt.Rows.Count = 0 Then
            'dt.Constraints.Clear()
            'Add a blank row to the dataset
            If dt.DataSet Is Nothing Then
                Dim ds As New DataSet
                ds.Tables.Add(dt)
                dt = ds.Tables(0)
                dt.DataSet.EnforceConstraints = False
            End If

            dt.DataSet.EnforceConstraints = False
            dt.Rows.Add(dt.NewRow())

            For n As Short = 0 To dt.Columns.Count - 1
                If dt.Columns(n).ReadOnly = False Then
                    If dt.Columns(n).DataType.ToString = "System.Int16" Or dt.Columns(n).DataType.ToString = "System.Int32" Then
                        dt.Rows(0)(n) = 0
                    End If
                End If
            Next
        End If
    End Sub
    Public Shared Function RedirectToHomePage() As String


        If System.Web.HttpContext.Current.Session("UserName") = "systemadmin" Then
            Dim aRoles() As String = System.Web.Security.Roles.Provider.GetRolesForUser(System.Web.HttpContext.Current.Session("UserName"))
            If aRoles(0) = "SystemAdmin" Then
                Return "~/Home/SystemSetting.aspx"
            End If
        Else
            Dim dr As DataRow = BOPagePermission.GetDefaultPage()
            Dim ReturnURL As String = System.Web.HttpContext.Current.Request.QueryString("ReturnPage")
            LoggingBLL.WriteToLog("RedirectToHomePage: ReturnURL" & ReturnURL)
            If ReturnURL <> "" Then
                Return ReturnURL
            ElseIf Not dr Is Nothing Then
                LoggingBLL.WriteToLog("RedirectToHomePage: Return GetDefaultPage" & "~/" & dr("Folder") & "/" & dr("SystemCategoryPage"))
                Return "~/" & dr("Folder") & "/" & dr("SystemCategoryPage")
            Else
                LoggingBLL.WriteToLog("RedirectToHomePage: Return ~/Employee/Default.aspx")
                Return "~/Attendance/MyChart.aspx"
            End If
        End If
    End Function
    Public Shared Function RedirectToMobileHomePage() As String
        Dim ReturnURL As String = System.Web.HttpContext.Current.Request.QueryString("ReturnPage")
        If ReturnURL <> "" Then
            Return ReturnURL
        Else
            Return "~/Mobile/"
        End If
    End Function
    Public Shared Sub RedirectToExecuteScriptPage()
        System.Web.HttpContext.Current.Response.Redirect("~\Home\ExecuteScript.aspx")
    End Sub
    Public Shared Sub RedirectToDatabaseSetupPage()
        System.Web.HttpContext.Current.Response.Redirect("~\Home\SetupDatabase.aspx")
    End Sub
    Public Shared Sub RedirectToAdminHomePage()
        System.Web.HttpContext.Current.Response.Redirect("~/AccountAdmin/Default.aspx", False)
    End Sub
    Public Shared Sub RedirectToLoginPage(Optional ByVal CurrentPage As System.Web.UI.Page = Nothing, Optional ByVal IsTimeLiveMobileLogin As Boolean = False)
        Dim loginUrl As String = IIf(IsTimeLiveMobileLogin, "~/Mobile/Default.aspx", "~/Default.aspx")
        If Not CurrentPage Is Nothing Then
            Dim currentPageName As String = CurrentPage.ResolveClientUrl(HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.RawUrl))
            loginUrl = loginUrl + "?ReturnPage=" + currentPageName
        End If
        System.Web.HttpContext.Current.Response.Redirect(loginUrl)
    End Sub
    Public Shared Sub RedirectToLoginPageForCallBack(Optional ByVal CurrentPage As System.Web.UI.Page = Nothing, Optional ByVal IsTimeLiveMobileLogin As Boolean = False)
        Dim loginUrl As String = IIf(IsTimeLiveMobileLogin, "~/Mobile/Default.aspx", "~/Default.aspx")
        If Not CurrentPage Is Nothing Then
            Dim currentPageName As String = CurrentPage.ResolveClientUrl(HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.RawUrl))
            loginUrl = loginUrl + "?ReturnPage=" + currentPageName
        End If
        'DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(loginUrl)
    End Sub
    Public Shared Sub RedirectToLoginPageAfterSystemSettings(ByVal password As String)
        System.Web.HttpContext.Current.Response.Redirect("~/Default.aspx?Username=systemadmin", False)
    End Sub
    Public Shared Sub RedirectToMyTask()
        System.Web.HttpContext.Current.Response.Redirect("~/Employee/MyTasks.aspx", False)
    End Sub
    Public Shared Sub RedirectToAccountAddForInstalled()
        System.Web.HttpContext.Current.Response.Redirect("~/Home/AccountAdd.aspx?ApplicationMode=Installed", False)
    End Sub
    Public Shared Sub RedirectToSystemSettingForInstalled()
        System.Web.HttpContext.Current.Response.Redirect("~/Home/SystemSetting.aspx?ApplicationMode=Installed", False)
    End Sub
    Public Shared Sub RedirectToSystemSetting()
        System.Web.HttpContext.Current.Response.Redirect("~/Home/SystemSetting.aspx", False)
    End Sub
    Public Shared Sub RedirectToRegistrationComplete(ByVal EMailAddress As String)
        System.Web.HttpContext.Current.Response.Redirect("~/Home/RegistrationComplete.aspx?EMailAddress=" & EMailAddress, False)
    End Sub
    Public Shared Sub RedirectToNoPermissionPage()
        System.Web.HttpContext.Current.Response.Redirect("~/Employee/NoPermission.aspx", False)
    End Sub
    Public Shared Function GetSessionExpiredURL(Optional ByVal CurrentPage As System.Web.UI.Page = Nothing, Optional ByVal IsTimeLiveMobileLogin As Boolean = False) As String
        Dim loginUrl As String = IIf(IsTimeLiveMobileLogin, "../Mobile/Default.aspx", "../Authenticate/SessionExpired.aspx")
        If Not CurrentPage Is Nothing Then
            Dim currentPageName As String = CurrentPage.ResolveClientUrl(HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.RawUrl))
            loginUrl = loginUrl + "?ReturnPage=" + currentPageName
        End If
        Return loginUrl
    End Function
    Public Shared Function GetApplicationMode() As String
        If Not System.Configuration.ConfigurationManager.AppSettings("ApplicationMode") Is Nothing Then
            Return System.Configuration.ConfigurationManager.AppSettings("ApplicationMode")
        Else
            Return "Installed"
        End If
    End Function
    Public Shared Function GetIntegratedAuthentication() As String
        If Not System.Configuration.ConfigurationManager.AppSettings("IntegratedAuthentication") Is Nothing Then
            Return System.Configuration.ConfigurationManager.AppSettings("IntegratedAuthentication")
        Else
            Return "Yes"
        End If
    End Function
    Public Shared Function GetEnableSSL() As Boolean
        If Not System.Configuration.ConfigurationManager.AppSettings("EnableSSL") Is Nothing Then
            Return System.Configuration.ConfigurationManager.AppSettings("EnableSSL")
        Else
            Return False
        End If
    End Function
    Public Shared Sub ClearCellsForNoRecords(ByVal objRow As GridViewRow)

        If objRow.RowType = DataControlRowType.DataRow Then
            If objRow.Cells(0).Text = "0" Or objRow.Cells(0).Text = "&nbsp;" Then
                objRow.Cells(0).Text = ""
                Dim objCell As TableCell
                For Each objCell In objRow.Cells
                    objCell.Controls.Clear()
                Next
            End If
        End If

    End Sub
    Public Shared Sub ModifyAppSettings(ByVal Key As String, ByVal Value As String)
        Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim AppSec As AppSettingsSection = config.GetSection("appSettings")

        If Not AppSec Is Nothing Then
            If Not AppSec.Settings.Item(Key) Is Nothing Then
                AppSec.Settings(Key).Value = Value
                config.Save()
            Else
                AppSec.Settings.Add(Key, Value)
                AppSec.Settings(Key).Value = Value
                config.Save()
            End If

        End If

    End Sub
    Public Shared Sub InsertAppSettingsTag(ByVal Key As String, ByVal Value As String)
        Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim AppSec As AppSettingsSection = config.GetSection("appSettings")

        If Not AppSec Is Nothing Then
            If AppSec.Settings.Item(Key) Is Nothing Then
                AppSec.Settings.Add(Key, Value)
                AppSec.Settings(Key).Value = Value
                config.Save()
            End If
        End If
    End Sub
    Public Shared Sub ModifyConnectionString(ByVal Key As String, ByVal Value As String)
        Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim ConSec As ConnectionStringsSection = config.GetSection("connectionStrings")

        If Not ConSec Is Nothing Then
            ConSec.ConnectionStrings(Key).ConnectionString = Value
            config.Save()
        End If

    End Sub
    Public Shared Sub UpdateConnectionString(ByVal Value As String)
        Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim ConSec As ConnectionStringsSection = config.GetSection("connectionStrings")

        If Not ConSec Is Nothing Then
            ConSec.ConnectionStrings("LiveConnectionString").ConnectionString = Value
            ConSec.ConnectionStrings("LivetecsConnectionString").ConnectionString = Value
            config.Save()
        End If

    End Sub
    Public Shared Sub GetFromWebconfig(ByVal Key As String, ByVal Value As String)
        Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim AppSec As AppSettingsSection = config.GetSection("membership")

        If Not AppSec Is Nothing Then
            AppSec.Settings(Key).Value = Value
            config.Save()
        End If

    End Sub
    Public Shared Sub ChangeApplicationSettings()
        UIUtilities.ChangeSiteMapSetting()
        UIUtilities.AddLDAPConnString("ADService", "LDAP://.")
        UIUtilities.AddConnString("LivetecsConnectionString", "Data Source=.\SQLEXPRESS;AttachDBFileName=|DataDirectory|TimeLive_Data.MDF;Integrated Security=true;User Instance=true;", "System.Data.SqlClient")
    End Sub
    Public Shared Sub ChangeSiteMapSetting()

        Dim siteMapXMl As String
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        siteMapXMl = "<add name='AspNetSqlSiteMapProvider' type='CustomProviders.LiveSiteMapProvider' securityTrimmingEnabled='true' connectionStringName='LiveConnectionString' sqlCacheDependency='CommandNotification'/>"

        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim siteMapNode As System.Xml.XmlElement
        siteMapNode = document.GetElementsByTagName("siteMap").Item(0)

        If siteMapNode.Attributes("defaultProvider").Value <> "AspNetSqlSiteMapProvider" Then

            siteMapNode.Attributes("defaultProvider").Value = "AspNetSqlSiteMapProvider"

            siteMapNode.FirstChild.InnerXml = siteMapXMl
            document.Save(strPath)
            RestartApplication()

        End If
    End Sub
    Public Shared Sub ChangeControlsSetting()

        Dim siteMapXMl As String
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        siteMapXMl = "<add tagPrefix='asp' namespace='System.Web.UI' assembly='System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' />" & "<add tagPrefix='aspToolkit' namespace='AjaxControlToolkit' assembly='AjaxControlToolkit' />" & "<add tagPrefix='x' namespace='ControlExtenders' />"

        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim siteMapNode As System.Xml.XmlElement
        siteMapNode = document.GetElementsByTagName("controls").Item(0)

        siteMapNode.InnerXml = siteMapXMl
        document.Save(strPath)

    End Sub
    ''' <summary>
    ''' Get and change connection string web.config file
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ChangeConnectionStringAndAppSettingForWebConfigFile()
        ''oldwebconfigfile
        Dim oldconnectionstringXMl As String
        Dim oldappsettingXML As String
        Dim oldmembershipXML As String
        Dim document As New System.Xml.XmlDocument
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/webold.config")
        document.Load(strPath)
        Dim oldconnectionstring As System.Xml.XmlElement
        Dim oldappsetting As System.Xml.XmlElement
        Dim oldmembership As System.Xml.XmlElement
        oldconnectionstring = document.GetElementsByTagName("connectionStrings").Item(0)
        oldappsetting = document.GetElementsByTagName("appSettings").Item(0)
        oldmembership = document.GetElementsByTagName("membership").Item(0)

        Dim oldmembershiptag As String = oldmembership.GetAttribute("defaultProvider")
        oldconnectionstringXMl = oldconnectionstring.InnerXml
        oldappsettingXML = oldappsetting.InnerXml
        oldmembershipXML = oldmembership.InnerXml

        ''Newwebconfigfile
        Dim documentnew As New System.Xml.XmlDocument
        Dim strPathnew As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        documentnew.Load(strPathnew)
        Dim sitemapnew As System.Xml.XmlElement
        Dim appsetting As System.Xml.XmlElement
        Dim membership As System.Xml.XmlElement
        sitemapnew = documentnew.GetElementsByTagName("connectionStrings").Item(0)
        appsetting = documentnew.GetElementsByTagName("appSettings").Item(0)
        membership = documentnew.GetElementsByTagName("membership").Item(0)
        sitemapnew.InnerXml = oldconnectionstringXMl
        appsetting.InnerXml = oldappsettingXML
        membership.InnerXml = oldmembershipXML
        Dim newmembershiptag = membership.GetAttribute("defaultProvider")
        ''newmembershiptag.Replace(newmembershiptag.ToString, oldmembershiptag.ToString)
        membership.Attributes("defaultProvider").Value = oldmembershiptag.ToString
        'membership.GetAttribute("defaultProvider").Replace(newmembershiptag, oldmembershiptag)
        documentnew.Save(strPathnew)
    End Sub
    ''' <summary>
    ''' Get and change connection string web.config file
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub InserthttpModulesWebConfigFile()
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        Dim siteMapXMl As String = "<add type='DevExpress.Web.ASPxClasses.ASPxHttpHandlerModule, DevExpress.Web.v11.1, Version=11.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a' name='ASPxHttpHandlerModule'/> "
        Dim migrationlineXML As String = "<handlers xmlns='http://schemas.microsoft.com/.NetConfiguration/v2.0'>" & "<remove name='WebServiceHandlerFactory-ISAPI-2.0' /><remove name='ScriptResource' />" & "</handlers>" & "<modules xmlns='http://schemas.microsoft.com/.NetConfiguration/v2.0'>" & "<add type='DevExpress.Web.ASPxClasses.ASPxHttpHandlerModule, DevExpress.Web.v11.1, Version=11.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a' name='ASPxHttpHandlerModule' />" & "</modules>" & "<validation validateIntegratedModeConfiguration='false' />"
        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        ''Newwebconfigfile
        If UIUtilities.IsCheckWebConfigFramework4 = True And document.GetElementsByTagName("httpModules").Count = 0 Then
            Dim httpstrXMLnode As System.Xml.XmlElement = document.CreateElement("httpModules", "http://schemas.microsoft.com/.NetConfiguration/v2.0")
            Dim httpsiteMapNode As System.Xml.XmlElement
            httpsiteMapNode = document.GetElementsByTagName("system.web").Item(0)
            If document.GetElementsByTagName("httpModules").Count = 0 Then
                httpsiteMapNode.AppendChild(httpstrXMLnode)
                Dim httpwebassembly = document.GetElementsByTagName("httpModules").Item(0)
                httpwebassembly.InnerXml = siteMapXMl
            End If

            Dim strXMLnode As System.Xml.XmlElement = document.CreateElement("modules", "http://schemas.microsoft.com/.NetConfiguration/v2.0")
            Dim siteMapNode As System.Xml.XmlElement
            siteMapNode = document.GetElementsByTagName("system.webServer").Item(0)
            If document.GetElementsByTagName("modules").Count = 0 Then
                siteMapNode.AppendChild(strXMLnode)
                Dim webassembly = document.GetElementsByTagName("modules").Item(0)
                webassembly.InnerXml = siteMapXMl
            End If
            siteMapNode.InnerXml = migrationlineXML
            document.Save(strPath)
        End If
    End Sub
    ''' <summary>
    ''' Get and change connection string web.config file
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Inserthttphandlers()
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        Dim siteMapXMl1 As String = "<add path='ChartImg.axd' verb='GET,HEAD,POST' type='System.Web.UI.DataVisualization.Charting.ChartHttpHandler, System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' validate='false' />"
        Dim siteMapXMl2 As String = "<remove name='WebServiceHandlerFactory-ISAPI-2.0' />" & "<remove name='ScriptResource' />" & "<remove name='ChartImageHandler' />" & "<add name='ChartImageHandler' preCondition='integratedMode' verb='GET,HEAD,POST' path='ChartImg.axd' type='System.Web.UI.DataVisualization.Charting.ChartHttpHandler, System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' />"
        Dim siteMapXMl3 As String = "<add tagPrefix='aspToolkit' namespace='AjaxControlToolkit' assembly='AjaxControlToolkit' />" & "<add tagPrefix='x' namespace='ControlExtenders' />" & "<add tagPrefix='asp' namespace='System.Web.UI.DataVisualization.Charting' assembly='System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' />"

        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)


        If document.GetElementsByTagName("httpHandlers").Count = 0 Then
            ''httphandler tag for chart
            Dim httpstrXMLnode1 As System.Xml.XmlElement = document.CreateElement("httpHandlers", "http://schemas.microsoft.com/.NetConfiguration/v2.0")
            Dim httpsiteMapNode1 As System.Xml.XmlElement
            httpsiteMapNode1 = document.GetElementsByTagName("system.web").Item(0)
            If document.GetElementsByTagName("httpHandlers").Count = 0 Then
                httpsiteMapNode1.AppendChild(httpstrXMLnode1)
                Dim httpwebassembly = document.GetElementsByTagName("httpHandlers").Item(0)
                httpwebassembly.InnerXml = siteMapXMl1
            End If

            ''' no 2
            Dim httpsiteMapNode2 As System.Xml.XmlElement
            'httpsiteMapNodea = siteMapXMl
            Dim FirstChild As System.Xml.XmlElement
            httpsiteMapNode2 = document.GetElementsByTagName("system.webServer").Item(0)
            FirstChild = httpsiteMapNode2.FirstChild
            'FirstChild.PrependChild(httpsiteMapNode)
            If FirstChild.ChildNodes("3") Is Nothing Then
                FirstChild.InnerXml = siteMapXMl2
            End If

            ''' no 3
            Dim httpsiteMapNode3 As System.Xml.XmlElement
            'httpsiteMapNodea = siteMapXMl
            Dim PagesChild As System.Xml.XmlElement
            Dim Control As System.Xml.XmlElement
            httpsiteMapNode3 = document.GetElementsByTagName("system.web").Item(0)
            PagesChild = httpsiteMapNode3.ChildNodes("9")
            Control = PagesChild.ChildNodes("1")
            If Control.ChildNodes("2") Is Nothing Then
                'FirstChild.PrependChild(httpsiteMapNode)
                Control.InnerXml = siteMapXMl3
            End If

            document.Save(strPath)
        End If
    End Sub
    Public Shared Sub InsertTagCaptchaImage()

        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        Dim siteMapXMl1 As String = "<add path='ChartImg.axd' verb='GET,HEAD,POST' type='System.Web.UI.DataVisualization.Charting.ChartHttpHandler, System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' validate='false' />" & "<add path='CaptchaImage.axd' verb='*' type='MSCaptcha.captchaImageHandler' />"
        Dim siteMapXMl2 As String = "<remove name='WebServiceHandlerFactory-ISAPI-2.0' />" & "<remove name='ScriptResource' />" & "<remove name='ChartImageHandler' />" & "<add name='MSCaptcha' path='CaptchaImage.axd' verb='*' type='MSCaptcha.captchaImageHandler' resourceType='Unspecified' preCondition='integratedMode' />" & "<add name='ChartImageHandler' preCondition='integratedMode' verb='GET,HEAD,POST' path='ChartImg.axd' type='System.Web.UI.DataVisualization.Charting.ChartHttpHandler, System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' />"
        ''Dim siteMapXMl3 As String = "<add tagPrefix='aspToolkit' namespace='AjaxControlToolkit' assembly='AjaxControlToolkit' />" & "<add tagPrefix='x' namespace='ControlExtenders' />" & "<add tagPrefix='asp' namespace='System.Web.UI.DataVisualization.Charting' assembly='System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' />"

        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)


        ''' no 2
        Dim httpsiteMapNode2 As System.Xml.XmlElement
        'httpsiteMapNodea = siteMapXMl
        Dim FirstChild As System.Xml.XmlElement
        httpsiteMapNode2 = document.GetElementsByTagName("system.webServer").Item(0)
        FirstChild = httpsiteMapNode2.FirstChild
        'FirstChild.PrependChild(httpsiteMapNode)
        If FirstChild.ChildNodes("4") Is Nothing Then
            FirstChild.InnerXml = siteMapXMl2
        End If

        ''' no 3
        Dim httpsiteMapNode3 As System.Xml.XmlElement
        'httpsiteMapNodea = siteMapXMl
        Dim PagesChild As System.Xml.XmlElement
        Dim Control As System.Xml.XmlElement
        httpsiteMapNode3 = document.GetElementsByTagName("system.web").Item(0)
        PagesChild = httpsiteMapNode3.ChildNodes("0")
        If PagesChild.ChildNodes("1") Is Nothing Then
            PagesChild.InnerXml = siteMapXMl1
            document.Save(strPath)
        End If
    End Sub

    ''' <summary>
    ''' check dot net frame work
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function IsDotNetFrameWork4WebConfig() As Boolean

        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/webold.config")
        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim siteMapNode As System.Xml.XmlElement
        siteMapNode = document.GetElementsByTagName("compilation").Item(0)
        If siteMapNode.GetAttribute("targetFramework") <> "4.0" Then
            Return True
        End If
    End Function

    ''' <summary>
    ''' check dot net frame work
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function IsCheckWebConfigFramework4() As Boolean

        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim siteMapNode As System.Xml.XmlElement
        siteMapNode = document.GetElementsByTagName("compilation").Item(0)
        If siteMapNode.GetAttribute("targetFramework") = "4.0" Then
            Return True
        End If
    End Function
    ''' <summary>
    ''' change web.config file assembly setting 2010
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ChangeAssemblySettingForWebConfig()

        Dim siteMapXMl As String
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        siteMapXMl = "<add assembly='eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2'/>" & "<add assembly='eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2'/>" & "<add assembly='stdole, Version=7.0.3300.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'/>" & "<add assembly='System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089'/>" & "<add assembly='System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089'/>" & "<add assembly='System.Web.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089'/>" & "<add assembly='System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089'/>" & "<add assembly='System.Configuration.Install, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Data.OracleClient, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089'/>" & "<add assembly='System.Management, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.DirectoryServices.Protocols, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.EnterpriseServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Web.RegularExpressions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>" & "<add assembly='System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35'/>" & "<add assembly='System.Transactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089'/>" & "<add assembly='System.Runtime.Serialization.Formatters.Soap, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A'/>"

        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim siteMapNode As System.Xml.XmlElement
        siteMapNode = document.GetElementsByTagName("compilation").Item(0)

        If siteMapNode.GetAttribute("targetFramework") = "4.0" Or siteMapNode.GetAttribute("targetFramework") = "" Then
            Dim webassembly = document.GetElementsByTagName("assemblies").Item(0)
            webassembly.InnerXml = siteMapXMl
            document.Save(strPath)
            RestartApplication()
        End If
    End Sub
    ''' <summary>
    ''' change config setting web.config file
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ChangeConfigSettingForWebConfig()

        Dim siteMapXMl As String
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        siteMapXMl = "<sectionGroup name='smartMassEmail.providers'>" & "<section name='emailQueue' type='SmartMassEmail.Providers.EmailQueueConfigurationHandler, Providers'/>" & "<section name='emailDeQueue' type='SmartMassEmail.Providers.EmailDeQueueConfigurationHandler, Providers'/>" & "<section name='emailDispatch' type='SmartMassEmail.Providers.EmailDispatchConfigurationHandler, Providers'/>" & "<section name='processFailure' type='SmartMassEmail.Providers.ProcessFailureConfigurationHandler, Providers'/>" & "<section name='emailTemplate' type='SmartMassEmail.Providers.EmailTemplateConfigurationHandler, Providers'/>" & "</sectionGroup>" & "<section name='loggingConfiguration' type='Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LoggingSettings, Microsoft.Practices.EnterpriseLibrary.Logging, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null'/>" & "<section name='cachingConfiguration' type='Microsoft.Practices.EnterpriseLibrary.Caching.Configuration.CacheManagerSettings, Microsoft.Practices.EnterpriseLibrary.Caching, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null'/>" & "<section name='dataConfiguration' type='Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings, Microsoft.Practices.EnterpriseLibrary.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null'/>" & "<section name='netTiersService' type='SmartMassEmail.Data.Bases.NetTiersServiceSection, SmartMassEmail.Data' allowDefinition='MachineToApplication' restartOnExternalChanges='true'/>"

        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim siteMapNode As System.Xml.XmlElement
        siteMapNode = document.GetElementsByTagName("configSections").Item(0)

        If siteMapNode.GetAttribute("targetFramework") = "4.0" Or siteMapNode.GetAttribute("targetFramework") = "" Then
            siteMapNode.InnerXml = siteMapXMl
            document.Save(strPath)
            RestartApplication()
        End If
    End Sub
    Public Shared Sub ChangeGlobalizationSetting()

        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim siteMapNode As System.Xml.XmlElement
        siteMapNode = document.GetElementsByTagName("globalization").Item(0)

        If siteMapNode.GetAttribute("uiCulture") = "" Then
            Dim attrib As System.Xml.XmlAttribute = siteMapNode.OwnerDocument.CreateAttribute("uiCulture")
            attrib.Value = "auto"
            siteMapNode.Attributes.Append(attrib)
        End If
        document.Save(strPath)

    End Sub
    Public Shared Sub ChangePagesSetting()

        'Dim siteMapXMl As String
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        'siteMapXMl = "<pages enableEventValidation='false' theme='SkinFile' viewStateEncryptionMode='Never'>"

        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim siteMapNode As System.Xml.XmlElement
        siteMapNode = document.GetElementsByTagName("pages").Item(0)

        If siteMapNode.GetAttribute("viewStateEncryptionMode") = "" Then
            Dim attrib As System.Xml.XmlAttribute = siteMapNode.OwnerDocument.CreateAttribute("viewStateEncryptionMode")
            attrib.Value = "Never"
            siteMapNode.Attributes.Append(attrib)
        End If

        If siteMapNode.GetAttribute("enableViewStateMac") = "" Then
            Dim attrib As System.Xml.XmlAttribute = siteMapNode.OwnerDocument.CreateAttribute("enableViewStateMac")
            attrib.Value = "false"
            siteMapNode.Attributes.Append(attrib)
        End If

        document.Save(strPath)
        RestartApplication()
    End Sub
    Private Shared Sub RestartApplication()
        System.Web.HttpRuntime.UnloadAppDomain()
        UIUtilities.RedirectToLoginPage()
    End Sub
    Private Shared Sub RestartApplicationForMembershipProvider()
        System.Web.HttpRuntime.UnloadAppDomain()
        If System.Configuration.ConfigurationManager.AppSettings("ApplicationMode") = "Installed" Then
            'UIUtilities.RedirectToAccountAddForInstalled()
        End If
    End Sub
    Public Shared Sub ChangeMembershipProviderForADAuthentication(ByVal ADUsername As String, ByVal ADPassword As String, ByVal connectionProtection As String)

        Dim MembershipProviderXMl As String
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        MembershipProviderXMl = "<add name='AspNetActiveDirectoryMembershipProvider' type='System.Web.Security.ActiveDirectoryMembershipProvider, System.Web, Version=2.0.0.0, Culture=neutral,PublicKeyToken=b03f5f7f11d50a3a' connectionStringName='ADService' attributeMapUsername='sAMAccountName' connectionUsername=" & "'" & ADUsername & "'" & " connectionPassword=" & "'" & ADPassword & "'" & " connectionProtection='" & connectionProtection & "' enableSearchMethods='true'/>"



        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim MembershipProviderNode As System.Xml.XmlElement
        MembershipProviderNode = document.GetElementsByTagName("membership").Item(0)

        'If MembershipProviderNode.Attributes("defaultProvider").Value <> "AspNetActiveDirectoryMembershipProvider" Then

        MembershipProviderNode.Attributes("defaultProvider").Value = "AspNetActiveDirectoryMembershipProvider"

        MembershipProviderNode.FirstChild.InnerXml = MembershipProviderXMl
        document.Save(strPath)
        RestartApplicationForMembershipProvider()



        'End If


    End Sub
    Public Shared Sub ChangeMembershipProviderForOpenLDAPAuthentication(ByVal LDAPUsername As String, ByVal LDAPPassword As String, ByVal connectionProtection As String)

        Dim MembershipProviderXMl As String
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        MembershipProviderXMl = "<add name='OpenLDAPMembershipProvider' type='CustomProviders.LDAPProvider' connectionStringName='ADService' connectionUsername=" & "'" & LDAPUsername & "'" & " connectionPassword=" & "'" & LDAPPassword & "'" & " connectionProtection='" & connectionProtection & "' enableSearchMethods='true'/>"

        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim MembershipProviderNode As System.Xml.XmlElement
        MembershipProviderNode = document.GetElementsByTagName("membership").Item(0)

        MembershipProviderNode.Attributes("defaultProvider").Value = "OpenLDAPMembershipProvider"

        MembershipProviderNode.FirstChild.InnerXml = MembershipProviderXMl
        document.Save(strPath)
        RestartApplicationForMembershipProvider()
    End Sub
    ''Public Shared Sub AddtagfotServiceModel()

    Public Shared Sub InsertTagforServiceModel()
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        Dim siteMapXMl As String = "<behaviors>" & "<serviceBehaviors>" & "<behavior name=''>" & "<serviceMetadata httpGetEnabled='true'/>" & "<serviceDebug includeExceptionDetailInFaults='false'/>" & "</behavior>" & "</serviceBehaviors>" & "</behaviors>" & "<bindings>" & "<customBinding>" & "<binding name='TaskService.customBinding0' receiveTimeout='00:10:00' sendTimeout='00:10:00' openTimeout='00:10:00' closeTimeout='00:10:00'>" & "<binaryMessageEncoding/>" & "<httpTransport/>" & "</binding>" & "<binding name='EmployeeService.customBinding0' receiveTimeout='00:10:00' sendTimeout='00:10:00' openTimeout='00:10:00' closeTimeout='00:10:00'>" & "<binaryMessageEncoding/>" & "<httpTransport/>" & "</binding>" & "</customBinding>" & "</bindings>" & "<serviceHostingEnvironment aspNetCompatibilityEnabled='true' multipleSiteBindingsEnabled='true'/>" & "<services>" & "<service name='TaskService'>" & "<endpoint address='' binding='customBinding' bindingConfiguration='TaskService.customBinding0' contract='TaskService'/>" & "<endpoint address='mex' binding='mexHttpBinding' contract='IMetadataExchange'/>" & "</service>" & "<service name='EmployeeService'>" & "<endpoint address='' binding='customBinding' bindingConfiguration='EmployeeService.customBinding0' contract='EmployeeService'/>" & "<endpoint address='mex' binding='mexHttpBinding' contract='IMetadataExchange'/>" & "</service>" & "</services>"
        Dim siteMapXMlForHTTPS As String = "<behaviors>" & "<serviceBehaviors>" & "<behavior name=''>" & "<serviceMetadata httpsGetEnabled='true'/>" & "<serviceDebug includeExceptionDetailInFaults='false'/>" & "</behavior>" & "</serviceBehaviors>" & "</behaviors>" & "<bindings>" & "<customBinding>" & "<binding name='TaskService.customBinding0' receiveTimeout='00:10:00' sendTimeout='00:10:00' openTimeout='00:10:00' closeTimeout='00:10:00'>" & "<binaryMessageEncoding/>" & "<httpsTransport/>" & "</binding>" & "<binding name='EmployeeService.customBinding0' receiveTimeout='00:10:00' sendTimeout='00:10:00' openTimeout='00:10:00' closeTimeout='00:10:00'>" & "<binaryMessageEncoding/>" & "<httpsTransport/>" & "</binding>" & "</customBinding>" & "</bindings>" & "<serviceHostingEnvironment aspNetCompatibilityEnabled='true' multipleSiteBindingsEnabled='true'/>" & "<services>" & "<service name='TaskService'>" & "<endpoint address='' binding='customBinding' bindingConfiguration='TaskService.customBinding0' contract='TaskService'/>" & "<endpoint address='mex' binding='mexHttpsBinding' contract='IMetadataExchange'/>" & "</service>" & "<service name='EmployeeService'>" & "<endpoint address='' binding='customBinding' bindingConfiguration='EmployeeService.customBinding0' contract='EmployeeService'/>" & "<endpoint address='mex' binding='mexHttpsBinding' contract='IMetadataExchange'/>" & "</service>" & "</services>"
        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        ''Newwebconfigfile

        Dim httpstrXMLnode As System.Xml.XmlElement = document.CreateElement("system.serviceModel", "http://schemas.microsoft.com/.NetConfiguration/v2.0")
        Dim httpsiteMapNode As System.Xml.XmlElement
        httpsiteMapNode = document.GetElementsByTagName("configuration").Item(0)
        If document.GetElementsByTagName("system.serviceModel").Count = 0 Then
            httpsiteMapNode.AppendChild(httpstrXMLnode)
            Dim httpwebassembly = document.GetElementsByTagName("system.serviceModel").Item(0)
            If System.Configuration.ConfigurationManager.AppSettings("SitePrefix").Contains("https") Then
                httpwebassembly.InnerXml = siteMapXMlForHTTPS
            Else
                httpwebassembly.InnerXml = siteMapXMl
            End If
            document.Save(strPath)
        End If
    End Sub
    Public Shared Sub ChangeMembershipProviderForDefaultAuthentication()

        Dim MembershipProviderXMl As String
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        MembershipProviderXMl = "<add name='AspNetReadOnlyXmlMembershipProvider' type='CustomProviders.LiveMembershipProvider' description='Read-only XML membership provider' xmlFileName='~/App_Data/MembershipUsers.xml'/>"



        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim MembershipProviderNode As System.Xml.XmlElement
        MembershipProviderNode = document.GetElementsByTagName("membership").Item(0)

        If MembershipProviderNode.Attributes("defaultProvider").Value <> "AspNetReadOnlyXmlMembershipProvider" Then

            MembershipProviderNode.Attributes("defaultProvider").Value = "AspNetReadOnlyXmlMembershipProvider"

            MembershipProviderNode.FirstChild.InnerXml = MembershipProviderXMl
            document.Save(strPath)
            RestartApplicationForMembershipProvider()
        End If
    End Sub

    Public Shared Function GetADConnectionUsername() As String
        If New LDAPUtilitiesBLL().IsAspNetActiveDirectoryMembershipProvider Then
            Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
            Dim document As New System.Xml.XmlDocument
            document.Load(strPath)
            Dim MembershipProviderNode As System.Xml.XmlElement
            MembershipProviderNode = document.GetElementsByTagName("membership").Item(0)
            If MembershipProviderNode.FirstChild.FirstChild.Attributes("connectionUsername").Value <> "" Then
                Dim ConnectionUsername As String
                ConnectionUsername = MembershipProviderNode.FirstChild.FirstChild.Attributes("connectionUsername").Value
                Return ConnectionUsername
            End If
        End If
    End Function
    Public Shared Function IsAspNetActiveDirectoryMembershipProvider() As Boolean

        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")


        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)

        Dim MembershipProviderNode As System.Xml.XmlElement
        MembershipProviderNode = document.GetElementsByTagName("membership").Item(0)

        If MembershipProviderNode.Attributes("defaultProvider").Value = "AspNetActiveDirectoryMembershipProvider" Then
            Return True
        End If

    End Function
    Public Shared Function IsOpenLDAPMembershipProvider() As Boolean
        Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
        Dim document As New System.Xml.XmlDocument
        document.Load(strPath)
        Dim MembershipProviderNode As System.Xml.XmlElement
        MembershipProviderNode = document.GetElementsByTagName("membership").Item(0)
        If MembershipProviderNode.Attributes("defaultProvider").Value = "OpenLDAPMembershipProvider" Then
            Return True
        End If
    End Function
    Public Shared Function GetADConnectionPassword() As String
        If New LDAPUtilitiesBLL().IsAspNetActiveDirectoryMembershipProvider Then
            Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
            Dim document As New System.Xml.XmlDocument
            document.Load(strPath)
            Dim MembershipProviderNode As System.Xml.XmlElement
            MembershipProviderNode = document.GetElementsByTagName("membership").Item(0)
            If MembershipProviderNode.FirstChild.FirstChild.Attributes("connectionPassword").Value <> "" Then
                Dim ConnectionPassword As String
                ConnectionPassword = MembershipProviderNode.FirstChild.FirstChild.Attributes("connectionPassword").Value
                Return ConnectionPassword
            End If
        End If
    End Function
    Public Shared Function GetADConnectionProtection() As String
        If New LDAPUtilitiesBLL().IsAspNetActiveDirectoryMembershipProvider Then
            Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
            Dim document As New System.Xml.XmlDocument
            document.Load(strPath)
            Dim MembershipProviderNode As System.Xml.XmlElement
            MembershipProviderNode = document.GetElementsByTagName("membership").Item(0)
            If MembershipProviderNode.FirstChild.FirstChild.Attributes("connectionProtection").Value <> "" Then
                Dim ConnectionProtection As String
                ConnectionProtection = MembershipProviderNode.FirstChild.FirstChild.Attributes("connectionProtection").Value
                Return ConnectionProtection
            End If
        End If
    End Function
    Public Shared Function GetADAuthenticationType() As DirectoryServices.AuthenticationTypes
        If New LDAPUtilitiesBLL().IsAspNetActiveDirectoryMembershipProvider Then
            Dim strPath As String = System.Web.HttpContext.Current.Server.MapPath("~/web.config")
            Dim document As New System.Xml.XmlDocument
            document.Load(strPath)
            Dim MembershipProviderNode As System.Xml.XmlElement
            MembershipProviderNode = document.GetElementsByTagName("membership").Item(0)
            If MembershipProviderNode.FirstChild.FirstChild.Attributes("connectionProtection").Value <> "" Then
                Dim AuthenticationType As String
                AuthenticationType = MembershipProviderNode.FirstChild.FirstChild.Attributes("connectionProtection").Value
                If AuthenticationType = "Secure" Then
                    Return DirectoryServices.AuthenticationTypes.Encryption
                Else
                    Return DirectoryServices.AuthenticationTypes.None
                End If
            End If
        End If
    End Function
    Public Shared Sub AddConnString(ByVal name As String, ByVal connectionstring As String, ByVal providername As String)
        Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim ConSec As ConnectionStringsSection = config.GetSection("connectionStrings")

        If Not ConSec Is Nothing Then
            If ConSec.ConnectionStrings(name) Is Nothing Then
                ConSec.ConnectionStrings.Add(New ConnectionStringSettings(name, connectionstring, providername))
                config.Save()
            End If
        End If
    End Sub
    Public Shared Sub AddLDAPConnString(ByVal name As String, ByVal connectionstring As String)
        Dim config As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim ConSec As ConnectionStringsSection = config.GetSection("connectionStrings")

        If Not ConSec Is Nothing Then
            If ConSec.ConnectionStrings(name) Is Nothing Then
                ConSec.ConnectionStrings.Add(New ConnectionStringSettings(name, connectionstring))
                config.Save()
            End If
        End If

    End Sub
    Public Shared Function OnDeleteException(ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs)
        Dim CurrentPage As Page = System.Web.HttpContext.Current.CurrentHandler
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
            UIUtilities.ShowMessage("Can’t delete. Dependent data is exist with this record.", CurrentPage)
            ' Throw New Exception("Can't delete data because of existing dependent data.")
        Else
            Return Nothing
        End If
    End Function
    Public Shared Sub ShowMessage(ByVal strMessage As String, CurrentPage As Page)
        Dim strScript As String = "alert('" & strMessage & "');"
        If (Not CurrentPage.ClientScript.IsClientScriptBlockRegistered("clientScript")) Then
            ScriptManager.RegisterClientScriptBlock(CurrentPage, CurrentPage.GetType, "clientScript", strScript, True)
        End If
    End Sub
    Public Shared Function OnInsertException(ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs, ByVal MessageForException As String)
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
            Throw New Exception(MessageForException)
        Else
            Return Nothing
        End If
    End Function
    Public Shared Sub AfterGridViewRowDelete(ByVal FormView As System.Web.UI.WebControls.FormView)
        FormView.ChangeMode(FormViewMode.Insert)
    End Sub
    Public Shared Sub MakeDropdownReadonly(ByVal objDropdown As DropDownList)
        objDropdown.Attributes.Add("onMouseover", "this.disabled=true;")
    End Sub
    Public Shared Sub MakeCalendarPopUpReadonly(ByVal objCalendarpopup As eWorld.UI.CalendarPopup)
        objCalendarpopup.Attributes.Add("onMouseover", "this.disabled=true;")
    End Sub
    Public Shared Sub MakeTextboxReadonly(ByVal objTextbox As TextBox)
        objTextbox.ReadOnly = True
    End Sub
    Public Shared Sub SetFocus(ByVal control As Control)
        Dim p As Control = control.Parent
        While (Not (TypeOf p Is System.Web.UI.Page))
            p = p.Parent
        End While
        Dim sm As ScriptManager = ScriptManager.GetCurrent(DirectCast(p, System.Web.UI.Page))
        sm.SetFocus(control)
    End Sub
    Public Shared Sub DeleteTempFiles()


    End Sub
    Public Shared Sub DeleteDatabaseBackupTempFiles()
        'Dim BackupBLL As New DatabaseBackupBLL
        'BackupBLL.RecursivelyDeleteOldBackupFiles()
    End Sub
    Public Shared Function IsSendScheduledEmail() As Boolean
        If System.Configuration.ConfigurationManager.AppSettings("SendScheduledEmail") = "No" Then
            Return False
        End If
        Return True
    End Function
    Public Shared Function IsDevelopmentEnvironment() As Boolean
        If System.Configuration.ConfigurationManager.AppSettings("DevelopmentEnvironment") = "Yes" Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function DisableSchedule() As Boolean
        If System.Configuration.ConfigurationManager.AppSettings("DisableSchedule") = "Yes" Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function IsSignUpOnLogin() As Boolean
        If System.Configuration.ConfigurationManager.AppSettings("ShowSignUpOnLogin") = "Yes" Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function DeleteOldWewebConfigFile() As String
        Dim weboldconfig = System.Web.HttpContext.Current.Server.MapPath("~\webold.config")
        If System.IO.File.Exists(weboldconfig) = True Then
            If UIUtilities.IsDotNetFrameWork4WebConfig = True Then
                UIUtilities.ChangeConnectionStringAndAppSettingForWebConfigFile()
                System.IO.File.SetAttributes(System.Web.HttpContext.Current.Server.MapPath("~\webold.config"), IO.FileAttributes.Normal)
                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~\webold.config"))
            End If
        End If
    End Function
    Public Shared Function EnableDequeueMailSend() As Boolean
        If System.Configuration.ConfigurationManager.AppSettings("EnableDequeueMailSend") = "No" Then
            Return False
        End If
        Return True
    End Function
    Public Shared Function SchedulerThreadTime() As Double
        If Not System.Configuration.ConfigurationManager.AppSettings("SchedulerThreadTime") Is Nothing Then
            Return System.Configuration.ConfigurationManager.AppSettings("SchedulerThreadTime")
        End If
        Return 10
    End Function
    Public Shared Function GetHtmlEncodeString(Value As String) As String
        Return System.Web.HttpUtility.HtmlEncode(Value)
    End Function
    Public Shared Function DisableLicense() As Boolean
        If System.Configuration.ConfigurationManager.AppSettings("DisableLicense") = "Yes" Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function GetCompanyNameByApplication() As String
        If System.Configuration.ConfigurationManager.AppSettings("BugTracking") = "Yes" Then
            Return "MAS Tech"
        End If
        Return "MAS Tech"
    End Function
    Public Shared Function IsTrakLiveApplication() As Boolean
        If System.Configuration.ConfigurationManager.AppSettings("BugTracking") = "Yes" Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function GetLDAPBaseDN() As String
        If System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString <> "" Then
            Dim ADService As String = System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString
            Return ADService.Replace(ADService.Substring(0, ADService.ToUpper.IndexOf("DC", 1)), "")
        End If
    End Function
    Public Shared Function GetLDAPHost() As String
        If System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString <> "" Then
            Dim ADService As String = System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString
            ADService = ADService.Replace("LDAP://", "")
            Return ADService.Substring(0, ADService.IndexOf(":", 1))
        End If
    End Function
    Public Shared Function GetLDAPPort() As String
        If System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString <> "" Then
            Dim ADService As String = System.Configuration.ConfigurationManager.ConnectionStrings("ADService").ConnectionString
            ADService = ADService.Replace("LDAP://", "")
            Return ADService.Substring(0, ADService.IndexOf("/", 1)).Replace(UIUtilities.GetLDAPHost() & ":", "")
        End If
    End Function
End Class

