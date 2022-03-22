<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguageUserControl.ascx.cs" Inherits="WebApplication1.UserControl.LanguageUserControl" %>
<table>
    <tr>
        <td>
            <asp:ImageButton runat="server" ID="ImgBtn_En" ClientIDMode="Static" ImageUrl="~/Images/EN.png" Height="20px" Width="30px"
                OnClick="ImgBtn_En_Click" CausesValidation="false" />

        </td>
        <td>
            <asp:ImageButton runat="server" ID="ImgBtn_Ar" ClientIDMode="Static" ImageUrl="~/Images/AR.png" Height="20px" Width="30px"
                OnClick="ImgBtn_Fr_Click" CausesValidation="false" />
        </td>
    </tr>
</table>

