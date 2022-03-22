<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlTopMenu.ascx.vb" Inherits="Mobile_Controls_ctlTopMenu" %>



	<link href="http://localhost:9570/css/jquery.mmenu.all.css" rel="stylesheet" />
    <link href="../css/demo.css" rel="stylesheet" />

    <script src="../js1/jquery.min.js"></script>
     <script src="../js/jquery.mmenu.min.all.js">

     </script>

		<script type="text/javascript">
		    $(function () {
		        $('nav#menu').mmenu({
		            extensions: ['effect-slide-menu', 'pageshadow'],
		            searchfield: true,
		            counters: true,
		            navbar: {
		                title: 'Advanced menu'
		            },
		            navbars: [
						{
						    position: 'top',
						    content: ['searchfield']
						}, {
						    position: 'top',
						    content: [
								'prev',
								'title',
								'close'
						    ]
						}, {
						    position: 'bottom',
						    content: [
								'<a href="http://mmenu.frebsite.nl/wordpress-plugi1n.html" target="_blank">WordPress plugin</a>'
						    ]
						}
		            ]
		        });
		    });
		</script>


<div id="page1">
			<div class="header">
				<a href="#menu" target="_parent"></a>
				Demo
			</div>
			
			<nav id="menu">
				<ul>
					<li><a href="#" target="_parent">Home</a></li>
					<li><a href="http://www.yahoo.com" target="_parent">About us</a>
						<ul>
							<li><a href="#about/history" target="_parent">History</a></li>
							<li><a href="#about/team" target="_parent">The team</a>
								<ul>
									<li><a href="#about/team/management" target="_parent">Management</a></li>
									<li><a href="#about/team/sales" target="_parent">Sales</a></li>
									<li><a href="#about/team/development" target="_parent">Development</a></li>
								</ul>
							</li>
							<li><a href="#about/address" target="_parent">Our address</a></li>
						</ul>
					</li>
					<li><a href="#contact" target="_parent">Contact</a></li>
				</ul>
			</nav>
		</div>