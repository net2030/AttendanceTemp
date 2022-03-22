<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="VacationTypes.aspx.vb" Inherits="AccountAdmin_frmVacationTypes" title="Work Exception Types" Theme="SkinFile" %>

<%@ Register Src="Controls/ctlVacationTypes.ascx" TagName="ctlVacationTypes"
    TagPrefix="uc3" %>

<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc3:ctlVacationTypes ID="ctlVacationTypes1" runat="server" />
    <br />
                    &nbsp;<br />
</asp:Content>