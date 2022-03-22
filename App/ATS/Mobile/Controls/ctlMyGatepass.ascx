<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMyGatepass.ascx.vb" Inherits="Mobile_Controls_ctlMyGatepass1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2"Namespace="eWorld.UI" TagPrefix="ew" %>

<script type="text/javascript">
    function divexpandcollapse(divname) {
        var img = "img" + divname;
        if ($("#" + img).attr("src") == "../Images/plus.png" ) {
            $("#" + img)
            .closest("tr")
            .after("<tr><td></td><td colspan = '100%'>" + $("#" + divname)
                   .html() + "</td></tr>");
            $("#" + img).attr("src", "../Images/minus.png");
            document.getElementById(divname).style.display = "block";
            document.getElementById(divname + "li").style.display = "block";
        }
        else if ($("#" + img).attr("src") == "../Images/minus.png") {
            $("#" + img).closest("tr").next().remove();
            $("#" + img).attr("src", "../Images/plus.png");
            document.getElementById(divname).style.display = "none";
            document.getElementById(divname + "li").style.display = "none";
        }
        else if ($("#" + img).attr("src") == "../Images/plusR.png") {
            $("#" + img)
            .closest("tr")
            .after("<tr><td></td><td colspan = '100%'>" + $("#" + divname)
                   .html() + "</td></tr>");
            $("#" + img).attr("src", "../Images/minusR.png");
            document.getElementById(divname).style.display = "block";
            document.getElementById(divname + "li").style.display = "block";
        }
        else if ($("#" + img).attr("src") == "../Images/minusR.png") {
            $("#" + img).closest("tr").next().remove();
            $("#" + img).attr("src", "../Images/plusR.png");
            document.getElementById(divname).style.display = "none";
            document.getElementById(divname + "li").style.display = "none";
        }
        else if ($("#" + img).attr("src") == "../Images/plusG.png") {
            $("#" + img)
            .closest("tr")
            .after("<tr><td></td><td colspan = '100%'>" + $("#" + divname)
                   .html() + "</td></tr>");
            $("#" + img).attr("src", "../Images/minusG.png");
            document.getElementById(divname).style.display = "block";
            document.getElementById(divname + "li").style.display = "block";
        }
        else if ($("#" + img).attr("src") == "../Images/minusG.png") {
            $("#" + img).closest("tr").next().remove();
            $("#" + img).attr("src", "../Images/plusG.png");
            document.getElementById(divname).style.display = "none";
            document.getElementById(divname + "li").style.display = "none";
        }
    }
</script>

<%--<style type="text/css" >
    table {
       
        font-family:Arial
    }
        table, td {
             font-size:18px;
            margin:10px,10px,10px,10px;
        }
</style>--%>
<div data-role="header" data-theme="a" >
       <h1 style="text-align:left;margin-left:15px;" >My Gatepass</h1>
</div>

<div data-role="content1" data-theme="d" >  
     

     

  <asp:Repeater ID="R" runat="server" DataSourceID="dsGatePassesByEmployee" >
      <HeaderTemplate>
           <ul data-filter="false" data-inset="True" data-role="listview" data-split-icon="delete"  >
      </HeaderTemplate>
          <ItemTemplate>
                        

                 <li >
                     
                  <div style="float:left;margin-left:0px;">
                      
                   <a href="JavaScript:divexpandcollapse('div<%# Eval("GatepassId")%>');" >
<%--                   <img alt="Details" id='imgdiv<%# Eval("GatepassId") %>' src="../Images/plus.png" />--%>
                    <img alt="Details" id='imgdiv<%# Eval("GatepassId") %>' src='<%# If(Eval("IsApproved").ToString().Equals("True"), "../Images/plusG.png", If(Eval("Isrejected").ToString().Equals("True"), "../Images/plusR.png", "../Images/plus.png"))%>' />
                   </a>
                 
                     </div>
                  
                                              
                             <asp:LinkButton ID="lnkDayView" runat="server"
                                             style="font-size:9pt " 
                                             PostBackUrl='<%# If(Eval("IsApproved").ToString.Equals("False"), String.Format("~/Mobile/GatepassForm.aspx?GatePassId={0}", Eval("GatePassId")), "~/Mobile/Gatepass.aspx")%>'
                                             Text='<%# Eval("GatepassTypeName") & " - " & Eval("AprovalStatus")%>' 
                                             tooltip='<%# Eval("AprovalStatus") & "  -  " & Eval("GatepassTypeName")%>' 
                                             Enabled ='<%# If(Eval("IsApproved"),False ,True ) %>'>
                             </asp:LinkButton>

                             <asp:LinkButton ID="lnkDelete" runat="server" 
                                                      OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript()%>" 
                                                      PostBackUrl='<%# Eval("GatepassId", "~/Mobile/GatepassForm.aspx?IsDelete=True&GatepassId={0}")%>' 
                                                      Text="حذف" 
                                                      Visible ='<%# If(Eval("IsApproved"),False ,True ) %>'> 
                              </asp:LinkButton>
               
                   
                </li>
            
              <li id='div<%# Eval("GatepassId")%>li' style="display:none">
           
                    <div id='div<%# Eval("GatepassId")%>' style="display: none;">
                       <table >
                           <tr>
                           
                               <td>
                                   Start :
                                   <%# DataBinder.Eval(Container.DataItem, "GatepassBegTime") %>
                               </td>
                               
                           </tr>

                           <tr>
                          
                               <td>
                                   End :
                                   <%# DataBinder.Eval(Container.DataItem, "GatepassEndTime")%>
                               </td>
                           </tr>
                           <tr>
                           
                               <td>
                                   Comments :
                                   <%# DataBinder.Eval(Container.DataItem, "ApprovalNotes")%>
                               </td>
                           </tr>
                       </table>
              </div>
         </li>
                                  
     </ItemTemplate>
      <FooterTemplate>
          </ul>
      </FooterTemplate>
  </asp:Repeater>

    </div>

 <asp:Button ID="btnAdd" runat="server" Text="Add New" UseSubmitBehavior="False" data-inline="true"/>

</script>
<asp:ObjectDataSource ID="dsGatePassesByEmployee" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetGatepasssByEmployeeIdDataset" TypeName="ATS.BO.Framework.BOGatepass">
    <SelectParameters>
        <asp:Parameter Name="EmployeeId" Type="Int32" />
        <asp:Parameter Name="PageNo" Type="Int32" />
        <asp:Parameter Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>



