<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlDepartment1.ascx.vb" Inherits="AccountAdmin_Controls_ctlDepartment1" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<style type="text/css">
 table #example {
  border-collapse: separate;
  border-spacing: 5px;
  border: solid;
  
}
#example td, #example th {
  padding: 1px;
  border: none;
}
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
  
        <table id="example1">
            <tr>
                 <asp:Label runat="server" ID="Label2"  Text="" Visible="false"/>
            </tr>
             <tr >
                <td style="padding-top:10px">
                    <asp:Label runat="server" ID="Label1" AssociatedControlID="txtDepartmentId" Text="رقم الحاسب:" />
                        </td>
                <td style="padding-top:10px">
                    <asp:TextBox ID="txtDepartmentId" runat="server" CssClass="text" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td style="padding-top:10px">
                    <asp:Label runat="server" ID="lblParentNode" AssociatedControlID="ctlDepartmentTree1"
                        Text="الإدارة التابع لها:" />
                </td>
                <td style="padding-top:10px">
                    <uc1:ctlDepartmentTree runat="server" ID="ctlDepartmentTree1" />
                </td>
            </tr>
            <tr style="padding-top:10px">
                <td style="padding-top:10px">
                    <asp:Label runat="server" ID="lblNode" AssociatedControlID="txtNode" Text="إسم القسم:" />
                    
                        </td>
                <td style="padding-top:10px">
                    <asp:TextBox ID="txtNode" runat="server" CssClass="text" Width="300px" />&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="txtNode" SetFocusOnError="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top:10px;padding-bottom:20px">
                     <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="submit"  />
                             
                <asp:Button ID="btnCancel" runat="server" Text="إلغاء" CssClass="submit" 
                    onclick="btnCancel_Click" CausesValidation="false" />
              
                 <asp:Button ID="btnDelete" runat="server" Text="حذف" CssClass="submit" 
                    OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>"  />
                </td>
            </tr>
        </table>
        
    </div>
         </ContentTemplate>
</asp:UpdatePanel>
  <asp:Button ID="Button1" runat="server" Text="حذف" CssClass="submit" 
                    OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>"  />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
         <table id="example">
            <tr>
                <td>
        <telerik:RadTreeView ID="RadTreeView1" Runat="server" 
                              causesvalidation= false
                              DataFieldID="DepartmentId" 
                              DataFieldParentID="ParentID" 
                              DataSourceID="SqlDataSource1" 
                              DataTextField="DepartmentName" 
                              DataValueField="DepartmentId"
                              OnNodeClick="RadTreeView1_NodeClick"
                              autopostback="true"
                             
                              Font-Names="Times New Roman" Font-Size="14pt">
                             
        </telerik:RadTreeView>

</td>
                </tr>
             </table>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllDepartment" TypeName="ATS.BO.Framework.BODepartment">
   
</asp:ObjectDataSource>


<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ATSConnectionString %>" SelectCommand="Employees.SpDepartment_GetUserTreeForDropdownTree" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="EmployeeId" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>



