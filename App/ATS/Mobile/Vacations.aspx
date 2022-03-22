<%@ Page Language="VB" MasterPageFile="~/Mobile/Masters/MasterPageMobileEmployee.master" AutoEventWireup="false" CodeFile="Vacations.aspx.vb" Inherits="Mobile_Vacations" title="ÅÓÊÆÐÇäÇÊí" %>

<%@ Register Src="Controls/ctlMyVacations.ascx" TagName="ctlMyVacations"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ctlMyVacations ID="ctlMyVacations1"
        runat="server" />
</asp:Content>

