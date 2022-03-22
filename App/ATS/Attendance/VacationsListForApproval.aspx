<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="VacationsListForApproval.aspx.vb" Inherits="Employee_frmGatepassApproval" title="TimeLive - ÇáãæÇÝÞÉ Úáì ÇáÅÌÇÒÇÊ" Theme ="SkinFile" meta:resourcekey="PageResource1" %>
<%--<%@ Register Src="Controls/ctlVacationsApproval.ascx" TagName="ctlVacationsApproval" TagPrefix="uc1" %>--%>
<%@ Register Src="~/Attendance/Controls/ctlVacationApproval1.ascx" TagPrefix="uc1" TagName="ctlVacationApproval1" %>

<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <%--<uc1:ctlVacationsApproval id="ctlVacationsApproval1" runat="server">
    </uc1:ctlVacationsApproval>--%>
    <uc1:ctlVacationApproval1 runat="server" ID="ctlVacationApproval1" />
</asp:Content>

