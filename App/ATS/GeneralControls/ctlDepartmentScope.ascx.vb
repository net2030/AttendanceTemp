Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlDepartmentScope
    Inherits System.Web.UI.UserControl
    Dim moDepartment As BODepartment = New BODepartment
    Dim dt As DataTable = New DataTable
    Dim EmployeeId As Integer

    Protected Sub RadTreeView1_DataBound(sender As Object, e As EventArgs) Handles RadTreeView1.DataBound

        dt = moDepartment.GetScopes(EmployeeId).Tables(0)
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
        EmployeeId = Session("EmployeeID1")
    End Sub



    

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        moDepartment.DeleteScope(EmployeeId, 1)

        For Each nod As RadTreeNode In RadTreeView1.GetAllNodes()
            If nod.Checked AndAlso IsNothing(nod.ParentNode) Then
                moDepartment.AddScope(EmployeeId, CInt(nod.Value))
            ElseIf nod.Checked AndAlso Not IsNothing(nod.ParentNode) AndAlso nod.ParentNode.Checked Then
                Continue For
            ElseIf nod.Checked AndAlso Not IsNothing(nod.ParentNode) AndAlso Not nod.ParentNode.Checked Then
                moDepartment.AddScope(EmployeeId, CInt(nod.Value))
            End If
        Next

end_of_for:

    End Sub


    

End Class
