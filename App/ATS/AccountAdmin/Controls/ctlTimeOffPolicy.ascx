<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlTimeOffPolicy.ascx.vb" Inherits="AccountAdmin_Controls_ctlTimeOffPolicy" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/AccountAdmin/Controls/ctlEmployeesLeavePolicy.ascx" TagPrefix="uc1" TagName="ctlEmployeesLeavePolicy" %>



<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
    DataKeyNames="TimeOffPolicyId" Caption="����� ��������"
    SkinID="xgridviewSkinEmployee" Width="900px" CssClass="TableView" ShowHeaderWhenEmpty="True" DataSourceID="dsTimeOffPolicys" meta:resourcekey="GridView2Resource1">
    <Columns>

        <asp:BoundField DataField="TimeOffPolicyId" HeaderText="��� ������" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />

        <asp:BoundField DataField="TimeOffPolicyName" HeaderText="��� �������" meta:resourcekey="BoundFieldResource2" />

        <asp:BoundField DataField="EffectiveDate" HeaderText="����� ����� ��������" meta:resourcekey="BoundFieldResource3" />



        <asp:TemplateField HeaderText="�����" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit" ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="���" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
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
        <td>&nbsp<asp:Button ID="btnAddTimeOffPolicy" runat="server"
            Text="�����" Width="75px" UseSubmitBehavior="False" meta:resourcekey="btnAddTimeOffPolicyResource1" />

        </td>
    </tr>
</table>

<table>
    <tr>
        <td>
            <asp:FormView ID="FormView1" runat="server"
                SkinID="formviewskinemployee"
                DefaultMode="Insert" Width="1000px" Visible="False" meta:resourcekey="FormView1Resource1">
                <InsertItemTemplate>
                    <table width="100%" class="xformview">
                        <tr>
                            <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal9" runat="server" Text="������ ������� " meta:resourcekey="Literal9Resource2" />
                            </th>
                        </tr>
                        <tr>
                            <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal10" runat="server" Text="����� ����� ����" meta:resourcekey="Literal10Resource2" />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text="��� ������" meta:resourcekey="Literal1Resource2"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTimeOffPolicyId" runat="server" ReadOnly="True" Text='<%# Bind("TimeOffPolicyId") %>' meta:resourcekey="txtTimeOffPolicyIdResource2"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="��� �������" meta:resourcekey="Literal2Resource1"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtTimeOffPolicyName" runat="server" Style="margin-right: 0px" Width="390px" Text='<%# Bind("TimeOffPolicyName") %>' meta:resourcekey="txtTimeOffPolicyNameResource2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="����� ���" meta:resourcekey="Literal3Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" meta:resourcekey="ddlTypeResource2">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource5">...����...</asp:ListItem>
                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource6"> ��������</asp:ListItem>
                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource7"> �����������</asp:ListItem>
                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource8"> ���������</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0"
                                    ID="rfvDDL_Product7" Display="Dynamic"
                                    ControlToValidate="ddlType"
                                    runat="server" Text="*"
                                    ErrorMessage="*"
                                    ForeColor="Red" meta:resourcekey="rfvDDL_Product7Resource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="����� ����� ���" meta:resourcekey="Literal4Resource1"></asp:Literal>
                            </td>
                            <td>
                                <uc1:MyDate runat="server" ID="datEffectiveDate" />
                                <%--<telerik:raddatepicker runat="server" id="datEffectiveDate" selecteddate='<%# Eval("EffectiveDate")%>'></telerik:raddatepicker>--%>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                    Text="��� �������" meta:resourcekey="Button1Resource2" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;"
                                    Text="����� �����" meta:resourcekey="Button2Resource2" />
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>

                <EditItemTemplate>
                    <table width="100%" class="xformview">
                        <tr>
                            <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal9" runat="server" Text="������ �����" meta:resourcekey="Literal9Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                                <asp:Literal ID="Literal10" runat="server" Text="����� ����� ����" meta:resourcekey="Literal10Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text="��� ������" meta:resourcekey="Literal1Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTimeOffPolicyId" runat="server" ReadOnly="True" Text='<%# Bind("TimeOffPolicyId") %>' meta:resourcekey="txtTimeOffPolicyIdResource1"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="��� �������" meta:resourcekey="Literal5Resource1"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtTimeOffPolicyName" runat="server" Style="margin-right: 0px" Width="390px" Text='<%# Bind("TimeOffPolicyName") %>' meta:resourcekey="txtTimeOffPolicyNameResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="����� ���" meta:resourcekey="Literal6Resource1"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" SelectedValue='<%# Bind("TypeId") %>' Enabled="False" meta:resourcekey="ddlTypeResource1">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Selected="True">...����...</asp:ListItem>
                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource2"> ��������</asp:ListItem>
                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource3"> �����������</asp:ListItem>
                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource4"> ���������</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0"
                                    ID="rfvDDL_Product2" Display="Dynamic"
                                    ControlToValidate="ddlType"
                                    runat="server" Text="*"
                                    ErrorMessage="*"
                                    ForeColor="Red" meta:resourcekey="rfvDDL_Product2Resource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="����� �������" meta:resourcekey="Literal7Resource1"></asp:Literal>
                            </td>
                            <td>
                                <uc1:MyDate runat="server" ID="datEffectiveDate" selecteddate='<%# Eval("EffectiveDate")%>' />
                                <%--<telerik:raddatepicker runat="server" id="datEffectiveDate" selecteddate='<%# Eval("EffectiveDate")%>'></telerik:raddatepicker>--%>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                    Text="����� �������" meta:resourcekey="Button1Resource1" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" Text="����� �����" meta:resourcekey="Button2Resource1" />

                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>

            </asp:FormView>
            <br />
            <x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                SkinID="xgridviewSkinEmployee"
                Width="1000px"
                DataKeyNames="TimeOffPolicyDetailId,GatepassTypeId,EarnPeriodId,ResetToZeroPeriodId,VersionNo"
                Caption=" ��������"
                ShowFooter="True" meta:resourcekey="GridView1Resource1">
                <Columns>

                    <asp:TemplateField HeaderText="�����" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <%# Eval("TypeName")%>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlType" runat="server" DataTextField="TypeName" DataValueField="TypeId" Enabled="False" meta:resourcekey="ddlTypeResource3">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource9">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0"
                                ID="rfvDDL_Product3" Display="Dynamic"
                                ControlToValidate="ddlType"
                                runat="server" Text="*"
                                ErrorMessage="Please Select Employee"
                                ForeColor="Red" meta:resourcekey="rfvDDL_Product3Resource1"></asp:RequiredFieldValidator>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <asp:DropDownList ID="ddlType" runat="server" DataTextField="TypeName" DataValueField="TypeId" meta:resourcekey="ddlTypeResource4">
                            </asp:DropDownList>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="���� �������" meta:resourcekey="TemplateFieldResource4">

                        <ItemTemplate>
                            <%# Eval("EarnPeriod")%>'
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEarnPeriod" runat="server" DataTextField="PeriodName" DataValueField="PeriodId" meta:resourcekey="ddlEarnPeriodResource1">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource10">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0"
                                ID="rfvDDL_Product4" Display="Dynamic"
                                ControlToValidate="ddlEarnPeriod"
                                runat="server" Text="*"
                                ErrorMessage="Please Select Employee"
                                ForeColor="Red" meta:resourcekey="rfvDDL_Product4Resource1"></asp:RequiredFieldValidator>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <asp:DropDownList ID="ddlEarnPeriod" runat="server" DataTextField="PeriodName" DataValueField="PeriodId" meta:resourcekey="ddlEarnPeriodResource2">
                            </asp:DropDownList>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="���� �������" meta:resourcekey="TemplateFieldResource5">

                        <ItemTemplate>
                            <%# Eval("ResetToZeroPeriod")%>'
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlResetToZeroPeriod" runat="server" DataTextField="PeriodName" DataValueField="PeriodId" meta:resourcekey="ddlResetToZeroPeriodResource1">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource11">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0"
                                ID="rfvDDL_Product5" Display="Dynamic"
                                ControlToValidate="ddlResetToZeroPeriod"
                                runat="server" Text="*"
                                ErrorMessage="Please Select Employee"
                                ForeColor="Red" meta:resourcekey="rfvDDL_Product5Resource1"></asp:RequiredFieldValidator>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <asp:DropDownList ID="ddlResetToZeroPeriod" runat="server" DataTextField="PeriodName" DataValueField="PeriodId" meta:resourcekey="ddlResetToZeroPeriodResource2">
                            </asp:DropDownList>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText=" ������ ����������" meta:resourcekey="TemplateFieldResource6">

                        <ItemTemplate>
                            <%# Eval("InitialSetToMinutes")%>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <telerik:radnumerictextbox id="txtInitialSetToMinutes" value=' <%# CInt(Eval("InitialSetToMinutes"))%>' runat="server" width="70px">
                            <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                        </telerik:radnumerictextbox>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <telerik:radnumerictextbox id="txtInitialSetToMinutes" value="0" runat="server" width="70px">
                           <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                       </telerik:radnumerictextbox>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="������ ����������" meta:resourcekey="TemplateFieldResource7">
                        <ItemTemplate>
                            <%# Eval("ResetToMinutes")%>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <telerik:radnumerictextbox id="txtResetToMinutes" value=' <%# CInt(Eval("ResetToMinutes"))%>' runat="server" width="70px">
                            <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                        </telerik:radnumerictextbox>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <telerik:radnumerictextbox id="txtResetToMinutes" value="0" runat="server" width="70px">
                           <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                       </telerik:radnumerictextbox>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="������ �������" meta:resourcekey="TemplateFieldResource8">

                        <ItemTemplate>
                            <%# Eval("EarnMinutes")%>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <telerik:radnumerictextbox id="txtEarnMinutes" value=' <%# CInt(Eval("EarnMinutes"))%>' runat="server" width="70px">
                            <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                        </telerik:radnumerictextbox>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <telerik:radnumerictextbox id="txtEarnMinutes" value="0" runat="server" width="70px">
                           <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                       </telerik:radnumerictextbox>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="���� ������ �����" meta:resourcekey="TemplateFieldResource9">

                        <ItemTemplate>
                            <%# Eval("MinValue")%>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <telerik:radnumerictextbox id="txtMinValue" value=' <%# CInt(Eval("MinValue"))%>' runat="server" width="70px">
                            <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                        </telerik:radnumerictextbox>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <telerik:radnumerictextbox id="txtMinValue" value="0" runat="server" width="70px">
                           <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                       </telerik:radnumerictextbox>
                        </FooterTemplate>


                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="���� ������ �����" meta:resourcekey="TemplateFieldResource10">

                        <ItemTemplate>
                            <%# Eval("MaxValue")%>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <telerik:radnumerictextbox id="txtMaxValue" value=' <%# CInt(Eval("MaxValue"))%>' runat="server" width="70px">
                            <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                        </telerik:radnumerictextbox>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <telerik:radnumerictextbox id="txtMaxValue" value="0" runat="server" width="70px">
                           <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                       </telerik:radnumerictextbox>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="����� �������" meta:resourcekey="TemplateFieldResource11">

                        <ItemTemplate>
                            <asp:Label ID="lbl1" runat="server" Text='<%# Eval("IsCarryForwardName") %>' Font-Size="Larger" Font-Names="Arial" Font-Bold="True" meta:resourcekey="lbl1Resource1"></asp:Label>

                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:CheckBox ID="chkIsCarryForward" Checked='<%# CInt(Eval("IsCarryForward")) %>' runat="server" meta:resourcekey="chkIsCarryForwardResource1"></asp:CheckBox>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <asp:CheckBox ID="chkIsCarryForward" runat="server" meta:resourcekey="chkIsCarryForwardResource2"></asp:CheckBox>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="����� �������" meta:resourcekey="TemplateFieldResource12">

                        <ItemTemplate>
                            <%# Eval("strEffectiveDate")%>
                        </ItemTemplate>

                        <EditItemTemplate>

                           <%-- <telerik:raddatepicker runat="server" id="datEffectiveDate" selecteddate='<%# Eval("EffectiveDate")%>'></telerik:raddatepicker>--%>
                                            <uc1:MyDate runat="server" ID="datEffectiveDate" SelectedDate='<%# Eval("EffectiveDate")%>' />
                        </EditItemTemplate>

                        <FooterTemplate>
                           <%-- <telerik:raddatepicker runat="server" id="datEffectiveDate" selecteddate='<%# Now%>'></telerik:raddatepicker>--%>
                                            <uc1:MyDate runat="server" ID="datEffectiveDate" SelectedDate="<%# Now%>" />
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField meta:resourcekey="TemplateFieldResource13">

                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update" meta:resourcekey="lnkUpdateResource1"></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" meta:resourcekey="lnkCancelResource1"></asp:LinkButton>

                        </EditItemTemplate>

                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" CommandName="Edit" Visible='<%# If(Me.FormView1.CurrentMode = FormViewMode.Edit, True, False) %>' CssClass="edit_button" runat="server" Text="Edit" CausesValidation="False" meta:resourcekey="lnkEditResource1"></asp:LinkButton>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:LinkButton ID="lnkAddNew" runat="server" CommandName="AddNew" Text="���" meta:resourcekey="lnkAddNewResource1"></asp:LinkButton>
                        </FooterTemplate>

                    </asp:TemplateField>

                </Columns>

                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <th scope="col">
                                <asp:Literal ID="Literal11" runat="server" Text="�����" meta:resourcekey="Literal11Resource1"></asp:Literal></th>
                            <th scope="col">
                                <asp:Literal ID="Literal8" runat="server" Text="���� �������" meta:resourcekey="Literal8Resource1"></asp:Literal></th>
                            <th scope="col">
                                <asp:Literal ID="Literal61" runat="server" Text="���� �������" meta:resourcekey="Literal61Resource1"></asp:Literal></th>
                            <th scope="col">
                                <asp:Literal ID="Literal12" runat="server" Text=" ������ ����������" meta:resourcekey="Literal12Resource1"></asp:Literal></th>
                            <th scope="col">
                                <asp:Literal ID="Literal13" runat="server" Text="������ ����������" meta:resourcekey="Literal13Resource1"></asp:Literal></th>
                            <th scope="col">
                                <asp:Literal ID="Literal14" runat="server" Text="������ �������" meta:resourcekey="Literal14Resource1"></asp:Literal></th>
                            <th scope="col">
                                <asp:Literal ID="Literal15" runat="server" Text="����� �������" meta:resourcekey="Literal15Resource1"></asp:Literal></th>
                            <th scope="col">&nbsp;</th>
                        </tr>
                        <tr>

                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" DataTextField="TypeName" DataValueField="TypeId" meta:resourcekey="ddlTypeResource5">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource12">...����...</asp:ListItem>
                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource13"> ��������</asp:ListItem>
                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource14"> �����������</asp:ListItem>
                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource15"> ���������</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0"
                                    ID="rfvDDL_Product6" Display="Dynamic"
                                    ControlToValidate="ddlType"
                                    runat="server" Text="*"
                                    ErrorMessage="Please Select Employee"
                                    ForeColor="Red" meta:resourcekey="rfvDDL_Product6Resource1"></asp:RequiredFieldValidator>
                            </td>

                            <td>
                                <asp:DropDownList ID="ddlResetToZeroPeriod" runat="server" DataTextField="PeriodName" DataValueField="PeriodId" meta:resourcekey="ddlResetToZeroPeriodResource3">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource16">Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0"
                                    ID="RequiredFieldValidator2" Display="Dynamic"
                                    ControlToValidate="ddlResetToZeroPeriod"
                                    runat="server" Text="*"
                                    ErrorMessage="Please Select Employee"
                                    ForeColor="Red" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                            </td>

                            <td>
                                <asp:DropDownList ID="ddlEarnPeriod" runat="server" DataTextField="PeriodName" DataValueField="PeriodId" meta:resourcekey="ddlEarnPeriodResource3">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource17">Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0"
                                    ID="RequiredFieldValidator1" Display="Dynamic"
                                    ControlToValidate="ddlEarnPeriod"
                                    runat="server" Text="*"
                                    ErrorMessage="Please Select Employee"
                                    ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            </td>

                            <td>
                                <telerik:radnumerictextbox id="txtInitialSetToMinutes" value="0" runat="server" width="70px">
                                 <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                             </telerik:radnumerictextbox>
                            </td>

                            <td>
                                <telerik:radnumerictextbox id="txtResetToMinutes" value="0" runat="server" width="70px">
                                 <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                             </telerik:radnumerictextbox>
                            </td>

                            <td>
                                <telerik:radnumerictextbox id="txtEarnMinutes" value="0" runat="server" width="70px">
                                 <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                             </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsCarryForward" runat="server"></asp:CheckBox>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtMinValue" value="0" runat="server" width="70px">
                                 <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                             </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtMaxValue" value="0" runat="server" width="70px">
                                 <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                             </telerik:radnumerictextbox>
                            </td>
                            <td>
               <%--                 <telerik:raddatepicker runat="server" id="datEffectiveDate" selecteddate='<%# Eval("Now") %>' culture="ar-SA" hiddeninputtitleattibute="Visually hidden input created for functionality purposes." meta:resourcekey="datEffectiveDateResource5" wrappertablesummary="Table holding date picker control for selection of dates.">
                           <calendar usecolumnheadersasselectors="False" userowheadersasselectors="False" viewselectortext="x">
                           </calendar>
                           <dateinput dateformat="d/M/yyyy" displaydateformat="d/M/yyyy" labelwidth="64px" width="">
                           </dateinput>
                           <datepopupbutton cssclass="" hoverimageurl="" imageurl="" />
                    </telerik:raddatepicker>--%>
                                <uc1:MyDate runat="server" ID="MyDate" />
                            </td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="���" meta:resourcekey="btnAddResource1" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

                <PagerSettings PageButtonCount="21" />
            </x:GridView>


            <br />
            <br />
        </td>
        <td>
            <% If Me.FormView1.CurrentMode = FormViewMode.Edit And GridView1.Rows.Count > 0 Then%>
            <uc1:ctlEmployeesLeavePolicy runat="server" ID="ctlEmployeesLeavePolicy" />
            <% End If%>
        </td>
    </tr>
</table>






<asp:ObjectDataSource ID="dsPolicy" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOPolicy">
    <SelectParameters>
        <asp:Parameter DefaultValue="1" Name="UserAccountId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>





<asp:ObjectDataSource ID="dsTimeOffPolicys" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetTimeOffPolicysDataset" TypeName="ATS.BO.Framework.BOTimeOffPolicy">
    <SelectParameters>
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="100" Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>


