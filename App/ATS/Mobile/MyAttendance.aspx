<%@ Page Language="VB" MasterPageFile="~/Mobile/Masters/MasterPageMobileEmployee.master" AutoEventWireup="false" CodeFile="MyAttendance.aspx.vb" Inherits="Mobile_MyAttendance" title="My Attendance" %>

<%@ Register Src="Controls/ctlMyAttendance.ascx" TagName="ctlMyAttendance"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ctlMyAttendance ID="ctlMyAttendance1"
        runat="server" />
</asp:Content>

