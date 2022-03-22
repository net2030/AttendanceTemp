<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="WorkExceptionTypes.aspx.vb" Inherits="AccountAdmin_frmWorkExceptionTypes" title="Work Exception Types" Theme="SkinFile" %>

<%@ Register Src="Controls/ctlWorkExceptionTypes.ascx" TagName="ctlWorkExceptionTypes"
    TagPrefix="uc3" %>

<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc3:ctlWorkExceptionTypes ID="ctlWorkExceptionTypes1" runat="server" />
    <br />
                    &nbsp;<br />
</asp:Content>