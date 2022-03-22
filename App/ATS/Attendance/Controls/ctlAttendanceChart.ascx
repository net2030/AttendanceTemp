<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAttendanceChart.ascx.vb" Inherits="Employee_Controls_ctlAbsentsBydate" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<script type="text/javascript">
   
		</script>
    
<asp:Chart ID="Chart1" runat="server" Width="1000px" Height="700px" BackColor="Transparent"
            Palette="SeaGreen">
            <Series>
                <asp:Series Name="Education" XValueMember="key1" YValueMembers="value" IsVisibleInLegend="true"
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
        </asp:Chart>



