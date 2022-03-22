<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountRoleList.ascx.vb" Inherits="AccountAdmin_Controls_ctlAccountRoleList" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="RoleId"
            Width="30%" SkinID="xgridviewSkinEmployee" CssClass="tableView" AllowSorting="True" Caption="����� �������" meta:resourcekey="GridView1Resource1" DataSourceID="dsRoleds">
            <Columns>
                <asp:BoundField DataField="RoleId" HeaderText="�����" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />

                <asp:BoundField DataField="RoleName" HeaderText="��� ��������" meta:resourcekey="BoundFieldResource2" />

                <asp:TemplateField HeaderText="�����" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit"
                            Visible='<%# If(Eval("RoleName").ToString().Equals("Administrator") Or Eval("RoleName").ToString().Equals("User"), False, True) %>'
                            CausesValidation="False" ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="���" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete"
                            Visible='<%# If(Eval("RoleName").ToString().Equals("Administrator") Or Eval("RoleName").ToString().Equals("User"), False, True) %>'
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                            OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False" ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
                </asp:TemplateField>

            </Columns>
        </x:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
<br />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:FormView ID="FormView1" runat="server" DataKeyNames="RoleId"
            DefaultMode="Insert" SkinID="formviewSkinEmployee" Width="550px" meta:resourcekey="FormView1Resource1">
            <EditItemTemplate>
                <table style="width: 100%" class="xview">
                    <tr>
                        <th class="caption" colspan="2" width="20%" style="height: 21px">
                            <asp:Literal ID="Literal1" runat="server" Text="������� �����" meta:resourcekey="Literal1Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" style="height: 21px" width="20%">
                            <asp:Literal ID="Literal2" runat="server" Text="�����" meta:resourcekey="Literal2Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text=" ��� ������:" meta:resourcekey="Literal8Resource1" />

                        </td>
                        <td>
                            <asp:TextBox ID="txtRoleId" runat="server" ReadOnly="True" Text='<%# Bind("RoleId") %>' meta:resourcekey="txtRoleIdResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Literal ID="Literal14" runat="server" Text="��� �����" meta:resourcekey="Literal14Resource1" />
                        </td>
                        <td >
                            <asp:TextBox
                                ID="RoleTextBox" runat="server" Text='<%# Bind("RoleName") %>' MaxLength="50"
                                Width="176px" meta:resourcekey="RoleTextBoxResource1"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RoleTextBox"
                                    Display="Dynamic" ErrorMessage="*" Width="1px" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text="��� ����� �� LADP:" meta:resourcekey="Literal6Resource1" />
                        </td>
                        <td>
                            <asp:TextBox ID="LDAPRoleTextBox" runat="server" MaxLength="50"
                                Text='<%# Bind("LDAPRole") %>' Width="176px" meta:resourcekey="LDAPRoleTextBoxResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkIsApprover" runat="server" Checked='<%# Eval("IsApprover") %>' Text="����� ���������" meta:resourcekey="chkIsApproverResource1" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsMasterApprover" runat="server" Checked='<%# Eval("IsMasterApprover") %>' Text=" ������ �����" meta:resourcekey="chkIsMasterApproverResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 26px; width: 30%;" width="20%" align="right"></td>
                        <td style="height: 26px; width: 70%; padding-bottom: 5px;">
                            <asp:Button ID="Button1" runat="server" OnClick="DataUpdate" Text="�����" Width="68px" meta:resourcekey="Button1Resource1" />&nbsp;
                            <asp:Button ID="Button2" runat="server" CommandName="Cancel" Text="����� �����" Width="68px" meta:resourcekey="Button2Resource1" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table style="width: 100%" class="xview">
                    <tr>
                        <th class="caption" colspan="2" style="height: 21px" width="20%">
                            <asp:Literal ID="Literal1" runat="server" Text="������� �����" meta:resourcekey="Literal1Resource2" />
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" style="height: 21px" width="20%">
                            <asp:Literal ID="Literal3" runat="server" Text="�����" meta:resourcekey="Literal3Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text=" ��� ������:" meta:resourcekey="Literal8Resource2" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtRoleId" runat="server" ReadOnly="True" Text='<%# Bind("RoleId") %>' meta:resourcekey="txtRoleIdResource2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text="��� �����" meta:resourcekey="Literal5Resource1" />
                        </td>
                        <td>
                            <asp:TextBox ID="RoleTextBox" runat="server" Text='<%# Bind("RoleName") %>' MaxLength="50"
                                Width="176px" meta:resourcekey="RoleTextBoxResource2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RoleTextBox"
                                ErrorMessage="*" Display="Dynamic" Width="1px" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>


                            <asp:Literal ID="Literal7" runat="server" Text="��� ����� �� LADP" meta:resourcekey="Literal7Resource1" />
                        </td>
                        <td>
                            <asp:TextBox
                                ID="LDAPRoleTextBox" runat="server" MaxLength="50"
                                Text='<%# Bind("LDAPRole") %>' Width="176px" meta:resourcekey="LDAPRoleTextBoxResource2"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkIsApprover" runat="server" Text="����� ���������" meta:resourcekey="chkIsApproverResource2" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsMasterApprover" runat="server" Text=" ������ �����" meta:resourcekey="chkIsMasterApproverResource2" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 26px; width: 30%;" width="20%" align="right"></td>
                        <td style="height: 26px; width: 70%; padding-bottom: 5px;" width="80%">
                            <asp:Button ID="btnAdd" runat="server" OnClick="DataAdd" Text="��� " Width="68px" meta:resourcekey="btnAddResource1" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </asp:FormView>

    </ContentTemplate>

</asp:UpdatePanel>
   <asp:ObjectDataSource ID="dsRoleds" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetList" TypeName="ATS.BO.Framework.BORole1">
        </asp:ObjectDataSource>