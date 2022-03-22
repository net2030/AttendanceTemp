<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlLogin.ascx.vb" Inherits="Mobile_Controls_ctlLogin" %>
<asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Mobile/MyChart.aspx" Width="100%">
       
    <LayoutTemplate>
    <div data-role="header" data-theme="a"> 
       <h1 style="text-align:left;margin-left:15px;">Login</h1> 
          </div> 
            <div data-role="content">  
         <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" >
              
                               <tr>
                    <td align="left">
                        <asp:Label ID="UserNameLabel" style="font-size:13px; font-family:Tahoma; font-weight:bold " runat="server" AssociatedControlID="UserName"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:TimeLive.Web, Username:%> " /></asp:Label></td>
                    </tr>
                    <tr><td>
                        <asp:TextBox ID="UserName"  style="height: 30px; font-size:12px "  runat="server" ></asp:TextBox><asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            ErrorMessage="User Name is required." ToolTip="<%$ Resources:TimeLive.Web, Username is required.%> " ValidationGroup="ctl00$Login1" CssClass="ErrorMessage" Display="Dynamic">*</asp:RequiredFieldValidator></td></tr>
                    
                
                <tr>
                    <td align="left"  >
                        <asp:Label ID="PasswordLabel" style="font-size:13px; font-family:Tahoma; font-weight:bold " runat="server" AssociatedControlID="Password"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:TimeLive.Web, Password:%> " /></asp:Label></td>
                     </tr>
                     <tr>
                    <td >
                        <asp:TextBox ID="Password" style="height: 30px" runat="server"  TextMode="Password"></asp:TextBox></td>
              </tr> 
                <tr>
                <td >
                
                        <asp:Button ID="Button1"   runat="server" CommandName="Login" Text="<%$ Resources:TimeLive.Web, Login%> "  ValidationGroup="ctl00$Login1"  UseSubmitBehavior="false"  data-inline="true" />
                 
                </tr>

                <tr>
                    <td align="center" colspan="1" style="color: red; height: 19px;">
                        <asp:Label ID="ErrorText" runat="server" CssClass="ErrorMessage"></asp:Label>
                        
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                        
                     </td>
                </tr>
   
                 <tr>
                     <td align="center" colspan="1" style="height:10px;">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                     </td>
                 </tr>
        </table>
        </div> 
       </LayoutTemplate>
                    
</asp:Login>

