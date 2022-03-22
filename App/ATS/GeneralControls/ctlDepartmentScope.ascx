<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlDepartmentScope.ascx.vb" Inherits="AccountAdmin_Controls_ctlDepartmentScope"  %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<script type="text/javascript" >
    function pageLoad() {
       
        var treeView = $find("<%= RadTreeView1.ClientID%>");
        var nodes = treeView.get_allNodes();
        nodes[0].expand();
        console.log(nodes.length);
       for (var i = 0; i < nodes.length; i++) {
          
           if (nodes[i].get_checked()) {
               
               ExpandParent(nodes[i]);
           }
       }
   }

    function ExpandParent(node) {
        node = node.get_parent();

        while (node != null) {
            if (node.expand) {
                node.expand();
            }

            node = node.get_parent();
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

    function CheckAllChildren(nodes, checked) {
        var i;
        var test;
        for (i = 0; i < nodes.get_count() ; i++) {
            nodes.getNode(i).check();

            if (nodes.getNode(i).get_nodes().get_count() > 0) {
                CheckAllChildren(nodes.getNode(i).get_nodes(), checked);
            }
        }
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

    function clientNodeChecked1(sender, eventArgs) {
        var childNodes = eventArgs.get_node().get_nodes();


        var nodes = eventArgs.get_node().get_parent().get_nodes();
       
        var node = eventArgs.get_node();
        var isChecked = eventArgs.get_node().get_checked();
        if (isChecked) {
            CheckAllChildren(childNodes, isChecked);
            checkParentIfAllChildsChecked(node, nodes);
        }
        else {
            UnCheckAllChildren(childNodes, isChecked);
            UnCheckAllParent(node);
        }
    }


    function UpdateAllChildren(nodes, checked) {
        var i;
        var test;
        for (i = 0; i < nodes.get_count() ; i++) {
            if (checked) {
                nodes.getNode(i).check();
            }
            else {
                nodes.getNode(i).set_checked(false);
            }

            if (nodes.getNode(i).get_nodes().get_count() > 0) {
                UpdateAllChildren(nodes.getNode(i).get_nodes(), checked);
            }
        }
    }

    function checkParentIfAllChildsChecked(node,allnodes) {
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

    
</script>

   

    <div>
      <telerik:RadTreeView ID="RadTreeView1" Runat="server" 
                                             style="direction:rtl;text-align:right;"
                                             DataFieldID="DepartmentId" 
                                             DataFieldParentID="ParentId" 
                                             DataSourceID="SqlDataSource1"
                                             DataTextField="DepartmentName" 
                                           
                                             DataValueField="DepartmentId"   
                                             CheckBoxes="True"  
                                             OnClientNodeChecked="clientNodeChecked1">
           </telerik:RadTreeView>
  </div>

<div style="margin-top:40%;margin-left:50px;">
     <asp:Button ID="Button1" runat="server" Text="ÍÝÙ" />
    <br />
   <%-- <br />
     <telerik:RadButton ID="CancelButton" runat="server" OnClientClicked="closeWindow" AutoPostBack="false" CssClass="clicker" Text="Cancel" UseSubmitBehavior="False" EnableEmbeddedScripts="false" CausesValidation="False" RegisterWithScriptManager="False"/>--%>

</div>




<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ATSConnectionString %>" SelectCommand="Employees.SpDepartment_GetUserTreeForDropdownTree" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="EmployeeId" Type="Int32" />
         <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>







 