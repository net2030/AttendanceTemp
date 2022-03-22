Imports System.Configuration

Partial Class Shared_FileDownload
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        '    Dim EncryptFN As String = Request.Url.Query.Replace("?", "")
        '    Dim DecryptFN As String = LicensingBLL.GetStringFromEncryptedValueByUTF8(EncryptFN).Replace("FileName=", "")
        '    Dim dlDir As String = System.Configuration.ConfigurationManager.AppSettings("UploadFilesPath")
        '    Dim strFileName As String = Server.UrlDecode(DecryptFN)
        '    Dim path As String = Server.MapPath(dlDir + strFileName)
        '    Dim toDownload As System.IO.FileInfo = New System.IO.FileInfo(path)

        '    Response.Clear()
        '    Response.ContentType = "application/octet-stream"
        '    Response.AddHeader("Content-Disposition", "attachment;filename=" + toDownload.Name)

        '    Response.TransmitFile(path)
        '    Response.End()
        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub
End Class
