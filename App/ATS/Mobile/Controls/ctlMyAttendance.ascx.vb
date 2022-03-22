Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System

Partial Class Mobile_Controls_ctlMyAttendance
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
        Dim SDate As Date = DateAdd(DateInterval.Month, -1, Now.Date)
        Dim EDate As Date = Now.Date
        Me.dsEmployeeAttendance.SelectParameters.Item("EmployeeID").DefaultValue = Session("AccountEmployeeId")
        Me.dsEmployeeAttendance.SelectParameters.Item("BegDate").DefaultValue = SDate
        Me.dsEmployeeAttendance.SelectParameters.Item("EndDate").DefaultValue = EDate
        Me.R.DataBind()
    End Sub
#End Region

   

End Class
