Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Mobile_Controls_ctlMyWorkExceptions
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
            Dim SDate, EDate As Date
            SDate = DateAdd(DateInterval.Year, -2, Now.Date)
            EDate = Now.Date
            Me.dsWorkExceptions.SelectParameters.Item("BegDate").DefaultValue = SDate
            Me.dsWorkExceptions.SelectParameters.Item("EndDate").DefaultValue = EDate
            Me.dsWorkExceptions.SelectParameters("EmployeeId").DefaultValue = Session("AccountEmployeeId")
            Me.R.DataBind()
        Catch ex As Exception

        End Try

    End Sub
#End Region

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Response.Redirect("WorkExceptionForm.aspx")
    End Sub


End Class
