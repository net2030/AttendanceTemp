<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DepartmentScope.aspx.vb" Inherits="AccountAdmin_DepartmentScope" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/AccountAdmin/Controls/ctlDepartmentScope.ascx" TagPrefix="uc1" TagName="ctlDepartmentScope" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../css/reset.css?v=1">
    <link rel="stylesheet" href="../css/style.css?v=1">
    <link href="../Styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" dir='<%$ Resources:MulResource, PageDirection %>'>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <uc1:ctlDepartmentScope runat="server" ID="ctlDepartmentScope" />
    </form>
</body>
</html>
