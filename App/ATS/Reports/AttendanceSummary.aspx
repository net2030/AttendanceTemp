<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="AttendanceSummary.aspx.vb" Inherits="Employee_AttendanceByEmployee" title="TimeLive - ãáÎÕ ÇáÍÖæÑ æÇáÇäÕÑÇÝ" %>



<%@ Register Src="Controls/ctlAttendanceSummry.ascx" TagName="ctlAttendanceSummry"
    TagPrefix="uc1" %>
<asp:Content Content ID="Content1" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlAttendanceSummry id="ctlAttendanceSummry1" runat="server">
    </uc1:ctlAttendanceSummry>
</asp:Content>

