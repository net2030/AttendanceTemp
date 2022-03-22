<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlVacationApproval.ascx.vb" Inherits="Mobile_Controls_ctlVacationApproval" %>
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


    $(document).ready(function () {

        var labels = document.getElementsByTagName("Label");

        for (var i = 0 ; i < labels.length; i++)
            $(document).on("click touchstart", "#" + labels[i].id, function (e) {
                if (this.id.indexOf("lblApproved") > -1)
                    ChangeColorWhenApprove(this);
                else if (this.id.indexOf("lblRejected") > -1)
                    ChangeColorWhenReject(this);
            });

    });

    function ChangeColorWhenApprove(sender) {
        unsaved = false;
        var matches = [];
        var links = document.getElementsByTagName("img");
        var labels = document.getElementsByTagName("Label");

        //ui-btn-text
        for (var i = 0; i < labels.length; i++) {
            if (labels[i].id.indexOf("lblApproved") > -1)
                matches.push(labels[i]);
        }

        for (var i = 0; i < matches.length; i++) {
            if (matches[i].id.localeCompare(sender.id) == 0)
                document.getElementById(links[i+1].id).style.backgroundColor = "#00FF00";
        }

    }

    function ChangeColorWhenReject(sender) {
        unsaved = false;
        var matches = [];
        var links = document.getElementsByTagName("img");
        var labels = document.getElementsByTagName("Label");
        //alert(links.length);
        //ui-btn-text
        for (var i = 0; i < labels.length; i++) {
            if (labels[i].id.indexOf("lblRejected") > -1)
                matches.push(labels[i]);
        }

        for (var i = 0; i < matches.length; i++) {
            if (matches[i].id.localeCompare(sender.id) == 0)
                document.getElementById(links[i+1].id).style.backgroundColor = "#FF0000";
            //}

        }

    }
   
</script>

<script type="text/javascript">
    unsaved = true;
    window.onbeforeunload = function () {
        if (!window.unsaved) {
            return 'You must click "Update Approval Status" . If you leave it will not updated.';
        }
    };
 </script>

<div data-role="header" data-theme="a" >
       <h1 style="text-align:left;margin-left:15px;" >List of Pending Vacationes </h1>
</div>

<div data-role="content1" data-theme="d" style="width:100%" >  
     

     

  <asp:Repeater ID="R" runat="server" DataSourceID="dsVacationesApproval"  >
      <HeaderTemplate>
           <ul data-filter="false" data-inset="True" data-role="listview" data-split-icon="delete"  >
      </HeaderTemplate>
          <ItemTemplate>
                        
      <asp:HiddenField ID="HiddenField1" runat="server" value='<%# Eval("VacationId") %>' />
                 <li >
                  <div style="float:left;margin-left:0px">
                      
                   <a href="JavaScript:divexpandcollapse('div<%# Eval("VacationId")%>');">
                   <img alt="Details" id='imgdiv<%# Eval("VacationId") %>' src="../Images/plus.png" />
                   </a>
                 
                     </div>
                                              
                              <asp:LinkButton ID="lnkDayView" runat="server"
                                             style="font-size:9pt " 
                                             Text='<%# Eval("EmployeeName") & " - " & Eval("Period") & " - " & "أيام"%>' Enabled="false" >
                             </asp:LinkButton>   
                            
            
                   
                </li>
            
              <li id='div<%# Eval("VacationId")%>li' style="display:none">
           
                    <div id='div<%# Eval("VacationId")%>' style="display: none;">
                       <table style="width:100%">
                           <tr>
                               <td>
                                   Type :
                                </td>
                               <td >
                                   <%# DataBinder.Eval(Container.DataItem, "TypeName")%>
                               </td>
                               
                           </tr>
                           <tr>
                           
                               <td>
                                   Start :
                                    </td>
                               <td colspan="2">
                                   <%# DataBinder.Eval(Container.DataItem, "EffectiveDate")%>
                               </td>
                               
                           </tr>

                           <tr>
                          
                               <td>
                                   End :
                                 </td>
                                  <td colspan="2">
                                   <%# DataBinder.Eval(Container.DataItem, "DateExpire")%>
                               </td>
                           </tr>
                           <tr>
                          
                               <td>
                                   Return :
                                 </td>
                                  <td colspan="2">
                                   <%# DataBinder.Eval(Container.DataItem, "DateOfReturn")%>
                               </td>
                           </tr>
                           <tr>
                           
                               <td>
                                   Notes :
                               </td>
                               <td colspan="2">
                                   <%# DataBinder.Eval(Container.DataItem, "Note")%>
                               </td>
                           </tr>
                           <tr>
                               <tr>
                               <td colspan="3">
                                  <asp:TextBox ID="txtNotes" runat="server"  TextMode="MultiLine" style="margin-right: 0px"  Height="50px"></asp:TextBox>
                               </td>
                              </tr>
                       </table>
              </div>
                  <div data-role="fieldcontain" style="width:100%;font-size:small">
	                 <fieldset data-role="controlgroup" data-type="horizontal">
                           <asp:RadioButton ID="rdApproved" runat="server" GroupName="SpecificEmployee"></asp:RadioButton>
	         	            <asp:label runat="server" id="lblApproved"  AssociatedControlId="rdApproved" style="width:48%;font-size:small" >Approve</asp:label>
                           <asp:RadioButton ID="rdRejected" runat="server" GroupName="SpecificEmployee" ></asp:RadioButton>
	         	            <asp:label runat="server" ID="lblRejected" AssociatedControlId="rdRejected" style="width:48%;font-size:small">Reject</asp:label>
	                </fieldset>
	</div>
             </li>
                                  
     </ItemTemplate>
      <FooterTemplate>
          </ul>
 <asp:Button ID="btnApprove" runat="server" OnClientClick="unsaved=true;"  Text="Update Approval Status" UseSubmitBehavior="False" data-inline="true" OnClick="btnApprove_Click"/>
      </FooterTemplate>
  </asp:Repeater>
       

    </div>


</script>
<asp:ObjectDataSource ID="dsVacationesApproval" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetVacationByEmployeeManagerForApprovalDataset" TypeName="ATS.BO.Framework.BOVacation">
    <SelectParameters>
        <asp:SessionParameter Name="ManagerID" SessionField="AccountEmployeeId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>



