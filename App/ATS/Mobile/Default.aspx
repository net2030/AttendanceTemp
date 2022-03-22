<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Mobile_Default" Title ="Login" %>

<%@ Register Src="Controls/ctlLogin.ascx" TagName="ctlLogin" TagPrefix="uc1" %>


<html xmlns="http://www.w3.org/1999/xhtml"  >
<head runat="server">
  <link rel="stylesheet"  href="../Resources/jquery.mobile-1.0a4.1.min.css" />
    <script type="text/javascript" src="../Resources/jquery-1.5.2.min.js"></script>
	<script type="text/javascript" src="../Resources/jquery.mobile-1.0a4.1.min.js"></script>
</head>
<body>
    <div data-role="page" data-theme="d"> 
    <form id="form1" runat="server">
    <uc1:ctlLogin ID="CtlLogin1" runat="server" />
    </form>
    </div> 
</body>
</html>
