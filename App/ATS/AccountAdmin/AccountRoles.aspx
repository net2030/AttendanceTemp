<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="AccountRoles.aspx.vb" Inherits="AccountAdmin_Controls_frmAccountRoles" title="TimeLive - Roles" Theme="SkinFile" %>

<%@ Register Src="Controls/ctlAccountRoleList.ascx" TagName="ctlAccountRoleList"
    TagPrefix="uc3" %>

<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    
    <uc3:ctlAccountRoleList ID="CtlAccountRoleList1" runat="server" />
    
</asp:Content>

