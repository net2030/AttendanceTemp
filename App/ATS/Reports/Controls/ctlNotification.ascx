<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlNotification.ascx.vb" Inherits="Employee_Controls_ctlctlNotification" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script lang="c#">
    $('#ReportViewer1_fixedTable tbody tr').attr("align", "center");
</script>
<div id="Div1" runat="server" style="border-style: solid; border-color: inherit; border-width: medium; width: 700px; height: 113px;">
    <table class="xAdminOption" width="700px" style="">


        <tr>

            <td>

                <asp:Literal ID="Literal1" runat="server" Text="ØÑíÞÉ ÇáÝÑÒ :" meta:resourcekey="Literal1Resource1"></asp:Literal>
            </td>
            <td>
                <asp:RadioButton ID="rdAll" runat="server" GroupName="Opti" Text="Çáßá" Checked="True" meta:resourcekey="rdAllResource1" />
            </td>
            <td>
                <asp:RadioButton ID="rdRegistered" runat="server" GroupName="Opti" Text=" Êã ÇÕÏÇÑåÇ" meta:resourcekey="rdRegisteredResource1" />
            </td>
            <td>
                <asp:RadioButton ID="rdNotRegistered" runat="server" GroupName="Opti" Text="áã íÊã ÅÕÏÇÑåÇ" meta:resourcekey="rdNotRegisteredResource1" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal2" runat="server" Text="ÇáãÎæá ÈÇáÊæÞíÚ:" meta:resourcekey="Literal2Resource1"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlManagers" runat="server" DataSourceID="dsManagers" DataTextField="EmployeeName" DataValueField="JobTitle" meta:resourcekey="ddlManagersResource1">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAdd" runat="server" OnClick="btnView"
                    Text="ÚÑÖ"
                    UseSubmitBehavior="False" meta:resourcekey="btnAddResource1" />

            </td>
        </tr>
    </table>
</div>
<br />

<x:GridView ID="gvTreeNodes" runat="server" Width="90%" GridLines="None"
    DataKeyNames="EmployeeID,EmployeeNo,EmployeeName,DepartmentName,Title,AbsenceType,Days,StartDate,EndDate,StartDate1,EndDate1"
    AutoGenerateColumns="False"
    SkinID="xgridviewSkinEmployee" CssClass="TableView"
    Caption="ÞÇÆãÉ ÇáÇäÐÇÑÇÊ" Font-Names="Arial" Font-Size="Medium"
    OnPageIndexChanging="gvTreeNodes_PageIndexChanging"
    AllowSorting="True" meta:resourcekey="gvTreeNodesResource1">
    <Columns>
        <asp:BoundField DataField="EmployeeNo" HeaderText="ÑÞã ÇáãæÙÝ" meta:resourcekey="BoundFieldResource1">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="EmployeeName" HeaderText="ÅÓã ÇáãæÙÝ" meta:resourcekey="BoundFieldResource2">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="DepartmentName" HeaderText="ÇáÞÓã" meta:resourcekey="BoundFieldResource3">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="Title" HeaderText="ÇáæÙíÝÉ" meta:resourcekey="BoundFieldResource4">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="AbsenceType" HeaderText="äæÚ ÇáÛíÇÈ" meta:resourcekey="BoundFieldResource5">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="Days" HeaderText="ÚÏÏ ÃíÇã" meta:resourcekey="BoundFieldResource6">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="StartDate" HeaderText="ÊÇÑíÎ ÇáÈÏÇíÉ" meta:resourcekey="BoundFieldResource7">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="EndDate" HeaderText="ÊÇÑíÎ ÇáäåÇíÉ" meta:resourcekey="BoundFieldResource8">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:Button ID="ViewButton" runat="server"
                    CommandName="View"
                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    Text="ÚÑÖ" meta:resourcekey="ViewButtonResource1" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
            <ItemTemplate>
                <asp:Button ID="IssueButton" runat="server"
                    CommandName="Issue"
                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    Text="ÅÕÏÇÑ" meta:resourcekey="IssueButtonResource1" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
            <ItemTemplate>
                <asp:Button ID="ReceiptButton" runat="server"
                    CommandName="Receipt"
                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    Text="Êã ÇáÊÓáíã" meta:resourcekey="ReceiptButtonResource1" />
            </ItemTemplate>
        </asp:TemplateField>


    </Columns>

</x:GridView>



<div align="center">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" DocumentMapWidth="100%" Font-Names="Arial" Font-Size="8pt" Height="1100px" Visible="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="755px" ShowToolBar="False" meta:resourcekey="ReportViewer1Resource1">
        <LocalReport ReportPath="" EnableExternalImages="True">
        </LocalReport>
    </rsweb:ReportViewer>

</div>








<asp:SqlDataSource ID="dsManagers" runat="server" ConnectionString="<%$ ConnectionStrings:ATSConnectionString %>" SelectCommand="[Managements].[SpGetManagersForSignature]" SelectCommandType="StoredProcedure"></asp:SqlDataSource>






