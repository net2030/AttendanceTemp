<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMyGatePass.ascx.vb" Inherits="Attendance_Controls_ctlMyGatePass" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/Attendance/Controls/ctlEmployeesLeaveBalance.ascx" TagPrefix="uc1" TagName="ctlEmployeesLeaveBalance" %>

<script src="../js/jquery.min.js"></script>

<script type="text/javascript">
   

</script>

<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
    DataKeyNames="GatePassId" Caption="ﬁ«∆„… «–Ê‰«  «·„ÊŸ›Ì‰"
    SkinID="xgridviewSkinEmployee" Width="98%" CssClass="TableView" Style="text-align: center;" DataSourceID="dsGatePassesByEmployee" meta:resourcekey="GridView2Resource2">
    <Columns>

        <asp:BoundField DataField="Gatepassid" HeaderText=" ”·”·" ReadOnly="True" meta:resourcekey="BoundFieldResource7" />

        <asp:BoundField DataField="GatepassTypeName" HeaderText="‰Ê⁄ «·≈” ∆–«‰" meta:resourcekey="BoundFieldResource8" />

        <asp:BoundField DataField="GatepassBegTime" HeaderText="Ì»œ√ „‰" meta:resourcekey="BoundFieldResource9" />

        <asp:BoundField DataField="GatepassEndTime" HeaderText="Ì‰ ÂÌ ›Ì" meta:resourcekey="BoundFieldResource10" />

        <asp:BoundField DataField="AprovalStatus" HeaderText="Õ«·… «·ÿ·»" meta:resourcekey="BoundFieldResource11" />

        <asp:BoundField DataField="ApprovalNotes" HeaderText="„·«ÕŸ« " meta:resourcekey="BoundFieldResource12" />

        <asp:TemplateField HeaderText=" ⁄œÌ·" ShowHeader="False" meta:resourcekey="TemplateFieldResource3">
            <ItemTemplate>
                <asp:LinkButton ID="EditLinkButton" runat="server" CommandName="Edit" CausesValidation="False"
                    Visible='<%# If(Eval("IsApproved").ToString().Equals("True"), False, True) %>' meta:resourcekey="EditLinkButtonResource1"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Õ–›" ShowHeader="False" meta:resourcekey="TemplateFieldResource4">
            <ItemTemplate>
                <asp:LinkButton ID="DeleteLinkButton" runat="server" CommandName="DataDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    Visible='<%# If(Eval("IsApproved").ToString().Equals("True"), False, True) %>' OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False" meta:resourcekey="DeleteLinkButtonResource1"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
        </asp:TemplateField>

    </Columns>
</x:GridView>
<table style="width: 100%">
    <tr>
        <td>
            <br>
            &nbsp<asp:Button ID="btnAddGatePass" runat="server" OnClick="btnAddGatePass_Click" CausesValidation="False"
                Text="≈÷«›… ÃœÌœ" Width="75px" UseSubmitBehavior="False" meta:resourcekey="btnAddGatePassResource2" />

        </td>
    </tr>
</table>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>

<asp:FormView ID="FormView1" runat="server"
    SkinID="formviewskinemployee"
    DefaultMode="Insert" Width="98%" meta:resourcekey="FormView1Resource1">
    <InsertItemTemplate>
        <table width="100%" class="xformview">
            <tr>
                <th colspan="5" class="caption" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈” ∆–«‰ œÊ«„" meta:resourcekey="Literal9Resource2" />
                </th>
            </tr>
            <tr>
                <th colspan="5" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ≈” ∆–«‰ ÃœÌœ " meta:resourcekey="Literal10Resource2" />
                </th>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" meta:resourcekey="Label2Resource2" />
                </td>
                <td rowspan="5" colspan="2">
                    <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Literal ID="Literal1" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal1Resource2"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtGatepassId" runat="server" ReadOnly="True" meta:resourcekey="txtGatepassIdResource2"></asp:TextBox>
                </td>


            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal2" runat="server" Text=" ≈”„ «·„ÊŸ›:" meta:resourcekey="Literal2Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtEmployeeName" runat="server" ReadOnly="True" Text='<%# Session("EmployeeNameWithCode") %>' meta:resourcekey="txtEmployeeNameResource2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal3" runat="server" Text=" ‰Ê⁄ «·«” ∆–«‰:" meta:resourcekey="Literal3Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGatepassTypeId" runat="server" Style="margin-right: 0px"
                        DataSourceID="dsGatePassTypes"
                        DataTextField="GatepassTypeName"
                        DataValueField="GatepassTypeId" meta:resourcekey="ddlGatepassTypeIdResource2">

                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource2">select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="RequiredFieldValidator1" Display="Dynamic"
                        ControlToValidate="ddlGatepassTypeId"
                        runat="server" Text="*"
                        ErrorMessage="≈Œ — ‰Ê⁄ «·≈” ∆–«‰"
                        ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal4" runat="server" Text=" «· «—ÌŒ:" meta:resourcekey="Literal4Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" ID="datGatepassBegDate" SelectedDate="<%# Now.Date %>" />
                </td>

                <td colspan="2">
                    <uc1:MyDate runat="server" ID="datGatepassEndDate" SelectedDate="<%# Now.Date %>" Visible="False" />

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal5" runat="server" Text=" „‰ «·”«⁄…:" meta:resourcekey="Literal5Resource1"></asp:Literal>
                </td>
                <td>
                    <telerik:RadTimePicker ID="timGatepassBegTime" runat="server">
                        <TimeView CellSpacing="-1" Columns="4" Interval="00:15:00" StartTime="05:00:00" EndTime="18:00:00">
                        </TimeView>
                    </telerik:RadTimePicker>
                    <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="timGatepassBegTime"
                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource2"></asp:RequiredFieldValidator>

                </td>

                <td>
                    <asp:Literal ID="Literal6" runat="server" Text="≈·Ï «·”«⁄…:" meta:resourcekey="Literal6Resource1"></asp:Literal>
                </td>
                <td>
                    <telerik:RadTimePicker ID="timGatepassEndTime" runat="server">
                        <TimeView CellSpacing="-1" Columns="4" Interval="00:15:00" StartTime="05:00:00" EndTime="18:00:00">
                        </TimeView>
                    </telerik:RadTimePicker>
                    <asp:CustomValidator ID="ChckbalanceValidator" runat="server"
                        ControlToValidate="datGatepassBegDate$TextBox1"
                        ErrorMessage="·« ÌÊÃœ —’Ìœ ﬂ«›Ì"
                        ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource2"></asp:CustomValidator>
                </td>
            </tr>

            <tr>
                <td colspan="5">
                    <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator5" ControlToValidate="timGatepassEndTime"
                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator5Resource2"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="timGatepassEndTime"
                        ControlToCompare="timGatepassBegTime" Operator="GreaterThan" ErrorMessage="«·—Ã«¡ «· Õﬁﬁ „‰ Êﬁ  «·«” ∆–«‰." meta:resourcekey="CompareValidator1Resource2"></asp:CompareValidator>
                    <asp:CustomValidator ID="ChckLimitsValidator" runat="server"
                        ControlToValidate="datGatepassBegDate$TextBox1"
                        ErrorMessage="ÌÃ» «‰ ·«  ﬁ· «·„œ… ⁄‰ ‰’› ”«⁄… Ê·«  “Ìœ ⁄‰ 3 ”«⁄« "
                        ClientValidationFunction="checklimits" meta:resourcekey="ChckLimitsValidatorResource2"></asp:CustomValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal7" runat="server" Text=" „·«ÕŸ« :" meta:resourcekey="Literal7Resource1"></asp:Literal>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Style="margin-right: 0px" Width="300px" Height="100px" meta:resourcekey="txtNotesResource2"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                        Text="Õ›Ÿ «·≈” ∆–«‰" meta:resourcekey="Button1Resource2" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" CausesValidation="False"
                        Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource2" />
                </td>
            </tr>
        </table>
    </InsertItemTemplate>

    <EditItemTemplate>
        <table width="100%" class="xformview">
            <tr>
                <th colspan="5" class="caption" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈” ∆–«‰ œÊ«„" meta:resourcekey="Literal9Resource1" />
                </th>
            </tr>
            <tr>
                <th colspan="5" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ≈” ∆–«‰ ÃœÌœ " meta:resourcekey="Literal10Resource1" />
                </th>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" meta:resourcekey="Label2Resource1" />
                </td>
                <td rowspan="5" colspan="2">
                    <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                </td>
            </tr>
            <tr>
                <td style="width: 15%">
                    <asp:Literal ID="Literal1" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal1Resource2"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtGatepassId" runat="server" ReadOnly="True" Text='<%# Bind("GatepassId") %>' meta:resourcekey="txtGatepassIdResource1"></asp:TextBox>
                </td>

            </tr>


            <tr>
                <td>
                    <asp:Literal ID="Literal2" runat="server" Text=" ≈”„ «·„ÊŸ›:" meta:resourcekey="Literal2Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtEmployeeName" runat="server" ReadOnly="True" Text='<%# Session("EmployeeNameWithCode") %>' meta:resourcekey="txtEmployeeNameResource1"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal3" runat="server" Text=" ‰Ê⁄ «·«” ∆–«‰:" meta:resourcekey="Literal3Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGatepassTypeId" runat="server" Style="margin-right: 0px"
                        DataSourceID="dsGatePassTypes"
                        DataTextField="GatepassTypeName"
                        DataValueField="GatepassTypeId"
                        SelectedValue='<%# Bind("GatepassTypeId") %>' meta:resourcekey="ddlGatepassTypeIdResource1">
                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Selected="True">....≈Œ —....</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="RequiredFieldValidator5" Display="Dynamic"
                        ControlToValidate="ddlGatepassTypeId"
                        runat="server" Text="*"
                        ErrorMessage="*"
                        ForeColor="Red" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal4" runat="server" Text=" «· «—ÌŒ:" meta:resourcekey="Literal4Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" ID="datGatepassBegDate" SelectedDate='<%# Bind("GatepassBegDate") %>' />

                </td>

                <td>
                    <uc1:MyDate runat="server" ID="datGatepassEndDate" SelectedDate='<%# Bind("GatepassBegDate") %>' Visible="False" />

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal5" runat="server" Text=" „‰ «·”«⁄…:" meta:resourcekey="Literal5Resource1"></asp:Literal>
                </td>
                <td>
                    <telerik:RadTimePicker ID="timGatepassBegTime" runat="server" MinDate="1899-01-01" SelectedDate='<%# Bind("GatepassBegTime")%>'>
                        <TimeView CellSpacing="-1" Columns="4" Interval="00:15:00" StartTime="05:00:00" EndTime="18:00:00">
                        </TimeView>
                    </telerik:RadTimePicker>
                    <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="timGatepassBegTime"
                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>


                </td>

                <td>
                    <asp:Literal ID="Literal6" runat="server" Text="≈·Ï «·”«⁄…:" meta:resourcekey="Literal6Resource1"></asp:Literal>
                </td>
                <td>
                    <telerik:RadTimePicker ID="timGatepassEndTime" runat="server" MinDate="1899-01-01" SelectedDate='<%# Bind("GatepassEndTime")%>'>
                        <TimeView CellSpacing="-1" Columns="4" Interval="00:15:00" StartTime="05:00:00" EndTime="18:00:00">
                        </TimeView>
                    </telerik:RadTimePicker>
                    <asp:CustomValidator ID="ChckbalanceValidator" runat="server"
                        ControlToValidate="datGatepassBegDate$TextBox1"
                        ErrorMessage="·« ÌÊÃœ —’Ìœ ﬂ«›Ì"
                        ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource1"></asp:CustomValidator>
                </td>
            </tr>

            <tr>
                <td colspan="5">
                    <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator1" ControlToValidate="timGatepassEndTime"
                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="timGatepassEndTime"
                        ControlToCompare="timGatepassBegTime" Operator="GreaterThan" ErrorMessage="«·—Ã«¡ «· Õﬁﬁ „‰ Êﬁ  «·«” ∆–«‰." meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                    <asp:CustomValidator ID="ChckLimitsValidator" runat="server"
                        ControlToValidate="datGatepassBegDate$TextBox1"
                        ErrorMessage="ÌÃ» «‰ ·«  ﬁ· «·„œ… ⁄‰ ‰’› ”«⁄… Ê·«  “Ìœ ⁄‰ 3 ”«⁄« "
                        ClientValidationFunction="checklimits" meta:resourcekey="ChckLimitsValidatorResource1"></asp:CustomValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal7" runat="server" Text=" „·«ÕŸ« :" meta:resourcekey="Literal7Resource1"></asp:Literal>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Style="margin-right: 0px" Width="300px" Height="100px" Text='<%# Bind("Notes") %>' meta:resourcekey="txtNotesResource1"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                        Text=" ÕœÌÀ «·≈” ∆–«‰" meta:resourcekey="Button1Resource1" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" CausesValidation="False"
                        Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource2" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>

</asp:FormView>


<%--    </ContentTemplate>
</asp:UpdatePanel>--%>


<asp:Label ID="Label2" runat="server" Text="Label" ForeColor="#FF3300" Visible="False" meta:resourcekey="Label2Resource6"></asp:Label>


<asp:ObjectDataSource ID="dsGatePassTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOGatePassType">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="dsDepartments" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDepartmentsList" TypeName="LookUp"></asp:ObjectDataSource>

<%--<asp:ObjectDataSource ID="dsEmployees" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetEmployeesListByDepartment" TypeName="LookUp">
    <SelectParameters>
        <asp:SessionParameter Name="DepartmentId" SessionField="AccountDepartmentId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>--%>

<asp:ObjectDataSource ID="dsGatePassesByEmployee" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetGatepasssByEmployeeIdDataset" TypeName="ATS.BO.Framework.BOGatepass">
    <SelectParameters>
        <asp:Parameter Name="EmployeeId" Type="Int32" />
        <asp:Parameter Name="PageNo" Type="Int32" />
        <asp:Parameter Name="PageSize" Type="Int32" />
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>


