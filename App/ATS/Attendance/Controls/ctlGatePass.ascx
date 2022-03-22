<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlGatePass.ascx.vb" Inherits="Attendance_Controls_ctlGatePass" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Src="../../ctlDepartmentTree.ascx" TagName="ctlDepartmentTree" TagPrefix="uc1" %>

<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/Attendance/Controls/ctlEmployeesLeaveBalance.ascx" TagPrefix="uc1" TagName="ctlEmployeesLeaveBalance" %>

<style type="text/css">
    .style2 {
        height: 24px;
        width: 34%;
    }

    .style9 {
        width: 34%;
    }
</style>

<div align="right" style="margin-bottom: 20px; margin-top: 20px; border: solid;" runat="server" id="dvSearch">
    <table class="xAdminOption" width="98%">
        <tr>
            <td>
                <asp:Literal ID="Literal6" runat="server" Text="«·ﬁ”„" meta:resourcekey="Literal18Resource1"></asp:Literal>
            </td>

            <td colspan="2">
                <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Literal ID="Literal7" runat="server" Text=" «—ÌŒ «·»œ«Ì…" meta:resourcekey="Literal17Resource1"></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Startdate" />

            </td>
            <td>
                <asp:Literal ID="Literal8" runat="server" Text=" «—ÌŒ «·‰Â«Ì…" meta:resourcekey="Literal1Resource1"></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Enddate" />
                <asp:CustomValidator ID="CustomValidator2" runat="server"
                    ControlToValidate="Enddate$TextBox1"
                    ErrorMessage="End date must be greater or equa !"
                    ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1"></asp:CustomValidator>
            </td>
        </tr>

        <tr>

            <td>
                <asp:Button ID="Button4" runat="server" OnClick="btnView"
                    Text="⁄—÷ "
                    UseSubmitBehavior="False" meta:resourcekey="btnAddResource1" />
            </td>
        </tr>

    </table>

</div>

<%-- <div align="right" style="margin-bottom:20px;margin-top:20px;border:solid;width:98%" runat="server" id="dvSearch">
        <table class="xAdminOption" width="98%" style="font-size:18px;text-align:right;">
            <tr>
                <td>
                   <asp:Literal ID="Literal18" runat="server" Text=" «·ﬁ”„:" meta:resourcekey="Literal18Resource1"></asp:Literal>
                </td>

                <td>
                    <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" />

                </td>
            </tr>

            <tr>
                <td>
                  <asp:Literal ID="Literal17" runat="server" Text=" „‰:" meta:resourcekey="Literal17Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" id="Startdate" />

                </td>
               
                <td>
                    <asp:Literal ID="Literal1" runat="server" Text=" ≈·Ï :" meta:resourcekey="Literal1Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" id="Enddate" />
                      </td>
            </tr>
         
            <tr>

                <td>
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnView"  
                                Text="⁄—÷ " 
                                UseSubmitBehavior="False" meta:resourcekey="btnAddResource1"  />
                </td>
            </tr>

        </table>

    </div>--%>


<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>

<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
    DataKeyNames="GatepassId" Caption="ﬁ«∆„… «–Ê‰«  «·„ÊŸ›Ì‰"
    SkinID="xgridviewSkinEmployee" Width="98%" CssClass="TableView" Style="text-align: center;" DataSourceID="dsGatePassesByDepartment" ShowHeaderWhenEmpty="True" meta:resourcekey="GridView2Resource1">
    <Columns>
        <asp:BoundField DataField="Gatepassid" HeaderText="„”·”·" meta:resourcekey="BoundFieldResource1" />

        <asp:BoundField DataField="EmployeeCode" HeaderText="—ﬁ„ «·»’„…" SortExpression="EmployeeCode" meta:resourcekey="BoundFieldResource2">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="EmployeeName" HeaderText="≈”„ «·„ÊŸ›" SortExpression="EmployeeName" meta:resourcekey="BoundFieldResource3">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="DepartmentName" HeaderText="≈”„ «·ﬁ”„" SortExpression="DepartmentName" meta:resourcekey="BoundFieldResource4">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="GatepassTypeName" HeaderText="‰Ê⁄ «·≈” ∆–«‰" SortExpression="GatepassTypeName" meta:resourcekey="BoundFieldResource5">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="GatepassBegTime" HeaderText="Ì»œ√ „‰" SortExpression="GatepassBegTime" meta:resourcekey="BoundFieldResource6">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="GatepassEndTime" HeaderText="Ì‰ ÂÌ ›Ì" SortExpression="GatepassEndTime" meta:resourcekey="BoundFieldResource7">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="AprovalStatus" HeaderText="Õ«·… «·ÿ·»" SortExpression="AprovalStatus" meta:resourcekey="BoundFieldResource8">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="ApprovalNotes" HeaderText="„·«ÕŸ« " SortExpression="ApprovalNotes" meta:resourcekey="BoundFieldResource9">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>


        <asp:TemplateField HeaderText=" ⁄œÌ·" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit" CausesValidation="False"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Õ–›" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
            <ItemTemplate>
                <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                    OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
        </asp:TemplateField>

    </Columns>
</x:GridView>
<%--   </ContentTemplate>
</asp:UpdatePanel>--%>

<%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(161, 2) Then%>
<table style="width: 100%">
    <tr>
        <td>&nbsp
            <asp:Button ID="btnAddGatePass" runat="server" OnClick="btnAddGatePass_Click" CausesValidation="False"
            Text="≈÷«›… ÃœÌœ" Width="75px" UseSubmitBehavior="False" meta:resourcekey="btnAddGatePassResource1" />
        </td>
    </tr>
</table>
<%End If%>


<%--        <asp:FormView ID="FormView1" runat="server"
                      SkinID="formviewskinemployee"
                      DefaultMode="Insert" Width="98%" meta:resourcekey="FormView1Resource1" >
            <InsertItemTemplate>
                <table width="100%" class="xformview">
                    <tr>
                        <th colspan="5" class="caption" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈” ∆–«‰ œÊ«„" meta:resourcekey="Literal9Resource2"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="5" class="FormViewSubHeader" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ≈” ∆–«‰ ÃœÌœ " meta:resourcekey="Literal10Resource2" />
                        </th>
                    </tr>
                    <tr>
                         <td colspan="4">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource2"  />
                        </td>
                        <td rowspan="5" colspan="2">
                            <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                        </td>
                    </tr>
                   <td style="width:150px">
                        <td>
                           <asp:Literal ID="Literal16" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal16Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGatepassId" runat="server" ReadOnly="True" meta:resourcekey="txtGatepassIdResource2" ></asp:TextBox>
                        </td>

                       
                    <tr>
                        <td >

                            <asp:Literal ID="Literal2" runat="server" Text="  «·ﬁ”„ :" meta:resourcekey="Literal2Resource1"/>
                        </td>
                        <td colspan="2" >
                           <uc1:ctlDepartmentTree  runat="server" ID="ddlDepartments" AutoPostBack="true" OnSelectedNodeChanged="ddlDepartments_SelectedIndexChanged" />
                        </td>
                    </tr>

                    <tr>
                        <td  >
                           <asp:Literal ID="Literal15" runat="server" Text=" ≈”„ «·„ÊŸ›:" meta:resourcekey="Literal15Resource1"></asp:Literal>
                        </td>
                        <td  >
                            <asp:DropDownList ID="ddlEmployees" runat="server" style="margin-right: 0px" AutoPostBack="True"
                                              DataTextField="EmployeeName" 
                                              DataValueField="EmployeeId" OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged" meta:resourcekey="ddlEmployeesResource2">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource3" Text="Select"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                        ID="rfvDDL_Product" Display="Dynamic" 
                                                        ControlToValidate="ddlEmployees"
                                                        runat="server"  Text="*" 
                                                        ErrorMessage="≈Œ — «·„ÊŸ›"
                                                        ForeColor="Red" meta:resourcekey="rfvDDL_ProductResource2"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width:10%">
                            <asp:Literal ID="Literal11" runat="server" Text="—ﬁ„ «·»’„…:" meta:resourcekey="Literal11Resource1"/>
                        </td>

                        <td>
                            <asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" meta:resourcekey="txtEmployeeCodeResource1"  />
                            <asp:Button ID="Button3" runat="server" Text="»ÕÀ" CausesValidation="False" OnClick="FindByEmployeeCode" meta:resourcekey="Button3Resource1" />
                        </td>
                    </tr>

                    <tr>
                        <td >
                         <asp:Literal ID="Literal14" runat="server" Text=" ‰Ê⁄ «·≈” ∆–«‰:" meta:resourcekey="Literal14Resource1"></asp:Literal>
                        </td>
                        <td colspan="2" >
                            <asp:DropDownList ID="ddlGatepassTypeId" runat="server" style="margin-right: 0px" Width="200px" 
                                              DataSourceID="dsGatePassTypes" 
                                              DataTextField="GatepassTypeName" 
                                              DataValueField="GatepassTypeId" 
                                              SelectedValue='<%# Bind("GatepassTypeId") %>' meta:resourcekey="ddlGatepassTypeIdResource2">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource4" Selected="True" Text="select"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                        ID="RequiredFieldValidator1" Display="Dynamic" 
                                                        ControlToValidate="ddlGatepassTypeId"
                                                        runat="server"  Text="*" 
                                                        ErrorMessage="≈Œ — ‰Ê⁄ «·≈” ∆–«‰"
                                                        ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td >
                           <asp:Literal ID="Literal13" runat="server" Text=" «· «—ÌŒ:" meta:resourcekey="Literal13Resource1"></asp:Literal>
                        </td>
                        <td>
                            <uc1:MyDate runat="server" id="datGatepassBegDate" SelectedDate="<%# Now.Date %>" />
                        </td>

                        <td colspan="2" >
                            <uc1:MyDate runat="server" id="datGatepassEndDate" SelectedDate="<%# Now.Date %>" Visible="False"/>
                             
                        </td>
                    </tr>

                    <tr>
                        <td >
                           <asp:Literal ID="Literal12" runat="server" Text=" „‰ «·”«⁄…:" meta:resourcekey="Literal12Resource1"></asp:Literal>
                        </td>
                        <td>
                             <telerik:radtimepicker ID="timGatepassBegTime" Runat="server"  >
                            </telerik:radtimepicker>
                            <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="timGatepassBegTime"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource2"></asp:RequiredFieldValidator>
                           
                        </td>

                        <td >
                            <asp:Literal ID="Literal8" runat="server" Text=" ≈·Ï «·”«⁄…:" meta:resourcekey="Literal8Resource1"></asp:Literal>
                        </td>
                        <td  >
                              <telerik:radtimepicker ID="timGatepassEndTime" Runat="server" >
                            </telerik:radtimepicker>
                            <asp:CustomValidator id="ChckbalanceValidator" runat="server" 
                                                 ControlToValidate = "datGatepassBegDate$TextBox1"
                                                 ErrorMessage = "·« ÌÊÃœ —’Ìœ ﬂ«›Ì"
                                                 ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource2" ></asp:CustomValidator>
                        </td>
                        </tr>

                    <tr>
                         <td colspan="5">
                            <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator5" ControlToValidate="timGatepassEndTime"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator5Resource2"></asp:RequiredFieldValidator>
                             <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="timGatepassEndTime"
                                                  ControlToCompare="timGatepassBegTime" Operator="GreaterThan" ErrorMessage="«·—Ã«¡ «· Õﬁﬁ „‰ Êﬁ  «·«” ∆–«‰." meta:resourcekey="CompareValidator1Resource2"></asp:CompareValidator>
                             <asp:CustomValidator id="ChckLimitsValidator" runat="server" 
                                                 ControlToValidate = "datGatepassBegDate$TextBox1"
                                                 ErrorMessage = "ÌÃ» «‰ ·«  ﬁ· «·„œ… ⁄‰ ‰’› ”«⁄… Ê·«  “Ìœ ⁄‰ 3 ”«⁄« "
                                                 ClientValidationFunction="checklimits" meta:resourcekey="ChckLimitsValidatorResource2" ></asp:CustomValidator>
                        </td>
                    </tr>

                    <tr>
                        <td >
                           <asp:Literal ID="Literal1" runat="server" Text=" „·«ÕŸ« :" meta:resourcekey="Literal1Resource3"></asp:Literal>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" style="margin-right: 0px" Width="300px" Height="100px" Text='<%# Bind("Notes") %>' meta:resourcekey="txtNotesResource2"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                        Text="Õ›Ÿ «·≈” ∆–«‰" meta:resourcekey="Button1Resource2" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server"  OnClientClick="ReloadSamepage(); return false;" CausesValidation="False"
                                        Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource2" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>

            <EditItemTemplate>
                <table width="100%" class="xformview" style="font-size:16px;">
                    <tr>
                        <th colspan="5" class="caption" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈” ∆–«‰ œÊ«„" meta:resourcekey="Literal9Resource1"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="5" class="FormViewSubHeader" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ≈” ∆–«‰ ÃœÌœ " meta:resourcekey="Literal10Resource1" />
                        </th>
                    </tr>
                    <tr>
                         <td colspan="4">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1"  />
                        </td>
                        <td rowspan="5" colspan="2">
                            <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:150px">
                           <asp:Literal ID="Literal7" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal7Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGatepassId" runat="server" ReadOnly="True" Text='<%# Bind("GatepassId") %>' meta:resourcekey="txtGatepassIdResource1"></asp:TextBox>
                        </td>
                       
                    </tr>

                    <tr>
                        <td >
                           <asp:Literal ID="Literal6" runat="server" Text=" ≈”„ «·„ÊŸ›:" meta:resourcekey="Literal6Resource1"></asp:Literal>
                        </td>
                        <td >
                            <asp:DropDownList ID="ddlEmployees" runat="server" style="margin-right: 0px" Enabled="False"
                                           
                                              DataTextField="EmployeeName" 
                                              DataValueField="EmployeeId" meta:resourcekey="ddlEmployeesResource1" >
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="Select"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                        ID="rfvDDL_Product" Display="Dynamic" 
                                                        ControlToValidate="ddlEmployees"
                                                        runat="server"  Text="*" 
                                                        ErrorMessage="≈Œ — «·„ÊŸ›"
                                                        ForeColor="Red" meta:resourcekey="rfvDDL_ProductResource1"></asp:RequiredFieldValidator>
                        </td>

                        <td >
                           <asp:Literal ID="Literal5" runat="server" Text="‰Ê⁄ «·≈” ∆–«‰:" meta:resourcekey="Literal5Resource1"></asp:Literal>
                        </td>
                        <td >
                            <asp:DropDownList ID="ddlGatepassTypeId" runat="server" style="margin-right: 0px" Width="200px" 
                                              DataSourceID="dsGatePassTypes" 
                                              DataTextField="GatepassTypeName" 
                                              DataValueField="GatepassTypeId" 
                                              SelectedValue='<%# Bind("GatepassTypeId") %>' meta:resourcekey="ddlGatepassTypeIdResource1">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource2" Selected="True" Text="....≈Œ —...."></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                        ID="RequiredFieldValidator5" Display="Dynamic" 
                                                        ControlToValidate="ddlEmployees"
                                                        runat="server"  Text="*" 
                                                        ErrorMessage="≈Œ — ‰Ê⁄ «·≈” ∆–«‰"
                                                        ForeColor="Red" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                   <tr>
                        <td >
                           <asp:Literal ID="Literal4" runat="server" Text=" «· «—ÌŒ:" meta:resourcekey="Literal4Resource1"></asp:Literal>
                        </td>
                        <td>
                           <uc1:MyDate runat="server" id="datGatepassBegDate" selecteddate='<%# Bind("GatepassBegDate") %>'/>
                       
                        </td>

                        <td>
                            <uc1:MyDate runat="server" id="datGatepassEndDate" selecteddate='<%# Bind("GatepassBegDate") %>' Visible="False"/>
                            
                        </td>
                    </tr>

                    <tr>
                        <td >
                           <asp:Literal ID="Literal3" runat="server" Text=" „‰ «·”«⁄…:" meta:resourcekey="Literal3Resource1"></asp:Literal>
                        </td>
                        <td>
                             <telerik:radtimepicker ID="timGatepassBegTime" Runat="server" MinDate="1899-01-01" selecteddate='<%# Bind("GatepassBegTime")%>' >
                            </telerik:radtimepicker>
                            <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="timGatepassBegTime"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>

                            
                        </td>

                        <td >
                            <asp:Literal ID="Literal1" runat="server" Text=" ≈·Ï «·”«⁄…:" meta:resourcekey="Literal1Resource2"></asp:Literal>
                           
                        </td>
                        <td>
                              <telerik:radtimepicker ID="timGatepassEndTime" Runat="server" MinDate="1899-01-01" selecteddate='<%# Bind("GatepassEndTime")%>'>
                            </telerik:radtimepicker>
                             <asp:CustomValidator id="ChckbalanceValidator" runat="server" 
                                                 ControlToValidate = "datGatepassBegDate$TextBox1"
                                                 ErrorMessage = "·« ÌÊÃœ —’Ìœ ﬂ«›Ì"
                                                 ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource1" ></asp:CustomValidator>
                        </td>
                        </tr>

                    <tr>
                         <td colspan="5">
                            <asp:RequiredFieldValidator Font-Size="Small"  runat="server" ID="RequiredFieldValidator1" ControlToValidate="timGatepassEndTime"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                             <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="timGatepassEndTime"
                                                  ControlToCompare="timGatepassBegTime" Operator="GreaterThan" ErrorMessage="«·—Ã«¡ «· Õﬁﬁ „‰ Êﬁ  «·«” ∆–«‰." meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                             <asp:CustomValidator id="ChckLimitsValidator" runat="server" 
                                                 ControlToValidate = "datGatepassBegDate$TextBox1"
                                                 ErrorMessage = "ÌÃ» «‰ ·«  ﬁ· «·„œ… ⁄‰ ‰’› ”«⁄… Ê·«  “Ìœ ⁄‰ 3 ”«⁄« "
                                                 ClientValidationFunction="checklimits" meta:resourcekey="ChckLimitsValidatorResource1" ></asp:CustomValidator>
                        </td>
                    </tr>

                    <tr>
                         <td >
                           <asp:Literal ID="Literal19" runat="server" Text=" „·«ÕŸ« :" meta:resourcekey="Literal1Resource3"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" style="margin-right: 0px" Width="300px" Height="100px" Text='<%# Bind("Notes") %>' meta:resourcekey="txtNotesResource1"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                        Text=" ÕœÌÀ «·≈” ∆–«‰" meta:resourcekey="Button1Resource1" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" CausesValidation="False"
                                        Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource1" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>

        </asp:FormView>--%>

<asp:FormView ID="FormView1" runat="server"
    SkinID="formviewskinemployee"
    DefaultMode="Insert" Width="98%">
    <InsertItemTemplate>
        <table width="100%" class="xformview">
            <tr>
                <th colspan="5" class="caption" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈” ∆–«‰ œÊ«„" meta:resourcekey="Literal9Resource2" />
                </th>
            </tr>
            <tr>
                <th colspan="5" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ≈” ∆–«‰ ÃœÌœ " meta:resourcekey="Literal10Resource2" />
                </th>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" />
                </td>
                <td rowspan="5" colspan="2">
                    <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    <asp:Literal ID="Literal16" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal16Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtGatepassId" runat="server" ReadOnly="true"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>

                    <asp:Literal ID="Literal2" runat="server" Text="  «·ﬁ”„ :" meta:resourcekey="Literal2Resource1" />
                </td>
                <td colspan="2">
                    <uc1:ctlDepartmentTree runat="server" ID="ddlDepartments" AutoPostBack="true" OnSelectedNodeChanged="ddlDepartments_SelectedIndexChanged" />
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal15" runat="server" Text=" ≈”„ «·„ÊŸ›:" meta:resourcekey="Literal15Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmployees" runat="server" Style="margin-right: 0px" AutoPostBack="true"
                        DataTextField="EmployeeName"
                        DataValueField="EmployeeId" OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="rfvDDL_Product" Display="Dynamic"
                        ControlToValidate="ddlEmployees"
                        runat="server" Text="*"
                        ErrorMessage="≈Œ — «·„ÊŸ›"
                        ForeColor="Red" meta:resourcekey="rfvDDL_ProductResource2"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 10%">
                    <asp:Literal ID="Literal11" runat="server" Text="—ﬁ„ «·»’„…:" meta:resourcekey="Literal11Resource1" />
                </td>

                <td>
                    <asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" />
                    <asp:Button ID="Button3" runat="server" Text="»ÕÀ" CausesValidation="False" OnClick="FindByEmployeeCode" meta:resourcekey="Button3Resource1" />
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal14" runat="server" Text=" ‰Ê⁄ «·≈” ∆–«‰:" meta:resourcekey="Literal14Resource1"></asp:Literal>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlGatepassTypeId" runat="server" Style="margin-right: 0px" Width="200px"
                        DataSourceID="dsGatePassTypes"
                        DataTextField="GatepassTypeName"
                        DataValueField="GatepassTypeId"
                        SelectedValue='<%# Bind("GatepassTypeId")%> '>
                        <asp:ListItem Value="0">select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="RequiredFieldValidator1" Display="Dynamic"
                        ControlToValidate="ddlGatepassTypeId"
                        runat="server" Text="*"
                        ErrorMessage="≈Œ — ‰Ê⁄ «·≈” ∆–«‰"
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal13" runat="server" Text=" «· «—ÌŒ:" meta:resourcekey="Literal13Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" ID="datGatepassBegDate" SelectedDate="<%# Now.Date%>" />
                </td>

                <%--<td >
                            ≈·Ï  «—ÌŒ:
                        </td>--%>
                <td colspan="2">
                    <uc1:MyDate runat="server" ID="datGatepassEndDate" SelectedDate="<%# Now.Date%>" Visible="false" />
                    <%-- <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "datGatepassEndDate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" >
                            </asp:CustomValidator>--%>
                             
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal12" runat="server" Text=" „‰ «·”«⁄…:" meta:resourcekey="Literal12Resource1"></asp:Literal>
                </td>
                <td>
                    <telerik:RadTimePicker ID="timGatepassBegTime" runat="server">
                        <TimeView CellSpacing="-1" Columns="4" Interval="00:15:00" StartTime="05:00:00" EndTime="18:00:00">
                        </TimeView>
                    </telerik:RadTimePicker>
                    <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="timGatepassBegTime"
                        ErrorMessage="*">

                    </asp:RequiredFieldValidator>

                </td>

                <td>
                    <asp:Literal ID="Literal8" runat="server" Text=" ≈·Ï «·”«⁄…:" meta:resourcekey="Literal8Resource1"></asp:Literal>
                </td>
                <td>
                    <telerik:RadTimePicker ID="timGatepassEndTime" runat="server">
                        <TimeView CellSpacing="-1" Columns="4" Interval="00:15:00" StartTime="05:00:00" EndTime="18:00:00">
                        </TimeView>
                    </telerik:RadTimePicker>
                    <asp:CustomValidator ID="ChckbalanceValidator" runat="server"
                        ControlToValidate="datGatepassBegDate$TextBox1"
                        ErrorMessage="·« ÌÊÃœ —’Ìœ ﬂ«›Ì"
                        ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource2"></asp:CustomValidator>
                </td>
            </tr>

            <tr>
                <td colspan="5">
                    <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator5" ControlToValidate="timGatepassEndTime"
                        ErrorMessage="*">

                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator3" ControlToValidate="timGatepassEndTime"
                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator5Resource2"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="timGatepassEndTime"
                        ControlToCompare="timGatepassBegTime" Operator="GreaterThan" ErrorMessage="«·—Ã«¡ «· Õﬁﬁ „‰ Êﬁ  «·«” ∆–«‰." meta:resourcekey="CompareValidator1Resource2"></asp:CompareValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal1" runat="server" Text=" „·«ÕŸ« :" meta:resourcekey="Literal1Resource3"></asp:Literal>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Style="margin-right: 0px" Width="300px" Height="100px" Text='<%# Bind("Notes")%>'></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                        Text="Õ›Ÿ «·≈” ∆–«‰" meta:resourcekey="Button1Resource2" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" CausesValidation="false"
                        Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource2" />
                </td>
            </tr>
        </table>
    </InsertItemTemplate>

    <EditItemTemplate>
        <table width="100%" class="xformview">
            <tr>
                <th colspan="5" class="caption" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈” ∆–«‰ œÊ«„" meta:resourcekey="Literal9Resource1" />
                </th>
            </tr>
            <tr>
                <th colspan="5" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ≈” ∆–«‰ ÃœÌœ " meta:resourcekey="Literal10Resource1" />
                </th>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" />
                </td>
                <td rowspan="5" colspan="2">
                    <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal7" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal7Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtGatepassId" runat="server" ReadOnly="true" Text='<%# Bind("GatepassId")%>'></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal6" runat="server" Text=" ≈”„ «·„ÊŸ›:" meta:resourcekey="Literal6Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmployees" runat="server" Style="margin-right: 0px" Enabled="false"
                        DataTextField="EmployeeName"
                        DataValueField="EmployeeId">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="rfvDDL_Product" Display="Dynamic"
                        ControlToValidate="ddlEmployees"
                        runat="server" Text="*"
                        ErrorMessage="≈Œ — «·„ÊŸ›"
                        ForeColor="Red" meta:resourcekey="rfvDDL_ProductResource1"></asp:RequiredFieldValidator>
                </td>

                <td>
                    <asp:Literal ID="Literal5" runat="server" Text="‰Ê⁄ «·≈” ∆–«‰:" meta:resourcekey="Literal5Resource1"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGatepassTypeId" runat="server" Style="margin-right: 0px" Width="200px"
                        DataSourceID="dsGatePassTypes"
                        DataTextField="GatepassTypeName"
                        DataValueField="GatepassTypeId"
                        SelectedValue='<%# Bind("GatepassTypeId")%> '>
                        <asp:ListItem Value="0">....≈Œ —....</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="RequiredFieldValidator5" Display="Dynamic"
                        ControlToValidate="ddlGatepassTypeId"
                        runat="server" Text="*"
                        ErrorMessage="≈Œ — ‰Ê⁄ «·≈” ∆–«‰"
                        ForeColor="Red" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal4" runat="server" Text=" «· «—ÌŒ:" meta:resourcekey="Literal4Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" ID="datGatepassBegDate" SelectedDate='<%# Bind("GatepassBegDate")%>' />

                </td>

                <%-- <td >
                            ≈·Ï  «—ÌŒ:
                        </td>--%>
                <td>
                    <uc1:MyDate runat="server" ID="datGatepassEndDate" SelectedDate='<%# Bind("GatepassBegDate")%>' Visible="false" />
                    <%-- <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "datGatepassEndDate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" >
                            </asp:CustomValidator>--%>
                            
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal3" runat="server" Text=" „‰ «·”«⁄…:" meta:resourcekey="Literal3Resource1"></asp:Literal>
                </td>
                <td>
                    <telerik:RadTimePicker ID="timGatepassBegTime" runat="server" MinDate="1899-01-01" SelectedDate='<%# Bind("GatepassBegTime")%>'>
                        <TimeView CellSpacing="-1" Columns="4" Interval="00:15:00" StartTime="05:00:00" EndTime="18:00:00">
                        </TimeView>
                    </telerik:RadTimePicker>
                    <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="timGatepassBegTime"
                        ErrorMessage="*">

                    </asp:RequiredFieldValidator>


                </td>

                <td>
                    <asp:Literal ID="Literal1" runat="server" Text=" ≈·Ï «·”«⁄…:" meta:resourcekey="Literal1Resource2"></asp:Literal>
                </td>
                <td>
                    <telerik:RadTimePicker ID="timGatepassEndTime" runat="server" MinDate="1899-01-01" SelectedDate='<%# Bind("GatepassEndTime")%>'>
                        <TimeView CellSpacing="-1" Columns="4" Interval="00:15:00" StartTime="05:00:00" EndTime="18:00:00">
                        </TimeView>
                    </telerik:RadTimePicker>

                </td>
            </tr>

            <tr>
                <td colspan="5">
                    <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator1" ControlToValidate="timGatepassEndTime"
                        ErrorMessage="*">

                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="timGatepassEndTime"
                        ControlToCompare="timGatepassBegTime" Operator="GreaterThan" ErrorMessage="«·—Ã«¡ «· Õﬁﬁ „‰ Êﬁ  «·«” ∆–«‰." meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Literal ID="Literal19" runat="server" Text=" „·«ÕŸ« :" meta:resourcekey="Literal1Resource3"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Style="margin-right: 0px" Width="300px" Height="100px" Text='<%# Bind("Notes")%>'></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                        Text=" ÕœÌÀ «·≈” ∆–«‰" meta:resourcekey="Button1Resource1" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" CausesValidation="false"
                        Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource1" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>

</asp:FormView>




<asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Larger" ForeColor="#FF3300" Visible="False" meta:resourcekey="Label2Resource3"></asp:Label>


<asp:ObjectDataSource ID="dsGatePassTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOGatePassType">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>













<asp:ObjectDataSource ID="dsDepartments" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDepartmentsList" TypeName="LookUp"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="dsGatePassesByDepartment" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDepartmentGatepassDateDataset" TypeName="ATS.BO.Framework.BOGatepass">
    <SelectParameters>
        <asp:Parameter Name="DepartmentId" Type="Int32" />
        <asp:Parameter Name="BegDate" Type="DateTime" />
        <asp:Parameter Name="EndDate" Type="DateTime" />
        <asp:Parameter Name="OptionNo" Type="Int32" />
         <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>




<asp:ObjectDataSource ID="dsEmployees" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetEmployeesListByDepartment" TypeName="LookUp">
    <SelectParameters>
        <asp:SessionParameter Name="DepartmentId" SessionField="AccountDepartmentId1" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>


