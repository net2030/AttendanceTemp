Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Mobile_Controls_ctlWorkExceptionApproval
    Inherits System.Web.UI.UserControl

    Dim moBOWorkException As New ATS.BO.Framework.BOWorkException

#Region "Load"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            GridViewBinding()

        End If

    End Sub

#End Region

#Region "Data Binding"
    Private Sub GridViewBinding()

    End Sub
#End Region

#Region "Update Approval Status"
    Protected Sub btnApprove_Click()
        Dim WorkExceptionId As Integer
        Dim UserId As Integer = Session("AccountEmployeeId")
        Dim Notes As String
        For Each item In R.Items
            WorkExceptionId = CInt(DirectCast(item.FindControl("HiddenField1"), HiddenField).Value)
            Notes = CType(item.FindControl("txtNotes"), TextBox).Text

            If DirectCast(item.FindControl("rdApproved"), CheckBox).Checked = True Then
                If Not moBOWorkException.UpdateWorkExceptionApprovalStatus(WorkExceptionId, True, False, UserId, Notes) Then
                    GoTo end_of_for
                End If
            End If
            If DirectCast(item.FindControl("rdRejected"), CheckBox).Checked = True Then
                If Not moBOWorkException.UpdateWorkExceptionApprovalStatus(WorkExceptionId, False, True, UserId, Notes) Then
                    GoTo end_of_for
                End If
            End If
        Next

end_of_for:
        R.DataBind()
    End Sub
#End Region

End Class
