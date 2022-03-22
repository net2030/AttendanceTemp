<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountPolicy.ascx.vb" Inherits="AccountAdmin_Controls_ctlAccountpolicy" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style type="text/css">
    .style2 {
        height: 24px;
        width: 34%;
    }

    .style9 {
        width: 34%;
    }
</style>

<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
    DataKeyNames="PolicyId" Caption="ÞÇÆãÉ ÓíÇÓÇÊ ÇáÏæÇã"
    SkinID="xgridviewSkinEmployee" Width="50%" CssClass="TableView" meta:resourcekey="GridView2Resource1" DataSourceID="dsPolicy">
    <Columns>

        <asp:BoundField ItemStyle-Width="10%" ItemStyle-Wrap="false" DataField="PolicyId" HeaderText="ÇáÑÞã" ReadOnly="True" meta:resourcekey="BoundFieldResource1">

            <ItemStyle Wrap="False" Width="10%"></ItemStyle>
        </asp:BoundField>

        <asp:BoundField DataField="PolicyName" HeaderText="ÅÓã ÇáÓíÇÓÉ" meta:resourcekey="BoundFieldResource2" />

         <asp:BoundField DataField="PolicyNameEN" HeaderText="ÅÓã ÇáÓíÇÓÉ" meta:resourcekey="BoundFieldResource3" />



        <asp:TemplateField HeaderText="ÊÚÏíá" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit" ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ÍÐÝ" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
            <ItemTemplate>
                <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False" ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
        </asp:TemplateField>

    </Columns>
</x:GridView>
<br />
<table style="width: 100%">
    <tr>
        <td>&nbsp<asp:Button ID="btnAddPolicy" runat="server"
            Text="ÊÓÌíá ÌÏíÏ" Width="75px" UseSubmitBehavior="False" meta:resourcekey="btnAddPolicyResource1" />

        </td>
    </tr>
</table>



<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>

        <asp:FormView ID="FormView1" runat="server"
            SkinID="formviewskinemployee"
            DefaultMode="Insert" Width="800px" Visible="False" meta:resourcekey="FormView1Resource1">
            <InsertItemTemplate>
                <table border="0" width="100%" class="xformview">
                    <tr>
                        <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÓíÇÓÉ ÇáÏæÇã" meta:resourcekey="Literal9Resource2" />
                        </th>
                    </tr>
                    <tr>
                        <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                            <asp:Literal ID="Literal10" runat="server" Text="ÊÓÌíá ÓíÇÓÉ ÏæÇã ÌÏíÏÉ" meta:resourcekey="Literal10Resource2" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource2" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="ÑÞã ÇáÍÇÓÈ" meta:resourcekey="Literal2Resource2"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPolicyId" runat="server" ReadOnly="True" Text='<%# Bind("PolicyId") %>' meta:resourcekey="txtPolicyIdResource2"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text="  ÅÓã ÓíÇÓÉ ÇáÏæÇã :" meta:resourcekey="Literal1Resource2"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtPolicyName" runat="server" Style="margin-right: 0px" Width="390px" Text='<%# Bind("PolicyName") %>' meta:resourcekey="txtPolicyNameResource2"></asp:TextBox>
                        </td>
                    </tr>

                     <tr>
                        <td>
                            <asp:Literal ID="Literal11" runat="server" Text="  ÅÓã ÓíÇÓÉ ÇáÏæÇã :" meta:resourcekey="Literal11Resource2"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtPolicyNameEN" runat="server" Style="margin-right: 0px" Width="390px" ></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="ÅÍÊÓÇÈ ÇáÊÃÎíÑ ß ÛíÇÈ:" meta:resourcekey="Literal3Resource2"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMarkObsentDuration" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("MarkObsentDuration") %>' meta:resourcekey="txtMarkObsentDurationResource2"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text="ÅÍÊÓÇÈ ÇáÊÃÎíÑ ÛíÇÈ äÕÝ íæã" meta:resourcekey="Literal4Resource2"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtLateLimitPerMonthMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("LateLimitPerMonthMinutes") %>' meta:resourcekey="txtLateLimitPerMonthMinutesResource2"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text="ÇáÓãÇÍ ÈÇáÎÑæÌ ÞÈá äåÇíÉ ÇáÏæÇã" meta:resourcekey="Literal5Resource2"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLateOutMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("LateOutMinutes") %>' meta:resourcekey="txtLateOutMinutesResource2"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text="ÝÊÑÉ ÇáÓãÇÍ ÈÇáÊÓÌíá ÈÚÏ ÇáÏæÇã" meta:resourcekey="Literal6Resource2"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEarlyOutMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("EarlyOutMinutes") %>' meta:resourcekey="txtEarlyOutMinutesResource2"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal7" runat="server" Text="ÝÊÑÉ ÇáÓãÇÍ ÈÇáÊÓÌíá ÞÈá ÈÏÇíÉ ÇáÏæÇã" meta:resourcekey="Literal7Resource2"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEarlyInMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("EarlyInMinutes") %>' meta:resourcekey="txtEarlyInMinutesResource2"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text="ÝÊÑÉ ÇáÓãÇÍ ÈÇáÊÃÎíÑ ÚäÏ ÈÏÇíÉ ÇáÏæÇã" meta:resourcekey="Literal8Resource2"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtLateInMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("LateInMinutes") %>' meta:resourcekey="txtLateInMinutesResource2"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                Text="ÍÝÙ ÇáÓíÇÓÉ" meta:resourcekey="Button1Resource2" />
                            <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;"
                                Text="ÅáÞÇÁ ÇáÇãÑ" meta:resourcekey="Button2Resource2" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>

            <EditItemTemplate>
                <table width="100%" class="xformview">
                    <tr>
                        <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÓíÇÓÉ ÇáÏæÇã" meta:resourcekey="Literal9Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                            <asp:Literal ID="Literal10" runat="server" Text="ÊÍÏíË ÓíÇÓÉ ÏæÇã" meta:resourcekey="Literal10Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="ÑÞã ÇáÍÇÓÈ" meta:resourcekey="Literal2Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPolicyId" runat="server" ReadOnly="True" Text='<%# Bind("PolicyId") %>' meta:resourcekey="txtPolicyIdResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text="  ÅÓã ÓíÇÓÉ ÇáÏæÇã :" meta:resourcekey="Literal1Resource1"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtPolicyName" runat="server" Style="margin-right: 0px" Width="390px" Text='<%# Bind("PolicyName") %>' meta:resourcekey="txtPolicyNameResource1"></asp:TextBox>
                        </td>
                    </tr>

                       <tr>
                        <td>
                            <asp:Literal ID="Literal11" runat="server" Text="  ÅÓã ÓíÇÓÉ ÇáÏæÇã :" meta:resourcekey="Literal11Resource2"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtPolicyNameEN" runat="server" Style="margin-right: 0px" Width="390px" Text='<%# Bind("PolicyNameEN")%>'></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="ÅÍÊÓÇÈ ÇáÊÃÎíÑ ß ÛíÇÈ:" meta:resourcekey="Literal3Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMarkObsentDuration" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("MarkObsentDuration") %>' meta:resourcekey="txtMarkObsentDurationResource1"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text="ÏÞÇÆÞ ÇáÊÃÎíÑ ÇáãÓãæÍ ÈåÇ ÔåÑíÇ" meta:resourcekey="Literal4Resource1"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtLateLimitPerMonthMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("LateLimitPerMonthMinutes") %>' meta:resourcekey="txtLateLimitPerMonthMinutesResource1"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text="ÇáÓãÇÍ ÈÇáÎÑæÌ ÞÈá äåÇíÉ ÇáÏæÇã" meta:resourcekey="Literal5Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLateOutMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("LateOutMinutes") %>' meta:resourcekey="txtLateOutMinutesResource1"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text="ÝÊÑÉ ÇáÓãÇÍ ÈÇáÊÓÌíá ÈÚÏ ÇáÏæÇã" meta:resourcekey="Literal6Resource1"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEarlyOutMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("EarlyOutMinutes") %>' meta:resourcekey="txtEarlyOutMinutesResource1"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal7" runat="server" Text="ÝÊÑÉ ÇáÓãÇÍ ÈÇáÊÓÌíá ÞÈá ÈÏÇíÉ ÇáÏæÇã" meta:resourcekey="Literal7Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEarlyInMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("EarlyInMinutes") %>' meta:resourcekey="txtEarlyInMinutesResource1"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text="ÝÊÑÉ ÇáÓãÇÍ ÈÇáÊÃÎíÑ ÚäÏ ÈÏÇíÉ ÇáÏæÇã" meta:resourcekey="Literal8Resource1"></asp:Literal>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtLateInMinutes" runat="server" Style="margin-right: 0px" Width="50px" Text='<%# Bind("LateInMinutes") %>' meta:resourcekey="txtLateInMinutesResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                Text="ÊÍÏíË" meta:resourcekey="Button1Resource1" />
                            <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;"
                                Text="ÅáÛÇÁ ÇáÇãÑ" meta:resourcekey="Button2Resource1" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>

        </asp:FormView>

    </ContentTemplate>
</asp:UpdatePanel>


   <asp:ObjectDataSource ID="dsPolicy" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetAllPoliciesDataset" TypeName="ATS.BO.Framework.BOPolicy">
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
                <asp:Parameter DefaultValue="100" Name="PageSize" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>


