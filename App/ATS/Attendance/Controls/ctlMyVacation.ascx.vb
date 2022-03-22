Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Imports System.Globalization
Imports ATS.BO.Framework

Partial Class Attendance_Controls_ctlMyVacation
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moEmployee As BOEmployee = New BOEmployee
    Dim moVacation As BOVacation = New BOVacation
    Dim eVacation As New BOEVacation
    Dim moVersionNo As Byte()
#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not (Me.IsPostBack) Then
            GridViewBinding()
            MyVacationFormView.Visible = False
        End If

    End Sub
#End Region

#Region "Data Binding"
    Private Sub GridViewBinding()


        Dim EmpId As Integer = CInt(Session("AccountEmployeeId"))

        Try
            Me.dsVacations.SelectParameters.Item("EmployeeID").DefaultValue = EmpId

            GridView2.DataBind()
        Catch ex As System.Exception
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
        Dim x As Integer = e.NewEditIndex
        Dim row As GridViewRow = GridView2.Rows(x)
        Dim VacationId As String = row.Cells(0).Text
        Dim VacationsList As New List(Of BOEVacation)()
        eVacation = moVacation.Find(VacationId)
        VacationsList.Add(eVacation)
        MyVacationFormView.DataSource = VacationsList
        Me.MyVacationFormView.ChangeMode(FormViewMode.Edit)
        Me.MyVacationFormView.DataBind()
        Me.GridView2.Visible = False
        MyVacationFormView.Visible = True
        btnAddVacation.Visible = False


    End Sub

#End Region

#Region "Form Events"

    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles MyVacationFormView.DataBound

        hdnAltEmployeeRequiredValidator.Value = DirectCast(MyVacationFormView.FindControl("AlternativeEmployeeRequiredFieldValidator"), RequiredFieldValidator).ClientID
        hdnddlVacationtype.Value = DirectCast(MyVacationFormView.FindControl("ddlVacationTypeId"), DropDownList).ClientID


        System.Web.HttpContext.Current.Session.Add("StartDateClientId", DirectCast(MyVacationFormView.FindControl("datEffectiveDate"), GeneralControls_MyDate).TxtDate.ClientID)
        System.Web.HttpContext.Current.Session.Add("EndDateClientId", DirectCast(MyVacationFormView.FindControl("datDateExpire"), GeneralControls_MyDate).TxtDate.ClientID)


        If MyVacationFormView.CurrentMode = FormViewMode.Edit Then
            If Not IsNothing(eVacation.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eVacation.VersionNo)
                CType(MyVacationFormView.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(eVacation.EmployeeId, 1)

            End If
            If Not IsNothing(eVacation.AltEmployeeId) Then
                Dim EmployeesList As New List(Of BOEEmployee)()
                Dim eEmployee1 As New BOEEmployee
                eEmployee1 = moEmployee.Find(eVacation.AltEmployeeId)
                EmployeesList.Add(eEmployee1)

                CType(Me.MyVacationFormView.FindControl("ddlAltEmployeeId"), DropDownList).DataSource = EmployeesList
                CType(Me.MyVacationFormView.FindControl("ddlAltEmployeeId"), DropDownList).DataBind()
                CType(Me.MyVacationFormView.FindControl("ddlAltEmployeeId"), DropDownList).SelectedValue = eVacation.AltEmployeeId
            End If
        End If
    End Sub

#End Region

#Region "Other controls Events"

    Protected Sub btnAddVacation_Click(sender As Object, e As EventArgs) Handles btnAddVacation.Click
        Me.MyVacationFormView.Visible = True
        Me.MyVacationFormView.DefaultMode = FormViewMode.Insert
        Me.GridView2.Visible = False
        Me.btnAddVacation.Visible = False
        CType(MyVacationFormView.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(Session("AccountEmployeeId"), 1)

    End Sub



    Protected Sub btnView()

        GridViewBinding()

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

                Dim lbl As Label = DirectCast(Me.MyVacationFormView.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.MyVacationFormView.FindControl("Button2"), Button).Text = "����"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)

            Else
                boolSeccessed = False
                Dim lbl As Label = DirectCast(Me.MyVacationFormView.FindControl("Label2"), Label)
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

                Dim lbl As Label = DirectCast(Me.MyVacationFormView.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.MyVacationFormView.FindControl("Button2"), Button).Text = "����"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)

            Else
                boolSeccessed = False
                Dim lbl As Label = DirectCast(Me.MyVacationFormView.FindControl("Label2"), Label)
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
                If DirectCast(MyVacationFormView.FindControl("txtVacationId"), TextBox).Text.Trim <> "" Then
                    .VacationId = DirectCast(MyVacationFormView.FindControl("txtVacationId"), TextBox).Text
                End If

                .TypeId = DirectCast(MyVacationFormView.FindControl("ddlVacationTypeId"), DropDownList).SelectedValue
                .AltEmployeeId = DirectCast(MyVacationFormView.FindControl("ddlAltEmployeeId"), DropDownList).SelectedValue

                .EmployeeId = Session("AccountEmployeeId")
                .EffectiveDate = DirectCast(MyVacationFormView.FindControl("datEffectiveDate"), GeneralControls_MyDate).SelectedDate
                .DateExpire = DirectCast(MyVacationFormView.FindControl("datDateExpire"), GeneralControls_MyDate).SelectedDate
                .DateOfReturn = DirectCast(MyVacationFormView.FindControl("datDateOfReturn"), GeneralControls_MyDate).SelectedDate
                .Note = DirectCast(MyVacationFormView.FindControl("txtNotes"), TextBox).Text
                .Address = DirectCast(MyVacationFormView.FindControl("txtAddress"), TextBox).Text
                .Contact = DirectCast(MyVacationFormView.FindControl("txtContact"), TextBox).Text


                '.VersionNo = moVersionNo
            End With
        Catch ex As System.Exception
            Dim strEx As String = ex.Message
        End Try




        Return eVacation
    End Function

    Protected Sub FindAltEmployeeByEmployeeCode()
        Try


            Dim txtEmbloyeeCode As TextBox = DirectCast(Me.MyVacationFormView.FindControl("txtEmployeeCode"), TextBox)
            Dim ddlEmps As DropDownList = DirectCast(Me.MyVacationFormView.FindControl("ddlAltEmployeeId"), DropDownList)



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
