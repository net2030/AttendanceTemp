
Partial Class AccountAdmin_Controls_ctlAccountPagePermissionList
    Inherits System.Web.UI.UserControl



    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim row As GridViewRow
        Dim objAccountPagePermission As New ATS.BO.BOPagePermission



        Try
            For Each row In Me.GridView1.Rows
                If CType(row.FindControl("chkListView"), CheckBox).Checked = True Then
                    objAccountPagePermission.SetAccountPagePermission(1, ddlAccountRoleId.SelectedValue, Me.GridView1.DataKeys(row.RowIndex).Item(0), 1, False, Session("AccountEmployeeId"))
                Else
                    If CType(row.FindControl("chkListView"), CheckBox).Checked = False Then
                        objAccountPagePermission.SetAccountPagePermission(1, ddlAccountRoleId.SelectedValue, Me.GridView1.DataKeys(row.RowIndex).Item(0), 1, True, Session("AccountEmployeeId"))
                    End If
                End If


                If CType(row.FindControl("chkAdd"), CheckBox).Checked = True Then
                    objAccountPagePermission.SetAccountPagePermission(1, ddlAccountRoleId.SelectedValue, Me.GridView1.DataKeys(row.RowIndex).Item(0), 2, False, Session("AccountEmployeeId"))
                Else
                    If CType(row.FindControl("chkAdd"), CheckBox).Checked = False Then
                        objAccountPagePermission.SetAccountPagePermission(1, ddlAccountRoleId.SelectedValue, Me.GridView1.DataKeys(row.RowIndex).Item(0), 2, True, Session("AccountEmployeeId"))
                    End If
                End If


                If CType(row.FindControl("chkEdit"), CheckBox).Checked = True Then
                    objAccountPagePermission.SetAccountPagePermission(1, ddlAccountRoleId.SelectedValue, Me.GridView1.DataKeys(row.RowIndex).Item(0), 3, False, Session("AccountEmployeeId"))
                Else
                    If CType(row.FindControl("chkEdit"), CheckBox).Checked = False Then
                        objAccountPagePermission.SetAccountPagePermission(1, ddlAccountRoleId.SelectedValue, Me.GridView1.DataKeys(row.RowIndex).Item(0), 3, True, Session("AccountEmployeeId"))
                    End If
                End If

                If CType(row.FindControl("chkDelete"), CheckBox).Checked = True Then
                    objAccountPagePermission.SetAccountPagePermission(1, ddlAccountRoleId.SelectedValue, Me.GridView1.DataKeys(row.RowIndex).Item(0), 4, False, Session("AccountEmployeeId"))
                Else
                    If CType(row.FindControl("chkDelete"), CheckBox).Checked = False Then
                        objAccountPagePermission.SetAccountPagePermission(1, ddlAccountRoleId.SelectedValue, Me.GridView1.DataKeys(row.RowIndex).Item(0), 4, True, Session("AccountEmployeeId"))
                        'CType(row.FindControl("chkDeleteShowAllData"), CheckBox).Checked, CType(row.FindControl("chkDeleteShowMyData"), CheckBox).Checked, CType(row.FindControl("chkDeleteShowMyProjectsData"), CheckBox).Checked, CType(row.FindControl("chkDeleteShowMyTeamsData"), CheckBox).Checked
                    End If
                End If

            Next
 

        Catch ex As Exception
            Throw ex

        End Try
        Response.Redirect("../AccountAdmin/AdminMain.aspx")
        ' UIUtilities.RedirectToAdminHomePage()

    End Sub

    Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
        ATS.BO.BOPagePermission.SetPagePermissionForGridViewAndButton(85, Me.GridView1, Me.btnUpdate)
        Me.SetDefaultPage()
        Dim row As GridViewRow
        Dim bll As New ATS.BO.BOPagePermission

        For Each row In Me.GridView1.Rows
            Dim dt As DataTable = bll.GetDataForAccountPagePermission(ddlAccountRoleId.SelectedValue, Me.GridView1.DataKeys(row.RowIndex).Item(0))
            Me.HideInGrid(row)

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                For Each dr In dt.Rows


                    If Me.ddlAccountRoleId.SelectedValue = dr("AccountRoleId") And Me.GridView1.DataKeys(row.RowIndex).Item(0) = dr("SystemSecurityCategoryPageId") And dr("SystemPermissionId") = 1 Then
                        CType(row.FindControl("chkListView"), CheckBox).Checked = True
                        Me.ShowDataCheckBoxes("chkListViewShowAllData", "ShowAllData", row, dr)

                    End If

                    If Me.ddlAccountRoleId.SelectedValue = dr("AccountRoleId") And Me.GridView1.DataKeys(row.RowIndex).Item(0) = dr("SystemSecurityCategoryPageId") And dr("SystemPermissionId") = 2 Then
                        CType(row.FindControl("chkAdd"), CheckBox).Checked = True

                    End If

                    If Me.ddlAccountRoleId.SelectedValue = dr("AccountRoleId") And Me.GridView1.DataKeys(row.RowIndex).Item(0) = dr("SystemSecurityCategoryPageId") And dr("SystemPermissionId") = 3 Then
                        CType(row.FindControl("chkEdit"), CheckBox).Checked = True

                    End If

                    If Me.ddlAccountRoleId.SelectedValue = dr("AccountRoleId") And Me.GridView1.DataKeys(row.RowIndex).Item(0) = dr("SystemSecurityCategoryPageId") And dr("SystemPermissionId") = 4 Then
                        CType(row.FindControl("chkDelete"), CheckBox).Checked = True

                    End If

                Next
            End If
        Next
    End Sub

    Protected Sub ddlAccountRoleId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.GridView1.DataBind()
        Dim dr As DataRow = ATS.BO.BOPagePermission.GetDefaultPage()
        ddlDefaultAccountPageId.SelectedValue = dr("DefaultAccountPageId")
    End Sub
    Public Function HideInGrid(ByVal row As GridViewRow)
      

        If Me.GridView1.DataKeys(row.RowIndex).Item(1).ToString <> "True" Then
            CType(row.FindControl("chkListView"), CheckBox).Visible = False
            CType(row.FindControl("chkListViewShowAllData"), CheckBox).Visible = False
           
        End If

        If Me.GridView1.DataKeys(row.RowIndex).Item(2).ToString <> "True" Then
            CType(row.FindControl("chkAdd"), CheckBox).Visible = False
        End If

        If Me.GridView1.DataKeys(row.RowIndex).Item(3).ToString <> "True" Then
            CType(row.FindControl("chkEdit"), CheckBox).Visible = False
        End If

        If Me.GridView1.DataKeys(row.RowIndex).Item(4).ToString <> "True" Then
            CType(row.FindControl("chkDelete"), CheckBox).Visible = False
        End If

        If Me.GridView1.DataKeys(row.RowIndex).Item(6).ToString <> "True" Then
            CType(row.FindControl("chkListViewShowAllData"), CheckBox).Visible = False
        End If
    End Function
    Public Function ShowDataCheckBoxes(ByVal CheckBoxName As String, ByVal FieldName As String, ByVal row As GridViewRow, ByVal dr As DataRow)
        If IsDBNull(dr(FieldName)) Then
            CType(row.FindControl(CheckBoxName), CheckBox).Checked = False
        ElseIf dr(FieldName) = False Then
            CType(row.FindControl(CheckBoxName), CheckBox).Checked = False
        Else
            CType(row.FindControl(CheckBoxName), CheckBox).Checked = dr(FieldName)
        End If
    End Function
    Public Sub SetDefaultPage()
        If Not Me.IsPostBack Then
            Me.SetDataForDefaultPageDropDown(ddlDefaultAccountPageId)
        End If

    End Sub
    Public Sub SetDataForDefaultPageDropDown(ByVal DropDownName As DropDownList)

        'Dim objDataView As New DataView()
        'Dim BLL As New AccountPagePermissionBLL
        'Dim dt As TimeLiveDataSet.SystemSecurityCategoryPageDataTable

        'dt = BLL.GetSystemSecurityCategoryPagesByIsSiteMapPageForDefaultPage(True)

        'If dt.Rows.Count > 0 Then
        '    objDataView = dt.DefaultView
        '    objDataView.Sort = "SystemCategoryPageDescription"
        '    DropDownName.DataSource = objDataView
        '    DropDownName.DataBind()
        'End If

    End Sub
    
    
    
    
    
End Class
