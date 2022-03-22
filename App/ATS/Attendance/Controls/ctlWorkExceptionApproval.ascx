<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlWorkExceptionApproval.ascx.vb" Inherits="Attendance_Controls_ctlWorkExceptionApproval" %>
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
            DataKeyNames="WorkExceptionId" Caption="����� ���������� ������ �������� �� ������ ��������" 
            SkinID="xgridviewSkinEmployee" Width="98%" CssClass="TableView" style="text-align:center;font-size:large" DataSourceID="dsWorkException" ShowHeaderWhenEmpty="True">
    <Columns>

        <asp:BoundField DataField="WorkExceptionId" HeaderText="�����" ReadOnly="True"  />

        <asp:BoundField DataField="EmployeeName" HeaderText="��� ������" />

        <asp:BoundField DataField="WorkExceptionTypeName" HeaderText="��� ���������" />

        <asp:BoundField DataField="WorkExceptionBegDate" HeaderText="����� �������" />

        <asp:BoundField DataField="WorkExceptionEndDate" HeaderText="����� �������" />

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

<asp:ObjectDataSource ID="dsWorkException" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWorkExceptionByEmployeeManagerForApprovalDataset" TypeName="ATS.BO.FrameWork.BOWorkException">
    <SelectParameters>
        <asp:SessionParameter Name="ManagerID" SessionField="AccountEmployeeId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

