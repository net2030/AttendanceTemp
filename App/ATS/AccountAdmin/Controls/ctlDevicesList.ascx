<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlDevicesList.ascx.vb" Inherits="AccountAdmin_Controls_ctlDevicesList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/AccountAdmin/Controls/ctlDeviceEmployees.ascx" TagPrefix="uc1" TagName="ctlDeviceEmployees" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>


<style type="text/css">
    .style2 {
        height: 24px;
        width: 34%;
    }

    .style9 {
        width: 34%;
    }
</style>
<script src="../js/jquery.min.js"></script>

<asp:Label ID="lblErr" runat="server" ForeColor="Red" meta:resourcekey="lblErrResource1"></asp:Label>

        <x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="MachineId,MachineName,Location"
            Caption="ÞÇÆãÉ ÃÌåÒÉ ÇáÍÖæÑ æÇáÇäÕÑÇÝ"
            SkinID="xgridviewSkinEmployee" Width="50%" CssClass="TableView" PageSize="300" OnRowEditing="GridView2_RowEditing" meta:resourcekey="GridView2Resource2">
            <Columns>

                <asp:BoundField ItemStyle-Width="20%" ItemStyle-Wrap="false" DataField="MachineId" HeaderText="ÇáÑÞã" meta:resourcekey="BoundFieldResource4"  >
<ItemStyle Wrap="False" Width="20%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="MachineName" HeaderText="ÅÓã ÇáÌåÇÒ" meta:resourcekey="BoundFieldResource5" />
                 <asp:BoundField DataField="Location" HeaderText="ãæÞÚ ÇáÌåÇÒ" meta:resourcekey="BoundFieldResource6" />
                <asp:BoundField DataField="IPAddress" HeaderText="ÚäæÇä ÇáÌåÇÒ" meta:resourcekey="BoundFieldResource7" />
       <%--         <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="ShowEmployees" runat="server"
                            CommandName="show"
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                            Text="ÇáãæÙÝíä ÇáãÓÌáíä" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="ÇáÍÇáÉ" ShowHeader="False" ItemStyle-Width="6%" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:Image ID="Status" runat="server" ImageUrl='<%# If(PortScan(Eval("IPAddress"), If(Eval("MachineTypeId") = 1, 37777, If(Eval("MachineTypeId") = 2, 4370, If(Eval("MachineTypeId") = 3, 1480, 10001)))), "~/Images/online.gif", "~/Images/offline.gif") %>' Width="48px" Height="48px" ImageAlign="Middle" meta:resourcekey="StatusResource2"></asp:Image>
                    </ItemTemplate>

<ItemStyle Width="6%"></ItemStyle>

                </asp:TemplateField>
                  <asp:TemplateField HeaderText="ÊÚÏíá" ShowHeader="False" meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:LinkButton id="EditLinkButton" runat="server" Text="" CommandName="Edit" CausesValidation="False"  ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" cssclass="edit_button" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ÍÐÝ" ShowHeader="False" meta:resourcekey="TemplateFieldResource4">
            <ItemTemplate>
                <asp:LinkButton id="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                 OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False" ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" cssclass="delete_button" HorizontalAlign="Center" />
        </asp:TemplateField>

            </Columns>
        </x:GridView>

 <asp:Button ID="btnAddMachine" runat="server"   
                            Text="ÅÖÇÝÉ ÌÏíÏ" 
                            UseSubmitBehavior="False" CausesValidation = "False" meta:resourcekey="btnAddMachineResource1" />




<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <ContentTemplate>--%>
<table>
    <tr>
        <td style="width:50%">
<asp:FormView ID="FormView1" runat="server"
                      SkinID="formviewskinemployee"
                      DefaultMode="Insert" Width="100%" meta:resourcekey="FormView1Resource1" >
      
         <EditItemTemplate>
            <table  width="100%" class="xview">
               <tr>
                  <th class="caption" colspan="4" style="width: 20%; height: 21px">
                     <asp:Literal ID="Literal1" runat="server" Text="ãÚáæãÇÊ ÇáÌåÇÒ" meta:resourcekey="Literal1Resource1" />
                  </th>
               </tr>
               <tr>
                  <th class="FormViewSubHeader" colspan="4" style="width: 20%; height: 21px">
                     <asp:Literal ID="Literal2" runat="server" Text="ÊÚÏíá ÇáÌåÇÒ" meta:resourcekey="Literal2Resource1" />
                  </th>
               </tr>
               <tr>
                  <td colspan ="2">
                     <asp:Label ID="Label2" runat ="server" meta:resourcekey="Label2Resource1" ></asp:Label>
                  </td>
               </tr>

                 <tr>
                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text=" ÊÓáÓá:" meta:resourcekey="Literal8Resource1"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtMachineId" runat="server" ReadOnly="True"  Text='<%# Bind("MachineId") %>' meta:resourcekey="txtMachineIdResource1"></asp:TextBox>
                        </td>
                    </tr>

                  <tr>
                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text=" ÅÓã ÇáÌåÇÒ:" meta:resourcekey="Literal4Resource1"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtMachineName" runat="server"  Text='<%# Bind("MachineName") %>' meta:resourcekey="txtMachineNameResource1"></asp:TextBox>
                              <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtMachineName"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                        </td>

                      <td>
                            <asp:Literal ID="Literal5" runat="server" Text=" ãæÞÚ ÇáÌåÇÒ:" meta:resourcekey="Literal5Resource1"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtLocation" runat="server"  Text='<%# Bind("Location") %>' meta:resourcekey="txtLocationResource1"></asp:TextBox>
                              <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtLocation"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        </td>

                    </tr>

                 <tr>
                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text=" ÚäæÇä ÇáÌåÇÒ:" meta:resourcekey="Literal6Resource1"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtIPAddress" runat="server"  Text='<%# Bind("IPAddress") %>' meta:resourcekey="txtIPAddressResource1"></asp:TextBox>
                              <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtIPAddress"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                                                             ID="RegularExpressionValidator1" 
                                                             runat="server" ErrorMessage="Invalid IP !" 
                                                             ControlToValidate="txtIPAddress" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator></div>
                        </td>

                      <td>
                            <asp:Literal ID="Literal7" runat="server" Text=" äæÚ ÇáÌåÇÒ:" meta:resourcekey="Literal7Resource1"/>
                           
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server"  SelectedValue='<%# Bind("MachineTypeId") %>' meta:resourcekey="ddlTypeResource1">
                                <asp:ListItem Value="0" Text="ÅÎÊÑ äæÚ ÇáÌåÇÒ" meta:resourcekey="ListItemResource1" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="MAS" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                <asp:ListItem Value="2" Text="ZKTeco" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                <asp:ListItem Value="3" Text="BioStar" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                <asp:ListItem Value="4" Text="L1 Identitty" meta:resourcekey="ListItemResource5"></asp:ListItem>
                            </asp:DropDownList>
                              <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                        ID="RequiredFieldValidator4" Display="Dynamic" 
                                                        ControlToValidate="ddlType"
                                                        runat="server"  Text="*" 
                                                        ErrorMessage="*"
                                                        ForeColor="Red" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                         
                        </td>

                    </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>

                 <tr>
                         <td colspan="2">
                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                        Text="ÍÝÙ" meta:resourcekey="btnUpdateResource1" />
                        
                            <asp:Button ID="Button2" runat="server"  OnClick="Button2_Click" CausesValidation="False"
                                        Text="ÅáÛÇÁ ÇáÃãÑ" meta:resourcekey="Button2Resource1" />
                        </td>
                    </tr>
 
            </table>
         </EditItemTemplate>
         <InsertItemTemplate>
            <table  width="100%" class="xview">
               <tr>
                  <th class="caption" colspan="4" style="width: 20%; height: 21px">
                     <asp:Literal ID="Literal1" runat="server" Text="ãÚáæãÇÊ ÇáÌåÇÒ" meta:resourcekey="Literal1Resource2" />
                  </th>
               </tr>
               <tr>
                  <th class="FormViewSubHeader" colspan="4" style="width: 20%; height: 21px">
                     <asp:Literal ID="Literal2" runat="server" Text="ÅÖÇÝÉ ÇáÌåÇÒ" meta:resourcekey="Literal2Resource2" />
                  </th>
               </tr>
               <tr>
                  <td colspan ="2">
                     <asp:Label ID="Label2" runat ="server" meta:resourcekey="Label2Resource2"  ></asp:Label>
                  </td>
               </tr>

                <tr>
                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text=" ÅÓã ÇáÌåÇÒ:" meta:resourcekey="Literal4Resource2"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtMachineName" runat="server" meta:resourcekey="txtMachineNameResource2"></asp:TextBox>
                             <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtMachineName"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource2"></asp:RequiredFieldValidator>
                        </td>

                      <td>
                            <asp:Literal ID="Literal5" runat="server" Text=" ãæÞÚ ÇáÌåÇÒ:" meta:resourcekey="Literal5Resource2"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtLocation" runat="server" meta:resourcekey="txtLocationResource2"  ></asp:TextBox>
                              <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtLocation"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator>
                        </td>

                    </tr>

                 <tr>
                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text=" ÚäæÇä ÇáÌåÇÒ:" meta:resourcekey="Literal6Resource2"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtIPAddress" runat="server" meta:resourcekey="txtIPAddressResource2"  ></asp:TextBox>
                              <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtIPAddress"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator3Resource2"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                                                             ID="RegularExpressionValidator1" 
                                                             runat="server" ErrorMessage="Invalid IP !" 
                                                             ControlToValidate="txtIPAddress" meta:resourcekey="RegularExpressionValidator1Resource2"></asp:RegularExpressionValidator></div>


                        </td>

                      <td>
                            <asp:Literal ID="Literal7" runat="server" Text=" äæÚ ÇáÌåÇÒ:" meta:resourcekey="Literal7Resource2"/>
                           
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" meta:resourcekey="ddlTypeResource2"  >
                                <asp:ListItem Value="0" Text="ÅÎÊÑ äæÚ ÇáÌåÇÒ" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                <asp:ListItem Value="1" Text="MAS" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                <asp:ListItem Value="2" Text="ZKTeco" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                <asp:ListItem Value="3" Text="BioStar" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                <asp:ListItem Value="4" Text="L1 Identitty" meta:resourcekey="ListItemResource10"></asp:ListItem>
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                        ID="RequiredFieldValidator4" Display="Dynamic" 
                                                        ControlToValidate="ddlType"
                                                        runat="server"  Text="*" 
                                                        ErrorMessage="*"
                                                        ForeColor="Red" meta:resourcekey="RequiredFieldValidator4Resource2"></asp:RequiredFieldValidator>
                         
                        </td>

                    </tr>

                 <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>

                 <tr>
                        <td colspan="2">
                            <asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click"
                                        Text="ÍÝÙ" meta:resourcekey="btnAddNewResource1" />
                     
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click"  Text="ÅáÛÇÁ ÇáÃãÑ" CausesValidation="False" meta:resourcekey="Button2Resource2" />
                        </td>
                    </tr>
 
     
            </table>
         </InsertItemTemplate>
         
      </asp:FormView>
        </td>
        <td style="">
             <% If Me.FormView1.CurrentMode = FormViewMode.Edit And Not Me.GridView2.Visible Then%>
             <uc1:ctlDeviceEmployees runat="server" ID="ctlDeviceEmployees" />
  <% End If %>
        </td>
    </tr>
</table>
         
<%--   </ContentTemplate>
</asp:UpdatePanel>--%>
<br />

 