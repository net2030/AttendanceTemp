Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlEmployeesLeavePolicy
    Inherits System.Web.UI.UserControl
    Dim moDepartment As BODepartment = New BODepartment
    Dim oEmployee As New BOEmployee
    Dim dt As DataTable = New DataTable
    Dim PolicyId As Integer = 0
    Dim dupplicated As Boolean = False
    

    Public Sub ListBoxDataBinding(ByVal Id As Integer)
        Me.dsEmployeesByManager.SelectParameters.Item("PolicyId").DefaultValue = Id
        SelectedListBox.DataBind()
    End Sub


    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            AvailableListBox.Items.Clear()
            AvailableListBox.DataSource = oEmployee.GetDepartmentEmployeesDataset(ctlDepartmentTree1.SelectedValue, 1, 100).Tables(0)
            AvailableListBox.DataBind()
            AvailableListBox.Sort = RadListBoxSort.Ascending
            AvailableListBox.SortItems()
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
    End Sub

    Protected Sub SelectedListBox_Deleting(sender As Object, e As RadListBoxDeletingEventArgs) Handles SelectedListBox.Deleting
        PolicyId = Session("TimeOffPolicyId")
        For Each item As RadListBoxItem In e.Items
            Dim EmployeeId As Integer = item.Value
            DeleteEmployee(PolicyId, EmployeeId)
        Next
    End Sub


  

   Protected Sub SelectedListBox_Inserted(sender As Object, e As RadListBoxEventArgs) Handles SelectedListBox.Inserting
        PolicyId = Session("TimeOffPolicyId")
        For Each item As RadListBoxItem In e.Items
            Dim EmployeeId As Integer = item.Value
            AddEmployee(PolicyId, EmployeeId)
        Next
    End Sub


    Private Sub AddEmployee(ByVal TimeOffPolicyId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOTimeOffPolicy
        With oEmployee
            If Not .AddEmployee(TimeOffPolicyId, EmployeeId) Then
                Label2.Text = .InfoMessage
                dupplicated = True
            Else
                Label2.Text = ""
                dupplicated = False
            End If
        End With
    End Sub

    Private Sub DeleteEmployee(ByVal TimeOffPolicyId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOTimeOffPolicy
        With oEmployee
            If Not .DeleteEmployee(TimeOffPolicyId, EmployeeId) Then
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

    Protected Sub SelectedListBox_Transferred(sender As Object, e As RadListBoxTransferredEventArgs) Handles AvailableListBox.Transferred
        For Each item As RadListBoxItem In e.Items

            If dupplicated Then

                e.SourceListBox.Items.Add(item)
                item.ForeColor = System.Drawing.Color.Red
                ' AvailableListBox.DataBind()
                SelectedListBox.DataBind()

                AvailableListBox.Sort = RadListBoxSort.Ascending
                AvailableListBox.SortItems()

            End If
        Next

        'ListBoxDataBinding(PolicyId)
        'SelectedListBox.DataBind()
    End Sub

   
End Class
