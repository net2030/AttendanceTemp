<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlHoliday.ascx.vb" Inherits="Attendance_Controls_ctlHoliday" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/AccountAdmin/Controls/ctlEmployeesHolidayException.ascx" TagPrefix="uc1" TagName="ctlEmployeesHolidayException" %>







<style type="text/css">
    .style1 {
        height: 23px;
    }
</style>



<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <asp:FormView ID="FormView1" runat="server"
                        SkinID="formviewskinemployee"
                        DefaultMode="Insert" Width="100%" BorderStyle="Solid" Visible="False" meta:resourcekey="FormView1Resource1">
                        <InsertItemTemplate>
                            <table width="100%" class="xformview">
                                <tr>
                                    <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                                        <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÅÌÇÒÉ ÑÓãíÉ" meta:resourcekey="Literal9Resource2" />
                                    </th>
                                </tr>
                                <tr>
                                    <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                        <asp:Literal ID="Literal10" runat="server" Text="ÊÓÌíá ÅÌÇÒÉ ÌÏíÏÉ" meta:resourcekey="Literal10Resource2" />
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource2"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal1" runat="server" Text=" ÑÞã ÇáÍÇÓÈ:" meta:resourcekey="Literal1Resource2" />

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHolidayId" runat="server" ReadOnly="True" Text='<%# Bind("HolidayId") %>' meta:resourcekey="txtHolidayIdResource2"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal3" runat="server" Text="  ÅÓã ÇáÅÌÇÒÉ :" meta:resourcekey="Literal3Resource2" />

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHolidayName" runat="server" Text='<%# Bind("HolidayName") %>' meta:resourcekey="txtHolidayNameResource2"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal5" runat="server" Text="  ÊÈÏÃ Ýí :" meta:resourcekey="Literal5Resource2" />
                                    </td>
                                    <td>
                                        <uc1:MyDate runat="server" ID="datEffectiveDate" SelectedDate="<%# Now %>" />
                                    </td>

                                    <td>
                                        <asp:Literal ID="Literal6" runat="server" Text="  ÊäÊåí Ýí :" meta:resourcekey="Literal6Resource2" />
                                    </td>
                                    <td>
                                        <uc1:MyDate runat="server" ID="datDateExpire" SelectedDate="<%# DateAdd(DateInterval.Day, 1, Now) %>" />
                                        <asp:CustomValidator ID="CustomValidator2" runat="server"
                                            ControlToValidate="datDateExpire$TextBox1"
                                            ErrorMessage="Start date must be greater or equal!!!"
                                            ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource2"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal8" runat="server" Text="   ãáÇÍÙÇÊ :" meta:resourcekey="Literal8Resource2" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="200px" Height="70px" Text='<%# Bind("Note") %>' meta:resourcekey="txtNotesResource2"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                            Text="ÍÝÙ ÇáÅÌÇÒÉ" meta:resourcekey="Button1Resource2" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button2" runat="server" CommandName="Cancel" OnClientClick="ReloadSamepage(); return false;"
                                            Text="ÅáÛÇÁ ÇáÃãÑ" meta:resourcekey="Button2Resource2" />
                                    </td>
                                </tr>
                            </table>
                        </InsertItemTemplate>

                        <EditItemTemplate>
                            <table width="100%" class="xformview">
                                <tr>
                                    <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                                        <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÅÌÇÒÉ ãæÙÝ" meta:resourcekey="Literal9Resource1" />
                                    </th>
                                </tr>
                                <tr>
                                    <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                        <asp:Literal ID="Literal10" runat="server" Text="ÊÍÏíË ÈíÇäÇÊ ÅÌÇÒÉ" meta:resourcekey="Literal10Resource1" />
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal1" runat="server" Text=" ÑÞã ÇáÍÇÓÈ:" meta:resourcekey="Literal1Resource1" />

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHolidayId" runat="server" ReadOnly="True" Text='<%# Bind("HolidayId") %>' meta:resourcekey="txtHolidayIdResource1"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal3" runat="server" Text="  ÅÓã ÇáÅÌÇÒÉ :" meta:resourcekey="Literal3Resource1" />

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHolidayName" runat="server" Text='<%# Eval("HolidayName") %>' meta:resourcekey="txtHolidayNameResource1"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal5" runat="server" Text="  ÊÈÏÃ Ýí :" meta:resourcekey="Literal5Resource1" />
                                    </td>
                                    <td>
                                        <uc1:MyDate runat="server" ID="datEffectiveDate" SelectedDate='<%# Bind("EffectiveDate") %>' />
                                    </td>

                                    <td>
                                        <asp:Literal ID="Literal6" runat="server" Text="  ÊäÊåí Ýí :" meta:resourcekey="Literal6Resource1" />
                                    </td>
                                    <td>
                                        <uc1:MyDate runat="server" ID="datDateExpire" SelectedDate='<%# Bind("DateExpire") %>' />
                                        <asp:CustomValidator ID="CustomValidator2" runat="server"
                                            ControlToValidate="datDateExpire$TextBox1"
                                            ErrorMessage="Start date must be greater or equal!!!"
                                            ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal8" runat="server" Text="   ãáÇÍÙÇÊ :" meta:resourcekey="Literal8Resource1" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="200px" Height="70px" Text='<%# Bind("Note") %>' meta:resourcekey="txtNotesResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                            Text="ÊÍÏíË ÇáÅÌÇÒÉ" meta:resourcekey="Button1Resource1" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button2" runat="server" CommandName="Cancel" OnClientClick="ReloadSamepage(); return false;"
                                            Text="ÅáÛÇÁ ÇáÃãÑ" meta:resourcekey="Button2Resource1" />
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>

                    </asp:FormView>
                </td>
                <td>
                    <% If Me.FormView1.CurrentMode = FormViewMode.Edit Then%>
                    <uc1:ctlEmployeesHolidayException runat="server" ID="ctlEmployeesHolidayException" />
                    <% End If%>
                </td>
            </tr>
        </table>


    </ContentTemplate>
</asp:UpdatePanel>


<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
    DataKeyNames="HolidayId" Caption="ÞÇÆãÉ ÇáÅÌÇÒÇÊ ÇáÑÓãíÉ" DataSourceID="dsHolidays"
    SkinID="xgridviewSkinEmployee" Width="70%" ShowHeaderWhenEmpty="True" meta:resourcekey="GridView2Resource1">
    <Columns>

        <asp:BoundField DataField="HolidayId" HeaderText="ãÓáÓá" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />

        <asp:BoundField DataField="HolidayName" HeaderText="ÅÓã ÇáÅÌÇÒÉ" meta:resourcekey="BoundFieldResource2" />

        <asp:BoundField DataField="EffectiveDate" HeaderText="ÊÇÑíÎ ÇáÈÏÇíÉ" meta:resourcekey="BoundFieldResource3" />

        <asp:BoundField DataField="DateExpire" HeaderText="ÊÇÑíÎ ÇáäåÇíÉ" meta:resourcekey="BoundFieldResource4" />

        <asp:TemplateField HeaderText="ÊÚÏíá" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ÍÐÝ" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
            <ItemTemplate>
                <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
</x:GridView>
<asp:Button ID="btnAddHoliday" runat="server"
    Text="ÅÖÇÝÉ ÌÏíÏ"
    UseSubmitBehavior="False" meta:resourcekey="btnAddHolidayResource1" />
<p>
    &nbsp;
</p>



<asp:ObjectDataSource ID="dsHolidays" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetHolidaysDataset" TypeName="ATS.BO.Framework.BOHoliday">
    <SelectParameters>
        <asp:Parameter Name="PageNo" Type="Int32" DefaultValue="1" />
        <asp:Parameter Name="PageSize" Type="Int32" DefaultValue="100" />
    </SelectParameters>
</asp:ObjectDataSource>





