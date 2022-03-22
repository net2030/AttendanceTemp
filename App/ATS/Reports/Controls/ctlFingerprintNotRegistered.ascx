<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlFingerprintNotRegistered.ascx.vb" Inherits="Employee_Controls_ctlctlFingerprintNotRegistered" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register src="../../ctlDepartmentTree.ascx" tagname="ctlDepartmentTree" tagprefix="uc1" %>
<style type="text/css">
    .style1
    {
        height: 23px;
    }
</style>
<div style="border-style: solid; border-color: inherit; border-width: medium; width:737px;">


<table class="xAdminOption" style="width: 737px;">

    <tr>
        <th colspan="6" class="caption" style="text-align:center;">
            <asp:Literal ID="Literal1" runat="server" Text="ÊÞÑíÑ ÊÓÌíá ÈÕãÇÊ ÇáãæÙÝíä ÍÓÈ ÇáÞÓã" meta:resourcekey="Literal1Resource1"></asp:Literal>

        </th>
    </tr>
    <tr>
        <th colspan="6" class="FormViewSubHeader" style="text-align:center;">
            &nbsp;</th>
    </tr>
    <tr>
        <td style="width:100px;">
            <asp:Literal ID="Literal2" runat="server" Text=" ÇáÞÓã :" meta:resourcekey="Literal2Resource1"></asp:Literal>
        </td>

        <td colspan="3">
                  <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" IsRequired="true"/>
        </td>
    </tr>

    
   <%-- <tr>
        <td>
            <div style="width:100px;height:10px" />
        </td>
    </tr>--%>
    <tr>

        <td>
            <asp:Button ID="btnAdd" runat="server"
                        Text="ÚÑÖ ÇáÊÞÑíÑ" 
                        UseSubmitBehavior="False" meta:resourcekey="btnAddResource1" />
        
        </td>
        <td>
                <asp:RadioButton ID="rdAll" runat="server" GroupName="Opti" Text="Çáßá" Checked="True" meta:resourcekey="rdAllResource1" />
        </td>
        <td>
                <asp:RadioButton ID="rdRegistered" runat="server" GroupName="Opti" Text="Êã ÊÓÌíáåã" meta:resourcekey="rdRegisteredResource1" />
        </td>
        <td>
                <asp:RadioButton ID="rdNotRegistered" runat="server" GroupName="Opti" Text="áã íÊã ÊÓÌíáåã" meta:resourcekey="rdNotRegisteredResource1" />
        </td>
    </tr>
   
   </table>
    </div>
<br />
 
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Font-Names="Verdana" Font-Size="8pt"  Height="1000px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="737px" meta:resourcekey="ReportViewer1Resource1">
                <LocalReport ReportPath="" EnableExternalImages="True">
                </LocalReport>
            </rsweb:ReportViewer>
    
    
    <asp:ObjectDataSource ID="dsDepartments" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDepartmentsList" TypeName="LookUp">
    </asp:ObjectDataSource>



    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="AttendanceDAL" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAttendanceByEmployee">
        <SelectParameters>
            <asp:Parameter Name="EmployeeId" Type="Int32" />
            <asp:Parameter Name="BegDate" Type="DateTime" />
            <asp:Parameter Name="EndDate" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>

