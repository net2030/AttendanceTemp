<%@ Page Language="VB" MasterPageFile="~/Mobile/Masters/MasterPageMobileEmployee.master" AutoEventWireup="false" CodeFile="VacationForm.aspx.vb" Inherits="Mobile_VacationForm" title="Period View" %>

<%@ Register Src="Controls/ctlVacationForm.ascx" TagName="ctlVacationForm"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ctlVacationForm ID="ctlVacationForm1"
        runat="server" />
</asp:Content>

