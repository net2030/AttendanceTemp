Imports System.Data.SqlClient
Imports System.Net


Partial Class AccountAdmin_Controls_ctlLogsImport
    Inherits System.Web.UI.UserControl


    'Dim DevicesDT As New DataTable
    'Dim ConnectedDevicesDT As New DataTable
    Public Shared LogsDT As DataTable
    Public Shared MASLogsDT As DataTable
    Public Shared ZKLogsDT As DataTable
    Public Shared BioStarLogsDT As DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Timer1 As Timer = New Timer

        If Not Me.IsPostBack Then

            ' gridviewdatabinding()


        End If

    End Sub

    Protected Sub GetDevices()


    End Sub

    Protected Sub connect()



    End Sub

    Protected Sub GetLogs()
        LogsDT = New DataTable
        'MASLogsDT = New DataTable
        'ZKLogsDT = New DataTable
        'BioStarLogsDT = New DataTable


        'Dim MASobj As MAS.MASLogs
        'MASobj = New MAS.MASLogs()
        'MASLogsDT = MASobj.GetLogs(LogDate.SelectedDate)

        'Dim ZKobj As ZK.ZkLogs
        'ZKobj = New ZK.ZkLogs()
        'LogsDT = ZKobj.GetLogs(LogDate.SelectedDate)


        'Dim BIOobj As BioStar.BioStarLogs
        'BIOobj = New BioStar.BioStarLogs()
        'LogsDT = BIOobj.GetLogs(LogDate.SelectedDate)


        'If (MASLogsDT.Rows.Count > 0) Then
        '    LogsDT = MASLogsDT.Copy()
        'End If

        'If (ZKLogsDT.Rows.Count > 0) Then
        '    LogsDT.Merge(ZKLogsDT)
        'End If

        gridviewdatabinding()

    End Sub

    


    

    Protected Sub gridviewdatabinding()

        GridView2.DataSource = LogsDT
        GridView2.DataBind()
    End Sub
    Protected Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        GridView2.PageIndex = e.NewPageIndex
        gridviewdatabinding()
    End Sub
End Class
