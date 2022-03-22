<%@ Page Language="VB" MasterPageFile="~/Mobile/Masters/MasterPageMobileEmployee.master" AutoEventWireup="false" CodeFile="MyChart.aspx.vb" Inherits="Mobile_MyChart" title="Period View" %>
<%@ Register Src="Controls/ctlMyChart.ascx" TagName="ctlMyChart" TagPrefix="uc1" %>
<%@ Register Src="~/Mobile/Controls/ctlEmployeesChart.ascx" TagPrefix="uc1" TagName="ctlEmployeesChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ctlMyChart ID="ctlMyChart1" runat="server" />
    <uc1:ctlEmployeesChart runat="server" ID="ctlEmployeesChart" />
</asp:Content>


