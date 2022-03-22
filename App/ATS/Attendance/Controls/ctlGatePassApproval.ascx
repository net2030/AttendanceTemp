<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlGatePassApproval.ascx.vb" Inherits="Attendance_Controls_ctlGatePassForApproval" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>



<x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="GatePassId" Caption="����� �������� �������� �� ������ ��������" 
            SkinID="xgridviewSkinEmployee" Width="98%" CssClass="TableView" style="text-align:center;font-size:large" DataSourceID="dsGatePass" ShowHeaderWhenEmpty="True">
    <Columns>

        <asp:BoundField DataField="Gatepassid" HeaderText="��� ������"  />

        <asp:BoundField DataField="EmployeeName" HeaderText="��� ������"   />

        <asp:BoundField DataField="GatepassTypeName" HeaderText="��� ���������" />

        <asp:BoundField DataField="GatepassBegTime" HeaderText="���� ��" />

        <asp:BoundField DataField="GatepassEndTime" HeaderText="����� ��" />

        <%--<asp:BoundField DataField="AprovalStatus" HeaderText="���� �����" />--%>
        <asp:BoundField DataField="Notes" HeaderText="��� �����" />

        <asp:TemplateField HeaderText="������ ����� ">
                        <itemtemplate>
                            <asp:RadioButton ID="rdSpecificEmployee" runat="server" GroupName="SpecificEmployee" ></asp:RadioButton>
                        </itemtemplate>
                    
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��� �����" >
                        <itemtemplate>
                            <asp:RadioButton ID="rdSpecificEmployeeRejected" runat="server" GroupName="SpecificEmployee" ></asp:RadioButton>
                        </itemtemplate>
                    
                    </asp:TemplateField>

         <asp:TemplateField HeaderText="�������" >
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
        Text="����� ���� ���������" Width="163px" UseSubmitBehavior="False" />

        </td>
    </tr>
</table>


<asp:ObjectDataSource ID="dsGatePass" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetGatepassByEmployeeManagerForApprovalDataset" TypeName="ATS.BO.FrameWork.BOGatepass">
    <SelectParameters>
        <asp:SessionParameter Name="ManagerID" SessionField="AccountEmployeeId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>



