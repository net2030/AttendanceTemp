Imports Telerik.Web.UI
Imports ATS.BO.Framework


Partial Class AccountAdmin_Controls_ctlAccountWorkTypeList
    Inherits System.Web.UI.UserControl

#Region "General Declarations"
    Dim WorkScheduleId As Integer
    Dim moWorkSchedule As BOWorkSchedule = New BOWorkSchedule
    Dim moWorkScheduleDay As BOWorkScheduleDay = New BOWorkScheduleDay
    Dim eWorkSchedule As New BOEWorkSchedule
    Dim moVersionNo As Byte()
#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            ATS.BO.BOPagePermission.SetPagePermission(13, Me.GridView2, Me.FormView1, "Button1", 4, 5)
            GridViewDataBinding()

        End If

    End Sub
#End Region

#Region "Data Binding"

    Protected Sub BindFormView(ByVal WorkScheduleId As Integer)

        Dim WorkSchedulesList As New List(Of BOEWorkSchedule)()
        eWorkSchedule = moWorkSchedule.Find(WorkScheduleId)
        WorkSchedulesList.Add(eWorkSchedule)
        FormView1.DataSource = WorkSchedulesList
        Me.FormView1.ChangeMode(FormViewMode.Edit)
        Me.FormView1.DataBind()
        Me.GridView2.Visible = False
        FormView1.Visible = True
        GridView1.Visible = True
        GridViewDataBinding()
        btnAddWorkSchedule.Visible = False
    End Sub

    Private Sub GridViewDataBinding()
        If Me.FormView1.CurrentMode = FormViewMode.Insert Then
            BindDummyWorkScheduleDaysas()
        Else
            Dim id As Integer = DirectCast(FormView1.FindControl("txtWorkScheduleId"), TextBox).Text
            GridView1.DataSource = moWorkScheduleDay.GetWorkScheduleDaysDataset(id)
            GridView1.DataBind()
        End If
    End Sub

    Private Sub BindDummyWorkScheduleDaysas()

        Dim ds As New DataSet()
        Dim modsWorkScheduleDays As New DataTable()
        modsWorkScheduleDays.Columns.Add("WeekDayNo")
        modsWorkScheduleDays.Columns.Add("WeekDayName")
        modsWorkScheduleDays.Columns.Add("IsWeekendDay")
        modsWorkScheduleDays.Columns.Add("WorkBegTime")
        modsWorkScheduleDays.Columns.Add("WorkEndTime")
        modsWorkScheduleDays.Columns.Add("VersionNo")


        modsWorkScheduleDays.Rows.Clear()


        Dim BegTime As [String] = Now
        Dim EndTime As [String] = Now

        Dim NewRow1 As DataRow = modsWorkScheduleDays.NewRow()

        NewRow1("WeekDayNo") = 1
        NewRow1("WeekDayName") = "Saturday"
        NewRow1("IsWeekendDay") = False
        NewRow1("WorkBegTime") = BegTime
        NewRow1("WorkEndTime") = BegTime
        modsWorkScheduleDays.Rows.Add(NewRow1)

        Dim NewRow2 As DataRow = modsWorkScheduleDays.NewRow()
        NewRow2("WeekDayNo") = 2
        NewRow2("WeekDayName") = "Sunday"
        NewRow2("IsWeekendDay") = False
        NewRow2("WorkBegTime") = BegTime
        NewRow2("WorkEndTime") = EndTime
        modsWorkScheduleDays.Rows.Add(NewRow2)

        Dim NewRow3 As DataRow = modsWorkScheduleDays.NewRow()
        NewRow3("WeekDayNo") = 3
        NewRow3("WeekDayName") = "Monday"
        NewRow3("IsWeekendDay") = False
        NewRow3("WorkBegTime") = BegTime
        NewRow3("WorkEndTime") = EndTime
        modsWorkScheduleDays.Rows.Add(NewRow3)

        Dim NewRow4 As DataRow = modsWorkScheduleDays.NewRow()
        NewRow4("WeekDayNo") = 4
        NewRow4("WeekDayName") = "Tuesday"
        NewRow4("IsWeekendDay") = False
        NewRow4("WorkBegTime") = BegTime
        NewRow4("WorkEndTime") = EndTime
        modsWorkScheduleDays.Rows.Add(NewRow4)

        Dim NewRow5 As DataRow = modsWorkScheduleDays.NewRow()
        NewRow5("WeekDayNo") = 5
        NewRow5("WeekDayName") = "Wednsday"
        NewRow5("IsWeekendDay") = False
        NewRow5("WorkBegTime") = BegTime
        NewRow5("WorkEndTime") = EndTime
        modsWorkScheduleDays.Rows.Add(NewRow5)

        Dim NewRow6 As DataRow = modsWorkScheduleDays.NewRow()
        NewRow6("WeekDayNo") = 6
        NewRow6("WeekDayName") = "Thrusday"
        NewRow6("IsWeekendDay") = False
        NewRow6("WorkBegTime") = BegTime
        NewRow6("WorkEndTime") = EndTime
        modsWorkScheduleDays.Rows.Add(NewRow6)

        Dim NewRow7 As DataRow = modsWorkScheduleDays.NewRow()
        NewRow7("WeekDayNo") = 7
        NewRow7("WeekDayName") = "Friday"
        NewRow7("IsWeekendDay") = False
        NewRow7("WorkBegTime") = BegTime
        NewRow7("WorkEndTime") = BegTime
        modsWorkScheduleDays.Rows.Add(NewRow7)

        ds.Tables.Add(modsWorkScheduleDays)
        Dim dv As DataView = Nothing
        dv = ds.Tables(0).DefaultView



        GridView1.DataSource = dv
        GridView1.DataBind()

    End Sub

#End Region

#Region "GridView "

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Select" Then
            Me.GridView2.Visible = False
            Me.btnAddWorkSchedule.Visible = False
        ElseIf (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("WorkScheduleId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView2.DataSource = Nothing
            GridView2.DataBind()
        End If
    End Sub

    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing
        Dim x As Integer = e.NewEditIndex

        Dim row As GridViewRow = GridView2.Rows(x)
        Dim WorkScheduleId As String = row.Cells(0).Text
        Dim WorkSchedulesList As New List(Of BOEWorkSchedule)()
        eWorkSchedule = moWorkSchedule.Find(WorkScheduleId)
        WorkSchedulesList.Add(eWorkSchedule)
        FormView1.DataSource = WorkSchedulesList
        Me.FormView1.ChangeMode(FormViewMode.Edit)
        Me.FormView1.DataBind()
        Me.GridView2.Visible = False
        FormView1.Visible = True
        GridView1.Visible = True
        GridViewDataBinding()
        btnAddWorkSchedule.Visible = False

    End Sub



    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        GridView1.EditIndex = -1
        GridViewDataBinding()
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        GridView1.EditIndex = e.NewEditIndex
        GridViewDataBinding()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating

        Dim eWorkSchedualDay As BOEWorkScheduleDay = New BOEWorkScheduleDay
        eWorkSchedualDay.WorkScheduleId = DirectCast(FormView1.FindControl("txtWorkScheduleId"), TextBox).Text
        eWorkSchedualDay.WeekDayNo = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtWeekDayNo"), Label).Text
        eWorkSchedualDay.IsWeekendDay = DirectCast(GridView1.Rows(e.RowIndex).FindControl("chkIsWeekendDay"), CheckBox).Checked
        eWorkSchedualDay.WorkBegTime = DirectCast(GridView1.Rows(e.RowIndex).FindControl("datWorkBegTime"), RadTimePicker).SelectedDate
        eWorkSchedualDay.WorkEndTime = DirectCast(GridView1.Rows(e.RowIndex).FindControl("datWorkEndTime"), RadTimePicker).SelectedDate
        eWorkSchedualDay.VersionNo = GridView1.DataKeys(e.RowIndex)("VersionNo")

        If Not moWorkScheduleDay.Update(eWorkSchedualDay.WorkScheduleId,
                                 1,
                                 eWorkSchedualDay.WeekDayNo,
                                 eWorkSchedualDay.IsWeekendDay,
                                 eWorkSchedualDay.WorkBegTime,
                                 eWorkSchedualDay.WorkEndTime,
                                 Session("AccountEmployeeId"),
                                 eWorkSchedualDay.VersionNo) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moWorkScheduleDay.InfoMessage), True)
        End If


        GridView1.EditIndex = -1
        GridViewDataBinding()
    End Sub

#End Region

#Region "Form Events"

    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles FormView1.DataBound
        If Me.FormView1.CurrentMode = FormViewMode.Edit Then
            If Not IsNothing(eWorkSchedule.WorkScheduleId) Then
                System.Web.HttpContext.Current.Session.Add("WorkScheduleId", eWorkSchedule.WorkScheduleId)
                ctlEmployeesWorkSchedule.ListBoxDataBinding(eWorkSchedule.WorkScheduleId)

            End If
            If Not IsNothing(eWorkSchedule.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eWorkSchedule.VersionNo)
            End If
        End If

    End Sub

#End Region

#Region "Other Controls Events handling"

    Protected Sub btnAddWorkSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddWorkSchedule.Click
        Me.FormView1.Visible = True
        Me.FormView1.DefaultMode = FormViewMode.Insert
        GridView1.Visible = True
        Me.GridView2.Visible = False
        btnAddWorkSchedule.Visible = False
        BindDummyWorkScheduleDaysas()
    End Sub



#End Region

#Region "Data Manipulation"

    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eWorkSchedule = FillObject()

        moVersionNo = Session("VersionNo")

        With moWorkSchedule
            If .Update(eWorkSchedule.WorkScheduleId,
                       eWorkSchedule.WorkScheduleName,
                       eWorkSchedule.WorkScheduleTypeId,
                       eWorkSchedule.DepartmentId,
                       eWorkSchedule.ScheduleBegDate,
                       eWorkSchedule.ScheduleEndDate,
                       eWorkSchedule.IsEffectiveDuringHoliday,
                       eWorkSchedule.IsPolicyApplied,
                       eWorkSchedule.PolicyId,
                       1,
                       0,
                       7,
                       Now.Date,
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

            Else
                boolSeccessed = False
                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Red
                lbl.Visible = True
                lbl.Text = .InfoMessage
            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataAdd() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eWorkSchedule = FillObject()



        With moWorkSchedule
            If .Add(eWorkSchedule.WorkScheduleName,
                       eWorkSchedule.WorkScheduleTypeId,
                       eWorkSchedule.DepartmentId,
                       eWorkSchedule.ScheduleBegDate,
                       eWorkSchedule.ScheduleEndDate,
                       eWorkSchedule.IsEffectiveDuringHoliday,
                       eWorkSchedule.IsPolicyApplied,
                       eWorkSchedule.PolicyId,
                       1,
                       0,
                       7,
                       Now.Date,
                       Session("AccountEmployeeId")) Then

                boolSeccessed = True

                Dim eWorkSchedualday As BOEWorkScheduleDay = New BOEWorkScheduleDay

                For Each row1 As GridViewRow In GridView1.Rows
                    eWorkSchedualday.WorkScheduleId = .Identity
                    eWorkSchedualday.WeekDayNo = DirectCast(row1.FindControl("txtWeekDayNo"), Label).Text
                    eWorkSchedualday.IsWeekendDay = DirectCast(row1.FindControl("chkIsWeekendDay"), CheckBox).Checked
                    eWorkSchedualday.WorkBegTime = DirectCast(row1.FindControl("datWorkBegTime"), RadTimePicker).SelectedDate
                    eWorkSchedualday.WorkEndTime = DirectCast(row1.FindControl("datWorkEndTime"), RadTimePicker).SelectedDate

                    moWorkScheduleDay.Add(eWorkSchedualday.WorkScheduleId,
                                          1,
                                          eWorkSchedualday.WeekDayNo,
                                          eWorkSchedualday.IsWeekendDay,
                                          eWorkSchedualday.WorkBegTime,
                                          eWorkSchedualday.WorkEndTime,
                                          Session("AccountEmployeeId"))
                Next

                BindFormView(.Identity)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button1"), Button).Enabled = False
                System.Web.HttpContext.Current.Session.Add("WorkScheduleId", .Identity)


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
        moWorkSchedule.Delete(id)
        Return moWorkSchedule.InfoMessage
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

    Private Function FillObject() As BOEWorkSchedule


        Try
            With eWorkSchedule
                If DirectCast(FormView1.FindControl("txtWorkScheduleId"), TextBox).Text.Trim <> "" Then
                    .WorkScheduleId = DirectCast(FormView1.FindControl("txtWorkScheduleId"), TextBox).Text
                End If
                .WorkScheduleName = DirectCast(FormView1.FindControl("txtWorkScheduleName"), TextBox).Text
                .ScheduleBegDate = DirectCast(FormView1.FindControl("datStartDate"), GeneralControls_MyDate).SelectedDate
                .ScheduleEndDate = DirectCast(FormView1.FindControl("datEnddate"), GeneralControls_MyDate).SelectedDate
                .IsEffectiveDuringHoliday = True
                .WorkScheduleTypeId = 1
                .DepartmentId = 1
                .IsPolicyApplied = True
                .PolicyId = DirectCast(FormView1.FindControl("ddlPolicy"), DropDownList).SelectedValue


                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eWorkSchedule
    End Function

#End Region


End Class
