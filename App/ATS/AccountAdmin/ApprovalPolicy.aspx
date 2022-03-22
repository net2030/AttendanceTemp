<%@ Page Title="Leave Policy" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="ApprovalPolicy.aspx.vb" Inherits="AccountAdmin_ApprovalPolicy" %>

<%@ Register Src="~/AccountAdmin/Controls/ApprovalPolicy.ascx" TagPrefix="uc1" TagName="ApprovalPolicy" %>


<asp:Content ContentID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ApprovalPolicy runat="server" ID="ApprovalPolicy" />
</asp:Content>
