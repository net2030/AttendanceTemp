<%@ Page Title="العطل الرسمية" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="MessageText.aspx.vb" Inherits="AccountAdmin_WorkException" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/ctlMessagText.ascx" TagName="ctlMessagText"
    TagPrefix="uc1" %>
<asp:Content ContentID="C" ContentPlaceHolderID="C" Runat="Server">
    
    <uc1:ctlMessagText runat="server" />
    
  
</asp:Content>
