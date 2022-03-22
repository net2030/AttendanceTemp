Imports ATS.BO
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlEmployer
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moEmployer As BOEmployer = New BOEmployer
    Dim eEmployer As New BOEEmployer
    Dim moVersionNo As Byte()
#End Region

#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            GridViewDataBinding()
            'FormView1.Visible = False
        End If

    End Sub
#End Region

#Region "Data Binding"
    Private Sub GridViewDataBinding()
        'GridView1.DataSource = Nothing
        'GridView1.DataSource = moEmployer.GetList
        GridView1.DataBind()
        GridView1.EditIndex = -1
    End Sub
#End Region

#Region "Controls Events"

#Region "GridView Events"

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView1.DataKeys(index)("EmployerId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridViewDataBinding()

        End If
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        Try


            Dim x As Integer = e.NewEditIndex
            Dim row As GridViewRow = GridView1.Rows(x)
            Dim EmployerId As String = row.Cells(0).Text

            Dim EmployerList As New List(Of BOEEmployer)()


            eEmployer = moEmployer.Find(EmployerId)

            EmployerList.Add(eEmployer)

            FormView1.DataSource = EmployerList
            Me.FormView1.ChangeMode(FormViewMode.Edit)
            Me.FormView1.DataBind()
            'Me.GridView1.Visible = False
            'FormView1.Visible = True
            Me.FormView1.ChangeMode(FormViewMode.Edit)

        Catch ex As Exception

        End Try
    End Sub
#End Region


#Region "FormView Events"

    Protected Sub FormView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormView1.DataBound
        'BOPagePermission.SetPagePermission(9, Me.GridView1, Me.FormView1, "btnAdd", 2, 3)
        If FormView1.CurrentMode = FormViewMode.Edit Then
            If Not IsNothing(eEmployer.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eEmployer.VersionNo)
            End If
        End If
    End Sub

#End Region

#Region "Other Controls Events"

#End Region

#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eEmployer = FillObject()

        moVersionNo = Session("VersionNo")

        With moEmployer
            If .Update(eEmployer.EmployerId,
                       eEmployer.EmployerName,
                       Session("AccountEmployeeId"),
                       moVersionNo, eEmployer.EmployerNameEN) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEGatepass
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtGatepassId.Text)), BOEGatepass)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                Me.FormView1.ChangeMode(FormViewMode.Insert)
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
                GridViewDataBinding()
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
        eEmployer = FillObject()



        With moEmployer
            If .Add(eEmployer.EmployerName,
                       Session("AccountEmployeeId"), eEmployer.EmployerNameEN) Then

                boolSeccessed = True

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                Me.FormView1.ChangeMode(FormViewMode.Insert)
                GridViewDataBinding()
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
        moEmployer.Delete(id)
        Return moEmployer.InfoMessage
    End Function
#End Region

#Region "Common Methods"
    Private Function FillObject() As BOEEmployer
        Dim eEmployer As New BOEEmployer

        Try
            With eEmployer
                .EmployerName = DirectCast(FormView1.FindControl("AccountEmployerTextBox"), TextBox).Text
                .EmployerNameEN = DirectCast(FormView1.FindControl("AccountEmployerENTextBox"), TextBox).Text


                .UpdatedUserAccountId = Session("AccountEmployeeId")

                If DirectCast(FormView1.FindControl("txtAccountEmployerId"), TextBox).Text.Trim <> "" Then
                    .EmployerId = DirectCast(FormView1.FindControl("txtAccountEmployerId"), TextBox).Text
                End If



                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eEmployer
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
End Class
