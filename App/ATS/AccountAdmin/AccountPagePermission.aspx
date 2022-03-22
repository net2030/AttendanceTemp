<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="AccountPagePermission.aspx.vb" Inherits="AccountAdmin_AccountPagePermission" title="TimeLive - Role Permissions" %>

<%@ Register Src="Controls/ctlAccountPagePermissionList.ascx" TagName="ctlAccountPagePermissionList"
    TagPrefix="uc1" %>
<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlAccountPagePermissionList ID="CtlAccountPagePermissionList1" runat="server" />
</asp:Content>

