<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ApprovalPolicy.ascx.vb" Inherits="AccountAdmin_Controls_ctlApprovalPolicy" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/AccountAdmin/Controls/ctlEmployeesApprovalPolicy.ascx" TagPrefix="uc1" TagName="ctlEmployeesApprovalPolicy" %>
<table>
    <tr>
        <td>
            <asp:FormView ID="FormView1" runat="server"
                SkinID="formviewskinemployee"
                DefaultMode="Insert" Width="500px" Visible="False" meta:resourcekey="FormView1Resource1">
                <InsertItemTemplate>
                    <table width="100%" class="xformview">
                        <tr>
                            <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  «·”Ì«”… " meta:resourcekey="Literal9Resource2" />
                            </th>
                        </tr>
                        <tr>
                            <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ”Ì«”… ÃœÌœ" meta:resourcekey="Literal10Resource2" />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="  —ﬁ„ «·Õ«”» :" meta:resourcekey="Literal3Resource2"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtApprovalPolicyId" runat="server" ReadOnly="True" meta:resourcekey="txtApprovalPolicyIdResource2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="≈”„ «·”Ì«”… :" meta:resourcekey="Literal2Resource2"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtApprovalPolicyName" runat="server" Style="margin-right: 0px" Width="390px" meta:resourcekey="txtApprovalPolicyNameResource2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text="  ‰ÿ»ﬁ ⁄·Ï:" meta:resourcekey="Literal1Resource2"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTimeOffApprovalType" runat="server" meta:resourcekey="ddlTimeOffApprovalTypeResource2">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource6">...≈Œ —...</asp:ListItem>
                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource7"> «·≈Ã«“« </asp:ListItem>
                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource8"> «·≈” À‰«¡« </asp:ListItem>
                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource9"> «·≈” ∆–«‰</asp:ListItem>
                                    <asp:ListItem Value="4" meta:resourcekey="ListItemResource10"> «·Ã„Ì⁄</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0"
                                    ID="rfvDDL_Product7" Display="Dynamic"
                                    ControlToValidate="ddlTimeOffApprovalType"
                                    runat="server" Text="*"
                                    ErrorMessage="*"
                                    ForeColor="Red" meta:resourcekey="rfvDDL_Product7Resource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                    Text="Õ›Ÿ «·”Ì«”…" meta:resourcekey="Button1Resource2" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;"
                                    Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource2" />
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <table width="100%" class="xformview">
                        <tr>
                            <th colspan="4" class="caption" style="text-align: center;">
                                <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ”Ì«”…" meta:resourcekey="Literal9Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ”Ì«”… ÃœÌœ" meta:resourcekey="Literal10Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="  —ﬁ„ «·Õ«”» :" meta:resourcekey="Literal3Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtApprovalPolicyId" runat="server" ReadOnly="True" Text='<%# Bind("ApprovalPolicyId") %>' meta:resourcekey="txtApprovalPolicyIdResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="≈”„ «·”Ì«”… :" meta:resourcekey="Literal2Resource1"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtApprovalPolicyName" runat="server" Style="margin-right: 0px" Width="390px" Text='<%# Bind("ApprovalPolicyName") %>' meta:resourcekey="txtApprovalPolicyNameResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text="  ‰ÿ»ﬁ ⁄·Ï:" meta:resourcekey="Literal1Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTimeOffApprovalType" runat="server" SelectedValue='<%# Bind("TimeOffApprovalType") %>' Enabled="False" meta:resourcekey="ddlTimeOffApprovalTypeResource1">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Selected="True">...≈Œ —...</asp:ListItem>
                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource2"> «·≈Ã«“« </asp:ListItem>
                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource3"> «·≈” À‰«¡« </asp:ListItem>
                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource4"> «·≈” ∆–«‰</asp:ListItem>
                                    <asp:ListItem Value="4" meta:resourcekey="ListItemResource5"> «·Ã„Ì⁄</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0"
                                    ID="rfvDDL_Product2" Display="Dynamic"
                                    ControlToValidate="ddlTimeOffApprovalType"
                                    runat="server" Text="*"
                                    ErrorMessage="*"
                                    ForeColor="Red" meta:resourcekey="rfvDDL_Product2Resource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                    Text=" ÕœÌÀ «·”Ì«”…" meta:resourcekey="Button1Resource1" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource1" />
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>
            </asp:FormView>
            <br />
            <x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                SkinID="xgridviewSkinEmployee"
                Width="500px"
                DataKeyNames="ApprovalPathId,ApprovalPolicyId,ApproverTypeId,VersionNo"
                Caption=" «· ›«’Ì·"
                ShowFooter="True" meta:resourcekey="GridView1Resource1">
                <Columns>
                    <asp:TemplateField HeaderText="—ﬁ„ «·„”«—" ItemStyle-Width="100px" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <%# Eval("ApprovalPathSequence")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <%# Eval("ApprovalPathSequence")%>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <%# GridView1.Rows.Count + 1%>
                        </FooterTemplate>

                        <ItemStyle Width="100px"></ItemStyle>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="«·‰Ê⁄" meta:resourcekey="TemplateFieldResource4">
                        <ItemTemplate>
                            <%# Eval("ApproverTypeName")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlApproverType" runat="server" DataTextField="RoleName" DataValueField="RoleId" meta:resourcekey="ddlApproverTypeResource1">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource11">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0"
                                ID="rfvDDL_Product3" Display="Dynamic"
                                ControlToValidate="ddlApproverType"
                                runat="server" Text="*"
                                ErrorMessage="Please Select Employee"
                                ForeColor="Red" meta:resourcekey="rfvDDL_Product3Resource1"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlApproverType" runat="server" DataTextField="RoleName" DataValueField="RoleId" meta:resourcekey="ddlApproverTypeResource2">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update" meta:resourcekey="lnkUpdateResource1"></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" meta:resourcekey="lnkCancelResource1"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" CommandName="Edit" Visible='<%# If(Me.FormView1.CurrentMode = FormViewMode.Edit, True, False) %>' CssClass="edit_button" runat="server" Text="Edit" CausesValidation="False" meta:resourcekey="lnkEditResource1"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnkAddNew" runat="server" CommandName="AddNew" Text="Õ›Ÿ" meta:resourcekey="lnkAddNewResource1"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table class="xGridView" cellspacing="0" cellpadding="0" rules="cols" border="1" style="width: 800px; border-collapse: collapse;">

                        <tbody>
                            <tr>
                                <th scope="col">—ﬁ„ «·„”«—</th>
                                <th scope="col">«·œÊ—</th>
                                <th scope="col">&nbsp;</th>
                            </tr>


                            <tr style="background-color: #EFEFF1;">
                                <td>1
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlApproverType" runat="server" DataTextField="RoleName" DataValueField="RoleId" Width="200px" meta:resourcekey="ddlApproverTypeResource3">
                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource12">...≈Œ —...</asp:ListItem>
                                        <asp:ListItem Value="1" meta:resourcekey="ListItemResource13"> «·≈Ã«“« </asp:ListItem>
                                        <asp:ListItem Value="2" meta:resourcekey="ListItemResource14"> «·≈” À‰«¡« </asp:ListItem>
                                        <asp:ListItem Value="3" meta:resourcekey="ListItemResource15"> «·≈” ∆–«‰</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="0"
                                        ID="rfvDDL_Product6" Display="Dynamic"
                                        ControlToValidate="ddlApproverType"
                                        runat="server" Text="*"
                                        ErrorMessage="Please Select Employee"
                                        ForeColor="Red" meta:resourcekey="rfvDDL_Product6Resource1"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Õ›Ÿ" meta:resourcekey="btnAddResource1" />
                                </td>

                            </tr>
                        </tbody>
                    </table>

                </EmptyDataTemplate>
                <PagerSettings PageButtonCount="21" />
            </x:GridView>
            <br />
            <br />
        </td>
        <td>
            <% If Me.FormView1.CurrentMode = FormViewMode.Edit And GridView1.Rows.Count > 0 Then%>
            <uc1:ctlEmployeesApprovalPolicy runat="server" ID="ctlEmployeesApprovalPolicy" />
            <% End If%>
        </td>
    </tr>
</table>
<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
    DataKeyNames="ApprovalPolicyId" Caption="ﬁ«∆„… „”«—«  «·„Ê«›ﬁ« "
    SkinID="xgridviewSkinEmployee" Width="50%" CssClass="TableView" Style="text-align: center;" ShowHeaderWhenEmpty="True" DataSourceID="dsApprovalPolicys" meta:resourcekey="GridView2Resource1">
    <Columns>
        <asp:BoundField DataField="ApprovalPolicyId" HeaderText="—ﬁ„ «·Õ«”»" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />
        <asp:BoundField DataField="ApprovalPolicyName" HeaderText="„”«— «·„Ê«›ﬁ…" meta:resourcekey="BoundFieldResource2" />
        <asp:BoundField DataField="TimeOffApprovalType" HeaderText="Ì‰ÿ»ﬁ ⁄·Ï" meta:resourcekey="BoundFieldResource3" />
        <asp:TemplateField HeaderText=" ⁄œÌ·" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Õ–›" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
            <ItemTemplate>
                <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
</x:GridView>
<br />
<table style="width: 100%">
    <tr>
        <td>&nbsp
         <asp:Button ID="btnAddApprovalPolicy" runat="server"
             Text="≈÷«›…" Width="75px" UseSubmitBehavior="False" meta:resourcekey="btnAddApprovalPolicyResource1" />
        </td>
    </tr>
</table>


<asp:ObjectDataSource ID="dsPolicy" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOPolicy">
    <SelectParameters>
        <asp:Parameter DefaultValue="1" Name="UserAccountId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="dsApprovalPolicys" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetApprovalPolicysDataset" TypeName="ATS.BO.Framework.BOApprovalPolicy">
    <SelectParameters>
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="100" Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
