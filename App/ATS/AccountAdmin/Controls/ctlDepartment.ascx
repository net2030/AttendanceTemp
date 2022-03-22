<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlDepartment.ascx.vb" Inherits="AccountAdmin_Controls_ctlDepartment" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <table>
            <tr>
                <asp:Label runat="server" ID="Label2" Visible="False" meta:resourcekey="Label2Resource1" />
            </tr>
            <tr>
                <td style="padding-top: 10px">
                    <asp:Label runat="server" ID="Label1" AssociatedControlID="txtDepartmentId" Text="رقم الحاسب:" meta:resourcekey="Label1Resource1" />
                </td>
                <td style="padding-top: 10px">
                    <asp:TextBox ID="txtDepartmentId" runat="server" CssClass="text" ReadOnly="True" meta:resourcekey="txtDepartmentIdResource1" />
                </td>
            </tr>
            <tr>
                <td style="padding-top: 10px">
                    <asp:Label runat="server" ID="lblParentNode" AssociatedControlID="ctlDepartmentTree1"
                        Text="الإدارة التابع لها:" meta:resourcekey="lblParentNodeResource1" />
                </td>
                <td style="padding-top: 10px">
                    <uc1:ctlDepartmentTree runat="server" ID="ctlDepartmentTree1" />
                </td>
            </tr>
            <tr style="padding-top: 10px">
                <td style="padding-top: 10px">
                    <asp:Label runat="server" ID="lblNode" AssociatedControlID="txtNode" Text="إسم القسم:" meta:resourcekey="lblNodeResource1" />

                </td>
                <td style="padding-top: 10px">
                    <asp:TextBox ID="txtNode" runat="server" CssClass="text" Width="300px" meta:resourcekey="txtNodeResource1" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="txtNode" SetFocusOnError="True" meta:resourcekey="RequiredFieldValidator1Resource1" />
                </td>
            </tr>

             <tr style="padding-top: 10px">
                <td style="padding-top: 10px">
                    <asp:Label runat="server" ID="lblNodeEN" AssociatedControlID="txtNodeEN" Text="إسم القسمE :" meta:resourcekey="lblNodeENResource1" />

                </td>
                <td style="padding-top: 10px">
                    <asp:TextBox ID="txtNodeEN" runat="server" CssClass="text" Width="300px" meta:resourcekey="txtNodeResource1" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        ControlToValidate="txtNodeEN" SetFocusOnError="True" meta:resourcekey="RequiredFieldValidator1Resource1" />
                </td>
            </tr>

            <tr>
                <td style="padding-top: 10px; padding-bottom: 20px">
                    <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="submit"
                        OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnCancel" runat="server" Text="إلغاء" CssClass="submit"
                        OnClick="btnCancel_Click" CausesValidation="False" meta:resourcekey="btnCancelResource1" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <x:GridView ID="gvTreeNodes" runat="server" Width="70%" GridLines="None" DataKeyNames="DepartmentId,ParentId,DepartmentName" AutoGenerateColumns="False"
            SkinID="xgridviewSkinEmployee" CssClass="TableView" Caption="قائمة الأقسام" DataSourceID="ObjectDataSource1" meta:resourcekey="gvTreeNodesResource1">
            <Columns>

                <asp:BoundField DataField="DepartmentId" HeaderText="رقم القسم" meta:resourcekey="BoundFieldResource1" />

                <asp:BoundField DataField="DepartmentName" HeaderText="إسم القسم" SortExpression="DepartmentName" meta:resourcekey="BoundFieldResource2"></asp:BoundField>

                <asp:BoundField DataField="ParentName" HeaderText="الإدارة التابع لها" SortExpression="ParentName" meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                <asp:TemplateField HeaderText="تعديل" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit" CausesValidation="False" ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="حذف" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                            OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False" ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </x:GridView>

    </ContentTemplate>
</asp:UpdatePanel>

<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllDepartment" TypeName="ATS.BO.Framework.BODepartment"></asp:ObjectDataSource>


