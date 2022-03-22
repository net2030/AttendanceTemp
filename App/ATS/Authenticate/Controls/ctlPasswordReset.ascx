<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlPasswordReset.ascx.vb" Inherits="Authenticate_Controls_ctlPasswordReset" %>


<br />
<br />
<br />
<br />
<div align="center">
    <table class="xFormView" style="width:700px">
        <tr>
            <td>
                <table width="100%" class="xFormView">
                    <tr>
                        <th class="caption" colspan="2" style="height: 21px">
                            <asp:Literal ID="Literal1" runat="server" Text="Trouble Accessing Your Account" meta:resourcekey="Literal1Resource1" />
                           
                        </th>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 35px">
                            <strong>
                                <asp:Literal ID="Literal2" runat="server" Text="Forgot your password? Enter your login email below. We sent a link to reset your password to the following email addresses." meta:resourcekey="Literal2Resource1"></asp:Literal>
                            </strong>

                        </td>
                    </tr>
                    <tr>
                        <td class="FormViewLabelCell">
                            <asp:Literal ID="Literal19" runat="server" Text="Email Address>" meta:resourcekey="Literal19Resource1" />:</td>
                        <td>
                            <asp:TextBox ID="txtEmailAddress" runat="server" Width="300px" meta:resourcekey="txtEmailAddressResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmailAddress"
                                CssClass="ErrorMessage" Display="Dynamic" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="FormViewLabelCell"></td>
                        <td>
                            <asp:Button ID="btnPasswordReset" runat="server" Text="Reset Password" Width="105px" meta:resourcekey="btnPasswordResetResource1" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" Text="This is not a valid email address. If you would like to register please Sign Up. Thank you." Visible="False" CssClass="ErrorMessage" Width="100%" meta:resourcekey="Label2Resource1"></asp:Label><asp:Label ID="Label1" runat="server"
                                Visible="False" ForeColor="Green" Width="100%" meta:resourcekey="Label1Resource1"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
