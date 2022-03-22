<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountEmployeeAttendanceShift.ascx.vb" Inherits="Controls_ctlEmployeeAttendanceShift" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%--<%@ Register Assembly="C1.Web.C1WebReport.2" Namespace="C1.Web.C1WebReport" TagPrefix="cc1" %>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<%--<meta content="text/html"; charset="UTF-8" http-equiv="Content-Type" />--%>
<script src="../js/jquery.min.js"></script>
<script type="text/javascript">
    var oldTR;
    function divexpandcollapse(divname) {
        var img = "img" + divname;

        if ($("#" + img).attr("src") == "../Images/plus.png") {

            $("#" + img).closest("tr").after("<tr><td colspan = '100%'>" + $("#" + divname).html() + "</td></tr>");
            console.log($("#" + img).parent().parent().parent().next().next().html())
            oldTR = $("#" + img).parent().parent().parent().next().next().html()
            var newTD = '<td>&nbsp;</td><td>&nbsp;</td>'
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

    //$(".loading").css({ display: 'inline' });
    //tournamentCollection.fetch({
    //    success: function (bookResponse) {
    //        $(".loading").css({ display: 'none' });
    //    }
    //});


    $(document).ready(function () {
       
        var childGridHTML =[]
        var childGrids = document.getElementsByClassName("ChildGrid")
        for (var i = 0; i < childGrids.length; i++) {
            childGridHTML[i] = childGrids[i].innerHTML
            childGrids[i].innerHTML=""
        }
       var gridId = document.getElementById("<%= GrdEmployeeAttendance.ClientID%>").id
        MergeCommonRows($('#' + gridId));

      //  console.log(childGridHTML)

        for (var i = 0; i < childGrids.length; i++) {
           
            childGrids[i].innerHTML = childGridHTML[i]
        }

    })


    function MergeCommonRows(table) {

        var firstColumnBrakes = [];
        // iterate through the columns instead of passing each column as function parameter:
        for (var i = 2; i <= table.find('th').length; i++) {
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

                    jthis.prev().addClass('hidden');
                    jthis.parent().prev().find("td:first").attr("rowspan", 2);

                    


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

<style type="text/css">
    .altrow {
        border-top:0px !important;
    }
    .row {
        border-bottom:0px !important;
    }
</style>

<x:GridView  ID="GrdEmployeeAttendance" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" DataKeyNames="LogId,EmployeeID,LogDate"  
   DataSourceID="dsEmployeeAttendance" SkinID="xgridviewSkinEmployee"  Caption="ÓÌáÇÊ ÍÖæÑ æÇäÕÑÇÝ ãæÙÝ" Width="98%"  Font-Names="Arial">

         <Columns>

         <asp:TemplateField ItemStyle-Width="20px">
                            <ItemTemplate>
                                <a href="JavaScript:divexpandcollapse('div<%# Eval("LogId")%>');">
                                    <img alt="Details" id='imgdiv<%# Eval("LogId") %>' src="../Images/plus.png" />
                                </a>
                                <div id='div<%# Eval("LogId")%>' style="display: none;">
                                    <asp:GridView ID="grdViewEmployeeLogs" runat="server" AutoGenerateColumns="false" OnRowCommand="grdViewEmployeeLogs_RowCommand"
                                                   CssClass="ChildGrid">
                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="LogTime" HeaderText="æÞÊ ÇáÊÓÌíá"  DataFormatString="{0:hh:mm tt}"/>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="LogTypeName" HeaderText="äæÚ ÇáÓÌá" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="MachineName" HeaderText="ÅÓã ÇáÌåÇÒ" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="IPAddress" HeaderText="ÚäæÇä ÇáÌåÇÒ" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="Location" HeaderText="ãæÞÚ ÇáÌåÇÒ" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="ManualRecord" HeaderText="äæÚ ÇáÅÖÇÝÉ" />
                                        

                                             <asp:TemplateField HeaderText="ÊÛííÑ" ShowHeader="False">
                                               <ItemTemplate>
                                                      <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("LogId")%>' CommandName="change"  runat="server"><img width="30" height="30" src="../images/swap.png" alt="change" /></asp:LinkButton> 
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                        
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ItemTemplate>
         </asp:TemplateField>

       <%-- <asp:BoundField DataField="LogId" HeaderText="ãÓáÓá"  SortExpression="LogId"  Visible="true" >
            <HeaderStyle HorizontalAlign="Center"  />
            <ItemStyle HorizontalAlign="Center"  />
        </asp:BoundField>--%>

        <asp:BoundField DataField="LogDate" HeaderText="ÊÇÑíÎ ÇáÍÖæÑ"  SortExpression="LogDate" dataformatstring="{0:dd-MM-yyyy}"  htmlencode="false"  >
            <HeaderStyle HorizontalAlign="Center"  />
            <ItemStyle HorizontalAlign="Center" Width="15%"  />
        </asp:BoundField>

             

        <asp:BoundField DataField="InTime" HeaderText="æÞÊ ÇáÏÎæá"  SortExpression="InTime"  DataFormatString="{0:hh:mm tt}" >
            <HeaderStyle HorizontalAlign="Center"  />
            <ItemStyle HorizontalAlign="Center"  />
        </asp:BoundField>

        <asp:BoundField DataField="OutTime" HeaderText="æÞÊ ÇáÎÑæÌ"  SortExpression="OutTime" DataFormatString="{0:hh:mm tt}" >
            <HeaderStyle HorizontalAlign="Center"  />
            <ItemStyle HorizontalAlign="Center"  />
        </asp:BoundField>

        <asp:BoundField DataField="StatusName" HeaderText="ÍÇáÉ ÇáÏæÇã"  SortExpression="StatusName" >
            <HeaderStyle HorizontalAlign="Center"  />
            <ItemStyle HorizontalAlign="Center"  />
        </asp:BoundField>

        <asp:BoundField DataField="InLateMinutes" HeaderText="ÊÃÎíÑ ÇáÏÎæá"  SortExpression="InLateMinutes" >
            <HeaderStyle HorizontalAlign="Center"  />
            <ItemStyle HorizontalAlign="Center"  />
        </asp:BoundField>

        <asp:BoundField DataField="OutLateMinutes" HeaderText="ÎÑæÌ ãÈßÑ"  SortExpression="OutLateMinutes" >
            <HeaderStyle HorizontalAlign="Center"  />
            <ItemStyle HorizontalAlign="Center"  />
        </asp:BoundField>

        <asp:BoundField DataField="ActualWorkMinutes" HeaderText="æÞÊ ÇáÚãá ÇáÝÚáí"  SortExpression="ActualWorkMinutes" >
            <HeaderStyle HorizontalAlign="Center"  />
            <ItemStyle HorizontalAlign="Center"  />
        </asp:BoundField>

    </Columns>
</x:GridView>

<%--    </ContentTemplate>
</asp:UpdatePanel>--%>



<asp:ObjectDataSource ID="dsEmployeeAttendance" runat="server" OldValuesParameterFormatString="original_{0}"
                              SelectMethod="GetEmployeeAttendanceByDate" TypeName="ATS.BO.Framework.BOAttendanceLog">
            <SelectParameters>
                <asp:Parameter Name="EmployeeId" Type="Int32" />
                <asp:Parameter Name="BegDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />
                <asp:Parameter Name="PageNo" Type="Int32" DefaultValue="1" />
                <asp:Parameter DefaultValue="300" Name="PageSize" Type="Int32" />
                <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
                <asp:Parameter Name="Op" Type="Int32" DefaultValue="0" />
            </SelectParameters>
        </asp:ObjectDataSource>

