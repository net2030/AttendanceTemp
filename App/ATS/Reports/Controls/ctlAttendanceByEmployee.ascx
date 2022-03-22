<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAttendanceByEmployee.ascx.vb" Inherits="Employee_Controls_ctlAttendanceByEmployee" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>



<style type="text/css">
    .style1
    {
        height: 23px;
    }
</style>
<div style="border:solid;width:1000px;">

    <table class="xAdminOption" width="1000px" >

        <tr>
            <th colspan="6" class="caption" style="text-align:center;">
                <asp:Literal ID="Literal9" runat="server" Text="ÊÞÑíÑ ÍÖæÑ æÅäÕÑÇÝ  " meta:resourcekey="Literal9Resource1"/>
            </th>
        </tr>
        <tr>
            <th colspan="6" class="FormViewSubHeader" style="text-align:center;">
                <asp:Literal ID="Literal10" runat="server" Text="íÚÑÖ åÐÇ ÇáÊÞÑíÑ ÊÝÇÕíá ÍÖæÑ æÅäÕÑÇÝ ãæÙÝ ãÚíä ÎáÇá ÊÇÑíÎ ãÚíä" meta:resourcekey="Literal10Resource1" />
            </th>
        </tr>
         <tr>
        <td>
            <asp:Literal ID="Literal1" runat="server" Text="ÇáÞÓã :" meta:resourcekey="Literal1Resource1"></asp:Literal>
        </td>
             

        <td>
            <uc1:ctlDepartmentTree runat="server" ID="ddlDepartment" AutoPostBack="true" IsRequired="false"/>
        </td>
    </tr>
        <tr>
            <td>

                <asp:Literal ID="Literal2" runat="server" Text=" ÅÓã ÇáãæÙÝ :" meta:resourcekey="Literal2Resource1"></asp:Literal>
            </td>

            <td>
                <asp:DropDownList ID="ddlEmployees" runat="server" DataTextField="EmployeeName" DataValueField="EmployeeId" meta:resourcekey="ddlEmployeesResource1" >
                    <asp:ListItem Value="0" Text ="...................."></asp:ListItem>
                </asp:DropDownList>
                 <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                        ID="rfvDDL_Product" Display="Dynamic"
                        ControlToValidate="ddlEmployees"
                        runat="server" Text="*"
                        ErrorMessage="ÅÎÊÑ ÇáãæÙÝ"
                        ForeColor="Red" meta:resourcekey="rfvDDL_ProductResource1"></asp:RequiredFieldValidator>
            </td>

             <td>
                 <asp:Literal ID="Literal3" runat="server" Text=" ÑÞã ÇáÈÕãÉ :" meta:resourcekey="Literal3Resource1"></asp:Literal>
            </td>

            <td>
                <asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" meta:resourcekey="txtEmployeeCodeResource1"  />
                <asp:Button ID="Button1" runat="server" Text="ÈÍË" CausesValidation="false" meta:resourcekey="Button1Resource1" />
            </td>

        </tr>

        <tr>
            <td>
                <asp:Literal ID="Literal4" runat="server" Text=" ÈÏÇíÉ ÇáÝÊÑÉ  :" meta:resourcekey="Literal4Resource1"></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Startdate" SelectedDate="<%# DateAdd(DateInterval.Month, -1, Now) %>"/>
                <%--<telerik:RadDatePicker ID="Startdate" Runat="server">
                <DateInput DateFormat="yyyy/MM/dd"></DateInput> </telerik:raddatepicker>--%>
            </td>
            <td>
                <asp:Literal ID="Literal5" runat="server" Text="äåÇíÉ ÇáÝÊÑÉ :" meta:resourcekey="Literal5Resource1"></asp:Literal>
            </td>
            <td>
                <uc1:MyDate runat="server" ID="Enddate" SelectedDate="<%# Now.Date %>" />
                <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "EndDate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1" ></asp:CustomValidator>
               <%-- <telerik:RadDatePicker ID="Enddate" Runat="server">
                <DateInput DateFormat="yyyy/MM/dd"></DateInput> </telerik:raddatepicker>--%>
            </td>
        </tr>

         <tr>
         <td>
             <asp:Literal ID="Literal6" runat="server" Text="  ÍÇáÉ ÇáÏæÇã :"></asp:Literal>
        </td>
         <td>
            <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="StatusName" DataValueField="StatusId" DataSourceID="ObjectDataSource2">
            </asp:DropDownList>
        </td>
    </tr>

        <tr>
            <td>
                <div style="width:100px;height:10px" />
            </td>
        </tr>
        <tr>

            <td>
                <asp:Button ID="btnAdd" runat="server" OnClick="btnView" 
                            Text="ÚÑÖ ÇáÊÞÑíÑ" 
                            UseSubmitBehavior="False" meta:resourcekey="btnAddResource1" />
            </td>
        </tr>
        
    </table>
</div>
<br />
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAttendanceStatusList" TypeName="ATS.BO.Framework.BOAttendanceLog"></asp:ObjectDataSource>
 
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="575px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1000px" meta:resourcekey="ReportViewer1Resource1">
        <LocalReport ReportPath="" EnableExternalImages="True">
        </LocalReport>
    </rsweb:ReportViewer>


