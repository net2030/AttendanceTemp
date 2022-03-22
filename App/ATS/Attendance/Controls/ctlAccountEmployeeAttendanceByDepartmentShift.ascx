<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountEmployeeAttendanceByDepartmentShift.ascx.vb" Inherits="ctlAccountEmployeeAttendanceByDepartmentShift" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../ctlDepartmentTree.ascx" TagName="ctlDepartmentTree" TagPrefix="uc1" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>



<script src="../js/jquery.min.js"></script>
<script type="text/javascript">
    var oldTR;
    function divexpandcollapseDepartment(divname) {
        var img = "img" + divname;
        
        if ($("#" + img).attr("src") == "../Images/plus.png") {

            $("#" + img).closest("tr").after("<tr><td colspan = '100%'>" + $("#" + divname).html() + "</td></tr>");
            console.log($("#" + img).parent().parent().parent().next().next().html())
             oldTR = $("#" + img).parent().parent().parent().next().next().html()
            var newTD = '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>'
            var newTR = newTD + oldTR
            // $("#" + img).parent().parent().parent().next().next().addClass('hidden');
            $("#" + img).parent().parent().parent().next().next().html(newTR)

            $("#" + img).attr("src", "../Images/minus.png");
        }
        else {
            $("#" + img).closest("tr").next().remove();
            $("#" + img).attr("src", "../Images/plus.png");
            // $("#" + img).parent().parent().parent().next().removeClass('hidden');
            $("#" + img).parent().parent().parent().next().html(oldTR)
            console.log(oldTR)
          //  console.log($("#" + img).parent().parent().parent().next().html())
        }
    }


    $(document).ready(function () {




        var childGridHTML = []
        var childGrids = document.getElementsByClassName("ChildGrid")
        for (var i = 0; i < childGrids.length; i++) {
            childGridHTML[i] = childGrids[i].innerHTML
            childGrids[i].innerHTML = ""
        }
        var gridId = document.getElementById("<%= GrdDepartmentAttendance0.ClientID%>").id
        MergeCommonRowsDepartment($('#' + gridId));

       // console.log(childGridHTML)

        for (var i = 0; i < childGrids.length; i++) {

            childGrids[i].innerHTML = childGridHTML[i]
        }

    })


    function MergeCommonRowsDepartment(table) {

        var firstColumnBrakes = [];
        // iterate through the columns instead of passing each column as function parameter:
        for (var i = 1; i <= table.find('th').length; i++) {
            if (i > 2)
                continue
            var previous = null, cellToExtend = null, rowspan = 1;
            table.find("td:nth-child(" + i + ")").each(function (index, e) {
                var jthis = $(this), content = jthis.text();
                //jthis.parent().css({ "border": "1px solid green" });
                // check if current row "break" exist in the array. If not, then extend rowspan:
                if (previous == content && content !== "" && $.inArray(index, firstColumnBrakes) === -1) {
                    // hide the row instead of remove(), so the DOM index won't "move" inside loop.
                    jthis.addClass('hidden');

                    jthis.next().addClass('hidden');
                    jthis.parent().prev().find("td:eq(2)").attr("rowspan", 2);
                    jthis.parent().prev().find("td:eq(2)").addClass("altrow");

                    cellToExtend.attr("rowspan", (rowspan = rowspan + 1));
                    cellToExtend.addClass("altrow");
                    cellToExtend.parent().css({ "border-top": "1px solid #018B5E" });

                } else {
                    // store row breaks only for the first column:
                    if (i === 1) firstColumnBrakes.push(index);
                    rowspan = 1;
                    previous = content;
                    cellToExtend = jthis;
                }
            });
        }
        // now remove hidden td's (or leave them hidden if you wish):
        $('td.hidden').remove();
    }
</script>


<div align="right" style="margin-bottom: 20px; margin-top: 20px; border: solid;">
    <table class="xAdminOption" width="98%" style="text-align: right; margin-top: 0px;">
        <tr>
            <td>«·ﬁ”„ :
            </td>

            <td colspan="4">
                <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>ÃÂ… «· ÊŸÌ›
            </td>
            <td>
                <asp:DropDownList ID="ddlEmployer" runat="server" DataSourceID="dsAccountEmployerObject" DataValueField="EmployerId" DataTextField="EmployerName"></asp:DropDownList>
            </td>
            <td style="width: 10%;">‰Ê⁄ «·⁄ﬁœ
            </td>
            <td>
                <asp:DropDownList ID="ddlContractType" runat="server" DataSourceID="dsAccountContractTypeObject" DataValueField="ContractTypeId" DataTextField="ContractTypeName"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 10%"> «—ÌŒ «·Õ÷Ê— :
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Startdate" />
            </td>

        </tr>

        <tr>

            <td>
                <asp:Button ID="btnView" runat="server"
                    Text="⁄—÷ "
                    UseSubmitBehavior="False" />
            </td>
        </tr>

    </table>

</div>




&nbsp;&nbsp;<br />

<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>



<x:GridView ID="GrdDepartmentAttendance0" ShowHeaderWhenEmpty="true" runat="server" AutoGenerateColumns="False" DataKeyNames="LogId,EmployeeID,LogDate"
    DataSourceID="dsDepartmentAttendance" SkinID="xgridviewSkinEmployee" Caption="”Ã·«  Õ÷Ê— Ê«‰’—«› „ÊŸ›" Width="98%">
    <%--<x:GridView ID="GrdDepartmentAttendance0" SkinID="xgridviewSkinEmployee" 
            runat="server" AutoGenerateColumns="False" Width="98%" 
            Caption="”Ã·«  Õ÷Ê— Ê«‰’—«› „ÊŸ›Ì ≈œ«—…" DataKeyNames="LogId,EmployeeID,LogDate" CaptionAlign="Right" style=";text-align:right;direction:rtl;" DataSourceID="dsDepartmentAttendance" ShowHeaderWhenEmpty="True">--%>
    <Columns>



        


        <%-- <asp:BoundField DataField="LogDate" HeaderText=" «—ÌŒ «·Õ÷Ê—" SortExpression="LogDate" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>--%>

      <%--  <asp:BoundField DataField="LogId" HeaderText="„”·”·" />--%>

        <asp:BoundField DataField="BadgeNo" HeaderText="«·—ﬁ„ «·ÊŸÌ›Ì" SortExpression="BadgeNo">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Width="15%" />
        </asp:BoundField>

       

        <asp:BoundField DataField="EmployeeName" HeaderText="≈”„ «·„ÊŸ›" SortExpression="EmployeeName" HtmlEncode="false">
            <HeaderStyle HorizontalAlign="Center" Width="200px" />
            <ItemStyle HorizontalAlign="Center" Width="200px" />
        </asp:BoundField>


        <asp:TemplateField ItemStyle-Width="20px">
            <ItemTemplate>
                <a href="JavaScript:divexpandcollapseDepartment('div<%# Eval("LogId")%>');">
                    <img alt="Details" id='imgdiv<%# Eval("LogId") %>' src="../Images/plus.png" />
                </a>
                <div id='div<%# Eval("LogId")%>' style="display: none;">
                    <asp:GridView ID="grdViewEmployeeLogs" runat="server" AutoGenerateColumns="false" OnRowCommand="grdViewEmployeeLogs_RowCommand"
                        CssClass="ChildGrid">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="LogTime" HeaderText="Êﬁ  «· ”ÃÌ·" DataFormatString="{0:hh:mm tt}" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="LogTypeName" HeaderText="‰Ê⁄ «·”Ã·" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="MachineName" HeaderText="≈”„ «·ÃÂ«“" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="IPAddress" HeaderText="⁄‰Ê«‰ «·ÃÂ«“" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Location" HeaderText="„Êﬁ⁄ «·ÃÂ«“" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="ManualRecord" HeaderText="‰Ê⁄ «·≈÷«›…" />
                       

                            <asp:TemplateField HeaderText=" €ÌÌ—" ShowHeader="False">
                                    <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("LogId")%>'  runat="server"><img width="30" height="30" src="../images/swap.png" alt="change" /></asp:LinkButton> 
                                   </ItemTemplate>
                           </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="InTime" HeaderText="Êﬁ  «·œŒÊ·" SortExpression="InTime" DataFormatString="{0:hh:mm tt}">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="OutTime" HeaderText="Êﬁ  «·Œ—ÊÃ" SortExpression="OutTime" DataFormatString="{0:hh:mm tt}">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <%--<asp:TemplateField HeaderText="Õ«·… «·œÊ«„" HeaderStyle-Width="15%" SortExpression="StatusName" ItemStyle-HorizontalAlign="Center" >
                        <itemtemplate>
                            <asp:DropDownList ID="ddlAttendanceStatus" Enabled="false" runat="server" Font-Size="Large" Font-Names="Arial" DataSourceID="dsAttendanceStatusObject"
                                 DataTextField="StatusName" Width="90%" DataValueField="StatusId" SelectedValue='<%# Bind("StatusId") %> ' ></asp:DropDownList>
                        </itemtemplate>
         </asp:TemplateField>--%>

        <%-- <asp:TemplateField HeaderText="Õ«·… «·œÊ«„" HeaderStyle-Width="15%" SortExpression="StatusName" ItemStyle-HorizontalAlign="Center" >
                        <itemtemplate>
                            <asp:DropDownList ID="ddlAttendanceStatus" Enabled='<%# If(Session("Role").ToString().Equals("Administrator"), True, False)%>'  runat="server" Font-Size="Large" Font-Names="Arial" DataSourceID="dsAttendanceStatusObject"
                                 DataTextField="StatusName" Width="90%" DataValueField="StatusId" SelectedValue='<%# Bind("StatusId") %> ' ></asp:DropDownList>
                        </itemtemplate>
         </asp:TemplateField>--%>

        <asp:BoundField DataField="StatusName" HeaderText="Õ«·… «·œÊ«„" SortExpression="StatusName">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="InLateMinutes" HeaderText=" √ŒÌ— «·œŒÊ·" SortExpression="InLateMinutes">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="OutLateMinutes" HeaderText="Œ—ÊÃ „»ﬂ—" SortExpression="OutLateMinutes">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="ActualWorkMinutes" HeaderText="Êﬁ  «·⁄„· «·›⁄·Ì" SortExpression="ActualWorkMinutes">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <%-- <asp:TemplateField HeaderText="„·«ÕŸ« ">
                        <itemtemplate>
                            <asp:TextBox ID="Comments" runat="server"   TextMode="MultiLine" ></asp:TextBox>
                        </itemtemplate>
         </asp:TemplateField>--%>
    </Columns>
</x:GridView>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
<%--<asp:Button ID="btnUpdate" runat="server"
                        Text=" ÕœÌÀ «·”Ã·« " 
                        UseSubmitBehavior="False" />--%>
<%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(178, 1) Then%>
<br>
<asp:Button ID="addManual" runat="server"
    Text="≈÷«›… »’„… ÌœÊÌ…"
    UseSubmitBehavior="False" Visible="false" />
<% End If%>
<%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(183, 1) Then%>
<asp:Button ID="processAttendance" runat="server"
    Text="„⁄«·Ã… «·Õ÷Ê— Ê«·«‰’—«›"
    UseSubmitBehavior="False" Visible="false" />
<% End If%>
<%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(173, 1) Then%>
<asp:Button ID="btnDevices" runat="server"
    Text="√ÃÂ“… «·»’„…"
    UseSubmitBehavior="False" Visible="false" />
<% End If%>
<asp:ObjectDataSource ID="dsDepartmentAttendance" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDepartmentAttendanceDataset" TypeName="ATS.BO.Framework.BOAttendanceLog">
    <SelectParameters>
        <asp:Parameter Name="DepartmentId" Type="Int32" />
        <asp:Parameter Name="AttendanceDate" Type="DateTime" />
        <asp:Parameter DefaultValue="1" Name="Employer" Type="Int32" />
        <asp:Parameter DefaultValue="1" Name="ContractType" Type="Int32" />
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="10000" Name="PageSize" Type="Int32" />
         <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>






<asp:ObjectDataSource ID="dsAccountEmployerObject" runat="server" OldValuesParameterFormatString="original_{0}"
         SelectMethod="GetList" TypeName="ATS.BO.Framework.BOEmployer">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
      </asp:ObjectDataSource>

  <asp:ObjectDataSource ID="dsAccountContractTypeObject" runat="server" OldValuesParameterFormatString="original_{0}"
         SelectMethod="GetList" TypeName="ATS.BO.Framework.BOContractType">
       <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
      </asp:ObjectDataSource>
<%--</ContentTemplate>--%>