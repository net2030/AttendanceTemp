Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlDepartmentScope
    Inherits System.Web.UI.UserControl
    Dim moDepartment As BODepartment = New BODepartment
    Dim dt As DataTable = New DataTable
    Dim EmployeeId As Integer

    Protected Sub RadTreeView1_DataBound(sender As Object, e As EventArgs) Handles RadTreeView1.DataBound
        dt = moDepartment.GetScopes(Session("EmployeeId1")).Tables(0)
        If dt.Rows.Count = RadTreeView1.GetAllNodes().Count Then
            RadTreeView1.CheckAllNodes()
            RadTreeView1.ExpandAllNodes()
            Return
        End If
        For Each nod As RadTreeNode In RadTreeView1.GetAllNodes()
            For Each row As DataRow In dt.Rows
                If Not IsNothing(nod.ParentNode) AndAlso row("DepartmentId") = nod.Value Then

                    nod.ParentNode.Expanded = True
                    nod.CheckChildNodes()
                    nod.Expanded = True
                    nod.Checked = True
                    nod.CheckChildNodes()
                    GoTo end_of_for
                End If
end_of_for:
            Next

        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'RadTreeView1.DataSource = New BODepartment().GetEmployeeDepartmentsListForDropdownTree(0)
            'RadTreeView1.DataBind()
        End If
    End Sub



    

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim success As Boolean = True
        Dim Msg As String = ""

        If (moDepartment.DeleteScope(Session("EmployeeId1"), 1)) Then
            success = True
            Msg = moDepartment.InfoMessage
        End If

        Try

            For Each nod As RadTreeNode In RadTreeView1.GetAllNodes()
                If nod.Checked AndAlso IsNothing(nod.ParentNode) Then
                    If (moDepartment.AddScope(Session("EmployeeId1"), CInt(nod.Value))) Then
                        success = True

                    Else
                        success = False
                    End If
                    Msg = moDepartment.InfoMessage
                ElseIf nod.Checked AndAlso Not IsNothing(nod.ParentNode) AndAlso nod.ParentNode.Checked Then
                    Continue For
                ElseIf nod.Checked AndAlso Not IsNothing(nod.ParentNode) AndAlso Not nod.ParentNode.Checked Then
                    If (moDepartment.AddScope(Session("EmployeeId1"), CInt(nod.Value))) Then
                        success = True
                    Else
                        success = False
                    End If
                    Msg = moDepartment.InfoMessage
                End If
            Next

        Catch ex As Exception

        End Try

        If success Then
            Label2.ForeColor = Color.Green
            Label2.Visible = True
            Label2.Text = Msg
        Else
            Label2.ForeColor = Color.Red
            Label2.Text = Msg
        End If
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "test", "<script>unsaved=true;</script> ", True)
    End Sub

    Protected Sub RadTreeView1_NodeCheck(sender As Object, e As RadTreeNodeEventArgs) 'Handles RadTreeView1.NodeCheck
        If e.Node.Checked Then
            e.Node.CheckChildNodes()
        End If
        If Not e.Node.Checked Then
            e.Node.UncheckChildNodes()
        End If
    End Sub

    

End Class
