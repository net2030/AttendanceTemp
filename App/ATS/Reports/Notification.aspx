<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="Notification.aspx.vb" Inherits="Report_Notification" title=" ÇáÅäÐÇÑÇÊ" %>

<%@ Register Src="~/Reports/Controls/ctlNotificationNew.ascx" TagPrefix="uc1" TagName="ctlNotificationNew" %>




<%--<%@ Register Src="Controls/ctlNotification.ascx" TagName="ctlNotification" TagPrefix="uc1" %>--%>
<asp:Content Content ID="Content1" ContentPlaceHolderID="C" Runat="Server">
<%--    <uc1:ctlNotification id="ctlNotification1" runat="server"> </uc1:ctlNotification>--%>
    <uc1:ctlNotificationNew runat="server" ID="ctlNotificationNew" />

</asp:Content>

