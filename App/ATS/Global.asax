<%@ Application Language="VB" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
      
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs) Handles Me.BeginRequest
        ' Fires at the beginning of each request
        Dim cookie As HttpCookie = HttpContext.Current.Request.Cookies("CurrentLanguage")

        If cookie IsNot Nothing AndAlso cookie.Value IsNot Nothing Then
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(cookie.Value)
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(cookie.Value)
        Else
            cookie = New HttpCookie("CurrentLanguage")
            cookie.Expires = DateTime.Now.AddMonths(6)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("ar")
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("ar")
            
            cookie.Value = "ar-SA"
            cookie.Expires = DateTime.Now.AddMonths(6)
            Response.SetCookie(cookie)
            
        End If
    End Sub
</script>