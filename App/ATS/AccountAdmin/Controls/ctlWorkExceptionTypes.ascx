<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlWorkExceptionTypes.ascx.vb" Inherits="AccountAdmin_Controls_ctlWorkExceptionTypes" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="WorkExceptionTypeId"
            DataSourceID="dsAccountWorkExceptionTypeObject" SkinID="xgridviewSkinEmployee" AllowSorting="True" Width="98%" Caption="����� ����������" CssClass="tableView" meta:resourcekey="GridView1Resource1">
            <Columns>

                <asp:BoundField DataField="WorkExceptionTypeId" HeaderText="�����" InsertVisible="False"
                    ReadOnly="True" SortExpression="WorkExceptionTypeId" meta:resourcekey="BoundFieldResource1">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                </asp:BoundField>


                <asp:BoundField DataField="WorkExceptionTypeName" HeaderText="" SortExpression="WorkExceptionTypeName" meta:resourcekey="BoundFieldResource3">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="WorkExceptionTypeNameEN" HeaderText="" SortExpression="WorkExceptionTypeNameEN" meta:resourcekey="BoundFieldResource2">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>


                <asp:TemplateField HeaderText="�����" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit"
                            CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="���" ShowHeader="False" meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete"
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                            OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
                </asp:TemplateField>

            </Columns>
        </x:GridView>
        <asp:ObjectDataSource ID="dsAccountWorkExceptionTypeObject" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetWorkExceptionTypesDataset" TypeName="ATS.BO.Framework.BOWorkExceptionType">
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
                <asp:Parameter DefaultValue="1000" Name="PageSize" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </ContentTemplate>
</asp:UpdatePanel>
&nbsp;
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:FormView ID="FormView1" runat="server" DataKeyNames="WorkExceptionTypeId"
            DefaultMode="Insert" SkinID="formviewSkinEmployee" Width="416px" OnDataBound="FormView1_DataBound" meta:resourcekey="FormView1Resource1">
            <EditItemTemplate>
                <table width="100%" class="xview">
                    <tr>
                        <th class="caption" colspan="2" style="width: 20%; height: 21px">
                            <asp:Literal ID="Literal1" runat="server" Text="������� �����" meta:resourcekey="Literal1Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <th class="FormViewSubHeader" colspan="2" style="width: 20%; height: 21px">
                            <asp:Literal ID="Literal2" runat="server" Text="����� �����" meta:resourcekey="Literal2Resource1" />
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
                            <asp:TextBox ID="txtAccountWorkExceptionTypeId" runat="server" ReadOnly="True" Text='<%# Bind("WorkExceptionTypeId") %>' meta:resourcekey="txtAccountWorkExceptionTypeIdResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text="��� �����" meta:resourcekey="Literal4Resource1" />
                        </td>
                        <td style="width: 75%; height: 26px;">
                            <asp:TextBox
                                ID="AccountWorkExceptionTypeTextBox" runat="server"
                                Text='<%# Bind("WorkExceptionTypeName") %>' MaxLength="50" Width="176px" meta:resourcekey="AccountWorkExceptionTypeTextBoxResource1"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="AccountWorkExceptionTypeTextBox"
                                    Display="Dynamic" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text="��� �����" meta:resourcekey="Literal6Resource1" />
                        </td>
                        <td style="width: 75%; height: 26px;">
                            <asp:TextBox
                                ID="AccountWorkExceptionTypeENTextBox" runat="server"
                                Text='<%# Bind("WorkExceptionTypeNameEN")%>' MaxLength="50" Width="176px" meta:resourcekey="AccountWorkExceptionTypeTextBoxResource1"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="AccountWorkExceptionTypeENTextBox"
                                    Display="Dynamic" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></td>
                    </tr>

                    <tr>
                        <td class="FormViewLabelCell" style="width: 30%; height: 26px; display: none" align="right">
                            <asp:Literal ID="Literal5" runat="server" Text="����" meta:resourcekey="Literal5Resource1" />
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 30%; height: 26px;"></td>
                        <td style="width: 75%; height: 26px; padding-bottom: 5px;">
                            <asp:Button ID="Update" runat="server" OnClick="DataUpdate" Text="����� " Width="68px" meta:resourcekey="UpdateResource1" />
                            <asp:Button ID="Cancel" runat="server" OnClientClick="ReloadSamepage(); return false;" Text="�����" Width="68px" meta:resourcekey="CancelResource1" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table width="100%" class="xview">
                    <tr>
                        <th class="caption" colspan="2" style="height: 21px" width="30%">
                            <asp:Literal ID="Literal1" runat="server" Text="������� �����" meta:resourcekey="Literal1Resource2" />
                        </th>
                    </tr>
                    <tr>
                        <th class="FormViewSubHeader" colspan="2" style="height: 21px" width="30%">
                            <asp:Literal ID="Literal3" runat="server" Text="����� ���" meta:resourcekey="Literal3Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource2"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="��� �����" meta:resourcekey="Literal2Resource2" />
                        </td>
                        <td style="width: 75%; height: 26px;">
                            <asp:TextBox
                                ID="AccountWorkExceptionTypeTextBox" runat="server"
                                Text='<%# Bind("AccountWorkExceptionType") %>' MaxLength="50" Width="176px" meta:resourcekey="AccountWorkExceptionTypeTextBoxResource2"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="AccountWorkExceptionTypeTextBox"
                                    Display="Dynamic" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text="��� �����" meta:resourcekey="Literal6Resource1" />
                        </td>
                        <td style="width: 75%; height: 26px;">
                            <asp:TextBox
                                ID="AccountWorkExceptionTypeENTextBox" runat="server"
                                Text='<%# Bind("WorkExceptionTypeNameEN")%>' MaxLength="50" Width="176px" meta:resourcekey="AccountWorkExceptionTypeTextBoxResource1"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="AccountWorkExceptionTypeENTextBox"
                                    Display="Dynamic" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></td>
                    </tr>

                    <tr>
                        <td style="height: 26px;" class="formviewlabelcell" width="30%"></td>
                        <td style="width: 75%; height: 26px; padding-bottom: 5px;">
                            <asp:Button ID="Add" runat="server" OnClick="DataAdd" Text=" ���" Width="68px" meta:resourcekey="AddResource1" />
                              <asp:Button ID="Cancel" runat="server" OnClientClick="ReloadSamepage(); return false;" Text="�����" Width="68px" meta:resourcekey="CancelResource1" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>

        </asp:FormView>
    </ContentTemplate>
</asp:UpdatePanel>
