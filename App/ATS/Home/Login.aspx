<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Home_Login" %>


<%@ Register Src="../Authenticate/Controls/ctlLogin.ascx" TagName="ctlLogin" TagPrefix="uc1" %>

<html>
<head id="Head1" runat=server>
    <title>Untitled Page</title>
    <link href="../Styles.css" rel="stylesheet" type="text/css">
</link>
</head>
<body>
<form runat=server>

<table width="100%" border="1" height="100%">
  <tr>
    <td align=center>        <table class="BlockTable">
            <tbody>
            
            <TR><TD rowspan=2 style="WIDTH: 200px" class="block">
         <%--       <uc3:ctlProductsDetail id="CtlProductsDetail1" runat="server">

                </uc3:ctlProductsDetail></TD>--%>
            <TD style="PADDING-RIGHT: 10px; WIDTH: 222px"><uc1:ctlLogin id="CtlLogin1" runat="server"></uc1:ctlLogin></TD>
            <TD style="PADDING-RIGHT: 10px; WIDTH: 222px"><STRONG>What is TimeLive?</STRONG>&nbsp;<BR><BR><SPAN class=HomePageFeatureText 
style="FONT-SIZE: 10pt; FONT-FAMILY: Verdana">TimeLive Web Collaboration Suite 
is integrated suite for managing project life cycle including tasks, issues, 
bugs, timesheet, expense, attendance etc.  </SPAN></TD>
            
            </TR>
             <TR>
      
             <TD  style="WIDTH: 41px" class="block" align=right>
            

             </TD>
            <td></td>
            
            </TR>
            </tbody>
        </table>&nbsp;</td>
  </tr>
</table>
</form>

</body>
</html>
