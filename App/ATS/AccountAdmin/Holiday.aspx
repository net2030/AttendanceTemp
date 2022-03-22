<%@ Page Title="العطل الرسمية" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="Holiday.aspx.vb" Inherits="AccountAdmin_WorkException" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/ctlHoliday.ascx" TagName="ctlHoliday"
    TagPrefix="uc1" %>
<asp:Content ContentID="C" ContentPlaceHolderID="C" Runat="Server">
    
    <uc1:ctlHoliday ID="ctlHoliday1" runat="server" />
    
  
</asp:Content>
