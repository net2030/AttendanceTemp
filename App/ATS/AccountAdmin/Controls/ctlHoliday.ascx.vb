Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports ATS.BO.Framework

Partial Class Attendance_Controls_ctlHoliday
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moHoliday As BOHoliday = New BOHoliday
    Dim eHoliday As New BOEHoliday
    Dim moVersionNo As Byte()
#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            GridViewDataBinding()
            FormView1.Visible = False
            ATS.BO.BOPagePermission.SetPagePermission(174, Me.GridView2, Me.FormView1, "Button1", 4, 5)
        End If

    End Sub
#End Region

#Region "Data Binding"
    Private Sub GridViewDataBinding()
        'GridView2.DataSource = moHoliday.GetHolidaysDataset()
        GridView2.DataBind()
    End Sub
#End Region

#Region "Common Methods"
    Private Function FillObject() As BOEHoliday


        Try
            With eHoliday
                If DirectCast(FormView1.FindControl("txtHolidayId"), TextBox).Text.Trim <> "" Then
                    .HolidayId = DirectCast(FormView1.FindControl("txtHolidayId"), TextBox).Text
                End If

                .HolidayName = DirectCast(FormView1.FindControl("txtHolidayName"), TextBox).Text
                .EffectiveDate = DirectCast(FormView1.FindControl("datEffectiveDate"), GeneralControls_MyDate).SelectedDate
                .DateExpire = DirectCast(FormView1.FindControl("datDateExpire"), GeneralControls_MyDate).SelectedDate
                .Note = DirectCast(FormView1.FindControl("txtNotes"), TextBox).Text
                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eHoliday
    End Function

    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function

#End Region

#Region "Controls Events"

#Region "GridView Events"

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("HolidayId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)


            GridView2.DataBind()

        End If
    End Sub

    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing
        Try


            Dim x As Integer = e.NewEditIndex
            Dim row As GridViewRow = GridView2.Rows(x)
            Dim HolidayId As String = row.Cells(0).Text
            Session("HolidayId") = HolidayId
            Dim HolidaysList As New List(Of BOEHoliday)()


            eHoliday = moHoliday.Find(HolidayId)

            HolidaysList.Add(eHoliday)

            FormView1.DataSource = HolidaysList
            Me.FormView1.ChangeMode(FormViewMode.Edit)
            Me.FormView1.DataBind()
            Me.GridView2.Visible = False
            FormView1.Visible = True
            btnAddHoliday.Visible = False
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Form Events"

    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles FormView1.DataBound
        If FormView1.CurrentMode = FormViewMode.Edit Then
            If Not IsNothing(eHoliday.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eHoliday.VersionNo)
            End If
            If Not IsNothing(eHoliday.HolidayId) Then
                System.Web.HttpContext.Current.Session.Add("HolidayId", eHoliday.HolidayId)
                ctlEmployeesHolidayException.ListBoxDataBinding(eHoliday.HolidayId)
            End If

        End If
    End Sub

#End Region

#Region "Other Controls Events"

    Protected Sub btnAddHoliday_Click(sender As Object, e As EventArgs) Handles btnAddHoliday.Click
        Me.FormView1.Visible = True
        Me.FormView1.DefaultMode = FormViewMode.Insert
        Me.GridView2.Visible = False
        Me.btnAddHoliday.Visible = False

        'CType(FormView1.FindControl("datEffectiveDate"), GeneralControls_MyDate).SelectedDate = Now.Date
        'CType(FormView1.FindControl("datDateExpire"), GeneralControls_MyDate).SelectedDate = Now.Date

    End Sub
#End Region

#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eHoliday = FillObject()

        moVersionNo = Session("VersionNo")

        With moHoliday
            If .Update(eHoliday.HolidayId,
                       eHoliday.HolidayName,
                       eHoliday.EffectiveDate,
                       eHoliday.DateExpire,
                       eHoliday.Note,
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
        eHoliday = FillObject()



        With moHoliday
            If .Add(eHoliday.HolidayName,
                       eHoliday.EffectiveDate,
                       eHoliday.DateExpire,
                       eHoliday.Note,
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
                System.Web.HttpContext.Current.Session.Add("HolidayId", .Identity)
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
        moHoliday.Delete(id)
        Return moHoliday.InfoMessage
    End Function
#End Region

End Class
