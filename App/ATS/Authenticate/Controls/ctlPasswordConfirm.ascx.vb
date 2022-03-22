
Partial Class Authenticate_Controls_ctlPasswordConfirm
    Inherits System.Web.UI.UserControl

    Protected Sub btnPassword_Click(sender As Object, e As System.EventArgs) Handles btnPassword.Click

        Dim NewPassword As String = txtNewPassword.Text
        Dim PasswordVerificationCode As Guid = System.Guid.Empty
        If Not Me.Request.QueryString("Code") Is Nothing Then
            PasswordVerificationCode = New Guid(Me.Request.QueryString("Code"))
        End If

        Dim moEmployee As ATS.BO.Framework.BOEmployee = New ATS.BO.Framework.BOEmployee
        Dim ds As System.Data.DataSet = moEmployee.GetEmployeeByPasswordVerificationCode(PasswordVerificationCode)
        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            moEmployee.UpdatePasswordReset(dr("EMailAddress"), NewPassword)

                'moEmployee.SendPasswordResetEMail(dr.EMailAddress, NewPassword)
            ShowPasswordChangedMessage(Resources.MulResource.PasswordRestSuccessfully)
            Else
            ShowPasswordChangedMessage(Resources.MulResource.LinkExpired)
            End If
    End Sub
    Public Sub ShowPasswordChangedMessage(message As String)
        Dim strMessage As String = message
        Dim strScript As String = "alert('" & strMessage & "'); window.location.href = '../Default.aspx';"
        If (Not Me.Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "clientScript", strScript, True)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim PasswordVerificationCode As Guid = System.Guid.Empty
        If Not Me.Request.QueryString("Code") Is Nothing Then
            PasswordVerificationCode = New Guid(Me.Request.QueryString("Code"))
        End If
        Dim moEmployee As ATS.BO.Framework.BOEmployee = New ATS.BO.Framework.BOEmployee
        Dim ds As System.Data.DataSet = moEmployee.GetEmployeeByPasswordVerificationCode(PasswordVerificationCode)
        If ds.Tables.Count = 0 Or ds.Tables(0).Rows.Count = 0 Then
            ShowPasswordChangedMessage(Resources.MulResource.LinkExpired)
        End If
    End Sub
End Class
