<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountWorkSchedule.ascx.vb" Inherits="AccountAdmin_Controls_ctlAccountWorkTypeList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/AccountAdmin/Controls/ctlEmployeesWorkSchedule.ascx" TagPrefix="uc1" TagName="ctlEmployeesWorkSchedule" %>



<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
    DataKeyNames="WorkScheduleId" Caption="ÞÇÆãÉ ÌÏÇæá ÇáÏæÇã"
    SkinID="xgridviewSkinEmployee" Width="630px" CssClass="TableView" Style="text-align: center;" ShowHeaderWhenEmpty="True" DataSourceID="dsWorkSchedules" meta:resourcekey="GridView2Resource1">
    <Columns>

        <asp:BoundField DataField="WorkScheduleId" HeaderText="ÑÞã ÇáÍÇÓÈ" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />

        <asp:BoundField DataField="WorkScheduleName" HeaderText="ÅÓã ÇáÌÏæá" meta:resourcekey="BoundFieldResource2" />

        <asp:BoundField DataField="WorkScheduleNameEN" HeaderText="ÅÓã ÇáÌÏæá E" meta:resourcekey="BoundFieldResource5" />

        <asp:BoundField DataField="ScheduleBegDate" HeaderText="ÈÏÇíÉ ÇáÚãá ÈÇáÌÏæá" meta:resourcekey="BoundFieldResource3" />

        <asp:BoundField DataField="ScheduleEndDate" HeaderText="äåÇíÉ ÇáÚãá ÈÇáÌÏæá" meta:resourcekey="BoundFieldResource4" />

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
        <td>&nbsp<asp:Button ID="btnAddWorkSchedule" runat="server"
            Text="ÅÖÇÝÉ ÌÏíÏ" Width="75px" UseSubmitBehavior="False" meta:resourcekey="btnAddWorkScheduleResource1" />

        </td>
    </tr>
</table>

<table>
    <tr>
        <td>
            <asp:FormView ID="FormView1" runat="server"
                SkinID="formviewskinemployee"
                DefaultMode="Insert" Width="630px" Visible="False" meta:resourcekey="FormView1Resource1">
                <InsertItemTemplate>
                    <table width="100%" class="xformview">
                        <tr>
                            <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÌÏæá ÇáÏæÇã" meta:resourcekey="Literal9Resource2" />
                            </th>
                        </tr>
                        <tr>
                            <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal10" runat="server" Text="ÊÓÌíá ÌÏæá ÏæÇã ÌÏíÏ" meta:resourcekey="Literal10Resource2" />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text="ÑÞã ÇáÍÇÓÈ :" meta:resourcekey="Literal1Resource2"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWorkScheduleId" runat="server" ReadOnly="True" meta:resourcekey="txtWorkScheduleIdResource2"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="ÅÓã ÇáÌÏæá:" meta:resourcekey="Literal2Resource1"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtWorkScheduleName" runat="server" Style="margin-right: 0px" Width="390px" meta:resourcekey="txtWorkScheduleNameResource2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtWorkScheduleName"></asp:RequiredFieldValidator>

                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="Literal12" runat="server" Text="ÅÓã ÇáÌÏæá:" meta:resourcekey="Literal12Resource1"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtWorkScheduleNameEN" runat="server" Style="margin-right: 0px" Width="390px" meta:resourcekey="txtWorkScheduleNameENResource2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtWorkScheduleNameEN"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="ÊÇÑíå ÇáÈÏÇíÉ:" meta:resourcekey="Literal3Resource1"></asp:Literal>
                            </td>
                            <td>

                                <uc1:MyDate runat="server" ID="datStartDate" SelectedDate="<%# Now %>" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="ÊÇÑíÎ ÇáäåÇíÉ:" meta:resourcekey="Literal4Resource1"></asp:Literal>
                            </td>
                            <td>
                                <uc1:MyDate runat="server" ID="datEnddate" SelectedDate="<%# DateAdd(DateInterval.Year, 5, Now) %>" />
                                <asp:CustomValidator ID="CustomValidator2" runat="server"
                                    ControlToValidate="datEnddate$TextBox1"
                                    ErrorMessage="Start date must be greater or equal!!!"
                                    ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource2"></asp:CustomValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="ÇáÓíÇÓÉ ÇáãØÈÞÉ:" meta:resourcekey="Literal5Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPolicy" runat="server" DataSourceID="dsPolicy" DataTextField="PolicyName" DataValueField="PolicyId" meta:resourcekey="ddlPolicyResource2">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChkIsDefault" runat="server" Text="ÊØÈíÞ ÇáÌÏæá Úáì ßá ÇáãæÙÝíä" Visible="False" meta:resourcekey="ChkIsDefaultResource2" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                    Text="ÍÝÙ ÇáÌÏæá" meta:resourcekey="Button1Resource2" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;"
                                    Text="ÅáÛÇÁ ÇáÃãÑ" meta:resourcekey="Button2Resource2" />
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>

                <EditItemTemplate>
                    <table width="100%" class="xformview">
                        <tr>
                            <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÌÏæá ÇáÏæÇã" meta:resourcekey="Literal9Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal10" runat="server" Text="ÊÓÌíá ÌÏæá ÏæÇã ÌÏíÏ" meta:resourcekey="Literal10Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" meta:resourcekey="Label2Resource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text="ÑÞã ÇáÍÇÓÈ :" meta:resourcekey="Literal1Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWorkScheduleId" runat="server" ReadOnly="True" Text='<%# Bind("WorkScheduleId") %>' meta:resourcekey="txtWorkScheduleIdResource1"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="ÅÓã ÇáÌÏæá :" meta:resourcekey="Literal6Resource1"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtWorkScheduleName" runat="server" Style="margin-right: 0px" Width="390px" Text='<%# Bind("WorkScheduleName") %>' meta:resourcekey="txtWorkScheduleNameResource1"></asp:TextBox>
                            </td>
                        </tr>

                         <tr>
                            <td>
                                <asp:Literal ID="Literal12" runat="server" Text="ÅÓã ÇáÌÏæáE:" meta:resourcekey="Literal12Resource1"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtWorkScheduleNameEN" runat="server" Style="margin-right: 0px" Width="390px"  Text='<%# Bind("WorkScheduleNameEN") %>' meta:resourcekey="txtWorkScheduleNameENResource2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtWorkScheduleNameEN"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="ÊÇÑíÎ ÇáÈÏÇíÉ :" meta:resourcekey="Literal7Resource1"></asp:Literal>
                            </td>
                            <td>
                                <uc1:MyDate runat="server" ID="datStartDate" SelectedDate='<%# Bind("ScheduleBegDate") %>' />

                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="Literal8" runat="server" Text="ÊÇÑíÎ ÇáäåÇíÉ :" meta:resourcekey="Literal8Resource1"></asp:Literal>
                            </td>
                            <td>
                                <uc1:MyDate runat="server" ID="datEnddate" SelectedDate='<%# Bind("ScheduleEndDate") %>' />
                                <asp:CustomValidator ID="CustomValidator2" runat="server"
                                    ControlToValidate="datEnddate$TextBox1"
                                    ErrorMessage="Start date must be greater or equal!!!"
                                    ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1"></asp:CustomValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="Literal11" runat="server" Text="ÇáÓíÇÓÉ ÇáãØÈÞÉ :" meta:resourcekey="Literal11Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPolicy" runat="server" DataSourceID="dsPolicy" DataTextField="PolicyName" DataValueField="PolicyId" SelectedValue='<%# Bind("PolicyId") %>' meta:resourcekey="ddlPolicyResource1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChkIsDefault" runat="server" Text="ÊØÈíÞ ÇáÌÏæá Úáì ßá ÇáãæÙÝíä" Visible="False" meta:resourcekey="ChkIsDefaultResource1" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                    Text="ÊÍÏíË ÇáÌÏæá" meta:resourcekey="Button1Resource1" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" Text="ÅáÛÇÁ ÇáÃãÑ" meta:resourcekey="Button2Resource1" />

                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>

            </asp:FormView>

            <br />
           

            <x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Visible="False"
                SkinID="xgridviewSkinEmployee" Width="630px" DataKeyNames="WeekDayNo,VersionNo" Caption="ÃíÇã ÇáÚãá" PageSize="21" meta:resourcekey="GridView1Resource1">
                <Columns>

                    <asp:TemplateField HeaderText="ÇáÑÞã" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <asp:Label ID="txtWeekDayNo" runat="server" Text='<%# Eval("WeekDayNo") %>' meta:resourcekey="txtWeekDayNoResource2" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="txtWeekDayNo" runat="server" Text='<%# Eval("WeekDayNo") %>' meta:resourcekey="txtWeekDayNoResource1" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Åáíæã" meta:resourcekey="TemplateFieldResource4">
                        <ItemTemplate>
                            <asp:Label ID="WeekDayName" runat="server" Text='<%# Eval("WeekDayName") %>' meta:resourcekey="WeekDayNameResource1" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="WeekDayName1" runat="server" Text='<%# Eval("WeekDayName") %>' meta:resourcekey="WeekDayName1Resource1" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="äåÇíÉ ÅÓÈæÚ" meta:resourcekey="TemplateFieldResource5">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsWeekendDay" runat="server" Checked='<%# Eval("IsWeekendDay").ToString().Equals("True") %>' meta:resourcekey="chkIsWeekendDayResource2" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkIsWeekendDay" runat="server" Checked='<%# Eval("IsWeekendDay").ToString().Equals("True") %>' meta:resourcekey="chkIsWeekendDayResource1" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ÈÏÇíÉ ÇáÏæÇã" meta:resourcekey="TemplateFieldResource6">
                        <ItemTemplate>
                            <telerik:radtimepicker id="datWorkBegTime" runat="server" mindate="1900-01-01" enalbled="false" visible='<%# If(Eval("IsWeekendDay").ToString().Equals("True"), False, True)%>'
                                selecteddate='<%# If(IsDBNull(Eval("WorkBegTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkBegTime")))%>'>
                        </telerik:radtimepicker>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:radtimepicker id="datWorkBegTime" runat="server" mindate="1900-01-01" enalbled="false" readonly="true" selecteddate='<%# If(IsDBNull(Eval("WorkBegTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkBegTime")))%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="äåÇíÉ ÇáÏæÇã" meta:resourcekey="TemplateFieldResource7">
                        <ItemTemplate>
                            <telerik:radtimepicker id="datWorkEndTime" runat="server" mindate="1900-01-01" visible='<%# If(Eval("IsWeekendDay").ToString().Equals("True"), False, True)%>'
                                selecteddate='<%# If(IsDBNull(Eval("WorkEndTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkEndTime")))%>'>
                        </telerik:radtimepicker>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:radtimepicker id="datWorkEndTime" runat="server" mindate="1900-01-01" enalbled="false" readonly="true" selecteddate='<%# If(IsDBNull(Eval("WorkEndTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkEndTime")))%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update" meta:resourcekey="lnkUpdateResource1"></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" meta:resourcekey="lnkCancelResource1"></asp:LinkButton>

                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" CommandName="Edit" Visible='<%# If(Me.FormView1.CurrentMode = FormViewMode.Edit, True, False) %>' CssClass="edit_button" runat="server" Text="Edit" meta:resourcekey="lnkEditResource1"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerSettings PageButtonCount="21" />
            </x:GridView>
        </td>
        <td>
            <% If Me.FormView1.CurrentMode = FormViewMode.Edit Then%>
            <uc1:ctlEmployeesWorkSchedule runat="server" ID="ctlEmployeesWorkSchedule" />
            <% End If%>
        </td>
    </tr>
</table>








<asp:ObjectDataSource ID="dsPolicy" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOPolicy">
    <SelectParameters>
        <asp:Parameter DefaultValue="1" Name="UserAccountId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>





<asp:ObjectDataSource ID="dsWorkSchedules" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWorkSchedulesDataset" TypeName="ATS.BO.Framework.BOWorkSchedule">
    <SelectParameters>
        <asp:Parameter Name="DepartmentId" Type="Int32" DefaultValue="1" />
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="100" Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

