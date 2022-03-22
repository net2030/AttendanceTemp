<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Employee_Default"  Theme = "SkinFile" Title="TimeLive - Home" EnableViewState="false"%>

<%--<%@ Register Src="Controls/ctlMyOverDueTimesheets.ascx" TagName="ctlMyOverDueTimesheets" TagPrefix="uc33" %>
<%@ Register Src="Controls/ctlAccountProjectTaskByDuration.ascx" TagName="ctlAccountProjectTaskByDuration" TagPrefix="uc32" %>
<%@ Register Src="Controls/ctlMyOverDueGraphChart.ascx" TagName="ctlMyOverDueGraphChart" TagPrefix="uc15" %>
<%@ Register Src="Controls/ctlDueGraphChart.ascx" TagName="ctlDueGraphChart" TagPrefix="uc14" %>
<%@ Register Src="Controls/ctlMyProjectsForGraph.ascx" TagName="ctlMyProjectsForGraph" TagPrefix="uc13" %>
<%@ Register Src="Controls/ctlMyProjectsGraphChart.ascx" TagName="ctlMyProjectsGraphChart" TagPrefix="uc12" %>--%>
<%--<%@ Register Src="Controls/ctlProjectExpenseDetail.ascx" TagName="ctlProjectExpenseDetail" TagPrefix="uc10" %>--%>
<%--<%@ Register Src="Controls/ctlAccountProjectTaskSummary.ascx" TagName="ctlAccountProjectTaskSummary" TagPrefix="uc9" %>--%>
<%--<%@ Register Src="Controls/ctlProjectTaskByCompletedPercent.ascx" TagName="ctlProjectTaskByCompletedPercent" TagPrefix="uc8" %>--%>
<%--<%@ Register Src="Controls/ctlProjectTaskbyEstimatedCost.ascx" TagName="ctlProjectTaskbyEstimatedCost" TagPrefix="uc34" %>--%>
<%--<%@ Register Src="Controls/ctlProjectTaskCount.ascx" TagName="ctlProjectTaskCount" TagPrefix="uc7" %>
<%@ Register Src="Controls/ctlMyDueTimesheets.ascx" TagName="ctlMyDueTimesheets" TagPrefix="uc5" %>
<%@ Register Src="Controls/ctlOtherRecent.ascx" TagName="ctlOtherRecent" TagPrefix="uc4" %>
<%@ Register Src="Controls/ctlAccountEmployeeTimeEntryWeekSummary.ascx" TagName="ctlAccountEmployeeTimeEntryWeekSummary"TagPrefix="uc3" %>
<%@ Register Src="Controls/ctlMyReportedTasksShort.ascx" TagName="ctlMyReportedTasksShort" TagPrefix="uc2" %>
<%@ Register Src="Controls/ctlMyTasksShort.ascx" TagName="ctlMyTasksShort" TagPrefix="uc1" %>--%>
 

<asp:Content  ID="C" ContentPlaceHolderID="C" Runat="Server">
<%--<div style="text-align:right; padding:1px 12px 1px 0">
<a style="text-decoration:underline; font-weight:bold" href="AccountEmployeeDashboard.aspx" id="DS">Dashboard Setting</a>  
</div>--%>
<asp:Table ID="DoubleTable"  runat="server" Width="100%">
</asp:Table>
<asp:Table ID="FullSizeTable"  runat="server" Width="100%">
</asp:Table>


<%--<uc1:ctlMyTasksShort ID="TS" runat="server" />
<uc2:ctlMyReportedTasksShort ID="MT" runat="server" />
<uc3:ctlAccountEmployeeTimeEntryWeekSummary ID="WS" runat="server" />
<uc4:ctlOtherRecent ID="Or" runat="server" />
<uc5:ctlMyDueTimesheets ID="ODT" runat="server" />--%>
<%--<uc9:ctlAccountProjectTaskSummary ID="PS" runat="server" />--%>
<%--<uc10:ctlProjectExpenseDetail ID="PP" runat="server" />--%>
<%--<uc13:ctlMyProjectsForGraph ID="MP" runat="server" />
<uc33:ctlMyOverDueTimesheets ID="OVDT" runat="server" />
<uc7:ctlProjectTaskCount ID="PTC" runat="server" />
<uc8:ctlProjectTaskByCompletedPercent ID="PTBG" runat="server" />--%>
<%--<uc34:ctlProjectTaskbyEstimatedCost ID="PTEC" runat="server" />--%>
<%--<uc12:ctlMyProjectsGraphChart ID="MPGC" runat="server" />
<uc14:ctlDueGraphChart ID="DGC" runat="server" />
<uc15:ctlMyOverDueGraphChart ID="MODGC" runat="server" />
<uc32:ctlAccountProjectTaskByDuration ID="APGC" runat="server" />--%>

<%--<% IF IsMyTasksShort <> False %>  --%>      
    <%--    <table  border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td style="width: 100%;" valign="top" >
                <uc1:ctlMyTasksShort ID="TS" runat="server" /><br />
            </td>
        </tr>
        <tr>
            <td style="width: 100%;" valign="top" >
                <uc2:ctlMyReportedTasksShort ID="MT" runat="server" /><br />
            </td>
        </tr>
      
        <tr>
            <td style="width: 100%;" valign="top" >
                <uc3:ctlAccountEmployeeTimeEntryWeekSummary ID="WS" runat="server" /><br />
            </td>
        </tr>
     
        <tr>
            <td style="width: 100%;" valign="top" >
                <uc4:ctlOtherRecent ID="Or" runat="server" /><br />
            </td>
        </tr>
      
        <tr>        
                <td style="width: 100%;" valign="top" >
                <uc5:ctlMyDueTimesheets ID="ODT" runat="server" /><br />
                </td>
        </tr>
     
        <tr>
                <td style="width: 100%;" valign="top" >
                <uc9:ctlProjectSummary ID="PS" runat="server" /><br />
                </td>
        </tr>
    
        <tr>
                <td style="width: 100%;" valign="top" >
                <uc10:ctlProjectProcess ID="PP" runat="server" /><br />
                </td>
        </tr>
      
        <tr>
                <td style="width: 100%;" valign="top" >
                <uc11:ctlProjectLog ID="PL" runat="server" /><br />
                </td>
        </tr>
    
        <tr>
                <td style="width: 100%;" valign="top" >
                <uc13:ctlMyProject ID="MP" runat="server" /><br />
                </td>
        </tr>
    
        <tr>
                <td style="width: 100%;" valign="top" >
                <uc33:ctlMyOverDueTimesheets ID="OVDT" runat="server" /><br />
                </td>
        </tr>
        </table>    
                  
        <table class="lighttable" border=0 cellspacing=0 cellpadding=5 width="100%">

        <tr>
                <td style="width: 50%;" valign="top" class="lighttable">
                <uc7:ctlProjectTaskCount ID="PTC" runat="server" /><br />
                </td>
                
                <td style="width: 50%;" valign="top" class="lighttable">
                <uc8:ctlProjectTaskByGraph ID="PTBG" runat="server" /><br />
                </td>

        </tr>

        <tr>
                <td style="width: 50%;" valign="top" class="lighttable">
                <uc12:ctlMyProjectsGraphChart ID="MPGC" runat="server" /><br />
                </td>
        </tr>
        <tr>
                <td style="width: 50%;" valign="top" class="lighttable">
                <uc14:ctlDueGraphChart ID="DGC" runat="server" /><br />
                </td>
        </tr>

        <tr>
                <td style="width: 50%;" valign="top" class="lighttable">
                <uc15:ctlMyOverDueGraphChart ID="MODGC" runat="server" /><br />
                </td>
        </tr>
        <tr>
                <td style="width: 50%;" valign="top" class="lighttable">
                <uc32:ctlAccountProjectGraphCount ID="APGC" runat="server" /><br />
                </td>
        </tr>
        </table>--%>
    </asp:Content>