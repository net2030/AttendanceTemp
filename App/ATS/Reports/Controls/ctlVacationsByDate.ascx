<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlVacationsByDate.ascx.vb" Inherits="Employee_Controls_ctlVacationsByDate" %>

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
<div style="border-style: solid; border-color: inherit; border-width: medium; width:920px;">


    <table class="xAdminOption" width="920px">

    <tr>
        <th colspan="6" class="caption" style="text-align:center;">
            <asp:Literal ID="Literal9" runat="server" Text="ÊÞÑíÑ ÅÌÇÒÇÊ ÞÓã ãÍÏÏ" meta:resourcekey="Literal9Resource1"/>
        </th>
    </tr>
    <tr>
        <th colspan="6" class="FormViewSubHeader" style="text-align:center;">
            <asp:Literal ID="Literal10" runat="server" Text="íÚÑÖ åÐÇ ÇáÊÞÑíÑ ÅÌÇÒÇÊ ãæÙÝí ÞÓã ãÚíä ÎáÇá ÊÇÑíÎ ãÚíä" meta:resourcekey="Literal10Resource1" />
        </th>
    </tr>
    <tr>
        <td >
             <asp:Literal ID="Literal3" runat="server" Text=" fsfsdafasd:" meta:resourcekey="Literal3Resource12"></asp:Literal>
           
        </td>

        <td colspan="2">
                  <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" IsRequired="true"/>
        </td>
    </tr>

    <tr>
        <td>
            <asp:Literal ID="Literal1" runat="server" Text=" ÊÇÑíÎ ÇáÈÏÇíÉ :" meta:resourcekey="Literal1Resource1"></asp:Literal>
        </td>
        <td>
            <uc1:MyDate runat="server" ID="Startdate"  SelectedDate="<%# DateAdd(DateInterval.Month, -1, Now) %>"/>
           <%-- <telerik:RadDatePicker ID="Startdate" Runat="server">
            <DateInput DateFormat="yyyy/MM/dd"></DateInput> </telerik:raddatepicker>--%>
        </td>
        <td>
           
             <asp:Literal ID="Literal2" runat="server" Text="   :" meta:resourcekey="Literal2Resource1"></asp:Literal>
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
 
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Font-Names="Verdana" Font-Size="8pt"  Height="575px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="920px">
                <LocalReport ReportPath="" EnableExternalImages="True">
                </LocalReport>
            </rsweb:ReportViewer>
    
 

