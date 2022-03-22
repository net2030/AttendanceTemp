<%@ Page Language="VB" MasterPageFile="~/Mobile/Masters/MasterPageMobileEmployee.master" AutoEventWireup="false" CodeFile="GatepassForm.aspx.vb" Inherits="Mobile_GatepassForm" title="Period View" %>

<%@ Register Src="Controls/ctlGatepassForm.ascx" TagName="ctlGatepassForm"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ctlGatepassForm ID="ctlGatepassForm1"
        runat="server" />
</asp:Content>

