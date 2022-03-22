<%@ Page Title="سياسات الدوام" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="Policy.aspx.vb" Inherits="AccountAdmin_WorkSchedule" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

    <%@ Register Src="Controls/ctlAccountPolicy.ascx" TagName="ctlAccountPolicy"
    TagPrefix="uc1" %>
<asp:Content ContentID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlAccountPolicy ID="ctlAccountPolicy1" runat="server" />
</asp:Content>
