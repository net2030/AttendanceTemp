<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAccountEmployeeLogs.ascx.vb" Inherits="Controls_ctlEmployeeLogs" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>

<table class="xAdminOption" width="850px" >

      
        <tr>
            <td>
                <asp:Literal ID="Literal4" runat="server" Text=" ÈÏÇíÉ ÇáÝÊÑÉ  :" meta:resourcekey="Literal4Resource1" ></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Startdate"/>
             
            </td>
            <td>
                <asp:Literal ID="Literal5" runat="server" Text="äåÇíÉ ÇáÝÊÑÉ :" meta:resourcekey="Literal5Resource1" ></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Enddate"  />
                <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "EndDate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1" ></asp:CustomValidator>
             
            </td>
        </tr>



        <tr>
            <td>
                <div style="width:100px;height:10px" />
            </td>
        </tr>
        <tr>

            <td>
                <asp:Button ID="viewData" runat="server"   CausesValidation="false"
                            Text="ÚÑÖ ÇáÊÞÑíÑ" 
                            UseSubmitBehavior="False" meta:resourcekey="btnAddResource1"  />
            </td>

             <td>
                <asp:Button ID="btnExportToExcel" runat="server"  Enabled="false" CausesValidation="false"
                            Text="Export To Excel" 
                            UseSubmitBehavior="False"   />
            </td>
        </tr>
        
    </table>



<x:GridView ID="GrdEmployeeAttendance" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
    SkinID="xgridviewSkinEmployee" Caption="ÓÌáÇÊ ÍÖæÑ æÇäÕÑÇÝ ãæÙÝ" Width="98%" meta:resourcekey="GrdEmployeeAttendanceResource1"  >

    <Columns>


        <asp:BoundField DataField="LogId" HeaderText="ãÓáÓá" SortExpression="LogId" Visible="false" meta:resourcekey="BoundFieldResource1">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="LogDate" HeaderText="ÊÇÑíÎ ÇáÍÖæÑ" SortExpression="LogDate" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" meta:resourcekey="BoundFieldResource2" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="LogTime" HeaderText="æÞÊ ÇáÍÑßÉ" SortExpression="InTime" DataFormatString="{0:hh:mm tt}" meta:resourcekey="BoundFieldResource3" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="LogType" HeaderText="äæÚ ÇáÍÑßÉ" SortExpression="LogType" meta:resourcekey="BoundFieldResource4" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="Location" HeaderText="ãæÞÚ ÇáÌåÇÒ" SortExpression="Location" meta:resourcekey="BoundFieldResource5" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>



    </Columns>
</x:GridView>

<asp:GridView ID="GridView1" runat="server" style="display:none"></asp:GridView>




<%--<asp:ObjectDataSource ID="dsEmployeeAttendance" runat="server" OldValuesParameterFormatString="original_{0}"
                              SelectMethod="GetEmployeeAttendanceByDate" TypeName="ATS.BO.Framework.BOAttendanceLog">
            <SelectParameters>
                <asp:Parameter Name="EmployeeId" Type="Int32" />
                <asp:Parameter Name="BegDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />
                <asp:Parameter Name="PageNo" Type="Int32" DefaultValue="1" />
                <asp:Parameter DefaultValue="300" Name="PageSize" Type="Int32" />
                <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
                <asp:Parameter Name="Op" Type="Int32" DefaultValue="0" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>





