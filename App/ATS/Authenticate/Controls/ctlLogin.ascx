<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlLogin.ascx.vb" Inherits="Auth_Controls_ctlLogin" %>
<style type="text/css">
    .auto-style1 {
        height: 35px;
    }
</style>


<asp:Login ID="Login1" runat="server" Width="430px" meta:resourcekey="Login1Resource1">
    <LayoutTemplate>
        <table>
            <tr>
                <td width="30%"></td>
                <td align="Right">
                    <asp:Image ID="imgCompanyOwnLogo" runat="server" ImageAlign="Middle" Height="125px" Width="155px" ImageUrl="~/Images/TopHeader.jpg" AlternateText="CompanyLogo" />
                </td>
                <td width="30%">&nbsp;</td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" class="xFormView" width="375"
            style="border: 5px solid #F6F6F6;">
            <tr>

                <td>
                    <table cellpadding="0" cellspacing="0" class="xFormView" width="375"
                        style="border: 1px solid #d6dadf;">
                        <tr>
                            <th class="captionLogin" colspan="3" style="text-align: center;">
                                <asp:Literal ID="TimeLiveLogin" runat="server" Text="MASTMS LogIn" meta:resourcekey="TimeLiveLoginResource1" />
                            </th>
                        </tr>
                        <tr>
                            <td style="vertical-align: middle" width="35%">&nbsp;
                            </td>
                            <td
                                width="60%">&nbsp;</td>
                            <td
                                width="5%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="vertical-align: middle; padding-left: 20px" width="35%">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"
                                    Font-Bold="False" meta:resourcekey="UserNameLabelResource1">
                                    
                               

                                </asp:Label>

                            </td>
                            <td
                                width="60%" s>
                                <asp:TextBox ID="UserName" runat="server" CssClass="txtBox"
                                    Width="95%" meta:resourcekey="UserNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    CssClass="ErrorMessage" Display="Dynamic" ErrorMessage="User Name is required."
                                    ToolTip="<%$ Resources:TimeLive.Web, Username is required. %>"
                                    ValidationGroup="ctl00$Login1" meta:resourcekey="UserNameRequiredResource1">*</asp:RequiredFieldValidator>
                            </td>
                            <td
                                width="5%">&nbsp;</td>

                        </tr>
                        <tr>
                            <td
                                style="vertical-align: middle; padding-left: 20px" width="35%" class="auto-style1">
                                <asp:Label
                                    ID="PasswordLabel" runat="server" AssociatedControlID="Password"
                                    Font-Bold="False" meta:resourcekey="PasswordLabelResource1">
                                    
                                </asp:Label>
                            </td>
                            <td
                                width="60%" class="auto-style1">
                                <asp:TextBox ID="Password" runat="server" CssClass="txtBox"
                                    TextMode="Password" Width="95%" meta:resourcekey="PasswordResource1"></asp:TextBox>
                            </td>
                            <td
                                width="5%" class="auto-style1"></td>

                        </tr>

                        <tr>
                            <td
                                style="vertical-align: middle" width="35%">&nbsp;</td>
                            <td
                                width="5%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td
                                width="35%">&nbsp;</td>
                            <td width="60%">
                                <asp:Button
                                    ID="Button2" runat="server" CssClass="go" Text="Sign Up" OnClick="Button2_Click" meta:resourcekey="Button2Resource1" />
                                <asp:Button
                                    ID="Button1" runat="server" CommandName="Login" CssClass="go"
                                    Text="ÏÎæá" ValidationGroup="ctl00$Login1" meta:resourcekey="Button1Resource1" />
                            </td>
                            <td
                                width="5%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td
                                width="35%">&nbsp;</td>
                            <td width="60%">
                                <asp:Label ID="ErrorText"
                                    runat="server" CssClass="ErrorMessage" meta:resourcekey="ErrorTextResource1"></asp:Label>
                                <asp:Literal
                                    ID="FailureText" runat="server" EnableViewState="False" meta:resourcekey="FailureTextResource1"></asp:Literal>
                            </td>
                            <td
                                width="5%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td
                                colspan="3"
                                style="border: 1px solid #d6dadf; background-color: #F6F6F6; vertical-align: middle;"
                                height="30px">
                                <a
                                    href="Authenticate/PasswordReset.aspx" target="_blank"
                                    style="color: #000000; text-decoration: underline;">
                                    <asp:Literal
                                        ID="Literal5" runat="server"
                                        Text="äÓíÊ ßáãÉ ÇáãÑæÑ¿ " meta:resourcekey="Literal5Resource1" />
                                </a>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table
            cellpadding="0" cellspacing="0" align="center" class="xFormView" width="375">
            <tr>
                <td
                    width="45%">
                    <asp:Image ID="Image1" runat="server"
                        ImageUrl="~/Images/version.gif" meta:resourcekey="Image1Resource1" />
                </td>
                <td width="75px">
                    <asp:Label ID="VersionLabel"
                        runat="server" Font-Underline="True" Width="75px" meta:resourcekey="VersionLabelResource1">V 2.93</asp:Label>
                </td>
                <td></td>

            </tr>
        </table>
        <table
            cellpadding="0" cellspacing="0" align="center" class="xFormView" width="375">
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server"><%= Resources.MulResource.CopyRightTextResource1 + Now.Date.Year.ToString()  + "  " +  Resources.MulResource.CopyRightTextResource2  %></asp:Label>
                </td>
            </tr>
        </table>
    </LayoutTemplate>
</asp:Login>

<p>
    <br />
</p>
