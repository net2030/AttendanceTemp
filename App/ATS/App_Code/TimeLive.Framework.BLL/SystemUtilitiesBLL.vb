Imports Microsoft.VisualBasic
Imports System.Web.HttpRequest
Imports System.Web
Imports System.Net
Imports System.IO

Public Class SystemUtilitiesBLL

    Public Shared Function GetStringFromTextFile(ByVal FileName As String) As String
        Dim FileText As String

        FileText = System.IO.File.ReadAllText(FileName)

        Return FileText
    End Function
    Public Shared Function GetApplicationVersion() As Version
        Dim FileName As String
        FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/Version.txt")
        Dim VersionText As String = GetStringFromTextFile(FileName)

        If VersionText <> "" Then
            Return New Version(VersionText)
        Else
            Return Nothing
        End If

    End Function
    Public Shared Function OpenURL(URL As String) As String
        'Address of URL
        Dim request As HttpWebRequest = WebRequest.Create(URL)
        Dim response As HttpWebResponse = request.GetResponse()
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
        Dim str As String = reader.ReadLine()
        Return str
    End Function

End Class
