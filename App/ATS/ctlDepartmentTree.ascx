<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlDepartmentTree.ascx.vb" Inherits="ctlDepartmentTree" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<script type="text/javascript">
    function nodeClicking(sender, args) {
        var comboBox = $find("<%= RadComboBox1.ClientID %>");

        var node = args.get_node()

        comboBox.set_text(node.get_text());

        comboBox.trackChanges();
        comboBox.get_items().getItem(0).set_text(node.get_text());
        comboBox.commitChanges();

        comboBox.hideDropDown();
    }


    function OnClientDropDownOpenedHandler(sender, eventArgs) {
        var tree = sender.get_items().getItem(0).findControl("RadTreeView1");
        var selectedNode = tree.get_selectedNode();
        if (selectedNode) {
            selectedNode.scrollIntoView();
        }
    }


    function nodeClicking1(sender, args) {
        var comboBox = $find("<%= RadComboBox1.ClientID%>");
        
        var divs = document.getElementById("divRadTreeViewDepartment");
        
        var tree = $find(divs.getElementsByClassName("RadTreeView")[0].id);
        tree.unselectAllNodes();
        comboBox.set_text("...........");
       
        var node = args.get_node()
        if (node.get_parent()) {
            comboBox.set_text(node.get_parent().get_text());

            comboBox.trackChanges();
            comboBox.get_items().getItem(0).set_text(node.get_text());
            var x = comboBox.get_text();
            var node1 = tree.findNodeByText(x);
            node1.select();
        }
       

        //comboBox.commitChanges();

        //comboBox.hideDropDown();
    }

</script>

            <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="350px"
                                 Style="vertical-align: middle;"
                                 OnClientDropDownOpened="OnClientDropDownOpenedHandler"
                                  EmptyMessage="....................................."   
                                 OpenDropDownOnFocus="True">
                <ItemTemplate>
                    <div id="divRadTreeViewDepartment">
                        <telerik:RadTreeView runat="server" ID="RadTreeView1"   
                                             OnClientNodeClicking="nodeClicking" 
                                             Width="100%" Height="400px" 
                                             DataFieldID="DepartmentId" 
                                             DataFieldParentID="ParentId" 
                                             DataSourceID="SqlDataSource1"
                                             DataTextField="DepartmentName" 
                                             DataValueField="DepartmentId" causesvalidation= "false"  >
                                             

                        </telerik:RadTreeView>
                    </div>
                </ItemTemplate>
                <Items>
                    <telerik:RadComboBoxItem Text=""></telerik:RadComboBoxItem>
                </Items>
            </telerik:RadComboBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="RadComboBox1" Enabled="false" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>


<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ATSConnectionString %>" SelectCommand="Employees.SpDepartment_GetUserTreeForDropdownTree" SelectCommandType="StoredProcedure">
    <SelectParameters>
                    <asp:SessionParameter DefaultValue="" Name="EmployeeId" SessionField="AccountEmployeeId" Type="Int32" />
                    <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>

         <%--   <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LiveConnectionString %>" SelectCommand="SpDepartment_GetUserTreeForDropdownTree" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="" Name="EmployeeId" SessionField="AccountEmployeeId" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>--%>



