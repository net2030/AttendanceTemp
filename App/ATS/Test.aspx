<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Test.aspx.vb" Inherits="Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript">

        function pageLoad1() {


            var updateProgress = $get('<%= updateProgress.ClientID%>');
        var dynamicLayout = '<%= updateProgress.DynamicLayout.ToString().ToLower()%>';


        if (dynamicLayout) {
            updateProgress.style.display = "block";
        }
        else {
            updateProgress.style.visibility = "visible";
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:UpdateProgress ID="updateProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Button ID="Button1" ClientIDMode="Static" runat="server" Text="Button" OnClientClick="pageLoad1();" />

        </div>
    </form>
</body>
</html>
