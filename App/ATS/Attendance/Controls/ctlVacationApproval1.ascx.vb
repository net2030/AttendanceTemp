Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Attendance_Controls_ctlVacationApproval1
    Inherits System.Web.UI.UserControl

    Dim moBOVacation As New ATS.BO.Framework.BOVacation



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            GridView1.Caption = GridView1.Caption + Session("Role")
            Me.dsVacation.SelectParameters.Item("ManagerID").DefaultValue = Session("AccountEmployeeId")
            Me.EmployeeManagerGridView.DataBind()

            'Me.dsVacation1.SelectParameters.Item("ManagerID").DefaultValue = Session("AccountEmployeeId")
            'Me.GridView1.DataBind()


        End If

    End Sub


    Protected Sub btnAddVacation_Click()
        UpdateApprovalStatusEmployeeManager()
        UpdateApprovalStatusSpecificRole()

    End Sub

    Private Sub UpdateApprovalStatusEmployeeManager()
        For Each row1 As GridViewRow In EmployeeManagerGridView.Rows
            'For Approved
            If DirectCast(row1.FindControl("rdSpecificEmployee"), CheckBox).Checked = True Then
                moBOVacation.AddVacationApproval(Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("VacationId"),
                                                 Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("ApprovalPolicyId"),
                                                 Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("ApprovalPathId"),
                                                 True,
                                                 False,
                                                 CType(row1.FindControl("Comments"), TextBox).Text,
                                                Session("AccountEmployeeId"))
                'Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("ManagerId"))

                If Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("ApprovalPathSequence") = Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("MaxApprovalPathSequence") Then
                    moBOVacation.UpdateVacationApprovalStatus(Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("VacationId"),
                                                              True,
                                                              False,
                                                              Session("AccountEmployeeId"),
                                                              CType(row1.FindControl("Comments"), TextBox).Text)
                End If
            End If
            'For Rejected
            If DirectCast(row1.FindControl("rdSpecificEmployeeRejected"), CheckBox).Checked = True Then
                moBOVacation.AddVacationApproval(Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("VacationId"),
                                                 Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("ApprovalPolicyId"),
                                                 Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("ApprovalPathId"),
                                                 False,
                                                 True,
                                                 CType(row1.FindControl("Comments"), TextBox).Text,
                                                  Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("ManagerId"))

                moBOVacation.UpdateVacationApprovalStatus(Me.EmployeeManagerGridView.DataKeys(row1.RowIndex)("VacationId"),
                                                          False,
                                                          True,
                                                          Session("AccountEmployeeId"),
                                                          CType(row1.FindControl("Comments"), TextBox).Text)
            End If

        Next
        EmployeeManagerGridView.DataBind()
    End Sub

    Private Sub UpdateApprovalStatusSpecificRole()
        For Each row1 As GridViewRow In GridView1.Rows
            'For Approved
            If DirectCast(row1.FindControl("rdSpecificEmployee"), CheckBox).Checked = True Then
                moBOVacation.AddVacationApproval(Me.GridView1.DataKeys(row1.RowIndex)("VacationId"),
                                                 Me.GridView1.DataKeys(row1.RowIndex)("ApprovalPolicyId"),
                                                 Me.GridView1.DataKeys(row1.RowIndex)("ApprovalPathId"),
                                                 True,
                                                 False,
                                                 CType(row1.FindControl("Comments"), TextBox).Text,
                                                 Session("AccountEmployeeId"))

                If Me.GridView1.DataKeys(row1.RowIndex)("ApprovalPathSequence") = Me.GridView1.DataKeys(row1.RowIndex)("MaxApprovalPathSequence") Then
                    moBOVacation.UpdateVacationApprovalStatus(Me.GridView1.DataKeys(row1.RowIndex)("VacationId"),
                                                              True,
                                                              False,
                                                              Session("AccountEmployeeId"),
                                                              CType(row1.FindControl("Comments"), TextBox).Text)
                End If
            End If
            'For Rejected
            If DirectCast(row1.FindControl("rdSpecificEmployeeRejected"), CheckBox).Checked = True Then
                moBOVacation.AddVacationApproval(Me.GridView1.DataKeys(row1.RowIndex)("VacationId"),
                                                 Me.GridView1.DataKeys(row1.RowIndex)("ApprovalPolicyId"),
                                                 Me.GridView1.DataKeys(row1.RowIndex)("ApprovalPathId"),
                                                 False,
                                                 True,
                                                 CType(row1.FindControl("Comments"), TextBox).Text,
                                                  Session("AccountEmployeeId"))

                moBOVacation.UpdateVacationApprovalStatus(Me.GridView1.DataKeys(row1.RowIndex)("VacationId"),
                                                          False,
                                                          True,
                                                          Session("AccountEmployeeId"),
                                                          CType(row1.FindControl("Comments"), TextBox).Text)
            End If

        Next
        GridView1.DataBind()
    End Sub

    Protected Sub EmployeeManagerGridView_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles EmployeeManagerGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim VacationId As Integer = DirectCast(Me.EmployeeManagerGridView.DataKeys(e.Row.RowIndex)("VacationId"), Integer)
            Dim grdViewOrdersOfCustomer As GridView = DirectCast(e.Row.FindControl("grdViewApprovalLog"), GridView)
            grdViewOrdersOfCustomer.DataSource = moBOVacation.GetVacationApprovalLogByVacationId(VacationId)
            grdViewOrdersOfCustomer.DataBind()
        End If
    End Sub
End Class
