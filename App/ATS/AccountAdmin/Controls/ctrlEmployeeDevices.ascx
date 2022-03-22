<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctrlEmployeeDevices.ascx.vb" Inherits="AccountAdmin_Controls_ctrlEmployeeDevices"  %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<link href="../Styles.css" rel="stylesheet" />

<script type="text/javascript">
   //function ShowWindow() {
   //    alert("");
   //    var manager = GetRadWindowManager();
   //    // txtShowWindow is the id of a textarea on the page.
   //    var txtShowWindow = document.getElementById("txtShowWindow");
   //    var window1 = manager.getWindowByName("RadWindow1");
   //    window1.setUrl(txtShowWindow.value);
   //    window1.set_title(txtShowWindow.value);
   //    window1.show();
   //}
   
   function ChangeCheckBoxState(id, checkState) {
       var cb = document.getElementById(id);
       if (cb != null)
           cb.checked = checkState;
   }
   
   function ChangeAllCheckBoxStates(checkState) {
       // Toggles through all of the checkboxes defined in the CheckBoxIDs array
       // and updates their value to the checkState input parameter
       if (CheckBoxIDs != null) {
           for (var i = 0; i < CheckBoxIDs.length; i++)
               ChangeCheckBoxState(CheckBoxIDs[i], checkState);
       }
   }
   
   function ChangeHeaderAsNeeded() {
       // Whenever a checkbox in the GridView is toggled, we need to
       // check the Header checkbox if ALL of the GridView checkboxes are
       // checked, and uncheck it otherwise
       if (CheckBoxIDs != null) {
           // check to see if all other checkboxes are checked
           for (var i = 1; i < CheckBoxIDs.length; i++) {
               var cb = document.getElementById(CheckBoxIDs[i]);
               if (!cb.checked) {
                   // Whoops, there is an unchecked checkbox, make sure
                   // that the header checkbox is unchecked
                   ChangeCheckBoxState(CheckBoxIDs[0], false);
                   return;
               }
           }
   
           // If we reach here, ALL GridView checkboxes are checked
           ChangeCheckBoxState(CheckBoxIDs[0], true);
       }
   }
   </script>

   <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
    <ContentTemplate>

  <x:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="MachineId"
    DataSourceID="SqlDataSource1" SkinID="xgridviewSkinEmployee" Caption="ÞÇÆãÉ ÃáÃÌåÒÉ" Width="98%" >
    <Columns>
        
        <asp:BoundField DataField="MachineId" HeaderText="ÑÞã ÇáÌåÇÒ" />
         <asp:BoundField DataField="MachineName" HeaderText="ÅÓã ÇáÌåÇÒ" />
         <asp:BoundField DataField="Location" HeaderText="ãæÞÚ ÇáÌåÇÒ" />

        <asp:TemplateField>
         <HeaderTemplate>
            <asp:CheckBox 
               ID="chkCheckAll" runat="server" Width="15px" />
         </HeaderTemplate>
         <ItemTemplate>
            <asp:CheckBox 
               ID="chkSelect" runat="server" checked='<%# Eval("Selected").ToString().Equals("1")%>'  Width="15px" />
         </ItemTemplate>
         <HeaderStyle 
            HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
         <ItemStyle 
            HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
         </asp:TemplateField>
    </Columns>
</x:GridView>
      <table>
          <tr>
               <td style ="padding-left:50px;padding-right:20px;padding-top:20px;padding-bottom:20px">
                     <asp:Button ID="Button1" runat="server" Text="ÍÝÙ"  OnClientClick="CheckChanges();"/>
                 </td>
                 <td style ="padding-left:50px;padding-right:20px;padding-top:20px;padding-bottom:20px">
                   <asp:Label ID="Label2" runat ="server" Font-Size="Large" ></asp:Label>
                </td>
          </tr>
      </table>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ATSConnectionString %>" SelectCommand="Logs.SpGetEmployeeMacines" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="EmployeeId" SessionField="EmployeeId1" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>






 