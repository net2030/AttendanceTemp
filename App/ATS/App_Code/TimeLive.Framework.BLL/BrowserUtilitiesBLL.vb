Imports Microsoft.VisualBasic

Public Class BrowserUtilitiesBLL

    Public Shared Function IsCompatibleBrowserForPopupExtender() As Boolean
        Dim browser As System.Web.HttpBrowserCapabilities = System.Web.HttpContext.Current.Request.Browser
        If browser.Browser = "IE" And browser.MajorVersion < 7 Then
            Return False
        Else
            Return True
        End If
    End Function


End Class
