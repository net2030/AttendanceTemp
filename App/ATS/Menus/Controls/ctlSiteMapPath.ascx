<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlSiteMapPath.ascx.vb" Inherits="Menus_Controls_ctlSiteMapPath" %>
<asp:SiteMapPath ID="SP" runat="server"
    PathSeparator=" : ">
    <PathSeparatorStyle ForeColor="White" Font-Size="Smaller" />
    <CurrentNodeStyle  ForeColor="White" Font-Size="Smaller" />
    <NodeStyle ForeColor="White" Font-Size="Smaller" />
    <RootNodeStyle ForeColor="White" Font-Size="Smaller"  />
</asp:SiteMapPath>