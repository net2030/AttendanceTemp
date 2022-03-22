<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAuthorization.ascx.vb" Inherits="AccountAdmin_Controls_ctlAccountWorkTypeList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../ctlDepartmentTree.ascx" TagName="ctlDepartmentTree" TagPrefix="uc1" %>


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
                    /*width: 400px !important;*/
                }
        </style>

        <%--<div class="demo-container size-narrow">
    <div class="wrapper">
        <table  class="xAdminOption" style="font-size:large;width:650px">
                <tr>
                    <th class="caption" style="text-align:center;font-size:large;">
                        <asp:Literal ID="Literal9" runat="server" Text="ÊÝæíÖ ãæÙÝ ááÞíÇã ÈÇáãåÇã" meta:resourcekey="Literal9Resource1"/>
                    </th>
                </tr>
                <tr>
                    <th class="FormViewSubHeader" style="text-align:center;font-size:large;">
                        <asp:Literal ID="Literal10" runat="server" Text="áÊÝæíÖ ãæÙÝ íÑÌì äÞáå ãä Çáíãíä Çáì ÇáíÓÇÑ áÅáÛÇÁ ÇáÊÝæíÖ ÇáÚßÓ" meta:resourcekey="Literal10Resource1" />
                    </th>
                </tr>
                <tr>
                    <td>
                        <table class="xFormView" border="0" style="font-size:large">

                            <tr>
                                <td colspan="3" >
                               <asp:Label ID="lbldept" AssociatedControlID="ctlDepartmentTree1"   runat="server" Text=" ÇáÞÓã : " meta:resourcekey="lbldeptResource1" />
                       
                                    <uc1:ctlDepartmentTree runat="server" id="ctlDepartmentTree1" />
                         
                                    <asp:Button ID="Button4" runat="server" Text="ÚÑÖ" meta:resourcekey="Button4Resource1" />
                                </td>
                            </tr>
                            <tr>
                                <th class="FormViewSubHeader" >
                                    <asp:Literal ID="Literal1" runat="server" Text=" ÇáãæÙÝíä ÇáãÊæÝÑíä " meta:resourcekey="Literal1Resource1"></asp:Literal>
                                </th>
                                <th class="FormViewSubHeader" >
                                    <asp:Literal ID="Literal2" runat="server" Text=" ÇáÐíä Êã ÊÝæíÖåã " meta:resourcekey="Literal2Resource1"></asp:Literal>
                                </th>
                            </tr>
                            <tr>
                               <td style="width:50%"> 
                                   <telerik:RadTextBox ID="tbAvailableFilter" runat="server"
                                        Width="300px"
                                        EmptyMessage="ÈÍË Úä ãæÙÝ..."
                                        autocomplete="off"
                                        onkeyup="filterAvialableList();" LabelCssClass="" LabelWidth="64px" meta:resourcekey="tbAvailableFilterResource1" />
                               </td>
                                <td>
                                  
                                    &nbsp;<telerik:RadTextBox ID="tbSelectedFilter" runat="server"
                                        Width="300px"
                                        EmptyMessage="ÈÍË Úä ãæÙÝ..."
                                        autocomplete="off"
                                        onkeyup="filterSelectedList();" LabelCssClass="" LabelWidth="64px" meta:resourcekey="tbSelectedFilterResource1" />

                                    
                                </td>
                        </tr>
                            <tr>

                                <td   style="width:300px" >
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
                                                        DataValueField="EmployeeId" Font-Names="Arial" Font-Size="Large" meta:resourcekey="AvailableListBoxResource1" >
                                        <buttonsettings transferbuttons="All" />
                                    </telerik:RadListBox>

                                </td>
                                </tr>
                            <tr>
                                <td style="width:330px">
                                    <telerik:RadListBox ID="SelectedListBox" 
                                                        runat="server"
                                                        cssclass="ListBox"
                                                         style="text-align:right; top: 0px; left: 0px;"
                                                        SelectionMode="Multiple"  
                                                        TransferToID="AvailableListBox"
                                                        AutoPostBackOnTransfer="True"
                                                        width="330px" Height="300px" 
                                                        DataTextField="EmployeeName" 
                                                        DataValueField="AuthorizedID" Font-Names="Arial" Font-Size="Large" AllowTransfer="True" meta:resourcekey="SelectedListBoxResource1" ><localization alltoleft="ÅÖÇÝÉ ÇáÌãíÚ" alltoright="ÍÐÝ ÇáÌãíÚ" toleft="ÅÖÇÝÉ" toright="ÍÐÝ" /><ButtonSettings TransferButtons="All"></ButtonSettings>
                                    </telerik:RadListBox>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

  </div>
 </div>--%>


        <div class="demo-container size-narrow">

            <div class="wrapper">

                <table class="xAdminOption" style="width: 500px">
                    <tr>
                        <th class="caption" style="text-align: center;">
                            <asp:Literal ID="Literal9" runat="server" Text="ÊÝæíÖ ãæÙÝ ááÞíÇã ÈÇáãåÇã" meta:resourcekey="Literal9Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <th class="FormViewSubHeader" style="text-align: center;">
                            <asp:Literal ID="Literal10" runat="server" Text="áÊÝæíÖ ãæÙÝ íÑÌì äÞáå ãä ÃÚáì Çáì ÃÓÝá áÅáÛÇÁ ÇáÊÝæíÖ ÇáÚßÓ" meta:resourcekey="Literal10Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <table class="xFormView" border="0">

                                <tr>
                                    <td>
                                        <asp:Label ID="lbldept" AssociatedControlID="ctlDepartmentTree1" runat="server" Text=" ÇáÞÓã : " meta:resourcekey="lbldeptResource1" />

                                        <uc1:ctlDepartmentTree runat="server" ID="ctlDepartmentTree1" Width="280px" IsRequired="true" />

                                        <asp:Button ID="Button4" runat="server" Text="ÚÑÖ" meta:resourcekey="Button4Resource1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <telerik:radtextbox id="tbAvailableFilter" runat="server"
                                width="500px"
                                Emptymessage ="ÈÍË Úä ãæÙÝ..."
                                autocomplete="off"
                                onkeyup="filterAvialableList();" labelcssclass="" labelwidth="64px" meta:resourcekey="tbAvailableFilterResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:radlistbox runat="server"
                                id="AvailableListBox"
                                height="200px" width="500px" skin="Telerik"
                                allowtransfer="true"
                                allowtransferondoubleclick="True"
                                enabledraganddrop="True"
                                autopostbackontransfer="True"
                                transfertoid="SelectedListBox"
                                datatextfield="EmployeeName"
                                datavaluefield="EmployeeId"
                                buttonsettings-areawidth="35px"
                                buttonsettings-position="Bottom"
                                cssclass="block">
                             </telerik:radlistbox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:radtextbox id="tbSelectedFilter" runat="server"
                                width="500px"
                                Emptymessage ="ÈÍË Úä ãæÙÝ..."
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
                                datatextfield="EmployeeName"
                                datavaluefield="AuthorizedID"
                                height="100px"
                                width="500px"
                                skin="Telerik">
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
