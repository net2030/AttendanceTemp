Imports System.Management
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports Telerik.Web.UI

Partial Class AccountAdmin_Controls_ctlPreferences
    Inherits System.Web.UI.UserControl
    Private flg As Boolean

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Init



    End Sub

    Protected Sub UpdateAccount()

        Try
            Dim obj As AccountDAL = New AccountDAL
            Dim UserId As Integer = Session("AccountEmployeeId")
            Dim AccountId As Integer = Session("AccountId")
            Dim CompanyName As String = CType(FormView1.FindControl("txtCompanyName"), TextBox).Text
            Dim Address As String = CType(FormView1.FindControl("txtAddress"), TextBox).Text
            Dim Email As String = CType(FormView1.FindControl("EMailAddressTextBox"), TextBox).Text
            Dim Tel As String = CType(FormView1.FindControl("txtTel"), TextBox).Text
            Dim Fax As String = CType(FormView1.FindControl("txtFax"), TextBox).Text
            Dim PageSize As Integer = CInt(CType(FormView1.FindControl("txtPageSize"), TextBox).Text)
            Dim culture As String = CType(FormView1.FindControl("ddlCalender"), DropDownList).SelectedValue
            Dim NoOfSepratedAbsenceDays As Integer = CInt(CType(FormView1.FindControl("txtNoOfSepratedAbsenceDays"), TextBox).Text)
            Dim NoOfConsecutiveAbsenceDays As Integer = CInt(CType(FormView1.FindControl("txtNoOfConsecutiveAbsenceDays"), TextBox).Text)
            Dim VerificationLogStartTime As Date = CDate(CType(FormView1.FindControl("timVerificationLogStartTime"), RadTimePicker).SelectedDate)
            Dim VerificationLogEndTime As Date = CDate(CType(FormView1.FindControl("timVerificationLogEndTime"), RadTimePicker).SelectedDate)
            Dim FileUpload2 As FileUpload = CType(FormView1.FindControl("FileUpload2"), FileUpload)
            obj.UpdateAccount(CompanyName,
                              Address,
                              Email,
                              Tel,
                              Fax,
                              culture,
                              PageSize,
                              NoOfSepratedAbsenceDays,
                              NoOfConsecutiveAbsenceDays,
                              VerificationLogStartTime,
                              VerificationLogEndTime,
                              UserId,
                              AccountId)
            obj.FileUpload(FileUpload2)
            lbl1.Visible = True
            lbl1.ForeColor = Color.Green
            lbl1.Text = "تم  حفظ البيانات بنجاح"
            FormView1.Visible = False

        Catch ex As Exception
            lbl1.Visible = True
            lbl1.ForeColor = Color.Red
            lbl1.Text = "لم يتم التحديث"
            FormView1.Visible = False
        End Try
    End Sub

    Protected Sub UpgradeLicense()



        CType(FormView1.FindControl("Readers"), DropDownList).Enabled = True
        CType(FormView1.FindControl("LicenseType"), DropDownList).Visible = True
        CType(FormView1.FindControl("lblLicenseType"), Label).Visible = False

        CType(FormView1.FindControl("Button3"), Button).Visible = True
        CType(FormView1.FindControl("Button2"), Button).Visible = False






    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs)
        Dim DeviceId As String = programLic.SystemID()


        Dim ServerURL As String

        ServerURL = Request.Url.AbsoluteUri.Replace("AccountPreferences.aspx", "LicenseActivation.aspx") ' Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "") + "/AccountAdmin/LicenseActivation.aspx"

        Dim url = "http://mastechnology.net/Activation.php"
        Response.Clear()
        Dim sb = New System.Text.StringBuilder()
        sb.Append("<html>")
        sb.AppendFormat("<body onload='document.forms[0].submit()'>")
        sb.AppendFormat("<form action='{0}' method='post'>", url)
        sb.AppendFormat("<input type='hidden' name='txtCompanyName' value='{0}'>", CType(FormView1.FindControl("txtCompanyName"), TextBox).Text)
        sb.AppendFormat("<input type='hidden' name='txtAddress' value='{0}'>", CType(FormView1.FindControl("txtAddress"), TextBox).Text)
        sb.AppendFormat("<input type='hidden' name='EMailAddressTextBox' value='{0}'>", CType(FormView1.FindControl("EMailAddressTextBox"), TextBox).Text)
        sb.AppendFormat("<input type='hidden' name='txtTel' value='{0}'>", CType(FormView1.FindControl("txtTel"), TextBox).Text)
        sb.AppendFormat("<input type='hidden' name='txtFax' value='{0}'>", CType(FormView1.FindControl("txtFax"), TextBox).Text)
        sb.AppendFormat("<input type='hidden' name='Readers' value='{0}'>", CType(FormView1.FindControl("Readers"), DropDownList).SelectedValue)
        sb.AppendFormat("<input type='hidden' name='LicenseType' value='{0}'>", CType(FormView1.FindControl("LicenseType"), DropDownList).SelectedValue)
        sb.AppendFormat("<input type='hidden' name='txtDeviceID' value='{0}'>", programLic.AES_encrypt(DeviceId))
        sb.AppendFormat("<input type='hidden' name='ServerURL' value='{0}'>", ServerURL)
        sb.Append("</form>")
        sb.Append("</body>")
        sb.Append("</html>")
        Response.Write(sb.ToString())
        Response.End()

    End Sub


    Protected Sub Button3_Click1(sender As Object, e As EventArgs)
        Dim DeviceId As String = programLic.SystemID()


        Dim ServerURL As String

        ServerURL = Request.Url.AbsoluteUri.Replace("AccountPreferences.aspx", "LicenseActivation.aspx") ' Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "") + "/AccountAdmin/LicenseActivation.aspx"

        Dim url = "http://mastechnology.net/Activation.php"



        Dim reqparm As New Specialized.NameValueCollection
        reqparm.Add("txtCompanyName", CType(FormView1.FindControl("txtCompanyName"), TextBox).Text)
        reqparm.Add("txtAddress", CType(FormView1.FindControl("txtAddress"), TextBox).Text)
        reqparm.Add("EMailAddressTextBox", CType(FormView1.FindControl("EMailAddressTextBox"), TextBox).Text)
        reqparm.Add("txtTel", CType(FormView1.FindControl("txtTel"), TextBox).Text)
        reqparm.Add("txtFax", CType(FormView1.FindControl("txtFax"), TextBox).Text)
        reqparm.Add("Readers", CType(FormView1.FindControl("Readers"), DropDownList).SelectedValue)
        reqparm.Add("LicenseType", CType(FormView1.FindControl("LicenseType"), DropDownList).SelectedValue)
        reqparm.Add("txtDeviceID", programLic.AES_encrypt(DeviceId))
        reqparm.Add("ServerURL", ServerURL)

        If programLic.SendActivationRequest(reqparm) Then

            lbl1.Visible = True
            lbl1.ForeColor = Color.Green
            lbl1.Text = "تم  إرسال طلب تفعيل الرخصة بنجاح"
            FormView1.Visible = False

        Else
            lbl1.Visible = True
            lbl1.ForeColor = Color.Red
            lbl1.Text = "لم يتم إرسال طلب تفعيل الرخصة"
            FormView1.Visible = False
        End If



    End Sub

End Class
