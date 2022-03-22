Imports ATS.BO.Framework

Partial Class Authenticate_Controls_ctlPasswordChange
    Inherits System.Web.UI.UserControl
    Dim moEmployee As BOEmployee = New BOEmployee
    Dim eEmployee As New BOEEmployee
    Protected Sub btnPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPassword.Click

        Dim Username As String = ""
        If Not Request.QueryString("Username") Is Nothing Then
            Username = Request.QueryString("Username")
        End If

        If txtNewPassword.Text <> "" And txtReTypePassword.Text <> "" Then
            Dim AccountEmployeeId As Integer

            Dim IsPasswordExpired As Boolean = False

            Dim CurrentPassword As String = txtCurrentPassword.Text
            Dim NewPassword As String = txtNewPassword.Text

            If CurrentPassword = NewPassword Then
                ShowNotFoundMessagePasswordSameValidation()
                Return
            End If
            Dim moEmbloyee As New ATS.BO.Framework.BOEmployee

            AccountEmployeeId = DBUtilities.GetSessionAccountEmployeeId



            If moEmployee.GetEmployeeByUserNameAndPassword(Username, CurrentPassword).Tables.Count > 0 AndAlso moEmployee.GetEmployeeByUserNameAndPassword(Username, CurrentPassword).Tables(0).Rows.Count > 0 Then
                If moEmbloyee.ChangePassword(AccountEmployeeId, NewPassword) Then
                    ShowPasswordChangedMessage()
                End If
            Else
                ShowNotFoundMessagePasswordWrongValidation()
            End If
        End If
    End Sub
    Public Sub ShowNotFoundMessagePasswordSameValidation()
        Dim strMessage As String = "The New Password cannot be the same as the Current Password. Please enter a new password."
        Dim strScript As String = "alert('" & strMessage & "'); "
        If (Not Me.Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "clientScript", strScript, True)
        End If
    End Sub

    Public Sub ShowPasswordChangedMessage()
        Dim strMessage As String = "Your password has been successfully changed. Please login with your new password."
        Dim strScript As String = "alert('" & strMessage & "'); window.location.href = '../Default.aspx';"
        If (Not Me.Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "clientScript", strScript, True)
        End If
        'window.location = '" & URL & "';
    End Sub

    Public Sub ShowNotFoundMessagePasswordWrongValidation()
        Dim strMessage As String = "Incorrect current password."
        Dim strScript As String = "alert('" & strMessage & "'); "
        If (Not Me.Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "clientScript", strScript, True)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
