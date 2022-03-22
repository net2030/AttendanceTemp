<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountEmployeeAttendance.ascx.vb" Inherits="Controls_ctlEmployeeAttendance" %>



<script type="text/javascript">
    function divexpandcollapse1(divname) {
        var img = "img" + divname;
        if ($("#" + img).attr("src") == "../Images/plus.png") {
            $("#" + img)
            .closest("tr")
            .after("<tr><td></td><td colspan = '100%'>" + $("#" + divname)
                   .html() + "</td></tr>");
            $("#" + img).attr("src", "../Images/minus.png");
        }
        else {
            $("#" + img).closest("tr").next().remove();
            $("#" + img).attr("src", "../Images/plus.png");
        }
    }
    $(".loading").css({ display: 'inline' });
    tournamentCollection.fetch({
        success: function (bookResponse) {
            $(".loading").css({ display: 'none' });
        }
    });
</script>



<x:GridView ID="GrdEmployeeAttendance" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" DataKeyNames="LogId,EmployeeID,LogDate"
    DataSourceID="dsEmployeeAttendance" SkinID="xgridviewSkinEmployee" Caption="ÓÌáÇÊ ÍÖæÑ æÇäÕÑÇÝ ãæÙÝ" Width="98%"  meta:resourcekey="GrdEmployeeAttendanceResource1">

    <Columns>

        <asp:TemplateField ItemStyle-Width="20px" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <a href="JavaScript:divexpandcollapse1('div<%# Eval("LogId") %>');">
                    <img alt="Details" id='imgdiv<%# Eval("LogId") %>' src="../Images/plus.png" />
                </a>
                <div id='div<%# Eval("LogId") %>' style="display: none;">
                    <asp:GridView ID="grdViewEmployeeLogs" runat="server" AutoGenerateColumns="False"
                        CssClass="ChildGrid" meta:resourcekey="grdViewOrdersOfCustomerResource1" OnRowCommand="grdViewEmployeeLogs_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="LogTime" HeaderText="æÞÊ ÇáÊÓÌíá" DataFormatString="{0:hh:mm tt}" meta:resourcekey="BoundFieldResource1">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LogTypeName" HeaderText="äæÚ ÇáÓÌá" meta:resourcekey="BoundFieldResource2">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MachineName" HeaderText="ÅÓã ÇáÌåÇÒ" meta:resourcekey="BoundFieldResource3">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IPAddress" HeaderText="ÚäæÇä ÇáÌåÇÒ" meta:resourcekey="BoundFieldResource4">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Location" HeaderText="ãæÞÚ ÇáÌåÇÒ" meta:resourcekey="BoundFieldResource5">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ManualRecord" HeaderText="äæÚ ÇáÅÖÇÝÉ" meta:resourcekey="BoundFieldResource6">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                           <%-- <asp:BoundField DataField="ValidRecord" HeaderText="ÕÍÉÇáÓÌá" meta:resourcekey="BoundFieldResource7">

                                <ItemStyle Width="150px" />
                            </asp:BoundField>--%>
                             <asp:TemplateField HeaderText="ÊÛííÑ" ShowHeader="False">
                                               <ItemTemplate>
                                                      <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("LogId")%>'  runat="server"><img width="30" height="30" src="../images/swap.png" alt="change" /></asp:LinkButton> 
                                              </ItemTemplate>
                                          </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </ItemTemplate>

            <ItemStyle Width="20px"></ItemStyle>
        </asp:TemplateField>

        <asp:BoundField DataField="LogId" HeaderText="ãÓáÓá" SortExpression="LogId" Visible="false" meta:resourcekey="BoundFieldResource8">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="LogDate" HeaderText="ÊÇÑíÎ ÇáÍÖæÑ" SortExpression="LogDate" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" meta:resourcekey="BoundFieldResource9">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="InTime" HeaderText="æÞÊ ÇáÏÎæá" SortExpression="InTime" DataFormatString="{0:hh:mm tt}" meta:resourcekey="BoundFieldResource10">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="OutTime" HeaderText="æÞÊ ÇáÎÑæÌ" SortExpression="OutTime" DataFormatString="{0:hh:mm tt}" meta:resourcekey="BoundFieldResource11">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="StatusName" HeaderText="ÍÇáÉ ÇáÏæÇã" SortExpression="StatusName" meta:resourcekey="BoundFieldResource12">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="InLateMinutes" HeaderText="ÊÃÎíÑ ÇáÏÎæá" SortExpression="InLateMinutes" meta:resourcekey="BoundFieldResource13">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="OutLateMinutes" HeaderText="ÎÑæÌ ãÈßÑ" SortExpression="OutLateMinutes" meta:resourcekey="BoundFieldResource14">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="ActualWorkMinutes" HeaderText="æÞÊ ÇáÚãá ÇáÝÚáí" SortExpression="ActualWorkMinutes" meta:resourcekey="BoundFieldResource15">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


         <asp:TemplateField HeaderText="ÎÑæÌ ÈÏæä ÅÐä" ShowHeader="False" >
              <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("LogId")%>' runat="server" CommandName='<%# IIf(Eval("StatusId").ToString().Equals("3"), "CancelEscape", "Escape")%>'
                     Visible='<%# If(ATS.BO.BOPagePermission.IsPageHasPermissionOf(200, 3), True, False)%>'>
                    <img width="30" height="30" src='<%# IIf(Eval("StatusId").ToString().Equals("3"), "../images/Cancel_Escape1.png", "../images/escape2.png")%>' alt="Cancel Escape"  />
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</x:GridView>




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





