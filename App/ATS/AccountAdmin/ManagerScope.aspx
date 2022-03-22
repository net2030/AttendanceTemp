<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManagerScope.aspx.vb" Inherits="AccountAdmin_ManagerScope" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="~/AccountAdmin/Controls/ctlManagerScope.ascx" TagPrefix="uc1" TagName="ctlManagerScope" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../css/reset.css?v=1">
    <link rel="stylesheet" href="../css/style.css?v=1">
    <link href="../Styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <uc1:ctlManagerScope runat="server" ID="ctlManagerScope" />
    </div>
    </form>
</body>
</html>

