<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="RegistrationComplete.aspx.vb" Inherits="Home_Default2" title="Account Created Successfully" %>




<tr class="xFormView" >
	    <td valign=top width="100%">
<table border=0 class="xFormView" width="100%">
        <tr><td>
<table border=0 class="xFormView">
    <tr>
        <td><h3> <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:TimeLive.Web, Your Web TimeSheet Account is Ready%> " /></h3>
        </td>
    </tr>
     <tr >
        <td><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx" CssClass="FormViewSubHeader"><img src="../Images/iconscreens.gif" /></asp:HyperLink>
        </td>
     </tr>
     <tr>
        <td class=FormViewSubHeader>
        <asp:Literal ID="Literal1" runat="server" Text=""/><br /><br />
        <br />
        <asp:Literal ID="Literal4" runat="server" Text="" /><br />
        <asp:Literal ID="Literal5" runat="server" Text="" />
        <br /><br />
        <asp:Literal ID="Literal6" runat="server" Text="Toll Free: 1-888-666-8154" />
        <br /><br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" CssClass="FormViewSubHeader"><asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:TimeLive.Web, Goto Login page%> " /></asp:HyperLink><br />
        <br />
        </td>
     </tr>
</table>
</td></tr></table>
	 </td>
</tr> 

