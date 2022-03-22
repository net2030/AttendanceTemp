<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="AttendanceChartSummuryByDepartment.aspx.vb" Inherits="Employee_AttendanceChartSummuryByDepartment" title="TimeLive - ÊÞÑíÑ ÈíÇäí ãáÎÕ ÍÓÈ ÇáÞÓã" %>
<%@ Register Src="Controls/ctlAttendanceSummuryChartByDateAndDepartment.ascx" TagName="ctlAttendanceSummuryChartByDateAndDepartment" TagPrefix="uc1" %>
<asp:Content Content ID="Content1" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlAttendanceSummuryChartByDateAndDepartment id="ctlAttendanceSummuryChartByDateAndDepartment1" runat="server">
    </uc1:ctlAttendanceSummuryChartByDateAndDepartment>
</asp:Content>

