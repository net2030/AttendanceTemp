<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlSetupDatabase.ascx.vb" Inherits="Home_Controls_ctlSetupDataBase" %>
<br />
<br />
<table class="xFormView" width="100%" ><tr><td>
<table width="100%" class="xFormView" align ="center" valign ="middle">
    <tr>
        <th class="caption" colspan="2">
            Database Setup Information</th>
    </tr>
    <tr>
        <th class="FormViewSubHeader" colspan="2" style="height: 21px">
            Database Setup</th>
    </tr>
    <tr>
        <th class="FormViewSubHeader" colspan="2" style="height:40px;">
            <asp:Label ID="Label1" runat="server" Font-Bold="False"  Font-Size="10pt"
                ForeColor="Blue"></asp:Label></th>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCellBold" style="width: 25%; height: 35px;">
            Select database setup option:
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:RadioButton ID="rdCreateDatabase" runat="server" AutoPostBack="True" GroupName="SetupOption"
                Text="Create Database and popuplate schema" /><br />
            <asp:RadioButton ID="rdPopulateSchema" runat="server" AutoPostBack="True" GroupName="SetupOption"
                Text="Populate schema only (for shared hosting)" /><br />
            <asp:RadioButton ID="rdExistingDatabase" runat="server" AutoPostBack="True" GroupName="SetupOption"
                Text="Use existing database" /></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCellBold" style="width: 25%; height: 35px;">
           <SPAN 
                  class=reqasterisk>*</span> Server Name: 
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:TextBox ID="txtServerName" runat="server" Width="188px"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="Smaller"
                ForeColor="Red" Text="(SQL Server name or IP Address where TimeLive database is required to be setup.)"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServerName"
                ErrorMessage="Server Name Required" CssClass="ErrorMessage" Display="Dynamic" ForeColor=""></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCellBold" style="width: 25%; height: 35px;">
            Instance Name:
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:TextBox ID="txtInstanceName" runat="server" Width="188px"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="Smaller"
                ForeColor="Red" Text="(Optional SQL Server Instance Name)"></asp:Label></td>
    </tr>

    <tr>
        <td align="right" class="FormViewLabelCellBold" style="width: 25%; height: 35px;">
           <SPAN 
                  class=reqasterisk>*</span> Authentication Type:
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:DropDownList ID="ddlAuthenticationType" runat="server" AppendDataBoundItems="True"
                AutoPostBack="True" Width="200px">
                <asp:ListItem Value="1">SQL Server Authentication</asp:ListItem>
                <asp:ListItem Value="0">Windows Authentication</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="Smaller"
                ForeColor="Red" Text="(Type of authentication which is setup on your SQL Server)"></asp:Label></td>
    </tr>
    <% If ddlAuthenticationType.SelectedValue <> 0 And rdCreateDatabase.Checked = True Then%>
    <tr>
        <td align="right" class="FormViewLabelCellBold" style="width: 25%; height: 35px;">
            &nbsp;SA Login:
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:TextBox ID="txtLogin" runat="server" Width="188px">sa</asp:TextBox>
            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="Smaller"
                ForeColor="Red" Text='(sa username: Either "sa" or "sa" equivalent: Only for SQL Server Authentication)'></asp:Label></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCellBold" style="width: 25%; height: 35px;">
            SA
            Password:
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:TextBox ID="txtPassword" runat="server" Width="188px"></asp:TextBox>
            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="Smaller"
                ForeColor="Red" Text="(sa user password: Required only for SQL Server Authentication)"></asp:Label></td>
    </tr>
    <% End If%>
    <tr>
        <td align="right" class="FormViewLabelCellBold" style="width: 25%; height: 35px;">
           <SPAN 
                  class=reqasterisk>*</span> Database Name:
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:TextBox ID="txtDataBaseName" runat="server" Width="188px"></asp:TextBox>
            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="Smaller"
                ForeColor="Red" Text="(Name of database which is required to be setup on SQL Server specified instance)"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDataBaseName"
                ErrorMessage="Database Name Required" CssClass="ErrorMessage" Display="Dynamic" ForeColor=""></asp:RequiredFieldValidator></td>
    </tr>
          <% If ddlAuthenticationType.SelectedValue <> 0 Then%>
    <tr>
        <td align="right" class="FormViewLabelCellBold" style="width: 25%; height: 35px;">
           <SPAN 
                  class=reqasterisk>*</span> Database Username:
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:TextBox ID="txtTimeLiveUserName" runat="server" Width="188px"></asp:TextBox>
            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="Smaller"
                ForeColor="Red" Text="(New TimeLive database username which will be created for TimeLive database)"></asp:Label></td>
    </tr>
    <tr>
        <td align="right" class="FormViewLabelCellBold" style="width: 25%; height: 35px;">
           <SPAN 
                  class=reqasterisk>*</span> Database User's Password:
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:TextBox ID="txtTimeLivePassword" runat="server" Width="188px"></asp:TextBox>
            <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="Smaller"
                ForeColor="Red" Text="(New TimeLive database user's password which will be created for TimeLive database)"></asp:Label></td>
    </tr>
       <% End If%>
    <tr>
        <td align="right" class="FormViewLabelCell" style="width: 25%; height: 35px;">
        </td>
        <td style="width: 75%; height: 35px;">
            <asp:Button ID="btnCreateDatabase" runat="server" Text="Create Database and populate schema" Width="250px" Visible="False" />
            <asp:Button ID="btnPopulateSchema" runat="server" Text="Populate schema only" Width="130px" Visible="False" />
            <asp:Button ID="btnExistingDatabase" runat="server" Text="Use Existing Database" Width="140px" Visible="False" />
            <asp:Label ID="lblexception" runat="server" Text="Label" Visible="False" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="Red"></asp:Label><asp:TextBox ID="TextBox1" runat="server" Enabled="False" Visible="False" Width="10px"></asp:TextBox></td>
    </tr>
 
</table>
</td></tr></table>
<br />
&nbsp;&nbsp;
