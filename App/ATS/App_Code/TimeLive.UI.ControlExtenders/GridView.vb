Imports Microsoft.VisualBasic
Namespace ControlExtenders
    Partial Public Class GridView : Inherits System.Web.UI.WebControls.GridView

        ' METHOD:: OnLoad
        Protected Overrides Sub OnLoad(ByVal e As EventArgs)
            MyBase.OnLoad(e)
            Me.SetPageSize()
        End Sub
        Protected Overloads Overrides Sub InitializeRow(ByVal row As GridViewRow, ByVal fields As DataControlField())
            MyBase.InitializeRow(row, fields)
            If row.RowType = DataControlRowType.Header Then
                InitializeHeaderRow(row, fields)
            End If
        End Sub
        Protected Overridable Sub InitializeHeaderRow(ByVal row As GridViewRow, ByVal fields As DataControlField())
            ' AddGlyph(Me, row)
            AddFilters(row, fields)
        End Sub
        Protected Overridable Sub AddFilters(ByVal headerRow As GridViewRow, ByVal fields As DataControlField())
            For i As Integer = 0 To Columns.Count - 1
                If Not Columns(i).SortExpression = [String].Empty Then
                    AddFilter(i, headerRow.Cells(i), fields(i))
                End If
            Next
        End Sub
        Dim m_txtFilter As New Hashtable
        Dim m_ddlFilter As New Hashtable

        Protected Overridable Sub AddFilter(ByVal columnIndex As Integer, ByVal headerCell As TableCell, ByVal field As DataControlField)
            If headerCell.Controls.Count = 0 Then
                Dim ltlHeaderText As New LiteralControl(headerCell.Text)
                headerCell.Controls.Add(ltlHeaderText)
            End If
            Dim ltlBreak As New LiteralControl("</br>")
            headerCell.Controls.Add(ltlBreak)
            Dim txtFilter As TextBox = Nothing
            If m_txtFilter.Contains(columnIndex) Then
                txtFilter = DirectCast(m_txtFilter(columnIndex), TextBox)
            Else
                txtFilter = New TextBox()
                txtFilter.ID = (ID & "_txtFilter") + columnIndex.ToString()
                'txtFilter.Style.Add(HtmlTextWriterStyle.Width, Convert.ToString(field.ItemStyle.Width))
                If field.ItemStyle.Width.Value <> 0.0R Then
                    'txtFilter.Style.Add(HtmlTextWriterStyle.Width, Convert.ToString(100))
                ElseIf field.HeaderStyle.Width.Value <> 0.0R Then
                    'txtFilter.Style.Add(HtmlTextWriterStyle.Width, Convert.ToString(field.HeaderStyle.Width.Value))
                Else
                End If
                'txtFilter.Attributes.Add("Width", "8%")
                txtFilter.Style.Add(HtmlTextWriterStyle.Width, Convert.ToString(75) & "%")
                'txtFilter.Style.Add(HtmlTextWriterStyle.TextAlign, "Center")

                m_txtFilter(columnIndex) = txtFilter
                AddHandler txtFilter.TextChanged, AddressOf Me.HandleFilterCommand
                Dim js As String
                js = "javascript:" & Page.GetPostBackEventReference(Me, "@@@@@buttonPostBack") & ";"
                txtFilter.Attributes.Add("onchange", js)

            End If

            headerCell.Controls.Add(txtFilter)


        End Sub
        Private Sub HandleFilterCommand(ByVal sender As Object, ByVal e As EventArgs)
            'this is required to make sure that unsetting of filter is also handled 
            Me.RequiresDataBinding = True
            ' Dim filterArgs As New FilterCommandEventArgs()
            'filterArgs.FilterExpression = GetFilterCommand()
            Dim filterString = GetFilterCommand()
            'Me.OnFilterCommand(filterArgs)
        End Sub
        Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
            Dim filterCommand As String = GetFilterCommand()
            If [String].IsNullOrEmpty(filterCommand) = False Then
                ApplyFilterCommand(filterCommand)                
            End If
            Try
                MyBase.OnPreRender(e)
            Catch ex As Exception
                Exit Sub
            End Try
        End Sub
        Public Function IsBoundColumn(ByVal objColumn As DataControlField) As Boolean
            If Not objColumn.SortExpression = String.Empty Then
                Return True
            End If
        End Function
        Protected Overridable Function GetFilterCommand() As String
            ' Return ""
            Dim filterCommand As String = ""
            Dim i As Integer = 0
            While i < Me.Columns.Count
                If IsBoundColumn(Me.Columns(i)) Then
                    If Me.HeaderRow Is Nothing Then
                        i = i + 1

                        Continue While
                    End If
                    Dim headerCell As DataControlFieldHeaderCell = DirectCast(Me.HeaderRow.Cells(i), DataControlFieldHeaderCell)
                    Dim txtFilter As TextBox = DirectCast(m_txtFilter(i), TextBox)

                    Dim aColumn As DataControlField
                    'If Not (TypeOf Me.Columns(i) Is BoundField) Then
                    '    i = i + 1
                    '    Continue While
                    'End If
                    aColumn = DirectCast(Me.Columns(i), DataControlField)
                    If [String].IsNullOrEmpty(txtFilter.Text) Then
                        i = i + 1

                        Continue While
                    End If
                    Dim TextValue As String = txtFilter.Text
                    TextValue = TextValue.Replace("'", "''")

                    If [String].IsNullOrEmpty(filterCommand) Then
                        '                       Convert(TimespanColumn, 'System.String'
                        filterCommand = "Convert(" & aColumn.SortExpression + "," + Chr(39) + "System.String" + Chr(39) + ") like " + Chr(39) + "%" + TextValue + "%" + Chr(39)
                    Else
                        filterCommand += " AND " + "Convert(" & aColumn.SortExpression + "," + Chr(39) + "System.String" + Chr(39) + ") like " + Chr(39) + "%" + TextValue + "%" + Chr(39)
                    End If
                End If
                System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
            End While
            Return filterCommand
        End Function
        Protected Overridable Sub ApplyFilterCommand(ByVal filterCommand As String)
            Try
            Dim dsv As DataSourceView = Me.GetData()
            Dim objDataSourceView As ObjectDataSourceView = dsv
            objDataSourceView.FilterExpression = filterCommand
            Me.DataBind()

            If Me.Rows.Count = 0 Then
                objDataSourceView.FilterExpression = ""
                Me.ShowNotFoundMessage()
                Me.DataBind()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Sub ShowNotFoundMessage()
            Dim strMessage As String = "áÇ íæÌÏ ÓÌáÇÊ áÚÑÖåÇ æÝÞ ÈíÇäÇÊ ÇáÝÑÒ ÇáÊí Êã ÅÏÎÇáåÇ."
            Dim strScript As String = "alert('" & strMessage & "'); "
            If (Not Me.Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "clientScript", strScript, True)
            End If
        End Sub
        Public Sub SetPageSize()
            'Skip hardcoded page size for role permission
            If Me.PageSize <> 500 And Me.PageSize <> 25 And Me.PageSize <> 21 And Me.PageSize <> 70 Then
                Me.PageSize = DBUtilities.GetPageSize
            End If
        End Sub

    End Class

End Namespace