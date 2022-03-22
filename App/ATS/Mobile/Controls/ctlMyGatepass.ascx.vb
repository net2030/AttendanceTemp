Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Mobile_Controls_ctlMyGatepass1
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
        Me.dsGatePassesByEmployee.SelectParameters("EmployeeId").DefaultValue = Session("AccountEmployeeId")
        Me.R.DataBind()
    End Sub
#End Region

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Response.Redirect("GatepassForm.aspx")
    End Sub

    
End Class
