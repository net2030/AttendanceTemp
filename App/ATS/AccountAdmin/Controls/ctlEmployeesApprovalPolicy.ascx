<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlEmployeesApprovalPolicy.ascx.vb" Inherits="AccountAdmin_Controls_ctlEmployeesApprovalPolicy" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <style type="text/css">
            .demo-container {
                /*text-align: center;*/
            }

                .demo-container .wrapper {
                    display: inline;
                    display: inline-block;
                    zoom: 1;
                }

                .demo-container .RadListBox {
                    text-align: left;
                }

                .demo-container .block {
                    display: block !important;
                    width: 500px !important;
                }
        </style>

        <div class="demo-container size-narrow">

            <div class="wrapper">

                <table class="xAdminOption" style="width: 500px">
                    <tr>
                        <th class="caption" style="text-align: center;">
                            <asp:Literal ID="Literal9" runat="server" Text="«·„ÊŸ›Ì‰ «·–Ì‰  ‰ÿ»ﬁ ⁄·ÌÂ„ «·”Ì«”…" meta:resourcekey="Literal9Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <th class="FormViewSubHeader" style="text-align: center;">
                            <asp:Literal ID="Literal10" runat="server" Text="·≈÷«›… „ÊŸ› Ì—ÃÏ ‰ﬁ·Â „‰ √⁄·Ï «·Ï √”›· ··≈“«·… «·⁄ﬂ”" meta:resourcekey="Literal10Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <table class="xFormView" border="0">

                                <tr>
                                    <td>
                                        <asp:Label ID="lbldept" AssociatedControlID="ctlDepartmentTree1" runat="server" Text=" «·ﬁ”„ : " meta:resourcekey="lbldeptResource1" />

                                        <uc1:ctlDepartmentTree runat="server" ID="ctlDepartmentTree1" Width="350px" />

                                        <asp:Button ID="Button4" runat="server" Text="⁄—÷" meta:resourcekey="Button4Resource1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:radtextbox id="tbAvailableFilter" runat="server"
                                width="500px"
                                Emptymessage="»ÕÀ ⁄‰ „ÊŸ›..."
                                autocomplete="off"
                                onkeyup="filterAvialableList();" labelcssclass="" labelwidth="64px" meta:resourcekey="tbAvailableFilterResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:radlistbox runat="server"
                                id="AvailableListBox"
                                height="200px" width="500px" skin="Telerik"
                                allowtransfer="True"
                                allowtransferondoubleclick="True"
                                enabledraganddrop="True"
                                autopostbackontransfer="True"
                                transfertoid="SelectedListBox"
                                datatextfield="EmployeeName"
                                datavaluefield="EmployeeId"
                                cssclass="block" meta:resourcekey="AvailableListBoxResource1">
                                <buttonsettings areawidth="35px" position="Bottom" transferbuttons="All" />
                             </telerik:radlistbox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:radtextbox id="tbSelectedFilter" runat="server"
                                width="500px"
                                Emptymessage="»ÕÀ ⁄‰ „ÊŸ›..."
                                autocomplete="off"
                                onkeyup="filterSelectedList();" labelcssclass="" labelwidth="64px" meta:resourcekey="tbSelectedFilterResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:radlistbox
                                rendermode="Lightweight"
                                runat="server"
                                id="SelectedListBox"
                                selectionmode="Multiple"
                                transfertoid="AvailableListBox"
                                autopostbackontransfer="True"
                                datasourceid="dsEmployeesByPolicy"
                                datatextfield="EmployeeName"
                                datavaluefield="EmployeeId"
                                height="200px"
                                width="500px"
                                skin="Telerik" meta:resourcekey="SelectedListBoxResource1">
                                <buttonsettings transferbuttons="All" />
                            </telerik:radlistbox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" >
    <ContentTemplate>
   

    <div> 
    
        <table  class="xAdminOption" style="font-size:large;width:800PX">
                <tr>
                    <th class="caption" style="text-align:center;font-size:large;">
                        <asp:Literal ID="Literal9" runat="server" Text="«·„ÊŸ›Ì‰ «·–Ì‰  ‰ÿ»ﬁ ⁄·ÌÂ„ «·”Ì«”…"/>
                    </th>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" style="text-align:center;font-size:large;">
                        <asp:Literal ID="Literal10" runat="server" Text="·≈÷«›… „ÊŸ› Ì—ÃÏ ‰ﬁ·Â „‰ «·Ì„Ì‰ «·Ï «·Ì”«— Ê··Õ–› «·⁄ﬂ”" />
                    </th>
                </tr>
                <tr>
                    <td>
                        <table class="xFormView" border="0" style="font-size:large">

                            <tr>
                                <td colspan="3" >
                               <asp:Label ID="lbldept" AssociatedControlID="ctlDepartmentTree1"   runat="server" Text=" «·ﬁ”„ : " />
                       
                                    <uc1:ctlDepartmentTree runat="server" id="ctlDepartmentTree1" />
                         
                                    <asp:Button ID="Button4" runat="server" Text="⁄—÷" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                  <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" Font-Size="Large"  />
                               </td>
                            </tr>
                            <tr>
                                <th class="FormViewSubHeader" >
                                    «·„ÊŸ›Ì‰ «·„ Ê›—Ì‰ 
                                </th>
                                <th class="FormViewSubHeader" >
                                «·–Ì‰  „ «Œ Ì«—Â„ 
                                </th>
                            </tr>
                           <tr>
                               <td style="width:50%"> 
                                   <telerik:RadTextBox ID="tbAvailableFilter" runat="server"
                                        Width="385px"
                                        EmptyMessage="»ÕÀ ⁄‰ „ÊŸ›..."
                                        autocomplete="off"
                                        onkeyup="filterAvialableList();" />
                               </td>
                                <td>

                                    &nbsp;&nbsp;&nbsp; &nbsp;
                                    <telerik:RadTextBox ID="tbSelectedFilter" runat="server"
                                        Width="365px"
                                        EmptyMessage="»ÕÀ ⁄‰ „ÊŸ›..."
                                        autocomplete="off"
                                        onkeyup="filterSelectedList();" />

                                    
                                </td>
                        </tr>
                            <tr>

                                <td   style="width:335px" >
                                    <telerik:RadListBox ID="AvailableListBox" 
                                                        runat="server"
                                                        EnableDragAndDrop="True"
                                                        width="100%"  Height="300px"
                                                        TransferToID="SelectedListBox"
                                                        cssclass="ListBox"
                                                        SelectionMode="Multiple" 
                                                        style="text-align:right;"
                                                        AllowTransferOnDoubleClick="True"
                                                        DataTextField="EmployeeName" 
                                                        DataValueField="EmployeeId" Font-Names="Arial" Font-Size="Large" Skin="Default" ></telerik:RadListBox>

                                </td>

                                <td style="width:330px">
                                    <telerik:RadListBox ID="SelectedListBox" 
                                                        runat="server"
                                                        cssclass="ListBox"
                                                         style="text-align:right; top: 0px; left: 0px;"
                                                        SelectionMode="Multiple"  
                                                        TransferToID="AvailableListBox"
                                                        AutoPostBackOnTransfer="True"
                                                        width="100%"  Height="300px" 
                                                        DataTextField="EmployeeName" 
                                                        DataValueField="EmployeeId" Font-Names="Arial" Font-Size="Large" DataSourceID="dsEmployeesByPolicy" AllowTransfer="True" ><localization alltoleft="≈÷«›… «·Ã„Ì⁄" alltoright="Õ–› «·Ã„Ì⁄" toleft="≈÷«›…" toright="Õ–›" /><ButtonSettings TransferButtons="All"></ButtonSettings>
                                    </telerik:RadListBox>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

  </div>

    </ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">

    function filterSelectedList() {
        var listbox = $find("<%= SelectedListBox.ClientID%>");
        var textbox = $find('<%= tbSelectedFilter.ClientID %>');

        clearListEmphasis(listbox);
        createMatchingList(listbox, textbox.get_textBoxValue());
    }

    function filterAvialableList() {
        var listbox = $find("<%= AvailableListBox.ClientID%>");
        var textbox = $find('<%= tbAvailableFilter.ClientID%>');

        clearListEmphasis(listbox);
        createMatchingList(listbox, textbox.get_textBoxValue());
    }

    // Remove emphasis from matching text in ListBox
    function clearListEmphasis(listbox) {
        var re = new RegExp("</{0,1}em>", "gi");
        var items = listbox.get_items();
        var itemText;

        items.forEach
        (
            function (item) {
                itemText = item.get_text();
                item.set_text(itemText.replace(re, ""));
            }
        )
    }


    // Emphasize matching text in ListBox and hide non-matching items
    function createMatchingList(listbox, filterText) {
        if (filterText != "") {
            filterText = escapeRegExCharacters(filterText);

            var items = listbox.get_items();
            var re = new RegExp(filterText, "i");

            items.forEach
            (
                function (item) {
                    var itemText = item.get_text();

                    if (itemText.match(re)) {
                        item.set_text(itemText.replace(re, "<em>" + itemText.match(re) + "</em>"));
                        item.set_visible(true);
                    }
                    else {
                        item.set_visible(false);
                    }
                }
            )
        }
        else {
            var items = listbox.get_items();

            items.forEach
            (
                function (item) {
                    item.set_visible(true);
                }
            )
        }
    }

    function rlbAvailable_OnClientTransferring(sender, eventArgs) {
        // Transferred items retain the emphasized text, so it needs to be cleared.
        clearListEmphasis(sender);
        // Clear the list. Optional, but prevents follow up situation.
        clearFilterText();
        createMatchingList(sender, "");
    }

    function rbtnClear_OnClientClicking(sender, eventArgs) {
        clearFilterText();

        var listbox = $find("<%= SelectedListBox.ClientID%>");

                clearListEmphasis(listbox);
                createMatchingList(listbox, "");
            }

            // Clears the text from the filter.
            function clearFilterText() {
                var textbox = $find('<%= tbSelectedFilter.ClientID %>');
                textbox.clear();
            }

            // Escapes RegEx character classes and shorthand characters
            function escapeRegExCharacters(text) {
                return text.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
            }

</script>
<asp:ObjectDataSource ID="dsEmployeesByPolicy" runat="server" TypeName="ATS.BO.Framework.BOApprovalPolicy" SelectMethod="GetApprovalPolicyEmployees" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:Parameter Name="PolicyId" Type="Int32" />
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="200" Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>









