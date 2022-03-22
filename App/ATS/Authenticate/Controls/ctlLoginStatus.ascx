<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlLoginStatus.ascx.vb" Inherits="Employee_Controls_ctlLoginStatus" EnableViewState="false" %>
        <%@ Register Src="~/Menus/Controls/ctlSiteMapPath.ascx" TagName="ctlSiteMapPath" TagPrefix="uc4" %>        
            <asp:LoginStatus ID="S" runat="server"
    LogoutText="ÊÓÌíá ÎÑæÌ"  LogoutAction="Redirect" 
    LogoutPageUrl="~/Authenticate/DoLogout.aspx"
    Font-Underline="True" ForeColor="#BFBFBF" 
    LoginImageUrl="~/Images/logout.png" LogoutImageUrl="~/Images/logout.png" ToolTip="Logout"
    Width="25px"/>