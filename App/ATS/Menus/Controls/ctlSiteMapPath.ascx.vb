
Partial Class Menus_Controls_ctlSiteMapPath
    Inherits System.Web.UI.UserControl
    Protected Sub SiteMapPath1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SiteMapNodeItemEventArgs) Handles SP.ItemCreated
        If TypeOf e.Item.Controls.Item(0) Is HyperLink Then
            CType(e.Item.Controls.Item(0), HyperLink).Text = CType(e.Item.Controls.Item(0), HyperLink).Text
        ElseIf TypeOf e.Item.Controls.Item(0) Is Literal Then
            CType(e.Item.Controls.Item(0), Literal).Text = CType(e.Item.Controls.Item(0), Literal).Text
        End If
    End Sub
End Class
