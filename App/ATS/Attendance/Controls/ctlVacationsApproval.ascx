<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlVacationsApproval.ascx.vb" Inherits="Attendance_Controls_ctlVacationApproval" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<style type="text/css">
    .style2
    {
        height: 24px;
        width: 34%;
    }
    .style9
    {
        width: 34%;
    }
</style>

<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="VacationId" Caption="ÞÇÆãÉ ÈÅÌÇÒÇÊ ÇáãæÙÝíä Ýí ÇäÊÙÇÑ ÇáãæÇÝÞÉ" 
            SkinID="xgridviewSkinEmployee" Width="98%" CssClass="TableView" style="text-align:center;font-size:large" DataSourceID="dsVacation" ShowHeaderWhenEmpty="True">
    <Columns>

         <asp:BoundField DataField="VacationId" HeaderText="ãÓáÓá" ReadOnly="True"  />

                <asp:BoundField DataField="EmployeeName" HeaderText="ÅÓã ÇáãæÙÝ" />

                <asp:BoundField DataField="DepartmentName" HeaderText="ÇáÞÓã" />

                <asp:BoundField DataField="TypeName" HeaderText="äæÚ ÇáÅÌÇÒÉ" />

                <asp:BoundField DataField="EffectiveDate" HeaderText="ÊÇÑíÎ ÇáÈÏÇíÉ" />

                <asp:BoundField DataField="DateExpire" HeaderText="ÊÇÑíÎ ÇáäåÇíÉ" />

                <asp:BoundField DataField="DateOfReturn" HeaderText="ÊÇÑíÎ ÇáãÈÇÔÑÉ" />

        <%--<asp:BoundField DataField="AprovalStatus" HeaderText="ÍÇáÉ ÇáØáÈ" />--%>
        <asp:BoundField DataField="Note" HeaderText="æÕÝ ÇáØáÈ" />

        <asp:TemplateField HeaderText="ÅÚÊãÇÏ ÇáØáÈ ">
                        <itemtemplate>
                            <asp:RadioButton ID="rdSpecificEmployee" runat="server" GroupName="SpecificEmployee" ></asp:RadioButton>
                        </itemtemplate>
                    
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ÑÝÖ ÇáØáÈ" >
                        <itemtemplate>
                            <asp:RadioButton ID="rdSpecificEmployeeRejected" runat="server" GroupName="SpecificEmployee" ></asp:RadioButton>
                        </itemtemplate>
                    
                    </asp:TemplateField>

         <asp:TemplateField HeaderText="ãáÇÍÙÇÊ" >
                        <itemtemplate>
                            <asp:TextBox ID="Comments" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </itemtemplate>
         </asp:TemplateField>

    </Columns>
</x:GridView>
<table style="width: 100%">
    <tr>
        <td>
            &nbsp<asp:Button ID="btnAddGatePass" runat="server" OnClick="btnAddGatePass_Click"
        Text="ÊÍÏíË ÍÇáÉ ÇáãæÇÝÞÇÊ" Width="163px" UseSubmitBehavior="False" />

        </td>
    </tr>
</table>


<asp:ObjectDataSource ID="dsVacation" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetVacationByEmployeeManagerForApprovalDataset" TypeName="ATS.BO.FrameWork.BOVacation">
    <SelectParameters>
        <asp:SessionParameter Name="ManagerID" SessionField="AccountEmployeeId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>



