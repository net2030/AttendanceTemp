Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlManagerScope
    Inherits System.Web.UI.UserControl
    Dim moDepartment As BODepartment = New BODepartment
    Dim oEmployee As New BOEmployee
    Dim dt As DataTable = New DataTable
    Dim EmployeeId As Integer

    

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.dsEmployeesByManager.SelectParameters.Item("ManagerId").DefaultValue = Session("EmployeeId1")
            SelectedListBox.DataBind()
        End If
    End Sub


    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            AvailableListBox.DataSource = oEmployee.GetDepartmentEmployeesDataset(ctlDepartmentTree1.SelectedValue, 1, 100).Tables(0)
            AvailableListBox.DataBind()
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
    End Sub


    Protected Sub SelectedListBox_Deleted(sender As Object, e As RadListBoxEventArgs) Handles SelectedListBox.Deleted
        Dim ManagerId As Integer = 0
        Dim UserId As Integer = Session("AccountEmployeeId")
        For Each item As RadListBoxItem In e.Items
            Dim EmployeeId As Integer = item.Value
            oEmployee.UpdateEmployeeManager(EmployeeId, ManagerId, UserId)
        Next
    End Sub

    Protected Sub SelectedListBox_Inserted(sender As Object, e As RadListBoxEventArgs) Handles SelectedListBox.Inserted
        Dim ManagerId As Integer = Session("EmployeeId1")
        Dim UserId As Integer = Session("AccountEmployeeId")
        For Each item As RadListBoxItem In e.Items
            Dim EmployeeId As Integer = item.Value
            oEmployee.UpdateEmployeeManager(EmployeeId, ManagerId, UserId)
        Next
    End Sub

    



End Class
