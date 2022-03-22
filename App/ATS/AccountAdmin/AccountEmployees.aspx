<%@ Page Language="VB"  MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="AccountEmployees.aspx.vb" Inherits="AccountAdmin_frmAccountEmployees" title="TimeLive - Employees" Theme="SkinFile" %>

<%@ Register Src="Controls/ctlAccountEmployeeList.ascx" TagName="ctlAccountEmployeeForm"
    TagPrefix="uc1" %>
<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlAccountEmployeeForm ID="CtlAccountEmployeeForm1" runat="server" />
</asp:Content>

