<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlGatePassApproval.ascx.vb" Inherits="Attendance_Controls_ctlGatePassForApproval" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>



<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="GatePassId" Caption="ÞÇÆãÉ ÈÅÓÊÆÐÇä ÇáãæÙÝíä Ýí ÇäÊÙÇÑ ÇáãæÇÝÞÉ" 
            SkinID="xgridviewSkinEmployee" Width="98%" CssClass="TableView" style="text-align:center;font-size:large" DataSourceID="dsGatePass" ShowHeaderWhenEmpty="True">
    <Columns>

        <asp:BoundField DataField="Gatepassid" HeaderText="ÑÞã ÇáÍÇÓÈ"  />

        <asp:BoundField DataField="EmployeeName" HeaderText="ÅÓã ÇáãæÙÝ"   />

        <asp:BoundField DataField="GatepassTypeName" HeaderText="äæÚ ÇáÅÓÊÆÐÇä" />

        <asp:BoundField DataField="GatepassBegTime" HeaderText="íÈÏÃ ãä" />

        <asp:BoundField DataField="GatepassEndTime" HeaderText="íäÊåí Ýí" />

        <%--<asp:BoundField DataField="AprovalStatus" HeaderText="ÍÇáÉ ÇáØáÈ" />--%>
        <asp:BoundField DataField="Notes" HeaderText="æÕÝ ÇáØáÈ" />

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


<asp:ObjectDataSource ID="dsGatePass" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetGatepassByEmployeeManagerForApprovalDataset" TypeName="ATS.BO.FrameWork.BOGatepass">
    <SelectParameters>
        <asp:SessionParameter Name="ManagerID" SessionField="AccountEmployeeId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>



