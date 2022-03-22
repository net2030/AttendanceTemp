Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Attendance_Controls_ctlGatePassForApproval
    Inherits System.Web.UI.UserControl

    Dim moBOGatepass As New ATS.BO.Framework.BOGatepass



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            'If Me.Request.QueryString("GatepassId") Is Nothing Then
            '    FormView1.ChangeMode(FormViewMode.Insert)
            'Else
            '    Me.dsGatePass.SelectParameters("GatepassId").DefaultValue = Me.Request.QueryString("GatepassId")
            '    FormView1.ChangeMode(FormViewMode.Edit)
            'End If
            '  Me.dsGatePass.SelectParameters.Item("EmployeeManagerID").DefaultValue = Session("AccountEmployeeId")
            Me.GridView2.DataBind()


        End If

    End Sub


    Protected Sub btnAddGatePass_Click()
        Dim GatePassId As Integer
        Dim UserId As Integer = Session("AccountEmployeeId")
        Dim Notes As String

        For Each row1 As GridViewRow In GridView2.Rows

            GatePassId = Me.GridView2.DataKeys(row1.RowIndex)("GatePassId")
            Notes = CType(row1.FindControl("Comments"), TextBox).Text

            If DirectCast(row1.FindControl("rdSpecificEmployee"), CheckBox).Checked = True Then
                If Not moBOGatepass.UpdateGatepassApprovalStatus(GatePassId, True, False, UserId, Notes) Then
                    GoTo end_of_for
                End If
            End If
            If DirectCast(row1.FindControl("rdSpecificEmployeeRejected"), CheckBox).Checked = True Then
                If Not moBOGatepass.UpdateGatepassApprovalStatus(GatePassId, False, True, UserId, Notes) Then
                    GoTo end_of_for
                End If
            End If
        Next

end_of_for:
        GridView2.DataBind()
    End Sub


End Class
