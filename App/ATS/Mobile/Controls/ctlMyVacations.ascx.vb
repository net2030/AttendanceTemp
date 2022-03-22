Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Mobile_Controls_ctlMyVacations
    Inherits System.Web.UI.UserControl

#Region "Load"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            GridViewBinding()
        End If

    End Sub

#End Region

#Region "Data Binding"
    Private Sub GridViewBinding()
        Try
            Me.dsVacations.SelectParameters("EmployeeId").DefaultValue = Session("AccountEmployeeId")
            Me.R.DataBind()
        Catch ex As Exception

        End Try
        
    End Sub
#End Region

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Response.Redirect("VacationForm.aspx")
    End Sub


End Class
