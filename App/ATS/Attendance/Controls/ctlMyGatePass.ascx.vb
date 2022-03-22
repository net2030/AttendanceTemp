Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports ATS.BO.Framework

Partial Class Attendance_Controls_ctlMyGatePass
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moVersionNo As Byte()
    Dim moGatePass As BOGatepass = New BOGatepass
    Dim eGatePass As BOEGatepass = New BOEGatepass
#End Region

#Region "Load"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            GridViewBinding()
            FormView1.Visible = False
        End If

    End Sub


#End Region

#Region "common Methods"
    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function

    Private Function FillObject() As BOEGatepass
        Dim eGatepass As New BOEGatepass

        Try
            With eGatepass
                If DirectCast(FormView1.FindControl("txtGatepassId"), TextBox).Text.Trim <> "" Then
                    .GatepassId = DirectCast(FormView1.FindControl("txtGatepassId"), TextBox).Text
                End If

                .EmployeeId = Session("AccountEmployeeId")
                .GatepassBegDate = DirectCast(FormView1.FindControl("datGatepassBegDate"), GeneralControls_MyDate).SelectedDate
                .GatepassEndDate = DirectCast(FormView1.FindControl("datGatepassEndDate"), GeneralControls_MyDate).SelectedDate
                .GatepassBegTime = DirectCast(FormView1.FindControl("timGatepassBegTime"), RadDatePicker).SelectedDate
                .GatepassEndTime = DirectCast(FormView1.FindControl("timGatepassEndTime"), RadDatePicker).SelectedDate
                .GatepassTypeId = DirectCast(FormView1.FindControl("ddlGatepassTypeId"), DropDownList).SelectedValue
                .Notes = DirectCast(FormView1.FindControl("txtNotes"), TextBox).Text
                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try




        Return eGatepass
    End Function

#End Region

#Region "Data Binding"
    Private Sub GridViewBinding()
        Me.dsGatePassesByEmployee.SelectParameters("EmployeeId").DefaultValue = Session("AccountEmployeeId")
        Me.GridView2.DataBind()
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

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("GatePassId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView2.DataSource = Nothing
            GridView2.DataBind()
        End If
    End Sub

    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing
        Dim x As Integer = e.NewEditIndex
        Dim row As GridViewRow = GridView2.Rows(x)
        Dim GatePassId As String = row.Cells(0).Text
        Dim GatePasssList As New List(Of BOEGatepass)()
        eGatePass = moGatePass.Find(GatePassId)
        GatePasssList.Add(eGatePass)
        FormView1.DataSource = GatePasssList
        Me.FormView1.ChangeMode(FormViewMode.Edit)
        Me.FormView1.DataBind()
        Me.GridView2.Visible = False
        FormView1.Visible = True
        btnAddGatePass.Visible = False
    End Sub

#End Region

#Region "FormView Events"
    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles FormView1.DataBound
        If FormView1.CurrentMode = FormViewMode.Edit Then
            If Not IsNothing(eGatePass.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eGatePass.VersionNo)
                CType(FormView1.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(eGatePass.EmployeeId, 3)

            End If
        End If


    End Sub
#End Region

#Region "Other Controls Events"

    Protected Sub btnAddGatePass_Click()
      

        Me.FormView1.Visible = True
        Me.FormView1.DefaultMode = FormViewMode.Insert
        Me.GridView2.Visible = False
        Me.btnAddGatePass.Visible = False
        CType(FormView1.FindControl("ctlEmployeesLeaveBalance"), AccountAdmin_Controls_ctlEmployeesLeaveBalance).GridViewDataBinding(Session("AccountEmployeeId"), 3)


        'CType(FormView1.FindControl("ddlEmployees"), DropDownList).Enabled = False
        CType(FormView1.FindControl("datGatepassBegDate"), GeneralControls_MyDate).SelectedDate = Now.Date
        CType(FormView1.FindControl("datGatepassEndDate"), GeneralControls_MyDate).SelectedDate = Now.Date
        CType(FormView1.FindControl("timGatepassBegTime"), RadDatePicker).SelectedDate = Now.Date
        CType(FormView1.FindControl("timGatepassEndTime"), RadDatePicker).SelectedDate = Now.Date


    End Sub

#End Region

#End Region

End Class
