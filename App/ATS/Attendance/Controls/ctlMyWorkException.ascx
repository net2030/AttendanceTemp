<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMyWorkException.ascx.vb" Inherits="Attendance_Controls_ctlMyWorkException" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/Attendance/Controls/ctlEmployeesLeaveBalance.ascx" TagPrefix="uc1" TagName="ctlEmployeesLeaveBalance" %>


<style type="text/css">
    .style1
    {
        height: 23px;
    }
</style>

<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="WorkExceptionId" Caption="ﬁ«∆„… ≈” À«¡«  «·œÊ«„" 
            SkinID="xgridviewSkinEmployee" Width="98%"   ShowHeaderWhenEmpty="True" DataSourceID="dsWorkExceptions" meta:resourcekey="GridView2Resource1">
    <Columns>

        <asp:BoundField DataField="WorkExceptionId" HeaderText="„”·”·" ReadOnly="True" meta:resourcekey="BoundFieldResource1"  />

        <asp:BoundField DataField="EmployeeName" HeaderText="≈”„ «·„ÊŸ›" meta:resourcekey="BoundFieldResource2" />

        <asp:BoundField DataField="WorkExceptionTypeName" HeaderText="‰Ê⁄ «·≈” À‰«¡" meta:resourcekey="BoundFieldResource3" />

        <asp:BoundField DataField="WorkExceptionBegDate" HeaderText=" «—ÌŒ «·»œ«Ì…" meta:resourcekey="BoundFieldResource4" />

        <asp:BoundField DataField="WorkExceptionEndDate" HeaderText=" «—ÌŒ «·‰Â«Ì…" meta:resourcekey="BoundFieldResource5" />

        <asp:BoundField DataField="AprovalStatus" HeaderText="Õ«·… «·ÿ·»" meta:resourcekey="BoundFieldResource6" />

        <asp:BoundField DataField="ApprovalNotes" HeaderText="„·«ÕŸ« " meta:resourcekey="BoundFieldResource7" />

        <asp:TemplateField HeaderText=" ⁄œÌ·" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:LinkButton id="EditLinkButton" runat="server" Text="" CommandName="Edit"   CausesValidation="False"
                                Visible='<%# If(Eval("IsApproved").ToString().Equals("True"), False, True) %>'   ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" cssclass="edit_button" HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Õ–›" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
            <ItemTemplate>
                <asp:LinkButton id="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                Visible='<%# If(Eval("IsApproved").ToString().Equals("True"), False, True) %>'  OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False" ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" cssclass="delete_button" HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
</x:GridView>

<p>
    <asp:Button ID="btnAdd0" runat="server" OnClick="btnNew"   CausesValidation="False"
                Text=" ”ÃÌ· ÃœÌœ" 
                UseSubmitBehavior="False" meta:resourcekey="btnAdd0Resource1" />
</p>

<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>

        <asp:FormView ID="FormView1" runat="server"
                      SkinID="formviewskinemployee"
                      DefaultMode="Insert" Width="98%" Visible="False" meta:resourcekey="FormView1Resource1" >
            <InsertItemTemplate>
                <table width="100%" class="xformview" >
                    <tr>
                        <th colspan="6" class="caption" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈” À‰«¡ œÊ«„" meta:resourcekey="Literal9Resource2"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="6" class="FormViewSubHeader" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal10" runat="server" Text=" ÕœÌÀ »Ì«‰«  ≈” À‰«¡ " meta:resourcekey="Literal10Resource2" />
                        </th>
                    </tr>
                    <tr>
                         <td colspan="2">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource2"  />
                        </td>
                        <td rowspan="6">
                            <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:10%">
                            <asp:Literal ID="Literal1" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal1Resource2"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWorkExceptionId" runat="server" ReadOnly="True" Text='<%# Bind("WorkExceptionId") %>' meta:resourcekey="txtWorkExceptionIdResource2"></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr>
                        <td >
                            <asp:Literal ID="Literal2" runat="server" Text="≈”„ «·„ÊŸ›: " meta:resourcekey="Literal2Resource2"></asp:Literal>
                        </td>
                        <td colspan="2" >
                             <asp:TextBox ID="txtEmployeeName" runat="server" ReadOnly="True" Text='<%# Session("EmployeeNameWithCode") %>' meta:resourcekey="txtEmployeeNameResource2"></asp:TextBox>

                        </td>
                        </tr>
                    <tr>
                        <td>
                             <asp:Literal ID="Literal3" runat="server" Text="‰Ê⁄ «·≈” À«¡" meta:resourcekey="Literal3Resource2"></asp:Literal>
                        </td>
                        <td >
                            <asp:DropDownList ID="ddlWorkExceptionTypeId" runat="server" style="margin-right: 0px"
                                              DataSourceID="dsWorkExceptionTypes" 
                                              DataTextField="WorkExceptionTypeName" 
                                              DataValueField="WorkExceptionTypeId" 
                                              SelectedValue='<%# Bind("WorkExceptionTypeId") %>' meta:resourcekey="ddlWorkExceptionTypeIdResource2">
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Selected="True">....≈Œ —....</asp:ListItem>
                            </asp:DropDownList>
                        <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                    ID="RequiredFieldValidator2" Display="Dynamic" 
                                                    ControlToValidate="ddlWorkExceptionTypeId"
                                                    runat="server"  Text="*" 
                                                    ErrorMessage="≈Œ — ‰Ê⁄ «·«” À‰«¡"
                                                    ForeColor="Red" ValidationGroup="Insert" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td >
                            <asp:Literal ID="Literal4" runat="server" Text=" Ì»œ√ „‰ :" meta:resourcekey="Literal4Resource2"></asp:Literal>
                        </td>
                        <td>
                            <uc1:MyDate runat="server" id="datWorkExceptionBegDate" SelectedDate="<%# Now.Date %>" />
                        </td>

                        <td >
                             <asp:Literal ID="Literal5" runat="server" Text="Ì‰ ÂÌ ›Ì: " meta:resourcekey="Literal5Resource2"></asp:Literal>
                        </td>
                        <td >
                            <uc1:MyDate runat="server" id="datWorkExceptionEndDate" SelectedDate="<%# Now.Date %>" />
                            <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "datWorkExceptionEndDate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource2" ></asp:CustomValidator>
                             <asp:CustomValidator id="ChckbalanceValidator" runat="server" 
                                                 ControlToValidate = "datWorkExceptionEndDate$TextBox1"
                                                 ErrorMessage = "·« ÌÊÃœ —’Ìœ ﬂ«›Ì"
                                                 ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource2" ></asp:CustomValidator>
                        </td>
                    </tr>

                    <tr>
                        <td >
                             <asp:Literal ID="Literal6" runat="server" Text="Ê’› «·„Â„…: " meta:resourcekey="Literal6Resource2"></asp:Literal>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNotes" runat="server"  TextMode="MultiLine" Width="300px" Height="70px" Text='<%# Bind("Notes") %>' meta:resourcekey="txtNotesResource2"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataAdd" 
                                        Text="Õ›Ÿ «·≈” À‰«¡" meta:resourcekey="Button1Resource2" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server"  OnClientClick="ReloadSamepage(); return false;"
                                        Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource2" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>

            <EditItemTemplate>
                <table width="100%" class="xformview" >
                    <tr>
                        <th colspan="6" class="caption" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈” À‰«¡ œÊ«„" meta:resourcekey="Literal9Resource1"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="6" class="FormViewSubHeader" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal10" runat="server" Text=" ÕœÌÀ »Ì«‰«  «” À‰«¡" meta:resourcekey="Literal10Resource1" />
                        </th>
                    </tr>
                    <tr>
                         <td colspan="2">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1"  />
                        </td>
                        <td rowspan="6">
                            <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:10%">
                            <asp:Literal ID="Literal1" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal1Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWorkExceptionId" runat="server" ReadOnly="True" Text='<%# Bind("WorkExceptionId") %>' meta:resourcekey="txtWorkExceptionIdResource1"></asp:TextBox>
                        </td>
                       
                    </tr>
                  

                    <tr>
                        <td >
                             <asp:Literal ID="Literal2" runat="server" Text="≈”„ «·„ÊŸ›: " meta:resourcekey="Literal2Resource1"></asp:Literal>
                        </td>
                        <td colspan="2" >
                             <asp:TextBox ID="txtEmployeeName" runat="server" ReadOnly="True" Text='<%# Session("EmployeeNameWithCode") %>' meta:resourcekey="txtEmployeeNameResource1"></asp:TextBox>
                         
                        </td>
                        </tr>
                    <tr>
                        <td >
                            <asp:Literal ID="Literal3" runat="server" Text=" ‰Ê⁄ «·≈” À‰«¡:" meta:resourcekey="Literal3Resource1"></asp:Literal>
                        </td>
                        <td colspan="2" >
                            <asp:DropDownList ID="ddlWorkExceptionTypeId" runat="server" style="margin-right: 0px" 
                                              DataSourceID="dsWorkExceptionTypes" 
                                              DataTextField="WorkExceptionTypeName" 
                                              DataValueField="WorkExceptionTypeId" 
                                              SelectedValue='<%# Bind("WorkExceptionTypeId") %>' meta:resourcekey="ddlWorkExceptionTypeIdResource1">
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="WorkExceptionTypeRequiredFieldValidator" ControlToValidate="ddlWorkExceptionTypeId"
                                                        ErrorMessage="≈Œ — ‰Ê⁄ «·≈” À‰«¡ !" meta:resourcekey="WorkExceptionTypeRequiredFieldValidatorResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                        <tr>
                        <td >
                            <asp:Literal ID="Literal4" runat="server" Text=" Ì»œ√ „‰:" meta:resourcekey="Literal4Resource1"></asp:Literal>
                        </td>
                        <td>
                            <uc1:MyDate runat="server" id="datWorkExceptionBegDate" selecteddate='<%# Bind("WorkExceptionBegDate") %>' />
                        </td>

                        <td style="width:10%">
                             <asp:Literal ID="Literal5" runat="server" Text=" Ì‰ ÂÌ ›Ì" meta:resourcekey="Literal5Resource1"></asp:Literal>
                        </td>
                        <td colspan="2" >
                            <uc1:MyDate runat="server" id="datWorkExceptionEndDate" selecteddate='<%# Bind("WorkExceptionEndDate") %>' />
                             <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "datWorkExceptionEndDate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1" ></asp:CustomValidator>
                             <asp:CustomValidator id="ChckbalanceValidator" runat="server" 
                                                 ControlToValidate = "datWorkExceptionEndDate$TextBox1"
                                                 ErrorMessage = "·« ÌÊÃœ —’Ìœ ﬂ«›Ì"
                                                 ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource1" ></asp:CustomValidator>
                        </td>
                    </tr>

                    <tr>
                        <td >
                             <asp:Literal ID="Literal6" runat="server" Text=" Ê’› «·„Â„…:" meta:resourcekey="Literal6Resource1"></asp:Literal>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="300px" Height="70px" Text='<%# Bind("Notes") %>' meta:resourcekey="txtNotesResource1"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                        Text=" ÕœÌÀ «·≈” À‰«¡" meta:resourcekey="Button1Resource1" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server"  OnClientClick="ReloadSamepage(); return false;"
                                        Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource1" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>

        </asp:FormView>

<%--    </ContentTemplate>
</asp:UpdatePanel>--%>

<asp:ObjectDataSource ID="dsWorkException" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWorkExceptionById" TypeName="WorkExceptionDAL">
    <SelectParameters>
        <asp:Parameter Name="WorkExceptionId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="dsWorkExceptionTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOWorkExceptionType">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>



<asp:ObjectDataSource ID="dsWorkExceptions" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWorkExceptionsByEmployeeIdDataset" TypeName="ATS.BO.Framework.BOWorkException">
    <SelectParameters>
        <asp:SessionParameter Name="EmployeeId" SessionField="AccountEmployeeId" Type="Int32" />
        <asp:Parameter Name="BegDate" Type="DateTime" />
        <asp:Parameter Name="EndDate" Type="DateTime" />
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="1000" Name="PageSize" Type="Int32" />
         <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>





