Imports System.Data
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Net
Imports System.Collections.Specialized
Imports System.Text

''' <summary>
''' Summary description for HttpHelper
''' </summary>
''' <Author>Samer Abu Rabie</Author>
Public NotInheritable Class HttpHelper
    Private Sub New()
    End Sub
    ''' <summary>
    ''' This method prepares an Html form which holds all data in hidden field in the addetion to form submitting script.
    ''' </summary>
    ''' <param name="url">The destination Url to which the post and redirection will occur, the Url can be in the same App or ouside the App.</param>
    ''' <param name="data">A collection of data that will be posted to the destination Url.</param>
    ''' <returns>Returns a string representation of the Posting form.</returns>
    ''' <Author>Samer Abu Rabie</Author>
    Private Shared Function PreparePOSTForm(url As String, data As NameValueCollection) As [String]
        'Set a name for the form
        Dim formID As String = "PostForm"

        'Build the form using the specified data to be posted.
        Dim strForm As New StringBuilder()
        strForm.Append("<form id=""" & formID & """ name=""" & formID & """ action=""" & url & """ method=""POST"">")
        For Each key As String In data
            strForm.Append("<input type=""hidden"" name=""" & key & """ value=""" & data(key) & """>")
        Next
        strForm.Append("</form>")

        'Build the JavaScript which will do the Posting operation.
        Dim strScript As New StringBuilder()
        strScript.Append("<script language='javascript'>")
        strScript.Append("var v" & formID & " = document." & formID & ";")
        strScript.Append("v" & formID & ".submit();")
        strScript.Append("</script>")

        'Return the form and the script concatenated. (The order is important, Form then JavaScript)
        Return strForm.ToString() & strScript.ToString()
    End Function
    ''' <summary>
    ''' POST data and Redirect to the specified url using the specified page.
    ''' </summary>
    ''' <param name="page">The page which will be the referrer page.</param>
    ''' <param name="destinationUrl">The destination Url to which the post and redirection is occuring.</param>
    ''' <param name="data">The data should be posted.</param>
    ''' <Author>Samer Abu Rabie</Author>
    Public Shared Sub RedirectAndPOST(page As Page, destinationUrl As String, data As NameValueCollection)
        'Prepare the Posting form
        Dim strForm As String = PreparePOSTForm(destinationUrl, data)

        'Add a literal control the specified page holding the Post Form, this is to submit the Posting form with the request.
        page.Controls.Add(New LiteralControl(strForm))
    End Sub
End Class
