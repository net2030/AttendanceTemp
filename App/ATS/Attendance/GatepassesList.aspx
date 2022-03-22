<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="GatepassesList.aspx.vb" Inherits="Employee_frmAccountWorkingDay" title="TimeLive - ÇáÅÓÊÆÐÇä" Theme ="SkinFile" meta:resourcekey="PageResource1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/ctlMyGatePass.ascx" TagName="ctlMyGatePass" TagPrefix="uc1"%>
<%@ Register Src="Controls/ctlGatePass.ascx" TagName="ctlGatePass"  TagPrefix="uc1" %>
  
<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
  

     <telerik:RadTabStrip runat="server"
                             ID="RadTabStrip1"
                             Align="Justify"
                             AutoPostBack="True"
                             MultiPageID="RadMultiPage1"
                             SelectedIndex="0" causesvalidation="false">
            <Tabs>

                 <telerik:RadTab Text="ÅÓÊÆÐÇäÇÊí" meta:resourcekey="TabResource1" Width="50%"></telerik:RadTab>
                <telerik:RadTab Text="ÅÐæäÇÊ ÇáãæÙÝíä" meta:resourcekey="TabResource2" Width="50%"></telerik:RadTab>
               
            </Tabs>
        </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" causesvalidation="false">

         <telerik:RadPageView runat="server"  ID="RadPageView2">
                <uc1:ctlMyGatePass id="ctlMyGatePass1" runat="server"/>
            </telerik:RadPageView>

            <telerik:RadPageView runat="server"   ID="RadPageView1">
                <uc1:ctlGatePass id="ctlGatePass1" runat="server"/>
            </telerik:RadPageView>

       
    </telerik:RadMultiPage>
   <%--   <uc1:ctlGatePass id="ctlGatePass1" runat="server"/>--%>
</asp:Content>

