<%@ Page Title="رخصة النظام" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="LicenseActivation.aspx.vb" Inherits="AccountAdmin_LicenseActivation" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/ctlLicensing.ascx" TagName="ctlLicensing" TagPrefix="uc1" %>

<asp:Content ContentID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlLicensing ID="ctlLicensing1" runat="server" />
</asp:Content>
