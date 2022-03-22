<%@ Page Language="VB" MasterPageFile="~/Mobile/Masters/MasterPageMobileEmployee.master" AutoEventWireup="false" CodeFile="GatePass.aspx.vb" Inherits="Mobile_GatePass" title="ÅÓÊÆÐÇäÇÊí" %>

<%@ Register Src="Controls/ctlMyGatepass.ascx" TagName="ctlMyGatepass"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ctlMyGatepass ID="ctlMyGatepass1"
        runat="server" />
</asp:Content>

