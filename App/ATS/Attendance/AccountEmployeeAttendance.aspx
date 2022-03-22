<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="AccountEmployeeAttendance.aspx.vb" Inherits="Employee_frmAccountEmployeeAttendance" title="ÞÇÆãÉ ÇáÍÖæÑ æÇáÅäÕÑÇÝ" Theme ="SkinFile" meta:resourcekey="PageResource1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%--<%@ Register Src="Controls/ctlAccountEmployeeAttendance.ascx" TagName="ctlAccountEmployeeAttendance" TagPrefix="uc1" %>
<%@ Register Src="Controls/ctlAccountEmployeeAttendanceByDepartment.ascx" TagName="ctlAccountEmployeeAttendanceByDepartment" TagPrefix="uc1" %>--%>
<%@ Register Src="~/Attendance/Controls/ctlAccountEmployeeAttendanceShift.ascx" TagPrefix="uc1" TagName="ctlAccountEmployeeAttendanceShift" %>
<%@ Register Src="~/Attendance/Controls/ctlAccountEmployeeAttendanceByDepartmentShift.ascx" TagPrefix="uc1" TagName="ctlAccountEmployeeAttendanceByDepartmentShift" %>
<%@ Register Src="~/Attendance/Controls/ctlAccountEmployeeAttendance.ascx" TagPrefix="uc1" TagName="ctlAccountEmployeeAttendance" %>
<%@ Register Src="~/Attendance/Controls/ctlAccountEmployeeAttendanceByDepartment.ascx" TagPrefix="uc1" TagName="ctlAccountEmployeeAttendanceByDepartment" %>





<asp:Content ID="Content1" ContentID="C" ContentPlaceHolderID="C" Runat="Server">

        <style type="text/css">
    .style1 {
        height: 23px;
    }

    .Grid td {
        background-color: White;
        color: Black;
        font-size: 10pt;
        line-height: 200%;
    }

    .Grid th {
        background-color: #336699;
        color: White;
        font-size: 10pt;
        line-height: 200%;
    }

    .ChildGrid td {
        background-color: #9B945F !important;
        color: White;
        font-size: 10pt;
        line-height: 200%;
    }

    .ChildGrid th {
        background-color: #0c5299 !important;
        color: black;
        font-size: 10pt;
        line-height: 200%;
    }


</style>

    <telerik:RadTabStrip runat="server"
                             ID="RadTabStrip1"
                             Align="Justify"
                             AutoPostBack="True"
                             MultiPageID="RadMultiPage1"
                             SelectedIndex="0"
                             causesvalidation="false">
            <Tabs>

                 <telerik:RadTab Text="ÇäÇ"  Width="50%" meta:resourcekey="MeResource1"></telerik:RadTab>
                <telerik:RadTab Text="ÇáãæÙÝíä" Width="50%" meta:resourcekey="EmpResource1"></telerik:RadTab>
               
            </Tabs>
        </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" causesvalidation="false">

         <telerik:RadPageView runat="server"  ID="RadPageView2">

             <uc1:ctlAccountEmployeeAttendance runat="server" ID="ctlAccountEmployeeAttendanceShift" />
            </telerik:RadPageView>
       
            <telerik:RadPageView runat="server"  ID="RadPageView1">
   
                <uc1:ctlAccountEmployeeAttendanceByDepartment runat="server" ID="ctlAccountEmployeeAttendanceByDepartmentShift" />
            </telerik:RadPageView>
     
       
    </telerik:RadMultiPage>





     <%-- <uc1:ctlAccountEmployeeAttendanceByDepartmentShift runat="server" ID="ctlAccountEmployeeAttendanceByDepartmentShift" />

     <uc1:ctlAccountEmployeeAttendanceShift runat="server" ID="ctlAccountEmployeeAttendanceShift" Visible="false" />--%>



  <%--  <uc1:ctlAccountEmployeeAttendance runat="server" ID="ctlAccountEmployeeAttendance" Visible="false" />
    <uc1:ctlAccountEmployeeAttendanceByDepartment runat="server" ID="ctlAccountEmployeeAttendanceByDepartment" />--%>
</asp:Content>