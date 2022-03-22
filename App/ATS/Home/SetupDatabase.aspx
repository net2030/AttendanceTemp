<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="SetupDatabase.aspx.vb" Inherits="Home_SetupDatabase" title="TimeLive - Setup Database" %>

<%@ Register Src="Controls/ctlSetupDataBase.ascx" TagName="ctlSetupDataBase" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
 <link href="../Styles.css" rel="stylesheet" type="text/css" />
 	<!-- For all browsers -->
	<link rel="stylesheet" href="../css/reset.css?v=1">
	<link rel="stylesheet" href="../css/style.css?v=1">
	<link rel="stylesheet" href="../css/colors.css?v=1">
	<link rel="stylesheet" media="print" href="../css/print.css?v=1">
	<!-- For progressively larger displays -->
	<%--<link rel="stylesheet" media="only all and (min-width: 480px)" href="../css/480.css?v=1">--%>
	<link rel="stylesheet" media="only all and (min-width: 768px)" href="../css/768.css?v=1">
<%--	<link rel="stylesheet" media="only all and (min-width: 992px)" href="../css/992.css?v=1">
	<link rel="stylesheet" media="only all and (min-width: 1200px)" href="../css/1200.css?v=1">--%>
	<!-- For Retina displays -->
	<link rel="stylesheet" media="only all and (-webkit-min-device-pixel-ratio: 1.5), only screen and (-o-min-device-pixel-ratio: 3/2), only screen and (min-device-pixel-ratio: 1.5)" href="../css/2x.css?v=1">


	<!-- Additional styles -->
	<link rel="stylesheet" href="../css/styles/form.css?v=1">
	<link rel="stylesheet" href="../css/styles/switches.css?v=1">
	<link rel="stylesheet" href="../css/styles/table.css?v=1">
</head>
<body >
    <form id="form1" runat="server" >
	<tr>
	    <td valign="top" width="100%" colspan="2"><uc1:ctlSetupDataBase ID="ctlSetupDataBase1" runat="server"/></td>
    </tr> 

        </form></body></html>

