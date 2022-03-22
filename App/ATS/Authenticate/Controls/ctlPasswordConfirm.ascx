<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlPasswordConfirm.ascx.vb" Inherits="Authenticate_Controls_ctlPasswordConfirm" %>
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<div align="center">


       <table class="xFormView" style="width:400px">
        <tr>
            <td>
                <table class="xFormView">

                    <tr>
                        <th class="caption" colspan="2" style="height: 21px" valign="middle" >
                            <asp:Literal ID="Literal3" runat="server" Text="You must change your password." meta:resourcekey="Literal3Resource1"></asp:Literal>
                        </th>
                    </tr>

                    <tr>
                        <td  class="FormViewLabelCell" style="width: 65px; height: 30px">
                            <asp:Literal ID="Literal1" runat="server" Text="New Password:" meta:resourcekey="Literal1Resource1"></asp:Literal>
                        </td>
                        <td style="width: 65%">
                            <asp:TextBox ID="txtNewPassword" runat="server" Width="200px" TextMode="Password" meta:resourcekey="txtNewPasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassword"
                                CssClass="ErrorMessage" Display="Dynamic" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNewPassword"
                            Display="Dynamic" SkinID="PasswordValidator" ValidationExpression="(?=^.{8,}$)(?=.*\d)(?=.*\W+)(?![.\n]).*$"></asp:RegularExpressionValidator>--%>

                        </td>
                    </tr>
                    <tr>
                        <td  class="FormViewLabelCell" style="width: 65px; height: 30px">
                            <asp:Literal ID="Literal2" runat="server" Text="Re-Type Password:" meta:resourcekey="Literal2Resource1"></asp:Literal>
                        </td>
                        <td style="width: 65%">
                            <asp:TextBox ID="txtReTypePassword" runat="server" Width="200px" TextMode="Password" meta:resourcekey="txtReTypePasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReTypePassword"
                                CssClass="ErrorMessage" Display="Dynamic" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtReTypePassword"
                                ControlToValidate="txtNewPassword" CssClass="ErrorMessage" ErrorMessage="(Mismatch)" Display="Dynamic" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td  class="FormViewLabelCell" style="width: 35%"></td>
                        <td style="width: 65%">
                            <asp:Button ID="btnPassword" runat="server" Text="Change Password And Login" meta:resourcekey="btnPasswordResource1" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <table>
</div>
