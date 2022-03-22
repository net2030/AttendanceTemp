<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="EmployeesReport.aspx.vb" Inherits="Employee_EmployeesReport" title=" ÊÞÑíÑ ÈíÇäÇÊ ÇáãæÙÝíä" %>



<%@ Register Src="Controls/ctlEmployeesReport.ascx" TagName="ctlEmployeesReport"
    TagPrefix="uc1" %>
<asp:Content Content ID="Content1" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlEmployeesReport id="ctlEmployeesReport1" runat="server">
    </uc1:ctlEmployeesReport>
</asp:Content>

