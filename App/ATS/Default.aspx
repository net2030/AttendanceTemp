<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" title=" äÙÇã ÇáÍÖæÑ æÇáÅäÕÑÇÝ" EnableViewState="false" Theme="SkinFile" %>

<%--<%@ Register Src="Authenticate/Controls/ctlLoginWithAccountAdd.ascx" TagName="ctlLoginWithAccountAdd"
    TagPrefix="uc2" %>--%>
<%@ Register Src="~/Authenticate/Controls/ctlLogin.ascx" TagName="ctlLogin" TagPrefix="uc1" %>
<%  If System.Configuration.ConfigurationManager.AppSettings("SHOW_LOGIN_WITH_ACCOUNT_PAGE") = "Yes" Then%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%End If %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"    >
<head id="Head1" runat=server>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="OT.css" type="text/css"/>
    <meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">	
	<meta name="description" content="">
	<meta name="author" content="">

	<meta name="HandheldFriendly" content="True">
	<meta name="MobileOptimized" content="320">
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">

	<!-- For all browsers -->
	<link rel="stylesheet" href="css/reset.css?v=1">
	<link rel="stylesheet" href="css/style.css?v=1">
	<link rel="stylesheet" href="css/colors.css?v=1">
	<link rel="stylesheet" media="print" href="css/print.css?v=1">
	<!-- For progressively larger displays -->
	<link rel="stylesheet" media="only all and (min-width: 480px)" href="css/480.css?v=1">
	<link rel="stylesheet" media="only all and (min-width: 768px)" href="css/768.css?v=1">
	<link rel="stylesheet" media="only all and (min-width: 992px)" href="css/992.css?v=1">
	<link rel="stylesheet" media="only all and (min-width: 1200px)" href="css/1200.css?v=1">
	<!-- For Retina displays -->
	<link rel="stylesheet" media="only all and (-webkit-min-device-pixel-ratio: 1.5), only screen and (-o-min-device-pixel-ratio: 3/2), only screen and (min-device-pixel-ratio: 1.5)" href="css/2x.css?v=1">

	<!-- Additional styles -->
	<link rel="stylesheet" href="css/styles/form.css?v=1">
	<link rel="stylesheet" href="css/styles/switches.css?v=1">
	<link rel="stylesheet" href="css/styles/table.css?v=1">

	<!-- DataTables -->
	<link rel="stylesheet" href="js/libs/DataTables/jquery.dataTables.css?v=1">

	<!-- JavaScript at bottom except for Modernizr -->
	<script src="js/libs/modernizr.custom.js"></script>

	<!-- For Modern Browsers -->
	<link rel="shortcut icon" href="img/favicons/favicon.png">
	<!-- For everything else -->
	<link rel="shortcut icon" href="img/favicons/favicon.ico">
	<!-- For retina screens -->
	<link rel="apple-touch-icon-precomposed" sizes="114x114" href="img/favicons/apple-touch-icon-retina.png">
	<!-- For iPad 1-->
	<link rel="apple-touch-icon-precomposed" sizes="72x72" href="img/favicons/apple-touch-icon-ipad.png">
	<!-- For iPhone 3G, iPod Touch and Android -->
	<link rel="apple-touch-icon-precomposed" href="img/favicons/apple-touch-icon.png">

	<!-- iOS web-app metas -->
	<meta name="apple-mobile-web-app-capable" content="yes">
	<meta name="apple-mobile-web-app-status-bar-style" content="black">

	<!-- Startup image for web apps -->
	<link rel="apple-touch-startup-image" href="img/splash/ipad-landscape.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:landscape)">
	<link rel="apple-touch-startup-image" href="img/splash/ipad-portrait.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:portrait)">
	<link rel="apple-touch-startup-image" href="img/splash/iphone.png" media="screen and (max-device-width: 320px)">

	<!-- Microsoft clear type rendering -->
	<meta http-equiv="cleartype" content="on">

    <style type="text/css">
    #UpdatePanel1 {
      width:200px; height:100px;
      border: 1px solid gray;
    }
    #UpdateProgress1 {
      width:200px; background-color: #FFC080;
      bottom: 0%; left: 0px; position: absolute;
    }
    </style>
</head>
<body leftmargin="0" topmargin="0">
<form id="form1" runat="server">
<%  If System.Configuration.ConfigurationManager.AppSettings("SHOW_LOGIN_WITH_ACCOUNT_PAGE") <> "Yes" Then%>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 90%">
        <tr>
            <td style="width: 100%; vertical-align:middle" align="center" runat="server"  dir='<%$ Resources:MulResource, PageDirection %>' >
<uc1:ctlLogin ID="CtlLogin1" runat="server" EnableViewState="false"  />
            </td>
        </tr>
    </table> 
    <%Else%>                
  
    <%End If%>     
                                     	<!-- JavaScript at the bottom for fast page loading -->

	<!-- Scripts -->
	<script src="js/libs/jquery-1.7.2.min.js"></script>
	<script src="js/setup.js"></script>

	<!-- Template functions -->
	<script src="js/developr.input.js"></script>
	<script src="js/developr.navigable.js"></script>
	<script src="js/developr.notify.js"></script>
	<script src="js/developr.scroll.js"></script>
	<script src="js/developr.tooltip.js"></script>
	<script src="js/developr.table.js"></script>

	<!-- Plugins -->
	<script src="js/libs/jquery.tablesorter.min.js"></script>
	<script src="js/libs/DataTables/jquery.dataTables.min.js"></script>

	<script>

	    // Call template init (optional, but faster if called manually)
	    $.template.init();

	    // Table sort - DataTables
	    var table = $('#sorting-advanced'),
			tableStyled = false;

	    table.dataTable({
	        'aoColumnDefs': [
				{ 'bSortable': false, 'aTargets': [0, 5] }
			],
	        'sPaginationType': 'full_numbers',
	        'sDom': '<"dataTables_header"lfr>t<"dataTables_footer"ip>',
	        'fnDrawCallback': function (oSettings) {
	            // Only run once
	            if (!tableStyled) {
	                table.closest('.dataTables_wrapper').find('.dataTables_length select').addClass('select blue-gradient glossy').styleSelect();
	                tableStyled = true;
	            }
	        }
	    });

	    // Table sort - styled
	    $('#sorting-example1').tablesorter({
	        headers: {
	            0: { sorter: false },
	            5: { sorter: false }
	        }
	    }).on('click', 'tbody td', function (event) {
	        // Do not process if something else has been clicked
	        if (event.target !== this) {
	            return;
	        }

	        var tr = $(this).parent(),
				row = tr.next('.row-drop'),
				rows;

	        // If click on a special row
	        if (tr.hasClass('row-drop')) {
	            return;
	        }

	        // If there is already a special row
	        if (row.length > 0) {
	            // Un-style row
	            tr.children().removeClass('anthracite-gradient glossy');

	            // Remove row
	            row.remove();

	            return;
	        }

	        // Remove existing special rows
	        rows = tr.siblings('.row-drop');
	        if (rows.length > 0) {
	            // Un-style previous rows
	            rows.prev().children().removeClass('anthracite-gradient glossy');

	            // Remove rows
	            rows.remove();
	        }

	        // Style row
	        tr.children().addClass('anthracite-gradient glossy');

	        // Add fake row
	        $('<tr class="row-drop">' +
				'<td colspan="' + tr.children().length + '">' +
					'<div class="float-right">' +
						'<button type="submit" class="button glossy mid-margin-right">' +
							'<span class="button-icon"><span class="icon-mail"></span></span>' +
							'Send mail' +
						'</button>' +
						'<button type="submit" class="button glossy">' +
							'<span class="button-icon red-gradient"><span class="icon-cross"></span></span>' +
							'Remove' +
						'</button>' +
					'</div>' +
					'<strong>Name:</strong> John Doe<br>' +
					'<strong>Account:</strong> admin<br>' +
					'<strong>Last connect:</strong> 05-07-2011<br>' +
					'<strong>Email:</strong> john@doe.com' +
				'</td>' +
			'</tr>').insertAfter(tr);

	    }).on('sortStart', function () {
	        var rows = $(this).find('.row-drop');
	        if (rows.length > 0) {
	            // Un-style previous rows
	            rows.prev().children().removeClass('anthracite-gradient glossy');

	            // Remove rows
	            rows.remove();
	        }
	    });

	    // Table sort - simple
	    $('#sorting-example2').tablesorter({
	        headers: {
	            5: { sorter: false }
	        }
	    });

	</script>           
</form>
</body>
</html>