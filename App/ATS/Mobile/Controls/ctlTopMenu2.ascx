<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlTopMenu2.ascx.vb" Inherits="Mobile_Controls_ctlTopMenu2" %>

   <link rel="stylesheet" href="../css/themes/default/jquery.mobile-1.4.5.min.css">
	<link rel="stylesheet" href="../_assets/css/jqm-demos.css">
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,700">
	<script src="../js1/jquery.js"></script>
    <script src="../_assets/js/index.js"></script>
    <script src="../js1/jquery.mobile-1.4.5.min.js"></script>

<a class="ui-btn ui-corner-all ui-shadow ui-btn-inline ui-icon-bars ui-btn-icon-left ui-btn-b" href="#popupNested" data-transition="pop" data-rel="popup">Choose a creature...</a>
<div id="popupNested" data-role="popup" data-theme="none">
    <div style="margin: 0px; width: 250px;" data-role="collapsibleset" data-theme="b" data-content-theme="a" data-expanded-icon="arrow-d" data-collapsed-icon="arrow-r">
        <div data-role="collapsible" data-inset="false">
        <h2>Farm animals</h2>
            <ul data-role="listview">
                <li><a href="#" data-rel="dialog">Chicken</a></li>
                <li><a href="#" data-rel="dialog">Cow</a></li>
                <li><a href="#" data-rel="dialog">Duck</a></li>
                <li><a href="#" data-rel="dialog">Sheep</a></li>
            </ul>
        </div><!-- /collapsible -->
        <div data-role="collapsible" data-inset="false">
        <h2>Pets</h2>
            <ul data-role="listview">
                <li><a href="#" data-rel="dialog">Cat</a></li>
                <li><a href="#" data-rel="dialog">Dog</a></li>
                <li><a href="#" data-rel="dialog">Iguana</a></li>
                <li><a href="#" data-rel="dialog">Mouse</a></li>
            </ul>
        </div><!-- /collapsible -->
        <div data-role="collapsible" data-inset="false">
        <h2>Ocean Creatures</h2>
            <ul data-role="listview">
                <li><a href="#" data-rel="dialog">Fish</a></li>
                <li><a href="#" data-rel="dialog">Octopus</a></li>
                <li><a href="#" data-rel="dialog">Shark</a></li>
                <li><a href="#" data-rel="dialog">Starfish</a></li>
            </ul>
        </div><!-- /collapsible -->
        <div data-role="collapsible" data-inset="false">
        <h2>Wild Animals</h2>
            <ul data-role="listview">
                <li><a href="#" data-rel="dialog">Lion</a></li>
                <li><a href="#" data-rel="dialog">Monkey</a></li>
                <li><a href="#" data-rel="dialog">Tiger</a></li>
                <li><a href="#" data-rel="dialog">Zebra</a></li>
            </ul>
        </div><!-- /collapsible -->
    </div><!-- /collapsible set -->
</div><!-- /popup -->
