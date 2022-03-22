<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlDailyAttendance.ascx.vb" Inherits="Employee_Controls_ctlDailyAttendance" %>

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
<div style="border:solid;width:1000px;">

    <%--<table class="xAdminOption" width="1000px" style="">

        <tr>
            <th colspan="6" class="caption" style="text-align:center;">
                <asp:Literal ID="Literal9" runat="server" Text=" ﬁ—Ì— Õ÷Ê— Ê≈‰’—«›  " meta:resourcekey="Literal9Resource1"/>
            </th>
        </tr>
        <tr>
            <th colspan="6" class="FormViewSubHeader" style="text-align:center;">
                <asp:Literal ID="Literal10" runat="server" Text="Ì⁄—÷ Â–« «· ﬁ—Ì—  ›«’Ì· Õ÷Ê— Ê≈‰’—«› ≈œ«—… „⁄Ì‰… Œ·«· ÌÊ„ „⁄Ì‰" meta:resourcekey="Literal10Resource1" />
            </th>
        </tr>
        <tr>
            <td style="width:10%;">
                «·ﬁ”„ :
            </td>

              <td colspan="4">
                  <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server"  />
            </td>
        </tr>
        <tr>
            <td>
                ÃÂ… «· ÊŸÌ›
            </td>
            <td>
                <asp:DropDownList ID="ddlEmployer" runat="server" DataSourceID="dsAccountEmployerObject" DataValueField="EmployerId" DataTextField="EmployerName" meta:resourcekey="ddlEmployerResource1"></asp:DropDownList>
            </td>
            <td style="width:10%;">
                ‰Ê⁄ «·⁄ﬁœ
            </td>
            <td>
                <asp:DropDownList ID="ddlContractType" runat="server" DataSourceID="dsAccountContractTypeObject" DataValueField="ContractTypeId" DataTextField="ContractTypeName" meta:resourcekey="ddlContractTypeResource1"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width:10%;">
                «· «—ÌŒ :
            </td>
            <td>
                <uc1:MyDate runat="server" ID="AttendanceDate"  />
           
            </td>
            
           
        </tr>
       
        <tr>
            
            <td>
                <asp:Button ID="btnAdd" runat="server" OnClick="btnView" 
                            Text="⁄—÷ «· ﬁ—Ì—" 
                            UseSubmitBehavior="False" meta:resourcekey="btnAddResource1" />
            </td>
        </tr>

    </table>--%>

    <table class="xAdminOption" width="1000px">

    <tr>
        <th colspan="6" class="caption" style="text-align:center;">
            <asp:Literal ID="Literal9" runat="server" Text="" meta:resourcekey="Literal9Resource1"/>
        </th>
    </tr>
    <tr>
        <th colspan="6" class="FormViewSubHeader" style="text-align:center;">
           <asp:Literal ID="Literal10" runat="server" Text="Ì⁄—÷ Â–« «· ﬁ—Ì—  ›«’Ì· Õ÷Ê— Ê≈‰’—«› ≈œ«—… „⁄Ì‰… Œ·«· ÌÊ„ „⁄Ì‰" meta:resourcekey="Literal10Resource1" />
        </th>
    </tr>
    <tr>
        <td >
             <asp:Literal ID="Literal3" runat="server" Text="  «·ﬁ”„ :" meta:resourcekey="Literal3Resource1"></asp:Literal>
           
        </td>

        <td colspan="2">
                  <uc1:ctlDepartmentTree ID="ctlDepartmentTree1" runat="server" IsRequired="true"/>
        </td>
    </tr>

    <tr>
        <td>
            <asp:Literal ID="Literal1" runat="server" Text="  «—ÌŒ «·»œ«Ì… :" meta:resourcekey="Literal1Resource1"></asp:Literal>
        </td>
        <td>
            <uc1:MyDate runat="server" ID="fromDate"  SelectedDate="<%# DateAdd(DateInterval.Month, -1, Now) %>"/>

        </td>
        <td>
           
             <asp:Literal ID="Literal2" runat="server" Text="   «—ÌŒ «·‰Â«Ì… :" meta:resourcekey="Literal2Resource1"></asp:Literal>
        </td>
        <td>
            <uc1:MyDate runat="server" ID="toDate" SelectedDate="<%# Now.Date %>" />
             <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "toDate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1" ></asp:CustomValidator>

        </td>
    </tr>

           <tr>
            <td>
                <asp:Literal ID="Literal4" runat="server" Text=" ÃÂ… «· ÊŸÌ›:" meta:resourcekey="Literal4Resource1"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlEmployer" runat="server" DataSourceID="dsAccountEmployerObject" DataValueField="EmployerId" DataTextField="EmployerName" meta:resourcekey="ddlEmployerResource1"></asp:DropDownList>
            </td>
            <td style="width:10%;">
                <asp:Literal ID="Literal5" runat="server" Text="‰Ê⁄ «·⁄ﬁœ" meta:resourcekey="Literal5Resource1"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlContractType" runat="server" DataSourceID="dsAccountContractTypeObject" DataValueField="ContractTypeId" DataTextField="ContractTypeName" meta:resourcekey="ddlContractTypeResource1"></asp:DropDownList>
            </td>

               <td style="width:10%;">
                   <asp:Literal ID="Literal6" runat="server" Text=" Ê—œÌ… «·⁄„·:" meta:resourcekey="Literal6Resource1"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlWorkSchedule" runat="server" DataSourceID="dsWorkScheduleObject" DataValueField="WorkScheduleId" DataTextField="WorkScheduleName" meta:resourcekey="ddlWorkScheduleResource1"> <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">....≈Œ —....</asp:ListItem></asp:DropDownList>
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
                        Text="⁄—÷ «· ﬁ—Ì—" 
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



<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="AttendanceDAL" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAttendanceByEmployee">
    <SelectParameters>
        <asp:Parameter Name="EmployeeId" Type="Int32" />
        <asp:Parameter Name="BegDate" Type="DateTime" />
        <asp:Parameter Name="EndDate" Type="DateTime" />
    </SelectParameters>
</asp:ObjectDataSource>

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


<asp:ObjectDataSource ID="dsWorkScheduleObject" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOWorkSchedule">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>