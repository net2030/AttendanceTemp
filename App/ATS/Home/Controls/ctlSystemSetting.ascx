<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlSystemSetting.ascx.vb" Inherits="Home_Controls_ctlSystemSetting" %>
<br />
<br />
<table class="xFormView" width="100%" dir="ltr"><tr><td>
<table width="100%" class="xFormView" align ="center" valign ="middle" dir="ltr">
    <tr>
        <th class="caption" colspan="2" style="text-align:left;">
            System Setting Information</th>
    </tr>
    <tr>
        <th class="FormViewSubHeader" colspan="2" style="text-align:left;">
            Authentication Settings</th>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" colspan="1" style="width: 30%">
            Authentication Type :</td>
        <td align="left">
            <asp:RadioButton ID="DefaultAuthenticationRadioButton" runat="server" AutoPostBack="True"
                Checked="True" GroupName="Authentication" Text="Default Authentication" OnCheckedChanged="DefaultAuthenticationRadioButton_CheckedChanged" /><asp:RadioButton
                    ID="ActiveDirectoryAuthenticationRadioButton" runat="server" AutoPostBack="True"
                    GroupName="Authentication" Text="Active Directory Authentication" OnCheckedChanged="ActiveDirectoryAuthenticationRadioButton_CheckedChanged" />
                    <asp:RadioButton ID="RDOpenLDAP" runat="server" AutoPostBack="True"
                    GroupName="Authentication" Text="Open LDAP Authentication" /></td>
    </tr>
    <tr>
        <th class="FormViewSubHeader" colspan="2" style="text-align:left;">
            System Administration Settings</th>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%">
            System Admin Password :</td>
        <td align="left">
            <asp:TextBox ID="txtSystemSettingPassword" runat="server" Width="198px" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%">
            System Admin Password Confirm :</td>
        <td align="left">
            <asp:TextBox ID="txtSystemSettingPasswordConfirm" runat="server" Width="198px" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtSystemSettingPasswordConfirm"
                ControlToValidate="txtSystemSettingPassword" CssClass="ErrorMessage" ErrorMessage="(Mismatch)"></asp:CompareValidator></td>
    </tr>
    <tr>
        <th class="FormViewSubHeader" colspan="2" style="text-align:left;">
            SMTP Settings</th>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%">
            <SPAN 
                  class=reqasterisk>*</span> SMTP Server :
        </td>
        <td align="left">
            <asp:TextBox ID="txtSMTPServer" runat="server" Width="480px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSMTPServer"
                Display="Dynamic" ErrorMessage="SMTP Server Required" Width="150px"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%">
            SMTP Username :
        </td>
        <td align="left">
            <asp:TextBox ID="txtSMTPUsername" runat="server" Width="480px"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%">
            SMTP Password :
        </td>
        <td align="left">
            <asp:TextBox ID="txtSMTPPassword" runat="server" Width="480px"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%">
            SMTP Port Number :
        </td>
        <td align="left">
            <asp:TextBox ID="txtSMTPPortNumber" runat="server" Width="480px"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%">
            Use SSL:</td>
        <td align="left">
            <asp:CheckBox ID="chkEnableSSL" runat="server" /></td>
    </tr>
    <tr>
        <th class="FormViewSubHeader" colspan="2" style="text-align:left;">
            Other Settings</th>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%">
           <SPAN 
                  class=reqasterisk>*</span> Application URL :
        </td>
        <td align="left">
            <asp:TextBox ID="txtApplicationURL" runat="server" Width="480px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApplicationURL"
                Display="Dynamic" ErrorMessage="URL Required"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%">
            Encrypt connection string:
        </td>
        <td align="left">
            <asp:CheckBox ID="chkencrypt" runat="server" /></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%; height: 65px;" valign="top">
           <SPAN 
                  class=reqasterisk>*</span> Connection String :
        </td>
        <td align="left" style="height: 65px">
            <asp:TextBox ID="txtConnectionString" runat="server" Height="42px"
                Width="480px" Rows="2" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtConnectionString"
                Display="Dynamic" ErrorMessage="Connection String Required"></asp:RequiredFieldValidator></td>
    </tr>
    <%
        If ActiveDirectoryAuthenticationRadioButton.Checked = True Then
     %>   
    <tr>
        <th class="FormViewSubHeader" colspan="2">
            Active Directory Settings</td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%" valign="top">
            <asp:Label ID="lblADConnectionString" runat="server" Text="Active Directory Connection String :"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtADConnectionString" runat="server" Height="42px" Rows="2" TextMode="MultiLine"
                Width="480px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtADConnectionString"
                Display="Dynamic" ErrorMessage="Connection String Required"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%" valign="top">
            Secured:
        </td>
        <td align="left">
            <asp:CheckBox ID="chkSecured" runat="server" /></td>
    </tr>
    
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%" valign="top">
            Integrated Authentication:</td>
        <td align="left">
            <asp:CheckBox ID="chkIntegratedAuthentication" runat="server" /></td>
    </tr>
    
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%;" valign="top">
            <asp:Label ID="lblADDomainName" runat="server" Text="Active Directory Domain Name :"></asp:Label></td>
        <td align="left" style="height: 26px">
            <asp:TextBox ID="ADDomainNameTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ADDomainNameTextBox"
                Display="Dynamic" ErrorMessage="Domain Name Required"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%" valign="top">
            <asp:Label ID="lblADUsername" runat="server" Text="Active Directory Username :"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="ADUsernameTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ADUsernameTextBox"
                Display="Dynamic" ErrorMessage="Username Required"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%; height: 26px;" valign="top">
            <asp:Label ID="lblADPassword" runat="server" Text="Active Directory Password :"></asp:Label></td>
        <td align="left" style="height: 26px">
            <asp:TextBox ID="ADPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ADPasswordTextBox"
                Display="Dynamic" ErrorMessage="Password Required"></asp:RequiredFieldValidator></td>
    </tr>
    <%
    End If
    %>
     <% If RDOpenLDAP.Checked = True Then %>   
      <tr>
        <th class="FormViewSubHeader" colspan="2">
            Open LDAP Settings</th>
    </tr>
     <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%" valign="top">
             <SPAN 
                  class=reqasterisk>* </span><asp:Label ID="Label1" runat="server" Text="LDAP Host:"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtLDAPHost" runat="server" 
                Width="480px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLDAPHost"
                Display="Dynamic" ErrorMessage="Host Required"></asp:RequiredFieldValidator></td>
    </tr>
     <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%" valign="top">
             <SPAN 
                  class=reqasterisk>* </span><asp:Label ID="Label2" runat="server" Text="LDAP Port:"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtLDAPPort" runat="server" 
                Width="480px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLDAPPort"
                Display="Dynamic" ErrorMessage="Port Required"></asp:RequiredFieldValidator></td>
    </tr>
     <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%" valign="top">
             <SPAN 
                  class=reqasterisk>* </span><asp:Label ID="Label3" runat="server" Text="LDAP Base DN:"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtLDAPBaseDN" runat="server" 
                Width="480px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLDAPBaseDN"
                Display="Dynamic" ErrorMessage="Base DN Required"></asp:RequiredFieldValidator></td>
    </tr>
      <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%" valign="top">
             <SPAN 
                  class=reqasterisk>* </span><asp:Label ID="Label4" runat="server" Text="LDAP Username:"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtLDAPUsername" runat="server" 
                Width="480px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtLDAPUsername"
                Display="Dynamic" ErrorMessage="Username Required"></asp:RequiredFieldValidator></td>
    </tr>
     <tr>
        <td align="right" class="FormViewLabelCell" style="width: 30%" valign="top">
             <SPAN 
                  class=reqasterisk>* </span><asp:Label ID="Label5" runat="server" Text="LDAP Password:"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtLDAPPassword" runat="server" 
                Width="480px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtLDAPPassword"
                Display="Dynamic" ErrorMessage="Password Required"></asp:RequiredFieldValidator></td>
    </tr>
     <% End If %>
    <tr>
        <td class="FormViewLabelCell" style="width: 30%">
        </td>
        <td align="left" style="padding-bottom: 5px">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                OnClick="btnUpdate_Click" Width="68px" />
            <asp:Label ID="lblErrorMessage" runat="server" Text="Label" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
          </td>
    </tr>
    
</table>
</td></tr></table>