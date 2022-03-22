Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports ATS.BO.Framework

Partial Class Task_Controls_ctlGatepassForm
    Inherits System.Web.UI.UserControl

    Dim moVersionNo As Byte()
    Dim eGatepass As New BOEGatepass
    Dim moGatePass As BOGatepass = New BOGatepass

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            If Not IsNothing(Request.QueryString("GatepassId")) AndAlso Not IsNothing(Request.QueryString("IsDelete")) AndAlso Request.QueryString("IsDelete") = True Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(Request.QueryString("GatepassId"))), True)
                Response.Redirect("GatePass.aspx")
            End If
            BindTask()
        End If

    End Sub
    Public Sub BindTask()
        Dim GatepassId As Integer
        If Not Me.Request.QueryString("GatepassId") Then
            GatepassId = Me.Request.QueryString("GatepassId")
            eGatepass = moGatePass.Find(GatepassId)
        End If
        If eGatepass.GatepassId <> 0 Then
            btnSave.Text = "Update"
            Me.txtGatepassId.Text = eGatepass.GatepassId
            Me.ddlGatepassTypeId.SelectedValue = eGatepass.GatepassTypeId
            Me.datGatepassBegDate.SelectedDate = eGatepass.GatepassBegDate
            Me.datGatepassEndDate.SelectedDate = eGatepass.GatepassEndDate
            Me.timGatepassBegTime.SelectedDate = eGatepass.GatepassBegTime
            Me.timGatepassEndTime.SelectedDate = eGatepass.GatepassEndTime
            Me.txtNotes.Text = eGatepass.Notes
            Session("VersionNo") = eGatepass.VersionNo
        Else
            Me.datGatepassBegDate.SelectedDate = Date.Now
            Me.datGatepassEndDate.SelectedDate = Date.Now
            Me.timGatepassBegTime.SelectedDate = Date.Now
            Me.timGatepassEndTime.SelectedDate = Date.Now
            Exit Sub
        End If

    End Sub


    Private Function FillObject() As BOEGatepass
        Dim eGatepass As New BOEGatepass

        Try
            With eGatepass
                If Me.txtGatepassId.Text.Trim <> "" Then
                    .GatepassId = txtGatepassId.Text
                End If

                .EmployeeId = Session("AccountEmployeeId")
                .GatepassBegDate = datGatepassBegDate.SelectedDate
                .GatepassEndDate = datGatepassEndDate.SelectedDate
                .GatepassBegTime = timGatepassBegTime.SelectedDate
                .GatepassEndTime = timGatepassEndTime.SelectedDate
                .GatepassTypeId = ddlGatepassTypeId.SelectedValue
                .Notes = txtNotes.Text

            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eGatepass
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
        moGatePass.Delete(id)
        Return moGatePass.InfoMessage
    End Function
#End Region

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("GatePass.aspx")
    End Sub
End Class
