Imports Telerik.Web.UI
Imports ATS.BO.Framework


Partial Class AccountAdmin_Controls_ctlAccountWorkScheduleShift
    Inherits System.Web.UI.UserControl

#Region "General Declarations"
    Dim WorkScheduleId As Integer
    Dim moWorkSchedule As BOWorkSchedule = New BOWorkSchedule
    Dim moWorkScheduleDay As BOWorkScheduleDay = New BOWorkScheduleDay
    Dim eWorkSchedule As New BOEWorkSchedule
    Dim moVersionNo As Byte()

    Dim myCulture As System.Globalization.CultureInfo
    Dim dayOfWeek As DayOfWeek
    Dim dayName As String
    Dim c As Integer

#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If grdShift1.Rows.Count > 0 Then
        '    grdShift1.PageSize = 100
        'End If



        If Not (Me.IsPostBack) Then
            ATS.BO.BOPagePermission.SetPagePermission(13, Me.GridView2, Me.FormView1, "Button1", 4, 5)


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
        grdShift1.Visible = True

        btnAddWorkSchedule.Visible = False
    End Sub

    Private Sub GridViewDataBinding(Optional ByVal grd As GridView = Nothing, Optional ByVal StartDate As Date = Nothing, Optional ByVal PeriodLength As Integer = 7)

        If Me.FormView1.CurrentMode = FormViewMode.Insert Then

            BindDummyWorkScheduleDays(grd, StartDate, PeriodLength)
            grd.DataBind()

        Else
            Dim id As Integer = DirectCast(FormView1.FindControl("txtWorkScheduleId"), TextBox).Text
            Dim ddl As DropDownList = DirectCast(FormView1.FindControl("ddlShiftsCount"), DropDownList)

            If ddl.SelectedValue = 1 Then
                grdShift1.DataSource = moWorkScheduleDay.GetWorkScheduleDaysDataset(id, 1)
                grdShift1.DataBind()
                If grdShift1.Rows.Count <> PeriodLength Then
                    BindDummyWorkScheduleDays(grd, StartDate, PeriodLength)
                End If

            ElseIf ddl.SelectedValue = 2 Then
                grdShift1.DataSource = moWorkScheduleDay.GetWorkScheduleDaysDataset(id, 1)
                grdShift1.DataBind()

                grdShift2.DataSource = moWorkScheduleDay.GetWorkScheduleDaysDataset(id, 2)
                grdShift2.DataBind()

                If grdShift1.Rows.Count <> PeriodLength Then
                    BindDummyWorkScheduleDays(grdShift1, StartDate, PeriodLength)
                End If
                If grdShift2.Rows.Count <> PeriodLength Then
                    BindDummyWorkScheduleDays(grdShift2, StartDate, PeriodLength)
                End If

            ElseIf ddl.SelectedValue = 3 Then
                grdShift1.DataSource = moWorkScheduleDay.GetWorkScheduleDaysDataset(id, 1)
                grdShift1.DataBind()

                grdShift2.DataSource = moWorkScheduleDay.GetWorkScheduleDaysDataset(id, 2)
                grdShift2.DataBind()

                grdShift3.DataSource = moWorkScheduleDay.GetWorkScheduleDaysDataset(id, 3)
                grdShift3.DataBind()

                If grdShift1.Rows.Count <> PeriodLength Then
                    BindDummyWorkScheduleDays(grdShift1, StartDate, PeriodLength)
                End If

                If grdShift2.Rows.Count <> PeriodLength Then
                    BindDummyWorkScheduleDays(grdShift2, StartDate, PeriodLength)
                End If

                If grdShift3.Rows.Count <> PeriodLength Then
                    BindDummyWorkScheduleDays(grdShift3, StartDate, PeriodLength)
                End If

            End If



           


            If eWorkSchedule.PeriodLength Mod 7 <> 0 Then
                grdShift1.Columns(1).Visible = False
                grdShift2.Columns(1).Visible = False
                grdShift3.Columns(1).Visible = False
            Else
                grdShift1.Columns(1).Visible = True
                grdShift2.Columns(1).Visible = True
                grdShift3.Columns(1).Visible = True
            End If

        End If
    End Sub

    'Private Sub BindDummyWorkScheduleDaysas(ByVal Grd As GridView, Optional ByVal PeriodLength As Integer = 7)

    '    Dim ds As New DataSet()
    '    Dim modsWorkScheduleDays As New DataTable()
    '    modsWorkScheduleDays.Columns.Add("WeekDayNo")
    '    modsWorkScheduleDays.Columns.Add("WeekDayName")
    '    modsWorkScheduleDays.Columns.Add("IsWeekendDay")
    '    modsWorkScheduleDays.Columns.Add("WorkBegTime")
    '    modsWorkScheduleDays.Columns.Add("WorkEndTime")
    '    modsWorkScheduleDays.Columns.Add("VersionNo")

    '    Dim WeekDays() As String = {"«·”» ", "«·√Õœ", "«·≈À‰Ì‰", "«·À·«À«¡", "«·√—»⁄«¡", "«·Œ„Ì”", "«·Ã„⁄…"}

    '    modsWorkScheduleDays.Rows.Clear()


    '    Dim BegTime As [String] = Now
    '    Dim EndTime As [String] = Now

    '    Dim i As Integer = 0
    '    For dayNo As Integer = 1 To PeriodLength

    '        Dim NewRow As DataRow = modsWorkScheduleDays.NewRow()

    '        NewRow("WeekDayNo") = dayNo
    '        NewRow("WeekDayName") = WeekDays(i)
    '        NewRow("IsWeekendDay") = False
    '        NewRow("WorkBegTime") = BegTime
    '        NewRow("WorkEndTime") = BegTime
    '        modsWorkScheduleDays.Rows.Add(NewRow)

    '        i = i + 1
    '        If i = 7 Then
    '            i = 0
    '        End If
    '    Next

    '    ds.Tables.Add(modsWorkScheduleDays)
    '    Dim dv As DataView = Nothing
    '    dv = ds.Tables(0).DefaultView



    '    Grd.DataSource = dv
    '    Grd.DataBind()



    'End Sub

    Private Sub BindDummyWorkScheduleDays(ByVal grd As GridView, ByVal PeriodStartDate As Date, Optional ByVal PeriodLength As Integer = 7)

       

        Dim ds As New DataSet()
        Dim modsWorkScheduleDays As New DataTable()
        modsWorkScheduleDays.Columns.Add("WeekDayNo")

        'If PeriodLength Mod 7 = 0 Then
        modsWorkScheduleDays.Columns.Add("WeekDayName")
        'End If

        modsWorkScheduleDays.Columns.Add("IsWeekendDay")
        modsWorkScheduleDays.Columns.Add("WorkBegTime")
        modsWorkScheduleDays.Columns.Add("WorkEndTime")
        modsWorkScheduleDays.Columns.Add("VersionNo")



        modsWorkScheduleDays.Rows.Clear()


        Dim BegTime As [String] = Now
        Dim EndTime As [String] = Now

        Dim i As Integer = 0
        For dayNo As Integer = 1 To PeriodLength


            myCulture = Globalization.CultureInfo.CurrentCulture
            dayOfWeek = myCulture.Calendar.GetDayOfWeek(PeriodStartDate)
            dayName = myCulture.DateTimeFormat.GetDayName(dayOfWeek)


            Dim NewRow As DataRow = modsWorkScheduleDays.NewRow()

            NewRow("WeekDayNo") = dayNo
            NewRow("WeekDayName") = dayName
            NewRow("IsWeekendDay") = False
            NewRow("WorkBegTime") = BegTime
            NewRow("WorkEndTime") = BegTime
            modsWorkScheduleDays.Rows.Add(NewRow)

            PeriodStartDate = PeriodStartDate.AddDays(1)


            'i = i + 1
            'If i = 7 Then
            '    i = 0
            'End If
        Next

        ds.Tables.Add(modsWorkScheduleDays)
        Dim dv As DataView = Nothing
        dv = ds.Tables(0).DefaultView



        grd.DataSource = dv
        grd.DataBind()

        'grd.PageSize = PeriodLength

        If PeriodLength Mod 7 <> 0 Then
            grd.Columns(1).Visible = False
        Else
            grd.Columns(1).Visible = True
        End If



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
        grdShift1.Visible = True
        grdShift2.Visible = True
        grdShift3.Visible = True
        GridViewDataBinding(Nothing, eWorkSchedule.FirstPeriodStartDate, eWorkSchedule.PeriodLength)
        btnAddWorkSchedule.Visible = False

    End Sub

   

    Protected Sub grdShift1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdShift1.RowDataBound
        If (eWorkSchedule.IsPeriodic = False And eWorkSchedule.PeriodLength = 7) Or (eWorkSchedule.IsPeriodic = True And eWorkSchedule.PeriodLength Mod 7 <> 0) Then
            Return
        End If

        Dim DayDate As Date
        If FormView1.CurrentMode = FormViewMode.Edit Then
            DayDate = DirectCast(FormView1.FindControl("datPeriodStartDate"), GeneralControls_MyDate).SelectedDate
        Else
            Return
        End If


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dr As DataRow = (CType(e.Row.DataItem, DataRowView)).Row
            Dim dayNo As String = e.Row.Cells(0).Text
            myCulture = Globalization.CultureInfo.CurrentCulture
            dayOfWeek = myCulture.Calendar.GetDayOfWeek(DayDate.AddDays(e.Row.RowIndex))
            dayName = myCulture.DateTimeFormat.GetDayName(dayOfWeek)
            e.Row.Cells(1).Text = dayName
        End If
    End Sub

    'Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles grdShift1.RowCancelingEdit
    '    grdShift1.EditIndex = -1

    'End Sub

    'Protected Sub grdShift1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles grdShift1.RowEditing
    '    grdShift1.EditIndex = e.NewEditIndex
    '    GridViewDataBinding()
    'End Sub

    'Protected Sub grdShift1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles grdShift1.RowUpdating

    '    Dim eWorkSchedualDay As BOEWorkScheduleDay = New BOEWorkScheduleDay
    '    eWorkSchedualDay.WorkScheduleId = DirectCast(FormView1.FindControl("txtWorkScheduleId"), TextBox).Text
    '    eWorkSchedualDay.WeekDayNo = DirectCast(grdShift1.Rows(e.RowIndex).FindControl("txtWeekDayNo"), Label).Text
    '    eWorkSchedualDay.IsWeekendDay = DirectCast(grdShift1.Rows(e.RowIndex).FindControl("chkIsWeekendDay"), CheckBox).Checked
    '    eWorkSchedualDay.WorkBegTime = DirectCast(grdShift1.Rows(e.RowIndex).FindControl("datWorkBegTime"), RadTimePicker).SelectedDate
    '    eWorkSchedualDay.WorkEndTime = DirectCast(grdShift1.Rows(e.RowIndex).FindControl("datWorkEndTime"), RadTimePicker).SelectedDate
    '    eWorkSchedualDay.VersionNo = grdShift1.DataKeys(e.RowIndex)("VersionNo")

    '    If Not moWorkScheduleDay.Update(eWorkSchedualDay.WorkScheduleId,
    '                             1,
    '                             eWorkSchedualDay.WeekDayNo,
    '                             eWorkSchedualDay.IsWeekendDay,
    '                             eWorkSchedualDay.WorkBegTime,
    '                             eWorkSchedualDay.WorkEndTime,
    '                             Session("AccountEmployeeId"),
    '                             eWorkSchedualDay.VersionNo) Then
    '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moWorkScheduleDay.InfoMessage), True)
    '    End If


    '    grdShift1.EditIndex = -1
    '    GridViewDataBinding()
    'End Sub

    'Protected Sub grdShift2_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles grdShift2.RowCancelingEdit
    '    grdShift2.EditIndex = -1
    '    GridViewDataBinding()
    'End Sub

    Protected Sub grdShift2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdShift2.RowDataBound
        If (eWorkSchedule.IsPeriodic = False And eWorkSchedule.PeriodLength = 7) Or (eWorkSchedule.IsPeriodic = True And eWorkSchedule.PeriodLength Mod 7 <> 0) Then
            Return
        End If

        Dim DayDate As Date
        If FormView1.CurrentMode = FormViewMode.Edit Then
            DayDate = DirectCast(FormView1.FindControl("datPeriodStartDate"), GeneralControls_MyDate).SelectedDate
        Else
            Return
        End If


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dr As DataRow = (CType(e.Row.DataItem, DataRowView)).Row
            Dim dayNo As String = e.Row.Cells(0).Text
            myCulture = Globalization.CultureInfo.CurrentCulture
            dayOfWeek = myCulture.Calendar.GetDayOfWeek(DayDate.AddDays(e.Row.RowIndex))
            dayName = myCulture.DateTimeFormat.GetDayName(dayOfWeek)
            e.Row.Cells(1).Text = dayName
        End If
    End Sub

    'Protected Sub grdShift2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles grdShift2.RowEditing
    '    grdShift2.EditIndex = e.NewEditIndex
    '    GridViewDataBinding()
    'End Sub

    'Protected Sub grdShift2_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles grdShift2.RowUpdating

    '    Dim eWorkSchedualDay As BOEWorkScheduleDay = New BOEWorkScheduleDay
    '    eWorkSchedualDay.WorkScheduleId = DirectCast(FormView1.FindControl("txtWorkScheduleId"), TextBox).Text
    '    eWorkSchedualDay.WeekDayNo = DirectCast(grdShift2.Rows(e.RowIndex).FindControl("txtWeekDayNo"), Label).Text
    '    eWorkSchedualDay.IsWeekendDay = DirectCast(grdShift2.Rows(e.RowIndex).FindControl("chkIsWeekendDay"), CheckBox).Checked
    '    eWorkSchedualDay.WorkBegTime = DirectCast(grdShift2.Rows(e.RowIndex).FindControl("datWorkBegTime"), RadTimePicker).SelectedDate
    '    eWorkSchedualDay.WorkEndTime = DirectCast(grdShift2.Rows(e.RowIndex).FindControl("datWorkEndTime"), RadTimePicker).SelectedDate
    '    eWorkSchedualDay.VersionNo = grdShift2.DataKeys(e.RowIndex)("VersionNo")

    '    If Not moWorkScheduleDay.Update(eWorkSchedualDay.WorkScheduleId,
    '                             2,
    '                             eWorkSchedualDay.WeekDayNo,
    '                             eWorkSchedualDay.IsWeekendDay,
    '                             eWorkSchedualDay.WorkBegTime,
    '                             eWorkSchedualDay.WorkEndTime,
    '                             Session("AccountEmployeeId"),
    '                             eWorkSchedualDay.VersionNo) Then
    '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moWorkScheduleDay.InfoMessage), True)
    '    End If


    '    grdShift2.EditIndex = -1
    '    GridViewDataBinding()
    'End Sub

    'Protected Sub grdShift3_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles grdShift3.RowCancelingEdit
    '    grdShift3.EditIndex = -1
    '    GridViewDataBinding()
    'End Sub

    Protected Sub grdShift3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdShift3.RowDataBound
        If (eWorkSchedule.IsPeriodic = False And eWorkSchedule.PeriodLength = 7) Or (eWorkSchedule.IsPeriodic = True And eWorkSchedule.PeriodLength Mod 7 <> 0) Then
            Return
        End If

        Dim DayDate As Date
        If FormView1.CurrentMode = FormViewMode.Edit Then
            DayDate = DirectCast(FormView1.FindControl("datPeriodStartDate"), GeneralControls_MyDate).SelectedDate
        Else
            Return
        End If


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dr As DataRow = (CType(e.Row.DataItem, DataRowView)).Row
            Dim dayNo As String = e.Row.Cells(0).Text
            myCulture = Globalization.CultureInfo.CurrentCulture
            dayOfWeek = myCulture.Calendar.GetDayOfWeek(DayDate.AddDays(e.Row.RowIndex))
            dayName = myCulture.DateTimeFormat.GetDayName(dayOfWeek)
            e.Row.Cells(1).Text = dayName
        End If
    End Sub

    'Protected Sub grdShift3_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles grdShift3.RowEditing
    '    grdShift3.EditIndex = e.NewEditIndex
    '    GridViewDataBinding()
    'End Sub

    'Protected Sub grdShift3_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles grdShift3.RowUpdating

    '    Dim eWorkSchedualDay As BOEWorkScheduleDay = New BOEWorkScheduleDay
    '    eWorkSchedualDay.WorkScheduleId = DirectCast(FormView1.FindControl("txtWorkScheduleId"), TextBox).Text
    '    eWorkSchedualDay.WeekDayNo = DirectCast(grdShift3.Rows(e.RowIndex).FindControl("txtWeekDayNo"), Label).Text
    '    eWorkSchedualDay.IsWeekendDay = DirectCast(grdShift3.Rows(e.RowIndex).FindControl("chkIsWeekendDay"), CheckBox).Checked
    '    eWorkSchedualDay.WorkBegTime = DirectCast(grdShift3.Rows(e.RowIndex).FindControl("datWorkBegTime"), RadTimePicker).SelectedDate
    '    eWorkSchedualDay.WorkEndTime = DirectCast(grdShift3.Rows(e.RowIndex).FindControl("datWorkEndTime"), RadTimePicker).SelectedDate
    '    eWorkSchedualDay.VersionNo = grdShift3.DataKeys(e.RowIndex)("VersionNo")

    '    If Not moWorkScheduleDay.Update(eWorkSchedualDay.WorkScheduleId,
    '                             3,
    '                             eWorkSchedualDay.WeekDayNo,
    '                             eWorkSchedualDay.IsWeekendDay,
    '                             eWorkSchedualDay.WorkBegTime,
    '                             eWorkSchedualDay.WorkEndTime,
    '                             Session("AccountEmployeeId"),
    '                             eWorkSchedualDay.VersionNo) Then
    '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(moWorkScheduleDay.InfoMessage), True)
    '    End If


    '    grdShift3.EditIndex = -1
    '    GridViewDataBinding()
    'End Sub

    'Protected Sub grdShift3_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles grdShift3.RowCancelingEdit
    '    grdShift3.EditIndex = -1
    '    GridViewDataBinding()
    'End Sub

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
        Else

            Dim PrevSaturday As DateTime = DateTime.Now
            While PrevSaturday.DayOfWeek <> dayOfWeek.Saturday
                PrevSaturday = PrevSaturday.AddDays(-1)
            End While
            DirectCast(FormView1.FindControl("datPeriodStartDate"), GeneralControls_MyDate).SelectedDate = PrevSaturday
            GridViewDataBinding(grdShift1, PrevSaturday, 7)
        End If

    End Sub

#End Region

#Region "Other Controls Events handling"

    Protected Sub btnAddWorkSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddWorkSchedule.Click
        Me.FormView1.Visible = True
        Me.FormView1.DefaultMode = FormViewMode.Insert
        grdShift1.Visible = True
        Me.GridView2.Visible = False
        btnAddWorkSchedule.Visible = False
        'BindDummyWorkScheduleDaysas(grdShift1)
    End Sub

    Protected Sub controlChange()
        Try
            Dim SelectedDate As Date = DirectCast(FormView1.FindControl("datPeriodStartDate"), GeneralControls_MyDate).SelectedDate
            Dim PeriodLength As Integer = DirectCast(FormView1.FindControl("numPeriodLength"), RadNumericTextBox).Value
            Dim ddl As DropDownList = DirectCast(FormView1.FindControl("ddlShiftsCount"), DropDownList)

            If ddl.SelectedValue = 1 Then
                grdShift2.Visible = False
                grdShift3.Visible = False
                grdShift2.DataSource = Nothing
                grdShift2.DataBind()
                grdShift3.DataSource = Nothing
                grdShift3.DataBind()
                grdShift1.Caption = "√Êﬁ«  «·œÊ«„"

                GridViewDataBinding(grdShift1, SelectedDate, PeriodLength)


            ElseIf ddl.SelectedValue = 2 Then
                grdShift2.Visible = True
                grdShift3.Visible = True

                grdShift1.Caption = "«·› —… «·√Ê·Ï"
                grdShift2.Caption = "«·› —… «·À«‰Ì…"

                GridViewDataBinding(grdShift1, SelectedDate, PeriodLength)
                GridViewDataBinding(grdShift2, SelectedDate, PeriodLength)


                grdShift3.DataSource = Nothing
                grdShift3.DataBind()
            ElseIf ddl.SelectedValue = 3 Then
                grdShift2.Visible = True
                grdShift3.Visible = True

                grdShift1.Caption = "«·› —… «·√Ê·Ï"
                grdShift2.Caption = "«·› —… «·À«‰Ì…"
                grdShift3.Caption = "«·› —… «·À«·À…"

                GridViewDataBinding(grdShift1, SelectedDate, PeriodLength)
                GridViewDataBinding(grdShift2, SelectedDate, PeriodLength)
                GridViewDataBinding(grdShift3, SelectedDate, PeriodLength)

            End If
            
        Catch ex As Exception

        End Try
       


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
                       eWorkSchedule.ShiftsCount,
                       eWorkSchedule.IsPeriodic,
                       eWorkSchedule.PeriodLength,
                       eWorkSchedule.FirstPeriodStartDate,
                       Session("AccountEmployeeId"),
                       moVersionNo,
                       eWorkSchedule.WorkScheduleNameEN) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEGatepass
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtGatepassId.Text)), BOEGatepass)
                'Call FormShow(UpdatedObject)

                SaveWorkScheduledays(eWorkSchedule.WorkScheduleId, grdShift1, 1)
                SaveWorkScheduledays(eWorkSchedule.WorkScheduleId, grdShift2, 2)
                SaveWorkScheduledays(eWorkSchedule.WorkScheduleId, grdShift3, 3)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button2"), Button).Text = "—ÃÊ⁄"

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
                       eWorkSchedule.ShiftsCount,
                       eWorkSchedule.IsPeriodic,
                       eWorkSchedule.PeriodLength,
                       eWorkSchedule.FirstPeriodStartDate,
                       Session("AccountEmployeeId"),
                       eWorkSchedule.WorkScheduleNameEN) Then

                boolSeccessed = True

                SaveWorkScheduledays(.Identity, grdShift1, 1)
                SaveWorkScheduledays(.Identity, grdShift2, 2)
                SaveWorkScheduledays(.Identity, grdShift3, 3)

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

    Private Sub SaveWorkScheduledays(ByVal WorkScheduleId As Integer, ByVal grd As GridView, ByVal ShiftNo As Integer)

        Dim eWorkSchedualday As BOEWorkScheduleDay = New BOEWorkScheduleDay

        For Each row As GridViewRow In grd.Rows
            eWorkSchedualday.WorkScheduleId = WorkScheduleId
            eWorkSchedualday.WeekDayNo = DirectCast(row.FindControl("txtWeekDayNo"), Label).Text
            eWorkSchedualday.IsWeekendDay = DirectCast(row.FindControl("chkIsWeekendDay"), CheckBox).Checked
            eWorkSchedualday.WorkBegTime = DirectCast(row.FindControl("datWorkBegTime"), RadTimePicker).SelectedDate
            eWorkSchedualday.WorkEndTime = DirectCast(row.FindControl("datWorkEndTime"), RadTimePicker).SelectedDate

            moWorkScheduleDay.Add(eWorkSchedualday.WorkScheduleId,
                                  ShiftNo,
                                  eWorkSchedualday.WeekDayNo,
                                  eWorkSchedualday.IsWeekendDay,
                                  eWorkSchedualday.WorkBegTime,
                                  eWorkSchedualday.WorkEndTime,
                                  Session("AccountEmployeeId"))
        Next

       
    End Sub

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
                .WorkScheduleNameEN = DirectCast(FormView1.FindControl("txtWorkScheduleNameEN"), TextBox).Text
                .ScheduleBegDate = DirectCast(FormView1.FindControl("datStartDate"), GeneralControls_MyDate).SelectedDate
                .ScheduleEndDate = DirectCast(FormView1.FindControl("datEnddate"), GeneralControls_MyDate).SelectedDate
                .IsEffectiveDuringHoliday = True
                .WorkScheduleTypeId = 1
                .DepartmentId = 1
                .IsPolicyApplied = True
                .PolicyId = DirectCast(FormView1.FindControl("ddlPolicy"), DropDownList).SelectedValue

                .ShiftsCount = DirectCast(FormView1.FindControl("ddlShiftsCount"), DropDownList).SelectedValue
                .IsPeriodic = DirectCast(FormView1.FindControl("ddlIsPeriodic"), DropDownList).SelectedValue
                .PeriodLength = DirectCast(FormView1.FindControl("numPeriodLength"), RadNumericTextBox).Value
                .FirstPeriodStartDate = DirectCast(FormView1.FindControl("datPeriodStartDate"), GeneralControls_MyDate).SelectedDate

                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eWorkSchedule
    End Function

#End Region

    Public Sub BindGridView()
        Dim genGridView As GridView = New GridView()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("FirstName")
        dt.Columns.Add("LastName")
        dt.Columns.Add("Age", GetType(System.Int32))
        dt.Columns.Add("Extend")
        Dim oItem As DataRow = dt.NewRow()
        oItem(0) = "Shawpnendu"
        oItem(1) = "Bikash"
        oItem(2) = 32
        oItem(3) = "Ex1"
        dt.Rows.Add(oItem)
        
        Dim extendColumn As BoundField = New BoundField()
        Dim templateField As TemplateField = New TemplateField()
        templateField.HeaderText = "Header 1"
        templateField.ItemTemplate = New CreateItemTemplate(DataControlRowType.DataRow)
        templateField.HeaderTemplate = New CreateItemTemplate(DataControlRowType.Header)
        templateField.EditItemTemplate = New CreateItemTemplate(DataControlRowType.DataRow)
        genGridView.Columns.Add(templateField)
        genGridView.DataSource = dt
        genGridView.DataBind()
        'Panel1.Controls.Add(genGridView)
    End Sub

    Public Sub BindGridView1()
        Dim gdDynamicGrid As GridView = New GridView()
        Dim tfObject As TemplateField = New TemplateField()
        tfObject.HeaderText = "Header Text"
        tfObject.HeaderStyle.Width = Unit.Percentage(30)

        tfObject.ItemTemplate = New CreateItemTemplate(ListItemType.Item)
        gdDynamicGrid.Columns.Add(tfObject)

        Dim PlaceHolder1 As New PlaceHolder()
        PlaceHolder1.Controls.Add(gdDynamicGrid)
    End Sub

End Class

Public Class CreateItemTemplate
    Implements ITemplate

    Private myListItemType As ListItemType

    Public Sub New()
    End Sub

    Public Sub New(ByVal Item As ListItemType)
        myListItemType = Item
    End Sub

    Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) Implements ITemplate.InstantiateIn
        If myListItemType = ListItemType.Item Then
            Dim txtCashCheque As TextBox = New TextBox()
            container.Controls.Add(txtCashCheque)
        End If
    End Sub

End Class