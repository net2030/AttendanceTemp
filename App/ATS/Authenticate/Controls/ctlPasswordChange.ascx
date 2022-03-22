<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlPasswordChange.ascx.vb" Inherits="Authenticate_Controls_ctlPasswordChange" %>
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<table width="100%" class="xFormView">
<tr>
   <td>
      <table width="100%" class="xFormView">
         <tr>
            <th class="caption" colspan="2" style="height: 21px"  valign="middle" align="center"  >
               ÌÃ»  €ÌÌ— ﬂ·„… «·„—Ê—.
            </th>
         </tr>
         <tr>
            <td align="right" class="FormViewLabelCell" style="width: 65px; height: 30px">
               ﬂ·„… «·„—Ê— «·Õ«·Ì…:
            </td>
            <td style="width: 65%">
               <asp:TextBox ID="txtCurrentPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
               <asp:RequiredFieldValidator
                  ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCurrentPassword"
                  CssClass="ErrorMessage" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
            <td align="right" class="FormViewLabelCell" style="width: 65px; height: 30px">
               ﬂ·„… «·„—Ê— «·ÃœÌœ…:
            </td>
            <td style="width: 65%">
               <asp:TextBox ID="txtNewPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassword"
                  CssClass="ErrorMessage" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Enabled="false" ControlToValidate="txtNewPassword"
                  Display="Dynamic" SkinID="PasswordValidator" ValidationExpression="(?=^.{8,}$)(?=.*\d)(?=.*\W+)(?![.\n]).*$"></asp:RegularExpressionValidator>
            </td>
         </tr>
         <tr>
            <td align="right" class="FormViewLabelCell" style="width: 65px; height: 30px">
                √ﬂÌœ ﬂ·„… «·„—Ê—:
            </td>
            <td style="width: 65%">
               <asp:TextBox ID="txtReTypePassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReTypePassword"
                  CssClass="ErrorMessage" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
               <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtReTypePassword"
                  ControlToValidate="txtNewPassword" CssClass="ErrorMessage" ErrorMessage="(Mismatch)" Display="Dynamic"></asp:CompareValidator>
            </td>
         </tr>
         <tr>
            <td align="right" class="FormViewLabelCell" style="width: 35%">
            </td>
            <td style="width: 65%">
               <asp:Button ID="btnPassword" runat="server" Text=" €ÌÌ— Ê ”ÃÌ· œŒÊ·" />
            </td>
         </tr>
         <tr>
            <td colspan="2">
               <asp:Label ID="lblIncorrect" runat="server" Text="ﬂ·„… „—Ê— Œ«ÿ∆…."
                  Visible="False" CssClass="ErrorMessage" Width="100%" Font-Bold="True" Font-Size="Larger"></asp:Label>
               <asp:Label ID="lblSameError" runat="server" Text="ﬂ·„… «·„—Ê— ÌÃ» «‰  ﬂÊ‰ „Œ ·›… ⁄‰ «·ﬁœÌ„…."
                  Visible="False" ForeColor="Red" Width="100%" Font-Bold="True" Font-Size="Larger"></asp:Label>
            </td>
         </tr>
      </table>
   </td>
</tr>
<table>