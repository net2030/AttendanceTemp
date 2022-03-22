Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports ATS.BO.Framework

Partial Class Task_Controls_ctlVacationForm
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moVacation As BOVacation = New BOVacation
    Dim eVacation As New BOEVacation
    Dim moVersionNo As Byte()
#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            If Not IsNothing(Request.QueryString("VacationId")) AndAlso Not IsNothing(Request.QueryString("IsDelete")) AndAlso Request.QueryString("IsDelete") = True Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(Request.QueryString("VacationId"))), True)
                Response.Redirect("Vacations.aspx")
            End If
            BindTask()
        End If

    End Sub
    Public Sub BindTask()
        Dim VacationId As Integer
        If Not Me.Request.QueryString("VacationId") Then
            VacationId = Me.Request.QueryString("VacationId")
            eVacation = moVacation.Find(VacationId)
        End If
        If eVacation.VacationId <> 0 Then
            btnSave.Text = "Update"
            Me.txtVacationId.Text = eVacation.VacationId
            Me.ddlVacationTypeId.SelectedValue = eVacation.TypeId
            Me.datEffectiveDate.SelectedDate = eVacation.EffectiveDate
            Me.datDateExpire.SelectedDate = eVacation.DateExpire
            Me.datDateOfReturn.SelectedDate = eVacation.DateOfReturn

            Me.txtNotes.Text = eVacation.Note
            Session("VersionNo") = eVacation.VersionNo
        Else
            Me.datEffectiveDate.SelectedDate = Date.Now
            Me.datDateExpire.SelectedDate = Date.Now
            Me.datDateOfReturn.SelectedDate = DateAdd(DateInterval.Day, 1, Date.Now)
            Exit Sub
        End If

    End Sub


    Private Function FillObject() As BOEVacation
        Dim eVacation As New BOEVacation

        Try
            With eVacation
                If Me.txtVacationId.Text.Trim <> "" Then
                    .VacationId = txtVacationId.Text
                End If

                .EmployeeId = Session("AccountEmployeeId")
                .EffectiveDate = datEffectiveDate.SelectedDate
                .DateExpire = datDateExpire.SelectedDate
                .DateOfReturn = datDateOfReturn.SelectedDate

                .TypeId = ddlVacationTypeId.SelectedValue
                .Note = txtNotes.Text

            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eVacation
    End Function


    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function



    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If btnSave.Text = "Save" Then
            DataAdd()
        Else
            DataUpdate()
        End If

    End Sub

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        Dim eVacation As BOEVacation = FillObject()

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
                'Dim UpdatedObject As BOEVacation
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtVacationId.Text)), BOEVacation)
                'Call FormShow(UpdatedObject)


                Label2.ForeColor = Color.Green
                Label2.Visible = True
                Label2.Text = .InfoMessage
                Button2.Text = "Back"
                btnSave.Enabled = False
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)

            Else
                boolSeccessed = False

                Label2.ForeColor = Color.Red
                Label2.Visible = True
                Label2.Text = .InfoMessage
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataAdd() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        Dim eVacation As BOEVacation = FillObject()



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
                'Dim UpdatedObject As BOEVacation
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtVacationId.Text)), BOEVacation)
                'Call FormShow(UpdatedObject)

                Label2.ForeColor = Color.Green
                Label2.Visible = True
                Label2.Text = .InfoMessage
                Button2.Text = "Back"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
                btnSave.Enabled = False
            Else
                boolSeccessed = False
                Label2.ForeColor = Color.Red
                Label2.Visible = True
                Label2.Text = .InfoMessage
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

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("Vacations.aspx")
    End Sub
End Class
