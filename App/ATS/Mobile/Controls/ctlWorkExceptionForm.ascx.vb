Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports ATS.BO.Framework

Partial Class Task_Controls_ctlWorkExceptionForm
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moWorkException As BOWorkException = New BOWorkException
    Dim eWorkException As New BOEWorkException
    Dim moVersionNo As Byte()
#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            If Not IsNothing(Request.QueryString("WorkExceptionId")) AndAlso Not IsNothing(Request.QueryString("IsDelete")) AndAlso Request.QueryString("IsDelete") = True Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(Request.QueryString("WorkExceptionId"))), True)
                Response.Redirect("WorkExceptions.aspx")
            End If
            BindTask()
        End If

    End Sub
    Public Sub BindTask()
        Dim WorkExceptionId As Integer
        If Not Me.Request.QueryString("WorkExceptionId") Then
            WorkExceptionId = Me.Request.QueryString("WorkExceptionId")
            eWorkException = moWorkException.Find(WorkExceptionId)
        End If
        If eWorkException.WorkExceptionId <> 0 Then
            btnSave.Text = "Update"
            Me.txtWorkExceptionId.Text = eWorkException.WorkExceptionId
            Me.ddlWorkExceptionTypeId.SelectedValue = eWorkException.WorkExceptionTypeId
            Me.datWorkExceptionBegDate.SelectedDate = eWorkException.WorkExceptionBegDate
            Me.datWorkExceptionEndDate.SelectedDate = eWorkException.WorkExceptionEndDate


            Me.txtNotes.Text = eWorkException.Notes
            Session("VersionNo") = eWorkException.VersionNo
        Else
            Me.datWorkExceptionBegDate.SelectedDate = Date.Now
            Me.datWorkExceptionEndDate.SelectedDate = Date.Now

            Exit Sub
        End If

    End Sub


    Private Function FillObject() As BOEWorkException
        Dim eWorkException As New BOEWorkException

        Try
            With eWorkException
                If Me.txtWorkExceptionId.Text.Trim <> "" Then
                    .WorkExceptionId = txtWorkExceptionId.Text
                End If

                .EmployeeId = Session("AccountEmployeeId")
                .WorkExceptionBegDate = datWorkExceptionBegDate.SelectedDate
                .WorkExceptionEndDate = datWorkExceptionEndDate.SelectedDate


                .WorkExceptionTypeId = ddlWorkExceptionTypeId.SelectedValue
                .Notes = txtNotes.Text

            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eWorkException
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
        Dim eWorkException As BOEWorkException = FillObject()

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
                'Dim UpdatedObject As BOEWorkException
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtWorkExceptionId.Text)), BOEWorkException)
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
        Dim eWorkException As BOEWorkException = FillObject()



        With moWorkException
            If .Add(eWorkException.EmployeeId,
                         eWorkException.WorkExceptionTypeId,
                         eWorkException.WorkExceptionBegDate,
                         eWorkException.WorkExceptionEndDate,
                         eWorkException.DepartmentId,
                         eWorkException.Notes,
                         Session("AccountEmployeeId")) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEWorkException
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtWorkExceptionId.Text)), BOEWorkException)
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
        moWorkException.Delete(id)
        Return moWorkException.InfoMessage
    End Function
#End Region

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("WorkExceptions.aspx")
    End Sub
End Class
