<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlDevicesChart.ascx.vb" Inherits="AccountAdmin_Controls_ctlDevicesChart" %>


<asp:Chart ID="cTestChart" runat="server" Height="521px" Palette="Pastel" Width="1200px">
    <Series>
        <asp:Series BackImageAlignment="Top" ChartType="StackedColumn" CustomProperties="PointWidth=0.8" IsValueShownAsLabel="True" Name="Series1" Font="Arial Narrow, 7.8pt" IsVisibleInLegend="False">
            
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1">
        </asp:ChartArea>
    </ChartAreas>
</asp:Chart>


