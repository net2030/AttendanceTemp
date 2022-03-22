<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccountDepartments.aspx.vb" Inherits="WebForms_frmAccountDepartments" MasterPageFile="~/Masters/MasterPageEmployee.master" Theme="SkinFile" Title="TimeLive - Departments" %>
<%@ Register Src="Controls/ctlDepartment.ascx" TagName="ctlDepartment"
    TagPrefix="uc1" %>


<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
                        <uc1:ctlDepartment ID="ctlDepartment1" runat="server" />


</asp:Content>

