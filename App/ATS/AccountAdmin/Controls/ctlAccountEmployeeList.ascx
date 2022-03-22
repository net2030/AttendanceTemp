<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountEmployeeList.ascx.vb" Inherits="Controls_ctlAccountEmployeeList" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>
<%@ Register Src="~/GeneralControls/ctlDepartmentScope.ascx" TagPrefix="uc1" TagName="ctlDepartmentScope" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>


<script type="text/javascript">
    function ValidatorUpdateDisplay(val1) {
        if (val1.id == 'C_C_C_CtlAccountEmployeeForm1_FormView1_CompareValidator1')
        console.log(val1)

        if (val1.id == 'C_C_C_CtlAccountEmployeeForm1_FormView1_rexNumber') {
            {
                if (!val1.isvalid) {

                    var ctrl = document.getElementById(val1.controltovalidate);
                    ctrl.style.background = '#FFAAAA';

                    val1.style.visibility = "visible"
                    val1.style.display = "inline-block"
                }
                else {

                    var ctrl = document.getElementById(val1.controltovalidate);
                    ctrl.style.backgroundColor = '';

                    val1.style.visibility = "hidden"
                    val1.style.display = "none"
                }
                return;
            }
           
        }


        if (val1.id == 'C_C_C_CtlAccountEmployeeForm1_FormView1_CompareValidator1') {
            {
                var ctrl = document.getElementById(val1.controltovalidate);
                var ctrl1 = document.getElementById("C_C_C_CtlAccountEmployeeForm1_FormView1_VerifyPasswordTextbox")

                if (!val1.isvalid) {

                    ctrl.style.background = '#FFAAAA';
                    ctrl1.style.background = '#FFAAAA';

                    val1.style.visibility = "visible"
                    val1.style.display = "inline-block"
                }
                else {

                   
                    ctrl.style.backgroundColor = '';
                    ctrl1.style.backgroundColor = '';

                    val1.style.visibility = "hidden"
                    val1.style.display = "none"
                }
                return;
            }

        }


        for (var i = 0; i < Page_Validators.length; i++) {
            var val = Page_Validators[i];
            var ctrl = document.getElementById(val.controltovalidate);
           
            if (ctrl != null && ctrl.style != null) {
                if (!val.isvalid)
                    ctrl.style.background = '#FFAAAA';
                else {
                    if (ctrl.id.indexOf("txtGovId") < 0 && ctrl.id.indexOf("PasswordTextBox") < 0)
                    ctrl.style.backgroundColor = '';
                }
            }
        }
    }
</script>
<%--<script type="text/javascript">
   function ShowWindow() {
       alert("");
       var manager = GetRadWindowManager();
       // txtShowWindow is the id of a textarea on the page.
       var txtShowWindow = document.getElementById("txtShowWindow");
       var window1 = manager.getWindowByName("RadWindow1");
       window1.setUrl(txtShowWindow.value);
       window1.set_title(txtShowWindow.value);
       window1.show();
   }
   
   function ChangeCheckBoxState(id, checkState) {
       var cb = document.getElementById(id);
       if (cb != null)
           cb.checked = checkState;
   }
   
   function ChangeAllCheckBoxStates(checkState) {
       // Toggles through all of the checkboxes defined in the CheckBoxIDs array
       // and updates their value to the checkState input parameter
       if (CheckBoxIDs != null) {
           for (var i = 0; i < CheckBoxIDs.length; i++)
               ChangeCheckBoxState(CheckBoxIDs[i], checkState);
       }
   }
   
   function ChangeHeaderAsNeeded() {
       // Whenever a checkbox in the GridView is toggled, we need to
       // check the Header checkbox if ALL of the GridView checkboxes are
       // checked, and uncheck it otherwise
       if (CheckBoxIDs != null) {
           // check to see if all other checkboxes are checked
           for (var i = 1; i < CheckBoxIDs.length; i++) {
               var cb = document.getElementById(CheckBoxIDs[i]);
               if (!cb.checked) {
                   // Whoops, there is an unchecked checkbox, make sure
                   // that the header checkbox is unchecked
                   ChangeCheckBoxState(CheckBoxIDs[0], false);
                   return;
               }
           }
   
           // If we reach here, ALL GridView checkboxes are checked
           ChangeCheckBoxState(CheckBoxIDs[0], true);
       }
   }
   </script>--%>
<script type="text/javascript">
    function clientShow(sender, eventArgs) {
        var txtInput = document.getElementById("txtInput");
        sender.argument = 1;
    }

</script>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <ContentTemplate>--%>
<asp:Label ID="lblErr" runat="server" ForeColor="Red" meta:resourcekey="lblErrResource1"></asp:Label>
<x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeId"
    DataSourceID="dsAccountEmployeeList" SkinID="xgridviewSkinEmployee" Caption="ﬁ«∆„… «·„ÊŸ›Ì‰" Width="98%" PageSize="30" meta:resourcekey="GridView1Resource1">
    <Columns>
        <asp:BoundField DataField="EmployeeId" HeaderText="—ﬁ„ «·Õ«”»" SortExpression="EmployeeId" meta:resourcekey="BoundFieldResource1">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="BadgeNo" HeaderText="—ﬁ„ «·»’„…" SortExpression="BadgeNo" meta:resourcekey="BoundFieldResource2">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

          <asp:BoundField DataField="GovId" HeaderText="—ﬁ„ «·ÂÊÌ…" SortExpression="GovId" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>

        <asp:BoundField DataField="FirstName" HeaderText="«·√”„ «·√Ê·" SortExpression="FirstName" meta:resourcekey="BoundFieldResource3">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="LastName" HeaderText="«·√”„ «·√ŒÌ—" SortExpression="LastName" meta:resourcekey="BoundFieldResource4">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="EmailAddress" HeaderText="«·»—Ìœ «·≈·ﬂ —Ê‰Ì" SortExpression="EmailAddress" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>

        <asp:BoundField DataField="DepartmentName" HeaderText="≈”„ «·ﬁ”„" SortExpression="DepartmentName" meta:resourcekey="BoundFieldResource5">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="EmployerName" HeaderText="ÃÂ… «· ÊŸÌ›" SortExpression="EmployerName" meta:resourcekey="BoundFieldResource6">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="ContractTypeName" HeaderText="‰Ê⁄ «·⁄ﬁœ" SortExpression="ContractTypeName" meta:resourcekey="BoundFieldResource7">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText=" ⁄œÌ·" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit" ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Õ–›" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
            <ItemTemplate>
                <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    OnClientClick="confirm(' Â·  —Ìœ »«· √ﬂÌœ Õ–› «·„ÊŸ› „⁄ Ã„Ì⁄ »Ì«‰« Âø');" CausesValidation="False" ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
            <HeaderTemplate>
                <asp:Image ID="Image1" runat="server" __designer:wfdid="w5" ToolTip="<%$ Resources:TimeLive.Web, Disabled_text %>" ImageUrl="~/Images/Disabled.gif" meta:resourcekey="Image1Resource1"></asp:Image>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Image ID="Image2" runat="server" Visible='<%# IIf(IsDBNull(Eval("IsActive")), "false", Not Eval("IsActive")) %>' __designer:wfdid="w4" ToolTip="<%$ Resources:TimeLive.Web, Disabled_text %>" ImageUrl="~/Images/Disabled.gif" meta:resourcekey="Image2Resource1"></asp:Image>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="1%" />
        </asp:TemplateField>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="hlDividents" NavigateUrl='<%# String.Format("~/Attendance/AccountEmployeeAttendance.aspx?EmployeeID={0}&EmployeeName={1}", Eval("EmployeeId"), Eval("FirstName") + " " + Eval("LastName")) %>' Text="”Ã· «·Õ÷Ê—" meta:resourcekey="hlDividentsResource1"></asp:HyperLink>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="20%" />
        </asp:TemplateField>
        <%--<asp:TemplateField>
         <HeaderTemplate>
            <asp:CheckBox 
               ID="chkCheckAll" runat="server" Width="15px" />
         </HeaderTemplate>
         <ItemTemplate>
            <asp:CheckBox 
               ID="chkSelect" runat="server" Width="15px" />
         </ItemTemplate>
         <HeaderStyle 
            HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
         <ItemStyle 
            HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
         </asp:TemplateField>--%>
    </Columns>
</x:GridView>
<%--<asp:TemplateField>
   <HeaderTemplate>
      <asp:CheckBox 
         ID="chkCheckAll" runat="server" Width="15px" />
   </HeaderTemplate>
   <ItemTemplate>
      <asp:CheckBox 
         ID="chkSelect" runat="server" Width="15px" />
   </ItemTemplate>
   <HeaderStyle 
      HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
   <ItemStyle 
      HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
   </asp:TemplateField>--%>
<table style="width: 98%">
    <tr>
        <td align="left" height="2" rowspan="1">
            <div style="margin: 5px; padding-top: 5px;">
                <asp:Button ID="btnAddEmployee" runat="server"
                    Text="≈÷«›… „ÊŸ›" Width="75px"
                    UseSubmitBehavior="False" meta:resourcekey="btnAddEmployeeResource1" />
                <asp:Button ID="btnDeleteSelectedItem" runat="server"
                    OnClick="btnDeleteSelectedItem_Click"
                    Text="Õ–› «·„Õœœ" Visible="False"
                    Width="90px" meta:resourcekey="btnDeleteSelectedItemResource1" />
            </div>
        </td>
    </tr>
</table>

<asp:FormView ID="FormView1" runat="server" Width="100%" SkinID="formviewSkinEmployee" DataKeyNames="EmployeeId" DefaultMode="Insert" Style="margin-right: 0px" Visible="False" meta:resourcekey="FormView1Resource1">
    <EditItemTemplate>
        <table border="0" id="Table3" language="javascript" cellpadding="0" cellspacing="2" style="width: 98%">
            <tbody>
                <tr>
                    <th class="caption" colspan="6" style="text-align: center;">
                        <asp:Literal ID="Literal1" runat="server"
                            Text="„⁄·Ê„«  «·„ÊŸ›" meta:resourcekey="Literal1Resource1"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal2" runat="server"
                            Text="«·„⁄·Ê„«  «·‘Œ’Ì…" meta:resourcekey="Literal2Resource1"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal10" runat="server"
                            Text="—ﬁ„ «·Õ«”» «·«·Ì" meta:resourcekey="Literal10Resource1"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccountEmployeeId" runat="server" MaxLength="50"
                            ReadOnly="True" Text='<%# Bind("EmployeeId") %>'
                            Width="128px" meta:resourcekey="txtAccountEmployeeIdResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal12" runat="server"
                            Text="—ﬁ„ «·ÂÊÌ…" meta:resourcekey="Literal12Resource1"></asp:Literal>

                    </td>
                    <td>
                        <asp:TextBox ID="txtGovId" runat="server" MaxLength="10"
                            Text='<%# Bind("GovId") %>'
                            Width="128px" meta:resourcekey="txtGovIdResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqGovId" runat="server"
                            ControlToValidate="txtGovId" CssClass="ErrorMessage" Display="Dynamic"
                            ErrorMessage="*" ValidationGroup="Edit" Width="1px" meta:resourcekey="rqGovIdResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="rexNumber"
                            ControlToValidate="txtGovId"
                            ValidationExpression="^[0-9]{10}$"
                            ErrorMessage="10 Œ«‰«   -  √—ﬁ«„ ›ﬁÿ!" meta:resourcekey="rexNumberResource1" />

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal5" runat="server"
                            Text="«·≈”„ «·√Ê·" meta:resourcekey="Literal5Resource1"></asp:Literal>

                    </td>
                    <td>
                        <asp:TextBox ID="FirstNameTextBox" runat="server"
                            Text='<%# Bind("FirstName") %>' ValidationGroup="Edit" Width="128px" meta:resourcekey="FirstNameTextBoxResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ControlToValidate="FirstNameTextBox" CssClass="ErrorMessage" Display="Dynamic"
                            ErrorMessage="*" ValidationGroup="Edit" Width="1px" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                    </td>
                    <td>

                        <asp:Literal ID="Literal6" runat="server"
                            Text="«·√”„ «·√Ê”ÿ:" meta:resourcekey="Literal6Resource1"></asp:Literal>

                    </td>
                    <td>
                        <asp:TextBox ID="MiddleNameTextBox" runat="server"
                            Text='<%# Bind("MiddleName") %>' ValidationGroup="Edit" Width="128px" meta:resourcekey="MiddleNameTextBoxResource1"></asp:TextBox>
                    </td>
                    <td>

                        <asp:Literal ID="Literal7" runat="server"
                            Text="«·≈”„ «·√ŒÌ—" meta:resourcekey="Literal7Resource1"></asp:Literal>

                    </td>
                    <td>
                        <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>'
                            ValidationGroup="Edit" Width="128px" meta:resourcekey="LastNameTextBoxResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ControlToValidate="LastNameTextBox" CssClass="ErrorMessage" Display="Dynamic"
                            ErrorMessage="*" ValidationGroup="Edit" Width="1px" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                     <td>

                        <asp:Literal ID="Literal32" runat="server"
                            Text="«·≈”„ »«·≈‰Ã·Ì“Ì" meta:resourcekey="Literal32Resource1"></asp:Literal>

                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="EmployeeNameEnglishTextBox" runat="server" Text='<%# Bind("EmployeeNameEnglish") %>' Style="width:300px"
                            ValidationGroup="Edit" Width="128px" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                            ControlToValidate="EmployeeNameEnglishTextBox" CssClass="ErrorMessage" Display="Dynamic"
                            ErrorMessage="*" ValidationGroup="Edit" Width="1px" ></asp:RequiredFieldValidator>
                    </t>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal17" runat="server"
                            Text="«·Ã‰”:" meta:resourcekey="Literal17Resource1"></asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSex" runat="server"
                            SelectedValue='<%# Bind("Sex") %>'
                            ValidationGroup="Edit" Width="128px" meta:resourcekey="ddlSexResource1">
                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource1" Selected="True">–ﬂ—</asp:ListItem>
                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource2">«‰ÀÏ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Literal ID="Literal13" runat="server"
                            Text="«·Ã‰”Ì…:" meta:resourcekey="Literal13Resource1"></asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCountryId" runat="server"
                            DataSourceID="dsSystemCountryObject" DataTextField="NationalityName"
                            DataValueField="NationalityId" SelectedValue='<%# Bind("NationalityId") %>'
                            ValidationGroup="Edit" Width="128px" meta:resourcekey="ddlCountryIdResource1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal19" runat="server"
                            Text="«·»—Ìœ «·«·Ìﬂ —Ê‰Ì:" meta:resourcekey="Literal19Resource1"></asp:Literal>

                    </td>
                    <td>
                        <asp:TextBox ID="EMailAddressTextBox" runat="server"
                            OnTextChanged="EMailAddressTextBox_TextChanged"
                            Text='<%# Bind("EMailAddress") %>' ValidationGroup="Edit" Width="128px" meta:resourcekey="EMailAddressTextBoxResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="EMailAddressTextBox" CssClass="ErrorMessage"
                            Display="Dynamic" ErrorMessage="Incorrect EMail Address" Font-Bold="True"
                            Font-Names="Verdana" Font-Size="X-Small"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="Edit" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                            ControlToValidate="EMailAddressTextBox" CssClass="ErrorMessage"
                            Display="Dynamic" ErrorMessage="*" ValidationGroup="Edit" Width="8px" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblExceptionText" runat="server" CssClass="ErrorMessage"
                            Text="«·√Ì„Ì· „ÊÃÊœ:" Visible="False" meta:resourcekey="lblExceptionTextResource1"></asp:Label>
                    </td>
                    <td>

                        <asp:Literal ID="Literal27" runat="server"
                            Text="—ﬁ„ «· ·›Ê‰" meta:resourcekey="Literal27Resource1"></asp:Literal>

                    </td>
                    <td>
                        <asp:TextBox ID="txtMobileNo" runat="server"
                            Text='<%# Bind("MobileNo") %>' ValidationGroup="Edit" Width="128px" meta:resourcekey="txtMobileNoResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal18" runat="server"
                            Text="„⁄·Ê„«   ”ÃÌ· «·œŒÊ·" meta:resourcekey="Literal18Resource1"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal14" runat="server"
                            Text="«”„ «·„” Œœ„:" meta:resourcekey="Literal14Resource1"></asp:Literal>

                    </td>
                    <td>
                        <asp:TextBox ID="UsernameTextBox" runat="server"
                            ValidationGroup="Insert" Width="128px"
                            Text='<%# Bind("Username") %>' meta:resourcekey="UsernameTextBoxResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                            ControlToValidate="UsernameTextBox" CssClass="ErrorMessage" Display="Dynamic"
                            ErrorMessage="*" ValidationGroup="Edit" Width="1px" meta:resourcekey="RequiredFieldValidator10Resource1"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnGetUserData" runat="server" Visible="False" OnClick="btnGetUserData_Click"
                            Text="⁄—÷ »Ì«‰«  «·„” Œœ„ " Width="110px" meta:resourcekey="btnGetUserDataResource1" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server"
                            CssClass="ErrorMessage" Display="Dynamic"
                            ErrorMessage="Specified user not exist"
                            OnServerValidate="CustomValidator1_ServerValidate" meta:resourcekey="CustomValidator1Resource1"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal20" runat="server"
                            Text="ﬂ·„… «·„—Ê—:" meta:resourcekey="Literal20Resource1"></asp:Literal>

                    </td>
                    <td>
                        <asp:TextBox ID="PasswordTextBox" runat="server" Text='<%# Bind("Password") %>'
                            TextMode="Password" ValidationGroup="Edit" Width="128px" meta:resourcekey="PasswordTextBoxResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                            ControlToValidate="PasswordTextBox" Display="Dynamic"
                            SkinID="PasswordValidator"
                            ValidationExpression="(?=^.{8,}$)(?=.*\d)(?=.*\W+)(?![.\n]).*$" Enabled="False" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                    </td>
                    <td valign="middle">

                        <asp:Literal ID="Literal21" runat="server"
                            Text=" √ﬂÌœ ﬂ·„… «·„—Ê—:" meta:resourcekey="Literal21Resource1"></asp:Literal>

                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="VerifyPasswordTextbox" runat="server" TextMode="Password"
                            ValidationGroup="Edit" Width="128px" meta:resourcekey="VerifyPasswordTextboxResource1"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server"
                            ControlToCompare="VerifyPasswordTextbox" ControlToValidate="PasswordTextBox"
                            CssClass="ErrorMessage" Display="Dynamic" ErrorMessage="(Mismatch)"
                            ValidationGroup="Edit" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal22" runat="server"
                            Text="’·«ÕÌ… «·„” Œœ„:" meta:resourcekey="Literal22Resource1" />
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlAccountRoleId" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsAccountRoleObject"
                            Enabled='<%# If(Session("AccountRoleId").ToString().Equals("1"), True, False) %>'
                            DataTextField="RoleName"
                            DataValueField="RoleId"
                            SelectedValue='<%# Bind("RoleId") %>' meta:resourcekey="ddlAccountRoleIdResource1">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource3" Selected="True">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator2" Display="Dynamic"
                            ControlToValidate="ddlAccountRoleId"
                            runat="server" Text="*"
                            ErrorMessage="*"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Edit" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                        <telerik:RadWindow ID="RadWindow1" Width="700px" Height="600px" runat="server" NavigateUrl="../DepartmentScope.aspx" meta:resourcekey="RadWindow1Resource1"></telerik:RadWindow>
                        <asp:Button ID="Button1" Text="ÕœÊœ ’·«ÕÌ… «·„” Œœ„" OnClientClick="showDialog(); return false;" runat="server" meta:resourcekey="Button1Resource1" />
                        <telerik:RadWindow ID="RadWindow2" Width="550px" Height="600px" runat="server" NavigateUrl="../ManagerScope.aspx" meta:resourcekey="RadWindow2Resource1"></telerik:RadWindow>
                        <asp:Button ID="btnManagerScope" runat="server" Text="«·„ÊŸ›Ì‰  Õ  «œ«— Â" OnClientClick="showDialog1(); return false;" meta:resourcekey="btnManagerScopeResource1" />
                    </td>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal15" runat="server"
                            Text="„⁄·Ê„«  «·ÊŸÌ›…" meta:resourcekey="Literal15Resource1"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal8" runat="server"
                            Text="«·—ﬁ„ «·ÊŸÌ›Ì" meta:resourcekey="Literal8Resource1"></asp:Literal>

                    </td>
                    <td>
                        <asp:TextBox ID="txtEmployeeNo" runat="server" MaxLength="50"
                            Text='<%# Bind("EmployeeNo") %>'
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="txtEmployeeNoResource1"></asp:TextBox>
                    </td>
                    <td>

                        <asp:Literal
                            ID="Literal76" runat="server"
                            Text="«·ÊŸÌ›…:" meta:resourcekey="Literal76Resource1" />
                    </td>
                    <td>
                        <asp:TextBox ID="JobTitleTextBox" runat="server"
                            Text='<%# Bind("JobTitle") %>' Width="128px" meta:resourcekey="JobTitleTextBoxResource1"></asp:TextBox></td>
                    <td>
                        <asp:Literal
                            ID="Literal74" runat="server"
                            Text=" «—ÌŒ ≈⁄ „«œ «·Õ÷Ê—:" meta:resourcekey="Literal74Resource1" /></td>
                    <td>
                        <uc1:MyDate runat="server" ID="HiredDateCalendarPopup" SelectedDate='<%# Bind("HireDate") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal28" runat="server"
                            Text="‰Ê⁄ «·⁄ﬁœ:" meta:resourcekey="Literal28Resource1" /></td>
                    <td>
                        <asp:DropDownList ID="ddlContractType" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsContractTypes"
                            DataTextField="ContractTypeName"
                            DataValueField="ContractTypeId"
                            SelectedValue='<%# Bind("ContractType") %>' meta:resourcekey="ddlContractTypeResource1">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource4" Selected="True">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator3" Display="Dynamic"
                            ControlToValidate="ddlContractType"
                            runat="server" Text="*"
                            ErrorMessage="≈Œ — ‰Ê⁄ «·⁄ﬁœ"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Edit" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator></td>
                    <td>
                        <asp:Literal ID="Literal29" runat="server"
                            Text="ÃÂ… «· ÊŸÌ›:" meta:resourcekey="Literal29Resource1" /></td>
                    <td>
                        <asp:DropDownList ID="ddlEmployer" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsEmployers"
                            DataTextField="EmployerName"
                            DataValueField="EmployerId"
                            SelectedValue='<%# Bind("Employer") %>' meta:resourcekey="ddlEmployerResource1">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource5" Selected="True">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator6" Display="Dynamic"
                            ControlToValidate="ddlAccountLocationId"
                            runat="server" Text="*"
                            ErrorMessage="≈Œ — ÃÂ… «· ÊŸÌ›"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Edit" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal
                            ID="Literal25" runat="server" Text="«·„œÌ— «·„»«‘—:" meta:resourcekey="Literal25Resource1" /></td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlEmployeeManagerId" runat="server" Style="margin-right: 0px"
                            DataTextField="EmployeeName"
                            DataValueField="EmployeeId" meta:resourcekey="ddlEmployeeManagerIdResource1">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource6">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator1" Display="Dynamic"
                            ControlToValidate="ddlEmployeeManagerId"
                            runat="server" Text="*"
                            CssClass="ErrorMessage"
                            ErrorMessage="≈Œ — «·„œÌ— «·„»«‘—"
                            ForeColor="Red" ValidationGroup="Edit" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator><asp:Literal ID="Literal11" runat="server" Text="—ﬁ„ «·»’„… ··„œÌ—:" meta:resourcekey="Literal11Resource1" /><asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" meta:resourcekey="txtEmployeeCodeResource1" /><asp:Button ID="Button3" runat="server" Text="»ÕÀ" CausesValidation="False" OnClick="FindByEmployeeCode" meta:resourcekey="Button3Resource1" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal23"
                            runat="server" Text="«·ﬁ”„" meta:resourcekey="Literal23Resource1" /></td>
                    <td colspan="3">
                        <uc1:ctlDepartmentTree runat="server" ID="ctlDepartmentTree1" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal24" runat="server"
                            Text="„Êﬁ⁄ «·⁄„·:" meta:resourcekey="Literal24Resource1" /></td>
                    <td>
                        <asp:DropDownList ID="ddlAccountLocationId" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsAccountLocation"
                            DataTextField="LocationName"
                            DataValueField="LocationId"
                            SelectedValue='<%# Bind("LocationId") %>' meta:resourcekey="ddlAccountLocationIdResource1">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource7" Selected="True">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="rfvDDL_Product" Display="Dynamic"
                            ControlToValidate="ddlAccountLocationId"
                            runat="server" Text="*"
                            ErrorMessage="≈Œ — „Êﬁ⁄ «·⁄„·"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Edit" meta:resourcekey="rfvDDL_ProductResource1"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal16" runat="server"
                            Text="„⁄·Ê„«  «·Õ÷Ê— Ê«·≈‰’—«›" meta:resourcekey="Literal16Resource1"></asp:Literal></th>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal3" runat="server"
                            Text="—ﬁ„ «·»’„…" meta:resourcekey="Literal3Resource1"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="EmployeeCodeTextBox" runat="server" MaxLength="50"
                            Text='<%# Bind("BadgeNo") %>'
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="EmployeeCodeTextBoxResource1"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                ControlToValidate="EmployeeCodeTextBox" CssClass="ErrorMessage" Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="Edit" Width="1px" meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator></td>
                    <td valign="middle">
                        <asp:Literal ID="Literal9"
                            runat="server" Text="ÃœÊ· «·œÊ«„ «·„‰ÿ»ﬁ ⁄·ÌÂ" meta:resourcekey="Literal9Resource1" /></td>
                    <td>
                        <asp:DropDownList ID="ddlWorkSchedules" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsWorkSchedules"
                            DataTextField="WorkScheduleName"
                            DataValueField="WorkScheduleId" meta:resourcekey="ddlWorkSchedulesResource1">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource8">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator9" Display="Dynamic"
                            ControlToValidate="ddlWorkSchedules"
                            runat="server" Text="*"
                            ErrorMessage="≈Œ — ÃœÊ· «·œÊ«„"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Edit" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>&nbsp; </td>
                    <td>
                        <asp:Literal
                            ID="Literal26" runat="server"
                            Text="‰‘ÿ:" meta:resourcekey="Literal26Resource1" /><asp:CheckBox ID="chkIsDisabled" runat="server"
                                Checked='<%# Bind("IsActive") %>'
                                Enabled='<%# IIf((Eval("EmployeeId") = DBUtilities.GetSessionAccountEmployeeId), False, True) %>' meta:resourcekey="chkIsDisabledResource1" /></td>
                    <td>
                        <asp:Literal ID="Literal30" runat="server" Text="ÌÕ”» ·Â Œ«—Ã «·œÊ«„:" meta:resourcekey="Literal30Resource1" /><asp:CheckBox ID="chkIsAllowOvertime" runat="server" Checked='<%# Bind("IsAllowOvertime") %>' meta:resourcekey="chkIsAllowOvertimeResource1" /></td>
                </td>
                     <td colspan="2">
                        <asp:Literal ID="Literal31" runat="server" Text="„⁄›Ì „‰ «· Õ÷Ì—:"  meta:resourcekey="Literal31Resource1"  Visible='<%# If(Session("AccountRoleId").ToString().Equals("1"), True, False) %>' />
                        <asp:CheckBox ID="chkIsExempt" runat="server" Checked='<%# Bind("IsExempt")%>'  Visible='<%# If(Session("AccountRoleId").ToString().Equals("1"), True, False) %>'/>
                    </td>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal36" runat="server"
                            Text="ŒÌ«—«  „ ﬁœ„…" meta:resourcekey="Literal36Resource1" /></th>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Literal ID="Literal37" runat="server"
                            Text="⁄‰Ê«‰ «·ÃÂ«“ «·„”„ÊÕ ·Â »«·œŒÊ· „‰Â" meta:resourcekey="Literal37Resource1" /><asp:TextBox ID="AccessAllowedFromIPTextBox" runat="server"
                                Text='<%# Bind("AllowedAccessFromIP") %>' Width="128px" meta:resourcekey="AccessAllowedFromIPTextBoxResource1"></asp:TextBox></td>
                    <td colspan="2">
                        <asp:Literal ID="Literal4" runat="server"
                            Text="ÌÃ»  €ÌÌ— ﬂ·„… «·„—Ê— ⁄‰œ  ”ÃÌ· «·œŒÊ·ø" meta:resourcekey="Literal4Resource1" /><asp:CheckBox ID="chkForcePasswordChange"
                                runat="server" Checked='<%# Bind("IsForcePasswordChange") %>' meta:resourcekey="chkForcePasswordChangeResource1" /></td>
                    <td>
                    <td style="width: 30%"></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="padding-bottom: 5px;">
                        <asp:Button ID="Button2"
                            runat="server" OnClick="NewEmployee"
                            Text="ÃœÌœ" CausesValidation="False"
                            Width="68px" meta:resourcekey="Button2Resource1" /><asp:Button ID="Update" runat="server" OnClick="DataUpdate"
                                Text=" ÕœÌÀ"
                                ValidationGroup="Edit" Width="68px" meta:resourcekey="UpdateResource1" /><asp:Button ID="Cancel" runat="server"
                                    OnClientClick="ReloadSamepage(); return false;"
                                    CausesValidation="False"
                                    Text="≈·€«¡ «·√„—"
                                    Width="68px" meta:resourcekey="CancelResource1" /></td>
                </tr>

            </tbody>
        </table>
    </EditItemTemplate>
    <InsertItemTemplate>
        <table border="0" id="Table3" language="javascript" cellpadding="0" cellspacing="2" style="width: 98%">
            <tbody>
                <tr>
                    <th class="caption" colspan="6" style="text-align: center;">
                        <asp:Literal ID="Literal1" runat="server"
                            Text="„⁄·Ê„«  «·„ÊŸ›" meta:resourcekey="Literal1Resource2"></asp:Literal></th>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal2" runat="server"
                            Text="«·„⁄·Ê„«  «·‘Œ’Ì…" meta:resourcekey="Literal2Resource2"></asp:Literal></th>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource2"></asp:Label></td>
                </tr>
                <tr style="display:none">
                    <td>
                        <asp:Literal ID="Literal10" runat="server"
                            Text="—ﬁ„ «·Õ«”» «·«·Ì" meta:resourcekey="Literal10Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccountEmployeeId" runat="server" MaxLength="50"
                            ReadOnly="True"
                            Width="128px" meta:resourcekey="txtAccountEmployeeIdResource2"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal12" runat="server"
                            Text="—ﬁ„ «·ÂÊÌ…" meta:resourcekey="Literal12Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGovId" runat="server" MaxLength="10"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="txtGovIdResource2"></asp:TextBox><asp:RequiredFieldValidator ID="rqGovId" runat="server"
                                ControlToValidate="txtGovId" CssClass="ErrorMessage" Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="Insert" Width="1px" meta:resourcekey="rqGovIdResource2"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="rexNumber"
                                    ControlToValidate="txtGovId"
                                    ValidationExpression="^[0-9]{10}$"
                                    ErrorMessage="10 Œ«‰«   -  √—ﬁ«„ ›ﬁÿ!" meta:resourcekey="rexNumberResource2" />

                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal5" runat="server"
                            Text="«·≈”„ «·√Ê·" meta:resourcekey="Literal5Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="FirstNameTextBox" runat="server"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="FirstNameTextBoxResource2"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="FirstNameTextBox" CssClass="ErrorMessage" Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="Insert" Width="1px" meta:resourcekey="RequiredFieldValidator4Resource2"></asp:RequiredFieldValidator></td>
                    <td>

                        <asp:Literal ID="Literal6" runat="server"
                            Text="«·√”„ «·√Ê”ÿ:" meta:resourcekey="Literal6Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="MiddleNameTextBox" runat="server"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="MiddleNameTextBoxResource2"></asp:TextBox></td>
                    <td>

                        <asp:Literal ID="Literal7" runat="server"
                            Text="«·≈”„ «·√ŒÌ—" meta:resourcekey="Literal7Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="LastNameTextBox" runat="server"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="LastNameTextBoxResource2"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="LastNameTextBox" CssClass="ErrorMessage" Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="Insert" Width="1px" meta:resourcekey="RequiredFieldValidator5Resource2"></asp:RequiredFieldValidator></td>
                </tr>

                  <tr>
                     <td>

                        <asp:Literal ID="Literal32" runat="server"
                            Text="«·≈”„ »«·≈‰Ã·Ì“Ì" meta:resourcekey="Literal32Resource1"></asp:Literal>

                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="EmployeeNameEnglishTextBox" runat="server" Style="width:300px"
                            ValidationGroup="Insert" Width="128px" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                            ControlToValidate="EmployeeNameEnglishTextBox" CssClass="ErrorMessage" Display="Dynamic"
                            ErrorMessage="*" ValidationGroup="Insert" Width="1px" ></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Literal ID="Literal17" runat="server"
                            Text="«·Ã‰”:" meta:resourcekey="Literal17Resource2"></asp:Literal></td>
                    <td>
                        <asp:DropDownList ID="ddlSex" runat="server"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="ddlSexResource2">
                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource9">–ﬂ—</asp:ListItem>
                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource10">«‰ÀÏ</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <asp:Literal ID="Literal13" runat="server"
                            Text="«·Ã‰”Ì…:" meta:resourcekey="Literal13Resource2"></asp:Literal></td>
                    <td>
                        <asp:DropDownList ID="ddlCountryId" runat="server"
                            DataSourceID="dsSystemCountryObject" DataTextField="NationalityName"
                            DataValueField="NationalityId"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="ddlCountryIdResource2">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal19" runat="server"
                            Text="«·»—Ìœ «·«·Ìﬂ —Ê‰Ì:" meta:resourcekey="Literal19Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="EMailAddressTextBox" runat="server"
                            OnTextChanged="EMailAddressTextBox_TextChanged"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="EMailAddressTextBoxResource2"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="EMailAddressTextBox" CssClass="ErrorMessage"
                                Display="Dynamic" ErrorMessage="Incorrect EMail Address" Font-Bold="True"
                                Font-Names="Verdana" Font-Size="X-Small"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="Insert" meta:resourcekey="RegularExpressionValidator1Resource2"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                    ControlToValidate="EMailAddressTextBox" CssClass="ErrorMessage"
                                    Display="Dynamic" ErrorMessage="*" ValidationGroup="Insert" Width="8px" meta:resourcekey="RequiredFieldValidator7Resource2"></asp:RequiredFieldValidator><asp:Label ID="lblExceptionText" runat="server" CssClass="ErrorMessage"
                                        Text="«·√Ì„Ì· „ÊÃÊœ:" Visible="False" meta:resourcekey="lblExceptionTextResource2"></asp:Label></td>
                    <td>

                        <asp:Literal ID="Literal27" runat="server"
                            Text="—ﬁ„ «· ·›Ê‰" meta:resourcekey="Literal27Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobileNo" runat="server"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="txtMobileNoResource2"></asp:TextBox></td>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal18" runat="server"
                            Text="„⁄·Ê„«   ”ÃÌ· «·œŒÊ·" meta:resourcekey="Literal18Resource2"></asp:Literal></th>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal14" runat="server"
                            Text="«”„ «·„” Œœ„:" meta:resourcekey="Literal14Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="UsernameTextBox" runat="server"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="UsernameTextBoxResource2"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                ControlToValidate="UsernameTextBox" CssClass="ErrorMessage" Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="Insert" Width="1px" meta:resourcekey="RequiredFieldValidator10Resource2"></asp:RequiredFieldValidator><asp:Button ID="btnGetUserData" runat="server" Visible="False" OnClick="btnGetUserData_Click"
                                    Text="⁄—÷ »Ì«‰«  «·„” Œœ„ " Width="110px" meta:resourcekey="btnGetUserDataResource2" /><asp:CustomValidator ID="CustomValidator1" runat="server"
                                        CssClass="ErrorMessage" Display="Dynamic"
                                        ErrorMessage="Specified user not exist"
                                        OnServerValidate="CustomValidator1_ServerValidate" meta:resourcekey="CustomValidator1Resource2"></asp:CustomValidator></td>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal20" runat="server"
                            Text="ﬂ·„… «·„—Ê—:" meta:resourcekey="Literal20Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="PasswordTextBox" runat="server"
                            TextMode="Password" ValidationGroup="Insert" Width="128px" meta:resourcekey="PasswordTextBoxResource2"></asp:TextBox>
                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="PasswordTextBox" Display="Dynamic"
                                SkinID="PasswordValidator"
                                ValidationExpression="(?=^.{8,}$)(?=.*\d)(?=.*\W+)(?![.\n]).*$" Enabled="False" meta:resourcekey="RegularExpressionValidator2Resource2"></asp:RegularExpressionValidator>--%>

                    </td>
                    <td
                        valign="middle">

                        <asp:Literal ID="Literal21" runat="server"
                            Text=" √ﬂÌœ ﬂ·„… «·„—Ê—:" meta:resourcekey="Literal21Resource2"></asp:Literal>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="VerifyPasswordTextbox" runat="server" TextMode="Password"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="VerifyPasswordTextboxResource2"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server"
                                ControlToCompare="VerifyPasswordTextbox" ControlToValidate="PasswordTextBox"
                                CssClass="ErrorMessage" Display="Dynamic" ErrorMessage="(Mismatch)"
                                ValidationGroup="Insert" meta:resourcekey="CompareValidator1Resource2"></asp:CompareValidator></td>
                </tr>
                <tr>
                    <td>
                     <asp:Literal ID="Literal22" runat="server"
                            Text="’·«ÕÌ… «·„” Œœ„:" meta:resourcekey="Literal22Resource2" /></td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlAccountRoleId" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsAccountRoleObject"
                            Enabled='<%# If(Session("AccountRoleId").ToString().Equals("1"), True, False) %>'
                            DataTextField="RoleName"
                            DataValueField="RoleId" meta:resourcekey="ddlAccountRoleIdResource2">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource11">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator2" Display="Dynamic"
                            ControlToValidate="ddlAccountRoleId"
                            runat="server" Text="*"
                            ErrorMessage="≈Œ — «·œÊ—"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Insert" meta:resourcekey="RequiredFieldValidator2Resource2"></asp:RequiredFieldValidator><telerik:RadWindow ID="RadWindow1" Width="550px" Height="600px" runat="server" NavigateUrl="../DepartmentScope.aspx" meta:resourcekey="RadWindow1Resource2"></telerik:RadWindow>
                        <asp:Button ID="btnScope" Visible="False" Text="ÕœÊœ ’·«ÕÌ… «·„” Œœ„" OnClientClick="showDialog(); return false;" runat="server" meta:resourcekey="btnScopeResource1" /><telerik:RadWindow ID="RadWindow2" Width="700px" Height="600px" runat="server" NavigateUrl="../ManagerScope.aspx" meta:resourcekey="RadWindow2Resource2"></telerik:RadWindow>
                        <asp:Button ID="btnManagerScope" Visible="False" runat="server" Text="«·„ÊŸ›Ì‰  Õ  «œ«— Â" OnClientClick="showDialog1(); return false;" meta:resourcekey="btnManagerScopeResource2" /></td>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal15" runat="server"
                            Text="„⁄·Ê„«  «·ÊŸÌ›…" meta:resourcekey="Literal15Resource2"></asp:Literal></th>
                </tr>
                <tr>
                    <td>

                        <asp:Literal ID="Literal8" runat="server"
                            Text="«·—ﬁ„ «·ÊŸÌ›Ì" meta:resourcekey="Literal8Resource2"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmployeeNo" runat="server" MaxLength="50"
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="txtEmployeeNoResource2"></asp:TextBox></td>
                    <td>

                        <asp:Literal
                            ID="Literal76" runat="server"
                            Text="«·ÊŸÌ›…:" meta:resourcekey="Literal76Resource2" />
                    </td>
                    <td>
                        <asp:TextBox ID="JobTitleTextBox" runat="server"
                            Width="128px" meta:resourcekey="JobTitleTextBoxResource2"></asp:TextBox></td>
                    <td>
                        <asp:Literal
                            ID="Literal74" runat="server"
                            Text=" «—ÌŒ ≈⁄ „«œ «·Õ÷Ê—:" meta:resourcekey="Literal74Resource2" />
                    </td>
                    <td>
                        <uc1:MyDate runat="server" ID="HiredDateCalendarPopup" SelectedDate='<%# Now.Date%>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal28" runat="server"
                            Text="‰Ê⁄ «·⁄ﬁœ:" meta:resourcekey="Literal28Resource2" /></td>
                    <td>
                        <asp:DropDownList ID="ddlContractType" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsContractTypes"
                            DataTextField="ContractTypeName"
                            DataValueField="ContractTypeId" meta:resourcekey="ddlContractTypeResource2">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource12">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator3" Display="Dynamic"
                            ControlToValidate="ddlContractType"
                            runat="server" Text="*"
                            ErrorMessage="≈Œ — ‰Ê⁄ «·⁄ﬁœ"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Insert" meta:resourcekey="RequiredFieldValidator3Resource2"></asp:RequiredFieldValidator></td>
                    <td>
                        <asp:Literal ID="Literal29" runat="server"
                            Text="ÃÂ… «· ÊŸÌ›:" meta:resourcekey="Literal29Resource2" /></td>
                    <td>
                        <asp:DropDownList ID="ddlEmployer" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsEmployers"
                            DataTextField="EmployerName"
                            DataValueField="EmployerId" meta:resourcekey="ddlEmployerResource2">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource13">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator6" Display="Dynamic"
                            ControlToValidate="ddlAccountLocationId"
                            runat="server" Text="*"
                            ErrorMessage="≈Œ — ÃÂ… «· ÊŸÌ›"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Insert" meta:resourcekey="RequiredFieldValidator6Resource2"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal
                            ID="Literal25" runat="server" Text="«·„œÌ— «·„»«‘—:" meta:resourcekey="Literal25Resource2" /></td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlEmployeeManagerId" runat="server" Style="margin-right: 0px"
                            DataTextField="EmployeeName"
                            DataValueField="EmployeeId" meta:resourcekey="ddlEmployeeManagerIdResource2">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource14">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator1" Display="Dynamic"
                            ControlToValidate="ddlEmployeeManagerId"
                            runat="server" Text="*"
                            ErrorMessage="*"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Insert" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator><asp:Literal ID="Literal11" runat="server" Text="—ﬁ„ «·»’„… ··„œÌ—:" meta:resourcekey="Literal11Resource2" /><asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" meta:resourcekey="txtEmployeeCodeResource2" /><asp:Button ID="Button3" runat="server" Text="»ÕÀ" CausesValidation="False" OnClick="FindByEmployeeCode" meta:resourcekey="Button3Resource2" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal23"
                            runat="server" Text="«·ﬁ”„" meta:resourcekey="Literal23Resource2" /></td>
                    <td colspan="2">
                        <uc1:ctlDepartmentTree runat="server" ID="ctlDepartmentTree1" IsRequired="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal24" runat="server"
                            Text="„Êﬁ⁄ «·⁄„·:" meta:resourcekey="Literal24Resource2" /></td>
                    <td>
                        <asp:DropDownList ID="ddlAccountLocationId" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsAccountLocation"
                            DataTextField="LocationName"
                            DataValueField="LocationId" meta:resourcekey="ddlAccountLocationIdResource2">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource15">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="rfvDDL_Product" Display="Dynamic"
                            ControlToValidate="ddlAccountLocationId"
                            runat="server" Text="*"
                            ErrorMessage="*"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Insert" meta:resourcekey="rfvDDL_ProductResource2"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal16" runat="server"
                            Text="„⁄·Ê„«  «·Õ÷Ê— Ê«·≈‰’—«›" meta:resourcekey="Literal16Resource2"></asp:Literal></th>
                </tr>
               <tr>
                    <td>

                        <asp:Literal ID="Literal3" runat="server"
                            Text="—ﬁ„ «·»’„…" meta:resourcekey="Literal3Resource1"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="EmployeeCodeTextBox" runat="server" MaxLength="50"
                            
                            ValidationGroup="Insert" Width="128px" meta:resourcekey="EmployeeCodeTextBoxResource1"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                ControlToValidate="EmployeeCodeTextBox" CssClass="ErrorMessage" Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="Insert" Width="1px" meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator></td>
                    <td valign="middle">
                        <asp:Literal ID="Literal9"
                            runat="server" Text="ÃœÊ· «·œÊ«„ «·„‰ÿ»ﬁ ⁄·ÌÂ" meta:resourcekey="Literal9Resource1" /></td>
                    <td>
                        <asp:DropDownList ID="ddlWorkSchedules" runat="server" Style="margin-right: 0px"
                            DataSourceID="dsWorkSchedules"
                            DataTextField="WorkScheduleName"
                            DataValueField="WorkScheduleId" meta:resourcekey="ddlWorkSchedulesResource1">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource8">....≈Œ —....</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                            ID="RequiredFieldValidator9" Display="Dynamic"
                            ControlToValidate="ddlWorkSchedules"
                            runat="server" Text="*"
                            ErrorMessage="≈Œ — ÃœÊ· «·œÊ«„"
                            CssClass="ErrorMessage"
                            ForeColor="Red" ValidationGroup="Insert" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <th class="FormViewSubHeader" colspan="6">
                        <asp:Literal ID="Literal36" runat="server"
                            Text="ŒÌ«—«  „ ﬁœ„…" meta:resourcekey="Literal36Resource2" /></th>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Literal ID="Literal37" runat="server"
                            Text="⁄‰Ê«‰ «·ÃÂ«“ «·„”„ÊÕ ·Â »«·œŒÊ· „‰Â" meta:resourcekey="Literal37Resource2" /><asp:TextBox ID="AccessAllowedFromIPTextBox" runat="server"
                                Width="128px" meta:resourcekey="AccessAllowedFromIPTextBoxResource2"></asp:TextBox></td>
                          <td colspan="2">
                        <asp:Literal ID="Literal4" runat="server"
                            Text="ÌÃ»  €ÌÌ— ﬂ·„… «·„—Ê— ⁄‰œ  ”ÃÌ· «·œŒÊ·ø" meta:resourcekey="Literal4Resource2" /><asp:CheckBox ID="chkForcePasswordChange"
                                runat="server" meta:resourcekey="chkForcePasswordChangeResource2" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="padding-bottom: 5px;">
                        <asp:Button ID="Button2"
                            runat="server" OnClick="NewEmployee"
                            Text="ÃœÌœ" CausesValidation="False"
                            Width="68px" meta:resourcekey="Button2Resource2" /><asp:Button ID="Update" runat="server" OnClick="DataAdd"
                                Text="Õ›Ÿ"
                                ValidationGroup="Insert" Width="68px" meta:resourcekey="UpdateResource2" /><asp:Button ID="Cancel" runat="server"
                                    OnClientClick="ReloadSamepage(); return false;"
                                    CausesValidation="False"
                                    Text="≈·€«¡ «·√„—"
                                    Width="68px" meta:resourcekey="CancelResource2" /><br />
                    </td>

                </tr>
            </tbody>
        </table>
    </InsertItemTemplate>
</asp:FormView>

<asp:ObjectDataSource ID="dsAccountEmployeeList" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetAllEmployeesDataset" TypeName="ATS.BO.Framework.BOEmployee">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="UserAccountId" SessionField="AccountEmployeeId" Type="Int32" />
        <asp:Parameter DefaultValue="1" Name="FilterOption" Type="Object" />
        <asp:Parameter DefaultValue="1" Name="SortOption" Type="Object" />
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="10000" Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="dsAccountRoleObject" runat="server" TypeName="ATS.BO.Framework.BORole1" SelectMethod="GetList" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="dsAccountLocation" runat="server" TypeName="ATS.BO.Framework.BOLocation" SelectMethod="GetList" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="dsSystemCountryObject" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.FrameWork.BONationality"></asp:ObjectDataSource>




<asp:ObjectDataSource ID="dsWorkSchedules" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOWorkSchedule">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="dsEmployers" runat="server" OldValuesParameterFormatString="original_{0}"
         SelectMethod="GetList" TypeName="ATS.BO.Framework.BOEmployer">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
      </asp:ObjectDataSource>

  <asp:ObjectDataSource ID="dsContractTypes" runat="server" OldValuesParameterFormatString="original_{0}"
         SelectMethod="GetList" TypeName="ATS.BO.Framework.BOContractType">
       <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
      </asp:ObjectDataSource>





<asp:ObjectDataSource ID="dsPosition" runat="server" TypeName="ATS.BO.Framework.BOPosition" SelectMethod="GetList" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:Parameter DefaultValue="1" Name="PositionTypeId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="dsPositionType" runat="server" TypeName="ATS.BO.Framework.BOPosition" SelectMethod="GetPositionTypesList" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
<script type="text/javascript">
    function showDialog() {

        var elems = $("div[id$='RadWindow1']");
        var wnd;


        for (var i = 0; i < elems.length; i++) {
            if (~elems[i].id.indexOf("Wrapper") > -1) {
                wnd = $find(elems[i].id);
                wnd.show();
            }
            //wnd = $find(elems[i].id);
            //wnd.show();
        }
    }

    function showDialog1() {

        var elems = $("div[id$='RadWindow2']");
        var wnd;


        for (var i = 0; i < elems.length; i++) {
            if (~elems[i].id.indexOf("Wrapper") > -1) {
                wnd = $find(elems[i].id);
                wnd.show();
            }
            //wnd = $find(elems[i].id);
            //wnd.show();
        }
    }


</script>
