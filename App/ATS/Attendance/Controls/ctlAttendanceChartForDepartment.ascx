<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAttendanceChartForDepartment.ascx.vb" Inherits="ctlAttendanceChartForDepartment" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<br />
<div Align="Center">
<%--    <asp:Chart ID="Chart1" runat="server" Width="1000px" Height="700px" BackColor="Transparent"
            Palette="SeaGreen">
            <Series>
                <asp:Series Name="Series1" XValueMember="key1" YValueMembers="value" IsVisibleInLegend="true"
                    ChartType="Pie" Color="255, 255, 128" CustomProperties="PieLabelStyle=Disabled" IsValueShownAsLabel="True">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                    <AxisX LineColor="DarkGray">
                        <MajorGrid LineColor="LightGray" />
                    </AxisX>
                    <AxisY LineColor="DarkGray">
                        <MajorGrid LineColor="LightGray" />
                    </AxisY>
                    <Area3DStyle Enable3D="True" WallWidth="5" LightStyle="Realistic"></Area3DStyle>
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend>
                </asp:Legend>
            </Legends>
        </asp:Chart>--%>

    <asp:Chart ID="Chart1" Width="1000px" Height="700px" BackColor="Transparent"
    Visible="true" ImageType="Png" runat="server"  PaletteCustomColors="128, 255, 128; 255, 128, 0">
    <Titles>
        <asp:Title TextStyle="Frame">
        </asp:Title>
    </Titles>
    <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
            LegendStyle="Column">
        </asp:Legend>
    </Legends>
    <Series>
        <asp:Series Name="Series1" ChartType="Pie" YValuesPerPoint="2">
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea IsSameFontSizeForAllAxes="true" BorderWidth="0" Name="ChartArea1">
            <Area3DStyle Enable3D="true" />
        </asp:ChartArea>
    </ChartAreas>
</asp:Chart>

</div>



