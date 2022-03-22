<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="ProcessAttendance.aspx.vb" Inherits="Attendance_ManualLog"  title="معالجة الحضور والانصراف" meta:resourcekey="PageResource1"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/ctlProcessAttendance.ascx" TagName="ctlProcessAttendance" TagPrefix="uc1" %>

<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
      <uc1:ctlProcessAttendance ID="ctlProcessAttendance" runat="server" />
</asp:Content>

