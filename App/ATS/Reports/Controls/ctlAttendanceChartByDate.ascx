<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAttendanceChartByDate.ascx.vb" Inherits="Employee_Controls_ctlAttendanceChartByDate" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register src="../../ctlDepartmentTree.ascx" tagname="ctlDepartmentTree" tagprefix="uc1" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>

<style type="text/css">
    .style1
    {
        height: 23px;
    }
</style>
<div style="border:solid;width:1100px;">

    <table class="xAdminOption" width="1100px" style="">

    <tr>
        <th colspan="6" class="caption" style="text-align:center;">
            <asp:Literal ID="Literal9" runat="server" Text="ÊÞÑíÑ ãÞÇÑäÉ" meta:resourcekey="Literal9Resource1"/>
        </th>
    </tr>
    <tr>
        <th colspan="6" class="FormViewSubHeader" style="text-align:center;">
            <asp:Literal ID="Literal10" runat="server" Text="íÚÑÖ åÐÇ ÇáÊÞÑíÑ ãÞÇÑäÉ ÍÇáÉ ÍÖæÑ Èíä ÇáÇÞÓÇã" meta:resourcekey="Literal10Resource1"/>
        </th>
    </tr>
    <tr>
        <td>
            <asp:Literal ID="Literal1" runat="server" Text="  ÇáÅÏÇÑÉ ÇáÑÆíÓíÉ" meta:resourcekey="Literal1Resource1"></asp:Literal>
        </td>

        <td>
            <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" IsRequired="true" />
        </td>
    </tr>

    <tr>
         <td>
             <asp:Literal ID="Literal4" runat="server" Text="  äØÇÞ ÇáãÞÇÑäÉ :" meta:resourcekey="Literal4Resource1"></asp:Literal>
        </td>
         <td>
            <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="StatusName" DataValueField="StatusId" DataSourceID="ObjectDataSource2" meta:resourcekey="ddlStatusResource1">
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>
            <asp:Literal ID="Literal2" runat="server" Text="ÊÇÑíÎ ÇáÈÏÇíÉ :" meta:resourcekey="Literal2Resource1"></asp:Literal>
        </td>
        <td>
            <uc1:MyDate runat="server" ID="Startdate" />
          
        </td>
        <td>
            <asp:Literal ID="Literal3" runat="server" Text=" ÊÇÑíÎ ÇáäåÇíÉ :" meta:resourcekey="Literal3Resource1"></asp:Literal>
        </td>
        <td>
            <uc1:MyDate runat="server" ID="Enddate" />
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
            <asp:Button ID="btnAdd" runat="server" OnClick="btnView" 
                        Text="ÚÑÖ ÇáÊÞÑíÑ" 
                        UseSubmitBehavior="False" meta:resourcekey="btnAddResource1" />
        </td>
    </tr>
   
   </table>

    </div>
<br />
 
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAttendanceStatusList" TypeName="ATS.BO.Framework.BOAttendanceLog">
                <SelectParameters>
                       <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />

                </SelectParameters>
            </asp:ObjectDataSource>
 
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Font-Names="Verdana" Font-Size="8pt"  Height="575px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1100px" meta:resourcekey="ReportViewer1Resource1">
                <LocalReport ReportPath="" EnableExternalImages="True">
                </LocalReport>
            </rsweb:ReportViewer>
  



    
