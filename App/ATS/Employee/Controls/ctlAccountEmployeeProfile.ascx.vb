Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class Controls_ctlAccountEmployeeProfile
    Inherits System.Web.UI.UserControl

    Dim moEmployee As BOEmployee = New BOEmployee
    Dim eEmployee As New BOEEmployee
    Dim IsUpdate As Boolean
    Public EmployeeId As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Dim GatePassId As String = Session("AccountEmployeeId")
            Dim EmployeesList As New List(Of BOEEmployee)()
            eEmployee = moEmployee.Find(GatePassId)

            FirstNameTextBox.Text = eEmployee.FirstName
            txtUsername.Text = eEmployee.UserName
            MiddleNameTextBox.Text = eEmployee.MiddleName
            LastNameTextBox.Text = eEmployee.LastName
            EMailAddressTextBox.Text = eEmployee.EmailAddress

        Catch ex As Exception
            Dim x As String = ex.InnerException.Message
        End Try
    End Sub

    Protected Sub Update_Click()

        If PasswordTextBox.Text <> "" And VerifyPasswordTextbox.Text <> "" Then
            Dim AccountEmployeeId As Integer
            Dim Username As String = ""
            Dim IsPasswordExpired As Boolean = False

            Dim CurrentPassword As String = CurrentPasswordTextBox.Text
            Dim NewPassword As String = PasswordTextBox.Text

            If CurrentPassword = NewPassword Then
                ShowNotFoundMessagePasswordSameValidation()
                Return
            End If
            Dim moEmbloyee As New ATS.BO.Framework.BOEmployee

            AccountEmployeeId = DBUtilities.GetSessionAccountEmployeeId
            Username = Session("UserName")


            If moEmployee.GetEmployeeByUserNameAndPassword(Username, CurrentPassword).Tables.Count > 0 AndAlso moEmployee.GetEmployeeByUserNameAndPassword(Username, CurrentPassword).Tables(0).Rows.Count > 0 Then
                If moEmbloyee.ChangePassword(AccountEmployeeId, NewPassword) Then
                    ShowPasswordChangedMessage()
                End If
            Else
                ShowNotFoundMessagePasswordWrongValidation()
            End If
        End If
        'Dim fu As FileUpload = DirectCast(Me.FormView1.FindControl("FileUpload2"), FileUpload)
        'moEmployee.FileUploadForProfile(fu, DBUtilities.GetSessionAccountEmployeeId)
        'Response.Redirect(UIUtilities.RedirectToHomePage, False)
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
End Class
