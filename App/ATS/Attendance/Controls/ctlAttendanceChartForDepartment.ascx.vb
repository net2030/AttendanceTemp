Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.Web.UI.DataVisualization.Charting
Imports ATS.BO.Framework

Partial Class ctlAttendanceChartForDepartment
    Inherits System.Web.UI.UserControl
    Private moAttendanceLog As New BOAttendanceLog


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim myPalette As Color() = New Color(6) {Color.PaleGreen, Color.Lime, Color.Blue, Color.Yellow, Color.OrangeRed, Color.Red, Color.Orange}
        If Not IsPostBack Then
            Dim SDate, EDate As Date
            Dim id As Integer



            Try
                SDate = DateAdd(DateInterval.Month, -1, Now.Date)
                EDate = Now.Date
                id = Session("AccountEmployeeId")
                Dim c As Integer = 0

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString)
                    Using cmd As New SqlCommand("Logs.SpAttendance_GetChartDataByDepartment")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = conn
                        With cmd.Parameters
                            .Add("@EmployeeId", SqlDbType.Int).Value = id

                            .Add("@BegDate", SqlDbType.Date).Value = SDate
                            .Add("@EndDate", SqlDbType.Date).Value = EDate
                        End With
                        Dim series As Series = Chart1.Series("Series1")
                        conn.Open()
                        Dim rdr As SqlDataReader = cmd.ExecuteReader()
                        While rdr.Read()

                            If rdr("value".ToString()) = 0 Then
                                Continue While
                            End If

                            If rdr("key1".ToString()) = "≈” À‰«¡ œÊ«„" Then
                                myPalette(c) = Color.PaleGreen
                            ElseIf rdr("key1".ToString()) = "Õ«÷—" Then
                                myPalette(c) = Color.Lime
                            ElseIf rdr("key1".ToString()) = "Õ—ﬂ«  €Ì— „ﬂ „·…" Then
                                myPalette(c) = Color.Blue
                            ElseIf rdr("key1".ToString()) = "Œ—ÊÃ „»ﬂ—" Then
                                myPalette(c) = Color.Yellow
                            ElseIf rdr("key1".ToString()) = "œŒÊ· „ √Œ— ÊŒ—ÊÃ „»ﬂ—" Then
                                myPalette(c) = Color.OrangeRed
                            ElseIf rdr("key1".ToString()) = "€«∆»" Then
                                myPalette(c) = Color.Red
                            ElseIf rdr("key1".ToString()) = "„ √Œ—" Then
                                myPalette(c) = Color.Orange
                            Else
                                myPalette(c) = Nothing
                            End If

                            series.Points.AddXY(rdr("key1").ToString(), rdr("value".ToString()))
                            Chart1.Series("Series1").Label = "#PERCENT{P2}"
                            Chart1.Series("Series1").LegendText = "#VALX"
                            Chart1.Legends(0).LegendStyle = LegendStyle.Column
                            Chart1.Legends(0).Docking = Docking.Right
                            Chart1.Legends(0).Alignment = System.Drawing.StringAlignment.Center

                            c += 1
                        End While
                        rdr.Close()
                    End Using
                End Using
            Catch ex As Exception
            End Try

        End If

        Me.Chart1.Palette = ChartColorPalette.None
        Me.Chart1.PaletteCustomColors = myPalette
        Chart1.DataBind()


        'ChartBinding()
    End Sub

    Private Sub ChartBinding()

        Dim SDate, EDate As Date
        Dim id As Integer
        Try
            SDate = DateAdd(DateInterval.Month, -1, Now.Date)
            EDate = Now.Date
            id = Session("AccountEmployeeId")
            Chart1.DataSource = moAttendanceLog.GetAttendanceChartDataByDepartment(id, SDate, EDate, ATS.BO.UIUtilities.GetCurrentLanguage()).Tables(0)
            SetColor()
            Chart1.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Public Sub SetColor()

        Dim myPalette As Color() = New Color(6) {Color.PaleGreen, Color.Lime, Color.Blue, Color.Yellow, Color.OrangeRed, Color.Red, Color.Orange}
        Me.Chart1.Palette = ChartColorPalette.None
        Me.Chart1.PaletteCustomColors = myPalette
    End Sub


    Protected Sub btnView()

        ChartBinding()

    End Sub



End Class