<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="DailyAttendance.aspx.vb" Inherits="Employee_AttendanceByEmployee" title="TimeLive - ÊÞÑíÑ íæãí" %>



<%@ Register Src="Controls/ctlDailyAttendance.ascx" TagName="ctlDailyAttendance"
    TagPrefix="uc1" %>
<asp:Content Content ID="Content1" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlDailyAttendance id="ctlDailyAttendance1" runat="server">
    </uc1:ctlDailyAttendance>
</asp:Content>

