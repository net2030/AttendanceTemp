Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Mobile_Controls_ctlGatepassApproval
    Inherits System.Web.UI.UserControl

    Dim moBOGatepass As New ATS.BO.Framework.BOGatepass

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
        Dim GatePassId As Integer
        Dim UserId As Integer = Session("AccountEmployeeId")
        Dim Notes As String
        For Each item In R.Items
            GatePassId = CInt(DirectCast(item.FindControl("HiddenField1"), HiddenField).Value)
            Notes = CType(item.FindControl("txtNotes"), TextBox).Text

            If DirectCast(item.FindControl("rdApproved"), CheckBox).Checked = True Then
                If Not moBOGatepass.UpdateGatepassApprovalStatus(GatePassId, True, False, UserId, Notes) Then
                    GoTo end_of_for
                End If
            End If
            If DirectCast(item.FindControl("rdRejected"), CheckBox).Checked = True Then
                If Not moBOGatepass.UpdateGatepassApprovalStatus(GatePassId, False, True, UserId, Notes) Then
                    GoTo end_of_for
                End If
            End If
        Next

end_of_for:
        R.DataBind()
    End Sub
    
#End Region

End Class
