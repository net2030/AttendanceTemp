<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlTopMenu1.ascx.vb" Inherits="Mobile_Controls_ctlTopMenu1" %>


<style type="text/css">
      .noti_bubble {
                    position:absolute;    /* This breaks the div from the normal HTML document. */
                    top: 0px;
                    right:-62px;
                    padding:1px 2px 1px 2px;
                    background-color:red; /* you could use a background image if you'd like as well */
                    color:white;
                    font-weight:bold;
                    font-size:0.77em;

                    /* The following is CSS3, but isn't crucial for this technique to work. */
                    /* Keep in mind that if a browser doesn't support CSS3, it's fine! They just won't have rounded borders and won't have a box shadow effect. */
                    /* You can always use a background image to produce the same effect if you want to, and you can use both together so browsers without CSS3 still have the rounded/shadow look. */
                    border-radius:30px;
                    box-shadow:1px 1px 1px gray;
                }       
</style>

<script src="../js/jquery-2.1.4.min.js"></script>
<script src="../js/jquery-ui.min.js"></script>
<script src="../js/hamburger.js"></script>
<link href="../css/hamburger.css" rel="stylesheet" />
<div id="container"> 
  
  <!--The Hamburger Button in the Header-->
  <header>
      <div style="float:right;margin-right:50%;color:white;font-weight:bold"> <p>  MAS TAM </p> </div>
    <div id="hamburger">
      <div></div>
      <div></div>
      <div></div>
        
    </div>
  </header>
  
  <!--The mobile navigation Markup hidden via css-->
  <nav>
    <ul>
      <li><a href="MyChart.aspx"  target="_parent">Home</a></li>
        <li><a href="MyAttendance.aspx"  target="_parent">My Attendance</a></li>
      <li><a href="GatePass.aspx"  target="_parent">GatePasses</a></li>
           <%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(159, 1) Then%>
        <li>
             <div style ="position:relative;width:20px;height:20px;">
            <a href="Approvals.aspx"  target="_parent">Approval</a>
                 <div class="noti_bubble" style ="font-size:larger;">
                     <label id="lblAllNotifications" ></label>
                 </div>
              </div>
        </li>
      <% End If%>
      <li><a href="Vacations.aspx" target="_parent">Vacations</a></li>
      <li><a href="WorkExceptions.aspx" target="_parent">Work Exceptions</a></li>
      <li><a href="http://mastechnology.net/" target="_blank">About</a></li>
      <li>
        <a href="../Authenticate/DoLogout.aspx" target="_parent">
          <img  runat="server" style="width:27px;height:27px" src="../../Images/logout.png" /> </li>
      </a>
    </ul>
  </nav>
  
 
  <div id="contentLayer"></div>
  
 
  <div id="content">
    


</div>

<script type = "text/javascript">
    var EmployeeId = "<%=Session("AccountEmployeeId")%>"; 
</script>