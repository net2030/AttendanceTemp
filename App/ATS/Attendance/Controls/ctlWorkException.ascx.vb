Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlProcessAttendance
    Inherits System.Web.UI.UserControl


#Region "General Declaration"
    Dim moWorkException As BOWorkException = New BOWorkException
    Dim eWorkException As New BOEWorkException
    Dim moVersionNo As Byte()
    Dim moEmployee As ATS.BO.Framework.BOEmployee

#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
           
            Startdate.SelectedDate = DateAdd(DateInterval.Month, -1, Now.Date)
            Enddate.SelectedDate = Now.Date
            GridViewBinding()
            ATS.BO.BOPagePermission.SetPagePermission(166, Me.GridView2, Me.FormView1, "Button1", 8, 9)
        End If

    End Sub
#End Region

#Region "Data Binding"

    Private Sub GridViewBinding()

        Dim SDate, EDate As Date
        Dim id As Integer

        Try
          

            id = CInt(ctlDepartmentTree1.SelectedValue)
            SDate = Startdate.SelectedDate
            EDate = Enddate.SelectedDate

            Me.dsWorkExceptions.SelectParameters.Item("DepartmentID").DefaultValue = id
            Me.dsWorkExceptions.SelectParameters.Item("BegDate").DefaultValue = SDate
            Me.dsWorkExceptions.SelectParameters.Item("EndDate").DefaultValue = EDate

            GridView2.DataBind()

        Catch ex As Exception
            Dim message As String = ex.Message
            ' Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try

    End Sub
#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eWorkException = FillObject()

        moVersionNo = Session("VersionNo")

        With moWorkException
            If .Update(eWorkException.WorkExceptionId,
                       eWorkException.EmployeeId,
                       eWorkException.WorkExceptionTypeId,
                       eWorkException.WorkExceptionBegDate,
                       eWorkException.WorkExceptionEndDate,
                       eWorkException.DepartmentId,
                       eWorkException.Notes,
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
                CType(Me.FormView1.FindControl("Button2"), Button).Text = "????"
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
        eWorkException = FillObject()



        With moWorkException
            If .Add(eWorkException.EmployeeId,
                       eWorkException.WorkExceptionTypeId,
                       eWorkException.WorkExceptionBegDate,
                       eWorkException.WorkExceptionEndDate,
                       eWorkException.DepartmentId,
                       eWorkException.Notes,
                       Session("AccountEmployeeId")) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEGatepass
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtGatepassId.Text)), BOEGatepass)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button2"), Button).Text = "????"
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
        moWorkException.Delete(id)
        Return moWorkException.InfoMessage
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

    Private Function FillObject() As BOEWorkException


        Try
            With eWorkException
                If DirectCast(FormView1.FindControl("txtWorkExceptionId"), TextBox).Text.Trim <> "" Then
                    .WorkExceptionId = DirectCast(FormView1.FindControl("txtWorkExceptionId"), TextBox).Text
                End If

                .WorkExceptionTypeId = DirectCast(FormView1.FindControl("ddlWorkExceptionTypeId"), DropDownList).SelectedValue
                '.DepartmentId = DirectCast(FormView1.FindControl("ddlDepartments"), DropDownList).SelectedValue
                .EmployeeId = DirectCast(FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue
                .WorkExceptionBegDate = DirectCast(FormView1.FindControl("datWorkExceptionBegDate"), GeneralControls_MyDate).SelectedDate
                .WorkExceptionEndDate = DirectCast(FormView1.FindControl("datWorkExceptionEndDate"), GeneralControls_MyDate).SelectedDate
                .Notes = DirectCast(FormView1.FindControl("txtNotes"), TextBox).Text
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eWorkException
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
                CType(FormView1.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(dt.Rows(0)("EmployeeId"), 2)

            Else
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Controls Events"

#Region "GridView Events"

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("WorkExceptionId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView2.DataSource = Nothing
            GridView2.DataBind()
        End If
    End Sub

    Protected Sub gvEG_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing

        Dim x As Integer = e.NewEditIndex
        Dim row As GridViewRow = GridView2.Rows(x)
        Dim WorkExceptionId As String = row.Cells(0).Text
        Dim WorkExceptionsList As New List(Of BOEWorkException)()
        eWorkException = moWorkException.Find(WorkExceptionId)
        WorkExceptionsList.Add(eWorkException)
        FormView1.DataSource = WorkExceptionsList
        Me.FormView1.ChangeMode(FormViewMode.Edit)
        Me.FormView1.DataBind()
        Me.GridView2.Visible = False
        FormView1.Visible = True
        btnAdd0.Visible = False

        dvSearch.Visible = False

    End Sub

#End Region

#Region "Form Events"

    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles FormView1.DataBound
        If FormView1.CurrentMode = FormViewMode.Edit Then

            If Not IsNothing(eWorkException.EmployeeId) Then
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).DataSource = New BOEmployee().GetEmployeesByIdDataset(Session("AccountEmployeeId"), eWorkException.EmployeeId) 'EmployeeDAL.GetManagerById(eVacation.EmployeeId)
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).DataBind()
                CType(Me.FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue = eWorkException.EmployeeId
                CType(FormView1.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(eWorkException.EmployeeId, 2)
            End If

            If Not IsNothing(eWorkException.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eWorkException.VersionNo)
            End If
        End If

    End Sub

#End Region

#Region "Other Events"
    Protected Sub btnView(sender As Object, e As EventArgs) Handles btnAdd.Click
        GridViewBinding()
    End Sub

    Protected Sub btnNew(sender As Object, e As EventArgs) Handles btnAdd0.Click
        Me.FormView1.Visible = True

        Me.FormView1.DefaultMode = FormViewMode.Insert
        Me.GridView2.Visible = False
        btnAdd0.Visible = False
        Me.dvSearch.Visible = False

        'Load TreeView 
        'ddlDepartment = TryCast(FormView1.FindControl("ddlDepartments"), DropDownList)
        'Dim EmpId As Integer = CInt(Session("AccountEmployeeId"))
        'ShowTreeNodes(EmpId)
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
        CType(FormView1.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(CType(FormView1.FindControl("ddlEmployees"), DropDownList).SelectedValue, 2)
    End Sub

#End Region
    
#End Region

End Class
