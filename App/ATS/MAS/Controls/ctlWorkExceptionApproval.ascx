<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlWorkExceptionApproval.ascx.vb" Inherits="Attendance_Controls_ctlWorkExceptionFinancialApproval" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>


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
            DataKeyNames="WorkExceptionId" Caption="ÞÇÆãÉ ÈÅÓÊËäÇÁÇÊ ÇáÏæÇã æãÓÊÍÞÇÊ ÇáãæÙÝíä" 
            SkinID="xgridviewSkinEmployee" Width="98%" CssClass="TableView" style="text-align:center;font-size:large" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="True">
    <Columns>

        <asp:BoundField DataField="WorkExceptionId" HeaderText="ãÓáÓá" ReadOnly="True"  />

        <asp:BoundField DataField="EmployeeName" HeaderText="ÅÓã ÇáãæÙÝ" />

        <asp:BoundField DataField="WorkExceptionTypeName" HeaderText="äæÚ ÇáÅÓÊËäÇÁ" />


           <asp:TemplateField HeaderText = "ÊÇÑíÎ ÇáÈÏÇíÉ">

                    <ItemTemplate>
                       <uc1:MyDate runat="server" ID="datWorkExceptionBegDate" SelectedDate= '<%# Eval("WorkExceptionBegDate")%>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                       <uc1:MyDate runat="server" ID="datWorkExceptionBegDate" SelectedDate= '<%# Eval("WorkExceptionBegDate")%>' />
                    </EditItemTemplate>


                </asp:TemplateField>


        <asp:TemplateField HeaderText = "ÊÇÑíÎ ÇáäåÇíÉ">

                    <ItemTemplate>
                        <uc1:MyDate runat="server" ID="datWorkExceptionEndDate" SelectedDate= '<%# Eval("WorkExceptionEndDate")%>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                       <uc1:MyDate runat="server" ID="datWorkExceptionEndDate" SelectedDate= '<%# Eval("WorkExceptionEndDate")%>' />
                    </EditItemTemplate>


                </asp:TemplateField>

        <asp:BoundField DataField="Notes" HeaderText="æÕÝ ÇáØáÈ" />

        <asp:TemplateField HeaderText="ÅÚÊãÇÏ ÇáØáÈ ">
            <itemtemplate>
                <asp:RadioButton ID="rdSpecificEmployee" runat="server" GroupName="SpecificEmployee"  Checked ='<%# Eval("IsApproved")%>' ></asp:RadioButton>
            </itemtemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="ÑÝÖ ÇáØáÈ" >
            <itemtemplate>
                <asp:RadioButton ID="rdSpecificEmployeeRejected" runat="server" GroupName="SpecificEmployee"  Checked ='<%# Eval("IsRejected")%>' ></asp:RadioButton>
            </itemtemplate>

        </asp:TemplateField>

         <asp:TemplateField HeaderText="ÇáãÈáÛ ÇáãÓÊÍÞ">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtDueAmount" Width="70px" runat="server" Value='<%# CDec(Eval("DueAmount"))%>'></telerik:RadNumericTextBox>
            </ItemTemplate>

        </asp:TemplateField>

      

         <asp:TemplateField HeaderText="ÇáãÏÝæÚ">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtAmount" Width="70px" runat="server" Value='<%# CDec(Eval("AmountPaid"))%>'></telerik:RadNumericTextBox>
            </ItemTemplate>

        </asp:TemplateField>
       
          <asp:TemplateField HeaderText="Êã ÇáÏÝÚ">
            <ItemTemplate>
             <asp:CheckBox ID="isPaid" runat="server" Checked ='<%# Eval("IsPaid")%>'></asp:CheckBox>
            </ItemTemplate>

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

<asp:ObjectDataSource ID="dsWorkException" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWorkExceptionByEmployeeManagerForApprovalDataset" TypeName="ATS.BO.FrameWork.BOWorkException">
    <SelectParameters>
        <asp:SessionParameter Name="ManagerID" SessionField="AccountEmployeeId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ATSConnectionString %>" SelectCommand="EXEC [Managements].SpWorkException1_GetForApproval">
</asp:SqlDataSource>









