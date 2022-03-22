Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports ATS.BO.Framework

Partial Class Attendance_Controls_ctlGatePass
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moVersionNo As Byte()
    Dim moGatePass As BOGatepass = New BOGatepass
    Dim eGatepass As New BOEGatepass
    Public uniqueKey As String = Guid.NewGuid().ToString("N")
    Dim moEmployee As ATS.BO.Framework.BOEmployee

#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            Startdate.SelectedDate = DateAdd(DateInterval.Month, -1, Now.Date)
            Enddate.SelectedDate = Now.Date
            FormView1.Visible = False
            ATS.BO.BOPagePermission.SetPagePermission(161, Me.GridView2, Me.FormView1, "Button1", 9, 10)
        End If

    End Sub
#End Region

#Region "common Methods"

    Private Function FillObject() As BOEGatepass
        Dim eGatepass As New BOEGatepass

        Try
            With eGatepass
                If DirectCast(FormView1.FindControl("txtGatepassId"), TextBox).Text.Trim <> "" Then
                    .GatepassId = DirectCast(FormView1.FindControl("txtGatepassId"), TextBox).Text
                End If

                .EmployeeId = DirectCast(FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue
                .GatepassBegDate = DirectCast(FormView1.FindControl("datGatepassBegDate"), GeneralControls_MyDate).SelectedDate
                .GatepassEndDate = DirectCast(FormView1.FindControl("datGatepassEndDate"), GeneralControls_MyDate).SelectedDate
                .GatepassBegTime = DirectCast(FormView1.FindControl("timGatepassBegTime"), RadDatePicker).SelectedDate
                .GatepassEndTime = DirectCast(FormView1.FindControl("timGatepassEndTime"), RadDatePicker).SelectedDate
                .GatepassTypeId = DirectCast(FormView1.FindControl("ddlGatepassTypeId"), DropDownList).SelectedValue
                .Notes = DirectCast(FormView1.FindControl("txtNotes"), TextBox).Text
                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eGatepass
    End Function

    Protected Sub FindByEmployeeCode()

        moEmployee = New BOEmployee
        Try
            Dim dt As DataTable = New DataTable
            Dim EmployeeCode As String = CType(Me.FormView1.FindControl("txtEmployeeCode"), TextBox).Text
            dt = moEmployee.GetEmployeesByBadgeDataset(Session("AccountEmployeeId"), EmployeeCode, 1, 1).Tables(0)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).DataSource = dt
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).DataBind()
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue = dt.Rows(0)("EmployeeId")
                CType(Me.FormView1.FindControl("ddlDepartments"), ctlDepartmentTree).SelectedValue = dt.Rows(0)("DepartmentId")
                CType(FormView1.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(dt.Rows(0)("EmployeeId"), 3)

            Else
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function
#End Region

#Region "Data Binding"
    Private Sub GridViewBinding()

        Dim SDate, EDate As Date
        Dim id As Integer

        Try
            


            id = CInt(ctlDepartmentTree1.SelectedValue)
            SDate = Startdate.SelectedDate
            EDate = Enddate.SelectedDate

            Me.dsGatePassesByDepartment.SelectParameters.Item("DepartmentID").DefaultValue = id
            Me.dsGatePassesByDepartment.SelectParameters.Item("BegDate").DefaultValue = SDate
            Me.dsGatePassesByDepartment.SelectParameters.Item("EndDate").DefaultValue = EDate

            GridView2.DataBind()
        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try

    End Sub
#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        Dim eGatepass As BOEGatepass = FillObject()

        moVersionNo = Session("VersionNo")

        With moGatePass
            If .Update(eGatepass.GatepassId,
                       eGatepass.EmployeeId,
                       eGatepass.GatepassTypeId,
                       eGatepass.GatepassBegDate,
                       eGatepass.GatepassEndDate,
                       eGatepass.GatepassBegTime,
                       eGatepass.GatepassEndTime,
                       eGatepass.ApprovedEmployeeId,
                       eGatepass.Notes,
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
        Dim eGatepass As BOEGatepass = FillObject()



        With moGatePass
            If .Add(eGatepass.EmployeeId,
                       eGatepass.GatepassTypeId,
                       eGatepass.GatepassBegDate,
                       eGatepass.GatepassEndDate,
                       eGatepass.GatepassBegTime,
                       eGatepass.GatepassEndTime,
                       eGatepass.ApprovedEmployeeId,
                       eGatepass.Notes,
                       Session("AccountEmployeeId")) Then

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

    Public Function DataDelete(ByVal id As Integer) As String
        moGatePass.Delete(id)
        Return moGatePass.InfoMessage
    End Function
#End Region

#Region "Controls Events"

#Region "GridView Events"

    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing
        Try
            Dim x As Integer = e.NewEditIndex
            Dim row As GridViewRow = GridView2.Rows(x)
            Dim GatePassId As String = row.Cells(0).Text
            Dim GatePasssList As New List(Of BOEGatepass)()
            eGatepass = moGatePass.Find(GatePassId)
            GatePasssList.Add(eGatepass)
            FormView1.DataSource = GatePasssList
            Me.FormView1.ChangeMode(FormViewMode.Edit)
            Me.FormView1.DataBind()
            Me.GridView2.Visible = False
            FormView1.Visible = True
            btnAddGatePass.Visible = False
            dvSearch.Visible = False

        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try

    End Sub

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("GatepassId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView2.DataSource = Nothing
            GridView2.DataBind()
            'GridViewBinding()
        End If
    End Sub
#End Region

#Region "FormView Events"
    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles FormView1.DataBound
        If FormView1.CurrentMode = FormViewMode.Edit Then

            If Not IsNothing(eGatepass.EmployeeId) Then
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).DataSource = New BOEmployee().GetEmployeesByIdDataset(Session("AccountEmployeeId"), eGatepass.EmployeeId) 'EmployeeDAL.GetManagerById(eVacation.EmployeeId)
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).DataBind()
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue = eGatepass.EmployeeId
                CType(FormView1.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(eGatepass.EmployeeId, 3)

            End If

            If Not IsNothing(eGatepass.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eGatepass.VersionNo)
            End If

        End If


    End Sub
#End Region

#Region "Other Controls Events"

    Protected Sub btnView()
        GridViewBinding()
    End Sub

    Protected Sub btnAddGatePass_Click(sender As Object, e As EventArgs) Handles btnAddGatePass.Click
        Try
            Me.FormView1.Visible = True
            Me.FormView1.DefaultMode = FormViewMode.Insert
            Me.GridView2.Visible = False
            Me.btnAddGatePass.Visible = False
           
            CType(FormView1.FindControl("timGatepassBegTime"), RadDatePicker).SelectedDate = Now.Date
            CType(FormView1.FindControl("timGatepassEndTime"), RadDatePicker).SelectedDate = Now.Date

            dvSearch.Visible = False
        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try
    End Sub

    Protected Sub ddlDepartments_SelectedIndexChanged()

        If Me.FormView1.CurrentMode = FormViewMode.Edit Then
            Return
        End If
        Try
           Dim ddlEmployees As DropDownList = CType(FormView1.Row.FindControl("ddlEmployees"), DropDownList)
            Dim DepartmentId As Integer
            If Not ddlEmployees Is Nothing Then
                DepartmentId = CType(FormView1.Row.FindControl("ddlDepartments"), ctlDepartmentTree).SelectedValue
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

    Protected Sub ddlEmployees_SelectedIndexChanged(sender As Object, e As EventArgs)
        CType(FormView1.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(CType(FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue, 3)
    End Sub
#End Region

#End Region

   
End Class
