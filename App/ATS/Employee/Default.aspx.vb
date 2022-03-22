
Partial Class Employee_Default
    Inherits System.Web.UI.Page
    Dim FullSizeTableRow As TableRow
    Dim FullSizeTableCell As TableCell
    Dim DoubleTableRow As TableRow
    Dim DoubleTableCell As TableCell
    Public DoubleRows As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect(UIUtilities.RedirectToHomePage, False)

    End Sub
    Public Sub AddRowForFullTable()
        FullSizeTableRow = New TableRow
        FullSizeTable.Rows.Add(FullSizeTableRow)
    End Sub
    Public Sub AddCellForFullTable()
        FullSizeTableCell = New TableCell
        FullSizeTableCell.Attributes.Add("Width", "100%")
    End Sub
    Public Sub AddFullSizeTableCellIntoRow()
        FullSizeTableRow.Cells.Add(FullSizeTableCell)
    End Sub
    Public Sub AddRowForDoubleTable()
        DoubleTableRow = New TableRow
        DoubleTable.Rows.Add(DoubleTableRow)
    End Sub
    Public Sub AddCellForDoubleTable()
        DoubleTableCell = New TableCell
        DoubleTableCell.Attributes.Add("Width", "50%")
        DoubleTableCell.VerticalAlign = VerticalAlign.Top
    End Sub
    Public Sub AddDoubleTableCellIntoRow()
        DoubleTableRow.Cells.Add(DoubleTableCell)
    End Sub
    Public Sub AddLiteral()
        Dim objLiterControl As New LiteralControl
        objLiterControl.Text = "<br />"
        FullSizeTableCell.Controls.Add(objLiterControl)
    End Sub
    Public Sub AddLiteralForDoubleTable()
        Dim objLiterControl As New LiteralControl
        objLiterControl.Text = "<br />"
        DoubleTableCell.Controls.Add(objLiterControl)
    End Sub
    
End Class