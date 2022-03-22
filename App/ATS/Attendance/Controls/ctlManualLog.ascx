<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlManualLog.ascx.vb" Inherits="Attendance_Controls_ctlManualLog" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register src="../../ctlDepartmentTree.ascx" tagname="ctlDepartmentTree" tagprefix="uc1" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>



    <div align="right" style="margin-bottom:20px;margin-top:20px;border:solid;" runat="server" id="dvSearch">
        <table class="xAdminOption" width="98%" >
            <tr>
                <td>
                   <asp:Literal ID="Literal6" runat="server" Text="ÇáÞÓã" meta:resourcekey="Literal6Resource1"></asp:Literal>
                </td>

                <td colspan="2">
                    <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" />
                </td>
            </tr>

            <tr>
                <td>
                     <asp:Literal ID="Literal7" runat="server" Text="ÊÇÑíÎ ÇáÈÏÇíÉ" meta:resourcekey="Literal7Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" id="Startdate" />
                    
                </td>
                <td>
                      <asp:Literal ID="Literal8" runat="server" Text="ÊÇÑíÎ ÇáäåÇíÉ" meta:resourcekey="Literal8Resource1"></asp:Literal>
                </td>
                <td>
                    <uc1:MyDate runat="server" id="Enddate" />
                     <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "Enddate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1" ></asp:CustomValidator>
                </td>
            </tr>

            <tr>

                <td>
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnView"  
                                Text="ÚÑÖ " 
                                UseSubmitBehavior="False" meta:resourcekey="btnAddResource1" />
                </td>
            </tr>

        </table>

    </div>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="LogId" Caption="ÞÇÆãÉ ÇáÓÌáÇÊ ÇáãÏÎáÉ íÏæíÇ" 
            SkinID="xgridviewSkinEmployee" Width="98%"   ShowHeaderWhenEmpty="True" DataSourceID="dsManualLogs" meta:resourcekey="GridView2Resource1">
            <Columns>
               

            <asp:BoundField DataField="LogId" HeaderText="ãÓáÓá" meta:resourcekey="BoundFieldResource1"/>
    
             
            <asp:BoundField DataField="EmployeeName" HeaderText="ÅÓã ÇáãæÙÝ"  SortExpression="EmployeeName" meta:resourcekey="BoundFieldResource2" >
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle HorizontalAlign="Center"  />
            </asp:BoundField>

            <asp:BoundField DataField="DepartmentName" HeaderText="ÇáÞÓã"  SortExpression="DepartmentName" meta:resourcekey="BoundFieldResource3" >
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle HorizontalAlign="Center"  />
            </asp:BoundField>  

            <asp:BoundField DataField="LogType" HeaderText="äæÚ ÇáÍÑßÉ"  SortExpression="LogType" meta:resourcekey="BoundFieldResource4" >
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle HorizontalAlign="Center"  />
            </asp:BoundField> 
                   
             <asp:BoundField DataField="LogDate" HeaderText="ÊÇÑíÎ ÇáÓÌá"  SortExpression="LogDate" meta:resourcekey="BoundFieldResource5" >
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle HorizontalAlign="Center"  />
            </asp:BoundField> 

             <asp:BoundField DataField="LogTime" HeaderText="æÞÊ ÇáÓÌá"  SortExpression="LogTime" meta:resourcekey="BoundFieldResource6" >
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle HorizontalAlign="Center"  />
            </asp:BoundField>  

         
             <asp:BoundField DataField="AddedBy" HeaderText="ÇÓã ÇáãÓÊÎÏã"  SortExpression="AddedBy" meta:resourcekey="BoundFieldResource7" >
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle HorizontalAlign="Center"  />
            </asp:BoundField>  


               <%-- <asp:TemplateField HeaderText="ÊÚÏíá" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:LinkButton id="EditLinkButton" runat="server" Text="" CommandName="Edit" CausesValidation="False"  ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" cssclass="edit_button" HorizontalAlign="Center" />
                </asp:TemplateField>--%>
                   
                 <asp:TemplateField HeaderText="ÍÐÝ" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                         <asp:LinkButton id="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                         OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False" ></asp:LinkButton>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="1%" cssclass="delete_button" />
                </asp:TemplateField>

            </Columns>
        </x:GridView>
   </ContentTemplate>
</asp:UpdatePanel>
  <asp:Button ID="btnAddVacation" runat="server"   
                            Text="ÅÖÇÝÉ ÌÏíÏ" 
                            UseSubmitBehavior="False" CausesValidation = "False" meta:resourcekey="btnAddVacationResource1" />
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="LookUp" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDepartmentsList">
</asp:ObjectDataSource>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>

        <asp:FormView ID="FormView1" runat="server"
                      SkinID="formviewskinemployee"
                      DefaultMode="Insert" Width="98%" BorderStyle="Solid"  meta:resourcekey="FormView1Resource1" >
            <InsertItemTemplate>
                 <table width="100%" class="xformview">
                      <tr>
                        <th colspan="5" class="caption" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÍÑßÉ ãæÙÝ" meta:resourcekey="Literal9Resource2"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="5" class="FormViewSubHeader" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal10" runat="server" Text="ÊÓÌíá ÍÑßÉ ÈÕãÉ ÌÏíÏÉ" meta:resourcekey="Literal10Resource2" />
                        </th>
                    </tr>
                     <tr>
                       <td colspan="2">
                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource2"  />
                       </td>
                  </tr>

                    <tr>
                        <td style="width:10%">
                            <asp:Literal ID="Literal1" runat="server" Text=" ÑÞã ÇáÍÇÓÈ:" meta:resourcekey="Literal1Resource2"/>
                           </td>
                        <td style="width:15%">
                            <asp:TextBox ID="txtLogId" runat="server" ReadOnly="True" Text='<%# Bind("LogId") %>' meta:resourcekey="txtLogIdResource2"></asp:TextBox>
                           
                        </td>
                        
                      
                    </tr>
                    <tr>
                        <td >
                           
                                  <asp:Literal ID="Literal2" runat="server" Text="  ÇáÞÓã :" meta:resourcekey="Literal2Resource1"/>
                        </td>
                        <td  colspan="2">
                              <uc1:ctlDepartmentTree runat="server" ID="ddlDepartments" AutoPostBack="true" OnSelectedNodeChanged="ddlDepartments_SelectedIndexChanged" />

                        </td>
                    </tr>

                    <tr>
                        <td >
                             <asp:Literal ID="Literal3" runat="server" Text="  ÅÓã ÇáãæÙÝ :" meta:resourcekey="Literal3Resource2"/>
                         
                        </td>
                        <td  >
                            <asp:DropDownList ID="ddlEmployees" runat="server" style="margin-right: 0px"  
                                            
                                              DataTextField="EmployeeName" 
                                              DataValueField="EmployeeId" meta:resourcekey="ddlEmployeesResource2" >
                                           
                            </asp:DropDownList>
                        </td>
                        <td style="width:10%">
                            <asp:Literal ID="Literal11" runat="server" Text="ÑÞã ÇáÈÕãÉ:" meta:resourcekey="Literal11Resource1"/>
                        </td>

                        <td>
                            <asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" meta:resourcekey="txtEmployeeCodeResource1"  />
                            <asp:Button ID="Button3" runat="server" Text="ÈÍË" CausesValidation="False" OnClick="FindByEmployeeCode" meta:resourcekey="Button3Resource1" />
                        </td>
                        </tr>
                     <tr>
                        <td >
                           <asp:Literal ID="Literal4" runat="server" Text="  äæÚ ÇáÍÑßÉ :" meta:resourcekey="Literal4Resource2"/>
                        </td>
                        <td  >
                           <asp:DropDownList ID="ddlLogTypeId" runat="server" AppendDataBoundItems="True"
                                SelectedValue='<%# Bind("LogTypeId") %>' meta:resourcekey="ddlLogTypeIdResource2">
                              
                               <asp:ListItem Text="ÏÎæá" Value="1" meta:resourcekey="ListItemResource4" Selected="True" />
                              <asp:ListItem Text="ÎÑæÌ" Value="2" meta:resourcekey="ListItemResource5" />
                                  
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td >
                          <asp:Literal ID="Literal5" runat="server" Text="ÊÇÑíÎ ÇáÍÑßÉ :" meta:resourcekey="Literal5Resource2"/>
                        </td>
                        <td>
                             <uc1:MyDate runat="server" id="datLogDate" SelectedDate="<%# Now.Date %>" />
                           
                        </td>

                        </tr>
                     <tr>
                         <td >
                          <asp:Literal ID="Literal88" runat="server" Text="æÞÊ ÇáÍÑßÉ :" meta:resourcekey="Literal88Resource2"/>
                        </td>
                         <td>
                              <telerik:radtimepicker ID="datLogTime" Runat="server" MinDate="1899-01-01" selecteddate='<%# Now%>'>
                            </telerik:radtimepicker>
                             <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="datLogTime"
                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource2"></asp:RequiredFieldValidator>
                         </td>
                     </tr>

                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                        Text="ÍÝÙ ÇáÍÑßÉ" meta:resourcekey="Button1Resource2" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" CausesValidation="False"
                                        Text="ÅáÛÇÁ ÇáÃãÑ" meta:resourcekey="Button2Resource2" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>

            <EditItemTemplate>
              <table width="100%" class="xformview">
                      <tr>
                        <th colspan="3" class="caption" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="ÈíÇäÇÊ ÊÓÌíá íÏæí ááÈÕãÉ" meta:resourcekey="Literal9Resource1"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="3" class="FormViewSubHeader" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal10" runat="server" Text="ÊÍÏíË ÈíÇäÇÊ ÓÌá íÏæí" meta:resourcekey="Literal10Resource1" />
                        </th>
                    </tr>
                  <tr>
                       <td colspan="2">
                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1"  />
                       </td>
                  </tr>
                    <tr>
                        <td style ="width:10%">
                            <asp:Literal ID="Literal1" runat="server" Text=" ÑÞã ÇáÍÇÓÈ:" meta:resourcekey="Literal1Resource1"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtlogId" runat="server" ReadOnly="True" Text='<%# Bind("LogId") %>' meta:resourcekey="txtlogIdResource1"></asp:TextBox>
                        </td>
                       
                    </tr>
                   

                    <tr>
                        <td >
                             <asp:Literal ID="Literal3" runat="server" Text="  ÅÓã ÇáãæÙÝ :" meta:resourcekey="Literal3Resource1"/>
                         
                        </td>
                        <td colspan="2" >
                            <asp:DropDownList ID="ddlEmployees" runat="server" style="margin-right: 0px" Enabled="False"
                                           
                                              DataTextField="EmployeeName" 
                                              DataValueField="EmployeeId" meta:resourcekey="ddlEmployeesResource1" >
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                        ID="rfvDDL_Product" Display="Dynamic" 
                                                        ControlToValidate="ddlEmployees"
                                                        runat="server"  Text="*" 
                                                        ErrorMessage="ÅÎÊÑ ÇáãæÙÝ"
                                                        ForeColor="Red" meta:resourcekey="rfvDDL_ProductResource1"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
 <tr>
                        <td >
                           <asp:Literal ID="Literal4" runat="server" Text="  äæÚ ÇáÍÑßÉ :" meta:resourcekey="Literal4Resource1"/>
                        </td>
                        <td  >
                           <asp:DropDownList ID="ddlLogTypeId" runat="server" AppendDataBoundItems="True"
                                SelectedValue='<%# Bind("LogTypeId") %>' meta:resourcekey="ddlLogTypeIdResource1">
                              
                               <asp:ListItem Text="ÏÎæá" Value="1" meta:resourcekey="ListItemResource2" Selected="True" />
                              <asp:ListItem Text="ÎÑæÌ" Value="2" meta:resourcekey="ListItemResource3" />
                                  
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td >
                          <asp:Literal ID="Literal5" runat="server" Text="ÊÇÑíÎ ÇáÍÑßÉ :" meta:resourcekey="Literal5Resource1"/>
                        </td>
                        <td>
                            <uc1:MyDate runat="server" id="datLogDate" selecteddate='<%# Bind("LogDate") %>' />
                          
                        </td>

                        </tr>
                     <tr>
                         <td >
                          <asp:Literal ID="Literal88" runat="server" Text="æÞÊ ÇáÍÑßÉ :" meta:resourcekey="Literal88Resource1"/>
                        </td>
                         <td>
                              <telerik:radtimepicker ID="datLogTime" Runat="server" MinDate="1899-01-01" selecteddate='<%# Bind("LogTime")%>'>
                            </telerik:radtimepicker>
                             
                             <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="datLogTime"
                                                        ErrorMessage="ÅÏÎá æÞÊ ÇáÍÑßÉ!" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                        Text="ÊÍÏíË ÇáÅÌÇÒÉ" meta:resourcekey="Button1Resource1" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server"  OnClientClick="ReloadSamepage(); return false;" CausesValidation="False"
                                        Text="ÅáÛÇÁ ÇáÃãÑ" meta:resourcekey="Button2Resource1" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>

        </asp:FormView>

<%--    </ContentTemplate>
</asp:UpdatePanel>--%>

<asp:ObjectDataSource ID="dsLog" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetLogFromMachineLogByID" TypeName="AttendanceDAL">
    <SelectParameters>
        <asp:Parameter Name="LogId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>





<asp:ObjectDataSource ID="dsManualLogs" runat="server" OldValuesParameterFormatString="original_{0}"
                              SelectMethod="GetManualLogsFromMachineLog" TypeName="ATS.BO.Framework.BOMachineLog" >
            <SelectParameters>
                <asp:Parameter Name="DepartmentID" Type="Int32" />
                <asp:Parameter Name="BegDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        






        
        
        


        




