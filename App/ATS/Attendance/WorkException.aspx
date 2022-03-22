<%@ Page Title="إستثناء الدوام" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="WorkException.aspx.vb" Inherits="AccountAdmin_WorkException" meta:resourcekey="PageResource1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/ctlMyWorkException.ascx" TagName="ctlMyWorkException" TagPrefix="uc1" %>
<%@ Register Src="Controls/ctlWorkException.ascx" TagName="ctlWorkException" TagPrefix="uc1" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>


<asp:Content ContentID="C" ContentPlaceHolderID="C" Runat="Server">
     <telerik:RadTabStrip runat="server"
                             ID="RadTabStrip1"
                             Align="Justify"
                             AutoPostBack="True"
                             MultiPageID="RadMultiPage1"
                             SelectedIndex="0"
                             causesvalidation="false">
            <Tabs>

                 <telerik:RadTab Text="إستثناءاتي"  Width="50%" meta:resourcekey="MeResource1"></telerik:RadTab>
                <telerik:RadTab Text="إستثناءات الموظفين" Width="50%" meta:resourcekey="EmpResource1"></telerik:RadTab>
               
            </Tabs>
        </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" causesvalidation="false">

         <telerik:RadPageView runat="server"  ID="RadPageView2">
               <uc1:ctlMyWorkException ID="ctlMyWorkException1" runat="server" Visible="true" />
            </telerik:RadPageView>
       
            <telerik:RadPageView runat="server"  ID="RadPageView1">
                <uc1:ctlWorkException ID="ctlWorkException1" runat="server" Visible="true" />
            </telerik:RadPageView>
     
       
    </telerik:RadMultiPage>

   <%--  <uc1:ctlWorkException ID="ctlWorkException1" runat="server" Visible="true" />--%>
    
</asp:Content>
