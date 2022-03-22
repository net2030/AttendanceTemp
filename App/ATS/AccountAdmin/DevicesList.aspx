<%@ Page Title="قائمة الأجهزة" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="DevicesList.aspx.vb" Inherits="AccountAdmin_Devices" meta:resourcekey="PageResource1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/ctlDevicesList.ascx" TagName="ctlDevicesList" TagPrefix="uc1" %>
<%@ Register Src="Controls/ctlDevicesChart.ascx" TagName="ctlDevicesChart" TagPrefix="uc1" %>
<%--<%@ Register Src="~/AccountAdmin/Controls/ctlLogsImport.ascx" TagPrefix="uc1" TagName="ctlLogsImport" %>--%>


<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
     

    <telerik:RadTabStrip runat="server"
                             ID="RadTabStrip1"
                             Align="Justify"
                             AutoPostBack="True"
                             MultiPageID="RadMultiPage1"
                             SelectedIndex="0"  CausesValidation="false">
            <Tabs>

                 <telerik:RadTab Text="حالة الاتصال" Width="33%" meta:resourcekey="ListResource1"></telerik:RadTab>
                <telerik:RadTab Text="مخطط بياني" Width="33%" meta:resourcekey="ChartResource1"></telerik:RadTab>
              <%--  <telerik:RadTab Text="إستيراد البيانات" Width="34%" meta:resourcekey="ImportResource1"></telerik:RadTab>--%>
            </Tabs>
        </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CausesValidation="false">

         <telerik:RadPageView runat="server"  ID="RadPageView2">
              <uc1:ctlDevicesList ID="ctlDevicesList1" runat="server" />
            </telerik:RadPageView>

            <telerik:RadPageView runat="server"   ID="RadPageView1" CausesValidation="false">
                 <uc1:ctlDevicesChart ID="ctlDevicesChart1" runat="server" />
            </telerik:RadPageView>

   <%--     <telerik:RadPageView runat="server"   ID="RadPageView3" CausesValidation="false" >
            <uc1:ctlLogsImport runat="server" ID="ctlLogsImport" />
            </telerik:RadPageView>--%>
    </telerik:RadMultiPage>
   </asp:Content>
