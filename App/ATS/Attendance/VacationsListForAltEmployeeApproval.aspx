<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="VacationsListForAltEmployeeApproval.aspx.vb" Inherits="Attendance_VacationsListForAltEmployeeApproval" title=" ÇáãæÇÝÞÉ Úáì ÇáÅÌÇÒÇÊ" Theme ="SkinFile" %>

<%@ Register Src="~/Attendance/Controls/ctlVacationsAltEmployeeApproval.ascx" TagPrefix="uc1" TagName="ctlVacationsAltEmployeeApproval" %>



<asp:Content Content ID="C" ContentPlaceHolderID="C" Runat="Server">
    <uc1:ctlVacationsAltEmployeeApproval runat="server" ID="ctlVacationsAltEmployeeApproval" />
</asp:Content>

