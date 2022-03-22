Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports ATS.BO.Framework

Partial Class Attendance_Controls_ctlManualLog
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moVersionNo As Byte()
    Dim moMachineLog As BOMachineLog = New BOMachineLog
    Dim eMachineLog As New BOEMachineLog
    Public uniqueKey As String = Guid.NewGuid().ToString("N")
    Dim moEmployee As ATS.BO.Framework.BOEmployee

#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            'If Me.Request.QueryString("VacationId") Is Nothing Then
            '    FormView1.ChangeMode(FormViewMode.Insert)
            'Else
            '    Me.dsVacation.SelectParameters("VacationId").DefaultValue = Me.Request.QueryString("VacationId")
            '    FormView1.ChangeMode(FormViewMode.Edit)
            'End If
            ' ATS.BO.BOPagePermission.SetPagePermission(178, Me.GridView2, Me.FormView1, "btnAddVacation", 7, 8)
            'Dim EmpId As Integer = CInt(Session("AccountEmployeeId"))
            'ShowTreeNodes(EmpId)
            Startdate.SelectedDate = DateAdd(DateInterval.Month, -1, Now.Date)
            Enddate.SelectedDate = Now.Date

            FormView1.Visible = False
        End If

    End Sub
#End Region

#Region "common Methods"

    Private Function FillObject() As BOEMachineLog
        Dim eMachineLog As New BOEMachineLog

        Try
            With eMachineLog
                If DirectCast(FormView1.FindControl("txtlogId"), TextBox).Text.Trim <> "" Then
                    .LogId = DirectCast(FormView1.FindControl("txtlogId"), TextBox).Text
                End If

                .LogTypeId = DirectCast(FormView1.FindControl("ddlLogTypeId"), DropDownList).SelectedValue
                .EmployeeId = DirectCast(FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue
                .LogDate = DirectCast(FormView1.FindControl("datLogDate"), GeneralControls_MyDate).SelectedDate
                .LogTime = DirectCast(FormView1.FindControl("datLogTime"), RadDatePicker).SelectedDate
               
                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eMachineLog
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


            Me.dsManualLogs.SelectParameters.Item("DepartmentID").DefaultValue = id
            Me.dsManualLogs.SelectParameters.Item("BegDate").DefaultValue = SDate
            Me.dsManualLogs.SelectParameters.Item("EndDate").DefaultValue = EDate
            GridView2.DataBind()
        Catch ex As Exception
        End Try

    End Sub
#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        Dim eMachineLog As BOEMachineLog = FillObject()

        moVersionNo = Session("VersionNo")

        With moMachineLog
            If .Update(eMachineLog.LogId,
                       eMachineLog.EmployeeId,
                       eMachineLog.LogDate,
                       eMachineLog.LogTime,
                       eMachineLog.LogTypeId,
                       Session("AccountEmployeeId"),
                       moVersionNo) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEMachineLog
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtMachineLogId.Text)), BOEMachineLog)
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
        Dim eMachineLog As BOEMachineLog = FillObject()



        With moMachineLog
            If .Add(eMachineLog.EmployeeId,
                       eMachineLog.LogDate,
                       eMachineLog.LogTime,
                       eMachineLog.LogTypeId,
                       Session("AccountEmployeeId")) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEMachineLog
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtMachineLogId.Text)), BOEMachineLog)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button2"), Button).Text = "ÑÌæÚ"
                'CType(Me.FormView1.FindControl("Button1"), Button).Enabled = False
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
        moMachineLog.Delete(id)
        Return moMachineLog.InfoMessage
    End Function
#End Region

#Region "Controls Events"

#Region "GridView Events"

    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing
        Try
            Dim x As Integer = e.NewEditIndex
            Dim row As GridViewRow = GridView2.Rows(x)
            Dim MachineLogId As String = row.Cells(0).Text
            Dim MachineLogsList As New List(Of BOEMachineLog)()
            eMachineLog = moMachineLog.Find(MachineLogId)
            MachineLogsList.Add(eMachineLog)
            FormView1.DataSource = MachineLogsList
            Me.FormView1.ChangeMode(FormViewMode.Edit)
            Me.FormView1.DataBind()
            Me.GridView2.Visible = False
            FormView1.Visible = True
            btnAddVacation.Visible = False

            dvSearch.Visible = False
        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try

    End Sub

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("LogId").ToString()
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

            If Not IsNothing(eMachineLog.EmployeeId) Then
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).DataSource = New BOEmployee().GetEmployeesByIdDataset(Session("AccountEmployeeId"), eMachineLog.EmployeeId) 'EmployeeDAL.GetManagerById(eVacation.EmployeeId)
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).DataBind()
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue = eMachineLog.EmployeeId
            End If

            If Not IsNothing(eMachineLog.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eMachineLog.VersionNo)
            End If
            ATS.BO.BOPagePermission.SetPagePermission(178, Me.GridView2, Me.FormView1, "btnAddVacation", 7, 8)

        End If
    End Sub
#End Region

#Region "Other Controls Events"

    Protected Sub btnView()
        GridViewBinding()
    End Sub

    Protected Sub btnAddLog_Click(sender As Object, e As EventArgs) Handles btnAddVacation.Click
        Try
            Me.FormView1.Visible = True
            Me.FormView1.DefaultMode = FormViewMode.Insert
            Me.GridView2.Visible = False
            Me.btnAddVacation.Visible = False
            dvSearch.Visible = False

            'Load TreeView 
            'ddlDepartment = TryCast(FormView1.FindControl("ddlDepartments"), DropDownList)
            'Dim EmpId As Integer = CInt(Session("AccountEmployeeId"))
            'ShowTreeNodes(EmpId)
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
#End Region

#End Region

    '#Region "ComboBox Tree View"

    '    Private ddlDepartment As DropDownList = New DropDownList
    '    Private level As Integer = -1
    '    Private Sub ShowTreeNodes(ByVal EmployeeId As Integer)


    '        ddlDepartment.Items.Clear()

    '        Dim dtNodes As DataTable = DepartmentDAL.GetDepartmentList(EmployeeId)
    '        'select all the nodes
    '        RecursiveFillTree(dtNodes, 0)
    '        ddlDepartment.Items.Insert(0, New ListItem("-ÅÎÊÑ-", "0"))

    '    End Sub

    '    Private Sub RecursiveFillTree(dtParent As DataTable, parentID As Integer)
    '        level += 1
    '        'on the each call level increment 1
    '        Dim appender As New StringBuilder()

    '        For j As Integer = 0 To level - 1

    '            appender.Append("&nbsp;&nbsp;&nbsp;&nbsp;")
    '        Next
    '        If level > 0 Then
    '            appender.Append("|__")
    '        End If

    '        Dim dv As New DataView(dtParent)

    '        dv.RowFilter = String.Format("ParentId = {0}", parentID)

    '        Dim i As Integer

    '        If dv.Count > 0 Then
    '            For i = 0 To dv.Count - 1
    '                ddlDepartment.Items.Add(New ListItem(Server.HtmlDecode(appender.ToString() & dv(i)("DepartmentName").ToString()), dv(i)("DepartmentId").ToString()))
    '                RecursiveFillTree(dtParent, Integer.Parse(dv(i)("DepartmentId").ToString()))
    '            Next
    '        End If

    '        level -= 1
    '        'on the each function end level will decrement by 1
    '    End Sub

    '#End Region
End Class
