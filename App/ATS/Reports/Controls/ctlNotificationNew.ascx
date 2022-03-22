<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlNotificationNew.ascx.vb" Inherits="Employee_Controls_ctlctlNotificationNew" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<script lang="c#">
    $('#ReportViewer1_fixedTable tbody tr').attr("align", "center");
</script>

    <table id="tbl1" runat="server" class="xAdminOption" width="1100px" style="">
            <tr>
        <th colspan="6" class="caption" style="text-align:center;">
            <asp:Literal ID="Literal9" runat="server" Text="ÇáÌÒÇÁÇÊ" />
        </th>
    </tr>
    <tr>
        <th colspan="6" class="FormViewSubHeader" style="text-align:center;">
            <asp:Literal ID="Literal10" runat="server" Text="Ýí åÐÉ ÇáÕÝÍÉ íãßäß ÇÕÏÇÑ ÇáÌÒÇÁÇÊ ãä ÇáäÙÇã"  />
        </th>
    </tr>

        <%-- <tr>

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
        </tr>--%>

        <tr>
            <td>
                <asp:Literal ID="Literal3" runat="server" Text="ÇáÞÓã : "></asp:Literal>
            </td>
            <td>
                <uc1:ctlDepartmentTree runat="server" ID="ctlDepartmentTree" IsRequired="true"/>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Literal ID="Literal1" runat="server" Text="äæÚ ÇáÌÒÇÁ"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Value="0" Text=".............."></asp:ListItem>
                    <asp:ListItem Value="1" Text="ÛíÇÈ íæã"></asp:ListItem>
                    <asp:ListItem Value="2" Text="ÛíÇÈ äÕÝ íæã"></asp:ListItem>
                    <asp:ListItem Value="3" Text="ÊÃÎíÑ"></asp:ListItem>
                    <asp:ListItem Value="4" Text="ãÚÝí"></asp:ListItem>
                </asp:DropDownList>
                  <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="RequiredFieldValidator2" Display="Dynamic"
                        ControlToValidate="ddlType"
                        runat="server" Text="ÅÎÊÑ äæÚ ÇáÌÒÇÁ"
                        ErrorMessage="ÅÎÊÑ äæÚ ÇáÌÒÇÁ"
                        ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
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
                <asp:Literal ID="Literal4" runat="server" Text=" ÊÇÑíÎ ÇáÈÏÇíÉ :" ></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Startdate" SelectedDate="<%# DateAdd(DateInterval.Month, -1, Now) %>" />
     
            </td>
            <td>

                <asp:Literal ID="Literal5" runat="server" Text="  ÊÇÑíÎ ÇáäåÇíÉ :" ></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Enddate" SelectedDate="<%# Now.Date %>" />
                <asp:CustomValidator ID="CustomValidator2" runat="server"
                    ControlToValidate="EndDate$TextBox1"
                    ErrorMessage="ÊÇÑíÎ ÇáäåÇíÉ íÌÈ  Çä íßæä ÇßÈÑ ãä Çæ íÓÇæí ÊÇÑíÎ ÇáÈÏÇíÉ !"
                    ClientValidationFunction="CompareDate"></asp:CustomValidator>

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

<br />

<x:GridView ID="gvTreeNodes" runat="server" Width="1100px" GridLines="None" ShowHeaderWhenEmpty="True"
    DataKeyNames="EmployeeID,EmployeeNo,EmployeeName,DepartmentName,JobTitle,SType,Days,StartDate,EndDate"
    AutoGenerateColumns="False"
    SkinID="xgridviewSkinEmployee" CssClass="TableView"
    Caption="ÞÇÆãÉ ÇáÇäÐÇÑÇÊ" Font-Names="Arial" 
    OnPageIndexChanging="gvTreeNodes_PageIndexChanging"
    AllowSorting="True" DataSourceID="dsSanctionNotification" >
    <Columns>
        <asp:BoundField DataField="EmployeeNo" HeaderText="ÑÞã ÇáãæÙÝ" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="ÅÓã ÇáãæÙÝ" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="DepartmentName" SortExpression="DepartmentName" HeaderText="ÇáÞÓã">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="JobTitle" SortExpression="JobTitle" HeaderText="ÇáæÙíÝÉ">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="SType" SortExpression="SType" HeaderText="äæÚ ÇáÌÒÇÁ" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="Days" SortExpression="Days" HeaderText="ÚÏÏ ÃíÇã">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="StartDate" SortExpression="StartDate" HeaderText="ÊÇÑíÎ ÇáÈÏÇíÉ">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="EndDate" SortExpression="EndDate" HeaderText="ÊÇÑíÎ ÇáäåÇíÉ" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

       <%-- <asp:TemplateField >
            <ItemTemplate>
                <asp:Button ID="ViewButton" runat="server"
                    CommandName="View"
                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    Text="ÚÑÖ" />
            </ItemTemplate>
        </asp:TemplateField>--%>

        <asp:TemplateField >
            <ItemTemplate>
                <asp:Button ID="IssueButton" runat="server"
                    CommandName="Issue"
                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    Text="ÅÕÏÇÑ"  />
            </ItemTemplate>
        </asp:TemplateField>

      <%--  <asp:TemplateField >
            <ItemTemplate>
                <asp:Button ID="ReceiptButton" runat="server"
                    CommandName="Receipt"
                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    Text="Êã ÇáÊÓáíã"  />
            </ItemTemplate>
        </asp:TemplateField>--%>


    </Columns>

</x:GridView>



<div align="center">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" DocumentMapWidth="100%" Font-Names="Arial" Font-Size="8pt" Height="1100px" Visible="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="755px" ShowToolBar="False" meta:resourcekey="ReportViewer1Resource1">
        <LocalReport ReportPath="" EnableExternalImages="True">
        </LocalReport>
    </rsweb:ReportViewer>

</div>








<asp:SqlDataSource ID="dsManagers" runat="server" ConnectionString="<%$ ConnectionStrings:ATSConnectionString %>" SelectCommand="[Managements].[SpGetManagersForSignature]" SelectCommandType="StoredProcedure"></asp:SqlDataSource>







<asp:ObjectDataSource ID="dsSanctionNotification" runat="server" OldValuesParameterFormatString="original_{0}"
                              SelectMethod="GetSanctionNotification" TypeName="ATS.BO.Framework.BOAttendanceLog">
            <SelectParameters>
                <asp:Parameter Name="DepartmentId" Type="Int32" />
                <asp:Parameter Name="BegDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" DefaultValue="" />
                <asp:Parameter Name="OptionNo" Type="Int32" DefaultValue="0" />
            </SelectParameters>
        </asp:ObjectDataSource>