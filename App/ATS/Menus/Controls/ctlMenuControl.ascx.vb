Imports System.Data.SqlClient
Partial Class Menus_Controls_ctlMenuControl
    Inherits System.Web.UI.UserControl



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ds As New DataSet
        Dim connStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString

        Dim conn As New SqlConnection(connStr)

        With conn
            Dim sql As String = "Select MenuID, Text, Url, Description, ParentID from Menu"
            Dim da As New SqlDataAdapter(sql, conn)
            da.Fill(ds)
            da.Dispose()
        End With

        ds.DataSetName = "Menus"
        ds.Tables(0).TableName = "Menu"
        Dim relation As New DataRelation("ParentChild", ds.Tables("Menu").Columns("MenuID"), ds.Tables("Menu").Columns("ParentID"), True)

        relation.Nested = True
        ds.Relations.Add(relation)

        xmlDataSource.Data = ds.GetXml()
        xmlDataSource.DataBind()




    End Sub
End Class
