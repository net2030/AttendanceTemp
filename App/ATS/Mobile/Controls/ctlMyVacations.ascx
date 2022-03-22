<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMyVacations.ascx.vb" Inherits="Mobile_Controls_ctlMyVacations" %>
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

<div data-role="header" data-theme="a" >
       <h1 style="text-align:left;margin-left:15px;" >My Vacations</h1>
</div>

<div data-role="content1" data-theme="d" >  
    
<asp:Repeater ID="R" runat="server" DataSourceID="dsVacations" >

          <ItemTemplate>
           <ul data-filter="false" data-inset="True" data-role="listview" data-split-icon="delete"  >

                 <li >
                  <div style="float:left;margin-left:0px">
                      
                   <a href="JavaScript:divexpandcollapse('div<%# Eval("VacationId")%>');">
                   <img alt="Details" id='imgdiv<%# Eval("VacationId") %>' src='<%# If(Eval("IsApproved").ToString().Equals("True"), "../Images/plusG.png", If(Eval("Isrejected").ToString().Equals("True"), "../Images/plusR.png", "../Images/plus.png"))%>' />
                   </a>
                 
                     </div>
                                              
                             <asp:LinkButton ID="lnkDayView" runat="server"
                                             style="font-size:9pt " 
                                             PostBackUrl='<%# String.Format("~/Mobile/VacationForm.aspx?VacationId={0}", Eval("VacationId"))%>'
                                             Text='<%# Eval("TypeName") & " - " & Eval("AprovalStatus")%>' 
                                             tooltip='<%# Eval("AprovalStatus") & "  -  " & Eval("TypeName")%>' 
                                             Enabled ='<%# If(Eval("IsApproved"),False ,True ) %>'>
                             </asp:LinkButton>
                             <asp:LinkButton ID="lnkDelete" runat="server" 
                                                      OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript()%>" 
                                                      PostBackUrl='<%# Eval("VacationId", "~/Mobile/VacationForm.aspx?IsDelete=True&VacationId={0}")%>' 
                                                      Text="حذف" 
                                                      Visible ='<%# If(Eval("IsApproved"),False ,True ) %>'> 
                              </asp:LinkButton>
               
                   
                </li>
            
             <li id='div<%# Eval("VacationId")%>li' style="display:none">
                    <div id='div<%# Eval("VacationId")%>' style="display: none;">
                       <table>
                           <tr>
                           
                               <td >
                                   Effective :
                                   <%# DataBinder.Eval(Container.DataItem, "EffectiveDate","{0:dd/M/yyyy}")%>
                                   <td style ="  padding-left: 50px;">
                                   Expired :
                                   <%# DataBinder.Eval(Container.DataItem, "DateExpire", "{0:dd/M/yyyy}")%>
                               </td>
                               </td>
                           </tr>

                           <tr>
                          
                               <td>
                                   Return :
                                   <%# DataBinder.Eval(Container.DataItem, "DateOfReturn", "{0:dd/M/yyyy}")%>
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
         
                                  </ul>
     </ItemTemplate>

  </asp:Repeater>

    </div>

 <asp:Button ID="btnAdd" runat="server" Text="Add New" UseSubmitBehavior="False" data-inline="true"/>

<asp:ObjectDataSource ID="dsVacations" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetVacationsByEmployeeIdDataset" TypeName="ATS.BO.Framework.BOVacation">
    <SelectParameters>
        <asp:SessionParameter Name="UserAccountId" SessionField="AccountEmployeeId" Type="Int32" />
        <asp:SessionParameter Name="EmployeeId" SessionField="AccountEmployeeId" Type="Int32" />
        <asp:Parameter Name="PageNo" Type="Int32" DefaultValue="1" />
        <asp:Parameter DefaultValue="50" Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>









