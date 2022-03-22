<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlDepartmentScope.ascx.vb" Inherits="AccountAdmin_Controls_ctlDepartmentScope"  %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<link href="../Styles.css" rel="stylesheet" />

<script type="text/javascript" >
   
    function clientNodeChecked(sender, eventArgs) {
        unsaved = false;
        unchanged = false;
        var childNodes = eventArgs.get_node().get_nodes();

        var nodes = eventArgs.get_node().get_parent().get_nodes();
        
        var node = eventArgs.get_node();
        node.expand();
        var isChecked = eventArgs.get_node().get_checked();
        //ExpandAllChildren(childNodes, isChecked);
        if (isChecked) {
            
            CheckAllChildren(childNodes, isChecked);
            checkParentIfAllChildsChecked(node, nodes);
        }
        else {
            UnCheckAllChildren(childNodes, isChecked);
            UnCheckAllParent(node);
        }
       
      
    }

    function CheckAllChildren(nodes, checked) {
        var i;
        var test;
        for (i = 0; i < nodes.get_count() ; i++) {
            nodes.getNode(i).check();

            //alert(i);
            if (nodes.getNode(i).get_nodes().get_count() > 0) {
                CheckAllChildren(nodes.getNode(i).get_nodes(), checked);
            }
        }
    }

    function checkParentIfAllChildsChecked(node, allnodes) {
        var flag = false
        //alert(allnodes.get_count());
        d:
            for (i = 0; i < allnodes.get_count() ; i++) {
                var IsChecked = allnodes.getNode(i).get_checked();
                //alert(allnodes.getNode(i).get_checked());
                if (!IsChecked) {
                    return;
                }


            }
        node.get_parent().set_checked(true);


    }

    function UnCheckAllChildren(nodes, checked) {
        var i;
        var test;
        for (i = 0; i < nodes.get_count() ; i++) {
            nodes.getNode(i).set_checked(false);

            if (nodes.getNode(i).get_nodes().get_count() > 0) {
                UnCheckAllChildren(nodes.getNode(i).get_nodes(), checked);
            }
        }
    }

    function UnCheckAllParent(node) {
        if (!node.get_checked()) {
            while (node.get_parent().set_checked != null) {
                node.get_parent().set_checked(false);
                node = node.get_parent();
            }
        }
    }

   

    unchanged =true
    unsaved = true;
   
    function CheckChanges() {
        if (unchanged) {
            alert("·„  Õ’· «Ì  €Ì—« ");
            return false
        }
        else
            return true;
        unsaved = true;
    }

    window.onbeforeunload = function () {
        if (!window.unsaved) {
            return '·„  Õ›Ÿ ÕœÊœ ’·«ÕÌ… «·«œ«—… !!.';
        }
    };
</script>

   <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
    <ContentTemplate>

        <table >
            <tr>
                <td style ="padding-left:50px;padding-right:20px;padding-top:20px;padding-bottom:20px">
                   <%--  <asp:Button ID="Button1" runat="server" Text="Save"  OnClientClick="CheckChanges();" />--%>
                    <asp:ImageButton ID="Button1" runat="server" ImageUrl="../../images/save.png" Width="40" Height ="40" OnClientClick="return CheckChanges();"  />
                 </td>
                 <td style ="padding-left:50px;padding-right:20px;padding-top:20px;padding-bottom:20px">
                   <asp:Label ID="Label2" runat ="server" Font-Size="Large" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadTreeView ID="RadTreeView1" Runat="server" 
                                             
                                             DataFieldID="DepartmentId" 
                                             DataFieldParentID="ParentId" 
                                             DataSourceID="SqlDataSource1"
                                             DataTextField="DepartmentName" 
                                           
                                             DataValueField="DepartmentId"   
                                             CheckBoxes="True"  
                                             OnClientNodeChecked="clientNodeChecked" Font-Size="Large">
                    </telerik:RadTreeView>
              </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ATSConnectionString %>" SelectCommand="Employees.SpDepartment_GetUserTreeForDropdownTree" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="EmployeeId" Type="Int32" />
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>







 