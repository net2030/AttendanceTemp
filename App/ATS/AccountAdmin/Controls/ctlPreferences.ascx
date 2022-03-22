<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlPreferences.ascx.vb" Inherits="AccountAdmin_Controls_ctlPreferences" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>

<asp:FormView ID="FormView1" runat="server"
    SkinID="formviewskinemployee"
    CellPadding="0"
    DefaultMode="Edit" DataSourceID="dsAccount" meta:resourcekey="FormView1Resource1">


    <EditItemTemplate>

        <table class="xview" >
            <tr>
                <th colspan="4" class="caption" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal9" runat="server" Text="بيانات الشركة ورخصة البرنامج" meta:resourcekey="Literal9Resource1" />
                </th>
            </tr>
            <tr>
                <th colspan="4" class="FormViewSubHeader" style="text-align: center; font-size: 14px;">
                    <asp:Literal ID="Literal10" runat="server" Text="تحديث بيانات الشركة" meta:resourcekey="Literal10Resource1" />
                </th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="txtCompanyName" Text="إسم الشركة:" meta:resourcekey="Label4Resource1"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox type="text" ID="txtCompanyName" Text='<%# Eval("AccountName") %>' Width="400px" runat="server" meta:resourcekey="txtCompanyNameResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtCompanyName" CssClass="ErrorMessage"
                        Display="Dynamic" ErrorMessage="*" Width="8px" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    <asp:Label ID="Label5" runat="server" Text="العنوان:" AssociatedControlID="txtAddress" meta:resourcekey="Label5Resource1"></asp:Label>
                    <br />
                    <asp:Label ID="Tip" runat="server" Font-Size="Smaller" Text="(ص . ب -المدينة -العنوان)" meta:resourcekey="TipResource1" />
                </td>
                <td>
                    <asp:TextBox type="text" name="txtAddress" ID="txtAddress" runat="server" Text='<%# Eval("Address1") %>' Width="250px" TextMode="MultiLine" Height="100px" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="التلفون:" AssociatedControlID="txtTel" meta:resourcekey="Label1Resource1" />
                </td>
                <td>
                    <asp:TextBox ID="txtTel" runat="server" Text='<%# Eval("Telephone") %>' meta:resourcekey="txtTelResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtTel" CssClass="ErrorMessage"
                        Display="Dynamic" ErrorMessage="*" Width="8px" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="الفاكس:" AssociatedControlID="txtFax" meta:resourcekey="Label6Resource1" />
                </td>

                <td>
                    <asp:TextBox ID="txtFax" runat="server" Text='<%# Eval("Fax") %>' meta:resourcekey="txtFaxResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtFax" CssClass="ErrorMessage"
                        Display="Dynamic" ErrorMessage="*" Width="8px" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>

                    <asp:Label ID="Label28" runat="server" Text="البريد الاليكتروني:"
                        AssociatedControlID="EMailAddressTextBox" meta:resourcekey="Label28Resource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="EMailAddressTextBox" runat="server" Text='<%# Eval("EMailAddress") %>'
                        name="EMailAddressTextBox" Width="168px" meta:resourcekey="EMailAddressTextBoxResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="EMailAddressTextBox" CssClass="ErrorMessage"
                        Display="Dynamic" ErrorMessage="Incorrect EMail Address" Font-Bold="True"
                        Font-Names="Verdana" Font-Size="X-Small"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="Edit" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                        ControlToValidate="EMailAddressTextBox" CssClass="ErrorMessage"
                        Display="Dynamic" ErrorMessage="*" Width="8px" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>

                </td>
                
                   <tr>
                    <td>نوع التقويم
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCalender" runat="server" SelectedValue='<%# Eval("culture")%>' >
                            <asp:ListItem Value="en-US">ميلادي</asp:ListItem>
                            <asp:ListItem Value="ar-SA">هجري</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="عدد سجلات العرض:" AssociatedControlID="txtFax" meta:resourcekey="Label11Resource1" />
                    </td>

                    <td>
                        <asp:TextBox ID="txtPageSize" runat="server" Text='<%# Eval("PageSize") %>' meta:resourcekey="txtPageSizeResource1" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ControlToValidate="txtFax" CssClass="ErrorMessage"
                            Display="Dynamic" ErrorMessage="*" Width="8px" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            <tr style="display :none">
                <td>
                    <asp:Label ID="Label12" runat="server" Text="عددأيام الغياب المتصلة لإصدار إنذار:" AssociatedControlID="txtFax" meta:resourcekey="Label12Resource1" />
                </td>

                <td>
                    <asp:TextBox ID="txtNoOfConsecutiveAbsenceDays" runat="server" Text='<%# Eval("NoOfConsecutiveAbsenceDays") %>' meta:resourcekey="txtNoOfConsecutiveAbsenceDaysResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="txtFax" CssClass="ErrorMessage"
                        Display="Dynamic" ErrorMessage="*" Width="8px" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="عددأيام الغياب المتقطعة لإصدار إنذار:" AssociatedControlID="txtFax" meta:resourcekey="Label13Resource1" />
                </td>

                <td>
                    <asp:TextBox ID="txtNoOfSepratedAbsenceDays" runat="server" Text='<%# Eval("NoOfSepratedAbsenceDays") %>' meta:resourcekey="txtNoOfSepratedAbsenceDaysResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                        ControlToValidate="txtFax" CssClass="ErrorMessage"
                        Display="Dynamic" ErrorMessage="*" Width="8px" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>


                                 <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text=" وقت بداية بصمة التحقق:" AssociatedControlID="timVerificationLogStartTime"  meta:resourcekey="Label14Resource1"/>
                    </td>

                    <td>
                         <telerik:RadTimePicker ID="timVerificationLogStartTime" Runat="server" MinDate="1900-01-01"  
                                               selecteddate='<%# If(IsDBNull(Eval("VerificationLogStartTime")), DateTime.Now, Convert.ToDateTime(Eval("VerificationLogStartTime")))%>'>
                        </telerik:RadTimePicker>
                       
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                            ControlToValidate="timVerificationLogStartTime" CssClass="ErrorMessage"
                            Display="Dynamic" ErrorMessage="*" Width="8px">
                        </asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="وقت نهاية بصمة التحقق:" AssociatedControlID="timVerificationLogEndTime" meta:resourcekey="Label15Resource1"/>
                    </td>

                    <td>
                         <telerik:RadTimePicker ID="timVerificationLogEndTime" Runat="server" MinDate="1900-01-01"  
                                               selecteddate='<%# If(IsDBNull(Eval("VerificationLogEndTime")), DateTime.Now, Convert.ToDateTime(Eval("VerificationLogEndTime")))%>'>
                        </telerik:RadTimePicker>
                        
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                            ControlToValidate="timVerificationLogEndTime" CssClass="ErrorMessage"
                            Display="Dynamic" ErrorMessage="*" Width="8px">
                        </asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
            </tr>

            </tr>

                     <tr>
                         <td>
                             <asp:Literal ID="Literal1" runat="server" Text="شعار الشركة:" meta:resourcekey="Literal1Resource1"></asp:Literal>
                         </td>
                         <td>

                             <asp:FileUpload ID="FileUpload2"
                                 runat="server" meta:resourcekey="FileUpload2Resource1" />
                         </td>

                     </tr>

            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="تحديث معلومات الشركة" OnClick="UpdateAccount" meta:resourcekey="Button1Resource1" />
                </td>
            </tr>

            <tr>
                <td>&nbsp;
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label8" ForeColor="Blue" runat="server" Text="بيانات الرخصة:" AssociatedControlID="Readers" meta:resourcekey="Label8Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border: outset" colspan="2">


                    <table class="xview">
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="الرقم المميز للجهاز:" AssociatedControlID="Label10"
                                    Visible='<%# If(Licensing.IsSameSystemID(Session("MachineId").ToString) = True, False, True) %>' meta:resourcekey="Label9Resource1"></asp:Label>
                            </td>

                            <td>

                                <asp:Label ID="Label10" runat="server" AssociatedControlID="Readers"
                                    Text="<%# Licensing.SystemID() %>"
                                    ForeColor='<%# If(Licensing.IsSameSystemID(Session("MachineId").ToString) = True, Color.Green, Color.Red) %>'
                                    Visible='<%# If(Licensing.IsSameSystemID(Session("MachineId").ToString) = True, False, True) %>' meta:resourcekey="Label10Resource1" />
                            </td>

                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="عدد القارئات المتاح:" AssociatedControlID="Readers" meta:resourcekey="Label3Resource1"></asp:Label>
                                </td>

                                <td>&nbsp; &nbsp;
                 <asp:DropDownList ID="Readers" runat="server"
                    BackColor='<%# If(programLic.IsReadersCountWithinRange(Session("LicenseId").ToString) = False, Color.Red, Color.White)%>'
                    SelectedValue='<%# programLic.AES_decrypt(Eval("LicenseId"))%>' Enabled="false">
                    <asp:ListItem Value="0">إختر</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="20">20 </asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="60">60</asp:ListItem>
                    <asp:ListItem Value="Unlimited">غير محدود</asp:ListItem>
                </asp:DropDownList>
                                    &nbsp;
                   <asp:Label ID="lbl100" runat="server" AssociatedControlID="Label3" Text="المُستخدَم حاليا:" meta:resourcekey="lbl100Resource1" />
                                    &nbsp;
                    <asp:Label ID="Label7" runat="server" AssociatedControlID="Label3"
                        ForeColor='<%# If(Licensing.IsReadersCountWithinRange(Session("LicenseId").ToString) = False, Color.Red, Color.Green) %>'
                        Text='<%# If(Licensing.GetDevices().ToString().Equals("99999"), "خطأ في الإتصال بقاعدة البيانات", Licensing.GetDevices()) %>' meta:resourcekey="Label7Resource1"></asp:Label>
                                </td>

                            </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="تاريخ إنتهاءالرخصة:" AssociatedControlID="lblLicenseType" meta:resourcekey="Label2Resource1"></asp:Label>
                            </td>

                            <td>&nbsp; &nbsp;
                 <asp:Label ID="lblLicenseType" runat="server"
                     ForeColor='<%# If(Licensing.IsExpired(Session("AccountExpiryActivation").ToString) = True, Color.Red, Color.Green) %>'
                     Text='<%# Licensing.AES_decrypt(Eval("AccountExpiryActivation")) %>' meta:resourcekey="lblLicenseTypeResource1"></asp:Label>

                                <asp:DropDownList ID="LicenseType" runat="server" Visible="False" meta:resourcekey="LicenseTypeResource2">
                                    <asp:ListItem Value="Trail" meta:resourcekey="ListItemResource11">تجريبي</asp:ListItem>
                                    <asp:ListItem Value="One Year" meta:resourcekey="ListItemResource12">سنة واحدة</asp:ListItem>
                                    <asp:ListItem Value="Two Year" meta:resourcekey="ListItemResource13">سنتين</asp:ListItem>
                                    <asp:ListItem Value="permanently" meta:resourcekey="ListItemResource14">غير منتهية</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td>
                                <asp:Button ID="Button2" runat="server" Text="تحديث الرخصة" OnClick="UpgradeLicense" meta:resourcekey="Button2Resource1" />
                                <asp:Button ID="Button3" runat="server" Text="أرسال طلب تحديث الرخصة" Width="145px" OnClick="Button3_Click1" Visible="False" meta:resourcekey="Button3Resource1" />
                            </td>


                </td>
            </tr>
        </table>
        </tr>
  
                
 
                 

           </table>
    </EditItemTemplate>

</asp:FormView>

<%--    </ContentTemplate>
</asp:UpdatePanel>--%>




<asp:ObjectDataSource ID="dsAccount" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByAccountId" TypeName="AccountDAL" DataObjectTypeName="System.Web.UI.WebControls.FileUpload" DeleteMethod="FileUpload" InsertMethod="AddAccountForActiveDirectory" UpdateMethod="UpdateApplicationVersion">

    <SelectParameters>
        <asp:SessionParameter Name="AccountId" SessionField="AccountId" Type="Int32" />
    </SelectParameters>

</asp:ObjectDataSource>


















<p>
    <asp:Label ID="lbl1" runat="server" Visible="False" meta:resourcekey="lbl1Resource1"></asp:Label>
</p>



















