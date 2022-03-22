Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.Script.Services
Imports ATS.BO.Framework

Partial Class Default2
    Inherits System.Web.UI.Page
    Dim moPolicy As BOPolicy = New BOPolicy
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        GridView2.DataSource = moPolicy.GetPoliciesDataset(1, 1, 100)
        Me.GridView2.DataBind()
    End Sub
End Class
