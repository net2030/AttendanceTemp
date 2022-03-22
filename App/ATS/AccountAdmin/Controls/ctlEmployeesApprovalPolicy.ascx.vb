Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlEmployeesApprovalPolicy
    Inherits System.Web.UI.UserControl
    Dim moDepartment As BODepartment = New BODepartment
    Dim oEmployee As New BOEmployee
    Dim dt As DataTable = New DataTable
    Dim PolicyId As Integer = 0



    Public Sub ListBoxDataBinding(ByVal Id As Integer)
        Me.dsEmployeesByPolicy.SelectParameters.Item("PolicyId").DefaultValue = Id
        SelectedListBox.DataBind()
    End Sub


    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            AvailableListBox.DataSource = oEmployee.GetDepartmentEmployeesDataset(ctlDepartmentTree1.SelectedValue, 1, 100).Tables(0)
            AvailableListBox.DataBind()
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
    End Sub




    Protected Sub SelectedListBox_Inserted(sender As Object, e As RadListBoxEventArgs) Handles SelectedListBox.Inserting
        PolicyId = Session("ApprovalPolicyId")
        For Each item As RadListBoxItem In e.Items
            Dim EmployeeId As Integer = item.Value
            AddEmployee(PolicyId, EmployeeId)
        Next
    End Sub


    Private Sub AddEmployee(ByVal ApprovalPolicyId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOApprovalPolicy
        With oEmployee
            If Not .AddEmployee(ApprovalPolicyId, EmployeeId) Then
                Label2.Text = .InfoMessage
            Else
                Label2.Text = ""
            End If
        End With
    End Sub

    Private Sub DeleteEmployee(ByVal ApprovalPolicyId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOApprovalPolicy
        With oEmployee
            If Not .DeleteEmployee(ApprovalPolicyId, EmployeeId) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With
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
