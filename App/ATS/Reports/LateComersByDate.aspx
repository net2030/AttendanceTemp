<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="LateComersByDate.aspx.vb" Inherits="Employee_AttendanceByEmployee" title="TimeLive - ÊÞÑíÑ ÇáãÊÃÎÑíä" %>

<%@ Register Src="~/Reports/Controls/ctlLateByDate.ascx" TagPrefix="uc1" TagName="ctlLateByDate" %>



<asp:Content Content ID="Content1" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlLateByDate runat="server" id="ctlLateByDate" />
</asp:Content>

