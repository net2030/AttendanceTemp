<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountEmployeeProfile.ascx.vb" Inherits="Controls_ctlAccountEmployeeProfile" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%--      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>

<table class="xAdminOption" style="width: 450px">
    <tr>
        <th class="caption" colspan="4" style="height: 21px">
            <asp:Literal ID="Literal1" runat="server" Text="«·„·› «·‘Œ’Ì" meta:resourcekey="Literal1Resource1" />
        </th>
    </tr>
    <tr>
        <th class="FormViewSubHeader" colspan="4" style="height: 21px">
            <asp:Literal ID="Literal2" runat="server" Text="«·„⁄·Ê„«  «·‘Œ’Ì…" meta:resourcekey="Literal2Resource1" />
        </th>
    </tr>
    <%Dim LDAP As New ATS.BO.LDAPUtilitiesBLL%>
    <%If LDAP.IsAspNetActiveDirectoryMembershipProvider = True Then%>
    <tr>
        <td class="FormViewLiteralCell">
            <asp:Literal ID="Literal4" runat="server" Text="≈”„ «·„” Œœ„" meta:resourcekey="Literal4Resource1" />
        </td>
        <td>
            <asp:TextBox ID="txtUsername" runat="server" MaxLength="20" Text='<%# Bind("Username") %>' Width="170px" meta:resourcekey="txtUsernameResource1"></asp:TextBox>
        </td>
        <td class="FormViewLiteralCell"></td>
        <td></td>
    </tr>
    <%End If%>
    <tr>
        <td class="FormViewLiteralCell">

            <asp:Literal ID="Literal5" runat="server" Text="«·≈”„ «·√Ê·" meta:resourcekey="Literal5Resource1" />

        </td>
        <td>
            <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' MaxLength="75" Width="170px" ReadOnly="True" meta:resourcekey="FirstNameTextBoxResource1"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator4" runat="server" ControlToValidate="FirstNameTextBox"
                Display="Dynamic" ErrorMessage="*" Width="1px" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="FormViewLiteralCell">

            <asp:Literal ID="Literal7" runat="server" Text="«·≈”„ «·√Ê”ÿ" meta:resourcekey="Literal7Resource1" />

        </td>
        <td>
            <asp:TextBox ID="MiddleNameTextBox" runat="server" Text='<%# Bind("MiddleName") %>' MaxLength="20" Width="170px" ReadOnly="True" meta:resourcekey="MiddleNameTextBoxResource1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="FormViewLiteralCell">

            <asp:Literal ID="Literal6" runat="server" Text="«·≈”„ «·√ŒÌ—" meta:resourcekey="Literal6Resource1" />

        </td>
        <td>
            <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' MaxLength="75" Width="170px" ReadOnly="True" meta:resourcekey="LastNameTextBoxResource1"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator5" runat="server" ControlToValidate="LastNameTextBox"
                Display="Dynamic" ErrorMessage="*" Width="1px" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="FormViewLiteralCell">

            <asp:Literal ID="Literal8" runat="server" Text="«·»—Ìœ «·≈·Ìﬂ —Ê‰Ì" meta:resourcekey="Literal8Resource1" />

        </td>
        <td>
            <asp:TextBox ID="EMailAddressTextBox" runat="server" Text='<%# Bind("EMailAddress") %>' MaxLength="50" Width="170px" ReadOnly="True" meta:resourcekey="EMailAddressTextBoxResource1"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator7" runat="server" ControlToValidate="EMailAddressTextBox"
                Display="Dynamic" ErrorMessage="*" Width="8px" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EMailAddressTextBox"
                Display="Dynamic" ErrorMessage="Incorrect EMail Address" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
        </td>
    </tr>

    <tr>
        <td
            class="FormViewLiteralCell">Image:</td>
        <td>
            <asp:FileUpload ID="FileUpload2" runat="server" meta:resourcekey="FileUpload2Resource1" />
        </td>
        <%--  <td  class="FormViewLiteralCell" >≈ŸÂ«— ’Ê—… «·⁄—÷:</td>
            <td >
               <asp:CheckBox 
                  ID="chkIsShowEmployeePicture" runat="server" />
            </td>--%>
    </tr>
    <tr>
        <td>
            <br /> <br /> <br /> 
        </td>
    </tr>
    <%If LDAP.IsAspNetActiveDirectoryMembershipProvider <> True Then%>
 
    <tr>
        <th class="FormViewSubHeader" style="height: 21px" colspan="4">
            <asp:Literal ID="Literal19" runat="server" Text="„⁄·Ê„«   ”ÃÌ· «·œŒÊ·" meta:resourcekey="Literal19Resource1" />
        </th>
    </tr>
     <tr>
        <td>
           
        </td>
    </tr>
       <tr style="display:none">
        <td class="FormViewLiteralCell">
            <asp:Literal ID="Literal3" runat="server" Text="≈”„ «·„” Œœ„" meta:resourcekey="Literal4Resource1" />
        </td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" MaxLength="20" Text='<%# Bind("Username")%>' Width="170px" meta:resourcekey="txtUsernameResource1"></asp:TextBox>
        </td>
        <td class="FormViewLiteralCell"></td>
        <td></td>
    </tr>
    <tr>
        <td class="FormViewLiteralCell">

            <asp:Literal ID="Literal22" runat="server"
                Text="ﬂ·„… «·„—Ê— «·Õ«·Ì…" meta:resourcekey="Literal22Resource1" />

        </td>
        <td>
            <asp:TextBox
                ID="CurrentPasswordTextBox" runat="server" MaxLength="50" TextMode="Password"
                Width="170px" meta:resourcekey="CurrentPasswordTextBoxResource1"></asp:TextBox>
        </td>
        <td class="FormViewLiteralCell">&nbsp; 
        </td>
        <td>&nbsp; 
        </td>
    </tr>
    <tr>
        <td  class="FormViewLiteralCell">

            <asp:Literal ID="Literal20" runat="server"
                Text="ﬂ·„… «·„—Ê—" meta:resourcekey="Literal20Resource1" />

        </td>
        <td>
            <asp:TextBox ID="PasswordTextBox"
                runat="server" MaxLength="50" TextMode="Password" Width="170px" meta:resourcekey="PasswordTextBoxResource2"></asp:TextBox>
            <%--<asp:RegularExpressionValidator 
                  ID="RegularExpressionValidator2" runat="server" 
                  ControlToValidate="PasswordTextBox" Display="Dynamic" 
                  SkinID="PasswordValidator" 
                  ValidationExpression="(?=^.{8,}$)(?=.*\d)(?=.*\W+)(?![.\n]).*$"></asp:RegularExpressionValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="FormViewLiteralCell">

            <asp:Literal ID="Literal21" runat="server"
                Text=" √ﬂÌœ ﬂ·„… «·„—Ê—" meta:resourcekey="Literal21Resource1" />

        </td>
        <td>
            <asp:TextBox
                ID="VerifyPasswordTextbox" runat="server" MaxLength="50" TextMode="Password"
                Width="170px" meta:resourcekey="VerifyPasswordTextboxResource1"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1"
                runat="server" ControlToCompare="VerifyPasswordTextbox"
                ControlToValidate="PasswordTextBox" CssClass="ErrorMessage"
                ErrorMessage="(Mismatch)" Width="35px" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
        </td>
    </tr>
    <%End If%>

     <tr>
        <td>
            <br /> <br /> 
        </td>
    </tr>

    <tr>
        
        <td colspan="2">
            <asp:Button
                ID="Update" runat="server" OnClick="Update_Click"
                Text=" ÕœÌÀ " Width="72px" meta:resourcekey="UpdateResource1" />
            &nbsp; 
               <asp:Button ID="Cancel" runat="server" CommandName="Cancel" Text="≈·€«¡ «·«„—" Width="72px" meta:resourcekey="CancelResource1" />
        </td>
    </tr>
</table>
