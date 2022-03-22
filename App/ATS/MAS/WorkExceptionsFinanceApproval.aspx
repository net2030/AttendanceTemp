<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="WorkExceptionsFinanceApproval.aspx.vb" Inherits="WorkExceptionsFinanceApproval" title="TimeLive - ÇáãæÇÝÞÉ Úáì ÇáÅÓÊËäÇÁ" Theme ="SkinFile" %>

<%@ Register Src="Controls/ctlWorkExceptionApproval.ascx" TagName="ctlWorkExceptionApproval"
    TagPrefix="uc1" %>
<%@ Register Src="~/GeneralControls/LanguageUserControl.ascx" TagPrefix="uc1" TagName="LanguageUserControl" %>

<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:LanguageUserControl runat="server" id="LanguageUserControl" />
    <uc1:ctlWorkExceptionApproval id="ctlWorkExceptionApproval1" runat="server">
    </uc1:ctlWorkExceptionApproval>
</asp:Content>

