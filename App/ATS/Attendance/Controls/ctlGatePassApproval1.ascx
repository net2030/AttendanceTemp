<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlGatePassApproval1.ascx.vb" Inherits="Attendance_Controls_ctlGatePassApproval1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<style type="text/css">
    .style1
    {
        height: 23px;
    }
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    .Grid td
    {
        background-color: White;
        color: Black;
        font-size: 10pt;
        line-height: 200%;
    }
    .Grid th
    {
        background-color:  #0c5299;
        color: White;
        font-size: 10pt;
        line-height: 200%;
    }
    .ChildGrid td
    {
        background-color: #336699 !important;
        color: White;
        font-size: 10pt;
        line-height: 200%;
    }
    .ChildGrid th
    {
        background-color: white !important;
        color: black;
        font-size: 10pt;
        line-height: 200%;
    }
</style>
<meta content="text/html"; charset="UTF-8" http-equiv="Content-Type" />
<script src="../js/jquery.min.js"></script>
<script type="text/javascript">
    function divexpandcollapse(divname) {
        var img = "img" + divname;
        if ($("#" + img).attr("src") == "../Images/plus.png") {
            $("#" + img)
            .closest("tr")
            .after("<tr><td></td><td colspan = '100%'>" + $("#" + divname)
                   .html() + "</td></tr>");
            $("#" + img).attr("src", "../Images/minus.png");
        }
        else {
            $("#" + img).closest("tr").next().remove();
            $("#" + img).attr("src", "../Images/plus.png");
        }
    }
    $(".loading").css({ display: 'inline' });
    tournamentCollection.fetch({
        success: function (bookResponse) {
            $(".loading").css({ display: 'none' });
        }
    });
</script>


<x:GridView ID="EmployeeManagerGridView" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="GatePassId,ApprovalPolicyId,ApprovalPathId,ManagerId,Rejected,Approved,ApprovalPathSequence,MaxApprovalPathSequence,EmployeeId" 
            Caption ="����� �������� �������� �� ������ ������� �� ���� �����(Direct Manager)" 
            SkinID="xgridviewSkinEmployee" Width="98%" CssClass="TableView" style="text-align:center;" DataSourceID="dsGatePass" meta:resourcekey="EmployeeManagerGridViewResource1" >
    <Columns>

        <asp:TemplateField ItemStyle-Width="20px" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <a href="JavaScript:divexpandcollapse('div<%# Eval("Gatepassid")%>');">
                                    <img alt="Details" id='imgdiv<%# Eval("Gatepassid") %>' src="../Images/plus.png" />
                                </a>
                                <div id='div<%# Eval("Gatepassid") %>' style="display: none;">
                                    <asp:GridView ID="grdViewApprovalLog" runat="server" AutoGenerateColumns="False"
                                                   CssClass="ChildGrid" meta:resourcekey="grdViewApprovalLogResource1">
                                        <Columns>

                                            <asp:BoundField  DataField="ApprovalPolicyName" HeaderText="���� ��������" meta:resourcekey="BoundFieldResource1" />
                                            <asp:BoundField DataField="ApprovalPathSequence" HeaderText="��� ������" meta:resourcekey="BoundFieldResource2" />
                                            <asp:BoundField  DataField="ApprovedBy" HeaderText="��� ������" meta:resourcekey="BoundFieldResource3" />
                                            <asp:BoundField  DataField="ApprovalStatus" HeaderText="���� ��������" meta:resourcekey="BoundFieldResource4" />
                                            <asp:BoundField  DataField="Comments" HeaderText="�����" meta:resourcekey="BoundFieldResource5" />
                                            <asp:BoundField  DataField="ApprovedOn" HeaderText="����� ��������" meta:resourcekey="BoundFieldResource6" />
                                           
                                        
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ItemTemplate>

<ItemStyle Width="20px"></ItemStyle>
         </asp:TemplateField>

        <asp:BoundField DataField="Gatepassid" HeaderText="��� ������" meta:resourcekey="BoundFieldResource7"  />

        <asp:BoundField DataField="FullName" HeaderText="��� ������" meta:resourcekey="BoundFieldResource8"   />

        <asp:BoundField DataField="GatepassTypeName" HeaderText="��� ���������" meta:resourcekey="BoundFieldResource9" />

        <asp:BoundField DataField="GatepassBegTime" HeaderText="���� ��" meta:resourcekey="BoundFieldResource10" />

        <asp:BoundField DataField="GatepassEndTime" HeaderText="����� ��" meta:resourcekey="BoundFieldResource11" />

        <%--<asp:BoundField DataField="AprovalStatus" HeaderText="���� �����" />--%>
        <asp:BoundField DataField="Notes" HeaderText="��� �����" meta:resourcekey="BoundFieldResource12" />

        <asp:TemplateField HeaderText="������ ����� " meta:resourcekey="TemplateFieldResource2">
            <itemtemplate>
                <asp:RadioButton ID="rdSpecificEmployee" runat="server" GroupName="SpecificEmployee" meta:resourcekey="rdSpecificEmployeeResource1" ></asp:RadioButton>
            </itemtemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="��� �����" meta:resourcekey="TemplateFieldResource3" >
            <itemtemplate>
                <asp:RadioButton ID="rdSpecificEmployeeRejected" runat="server" GroupName="SpecificEmployee" meta:resourcekey="rdSpecificEmployeeRejectedResource1" ></asp:RadioButton>
            </itemtemplate>

        </asp:TemplateField>

        <asp:TemplateField HeaderText="�������" meta:resourcekey="TemplateFieldResource4" >
            <itemtemplate>
                <asp:TextBox ID="Comments" runat="server" TextMode="MultiLine" meta:resourcekey="CommentsResource1"></asp:TextBox>
            </itemtemplate>
        </asp:TemplateField>

    </Columns>
</x:GridView>

<x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="GatePassId,ApprovalPolicyId,ApprovalPathId,ManagerId,Rejected,Approved,ApprovalPathSequence,MaxApprovalPathSequence,EmployeeId" 
            Caption ="����� �������� �������� �� ������ ������� ����� " 
            SkinID="xgridviewSkinEmployee" Width="98%" CssClass="TableView" style="text-align:center;" meta:resourcekey="GridView1Resource1"  >
    <Columns>

        <asp:BoundField DataField="Gatepassid" HeaderText="��� ������" meta:resourcekey="BoundFieldResource13"  />

        <asp:BoundField DataField="FullName" HeaderText="��� ������" meta:resourcekey="BoundFieldResource14"   />

        <asp:BoundField DataField="GatepassTypeName" HeaderText="��� ���������" meta:resourcekey="BoundFieldResource15" />

        <asp:BoundField DataField="GatepassBegTime" HeaderText="���� ��" meta:resourcekey="BoundFieldResource16" />

        <asp:BoundField DataField="GatepassEndTime" HeaderText="����� ��" meta:resourcekey="BoundFieldResource17" />

        <%--<asp:BoundField DataField="AprovalStatus" HeaderText="���� �����" />--%>
        <asp:BoundField DataField="Notes" HeaderText="��� �����" meta:resourcekey="BoundFieldResource18" />

        <asp:TemplateField HeaderText="������ ����� " meta:resourcekey="TemplateFieldResource5">
            <itemtemplate>
                <asp:RadioButton ID="rdSpecificEmployee" runat="server" GroupName="SpecificEmployee" meta:resourcekey="rdSpecificEmployeeResource2" ></asp:RadioButton>
            </itemtemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="��� �����" meta:resourcekey="TemplateFieldResource6" >
            <itemtemplate>
                <asp:RadioButton ID="rdSpecificEmployeeRejected" runat="server" GroupName="SpecificEmployee" meta:resourcekey="rdSpecificEmployeeRejectedResource2" ></asp:RadioButton>
            </itemtemplate>

        </asp:TemplateField>

        <asp:TemplateField HeaderText="�������" meta:resourcekey="TemplateFieldResource7" >
            <itemtemplate>
                <asp:TextBox ID="Comments" runat="server" TextMode="MultiLine" meta:resourcekey="CommentsResource2"></asp:TextBox>
            </itemtemplate>
        </asp:TemplateField>

    </Columns>
</x:GridView>
<% If GridView1.Rows.Count > 0 Or EmployeeManagerGridView.Rows.Count > 0 Then%>
<table style="width: 100%">
    <tr>
        <td>
            &nbsp<asp:Button ID="btnAddGatePass" runat="server" OnClick="btnAddGatePass_Click"
        Text="����� ���� ���������" Width="163px" UseSubmitBehavior="False" meta:resourcekey="btnAddGatePassResource1" />

        </td>
    </tr>
</table>
<% Else%>
<asp:Literal ID="Literal1" runat="server" Text="�� ���� �� ����� �����" meta:resourcekey="Literal1Resource1"></asp:Literal>
<% End if %>


<asp:ObjectDataSource ID="dsGatePass" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetGatepassByEmployeeManagerForApprovalDataset1" TypeName="ATS.BO.Framework.BOGatepass">
    <SelectParameters>
        <asp:Parameter Name="ManagerID" Type="Int32" />
         <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

<%--<asp:ObjectDataSource ID="dsGatePass1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetGatepassBySpecificRoleForApprovalDataset" TypeName="ATS.BO.Framework.BOGatepass">
    <SelectParameters>
        <asp:Parameter Name="ManagerID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>--%>
