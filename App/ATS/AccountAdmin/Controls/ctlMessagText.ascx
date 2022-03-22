<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMessagText.ascx.vb" Inherits="Attendance_Controls_ctlMessegeText" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>






<style type="text/css">
    .style1
    {
        height: 23px;
    }
</style>

                            
                            
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>

        <asp:FormView ID="FormView1" runat="server"
                      SkinID="formviewskinemployee"
                      DefaultMode="Insert" Width="50%" BorderStyle="Solid"  Visible="False" >
            <InsertItemTemplate>
                 <table width="100%" class="xformview">
                      <tr>
                        <th colspan="4" class="caption" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="≈‘⁄«— €Ì«» «Ê  √ŒÌ—"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="4" class="FormViewSubHeader" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· «‘⁄«— €Ì«» «Ê  √ŒÌ— ÃœÌœ" />
                        </th>
                    </tr>
                     <tr>
                         <td colspan ="4">
                             <asp:Label ID="Label2" runat ="server" ></asp:Label>
                         </td>
                     </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text=" «”„ «·„ÊŸ› :"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmpNameReceiveMessage" runat="server" ReadOnly="true" Text='<%# Bind("HolidayId")%>'></asp:TextBox>
                        </td>
                    </tr>
                    

                    <tr>
                        <td >
                             <asp:Literal ID="Literal3" runat="server" Text="  ‰Ê⁄ «·—”«·Â :"/>
                         
                        </td>
                       <td>
                            <asp:DropDownList ID="DDLMessageType" runat="server" >
                                <asp:ListItem>—”«·… €Ì«»</asp:ListItem>
                                <asp:ListItem>—”«·…  √ŒÌ— </asp:ListItem>
                                <asp:ListItem>ﬁ»Ê· «Ã«“…</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         </tr>
                        <tr>
                        <td>
                            <asp:Literal ID="literal4" runat="server" Text="‰’ «·—”«·Â:" />

                        </td>
                            <td>
                                <asp:TextBox ID="txtMessage" runat="server" Text=" " />
                              </td>
                    </tr>
                        <tr>
                            <td>
                            
                            </td>
                        </tr>
                     
                   

                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                        Text="Õ›Ÿ ‰„Ê–Ã «·—”«·Â" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" CommandName="Cancel" OnClientClick="ReloadSamepage(); return false;"
                                        Text="≈·€«¡ «·√„—" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>

            <EditItemTemplate>
              <table width="100%" class="xformview">
                      <tr>
                        <th colspan="4" class="caption" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈Ã«“… „ÊŸ›"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="4" class="FormViewSubHeader" style="text-align:center;font-size:14px;">
                            <asp:Literal ID="Literal10" runat="server" Text=" ÕœÌÀ »Ì«‰«  ≈Ã«“…" />
                        </th>
                    </tr>
                      <tr>
                         <td colspan ="4">
                             <asp:Label ID="Label2" runat ="server" ></asp:Label>
                         </td>
                     </tr>
                   <tr>
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text="  ”·”·:"/>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtHolidayId" runat="server" ReadOnly="true" Text='<%# Bind("HolidayId")%>'></asp:TextBox>
                        </td>
                    </tr>
                    

                    <tr>
                        <td >
                             <asp:Literal ID="Literal3" runat="server" Text="  ≈”„ «·≈Ã«“… :"/>
                         
                        </td>
                       <td>
                            <asp:TextBox ID="txtHolidayName" runat="server"  Text='<%# Eval("HolidayName")%>'></asp:TextBox>
                        </td>
                        </tr>
                     

                    <tr>
                        <td >
                          <asp:Literal ID="Literal5" runat="server" Text="   »œ√ ›Ì :"/>
                        </td>
                        <td>
                            <uc1:MyDate runat="server" ID="datEffectiveDate" selecteddate='<%# Bind("EffectiveDate")%>'/>
                        </td>

                        <td>
                             <asp:Literal ID="Literal6" runat="server" Text="   ‰ ÂÌ ›Ì :"/>
                        </td>
                        <td  >
                            <uc1:MyDate runat="server" ID="datDateExpire" selecteddate='<%# Bind("DateExpire")%>' />
                            <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "datDateExpire$TextBox1"
                                                 ErrorMessage = "Start date must be greater or equal!!!"
                                                 ClientValidationFunction="CompareDate" >
                            </asp:CustomValidator>
                        </td>
                        </tr>
                    <tr>
                        <td >
                             <asp:Literal ID="Literal8" runat="server" Text="   „·«ÕŸ«  :"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="200px" Height="70px" Text='<%# Bind("Note")%>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                        Text=" ÕœÌÀ «·≈Ã«“…" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" CommandName="Cancel" OnClientClick="ReloadSamepage(); return false;"
                                        Text="≈·€«¡ «·√„—" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>

        </asp:FormView>

    </ContentTemplate>
</asp:UpdatePanel>


<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="HolidayId" Caption="ﬁ«∆„… «·—”«∆· «·„”Ã·Â" DataSourceID="dsHolidays"
            SkinID="xgridviewSkinEmployee" Width="70%"  style="text-align:right;direction:rtl;" ShowHeaderWhenEmpty="True">
            <Columns>
               
                <asp:BoundField DataField="HolidayId" HeaderText="„”·”·" ReadOnly="True"  />

                <asp:BoundField DataField="HolidayName" HeaderText="‰’ «·—”«·Â" />

                <asp:BoundField DataField="EffectiveDate" HeaderText="‰Ê⁄Â« " />

                <asp:BoundField DataField="DateExpire" HeaderText="Êﬁ Â« " />

                <asp:TemplateField HeaderText=" ⁄œÌ·" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton id="EditLinkButton" runat="server" Text="" CommandName="Edit"  ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" cssclass="edit_button" HorizontalAlign="Center" />
                </asp:TemplateField>
                   
                <asp:TemplateField HeaderText="Õ–›" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton id="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete"  CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" 
                            OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" cssclass="delete_button" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </x:GridView>
  <asp:Button ID="btnAddHoliday" runat="server"   
                            Text="≈÷«›… ÃœÌœ" 
                            UseSubmitBehavior="False"/>
  <p>
      &nbsp;</p>

<asp:ObjectDataSource ID="dsHolidays" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetHolidaysDataset" TypeName="ATS.BO.Framework.BOHoliday">
    <SelectParameters>
        <asp:Parameter Name="PageNo" Type="Int32" DefaultValue="1" />
        <asp:Parameter Name="PageSize" Type="Int32" DefaultValue="100" />
    </SelectParameters>
</asp:ObjectDataSource>





  