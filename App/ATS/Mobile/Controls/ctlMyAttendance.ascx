<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMyAttendance.ascx.vb" Inherits="Mobile_Controls_ctlMyAttendance" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2"Namespace="eWorld.UI" TagPrefix="ew" %>

<script type="text/javascript">
    function divexpandcollapse(divname) {
        var img = "img" + divname;
        if ($("#" + img).attr("src") == "../Images/plus.png") {
            $("#" + img)
            .closest("tr")
            .after("<tr><td></td><td colspan = '100%'>" + $("#" + divname)
                   .html() + "</td></tr>");
            $("#" + img).attr("src", "../Images/minus.png");
            document.getElementById(divname).style.display = "block";
            document.getElementById(divname + "li").style.display = "block";
        }
        else {
            $("#" + img).closest("tr").next().remove();
            $("#" + img).attr("src", "../Images/plus.png");
            document.getElementById(divname).style.display = "none";
            document.getElementById(divname + "li").style.display = "none";
        }
    }

  
</script>


<div data-role="header" data-theme="a" >
       <h1 style="text-align:left;margin-left:15px;" >My Attendance</h1>
</div>

<div data-role="content1"  >  
     

     

  <asp:Repeater ID="R" runat="server" DataSourceID="dsEmployeeAttendance" >
      <HeaderTemplate>
           <ul data-filter="false"  data-role="listview"  >
      </HeaderTemplate>
          <ItemTemplate>
                        

                 <li >
                  <div style="float:left;margin-left:0px">
                      
                   <a href="JavaScript:divexpandcollapse('div<%# Eval("LogId")%>');">
                    <img alt="Details" id='imgdiv<%# Eval("LogId") %>' src="../Images/plus.png" />
                   </a>
                     <script type="text/javascript">
                         var stat =<%# Eval("StatusId") %>
                            
                         if (stat==75 || stat==45 || stat==110 ||  stat==120 ||   stat==80 ||  stat==85 ||  stat==70 ||  stat==65 )
                           document.getElementById('imgdiv<%# Eval("LogId") %>').style.backgroundColor = "#FF0000";
                         else   document.getElementById('imgdiv<%# Eval("LogId") %>').style.backgroundColor = "#00FF00";
                     </script>
                     </div>
                       
                    <asp:LinkButton ID="lnkDayView" runat="server"
                                             style="font-size:9pt " 
                                             Text='<%# Eval("LogDate") & " - " & Eval("StatusName")%>' Enabled="false" >
                             </asp:LinkButton>   
                                   
                </li>

              <li id='div<%# Eval("LogId")%>li' style="display:none">
           
                    <div id='div<%# Eval("LogId")%>' style="display: none;">
                       <table >
                           <tr>
                           
                               <td>
                                   In :
                                   <%# DataBinder.Eval(Container.DataItem, "InTime","{0:hh.mm tt}")%>
                               </td>

                               <td style="padding-left:10px">
                                   Out :
                                   <%# DataBinder.Eval(Container.DataItem, "OutTime","{0:hh.mm tt}")%>
                               </td>
                               
                           </tr>

                           <tr>
                          
                               <td>
                                   Late :
                                   <%# DataBinder.Eval(Container.DataItem, "LateMinutes")%>
                               </td>

                               <td style="padding-left:10px">
                                   Extra :
                                   <%# DataBinder.Eval(Container.DataItem, "ExtraTimeMinutes")%>
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


<asp:ObjectDataSource ID="dsEmployeeAttendance" runat="server" OldValuesParameterFormatString="original_{0}"
                              SelectMethod="GetEmployeeAttendanceByDate" TypeName="ATS.BO.Framework.BOAttendanceLog">
            <SelectParameters>
                <asp:SessionParameter Name="EmployeeId" SessionField="AccountEmployeeId" Type="Int32" />
                <asp:Parameter Name="BegDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />
                <asp:Parameter Name="PageNo" Type="Int32" DefaultValue="1" />
                <asp:Parameter DefaultValue="300" Name="PageSize" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        
        
        

</script>




        



