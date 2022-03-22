Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports ATS.BO.Framework

Partial Class Attendance_Controls_ctlVacation
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moVacation As BOVacation = New BOVacation
    Dim eVacation As New BOEVacation
    Dim moEmployee As ATS.BO.Framework.BOEmployee
    Dim moVersionNo As Byte()
#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            Startdate.SelectedDate = DateAdd(DateInterval.Month, -1, Now.Date)
            Enddate.SelectedDate = Now.Date
            VacationFormView.Visible = False
            ATS.BO.BOPagePermission.SetPagePermission(163, Me.GridView2, Me.VacationFormView, "Button1", 9, 10)
        End If

    End Sub
#End Region

#Region "Data Binding"
    Private Sub GridViewBinding()

        Dim SDate, EDate As Date
        Dim id As Integer
        Try

            'Dim control As ContentPlaceHolder = DirectCast(Page.Master.Master.Master.FindControl("C").FindControl("C").FindControl("C"), ContentPlaceHolder)
            'Dim objTestControl As ctlDepartmentTree = DirectCast(control.FindControl("ctlVacation1").FindControl("ctlDepartmentTree1"), ctlDepartmentTree)
            'Dim objComb As RadComboBox = DirectCast(objTestControl.FindControl("RadComboBox1"), RadComboBox)
            'Dim objTree As RadTreeView = DirectCast(objComb.Items(0).FindControl("RadTreeView1"), RadTreeView)

            id = ctlDepartmentTree1.selectedvalue()
            SDate = Startdate.SelectedDate
            EDate = Enddate.SelectedDate

            Me.dsVacations.SelectParameters.Item("DepartmentID").DefaultValue = id
            Me.dsVacations.SelectParameters.Item("BegDate").DefaultValue = SDate
            Me.dsVacations.SelectParameters.Item("EndDate").DefaultValue = EDate

            GridView2.DataBind()

        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try

    End Sub
#End Region

#Region "Controls Events"

#Region "GridView Events"

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("VacationId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView2.DataSource = Nothing
            GridView2.DataBind()
        ElseIf (e.CommandName = "print") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim VacationId As Integer = GridView2.DataKeys(index)("VacationId").ToString()

            lblDate.Text = Now.Date
            lblNo.Text = VacationId
            lblEmployeeName.Text = GridView2.DataKeys(index)("EmployeeName").ToString()
            lblEmployeeNo.Text = GridView2.DataKeys(index)("EmployeeNo").ToString()

            lblType.Text = GridView2.DataKeys(index)("TypeName").ToString()
            lblDepartment.Text = GridView2.DataKeys(index)("DepartmentName").ToString()
            lblEffectivedate.Text = GridView2.DataKeys(index)("EffectiveDate").ToString()
            lblExpireDate.Text = GridView2.DataKeys(index)("DateExpire").ToString()
            lblAltEmp.Text = GridView2.DataKeys(index)("AltEmployeename").ToString()
            lblApproveStatus.Text = GridView2.DataKeys(index)("AprovalStatus").ToString()
            lblAddess.Text = GridView2.DataKeys(index)("Address").ToString()
            lblContact.Text = GridView2.DataKeys(index)("Contact").ToString()
            lblNote.Text = GridView2.DataKeys(index)("Note").ToString()
            lblAddedBy.Text = GridView2.DataKeys(index)("AddedBy").ToString()
            lblAddedDate.Text = GridView2.DataKeys(index)("AddedDate").ToString()



            grdViewApprovalLog.DataSource = moVacation.GetVacationApprovalLogByVacationId(VacationId)
            grdViewApprovalLog.DataBind()

            PrintDiv.Visible = True
            GridView2.Visible = False
            btnAddVacation.Visible = False
            btnPrint.Visible = True

            Try
                Dim GovId As String = GridView2.DataKeys(index)("GovId").ToString()
                id1.Text = GovId(0)
                id2.Text = GovId(1)
                id3.Text = GovId(2)
                id4.Text = GovId(3)
                id5.Text = GovId(4)
                id6.Text = GovId(5)
                id7.Text = GovId(6)
                id8.Text = GovId(7)
                id9.Text = GovId(8)
                id10.Text = GovId(9)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub gvEG_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing
        Try
            Dim x As Integer = e.NewEditIndex
            Dim row As GridViewRow = GridView2.Rows(x)
            Dim VacationId As String = row.Cells(0).Text
            Dim VacationsList As New List(Of BOEVacation)()
            eVacation = moVacation.Find(VacationId)
            VacationsList.Add(eVacation)
            VacationFormView.DataSource = VacationsList
            Me.VacationFormView.ChangeMode(FormViewMode.Edit)
            Me.VacationFormView.DataBind()
            Me.GridView2.Visible = False
            VacationFormView.Visible = True
            btnAddVacation.Visible = False

            dvSearch.Visible = False
        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try
    End Sub


#End Region

#Region "Form Events"

    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles VacationFormView.DataBound

        hdnAltEmployeeRequiredValidator.Value = DirectCast(VacationFormView.FindControl("AlternativeEmployeeRequiredFieldValidator1"), RequiredFieldValidator).ClientID
        hdnddlVacationtype.Value = DirectCast(VacationFormView.FindControl("ddlVacationTypeId"), DropDownList).ClientID

        System.Web.HttpContext.Current.Session.Add("StartDateClientId", DirectCast(VacationFormView.FindControl("datEffectiveDate"), GeneralControls_MyDate).TxtDate.ClientID)
        System.Web.HttpContext.Current.Session.Add("EndDateClientId", DirectCast(VacationFormView.FindControl("datDateExpire"), GeneralControls_MyDate).TxtDate.ClientID)
     

        If VacationFormView.CurrentMode = FormViewMode.Edit Then

            If Not IsNothing(eVacation.EmployeeId) Then
                CType(Me.VacationFormView.FindControl("ddlEmployees"), DropDownList).DataSource = New BOEmployee().GetEmployeesByIdDataset(Session("AccountEmployeeId"), eVacation.EmployeeId) 'EmployeeDAL.GetManagerById(eVacation.EmployeeId)
                CType(Me.VacationFormView.FindControl("ddlEmployees"), DropDownList).DataBind()
                CType(Me.VacationFormView.FindControl("ddlEmployees"), DropDownList).SelectedValue = eVacation.EmployeeId
                CType(VacationFormView.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(eVacation.EmployeeId, 1)

            End If

            If Not IsNothing(eVacation.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eVacation.VersionNo)
            End If

            If Not IsNothing(eVacation.AltEmployeeId) Then
                moEmployee = New BOEmployee
                Dim EmployeesList As New List(Of BOEEmployee)()
                Dim eEmployee1 As New BOEEmployee
                eEmployee1 = moEmployee.Find(eVacation.AltEmployeeId)
                EmployeesList.Add(eEmployee1)

                CType(Me.VacationFormView.FindControl("ddlAltEmployeeId"), DropDownList).DataSource = EmployeesList
                CType(Me.VacationFormView.FindControl("ddlAltEmployeeId"), DropDownList).DataBind()
                CType(Me.VacationFormView.FindControl("ddlAltEmployeeId"), DropDownList).SelectedValue = eVacation.AltEmployeeId
            End If
        End If

    End Sub

#End Region

#Region "Other controls Events"

    Protected Sub btnAddVacation_Click(sender As Object, e As EventArgs) Handles btnAddVacation.Click
        Try
            Me.VacationFormView.Visible = True
            Me.VacationFormView.DefaultMode = FormViewMode.Insert
            Me.GridView2.Visible = False
            Me.btnAddVacation.Visible = False
            dvSearch.Visible = False
        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try

    End Sub

    Protected Sub ddlDepartments_SelectedIndexChanged()

        If Me.VacationFormView.CurrentMode = FormViewMode.Edit Then
            Return
        End If
        Try

            Dim ddlEmployees As DropDownList = CType(VacationFormView.Row.FindControl("ddlEmployees"), DropDownList)
            Dim DepartmentId As Integer
            If Not ddlEmployees Is Nothing Then
                DepartmentId = CType(VacationFormView.Row.FindControl("ddlDepartments"), ctlDepartmentTree).SelectedValue
                moEmployee = New ATS.BO.Framework.BOEmployee
                ddlEmployees.DataSource = moEmployee.GetDepartmentEmployeesDataset(DepartmentId, 1, 100).Tables(0)
                ddlEmployees.DataValueField = "EmployeeId"
                ddlEmployees.DataTextField = "EmployeeName"
                ddlEmployees.DataBind()
            End If
        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try
    End Sub

    Protected Sub btnView()
        GridViewBinding()
    End Sub

    Protected Sub ddlEmployees_SelectedIndexChanged(sender As Object, e As EventArgs)
        CType(VacationFormView.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(CType(VacationFormView.FindControl("ddlEmployees"), DropDownList).SelectedValue, 1)
    End Sub

#End Region
#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eVacation = FillObject()

        moVersionNo = Session("VersionNo")

        With moVacation
            If .Update(eVacation.VacationId,
                       eVacation.EmployeeId,
                       eVacation.TypeId,
                       eVacation.AltEmployeeId,
                       eVacation.EffectiveDate,
                       eVacation.DateExpire,
                       eVacation.DateOfReturn,
                       eVacation.Note,
                        eVacation.Address,
                       eVacation.Contact,
                       Session("AccountEmployeeId"),
                       moVersionNo) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEGatepass
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtGatepassId.Text)), BOEGatepass)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.VacationFormView.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.VacationFormView.FindControl("Button2"), Button).Text = "ÑÌæÚ"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)

            Else
                boolSeccessed = False
                Dim lbl As Label = DirectCast(Me.VacationFormView.FindControl("Label2"), Label)
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
        eVacation = FillObject()



        With moVacation
            If .Add(eVacation.EmployeeId,
                       eVacation.TypeId,
                       eVacation.AltEmployeeId,
                       eVacation.EffectiveDate,
                       eVacation.DateExpire,
                       eVacation.DateOfReturn,
                       eVacation.Note,
                        eVacation.Address,
                       eVacation.Contact,
                       Session("AccountEmployeeId")) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEGatepass
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtGatepassId.Text)), BOEGatepass)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.VacationFormView.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.VacationFormView.FindControl("Button2"), Button).Text = "ÑÌæÚ"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)

            Else
                boolSeccessed = False
                Dim lbl As Label = DirectCast(Me.VacationFormView.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Red
                lbl.Visible = True
                lbl.Text = .InfoMessage
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataDelete(ByVal id As Integer) As String
        moVacation.Delete(id)
        Return moVacation.InfoMessage
    End Function
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

    Private Function FillObject() As BOEVacation


        Try
            With eVacation
                If DirectCast(VacationFormView.FindControl("txtVacationId"), TextBox).Text.Trim <> "" Then
                    .VacationId = DirectCast(VacationFormView.FindControl("txtVacationId"), TextBox).Text
                End If

                .TypeId = DirectCast(VacationFormView.FindControl("ddlVacationTypeId"), DropDownList).SelectedValue
                .AltEmployeeId = DirectCast(VacationFormView.FindControl("ddlAltEmployeeId"), DropDownList).SelectedValue
                .EmployeeId = DirectCast(VacationFormView.FindControl("ddlEmployees"), DropDownList).SelectedValue
                .EffectiveDate = DirectCast(VacationFormView.FindControl("datEffectiveDate"), GeneralControls_MyDate).SelectedDate
                .DateExpire = DirectCast(VacationFormView.FindControl("datDateExpire"), GeneralControls_MyDate).SelectedDate
                .DateOfReturn = DirectCast(VacationFormView.FindControl("datDateOfReturn"), GeneralControls_MyDate).SelectedDate
                .Note = DirectCast(VacationFormView.FindControl("txtNotes"), TextBox).Text
                .Address = DirectCast(VacationFormView.FindControl("txtAddress"), TextBox).Text
                .Contact = DirectCast(VacationFormView.FindControl("txtContact"), TextBox).Text
                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eVacation
    End Function

    Protected Sub FindByEmployeeCode()
        moEmployee = New BOEmployee
        Try
            Dim dt As DataTable = New DataTable
            Dim EmployeeCode As String = CType(Me.VacationFormView.FindControl("txtEmployeeCode"), TextBox).Text
            dt = moEmployee.GetEmployeesByBadgeDataset(Session("AccountEmployeeId"), EmployeeCode, 1, 1).Tables(0)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                CType(Me.VacationFormView.FindControl("ddlEmployees"), DropDownList).DataSource = dt
                CType(Me.VacationFormView.FindControl("ddlEmployees"), DropDownList).DataBind()
                CType(Me.VacationFormView.FindControl("ddlEmployees"), DropDownList).SelectedValue = dt.Rows(0)("EmployeeId")
                CType(Me.VacationFormView.FindControl("ddlDepartments"), ctlDepartmentTree).SelectedValue = dt.Rows(0)("DepartmentId")
                CType(VacationFormView.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(dt.Rows(0)("EmployeeId"), 1)

            Else
                CType(Me.VacationFormView.FindControl("ddlEmployees"), DropDownList).SelectedValue = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub FindAltEmployeeByEmployeeCode()
        Try

            moEmployee = New BOEmployee
            Dim txtEmbloyeeCode As TextBox = DirectCast(Me.VacationFormView.FindControl("txtAltEmployeeCode"), TextBox)
            Dim ddlEmps As DropDownList = DirectCast(Me.VacationFormView.FindControl("ddlAltEmployeeId"), DropDownList)



            Dim dt As DataTable = New DataTable
            Dim EmployeeCode As String = txtEmbloyeeCode.Text
            'Dim DepartmentId As Integer = Session("AccountDepartmentId1")
            dt = moEmployee.GetEmployeesByBadgeDataset(0, EmployeeCode, 1, 1).Tables(0)
            'dt = EmployeeDAL.GetEmployeeByEmployeeCode(EmployeeCode, DepartmentId)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then


                ddlEmps.DataSource = dt
                ddlEmps.DataBind()
                ddlEmps.SelectedValue = dt.Rows(0)("EmployeeId")
            Else

                ddlEmps.SelectedValue = 0

            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

    

End Class
