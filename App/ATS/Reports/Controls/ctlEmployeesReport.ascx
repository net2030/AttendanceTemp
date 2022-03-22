<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlEmployeesReport.ascx.vb" Inherits="Employee_Controls_ctlEmployeesReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="../../ctlDepartmentTree.ascx" TagName="ctlDepartmentTree" TagPrefix="uc1" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>


<style type="text/css">
    .style1 {
        height: 23px;
    }
</style>
<div style="border: solid; width: 1000px;">

    <table class="xAdminOption" width="1000px" style="">

        <tr>
            <th colspan="6" class="caption" style="text-align: center;">
                <asp:Literal ID="Literal9" runat="server" Text="ÊÞÑíÑ ÈíÇäÇÊ ÇáãæÙÝíä  " meta:resourcekey="Literal9Resource1" />
            </th>
        </tr>
        <tr>
            <th colspan="6" class="FormViewSubHeader" style="text-align: center;">
                <asp:Literal ID="Literal10" runat="server" Text="íÚÑÖ åÐÇ ÇáÊÞÑíÑ ÊÝÇÕíá ÈíÇäÇÊ ÇáãæÙÝíä ÍÓÈ ÇáÞÓã" meta:resourcekey="Literal10Resource1" />
            </th>
        </tr>
        <tr>
            <td style="width: 10%;">
                <asp:Literal ID="Literal1" runat="server" Text="ÇáÞÓã :" meta:resourcekey="Literal1Resource1"></asp:Literal>
            </td>

            <td colspan="4">
                <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" IsRequired="true"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal2" runat="server" Text="ÌåÉ ÇáÊæÙíÝ" meta:resourcekey="Literal2Resource1"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlEmployer" runat="server" DataSourceID="dsAccountEmployerObject" DataValueField="EmployerId" DataTextField="EmployerName" meta:resourcekey="ddlEmployerResource1"></asp:DropDownList>
            </td>
            <td style="width: 10%;">
                <asp:Literal ID="Literal3" runat="server" Text="äæÚ ÇáÚÞÏ" meta:resourcekey="Literal3Resource1"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlContractType" runat="server" DataSourceID="dsAccountContractTypeObject" DataValueField="ContractTypeId" DataTextField="ContractTypeName" meta:resourcekey="ddlContractTypeResource1"></asp:DropDownList>
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

<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="575px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1000px" meta:resourcekey="ReportViewer1Resource1">
    <LocalReport ReportPath="" EnableExternalImages="True">
    </LocalReport>
</rsweb:ReportViewer>





<asp:ObjectDataSource ID="dsAccountEmployerObject" runat="server" OldValuesParameterFormatString="original_{0}"
         SelectMethod="GetList" TypeName="ATS.BO.Framework.BOEmployer">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
      </asp:ObjectDataSource>

  <asp:ObjectDataSource ID="dsAccountContractTypeObject" runat="server" OldValuesParameterFormatString="original_{0}"
         SelectMethod="GetList" TypeName="ATS.BO.Framework.BOContractType">
       <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
      </asp:ObjectDataSource>
