<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SessionExpired.aspx.vb" Inherits="Authenticate_SessionExpired" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"  >
<head runat="server">
    <title>TimeLive - Session Expired</title>
     <link href="../Styles.css" rel="stylesheet" type="text/css" />
 	<!-- For all browsers -->
	<link rel="stylesheet" href="../css/reset.css?v=1">
	<link rel="stylesheet" href="../css/style.css?v=1">
	<link rel="stylesheet" href="../css/colors.css?v=1">
	<link rel="stylesheet" media="print" href="../css/print.css?v=1">
	<link rel="stylesheet" media="only all and (min-width: 768px)" href="../css/768.css?v=1">
	<link rel="stylesheet" media="only all and (-webkit-min-device-pixel-ratio: 1.5), only screen and (-o-min-device-pixel-ratio: 3/2), only screen and (min-device-pixel-ratio: 1.5)" href="../css/2x.css?v=1">
	<!-- Additional styles -->
	<link rel="stylesheet" href="../css/styles/form.css?v=1">
	<link rel="stylesheet" href="../css/styles/switches.css?v=1">
	<link rel="stylesheet" href="../css/styles/table.css?v=1">
</head>
<body>
    <form id="form1" runat="server" dir='<%$ Resources:MulResource, PageDirection %>'>
     <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 90%">
        <tr>
            <td style="width: 100%; vertical-align:middle" align="center">
       <table class="xFormView" width="50%" ><tr><td>
<table width="100%" class="xFormView" style="text-align:center">
    <tr>
        <th class="caption" colspan="2" style="height: 21px">
            <asp:Literal ID="Literal1" runat="server" Text="انتهت الجلسة" meta:resourcekey="Literal1Resource1" /></th>
            </tr>
<%--             <tr>
        <td align="center" colspan="2" style="height: 35px">
            <strong> 
                <asp:Literal ID="Literal2" runat="server" Text="انتهت الجلسة." meta:resourcekey="Literal2Resource1"></asp:Literal>
            </strong>

        </td>
    </tr>--%>
    <tr>
        <td align="center" colspan="2" style="height: 35px">
            <strong> <a id="LoginURL" runat="server">
                <asp:Literal ID="Literal3" runat="server" Text="اضغط هنا" meta:resourcekey="Literal3Resource1"></asp:Literal>
                     </a> 
                <asp:Literal ID="Literal4" runat="server" Text=" إذا تريد تسجيل الدخول إلى حسابك مرة اخرى" meta:resourcekey="Literal4Resource1"></asp:Literal>
                .</strong></td>
    </tr>
  </table>
</td></tr></table>
 </td>
        </tr>
    </table> 
    </form>
</body>
</html>
