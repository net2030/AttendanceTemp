Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlApprovalPolicy
    Inherits System.Web.UI.UserControl

#Region "General Declarations"
    Dim ApprovalPolicyId As Integer
    Dim moApprovalPolicy As BOApprovalPolicy = New BOApprovalPolicy
    Dim moApprovalPath As BOApprovalPath = New BOApprovalPath
    Dim eApprovalPolicy As New BOEApprovalPolicy
    Dim moVersionNo As Byte()
#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            'ATS.BO.BOPagePermission.SetPagePermission(13, Me.GridView2, Me.FormView1, "Button1", 4, 5)


        End If

    End Sub
#End Region

#Region "Data Binding"

    Private Sub GridViewDataBinding()
        'If Me.FormView1.CurrentMode = FormViewMode.Edit Then
        Dim id As Integer = DirectCast(FormView1.FindControl("txtApprovalPolicyId"), TextBox).Text
        GridView1.DataSource = moApprovalPath.GetApprovalPathsByPolicyId(id)
        GridView1.DataBind()
        '   End If
    End Sub



    Private Function GetApprovalPolicyEmployeesDataSource(ByVal ApprovalPolicyId As Integer) As DataView
        Dim dv As DataView = Nothing

        Dim ds As System.Data.DataSet
        Try
            Dim oApprovalPolicyEmployee As New BOApprovalPolicy
            ' ds = oApprovalPolicyEmployee.GetApprovalPolicyEmployeesDataset(ApprovalPolicyId, 1, 100000)
            dv = ds.Tables(0).DefaultView
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try

        Return dv
    End Function


    Private Function GetEmployeesDataSource(ByVal DepartmentId As Integer) As DataView
        Dim dv As DataView = Nothing

        Dim ds As System.Data.DataSet
        Try
            Dim oEmployee As New BOEmployee
            ds = oEmployee.GetDepartmentEmployeesDataset(DepartmentId, 1, 100000)
            dv = ds.Tables(0).DefaultView
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try

        Return dv
    End Function
#End Region

#Region "GridView "

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Select" Then
            Me.GridView2.Visible = False
            Me.btnAddApprovalPolicy.Visible = False
        ElseIf (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("ApprovalPolicyId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView2.DataSource = Nothing
            GridView2.DataBind()
        End If
    End Sub

    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing
        Dim x As Integer = e.NewEditIndex

        Dim row As GridViewRow = GridView2.Rows(x)
        Dim ApprovalPolicyId As String = row.Cells(0).Text
        Dim ApprovalPolicysList As New List(Of BOEApprovalPolicy)()
        eApprovalPolicy = moApprovalPolicy.Find(ApprovalPolicyId)
        ApprovalPolicysList.Add(eApprovalPolicy)
        FormView1.DataSource = ApprovalPolicysList
        Me.FormView1.ChangeMode(FormViewMode.Edit)
        Me.FormView1.DataBind()
        Me.GridView2.Visible = False
        FormView1.Visible = True
        GridView1.Visible = True
        GridViewDataBinding()
        btnAddApprovalPolicy.Visible = False

    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        GridView1.EditIndex = -1
        GridViewDataBinding()
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName.Equals("AddNew") Then
            ' This will get you the footer
            Dim eApprovalPath As BOEApprovalPath = New BOEApprovalPath

            eApprovalPath.ApprovalPolicyId = DirectCast(FormView1.FindControl("txtApprovalPolicyId"), TextBox).Text


            eApprovalPath.ApproverTypeId = DirectCast(GridView1.FooterRow.FindControl("ddlApproverType"), DropDownList).SelectedValue
            eApprovalPath.EmployeeId = 0 ' DirectCast(GridView1.FooterRow.FindControl("ddlEarnPeriod"), DropDownList).SelectedValue
            eApprovalPath.ApprovalPathSequence = GridView1.Rows.Count + 1 ' DirectCast(GridView1.FooterRow.FindControl("ddlResetToZeroPeriod"), DropDownList).SelectedValue

            If Not moApprovalPath.Add(eApprovalPath.ApprovalPolicyId,
                                            eApprovalPath.ApproverTypeId,
                                            eApprovalPath.EmployeeId,
                                            eApprovalPath.ApprovalPathSequence,
                                            Session("AccountEmployeeId")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moApprovalPath.InfoMessage), True)
            End If


            GridView1.EditIndex = -1
            GridViewDataBinding()
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound



        Dim moRole As New BORole1()

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ddlApproverType As DropDownList = DirectCast(e.Row.FindControl("ddlApproverType"), DropDownList)

            If Not IsNothing(ddlApproverType) Then
                ddlApproverType.DataSource = moRole.GetApprovalRoles()
                ddlApproverType.DataBind()
                '  ddlApproverType.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(1).ToString()
            End If

        End If

        If e.Row.RowType = DataControlRowType.EmptyDataRow Then

            Dim ddlApproverType As DropDownList = DirectCast(e.Row.FindControl("ddlApproverType"), DropDownList)


            If Not IsNothing(ddlApproverType) Then
                ddlApproverType.DataSource = moRole.GetApprovalRoles()
                ddlApproverType.DataBind()
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            Dim ddlApproverType As DropDownList = DirectCast(e.Row.FindControl("ddlApproverType"), DropDownList)

            ddlApproverType.DataSource = moRole.GetApprovalRoles()
            ddlApproverType.DataBind()

        End If


    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        GridView1.EditIndex = e.NewEditIndex
        GridViewDataBinding()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating

        Dim eApprovalPath As BOEApprovalPath = New BOEApprovalPath

        eApprovalPath.ApprovalPathId = GridView1.DataKeys(e.RowIndex)("ApprovalPathId")
        eApprovalPath.ApprovalPolicyId = DirectCast(FormView1.FindControl("txtApprovalPolicyId"), TextBox).Text
        eApprovalPath.ApproverTypeId = DirectCast(GridView1.Rows(e.RowIndex).FindControl("ddlApproverType"), DropDownList).SelectedValue
        eApprovalPath.EmployeeId = 0 '
        eApprovalPath.ApprovalPathSequence = e.RowIndex + 1


        eApprovalPath.VersionNo = GridView1.DataKeys(e.RowIndex)("VersionNo")

        If Not moApprovalPath.Update(eApprovalPath.ApprovalPathId,
                                            eApprovalPath.ApprovalPolicyId,
                                            eApprovalPath.ApproverTypeId,
                                            eApprovalPath.EmployeeId,
                                            eApprovalPath.ApprovalPathSequence,
                                 Session("AccountEmployeeId"),
                                 eApprovalPath.VersionNo) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moApprovalPath.InfoMessage), True)
        End If


        GridView1.EditIndex = -1
        GridViewDataBinding()
    End Sub

#End Region

#Region "Form Events"

    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles FormView1.DataBound
        Try

      
        If Me.FormView1.CurrentMode = FormViewMode.Edit Then
            If Not IsNothing(eApprovalPolicy.ApprovalPolicyId) Then
                System.Web.HttpContext.Current.Session.Add("ApprovalPolicyId", eApprovalPolicy.ApprovalPolicyId)
                Me.ctlEmployeesApprovalPolicy.ListBoxDataBinding(eApprovalPolicy.ApprovalPolicyId)
            End If
            If Not IsNothing(eApprovalPolicy.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eApprovalPolicy.VersionNo)
            End If
        End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Other Controls Events handling"

    Protected Sub btnAddApprovalPolicy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddApprovalPolicy.Click
        Me.FormView1.Visible = True
        Me.FormView1.DefaultMode = FormViewMode.Insert
        GridView1.Visible = True
        Me.GridView2.Visible = False
        btnAddApprovalPolicy.Visible = False
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Dim gvr As GridViewRow = TryCast(GridView1.Controls(0).Controls(0), GridViewRow)

        If gvr Is Nothing Then
            Return
        End If
        ' Retrieve controls

        Dim eApprovalPath As BOEApprovalPath = New BOEApprovalPath

        eApprovalPath.ApprovalPolicyId = DirectCast(FormView1.FindControl("txtApprovalPolicyId"), TextBox).Text

        eApprovalPath.ApproverTypeId = DirectCast(gvr.FindControl("ddlApproverType"), DropDownList).SelectedValue
        eApprovalPath.EmployeeId = 0 'DirectCast(gvr.FindControl("ddlEarnPeriod"), DropDownList).SelectedValue
        eApprovalPath.ApprovalPathSequence = 1




        If Not moApprovalPath.Add(eApprovalPath.ApprovalPolicyId,
                                          eApprovalPath.ApproverTypeId,
                                          eApprovalPath.EmployeeId,
                                          eApprovalPath.ApprovalPathSequence,
                                          Session("AccountEmployeeId")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moApprovalPath.InfoMessage), True)
        End If
        GridViewDataBinding()
    End Sub
#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eApprovalPolicy = FillObject()

        moVersionNo = Session("VersionNo")

        With moApprovalPolicy
            If .Update(eApprovalPolicy.ApprovalPolicyId,
                       eApprovalPolicy.ApprovalPolicyName,
                       eApprovalPolicy.TimeOffApprovalType,
                       Session("AccountEmployeeId"),
                       moVersionNo) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEGatepass
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtGatepassId.Text)), BOEGatepass)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button1"), Button).Enabled = False
                CType(Me.FormView1.FindControl("Button2"), Button).Text = "ÑÌæÚ"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)

            Else
                boolSeccessed = False
                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Red
                lbl.Visible = True
                lbl.Text = .InfoMessage
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataAdd() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eApprovalPolicy = FillObject()



        With moApprovalPolicy
            If .Add(eApprovalPolicy.ApprovalPolicyName,
                       eApprovalPolicy.TimeOffApprovalType,
                       Session("AccountEmployeeId")) Then

                boolSeccessed = True


                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green

                lbl.Visible = True
                CType(Me.FormView1.FindControl("txtApprovalPolicyId"), TextBox).Text = .Identity
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button1"), Button).Enabled = False
                CType(Me.FormView1.FindControl("Button2"), Button).Text = "ÑÌæÚ"
                GridView1.Visible = True
                GridView1.DataBind()

            Else
                boolSeccessed = False
                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Red
                lbl.Visible = True
                lbl.Text = .InfoMessage
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataDelete(ByVal id As Integer) As String
        moApprovalPolicy.Delete(id)
        Return moApprovalPolicy.InfoMessage
    End Function

    Private Sub AddEmployee(ByVal ApprovalPolicyId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOApprovalPolicy
        With oEmployee
            If Not .AddEmployee(ApprovalPolicyId, EmployeeId) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With
    End Sub

    Private Sub DeleteEmployee(ByVal ApprovalPolicyId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOApprovalPolicy
        With oEmployee
            If Not .DeleteEmployee(ApprovalPolicyId, EmployeeId) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With
    End Sub
#End Region

#Region "common methods"
    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function

    Private Function FillObject() As BOEApprovalPolicy


        Try
            With eApprovalPolicy
                If DirectCast(FormView1.FindControl("txtApprovalPolicyId"), TextBox).Text.Trim <> "" Then
                    .ApprovalPolicyId = DirectCast(FormView1.FindControl("txtApprovalPolicyId"), TextBox).Text
                End If
                .ApprovalPolicyName = DirectCast(FormView1.FindControl("txtApprovalPolicyName"), TextBox).Text
                .TimeOffApprovalType = DirectCast(FormView1.FindControl("ddlTimeOffApprovalType"), DropDownList).SelectedValue
                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eApprovalPolicy
    End Function

#End Region

    
End Class
