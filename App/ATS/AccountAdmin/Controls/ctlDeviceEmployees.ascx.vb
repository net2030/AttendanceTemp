Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlDeviceEmployees
    Inherits System.Web.UI.UserControl

    Dim oEmployee As New BOEmployee
    Dim dt As DataTable = New DataTable
    Dim MachineId As Integer = 0



    Public Sub ListBoxDataBinding(ByVal Id As Integer)
        SelectedListBox.DataSource = GetMachineEmployeesDataSource(Id)
        SelectedListBox.DataBind()
    End Sub

    Private Function GetMachineEmployeesDataSource(ByVal MachineId As Integer) As DataView
        Dim dv As DataView = Nothing

        Dim ds As System.Data.DataSet
        Try
            Dim oMachineEmployee As New BOMachineEmployee
            ds = oMachineEmployee.GetMachineEmployeesDataset(MachineId, 1, 100000)
            dv = ds.Tables(0).DefaultView
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try

        Return dv
    End Function

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            AvailableListBox.DataSource = oEmployee.GetDepartmentEmployeesDataset(ctlDepartmentTree1.SelectedValue, 1, 1000).Tables(0)
            AvailableListBox.DataBind()
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
    End Sub

    Protected Sub SelectedListBox_Deleted(sender As Object, e As RadListBoxEventArgs) Handles SelectedListBox.Deleted
        MachineId = Session("MachineId")
        For Each item As RadListBoxItem In e.Items
            Dim EmployeeId As Integer = item.Value
            DeleteEmployee(MachineId, EmployeeId)
        Next
    End Sub

    Protected Sub SelectedListBox_Inserted(sender As Object, e As RadListBoxEventArgs) Handles SelectedListBox.Inserting
        MachineId = Session("MachineId")
        For Each item As RadListBoxItem In e.Items
            Dim EmployeeId As Integer = item.Value
            AddEmployee(MachineId, EmployeeId)
        Next
    End Sub

    Private Sub AddEmployee(ByVal MachineId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOMachineEmployee
        With oEmployee
            If Not .AddEmployee(MachineId, EmployeeId) Then
                Label2.Text = .InfoMessage
            Else
                Label2.Text = ""
            End If
        End With
    End Sub

    Private Sub DeleteEmployee(ByVal MachineId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOMachineEmployee
        With oEmployee
            If Not .DeleteEmployee(MachineId, EmployeeId) Then
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
