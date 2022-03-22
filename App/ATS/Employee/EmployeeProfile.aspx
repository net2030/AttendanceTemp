<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="EmployeeProfile.aspx.vb" Inherits="Employee_EmployeeProfile" title="ÇáÕÝÍÉ ÇáÔÎÕíÉ" meta:resourcekey="PageResource1" %>
<%@ Register Src="Controls/ctlAccountEmployeeProfile.ascx" TagName="ctlAccountEmployeeProfile" TagPrefix="uc1" %>
<%@ Register Src="Controls/ctlAuthorization.ascx" TagName="ctlAuthorization" TagPrefix="uc1" %>

<asp:Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <table>
        <tr>
            <td>
                  <uc1:ctlAccountEmployeeProfile ID="CtlAccountEmployeeProfile1" runat="server" />
            </td>
            <td>
                 <uc1:ctlAuthorization ID="ctlAuthorization1" runat="server" visible="False" />
            </td>
        </tr>
    </table>
  
    <br />
    <br />
    
</asp:Content>

