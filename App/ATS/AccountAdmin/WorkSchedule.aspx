<%@ Page Async="true" Title="جداول الدوام" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="WorkSchedule.aspx.vb" Inherits="AccountAdmin_WorkSchedule" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%--<%@ Register Src="Controls/ctlAccountWorkSchedule.ascx" TagName="ctlAccountWorkSchedule" TagPrefix="uc1" %>--%>

<%@ Register Src="~/AccountAdmin/Controls/ctlAccountWorkScheduleShift.ascx" TagPrefix="uc1" TagName="ctlAccountWorkScheduleShift" %>



<%--<asp:Content ContentID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlAccountWorkTypeList ID="CtlAccountWorkTypeList1" runat="server" />
</asp:Content>--%>


<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlAccountWorkScheduleShift runat="server" id="ctlAccountWorkScheduleShift" />
<%--   <uc1:ctlAccountWorkSchedule ID="ctlAccountWorkSchedule1" runat="server" />--%>

  <%--  <telerik:RadTabStrip runat="server"
                             ID="RadTabStrip1"
                             Align="Justify"
                             AutoPostBack="True"
                             MultiPageID="RadMultiPage1"
                             SelectedIndex="0" >
            <Tabs>

                 <telerik:RadTab Text="جداول الدوام الاسبوعية" Width="50%"></telerik:RadTab>
                <telerik:RadTab Text="الورديات" Width="50%" ></telerik:RadTab>
               
            </Tabs>
        </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CausesValidation="false">

         <telerik:RadPageView runat="server"  ID="RadPageView2">
                <uc1:ctlAccountWorkSchedule ID="ctlAccountWorkSchedule" runat="server" />
            </telerik:RadPageView>

            <telerik:RadPageView runat="server"   ID="RadPageView1" CausesValidation="false">
                  <uc1:ctlAccountWorkScheduleShifts ID="ctlAccountWorkScheduleShifts1" runat="server"  />
            </telerik:RadPageView>

       
    </telerik:RadMultiPage>--%>
   </asp:Content>

