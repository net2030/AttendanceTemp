Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Attendance_Controls_ctlVacationApproval
    Inherits System.Web.UI.UserControl

    Dim moVacation As New ATS.BO.Framework.BOVacation


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


    End Sub

    Protected Sub btnAddGatePass_Click()

        Dim VacationId As Integer
        Dim UserId As Integer = Session("AccountEmployeeId")
        Dim Notes As String

        For Each row1 As GridViewRow In GridView2.Rows

            VacationId = Me.GridView2.DataKeys(row1.RowIndex)("VacationId")
            Notes = CType(row1.FindControl("Comments"), TextBox).Text

            If DirectCast(row1.FindControl("rdSpecificEmployee"), CheckBox).Checked = True Then
                If Not moVacation.UpdateVacationApprovalStatus(VacationId, True, False, UserId, Notes) Then
                    GoTo end_of_for
                End If
            End If
            If DirectCast(row1.FindControl("rdSpecificEmployeeRejected"), CheckBox).Checked = True Then
                If Not moVacation.UpdateVacationApprovalStatus(VacationId, False, True, UserId, Notes) Then
                    GoTo end_of_for
                End If
            End If
        Next

end_of_for:
        GridView2.DataBind()
    End Sub


End Class
