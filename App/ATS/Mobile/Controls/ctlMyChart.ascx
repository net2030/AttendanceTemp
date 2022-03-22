<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMyChart.ascx.vb" Inherits="Task_Controls_ctlMyChart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2"Namespace="eWorld.UI" TagPrefix="ew" %>


<style type="text/css"> 
    .myChart {
        width: 100% !important;
        height: 70% !important;
    }
</style>

<script type="text/javascript">
    function resizeChart() {
        $find("<%=Chart1.ClientID%>")._chartObject.resize();
		    }
		    //Timeout is needed to handle the multiple resize event triggering in older browsers like IE7/IE8
		    var TO = false;
		    $(window).resize(function () {
		        if (TO !== false)
		            clearTimeout(TO);
		        TO = setTimeout(resizeChart, 200);
		    });
		</script>
		    <div style="width: 100%; height: 100%; border: 1px solid green;">
            
        <asp:Chart ID="Chart1" runat="server" Width="1100px" Height="800px" BackColor="Transparent" CssClass="myChart" RightToLeft="Yes">
            <Series>
                <asp:Series Name="Education" XValueMember="key1" YValueMembers="value" IsVisibleInLegend="true"
                    ChartType="Pie" Color="255, 255, 128" CustomProperties="PieLabelStyle=Disabled" IsValueShownAsLabel="True">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="false">
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
                <asp:Legend BackImageAlignment="TopRight" Docking="Bottom" Font="Arial, 20pt" IsTextAutoFit="False" IsDockedInsideChartArea="False">
                </asp:Legend>
            </Legends>
        </asp:Chart>
</div>



