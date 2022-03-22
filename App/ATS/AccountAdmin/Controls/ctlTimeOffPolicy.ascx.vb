Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlTimeOffPolicy
    Inherits System.Web.UI.UserControl

#Region "General Declarations"
    Dim TimeOffPolicyId As Integer
    Dim moTimeOffPolicy As BOTimeOffPolicy = New BOTimeOffPolicy
    Dim moTimeOffPolicyDetail As BOTimeOffPolicyDetail = New BOTimeOffPolicyDetail
    Dim eTimeOffPolicy As New BOETimeOffPolicy
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
        Dim id As Integer = DirectCast(FormView1.FindControl("txtTimeOffPolicyId"), TextBox).Text
        GridView1.DataSource = moTimeOffPolicyDetail.GetTimeOffPolicyDetailsByPolicyId(id)
        GridView1.DataBind()
        '   End If
    End Sub

    

    Private Function GetTimeOffPolicyEmployeesDataSource(ByVal TimeOffPolicyId As Integer) As DataView
        Dim dv As DataView = Nothing

        Dim ds As System.Data.DataSet
        Try
            Dim oTimeOffPolicyEmployee As New BOTimeOffPolicy
            ' ds = oTimeOffPolicyEmployee.GetTimeOffPolicyEmployeesDataset(TimeOffPolicyId, 1, 100000)
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
            Me.btnAddTimeOffPolicy.Visible = False
        ElseIf (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("TimeOffPolicyId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView2.DataSource = Nothing
            GridView2.DataBind()
        End If
    End Sub

    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing
        Dim x As Integer = e.NewEditIndex

        Dim row As GridViewRow = GridView2.Rows(x)
        Dim TimeOffPolicyId As String = row.Cells(0).Text
        Dim TimeOffPolicysList As New List(Of BOETimeOffPolicy)()
        eTimeOffPolicy = moTimeOffPolicy.Find(TimeOffPolicyId)
        TimeOffPolicysList.Add(eTimeOffPolicy)
        FormView1.DataSource = TimeOffPolicysList
        Me.FormView1.ChangeMode(FormViewMode.Edit)
        Me.FormView1.DataBind()
        Me.GridView2.Visible = False
        FormView1.Visible = True
        GridView1.Visible = True
        GridViewDataBinding()
        btnAddTimeOffPolicy.Visible = False

    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        GridView1.EditIndex = -1
        GridViewDataBinding()
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName.Equals("AddNew") Then
            ' This will get you the footer
            Dim eTimeOffPolicyDetail As BOETimeOffPolicyDetail = New BOETimeOffPolicyDetail

            eTimeOffPolicyDetail.TimeOffPolicyId = DirectCast(FormView1.FindControl("txtTimeOffPolicyId"), TextBox).Text

            eTimeOffPolicyDetail.GatepassTypeId = DirectCast(GridView1.FooterRow.FindControl("ddlType"), DropDownList).SelectedValue
            eTimeOffPolicyDetail.EarnPeriodId = DirectCast(GridView1.FooterRow.FindControl("ddlEarnPeriod"), DropDownList).SelectedValue
            eTimeOffPolicyDetail.ResetToZeroPeriodId = DirectCast(GridView1.FooterRow.FindControl("ddlResetToZeroPeriod"), DropDownList).SelectedValue
            eTimeOffPolicyDetail.InitialSetToMinutes = DirectCast(GridView1.FooterRow.FindControl("txtInitialSetToMinutes"), RadNumericTextBox).Value
            eTimeOffPolicyDetail.ResetToMinutes = DirectCast(GridView1.FooterRow.FindControl("txtResetToMinutes"), RadNumericTextBox).Value
            eTimeOffPolicyDetail.EarnMinutes = DirectCast(GridView1.FooterRow.FindControl("txtEarnMinutes"), RadNumericTextBox).Value
            eTimeOffPolicyDetail.EffectiveDate = DirectCast(GridView1.FooterRow.FindControl("datEffectiveDate"), GeneralControls_MyDate).SelectedDate
            eTimeOffPolicyDetail.IsCarryForward = DirectCast(GridView1.FooterRow.FindControl("chkIsCarryForward"), CheckBox).Checked
            eTimeOffPolicyDetail.MinValue = DirectCast(GridView1.FooterRow.FindControl("txtMinValue"), RadNumericTextBox).Value
            eTimeOffPolicyDetail.MaxValue = DirectCast(GridView1.FooterRow.FindControl("txtMaxValue"), RadNumericTextBox).Value

            If Not moTimeOffPolicyDetail.Add(eTimeOffPolicyDetail.TimeOffPolicyId,
                                              eTimeOffPolicyDetail.GatepassTypeId,
                                              eTimeOffPolicyDetail.EarnPeriodId,
                                              eTimeOffPolicyDetail.ResetToZeroPeriodId,
                                              eTimeOffPolicyDetail.InitialSetToMinutes,
                                              eTimeOffPolicyDetail.ResetToMinutes,
                                              eTimeOffPolicyDetail.EarnMinutes,
                                              eTimeOffPolicyDetail.EffectiveDate,
                                              eTimeOffPolicyDetail.IsCarryForward,
                                              eTimeOffPolicyDetail.MinValue,
                                              eTimeOffPolicyDetail.MaxValue,
                                              Session("AccountEmployeeId")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moTimeOffPolicyDetail.InfoMessage), True)
            End If


            GridView1.EditIndex = -1
            GridViewDataBinding()
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound



        Dim moTimeOffPolicyDetail As New BOTimeOffPolicyDetail()

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlType As DropDownList = DirectCast(e.Row.FindControl("ddlType"), DropDownList)
            Dim ddlEarnPeriod As DropDownList = DirectCast(e.Row.FindControl("ddlEarnPeriod"), DropDownList)
            Dim ddlResetToZeroPeriod As DropDownList = DirectCast(e.Row.FindControl("ddlResetToZeroPeriod"), DropDownList)


            If Not IsNothing(ddlType) Then
                ddlType.DataSource = moTimeOffPolicyDetail.GetTypesByLeaveType(DirectCast(FormView1.FindControl("ddlType"), DropDownList).SelectedValue)
                ddlType.DataBind()
                ddlType.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(1).ToString()
            End If


            If Not IsNothing(ddlEarnPeriod) Then
                ddlEarnPeriod.DataSource = moTimeOffPolicyDetail.GetPeriodsDataSet()
                ddlEarnPeriod.DataBind()
                ddlEarnPeriod.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(2).ToString()
            End If

            If Not IsNothing(ddlResetToZeroPeriod) Then
                ddlResetToZeroPeriod.DataSource = moTimeOffPolicyDetail.GetPeriodsDataSet()
                ddlResetToZeroPeriod.DataBind()
                ddlResetToZeroPeriod.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(3).ToString()
            End If
        End If

        If e.Row.RowType = DataControlRowType.EmptyDataRow Then

            Dim ddlType As DropDownList = DirectCast(e.Row.FindControl("ddlType"), DropDownList)
            Dim ddlEarnPeriod As DropDownList = DirectCast(e.Row.FindControl("ddlEarnPeriod"), DropDownList)
            Dim ddlResetToZeroPeriod As DropDownList = DirectCast(e.Row.FindControl("ddlResetToZeroPeriod"), DropDownList)

            If Not IsNothing(ddlType) Then
                ddlType.DataSource = moTimeOffPolicyDetail.GetTypesByLeaveType(DirectCast(FormView1.FindControl("ddlType"), DropDownList).SelectedValue)
                ddlType.DataBind()
            End If

            If Not IsNothing(ddlEarnPeriod) Then
                ddlEarnPeriod.DataSource = moTimeOffPolicyDetail.GetPeriodsDataSet()
                ddlEarnPeriod.DataBind()
            End If

            If Not IsNothing(ddlResetToZeroPeriod) Then
                ddlResetToZeroPeriod.DataSource = moTimeOffPolicyDetail.GetPeriodsDataSet()
                ddlResetToZeroPeriod.DataBind()
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            Dim ddlType As DropDownList = DirectCast(e.Row.FindControl("ddlType"), DropDownList)
            Dim ddlEarnPeriod As DropDownList = DirectCast(e.Row.FindControl("ddlEarnPeriod"), DropDownList)
            Dim ddlResetToZeroPeriod As DropDownList = DirectCast(e.Row.FindControl("ddlResetToZeroPeriod"), DropDownList)


            ddlType.DataSource = moTimeOffPolicyDetail.GetTypesByLeaveType(DirectCast(FormView1.FindControl("ddlType"), DropDownList).SelectedValue)
            ddlType.DataBind()

            ddlEarnPeriod.DataSource = moTimeOffPolicyDetail.GetPeriodsDataSet()
            ddlEarnPeriod.DataBind()

            ddlResetToZeroPeriod.DataSource = moTimeOffPolicyDetail.GetPeriodsDataSet()
            ddlResetToZeroPeriod.DataBind()
        End If


    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        GridView1.EditIndex = e.NewEditIndex
        GridViewDataBinding()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating

        Dim eTimeOffPolicyDetail As BOETimeOffPolicyDetail = New BOETimeOffPolicyDetail

        eTimeOffPolicyDetail.TimeOffPolicyDetailId = GridView1.DataKeys(e.RowIndex)("TimeOffPolicyDetailId")
        eTimeOffPolicyDetail.TimeOffPolicyId = DirectCast(FormView1.FindControl("txtTimeOffPolicyId"), TextBox).Text

        eTimeOffPolicyDetail.GatepassTypeId = DirectCast(GridView1.Rows(e.RowIndex).FindControl("ddlType"), DropDownList).SelectedValue
        eTimeOffPolicyDetail.EarnPeriodId = DirectCast(GridView1.Rows(e.RowIndex).FindControl("ddlEarnPeriod"), DropDownList).SelectedValue
        eTimeOffPolicyDetail.ResetToZeroPeriodId = DirectCast(GridView1.Rows(e.RowIndex).FindControl("ddlResetToZeroPeriod"), DropDownList).SelectedValue
        eTimeOffPolicyDetail.InitialSetToMinutes = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtInitialSetToMinutes"), RadNumericTextBox).Value
        eTimeOffPolicyDetail.ResetToMinutes = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtResetToMinutes"), RadNumericTextBox).Value
        eTimeOffPolicyDetail.EarnMinutes = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtEarnMinutes"), RadNumericTextBox).Value
        eTimeOffPolicyDetail.EffectiveDate = DirectCast(GridView1.Rows(e.RowIndex).FindControl("datEffectiveDate"), GeneralControls_MyDate).SelectedDate
        eTimeOffPolicyDetail.IsCarryForward = DirectCast(GridView1.Rows(e.RowIndex).FindControl("chkIsCarryForward"), CheckBox).Checked
        eTimeOffPolicyDetail.MinValue = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtMinValue"), RadNumericTextBox).Value
        eTimeOffPolicyDetail.MaxValue = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtMaxValue"), RadNumericTextBox).Value
        eTimeOffPolicyDetail.VersionNo = GridView1.DataKeys(e.RowIndex)("VersionNo")

        If Not moTimeOffPolicyDetail.Update(eTimeOffPolicyDetail.TimeOffPolicyDetailId,
                                            eTimeOffPolicyDetail.TimeOffPolicyId,
                                            eTimeOffPolicyDetail.GatepassTypeId,
                                            eTimeOffPolicyDetail.EarnPeriodId,
                                            eTimeOffPolicyDetail.ResetToZeroPeriodId,
                                            eTimeOffPolicyDetail.InitialSetToMinutes,
                                            eTimeOffPolicyDetail.ResetToMinutes,
                                            eTimeOffPolicyDetail.EarnMinutes,
                                            eTimeOffPolicyDetail.EffectiveDate,
                                            eTimeOffPolicyDetail.IsCarryForward,
                                            eTimeOffPolicyDetail.MinValue,
                                            eTimeOffPolicyDetail.MaxValue,
                                 Session("AccountEmployeeId"),
                                 eTimeOffPolicyDetail.VersionNo) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moTimeOffPolicyDetail.InfoMessage), True)
        End If


        GridView1.EditIndex = -1
        GridViewDataBinding()
    End Sub

#End Region

#Region "Form Events"

    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles FormView1.DataBound
        If Me.FormView1.CurrentMode = FormViewMode.Edit Then
            If Not IsNothing(eTimeOffPolicy.TimeOffPolicyId) Then
                System.Web.HttpContext.Current.Session.Add("TimeOffPolicyId", eTimeOffPolicy.TimeOffPolicyId)
                Me.ctlEmployeesLeavePolicy.ListBoxDataBinding(eTimeOffPolicy.TimeOffPolicyId)
            End If
            If Not IsNothing(eTimeOffPolicy.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eTimeOffPolicy.VersionNo)
            End If
        End If

    End Sub

#End Region

#Region "Other Controls Events handling"

    Protected Sub btnAddTimeOffPolicy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddTimeOffPolicy.Click
        Me.FormView1.Visible = True
        Me.FormView1.DefaultMode = FormViewMode.Insert
        GridView1.Visible = True
        Me.GridView2.Visible = False
        btnAddTimeOffPolicy.Visible = False
    End Sub

#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eTimeOffPolicy = FillObject()

        moVersionNo = Session("VersionNo")

        With moTimeOffPolicy
            If .Update(eTimeOffPolicy.TimeOffPolicyId,
                       eTimeOffPolicy.TimeOffPolicyName,
                       eTimeOffPolicy.TypeId,
                       eTimeOffPolicy.EffectiveDate,
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
        eTimeOffPolicy = FillObject()



        With moTimeOffPolicy
            If .Add(eTimeOffPolicy.TimeOffPolicyName,
                       eTimeOffPolicy.TypeId,
                       eTimeOffPolicy.EffectiveDate,
                       Session("AccountEmployeeId")) Then

                boolSeccessed = True


                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green

                lbl.Visible = True
                CType(Me.FormView1.FindControl("txtTimeOffPolicyId"), TextBox).Text = .Identity
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button2"), Button).Text = "ÑÌæÚ"
                CType(Me.FormView1.FindControl("Button1"), Button).Enabled = False
                GridView1.Visible = True
                GridView1.DataBind()

                System.Web.HttpContext.Current.Session.Add("TimeOffPolicyId", .Identity)
                'Me.FormView1.ChangeMode(FormViewMode.Edit)

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
        moTimeOffPolicy.Delete(id)
        Return moTimeOffPolicy.InfoMessage
    End Function

    Private Sub AddEmployee(ByVal TimeOffPolicyId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOTimeOffPolicy
        With oEmployee
            If Not .AddEmployee(TimeOffPolicyId, EmployeeId) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With
    End Sub

    Private Sub DeleteEmployee(ByVal TimeOffPolicyId As Integer, ByVal EmployeeId As Integer)
        Dim oEmployee As New BOTimeOffPolicy
        With oEmployee
            If Not .DeleteEmployee(TimeOffPolicyId, EmployeeId) Then
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

    Private Function FillObject() As BOETimeOffPolicy


        Try
            With eTimeOffPolicy
                If DirectCast(FormView1.FindControl("txtTimeOffPolicyId"), TextBox).Text.Trim <> "" Then
                    .TimeOffPolicyId = DirectCast(FormView1.FindControl("txtTimeOffPolicyId"), TextBox).Text
                End If
                .TimeOffPolicyName = DirectCast(FormView1.FindControl("txtTimeOffPolicyName"), TextBox).Text
                .TypeId = DirectCast(FormView1.FindControl("ddlType"), DropDownList).SelectedValue

                .EffectiveDate = DirectCast(FormView1.FindControl("datEffectiveDate"), GeneralControls_MyDate).SelectedDate
                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eTimeOffPolicy
    End Function

#End Region

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Dim gvr As GridViewRow = TryCast(GridView1.Controls(0).Controls(0), GridViewRow)

        If gvr Is Nothing Then
            Return
        End If
        ' Retrieve controls

        Dim eTimeOffPolicyDetail As BOETimeOffPolicyDetail = New BOETimeOffPolicyDetail

        eTimeOffPolicyDetail.TimeOffPolicyId = DirectCast(FormView1.FindControl("txtTimeOffPolicyId"), TextBox).Text

        eTimeOffPolicyDetail.GatepassTypeId = DirectCast(gvr.FindControl("ddlType"), DropDownList).Text
        eTimeOffPolicyDetail.EarnPeriodId = DirectCast(gvr.FindControl("ddlEarnPeriod"), DropDownList).SelectedValue
        eTimeOffPolicyDetail.ResetToZeroPeriodId = DirectCast(gvr.FindControl("ddlResetToZeroPeriod"), DropDownList).SelectedValue
        eTimeOffPolicyDetail.InitialSetToMinutes = DirectCast(gvr.FindControl("txtInitialSetToMinutes"), RadNumericTextBox).Value
        eTimeOffPolicyDetail.ResetToMinutes = DirectCast(gvr.FindControl("txtResetToMinutes"), RadNumericTextBox).Value
        eTimeOffPolicyDetail.EarnMinutes = DirectCast(gvr.FindControl("txtEarnMinutes"), RadNumericTextBox).Value
        eTimeOffPolicyDetail.EffectiveDate = DirectCast(gvr.FindControl("datEffectiveDate"), GeneralControls_MyDate).SelectedDate
        eTimeOffPolicyDetail.IsCarryForward = DirectCast(gvr.FindControl("chkIsCarryForward"), CheckBox).Checked
        eTimeOffPolicyDetail.MinValue = DirectCast(gvr.FindControl("txtMinValue"), RadNumericTextBox).Value
        eTimeOffPolicyDetail.MaxValue = DirectCast(gvr.FindControl("txtMaxValue"), RadNumericTextBox).Value



        If Not moTimeOffPolicyDetail.Add(eTimeOffPolicyDetail.TimeOffPolicyId,
                                          eTimeOffPolicyDetail.GatepassTypeId,
                                          eTimeOffPolicyDetail.EarnPeriodId,
                                          eTimeOffPolicyDetail.ResetToZeroPeriodId,
                                          eTimeOffPolicyDetail.InitialSetToMinutes,
                                          eTimeOffPolicyDetail.ResetToMinutes,
                                          eTimeOffPolicyDetail.EarnMinutes,
                                          eTimeOffPolicyDetail.EffectiveDate,
                                          eTimeOffPolicyDetail.IsCarryForward,
                                          eTimeOffPolicyDetail.MinValue,
                                          eTimeOffPolicyDetail.MaxValue,
                                          Session("AccountEmployeeId")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moTimeOffPolicyDetail.InfoMessage), True)
        End If
        GridViewDataBinding()
    End Sub
End Class
