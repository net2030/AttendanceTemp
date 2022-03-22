<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlDeviceEmployees.ascx.vb" Inherits="AccountAdmin_Controls_ctlDeviceEmployees"  %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
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
                            <asp:Literal ID="Literal9" runat="server" Text="ÇáãæÙÝíä ÇáãÓÌáíä Ýí åÐÇ ÇáÌåÇÒ" meta:resourcekey="Literal9Resource1"  />
                        </th>
                    </tr>
                    <tr>
                        <th class="FormViewSubHeader" style="text-align: center;">
                            <asp:Literal ID="Literal10" runat="server" Text="áÅÖÇÝÉ ãæÙÝ íÑÌì äÞáå ãä ÃÚáì Çáì ÃÓÝá ááÅÒÇáÉ ÇáÚßÓ" meta:resourcekey="Literal10Resource1"  />
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <table class="xFormView" border="0">

                                <tr>
                                    <td>
                                        <asp:Label ID="lbldept" AssociatedControlID="ctlDepartmentTree1" runat="server" Text=" ÇáÞÓã : " meta:resourcekey="lbldeptResource1"  />

                                        <uc1:ctlDepartmentTree runat="server" ID="ctlDepartmentTree1" Width="350px" />

                                        <asp:Button ID="Button4" runat="server" Text="ÚÑÖ" meta:resourcekey="Button4Resource1"  />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:radtextbox id="tbAvailableFilter" runat="server"
                                width="500px"
                                Emptymessage ="ÈÍË Úä ãæÙÝ..."
                                autocomplete="off"
                                onkeyup="filterAvialableList();" labelcssclass="" labelwidth="64px" meta:resourcekey="tbAvailableFilterResource1"  />
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
                                cssclass="block" meta:resourcekey="AvailableListBoxResource1" >
                                <buttonsettings areawidth="35px" position="Bottom" transferbuttons="All" />
                             </telerik:radlistbox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:radtextbox id="tbSelectedFilter" runat="server"
                                width="500px"
                                Emptymessage ="ÈÍË Úä ãæÙÝ..."
                                autocomplete="off"
                                onkeyup="filterSelectedList();" labelcssclass="" labelwidth="64px" meta:resourcekey="tbSelectedFilterResource1"  />
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
                               
                                datatextfield="EmployeeName"
                                datavaluefield="EmployeeId"
                                height="200px"
                                width="500px"
                                skin="Telerik" meta:resourcekey="SelectedListBoxResource1" >
                                <buttonsettings transferbuttons="All" />
                            </telerik:radlistbox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
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










 