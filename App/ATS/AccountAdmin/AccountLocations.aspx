<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="AccountLocations.aspx.vb" Inherits="AccountAdmin_frmAccountLocations" title="TimeLive - Locations" Theme="SkinFile" %>

<%@ Register Src="Controls/ctlAccountLocationList.ascx" TagName="ctlAccountLocationList"
    TagPrefix="uc3" %>

<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc3:ctlAccountLocationList ID="CtlAccountLocationList1" runat="server" />
    <br />
                    &nbsp;<br />
</asp:Content>