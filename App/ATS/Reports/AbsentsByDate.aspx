<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="AbsentsByDate.aspx.vb" Inherits="Employee_AttendanceByEmployee" title="TimeLive - ÊÞÑíÑ ÇáÛíÇÈ" meta:resourcekey="PageResource1" %>



<%@ Register Src="Controls/ctlAbsentsByDate.ascx" TagName="ctlAbsentsByDate"
    TagPrefix="uc1" %>
<asp:Content Content ID="Content1" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlAbsentsByDate id="ctlAbsentsByDate1" runat="server">
    </uc1:ctlAbsentsByDate>
</asp:Content>

