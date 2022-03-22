<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlTopMenu.ascx.vb" Inherits="Menus_Controls_ctlTopMenu" %>
<TABLE border=0 cellPadding=0 cellSpacing=0 class="TopNavigationLink">
    <TBODY>
        <TR>
            <TD style="WIDTH: 100px; HEIGHT: 27px; TEXT-ALIGN: center">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Employee/Default.aspx"></asp:HyperLink>
            </td>
            <%If Not System.Web.HttpContext.Current.Session("UserName") is nothing Then%>
            <%If Not System.Web.HttpContext.Current.User.IsInRole("SystemAdmin") Then%>
            <% If ATS.BO.BOPagePermission.IspageHasRights(28) = True Then%>
            <TD style="WIDTH: 100px; HEIGHT: 27px; TEXT-ALIGN: center">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Employee/MyTasks.aspx"></asp:HyperLink>
            </td>
            <%End If%>
            <% If ATS.BO.BOPagePermission.IspageHasRights(27) = True Then%>
            <TD style="WIDTH: 100px; HEIGHT: 27px; TEXT-ALIGN: center">
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Employee/MyProjects.aspx"></asp:HyperLink>
            </td>
            <%End If%>
            <% If ATS.BO.BOPagePermission.IspageHasRights(18) = True Then%>
            <% If Dbutilities.GetDefaultTimeEntryMode = "Day View" Then%>
            <TD style="WIDTH: 121px; HEIGHT: 27px; TEXT-ALIGN: center">
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Employee/AccountEmployeeTimeEntryDayView.aspx"></asp:HyperLink>
            </td>
            <%Else%>
            <TD style="WIDTH: 121px; HEIGHT: 27px; TEXT-ALIGN: center">
                <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Employee/AccountEmployeeTimeEntryPeriodView.aspx"></asp:HyperLink>
            </td>
            <%End If%>
            <%End If%>
            <% If ATS.BO.BOPagePermission.IspageHasRights(20) = True Then%>
            <TD style="WIDTH: 121px; HEIGHT: 27px; TEXT-ALIGN: center">
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Employee/AccountExpenseEntry.aspx"></asp:HyperLink>
            </td>
            <%End If%>
            <% If ATS.BO.BOPagePermission.IspageHasRights(124) = True Then%>
            <TD style="WIDTH: 100px; HEIGHT: 27px; TEXT-ALIGN: center">
                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/WebReport/MyReports.aspx"></asp:HyperLink>
            </td>
        <%End If%>
        <%End If%>
        <%End If%>
        </tr>
    </tbody>
</table>
