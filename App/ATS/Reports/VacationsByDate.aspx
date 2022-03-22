<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="VacationsByDate.aspx.vb" Inherits="Employee_VacationsByDate" title="TimeLive - ÊÞÑíÑ ÇáÅÌÇÒÇÊ" %>



<%@ Register Src="Controls/ctlVacationsByDate.ascx" TagName="ctlVacationsByDate"
    TagPrefix="uc1" %>
<asp:Content Content ID="Content1" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlVacationsByDate id="ctlVacationsByDate1" runat="server">
    </uc1:ctlVacationsByDate>
</asp:Content>

