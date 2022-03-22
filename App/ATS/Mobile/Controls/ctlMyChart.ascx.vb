Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports System.Web.UI.DataVisualization.Charting
Imports ATS.BO.Framework

Partial Class Task_Controls_ctlMyChart
    Inherits System.Web.UI.UserControl
    Private moAttendanceLog As New BOAttendanceLog

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ChartBinding()
    End Sub

    Private Sub ChartBinding()
        Try
            SetColor()
            Chart1.DataSource = moAttendanceLog.GetAttendanceChartData(Session("AccountEmployeeId"), DateAdd(DateInterval.Month, -1, Now.Date), Now.Date).Tables(0)
            Chart1.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Public Sub SetColor()
        Dim myPalette As Color() = New Color(6) {Color.PaleGreen, Color.Lime, Color.Blue, Color.Yellow, Color.OrangeRed, Color.Red, Color.Orange}
        Me.Chart1.Palette = ChartColorPalette.None
        Me.Chart1.PaletteCustomColors = myPalette
    End Sub

End Class
