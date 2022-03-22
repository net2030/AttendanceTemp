Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlAccountpolicy
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moPolicy As BOPolicy = New BOPolicy
    Dim ePolicy As New BOEPolicy
    Dim moVersionNo As Byte()
#End Region

#Region "Load"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            GridViewBinding()
            ATS.BO.BOPagePermission.SetPagePermission(167, Me.GridView2, Me.FormView1, "Button1", 2, 3)
        End If

    End Sub

#End Region

#Region "Data Binding"
    Private Sub GridViewBinding()
        'GridView2.DataSource = moPolicy.GetAllPoliciesDataset(1, 100)
        Me.GridView2.DataBind()
    End Sub
#End Region

#Region "common Methods"

    Private Function FillObject() As BOEPolicy
        Dim ePolicy As New BOEPolicy

        Try
            With ePolicy
                If DirectCast(FormView1.FindControl("txtPolicyId"), TextBox).Text.Trim <> "" Then
                    .PolicyId = DirectCast(FormView1.FindControl("txtPolicyId"), TextBox).Text
                End If

                .PolicyName = DirectCast(FormView1.FindControl("txtPolicyName"), TextBox).Text
                .PolicyNameEN = DirectCast(FormView1.FindControl("txtPolicyNameEN"), TextBox).Text
                .DepartmentId = 1
                .EarlyInMinutes = DirectCast(FormView1.FindControl("txtEarlyInMinutes"), TextBox).Text
                .LateInMinutes = DirectCast(FormView1.FindControl("txtLateInMinutes"), TextBox).Text
                .LateOutMinutes = DirectCast(FormView1.FindControl("txtLateOutMinutes"), TextBox).Text
                .EarlyOutMinutes = DirectCast(FormView1.FindControl("txtEarlyOutMinutes"), TextBox).Text
                .MarkObsentDuration = DirectCast(FormView1.FindControl("txtMarkObsentDuration"), TextBox).Text
                .LateLimitPerMonthMinutes = DirectCast(FormView1.FindControl("txtLateLimitPerMonthMinutes"), TextBox).Text
                .UpdatedUserAccountId = Session("AccountEmployeeId")
                
                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return ePolicy
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

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        Dim ePolicy As BOEPolicy = FillObject()

        moVersionNo = Session("VersionNo")

        With moPolicy
            If .Update(ePolicy.PolicyId,
                       ePolicy.PolicyName,
                       ePolicy.PolicyNameEN,
                       ePolicy.MarkObsentDuration,
                       ePolicy.LateInMinutes,
                       ePolicy.LateOutMinutes,
                       ePolicy.EarlyInMinutes,
                       ePolicy.EarlyOutMinutes,
                       ePolicy.LateLimitPerMonthMinutes,
                       ePolicy.DepartmentId,
                       ePolicy.UpdatedUserAccountId,
                       moVersionNo) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEPolicy
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtPolicyId.Text)), BOEPolicy)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button1"), Button).Enabled = False
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
        Dim ePolicy As BOEPolicy = FillObject()



        With moPolicy
            If .Add(ePolicy.PolicyName,
                       ePolicy.PolicyNameEN,
                       ePolicy.MarkObsentDuration,
                       ePolicy.LateInMinutes,
                       ePolicy.LateOutMinutes,
                       ePolicy.EarlyInMinutes,
                       ePolicy.EarlyOutMinutes,
                       ePolicy.LateLimitPerMonthMinutes,
                       ePolicy.DepartmentId,
                       ePolicy.UpdatedUserAccountId) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEPolicy
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtPolicyId.Text)), BOEPolicy)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button1"), Button).Enabled = False
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
        moPolicy.Delete(id)
        Return moPolicy.InfoMessage
    End Function
#End Region

#Region "Gridview"

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("PolicyId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView2.DataSource = Nothing
            GridView2.DataSource = moPolicy.GetAllPoliciesDataset(1, 100)
            GridView2.DataBind()

        End If
    End Sub

    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing
        Try
            Dim x As Integer = e.NewEditIndex
            Dim row As GridViewRow = GridView2.Rows(x)
            Dim PolicyId As String = row.Cells(0).Text
            Dim PolicisList As New List(Of BOEPolicy)()
            ePolicy = moPolicy.Find(PolicyId)
            PolicisList.Add(ePolicy)
            FormView1.DataSource = PolicisList
            Me.FormView1.ChangeMode(FormViewMode.Edit)
            Me.FormView1.DataBind()
            Me.GridView2.Visible = False
            FormView1.Visible = True
            btnAddPolicy.Visible = False


        Catch ex As Exception
            Dim message As String = ex.Message
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(message), True)
        End Try

    End Sub

#End Region

#Region "Other Controls events"
    Protected Sub btnAddPolicy_Click(sender As Object, e As EventArgs) Handles btnAddPolicy.Click
        Me.FormView1.Visible = True
        Me.FormView1.DefaultMode = FormViewMode.Insert
        Me.GridView2.Visible = False
        btnAddPolicy.Visible = False
    End Sub
#End Region

#Region "Form Events"
    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles FormView1.DataBound
        If FormView1.CurrentMode = FormViewMode.Edit Then
            If Not IsNothing(ePolicy.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", ePolicy.VersionNo)
            End If
        End If
    End Sub

#End Region

  
   
End Class
