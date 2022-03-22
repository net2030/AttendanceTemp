<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountWorkScheduleShift.ascx.vb" Inherits="AccountAdmin_Controls_ctlAccountWorkScheduleShift" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/AccountAdmin/Controls/ctlEmployeesWorkSchedule.ascx" TagPrefix="uc1" TagName="ctlEmployeesWorkSchedule" %>

<script type="text/javascript">
    function showHideRow(sender) {

        if (sender.checked) {
            $(sender).closest('td').next('td').find('table').hide();
            $(sender).closest('td').next('td').next('td').find('table').hide();
        }
        else {
            $(sender).closest('td').next('td').find('table').show();
            $(sender).closest('td').next('td').next('td').find('table').show();
        }
    }

    function showHideRowDates(sender) {

        if (sender.value == 'False') {
            $("#periodRow").hide()
        }
        else {
            $("#periodRow").show()
        }
    }

    $(document).ready(function () {

        hidePeriodRow()
        hideWorkTimesForOffDays()

    })

    function hidePeriodRow() {

        var ddl = $("#ddlIsPeriodic").val()
        if (ddl == 'False') {
            $("#periodRow").hide()
        }
        else {
            $("#periodRow").show()
        }
    }

    function hideWorkTimesForOffDays() {

        $(".xGridView").find('input[type="checkbox"]:checked').each(function () {
            $(this).closest('td').next('td').find('table').hide();
            $(this).closest('td').next('td').next('td').find('table').hide();
        });


    }

</script>

<table>
    <tr>
        <td>
            <x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                DataKeyNames="WorkScheduleId" Caption="ﬁ«∆„… Ãœ«Ê· «·œÊ«„"
                SkinID="xgridviewSkinEmployee" Width="630px" CssClass="TableView" Style="text-align: center;" ShowHeaderWhenEmpty="True" DataSourceID="dsWorkSchedules" meta:resourcekey="GridView2Resource1">
                <Columns>

                    <asp:BoundField DataField="WorkScheduleId" HeaderText=" ”·”·" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />

                    <asp:BoundField DataField="WorkScheduleName" HeaderText="≈”„ «·ÃœÊ·" meta:resourcekey="BoundFieldResource2" />

                    <asp:BoundField DataField="WorkScheduleNameEN" HeaderText="≈”„ «·ÃœÊ· E" meta:resourcekey="BoundFieldResource5" />

                    <asp:BoundField DataField="ScheduleBegDate" HeaderText="»œ«Ì… «·⁄„· »«·ÃœÊ·" meta:resourcekey="BoundFieldResource3" />

                    <asp:BoundField DataField="ScheduleEndDate" HeaderText="‰Â«Ì… «·⁄„· »«·ÃœÊ·" meta:resourcekey="BoundFieldResource4" />

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

            <table style="width: 100%">
                <tr>
                    <td>&nbsp<asp:Button ID="btnAddWorkSchedule" runat="server"
                        Text="≈÷«›… ÃœÌœ" Width="75px" UseSubmitBehavior="False" meta:resourcekey="btnAddWorkScheduleResource1" />

                    </td>
                </tr>
            </table>


            <asp:FormView ID="FormView1" runat="server"
                SkinID="formviewskinemployee"
                DefaultMode="Insert" Width="630px" Visible="False" meta:resourcekey="FormView1Resource1">
                <InsertItemTemplate>
                    <table width="100%" class="xformview" style="">
                        <tr>
                            <th colspan="5" class="caption" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ÃœÊ· «·œÊ«„" meta:resourcekey="Literal9Resource2" />
                            </th>
                        </tr>
                        <tr>
                            <th colspan="5" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ÃœÊ· œÊ«„ ÃœÌœ" meta:resourcekey="Literal10Resource2" />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" meta:resourcekey="Label2Resource2" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text=" ”·”· :" meta:resourcekey="Literal1Resource2"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWorkScheduleId" runat="server" ReadOnly="True" meta:resourcekey="txtWorkScheduleIdResource2"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text=" ≈”„ «·ÃœÊ· :" meta:resourcekey="Literal2Resource2"></asp:Literal>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtWorkScheduleName" runat="server" Style="margin-right: 0px" Width="390px" meta:resourcekey="txtWorkScheduleNameResource2"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtWorkScheduleName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                          <tr>
                            <td>
                                <asp:Literal ID="Literal12" runat="server" Text="≈”„ «·ÃœÊ·:" meta:resourcekey="Literal12Resource1"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtWorkScheduleNameEN" runat="server" Style="margin-right: 0px" Width="390px" meta:resourcekey="txtWorkScheduleNameENResource2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtWorkScheduleNameEN"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="  «—ÌŒ «·»œ«Ì… : :" meta:resourcekey="Literal3Resource2"></asp:Literal>

                            </td>
                            <td>

                                <uc1:MyDate runat="server" ID="datStartDate" SelectedDate="<%# DateAdd(DateInterval.Year, -10, Now) %>" />
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="  «—ÌŒ «·‰Â«Ì… :" meta:resourcekey="Literal4Resource2"></asp:Literal>

                            </td>
                            <td>
                                <uc1:MyDate runat="server" ID="datEnddate" SelectedDate="<%# DateAdd(DateInterval.Year, 10, Now) %>" />
                                <asp:CustomValidator ID="CustomValidator2" runat="server"
                                    ControlToValidate="datEnddate$TextBox1"
                                    ErrorMessage="Start date must be greater or equal!!!"
                                    ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource2"></asp:CustomValidator>
                            </td>
                        </tr>


                        <tr>
                            <td>

                                <asp:Literal ID="Literal5" runat="server" Text=" ﬂ—«— «·ÃœÊ·" meta:resourcekey="Literal5Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlIsPeriodic" ClientIDMode="Static" runat="server" onchange="showHideRowDates(this)" meta:resourcekey="ddlIsPeriodicResource2">
                                    <asp:ListItem Value="False" meta:resourcekey="ListItemResource6">≈”»Ê⁄Ì</asp:ListItem>
                                    <asp:ListItem Value="True" meta:resourcekey="ListItemResource7">€Ì— «”»Ê⁄Ì</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr id="periodRow">
                            <td>
                                <asp:Literal ID="lblPeriodLength" runat="server" Text="ÿÊ· «·œÊ—… »«·ÌÊ„" meta:resourcekey="lblPeriodLengthResource2"></asp:Literal>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="numPeriodLength" Width="90px" Value="7" OnTextChanged="controlChange" AutoPostBack="True" EmptyMessage="7" MinValue="7" MaxValue="70" Culture="en-US" DbValueFactor="1" LabelCssClass="" LabelWidth="64px" meta:resourcekey="numPeriodLengthResource2">
                                    <NumberFormat DecimalDigits="0" ZeroPattern="n" />
                                </telerik:RadNumericTextBox><br />
                            </td>

                            <td>
                                <asp:Literal ID="lblPeriodStartDate" runat="server" Text=" «—ÌŒ »œ«Ì… «·œÊ—…" meta:resourcekey="lblPeriodStartDateResource2"></asp:Literal>
                            </td>
                            <td>
                                <uc1:MyDate runat="server" ID="datPeriodStartDate" OnTextChanged="controlChange" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="  ⁄œœ › —«  «·œÊ«„" meta:resourcekey="Literal6Resource2"></asp:Literal>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlShiftsCount" runat="server" OnSelectedIndexChanged="controlChange" AutoPostBack="True" meta:resourcekey="ddlShiftsCountResource2">

                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource8">› —…</asp:ListItem>
                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource9">› — Ì‰</asp:ListItem>
                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource10">À·«À › —« </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="   «·”Ì«”… «·„ÿ»ﬁ… :" meta:resourcekey="Literal7Resource2"></asp:Literal>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPolicy" runat="server" DataSourceID="dsPolicy" DataTextField="PolicyName" DataValueField="PolicyId" meta:resourcekey="ddlPolicyResource2">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource11">≈Œ —</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                        ID="rfvDDL_Product" Display="Dynamic" 
                                                        ControlToValidate="ddlPolicy"
                                                        runat="server"  Text="*" 
                                                        ErrorMessage="*"
                                                        ForeColor="Red" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChkIsDefault" runat="server" Text=" ÿ»Ìﬁ «·ÃœÊ· ⁄·Ï ﬂ· «·„ÊŸ›Ì‰" Visible="False" meta:resourcekey="ChkIsDefaultResource2" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                    Text="Õ›Ÿ «·ÃœÊ·" meta:resourcekey="Button1Resource2" />
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
                            <th colspan="5" class="caption" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ÃœÊ· «·œÊ«„" meta:resourcekey="Literal9Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <th colspan="5" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ÃœÊ· œÊ«„ ÃœÌœ" meta:resourcekey="Literal10Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" meta:resourcekey="Label2Resource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text=" ”·”· :" meta:resourcekey="Literal1Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWorkScheduleId" runat="server" ReadOnly="True" Text='<%# Bind("WorkScheduleId") %>' meta:resourcekey="txtWorkScheduleIdResource1"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text=" ≈”„ «·ÃœÊ· :" meta:resourcekey="Literal2Resource1"></asp:Literal>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtWorkScheduleName" runat="server" Style="margin-right: 0px" Width="390px" Text='<%# Bind("WorkScheduleName") %>' meta:resourcekey="txtWorkScheduleNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtWorkScheduleName"></asp:RequiredFieldValidator>

                            </td>
                        </tr>

                         <tr>
                            <td>
                                <asp:Literal ID="Literal12" runat="server" Text="≈”„ «·ÃœÊ·E:" meta:resourcekey="Literal12Resource1"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtWorkScheduleNameEN" runat="server" Style="margin-right: 0px" Width="390px"  Text='<%# Bind("WorkScheduleNameEN") %>' meta:resourcekey="txtWorkScheduleNameENResource2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtWorkScheduleNameEN"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="  «—ÌŒ «·»œ«Ì… : :" meta:resourcekey="Literal3Resource1"></asp:Literal>
                            </td>
                            <td>
                                <uc1:MyDate runat="server" ID="datStartDate" SelectedDate='<%# Bind("ScheduleBegDate") %>' />

                            </td>
                        </tr>

                        <tr style="display: none">
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="  «—ÌŒ «·‰Â«Ì… :" meta:resourcekey="Literal4Resource1"></asp:Literal>

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
                                <asp:Literal ID="Literal11" runat="server" Text="  ﬂ—«— «·ÃœÊ· : :" meta:resourcekey="Literal11Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlIsPeriodic" ClientIDMode="Static" runat="server" SelectedValue='<%# Bind("IsPeriodic") %>' onchange="showHideRowDates(this)" meta:resourcekey="ddlIsPeriodicResource1">
                                    <asp:ListItem Value="False" meta:resourcekey="ListItemResource1" Selected="True">≈”»Ê⁄Ì</asp:ListItem>
                                    <asp:ListItem Value="True" meta:resourcekey="ListItemResource2">€Ì— «”»Ê⁄Ì</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr id="periodRow">
                            <td>
                                <asp:Literal ID="lblPeriodLength" runat="server" Text="ÿÊ· «·œÊ—… »«·ÌÊ„" meta:resourcekey="lblPeriodLengthResource1"></asp:Literal>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="numPeriodLength" Width="90px" OnTextChanged="controlChange" Text='<%# Bind("PeriodLength") %>' AutoPostBack="True" EmptyMessage="7" MinValue="7" MaxValue="70" Culture="en-US" DbValueFactor="1" LabelCssClass="" LabelWidth="64px" meta:resourcekey="numPeriodLengthResource1">
                                    <NumberFormat DecimalDigits="0" ZeroPattern="n" />
                                </telerik:RadNumericTextBox><br />
                            </td>

                            <td>
                                <asp:Literal ID="lblPeriodStartDate" runat="server" Text=" «—ÌŒ »œ«Ì… «·œÊ—…" meta:resourcekey="lblPeriodStartDateResource1"></asp:Literal>
                            </td>
                            <td>
                                <uc1:MyDate runat="server" ID="datPeriodStartDate" OnTextChanged="controlChange" AutoPostBack="true" SelectedDate='<%# Bind("FirstPeriodStartDate") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="  ⁄œœ › —«  «·œÊ«„" meta:resourcekey="Literal6Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlShiftsCount" runat="server" OnSelectedIndexChanged="controlChange" SelectedValue='<%# Bind("ShiftsCount") %>' AutoPostBack="True" meta:resourcekey="ddlShiftsCountResource1">

                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource3" Selected="True">› —…</asp:ListItem>
                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource4">› — Ì‰</asp:ListItem>
                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource5">À·«À › —« </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="   «·”Ì«”… «·„ÿ»ﬁ… :" meta:resourcekey="Literal7Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPolicy" runat="server" DataSourceID="dsPolicy" DataTextField="PolicyName" DataValueField="PolicyId" SelectedValue='<%# Bind("PolicyId") %>' meta:resourcekey="ddlPolicyResource1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChkIsDefault" runat="server" Text=" ÿ»Ìﬁ «·ÃœÊ· ⁄·Ï ﬂ· «·„ÊŸ›Ì‰" Visible="False" meta:resourcekey="ChkIsDefaultResource1" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                    Text=" ÕœÌÀ «·ÃœÊ·" meta:resourcekey="Button1Resource1" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource1" />

                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>

            </asp:FormView>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <x:GridView ID="grdShift1" runat="server" AutoGenerateColumns="False" Visible="False"
                            SkinID="xgridviewSkinEmployee" Width="630px" DataKeyNames="WeekDayNo,VersionNo" Caption="√Êﬁ«  «·œÊ«„" PageSize="70" meta:resourcekey="grdShift1Resource1">
                            <Columns>

                                <asp:TemplateField HeaderText="«·—ﬁ„" meta:resourcekey="TemplateFieldResource8">
                                    <ItemTemplate>
                                        <asp:Label ID="txtWeekDayNo" runat="server" Text='<%# Eval("WeekDayNo") %>' meta:resourcekey="txtWeekDayNoResource4" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="txtWeekDayNo" runat="server" Text='<%# Eval("WeekDayNo") %>' meta:resourcekey="txtWeekDayNoResource3" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="≈·ÌÊ„" meta:resourcekey="TemplateFieldResource9">
                                    <ItemTemplate>
                                        <asp:Label ID="WeekDayName" runat="server" Text='<%# Eval("WeekDayName") %>' meta:resourcekey="WeekDayNameResource2" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="WeekDayName1" runat="server" Text='<%# Eval("WeekDayName") %>' meta:resourcekey="WeekDayName1Resource2" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="»œÊ‰" meta:resourcekey="TemplateFieldResource10">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsWeekendDay" runat="server" Checked='<%# Eval("IsWeekendDay").ToString().Equals("True") %>' onclick='showHideRow(this);' meta:resourcekey="chkIsWeekendDayResource4" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkIsWeekendDay" runat="server" Checked='<%# Eval("IsWeekendDay").ToString().Equals("True") %>' onclick='showHideRow(this);' meta:resourcekey="chkIsWeekendDayResource3" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" »œ√" meta:resourcekey="TemplateFieldResource11">
                                    <ItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkBegTime" runat="server" MinDate="1900-01-01" Enalbled="false"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkBegTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkBegTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkBegTime" runat="server" MinDate="1900-01-01" Enalbled="false"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkBegTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkBegTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" ‰ ÂÌ" meta:resourcekey="TemplateFieldResource12">
                                    <ItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkEndTime" runat="server" MinDate="1900-01-01"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkEndTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkEndTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkEndTime" runat="server" MinDate="1900-01-01"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkEndTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkEndTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" CommandName="Edit" Visible='<%# If(Me.FormView1.CurrentMode = FormViewMode.Edit, True, False)%>' cssclass="edit_button" runat="server"  Text="Edit" > </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                            </Columns>
                        </x:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <x:GridView ID="grdShift2" runat="server" AutoGenerateColumns="False" Visible="False" PageSize="70"
                            SkinID="xgridviewSkinEmployee" Width="630px" DataKeyNames="WeekDayNo,VersionNo" Caption="«·Ê—œÌ… «·À«‰Ì…" meta:resourcekey="grdShift2Resource1">
                            <Columns>

                                <asp:TemplateField HeaderText="«·—ﬁ„" meta:resourcekey="TemplateFieldResource8">
                                    <ItemTemplate>
                                        <asp:Label ID="txtWeekDayNo" runat="server" Text='<%# Eval("WeekDayNo") %>' meta:resourcekey="txtWeekDayNoResource4" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="txtWeekDayNo" runat="server" Text='<%# Eval("WeekDayNo") %>' meta:resourcekey="txtWeekDayNoResource3" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="≈·ÌÊ„" meta:resourcekey="TemplateFieldResource9">
                                    <ItemTemplate>
                                        <asp:Label ID="WeekDayName" runat="server" Text='<%# Eval("WeekDayName") %>' meta:resourcekey="WeekDayNameResource2" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="WeekDayName1" runat="server" Text='<%# Eval("WeekDayName") %>' meta:resourcekey="WeekDayName1Resource2" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="»œÊ‰" meta:resourcekey="TemplateFieldResource10">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsWeekendDay" runat="server" Checked='<%# Eval("IsWeekendDay").ToString().Equals("True") %>' onclick='showHideRow(this);' meta:resourcekey="chkIsWeekendDayResource4" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkIsWeekendDay" runat="server" Checked='<%# Eval("IsWeekendDay").ToString().Equals("True") %>' onclick='showHideRow(this);' meta:resourcekey="chkIsWeekendDayResource3" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" »œ√" meta:resourcekey="TemplateFieldResource11">
                                    <ItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkBegTime" runat="server" MinDate="1900-01-01" Enalbled="false"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkBegTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkBegTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkBegTime" runat="server" MinDate="1900-01-01" Enalbled="false"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkBegTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkBegTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" ‰ ÂÌ" meta:resourcekey="TemplateFieldResource12">
                                    <ItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkEndTime" runat="server" MinDate="1900-01-01"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkEndTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkEndTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkEndTime" runat="server" MinDate="1900-01-01"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkEndTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkEndTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" CommandName="Edit" Visible='<%# If(Me.FormView1.CurrentMode = FormViewMode.Edit, True, False)%>' cssclass="edit_button" runat="server"  Text="Edit" > </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                            </Columns>

                        </x:GridView>
                    </td>
                </tr>

                <tr>
                    <td>
                        <x:GridView ID="grdShift3" runat="server" AutoGenerateColumns="False" Visible="False"
                            SkinID="xgridviewSkinEmployee" Width="630px" DataKeyNames="WeekDayNo,VersionNo" Caption="«·Ê—œÌ… «·À«·À…" PageSize="70" meta:resourcekey="grdShift3Resource1">
                            <Columns>

                                <asp:TemplateField HeaderText="«·—ﬁ„" meta:resourcekey="TemplateFieldResource8">
                                    <ItemTemplate>
                                        <asp:Label ID="txtWeekDayNo" runat="server" Text='<%# Eval("WeekDayNo") %>' meta:resourcekey="txtWeekDayNoResource4" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="txtWeekDayNo" runat="server" Text='<%# Eval("WeekDayNo") %>' meta:resourcekey="txtWeekDayNoResource3" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="≈·ÌÊ„" meta:resourcekey="TemplateFieldResource9">
                                    <ItemTemplate>
                                        <asp:Label ID="WeekDayName" runat="server" Text='<%# Eval("WeekDayName") %>' meta:resourcekey="WeekDayNameResource2" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="WeekDayName1" runat="server" Text='<%# Eval("WeekDayName") %>' meta:resourcekey="WeekDayName1Resource2" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="»œÊ‰" meta:resourcekey="TemplateFieldResource10">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsWeekendDay" runat="server" Checked='<%# Eval("IsWeekendDay").ToString().Equals("True") %>' onclick='showHideRow(this);' meta:resourcekey="chkIsWeekendDayResource4" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkIsWeekendDay" runat="server" Checked='<%# Eval("IsWeekendDay").ToString().Equals("True") %>' onclick='showHideRow(this);' meta:resourcekey="chkIsWeekendDayResource3" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" »œ√" meta:resourcekey="TemplateFieldResource11">
                                    <ItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkBegTime" runat="server" MinDate="1900-01-01" Enalbled="false"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkBegTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkBegTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkBegTime" runat="server" MinDate="1900-01-01" Enalbled="false"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkBegTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkBegTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" ‰ ÂÌ" meta:resourcekey="TemplateFieldResource12">
                                    <ItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkEndTime" runat="server" MinDate="1900-01-01"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkEndTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkEndTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTimePicker ID="datWorkEndTime" runat="server" MinDate="1900-01-01"
                                            SelectedDate='<%# If(IsDBNull(Eval("WorkEndTime")), DateTime.Now, Convert.ToDateTime(Eval("WorkEndTime")))%>'>
                                        </telerik:RadTimePicker>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" CommandName="Edit" Visible='<%# If(Me.FormView1.CurrentMode = FormViewMode.Edit, True, False)%>' cssclass="edit_button" runat="server"  Text="Edit" > </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                            </Columns>

                        </x:GridView>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <br />
            <% If Me.FormView1.CurrentMode = FormViewMode.Edit Then%>
            <uc1:ctlEmployeesWorkSchedule runat="server" ID="ctlEmployeesWorkSchedule" />
            <% End If%>
        </td>
    </tr>

</table>



<asp:ObjectDataSource ID="dsPolicy" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOPolicy">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>





<asp:ObjectDataSource ID="dsWorkSchedules" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWorkSchedulesDataset" TypeName="ATS.BO.Framework.BOWorkSchedule">
    <SelectParameters>
        <asp:Parameter Name="DepartmentId" Type="Int32" DefaultValue="1" />
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="100" Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

