<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="ManualLog.aspx.vb" Inherits="Attendance_ManualLog"  title="تسجيل بصمة يدويا" meta:resourcekey="PageResource1"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

    <%@ Register Src="Controls/ctlManualLog.ascx" TagName="ctlManualLog"
    TagPrefix="uc1" %>
<asp:Content ContentID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlManualLog ID="ctlManualLog1" runat="server" />
</asp:Content>
