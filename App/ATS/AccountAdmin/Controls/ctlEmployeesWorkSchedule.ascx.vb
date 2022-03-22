Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlEmployeesWorkSchedule
    Inherits System.Web.UI.UserControl
    Dim moDepartment As BODepartment = New BODepartment
    Dim oEmployee As New BOEmployee
    Dim dt As DataTable = New DataTable
    Dim WorkScheduleId As Integer = 0



    Public Sub ListBoxDataBinding(ByVal Id As Integer)
        SelectedListBox.DataSource = GetWorkScheduleEmployeesDataSource(Id)
        SelectedListBox.DataBind()
    End Sub

    Private Function GetWorkScheduleEmployeesDataSource(ByVal WorkScheduleId As Integer) As DataView
        Dim dv As DataView = Nothing

        Dim ds As System.Data.DataSet
        Try
            Dim oWorkScheduleEmployee As New BOWorkScheduleEmployee
            ds = oWorkScheduleEmployee.GetWorkScheduleEmployeesDataset(WorkScheduleId, 1, 100000)
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
        WorkScheduleId = Session("WorkScheduleId")
        For Each item As RadListBoxItem In e.Items
            Dim EmployeeId As Integer = item.Value
            DeleteEmployee(WorkScheduleId, EmployeeId)
        Next
    End Sub

    Protected Sub SelectedListBox_Inserted(sender As Object, e As RadListBoxEventArgs) Handles SelectedListBox.Inserting
        WorkScheduleId = Session("WorkScheduleId")
        For Each item As RadListBoxItem In e.Items
            Dim EmployeeId As Integer = item.Value
            AddEmployee(WorkScheduleId, EmployeeId)
        Next
    End Sub



    Private Sub AddEmployee(ByVal WorkScheduleId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOWorkScheduleEmployee
        With oEmployee
            If Not .AddEmployee(WorkScheduleId, EmployeeId) Then
                Label2.Text = .InfoMessage
            Else
                Label2.Text = ""
            End If
        End With
    End Sub

    Private Sub DeleteEmployee(ByVal WorkScheduleId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOWorkScheduleEmployee
        With oEmployee
            If Not .DeleteEmployee(WorkScheduleId, EmployeeId) Then
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
