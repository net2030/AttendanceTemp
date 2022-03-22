Imports Telerik.Web.UI

Partial Class ctlDepartmentTree
    Inherits System.Web.UI.UserControl
    Public objComb As RadComboBox
    Public objTree As RadTreeView
    Public node As RadTreeNode
    Public x As Integer



    Public Event SelectedNodeChanged()

    Public Property Enabled() As Boolean
        Get
            Return Me.RadComboBox1.Enabled
        End Get
        Set(value As Boolean)
            Me.RadComboBox1.Enabled = value
        End Set
    End Property

    Public Property IsRequired() As Boolean
        Get
            Return Me.RequiredFieldValidator4.Enabled
        End Get
        Set(value As Boolean)
            Me.RequiredFieldValidator4.Enabled = value
        End Set
    End Property

    Public Property Width() As Unit
        Get
            Return Me.RadComboBox1.Width
        End Get
        Set(value As Unit)
            Me.RadComboBox1.Width = value
        End Set
    End Property

    Public Property AutoPostBack() As Boolean
        Get
            Return CType(Me.RadComboBox1.Items(0).FindControl("RadTreeView1"), RadTreeView).SelectedValue
        End Get
        Set(value As Boolean)
            If value Then
                AddHandler (CType(RadComboBox1.Items(0).FindControl("RadTreeView1"), RadTreeView)).NodeClick, AddressOf RadTreeView1_NodeClick
            End If

        End Set
    End Property
    

    Public Property SelectedValue() As Integer
        Get
            Return CType(Me.RadComboBox1.Items(0).FindControl("RadTreeView1"), RadTreeView).SelectedValue
        End Get
        Set(value As Integer)
            CType(Me.RadComboBox1.Items(0).FindControl("RadTreeView1"), RadTreeView).UnselectAllNodes()
            Me.RadComboBox1.Text = "....إختر...."
            objTree = CType(RadComboBox1.Items(0).FindControl("RadTreeView1"), RadTreeView)
            node = objTree.FindNodeByValue(value)
            If Not IsNothing(node) Then
                node.Selected = True
                Me.RadComboBox1.Text = node.Text
            End If
        End Set
    End Property

    Public Property Text() As String
        Get
            Return Me.RadComboBox1.Text
        End Get
        Set(value As String)
            x = value

        End Set
    End Property



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'objComb = CType(Me.RadComboBox1, RadComboBox)

        'node = CType(CType(Me.RadComboBox1.Items(0).FindControl("RadTreeView1"), RadTreeView).FindNodeByValue(x), RadTreeNode)
        'If Not IsNothing(node) Then
        '    node.Selected = True
        '    Me.RadComboBox1.Text = node.Text
        'End If
    End Sub

    'Public Shared Sub clearSelection()
    '    CType(Me.RadComboBox1.Items(0).FindControl("RadTreeView1"), RadTreeView).UnselectAllNodes()
    'End Sub


    Protected Sub RadTreeView1_NodeClick(sender As Object, e As RadTreeNodeEventArgs)
        RaiseEvent SelectedNodeChanged()
    End Sub
End Class
