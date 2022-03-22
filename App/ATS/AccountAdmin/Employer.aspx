<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="Employer.aspx.vb" Inherits="AccountAdmin_frmEmployer" title="Work Exception Types" Theme="SkinFile" %>

<%@ Register Src="Controls/ctlEmployer.ascx" TagName="ctlEmployer"
    TagPrefix="uc3" %>

<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc3:ctlEmployer ID="ctlEmployer1" runat="server" />
    <br />
                    &nbsp;<br />
</asp:Content>