Imports ATS.BO
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlVacationTypes
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moVacationType As BOVacationType = New BOVacationType
    Dim eVacationType As New BOEVacationType
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
        'GridView1.DataSource = moVacationType.GetList
        GridView1.DataBind()
        GridView1.EditIndex = -1
    End Sub
#End Region

#Region "Controls Events"

#Region "GridView Events"

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView1.DataKeys(index)("TypeId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridViewDataBinding()

        End If
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        Try


            Dim x As Integer = e.NewEditIndex
            Dim row As GridViewRow = GridView1.Rows(x)
            Dim TypeId As String = row.Cells(0).Text

            Dim VacationTypesList As New List(Of BOEVacationType)()


            eVacationType = moVacationType.Find(TypeId)

            VacationTypesList.Add(eVacationType)

            FormView1.DataSource = VacationTypesList
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
            If Not IsNothing(eVacationType.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eVacationType.VersionNo)
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
        eVacationType = FillObject()

        moVersionNo = Session("VersionNo")

        With moVacationType
            If .Update(eVacationType.TypeId,
                       eVacationType.TypeName,
                       Session("AccountEmployeeId"),
                       moVersionNo, eVacationType.TypeNameEN) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEVacation
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtVacationId.Text)), BOEVacation)
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
        eVacationType = FillObject()



        With moVacationType
            If .Add(eVacationType.TypeName,
                       Session("AccountEmployeeId"), eVacationType.TypeNameEN) Then

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
        moVacationType.Delete(id)
        Return moVacationType.InfoMessage
    End Function
#End Region

#Region "Common Methods"
    Private Function FillObject() As BOEVacationType
        Dim eVacationType As New BOEVacationType

        Try
            With eVacationType
                .TypeName = DirectCast(FormView1.FindControl("AccountVacationTypeTextBox"), TextBox).Text
                .TypeNameEN = DirectCast(FormView1.FindControl("AccountVacationTypeENTextBox"), TextBox).Text


                .UpdatedUserAccountId = Session("AccountEmployeeId")

                If DirectCast(FormView1.FindControl("txtAccountTypeId"), TextBox).Text.Trim <> "" Then
                    .TypeId = DirectCast(FormView1.FindControl("txtAccountTypeId"), TextBox).Text
                End If



                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eVacationType
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
