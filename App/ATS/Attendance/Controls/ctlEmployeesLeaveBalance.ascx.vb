Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlEmployeesLeaveBalance
    Inherits System.Web.UI.UserControl

    Dim oEmployee As New BOEmployee
    Private intEmployeeId As Integer
    Private intTypeId As Integer

#Region "General Declarations"
    Dim moTimeOffPolicyDetail As BOTimeOffPolicyDetail = New BOTimeOffPolicyDetail
    Dim moVersionNo As Byte()
#End Region


    Public Property EmployeeId() As Integer
        Get
            Return intEmployeeId
        End Get
        Set(value As Integer)
            intEmployeeId = value
        End Set
    End Property

    Public Property TypeId() As Integer
        Get
            Return intTypeId
        End Get
        Set(value As Integer)
            intTypeId = value
        End Set
    End Property

    Public Sub GridViewDataBinding(ByVal EmployeeId As Integer, ByVal TypeId As Integer)
        Try
            Me.EmployeeId = EmployeeId
            Me.TypeId = TypeId
            gvBalance.DataSource = New ATS.BO.Framework.BOTimeOffPolicy().GetTimeOffBalanceByEmployeeID(EmployeeId, TypeId, Request.Cookies("CurrentLanguage").Value.ToString())
            gvBalance.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvBalance_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvBalance.RowEditing
        gvBalance.EditIndex = e.NewEditIndex
        GridViewDataBinding(gvBalance.DataKeys(e.NewEditIndex)(1), gvBalance.DataKeys(e.NewEditIndex)(3))
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvBalance.RowCancelingEdit
        gvBalance.EditIndex = -1
        GridViewDataBinding(gvBalance.DataKeys(e.RowIndex)(1), gvBalance.DataKeys(e.RowIndex)(3))
    End Sub

    Protected Sub gvBalance_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvBalance.RowUpdating

        If Not moTimeOffPolicyDetail.UpdateBalance(gvBalance.DataKeys(e.RowIndex)(0),
                                                  DirectCast(gvBalance.Rows(e.RowIndex).FindControl("txtCarryForward"), RadNumericTextBox).Value,
                                                  Session("AccountEmployeeId"),
                                                  gvBalance.DataKeys(e.RowIndex)(2)) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moTimeOffPolicyDetail.InfoMessage), True)
        End If

        gvBalance.EditIndex = -1
        GridViewDataBinding(gvBalance.DataKeys(e.RowIndex)(1), gvBalance.DataKeys(e.RowIndex)(3))
    End Sub

    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function
End Class
