<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountPagePermissionList.ascx.vb" Inherits="AccountAdmin_Controls_ctlAccountPagePermissionList" %>
<%--<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="c1i" %>--%>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="fieldset" style="width: 96%; margin-left: 6px;" align="left">
            <table class="xFormView" width="98%">
                <tr>
                    <td width="5%" class="FormViewSubHeader" style="padding-bottom: 5px" align="right" valign="middle">
                        <asp:Label ID="lblSelectRole" runat="server" Text="ÍÏÏ ÇáÏæÑ"
                            Font-Bold="True" Font-Size="11px" Width="78px" CssClass="HighlightedText" meta:resourcekey="lblSelectRoleResource1"></asp:Label>
                    </td>
                    <td style="padding-bottom: 5px">
                        <asp:DropDownList ID="ddlAccountRoleId" runat="server" DataSourceID="dsAccountRoleObject"
                            DataTextField="RoleName" DataValueField="RoleId" AutoPostBack="True" OnSelectedIndexChanged="ddlAccountRoleId_SelectedIndexChanged" Width="250px" meta:resourcekey="ddlAccountRoleIdResource1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="display:none">
                    <td width="5%" class="FormViewSubHeader" style="padding-top: 5px" valign="middle" align="right">
                        <asp:Label ID="Label1" runat="server" Text="ÕÝÍÉ ÇáÈÏÁ"
                            Font-Bold="True" Font-Size="11px" Width="78px" CssClass="HighlightedText" meta:resourcekey="Label1Resource1"></asp:Label>
                    </td>
                    <td style="padding-top: 5px">
                        <asp:DropDownList ID="ddlDefaultAccountPageId" runat="server"
                            DataTextField="SystemCategoryPageDescription" DataValueField="SystemSecurityCategoryPageId" Width="250px" AppendDataBoundItems="True" meta:resourcekey="ddlDefaultAccountPageIdResource1">
                            <asp:ListItem Selected="True" Text="ÅÎÊÑ" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <asp:ObjectDataSource ID="dsAccountRoleObject" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BORole1"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="dsDefaultAccountPageObject" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetSystemSecurityCategoryPagesByIsSiteMapPage" TypeName="ATS.BO.Framework.BOGatepass" DeleteMethod="DeleteAccountPagePermissionId" InsertMethod="UpdateAccountPagePermission">
            <DeleteParameters>
                <asp:Parameter Name="Original_AccountPagePermissionId" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="True" Name="IsSiteMapPage" Type="Boolean" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="AccountId" Type="Int32" />
                <asp:Parameter Name="AccountRoleId" Type="Int32" />
                <asp:Parameter Name="SystemSecurityCategoryPageId" Type="Int32" />
                <asp:Parameter Name="SystemPermissionId" Type="Int32" />
                <asp:Parameter Name="ShowAllData" Type="Boolean" />
                <asp:Parameter Name="ShowMyData" Type="Boolean" />
                <asp:Parameter Name="ShowMyProjectsData" Type="Boolean" />
                <asp:Parameter Name="ShowMyTeamsData" Type="Boolean" />
                <asp:Parameter Name="TillDate" Type="DateTime" />
                <asp:Parameter Name="TillHours" Type="Int32" />
                <asp:Parameter Name="Original_AccountPagePermissionId" Type="Int32" />
                <asp:Parameter Name="IsDelete" Type="Boolean" />
            </InsertParameters>
        </asp:ObjectDataSource>

        <x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="SystemSecurityCategoryPageId,IsAllowList,IsAllowAdd,IsAllowEdit,IsAllowDelete,IsShowTillOptions,IsShowDataOptions" DataSourceID="dsAccountPagePermissionObject"
            PageSize="500" Caption="ÇáÕáÇÍíÇÊ" SkinID="xgridviewSkinEmployee" Width="50%" meta:resourcekey="GridView1Resource1">
            <Columns>
                <asp:TemplateField SortExpression="SystemSecurityCategory" HeaderText="ÇáÝÆÉ" meta:resourcekey="TemplateFieldResource1">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SystemSecurityCategory") %>' __designer:wfdid="w67" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" Text="ÇáÝÆÉ" CommandArgument="SystemSecurityCategory" CommandName="Sort" CausesValidation="False" meta:resourcekey="LinkButton3Resource1"></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemStyle VerticalAlign="Middle" Width="15%" />
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("SystemSecurityCategory") %>' __designer:wfdid="w66" meta:resourcekey="Label4Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField InsertVisible="False" SortExpression="SystemSecurityCategoryPageId"
                    Visible="False" meta:resourcekey="TemplateFieldResource2">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("SystemSecurityCategoryPageId") %>' __designer:wfdid="w74" meta:resourcekey="Label1Resource2"></asp:Label>
                    </EditItemTemplate>
                    <ItemStyle Width="0%" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("SystemSecurityCategoryPageId") %>' __designer:wfdid="w73" meta:resourcekey="Label2Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="SystemCategoryPageDescription" HeaderText="ÅÓã ÇáÕÝÍÉ" meta:resourcekey="TemplateFieldResource3">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SystemCategoryPageDescription") %>' __designer:wfdid="w76" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" Text="ÅÓã ÇáÕÝÍÉ" CommandArgument="SystemCategoryPageDescription" CommandName="Sort" CausesValidation="False" meta:resourcekey="LinkButton4Resource1"></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemStyle VerticalAlign="Middle" Width="20%" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("SystemCategoryPageDescription") %>' __designer:wfdid="w75" meta:resourcekey="Label1Resource3"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ÍÏæÏ ÇáÕáÇÍíÉ" meta:resourcekey="TemplateFieldResource4">
                    <HeaderTemplate>
                        <asp:Label ID="lblPermissions" runat="server" Text="ÍÏæÏ ÇáÕáÇÍíÉ" __designer:wfdid="w25" meta:resourcekey="lblPermissionsResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemStyle VerticalAlign="Middle" Width="45%" />
                    <ItemTemplate>
                        <table class="xTableWithoutBorder" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td style="WIDTH: 100px; HEIGHT: auto; TEXT-ALIGN: left" class="labeltext">
                                        <asp:CheckBox ID="chkListView" runat="server"
                                            Text="ÚÑÖ/ÞÇÆãÉ" Width="100px"
                                            __designer:wfdid="w1" meta:resourcekey="chkListViewResource1"></asp:CheckBox>
                                    </td>
                                    <td style="WIDTH: 150px; HEIGHT: auto; TEXT-ALIGN: left"
                                        class="formviewlabelcell">
                                        <asp:CheckBox ID="chkListViewShowAllData" runat="server" Text="ÚÑÖ ÇáãæÙÝíä" Width="150px"
                                            __designer:wfdid="w2" meta:resourcekey="chkListViewShowAllDataResource1"></asp:CheckBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="WIDTH: 100px; HEIGHT: auto; TEXT-ALIGN: left"
                                        class="formviewlabelcell">
                                        <asp:CheckBox ID="chkAdd" runat="server"
                                            Text="ÅÖÇÝÉ" Width="100px" __designer:wfdid="w7" meta:resourcekey="chkAddResource1"></asp:CheckBox>
                                    </td>


                                </tr>
                                <tr>
                                    <td style="WIDTH: 100px; HEIGHT: auto; TEXT-ALIGN: left"
                                        class="formviewlabelcell">
                                        <asp:CheckBox ID="chkEdit"
                                            runat="server" Text="ÊÚÏíá" Width="90px" __designer:wfdid="w13" meta:resourcekey="chkEditResource1"></asp:CheckBox>
                                    </td>


                                </tr>
                                <tr>
                                    <td style="WIDTH: 100px; HEIGHT: auto; TEXT-ALIGN: left"
                                        class="formviewlabelcell">
                                        <asp:CheckBox ID="chkDelete" runat="server" Text="ÍÐÝ " Width="90px" __designer:wfdid="w19" meta:resourcekey="chkDeleteResource1"></asp:CheckBox>
                                    </td>



                                </tr>
                            </tbody>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </x:GridView>
        <asp:ObjectDataSource ID="dsAccountPagePermissionObject" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetSystemSecurityCategoryPageView" TypeName="ATS.BO.BOPagePermission" DeleteMethod="DeleteAccountPagePermissionId">
            <DeleteParameters>
                <asp:Parameter Name="Original_AccountPagePermissionId" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        <br />
        &nbsp<asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
            Text="ÊÍÏíË" Width="68px"
            UseSubmitBehavior="False" meta:resourcekey="btnUpdateResource1" />
    </ContentTemplate>
</asp:UpdatePanel>
