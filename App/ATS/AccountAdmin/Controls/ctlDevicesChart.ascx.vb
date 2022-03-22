Imports System.Data.SqlClient
Imports System.Web.UI.DataVisualization.Charting
Imports System.Drawing
Imports System.Web.Services

Partial Class AccountAdmin_Controls_ctlDevicesChart
    Inherits System.Web.UI.UserControl
    Dim moMachineLog As New ATS.BO.Framework.BOMachineLog
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Timer1 As Timer = New Timer
        ChartBinding()
    End Sub

    Private Sub ChartBinding()

        Try
            cTestChart.DataSource = moMachineLog.GetDevicesChartData().Tables(0)

            cTestChart.Series("Series1").XValueMember = "key1"
            cTestChart.Series("Series1").YValueMembers = "Value"
            cTestChart.ChartAreas("ChartArea1").AxisX.Interval = 1
            cTestChart.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            cTestChart.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False
            'cTestChart.ChartAreas(0).Area3DStyle.Enable3D = True
            cTestChart.DataBind()
        Catch ex As Exception
        End Try

    End Sub

End Class
