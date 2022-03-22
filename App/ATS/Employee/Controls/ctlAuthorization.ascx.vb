Imports Telerik.Web.UI
  
Partial Class AccountAdmin_Controls_ctlAccountWorkTypeList
Inherits System.Web.UI.UserControl
    Dim moEmployee As New ATS.BO.Framework.BOEmployee

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            Dim Authurizor As Integer = CInt(Session("AccountEmployeeId"))

            SelectedListBox.DataSource = moEmployee.GetAuthorizedEmployeeById(Authurizor)
            SelectedListBox.DataBind()


        End If

    End Sub


    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Try

            AvailableListBox.DataSource = moEmployee.GetDepartmentEmployeesDataset(ctlDepartmentTree1.SelectedValue).Tables(0)
            AvailableListBox.DataBind()
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try

    End Sub


    Protected Sub SelectedListBox_Deleted(sender As Object, e As RadListBoxEventArgs) Handles SelectedListBox.Deleted

        Dim Authurizor As Integer = Session("AccountEmployeeId")
        For Each item As RadListBoxItem In e.Items
            Dim Authorized As Integer = item.Value
            moEmployee.UnAuthorizeEmployee(Authurizor, Authorized)
        Next
    End Sub

    Protected Sub SelectedListBox_Inserted(sender As Object, e As RadListBoxEventArgs) Handles SelectedListBox.Inserted

        Dim Authurizor As Integer = Session("AccountEmployeeId")
        For Each item As RadListBoxItem In e.Items
            Dim Authorized As Integer = item.Value
            moEmployee.AuthorizeEmployee(Authurizor, Authorized)
        Next
    End Sub

    

    
End Class
