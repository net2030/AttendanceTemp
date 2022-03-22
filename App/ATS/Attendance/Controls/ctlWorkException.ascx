<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlWorkException.ascx.vb" Inherits="AccountAdmin_Controls_ctlProcessAttendance" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Src="../../ctlDepartmentTree.ascx" TagName="ctlDepartmentTree" TagPrefix="uc1" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/Attendance/Controls/ctlEmployeesLeaveBalance.ascx" TagPrefix="uc1" TagName="ctlEmployeesLeaveBalance" %>



<div align="right" style="margin-bottom: 20px; margin-top: 20px; border: solid;" runat="server" id="dvSearch">
    <table width="98%" style="margin: 10px 10px 10px 10px">
        <tr>
            <td>
                <div style="width: 100px; height: 10px" />
            </td>
        </tr>
        <tr>
            <td style="width: 150px">
                <asp:Literal ID="Literal1" runat="server" Text=" ÇáÞÓã :" meta:resourcekey="Literal1Resource1"></asp:Literal>
            </td>

            <td colspan="2">
                <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" SelectedValue="1" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="width: 100px; height: 10px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal2" runat="server" Text=" ÊÇÑíÎ ÇáÈÏÇíÉ :" meta:resourcekey="Literal2Resource1"></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Startdate" />
            </td>
            <td style="width: 150px">
                <asp:Literal ID="Literal3" runat="server" Text="  ÊÇÑíÎ ÇáäåÇíÉ :" meta:resourcekey="Literal3Resource1"></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Enddate" />
                <asp:CustomValidator ID="CustomValidator2" runat="server"
                    ControlToValidate="EndDate$TextBox1"
                    ErrorMessage="End date must be greater or equa !"
                    ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>
                <div style="width: 100px; height: 10px" />
            </td>
        </tr>
        <tr>

            <td>
                <asp:Button ID="btnAdd" runat="server"
                    Text="ÚÑÖ "
                    UseSubmitBehavior="False" meta:resourcekey="btnAddResource1" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="width: 100px; height: 10px" />
            </td>
        </tr>
    </table>

</div>

<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
    DataKeyNames="WorkExceptionId" Caption="ÞÇÆãÉ ÅÓÊËÇÁÇÊ ÇáÏæÇã"
    SkinID="xgridviewSkinEmployee" Width="98%" ShowHeaderWhenEmpty="True" DataSourceID="dsWorkExceptions" meta:resourcekey="GridView2Resource1">
    <Columns>

        <asp:BoundField DataField="WorkExceptionId" HeaderText="ãÓáÓá" meta:resourcekey="BoundFieldResource1" />

        <asp:BoundField DataField="EmployeeCode" HeaderText="ÑÞã ÇáÈÕãÉ" SortExpression="EmployeeCode" meta:resourcekey="BoundFieldResource2">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="EmployeeName" HeaderText="ÅÓã ÇáãæÙÝ" SortExpression="EmployeeName" meta:resourcekey="BoundFieldResource3">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="WorkExceptionTypeName" HeaderText="äæÚ ÇáÅÓÊËäÇÁ" SortExpression="WorkExceptionTypeName" meta:resourcekey="BoundFieldResource4">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="WorkExceptionBegDate" HeaderText="ÊÇÑíÎ ÇáÈÏÇíÉ" SortExpression="WorkExceptionBegDate" meta:resourcekey="BoundFieldResource5">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="WorkExceptionEndDate" HeaderText="ÊÇÑíÎ ÇáäåÇíÉ" SortExpression="WorkExceptionEndDate" meta:resourcekey="BoundFieldResource6">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="AprovalStatus" HeaderText="ÍÇáÉ ÇáØáÈ" SortExpression="AprovalStatus" meta:resourcekey="BoundFieldResource7">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:BoundField DataField="ApprovalNotes" HeaderText="ãáÇÍÙÇÊ" meta:resourcekey="BoundFieldResource8" />

        <asp:TemplateField HeaderText="ÊÚÏíá" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit" CausesValidation="False"></asp:LinkButton>
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
<%--   </ContentTemplate>
</asp:UpdatePanel>--%>

<%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(166, 2) Then%>
<p>
    <asp:Button ID="btnAdd0" runat="server" OnClick="btnNew"
        Text="ÊÓÌíá ÌÏíÏ"
        UseSubmitBehavior="False" CausesValidation="False" meta:resourcekey="btnAdd0Resource1" />
</p>
<%End If%>

<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>

<asp:FormView ID="FormView1" runat="server"
    SkinID="formviewskinemployee"
    DefaultMode="Insert" Width="98%" Visible="False" meta:resourcekey="FormView1Resource1">
    <InsertItemTemplate>
        <table width="100%" class="xformview">
            <tr>
                <th colspan="7" class="caption" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÅÓÊËäÇÁ ÏæÇã" meta:resourcekey="Literal9Resource2" />
                </th>
            </tr>
            <tr>
                <th colspan="7" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal10" runat="server" Text="ÊÓÌíá ÈíÇäÇÊ ÅÓÊËäÇÁ " meta:resourcekey="Literal10Resource2" />
                </th>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource2" />
                </td>
                <td rowspan="6">
                    <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Literal ID="Literal4" runat="server" Text="ÑÞã ÇáÍÇÓÈ:" meta:resourcekey="Literal4Resource2"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtWorkExceptionId" runat="server" ReadOnly="True" Text='<%# Bind("WorkExceptionId") %>' meta:resourcekey="txtWorkExceptionIdResource2"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal5" runat="server" Text="ÇáÞÓã:" meta:resourcekey="Literal5Resource2"></asp:Literal>
                </td>
                <td colspan="3">
                    <uc1:ctlDepartmentTree runat="server" ID="ddlDepartments" AutoPostBack="true" OnSelectedNodeChanged="ddlDepartments_SelectedIndexChanged" />
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal6" runat="server" Text="ÅÓã ÇáãæÙÝ:" meta:resourcekey="Literal6Resource1"></asp:Literal>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlEmployees" runat="server" Style="margin-right: 0px" AutoPostBack="True"
                        DataTextField="EmployeeName"
                        DataValueField="EmployeeId" OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged" meta:resourcekey="ddlEmployeesResource2">
                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="RequiredFieldValidator1" Display="Dynamic"
                        ControlToValidate="ddlEmployees"
                        runat="server" Text="*"
                        ErrorMessage="*"
                        ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>

                    <asp:Literal ID="Literal11" runat="server" Text="ÑÞã ÇáÈÕãÉ:" meta:resourcekey="Literal11Resource1" />

                    <asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" meta:resourcekey="txtEmployeeCodeResource1" />
                    <asp:Button ID="Button3" runat="server" Text="ÈÍË" CausesValidation="False" OnClick="FindByEmployeeCode" meta:resourcekey="Button3Resource1" />
                </td>

            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal7" runat="server" Text="äæÚ ÇáÅÓÊËäÇÁ:" meta:resourcekey="Literal7Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkExceptionTypeId" runat="server" Style="margin-right: 0px"
                        DataSourceID="dsWorkExceptionTypes"
                        DataTextField="WorkExceptionTypeName"
                        DataValueField="WorkExceptionTypeId"
                        SelectedValue='<%# Bind("WorkExceptionTypeId") %>' meta:resourcekey="ddlWorkExceptionTypeIdResource2">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="RequiredFieldValidator2" Display="Dynamic"
                        ControlToValidate="ddlWorkExceptionTypeId"
                        runat="server" Text="*"
                        ErrorMessage="*"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Literal ID="Literal8" runat="server" Text="íÈÏÃ ãä:" meta:resourcekey="Literal8Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" ID="datWorkExceptionBegDate" SelectedDate='<%# Now.Date %>' />
                </td>

                <td style="width: 10%">
                    <asp:Literal ID="Literal12" runat="server" Text="íäÊåí Ýí:" meta:resourcekey="Literal12Resource1"></asp:Literal>
                </td>
                <td colspan="2">
                    <uc1:MyDate runat="server" ID="datWorkExceptionEndDate" SelectedDate='<%# Now.Date %>' />
                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                        ControlToValidate="datWorkExceptionEndDate$TextBox1"
                        ErrorMessage="End date must be greater or equa !"
                        ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource3"></asp:CustomValidator>
                    <asp:CustomValidator ID="ChckbalanceValidator" runat="server"
                        ControlToValidate="datWorkExceptionEndDate$TextBox1"
                        ErrorMessage="áÇ íæÌÏ ÑÕíÏ ßÇÝí"
                        ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource2"></asp:CustomValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal13" runat="server" Text="æÕÝ ÇáãåãÉ:" meta:resourcekey="Literal13Resource1"></asp:Literal>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="300px" Height="70px" Text='<%# Bind("Notes") %>' meta:resourcekey="txtNotesResource2"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                        Text="ÍÝÙ ÇáÅÓÊËäÇÁ" meta:resourcekey="Button1Resource2" />
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
                <th colspan="7" class="caption" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÅÓÊËäÇÁ ÏæÇã" meta:resourcekey="Literal9Resource1" />
                </th>
            </tr>
            <tr>
                <th colspan="7" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal10" runat="server" Text="ÊÍÏíË ÈíÇäÇÊ ÇÓÊËäÇÁ" meta:resourcekey="Literal10Resource1" />
                </th>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1" />
                </td>
                <td rowspan="6">
                    <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                </td>
            </tr>
            <tr>
                <td style="width: 13%">
                    <asp:Literal ID="Literal4" runat="server" Text="ÑÞã ÇáÍÇÓÈ" meta:resourcekey="Literal4Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtWorkExceptionId" runat="server" ReadOnly="True" Text='<%# Bind("WorkExceptionId") %>' meta:resourcekey="txtWorkExceptionIdResource1"></asp:TextBox>
                </td>

            </tr>


            <tr>
                <td>
                    <asp:Literal ID="Literal5" runat="server" Text="ÅÓã ÇáãæÙÝ:" meta:resourcekey="Literal5Resource1"></asp:Literal>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlEmployees" runat="server" Style="margin-right: 0px"
                        DataTextField="EmployeeName"
                        DataValueField="EmployeeId" meta:resourcekey="ddlEmployeesResource1">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal14" runat="server" Text="äæÚ ÇáÅÓÊËäÇÁ:" meta:resourcekey="Literal14Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkExceptionTypeId" runat="server" Style="margin-right: 0px"
                        DataSourceID="dsWorkExceptionTypes"
                        DataTextField="WorkExceptionTypeName"
                        DataValueField="WorkExceptionTypeId"
                        SelectedValue='<%# Bind("WorkExceptionTypeId") %>' meta:resourcekey="ddlWorkExceptionTypeIdResource1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="RequiredFieldValidator2" Display="Dynamic"
                        ControlToValidate="ddlWorkExceptionTypeId"
                        runat="server" Text="*"
                        ErrorMessage="*"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="width: 10%">
                    <asp:Literal ID="Literal15" runat="server" Text="íÈÏÃ ãä:" meta:resourcekey="Literal15Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" ID="datWorkExceptionBegDate" SelectedDate='<%# Bind("WorkExceptionBegDate") %>' />

                </td>

                <td style="width: 10%">
                    <asp:Literal ID="Literal16" runat="server" Text="íäÊåí Ýí:" meta:resourcekey="Literal16Resource1"></asp:Literal>
                </td>
                <td colspan="2">
                    <uc1:MyDate runat="server" ID="datWorkExceptionEndDate" SelectedDate='<%# Bind("WorkExceptionEndDate") %>' />
                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                        ControlToValidate="datWorkExceptionEndDate$TextBox1"
                        ErrorMessage="End date must be greater or equa !"
                        ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource2"></asp:CustomValidator>
                    <asp:CustomValidator ID="ChckbalanceValidator" runat="server"
                        ControlToValidate="datWorkExceptionEndDate$TextBox1"
                        ErrorMessage="áÇ íæÌÏ ÑÕíÏ ßÇÝí"
                        ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource1"></asp:CustomValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal17" runat="server" Text="æÕÝ ÇáãåãÉ:" meta:resourcekey="Literal17Resource1"></asp:Literal>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="300px" Height="70px" Text='<%# Bind("Notes") %>' meta:resourcekey="txtNotesResource1"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                        Text="ÊÍÏíË ÇáÅÓÊËäÇÁ" meta:resourcekey="Button1Resource1" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;"
                        Text="ÅáÛÇÁ ÇáÃãÑ" meta:resourcekey="Button2Resource1" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>

</asp:FormView>

<%--    </ContentTemplate>
</asp:UpdatePanel>--%>

<asp:ObjectDataSource ID="dsWorkExceptionTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOWorkExceptionType">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>



<asp:ObjectDataSource ID="dsDepartments" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDepartmentsList" TypeName="LookUp"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="dsWorkExceptions" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWorkExceptionsByDepartmentDataset" TypeName="ATS.BO.Framework.BOWorkException">
    <SelectParameters>
        <asp:Parameter Name="DepartmentId" Type="Int32" />
        <asp:Parameter Name="BegDate" Type="DateTime" />
        <asp:Parameter Name="EndDate" Type="DateTime" />
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="100" Name="PageSize" Type="Int32" />
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
