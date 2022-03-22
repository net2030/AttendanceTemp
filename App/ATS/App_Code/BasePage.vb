
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Globalization

''' <summary>
''' Summary description for BasePage
''' </summary>
Public Class BasePage
    Inherits Page
    '
    ' TODO: Add constructor logic here
    '

    Public Sub New()
    End Sub

    Protected Overrides Sub InitializeCulture()
        Dim lang As String = [String].Empty
        Dim cookie As HttpCookie = Request.Cookies("CurrentLanguage")
        If cookie IsNot Nothing AndAlso cookie.Value IsNot Nothing Then
            lang = cookie.Value
            Dim Cul As CultureInfo = CultureInfo.CreateSpecificCulture(lang)
            System.Threading.Thread.CurrentThread.CurrentUICulture = Cul
            System.Threading.Thread.CurrentThread.CurrentCulture = Cul
        Else
            lang = "en-US"
            Dim Cul As CultureInfo = CultureInfo.CreateSpecificCulture(lang)
            System.Threading.Thread.CurrentThread.CurrentUICulture = Cul
            System.Threading.Thread.CurrentThread.CurrentCulture = Cul

            Dim cookie_new As New HttpCookie("CurrentLanguage")
            cookie_new.Value = lang
            cookie_new.Expires = DateTime.Now.AddMonths(6)
            Response.SetCookie(cookie_new)
        End If

        MyBase.InitializeCulture()
    End Sub
End Class
