<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountLocationList.ascx.vb" Inherits="AccountAdmin_Controls_ctlAccountLocationList" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="LocationId"
            DataSourceID="dsAccountLocationObject" SkinID="xgridviewSkinEmployee" AllowSorting="True" Width="98%" Caption="ãæÇÞÚ ÇáÚãá" CssClass="tableView" meta:resourcekey="GridView1Resource1">
            <Columns>
                <asp:BoundField DataField="LocationId" HeaderText="ÇáÑÞã" InsertVisible="False"
                    ReadOnly="True" SortExpression="LocationId" meta:resourcekey="BoundFieldResource1">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField SortExpression="LocationName" meta:resourcekey="TemplateFieldResource1">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LocationName") %>' __designer:wfdid="w31" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text="ÅÓã ÇáãæÞÚ" CommandArgument="LocationName" CommandName="Sort" CausesValidation="False" meta:resourcekey="LinkButton2Resource1"></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>' __designer:wfdid="w30" meta:resourcekey="Label1Resource1"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                    <ItemStyle Width="90%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ÊÚÏíá" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit"
                            CausesValidation="False" ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ÍÐÝ" ShowHeader="False" meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete"
                            Visible='<%# If(Eval("LocationId").ToString().Equals("1") , False, True) %>'
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                            OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False" ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </x:GridView>
        <asp:ObjectDataSource ID="dsAccountLocationObject" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetList" TypeName="ATS.BO.Framework.BOLocation"></asp:ObjectDataSource>
    </ContentTemplate>
</asp:UpdatePanel>
&nbsp;
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:FormView ID="FormView1" runat="server" DataKeyNames="LocationId"
            DefaultMode="Insert" SkinID="formviewSkinEmployee" Width="416px" OnDataBound="FormView1_DataBound" meta:resourcekey="FormView1Resource1">
            <EditItemTemplate>
                <table width="100%" class="xview">
                    <tr>
                        <th class="caption" colspan="2" style="width: 20%; height: 21px">
                            <asp:Literal ID="Literal1" runat="server" Text="ãÚáæãÇÊ ÇáãæÞÚ" meta:resourcekey="Literal1Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <th class="FormViewSubHeader" colspan="2" style="width: 20%; height: 21px">
                            <asp:Literal ID="Literal2" runat="server" Text="ÊÚÏíá ÇáãæÞÚ" meta:resourcekey="Literal2Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text=" ÑÞã ÇáÍÇÓÈ:" meta:resourcekey="Literal8Resource1" />

                        </td>
                        <td>
                            <asp:TextBox ID="txtAccountLocationId" runat="server" ReadOnly="True" Text='<%# Bind("LocationId") %>' meta:resourcekey="txtAccountLocationIdResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Literal ID="Literal4" runat="server" Text="ÅÓã ÇáãæÞÚ" meta:resourcekey="Literal4Resource1" />
                        </td>
                        <td>
                            <asp:TextBox
                                ID="AccountLocationTextBox" runat="server"
                                Text='<%# Bind("LocationName") %>' MaxLength="50" Width="176px" meta:resourcekey="AccountLocationTextBoxResource1"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="AccountLocationTextBox"
                                    Display="Dynamic" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text="ãÚØá" meta:resourcekey="Literal5Resource1" />
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 20%; height: 26px;"></td>
                        <td style="width: 75%; height: 26px; padding-bottom: 5px;">
                            <asp:Button ID="Update" runat="server" OnClick="DataUpdate" Text="ÊÍÏíË " Width="68px" meta:resourcekey="UpdateResource1" />
                            <asp:Button ID="Cancel" runat="server" CommandName="Cancel" Text="ÇáÛÇÁ" Width="68px" meta:resourcekey="CancelResource1" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table width="100%" class="xview">
                    <tr>
                        <th class="caption" colspan="2" style="height: 21px" width="30%">
                            <asp:Literal ID="Literal1" runat="server" Text="ãÚáæãÇÊ ÇáãæÞÚ" meta:resourcekey="Literal1Resource2" />
                        </th>
                    </tr>
                    <tr>
                        <th class="FormViewSubHeader" colspan="2" style="height: 21px" width="30%">
                            <asp:Literal ID="Literal3" runat="server" Text="ÅÖÇÝÉ ãæÞÚ" meta:resourcekey="Literal3Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource2"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="ÅÓã ÇáãæÞÚ" meta:resourcekey="Literal2Resource2" />
                        </td>
                        <td>
                            <asp:TextBox
                                ID="AccountLocationTextBox" runat="server"
                                Text='<%# Bind("AccountLocation") %>' MaxLength="50" Width="176px" meta:resourcekey="AccountLocationTextBoxResource2"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="AccountLocationTextBox"
                                    Display="Dynamic" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 26px;" width="30%"></td>
                        <td style="width: 75%; height: 26px; padding-bottom: 5px;">
                            <asp:Button ID="Add" runat="server" OnClick="DataAdd" Text=" ÍÝÙ" Width="68px" meta:resourcekey="AddResource1" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>

        </asp:FormView>
    </ContentTemplate>
</asp:UpdatePanel>
