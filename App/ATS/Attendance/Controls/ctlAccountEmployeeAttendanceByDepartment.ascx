<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountEmployeeAttendanceByDepartment.ascx.vb" Inherits="ctlAccountEmployeeAttendanceByDepartment" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../ctlDepartmentTree.ascx" TagName="ctlDepartmentTree" TagPrefix="uc1" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>



<script type="text/javascript">
    function divexpandcollapse(divname) {
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
</script>


<div style="margin-bottom: 20px; margin-top: 20px; border: solid;">
    <table width="98%" style="margin: 10px 10px 10px 10px;">

        <tr>
            <td>
                <asp:Literal ID="Literal1" runat="server" Text="ÇáÞÓã:" meta:resourcekey="Literal1Resource1"></asp:Literal>
            </td>

            <td colspan="2">
                <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="width: 100px; height: 10px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal2" runat="server" Text="ÌåÉ ÇáÊæÙíÝ" meta:resourcekey="Literal2Resource1"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlEmployer" runat="server" DataSourceID="dsAccountEmployerObject" DataValueField="EmployerId" DataTextField="EmployerName" meta:resourcekey="ddlEmployerResource1"></asp:DropDownList>
            </td>
            <td style="width: 10%;">
                <asp:Literal ID="Literal3" runat="server" Text="äæÚ ÇáÚÞÏ:" meta:resourcekey="Literal3Resource1"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlContractType" runat="server" DataSourceID="dsAccountContractTypeObject" DataValueField="ContractTypeId" DataTextField="ContractTypeName" meta:resourcekey="ddlContractTypeResource1"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <div style="width: 100px; height: 10px" />
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Literal ID="Literal4" runat="server" Text="ÊÇÑíÎ ÇáÍÖæÑ" meta:resourcekey="Literal4Resource1"></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Startdate" />
            </td>

        </tr>
        <tr>
            <td>
                <div style="width: 100px; height: 10px" />
            </td>
        </tr>
        <tr>

            <td>
                <asp:Button ID="btnView" runat="server"
                    Text="ÚÑÖ "
                    UseSubmitBehavior="False" meta:resourcekey="btnViewResource1" />
            </td>
        </tr>

    </table>

</div>





&nbsp;&nbsp;<br />


<x:GridView ID="GrdDepartmentAttendance0" ShowHeaderWhenEmpty="True" runat="server" AutoGenerateColumns="False" DataKeyNames="LogId,EmployeeID,LogDate" OnRowCommand="grdViewEmployeeLogs_RowCommand"
    DataSourceID="dsDepartmentAttendance" SkinID="xgridviewSkinEmployee" CssClass="TableView" Caption="ÓÌáÇÊ ÍÖæÑ æÇäÕÑÇÝ ãæÙÝ" Width="98%" meta:resourcekey="GrdDepartmentAttendance0Resource1">

    <Columns>

        <asp:TemplateField ItemStyle-Width="20px" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <a href="JavaScript:divexpandcollapse('div<%# Eval("LogId") %>');">
                    <img alt="Details" id='imgdiv<%# Eval("LogId") %>' src="../Images/plus.png" />
                </a>
                <div id='div<%# Eval("LogId") %>' style="display: none;">
                    <asp:GridView ID="grdViewEmployeeLogs" runat="server" AutoGenerateColumns="False"
                        CssClass="ChildGrid" meta:resourcekey="grdViewOrdersOfCustomerResource1">
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
                            <%--  <asp:BoundField DataField="ValidRecord" HeaderText="ÕÍÉÇáÓÌá" meta:resourcekey="BoundFieldResource7">

                                <ItemStyle Width="150px" />
                            </asp:BoundField>--%>

                            <asp:TemplateField HeaderText="ÊÛííÑ" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("LogId")%>' runat="server"><img width="30" height="30" src="../images/swap.png" alt="change" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </ItemTemplate>

            <ItemStyle Width="20px"></ItemStyle>
        </asp:TemplateField>

        <asp:BoundField DataField="LogId" HeaderText="ãÓáÓá" meta:resourcekey="BoundFieldResource8" />

        <asp:BoundField DataField="BadgeNo" HeaderText="ÑÞã ÇáÈÕãÉ" SortExpression="BadgeNo" meta:resourcekey="BoundFieldResource9">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="LogDate" HeaderText="ÊÇÑíÎ ÇáÍÖæÑ" SortExpression="LogDate" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" meta:resourcekey="BoundFieldResource10">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="EmployeeName" HeaderText="ÅÓã ÇáãæÙÝ" SortExpression="EmployeeName" HtmlEncode="false" meta:resourcekey="BoundFieldResource11">
            <HeaderStyle HorizontalAlign="Center" Width="200px" />
            <ItemStyle HorizontalAlign="Center" Width="200px" />
        </asp:BoundField>

        <asp:BoundField DataField="InTime" HeaderText="æÞÊ ÇáÏÎæá" SortExpression="InTime" DataFormatString="{0:hh:mm tt}" meta:resourcekey="BoundFieldResource12">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="OutTime" HeaderText="æÞÊ ÇáÎÑæÌ" SortExpression="OutTime" DataFormatString="{0:hh:mm tt}" meta:resourcekey="BoundFieldResource13">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="StatusName" HeaderText="ÍÇáÉ ÇáÏæÇã" SortExpression="StatusName" meta:resourcekey="BoundFieldResource14">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="InLateMinutes" HeaderText="ÊÃÎíÑ ÇáÏÎæá" SortExpression="InLateMinutes" meta:resourcekey="BoundFieldResource15">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="OutLateMinutes" HeaderText="ÎÑæÌ ãÈßÑ" SortExpression="OutLateMinutes" meta:resourcekey="BoundFieldResource16">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="ActualWorkMinutes" HeaderText="æÞÊ ÇáÚãá ÇáÝÚáí" SortExpression="ActualWorkMinutes" meta:resourcekey="BoundFieldResource17">
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

<%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(178, 1) Then%>
<asp:Button ID="addManual" runat="server"
    Text="ÅÖÇÝÉ ÈÕãÉ íÏæíÉ"
    UseSubmitBehavior="False" Visible="False" meta:resourcekey="addManualResource1" />
<% End If%>
<%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(183, 1) Then%>
<asp:Button ID="processAttendance" runat="server"
    Text="ãÚÇáÌÉ ÇáÍÖæÑ æÇáÇäÕÑÇÝ"
    UseSubmitBehavior="False" Visible="False" meta:resourcekey="processAttendanceResource1" />
<% End If%>
<%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(173, 1) Then%>
<asp:Button ID="btnDevices" runat="server"
    Text="ÃÌåÒÉ ÇáÈÕãÉ"
    UseSubmitBehavior="False" Visible="False" meta:resourcekey="btnDevicesResource1" />
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