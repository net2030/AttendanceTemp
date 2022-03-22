<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<%@ Import Namespace="System.Web.Services" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

 


<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    
    <title></title>
   
    <script type="text/javascript">
        function AddRow() {
            //Reference the GridView.
            var gridView = document.getElementById("<%=GridView2.ClientID%>");

        //Reference the TBODY tag.
        var tbody = gridView.getElementsByTagName("tbody")[0];

        //Reference the first row.
        var row = tbody.getElementsByTagName("tr")[1];

        //Check if row is dummy, if yes then remove.
        if (row.getElementsByTagName("td")[0].innerHTML.replace(/\s/g, '') == "") {
            tbody.removeChild(row);
        }

        //Clone the reference first row.
        row = row.cloneNode(true);

        //Add the Name value to first cell.
        var txtName = document.getElementById("<%=txtName.ClientID %>");
    SetValue(row, 1, "name", txtName);

        //Add the Country value to second cell.
    var txtCountry = document.getElementById("<%=txtCountry.ClientID %>");
    SetValue(row, 2, "country", txtCountry);

        //Add the row to the GridView.
    tbody.appendChild(row);
    return false;
};

function SetValue(row, index, name, textbox) {
    //Reference the Cell and set the value.
    row.cells[index].innerHTML = textbox.value;

    //Create and add a Hidden Field to send value to server.
    var input = document.createElement("input");
   input.type = "hidden";
    input.name = name;
    input.value = textbox.value;
    row.cells[index].appendChild(input);

    //Clear the TextBox.
    textbox.value = "";
}
</script>

</head>

<body>
<form id="ff" runat="server">
    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
     <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
  <x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Font-Size="Large"
                    DataKeyNames="PolicyId" Caption="قائمة سياسات الدوام" 
                    SkinID="xgridviewSkinEmployee" Width="50%" CssClass="TableView">
            <Columns>

                <asp:BoundField ItemStyle-Width="10%" ItemStyle-Wrap="false" DataField="PolicyId" HeaderText="الرقم" ReadOnly="True" />

                <asp:BoundField DataField="PolicyName" HeaderText="إسم السياسة" />


    
                <asp:TemplateField HeaderText="تعديل" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton id="EditLinkButton" runat="server" Text="" CommandName="Edit"  ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" cssclass="edit_button" HorizontalAlign="Center" />
                </asp:TemplateField>
                   
                <asp:TemplateField HeaderText="حذف" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton id="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                            OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" cssclass="delete_button" HorizontalAlign="Center" />
                </asp:TemplateField>

            </Columns>
        </x:GridView>

    

<br />
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 150px">
            Name:<br />
            <asp:TextBox ID="TextBox1" runat="server" Width="140" Text="" />
        </td>
        <td style="width: 150px">
            Country:<br />
            <asp:TextBox ID="TextBox2" runat="server" Width="140" Text="" />
        </td>
        <td style="width: 100px">
            <br />
            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClientClick="return AddRow()" />
        </td>
    </tr>
</table>
<br />
<asp:Button ID="Button1" Text="Submit" runat="server" />



    </form>

        
</body>

</html>