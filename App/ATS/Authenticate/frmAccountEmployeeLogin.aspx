<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageBase.master" AutoEventWireup="false" CodeFile="frmAccountEmployeeLogin.aspx.vb" Inherits="Authenticate_frmAccountEmployeeLogin" title="TimeLive - ÊÓÌíá ÇáÏÎæá" %>

<%@ Register Src="Controls/ctlLogin.ascx" TagName="ctlLogin" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftMenu" Runat="Server">
</asp:Content>
<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlLogin ID="CtlLogin1" runat="server" />
</asp:Content>

