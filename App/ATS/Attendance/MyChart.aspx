<%@ Page Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="MyChart.aspx.vb" Inherits="Employee_AttendanceByEmployee" title="TimeLive - ÇáãÎØØ ÇáÈíÇäí" meta:resourcekey="PageResource1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/ctlAttendanceChart.ascx" TagName="ctlAttendanceChart" TagPrefix="uc1" %>
<%@ Register Src="Controls/ctlAttendanceChartForDepartment.ascx" TagName="ctlAttendanceChartForDepartment" TagPrefix="uc1" %>

<asp:Content Content ID="Content1" ContentPlaceHolderID="C" Runat="Server">
 
     <telerik:RadTabStrip runat="server"
                             ID="RadTabStrip1"
                             Align="Justify"
                             AutoPostBack="True"
                             MultiPageID="RadMultiPage1"
                             SelectedIndex="0">
            <Tabs>

                 <telerik:RadTab Text="ÇáãáÎÕ ÇáÎÇÕ Èí" Width="50%" meta:resourcekey="RadTabResource1" ></telerik:RadTab>
                <telerik:RadTab Text="ãáÎÕ ÎÇÕ ÈÇáãæÙÝíä" Width="50%" meta:resourcekey="RadTabResource2" ></telerik:RadTab>
               
            </Tabs>
        </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">

         <telerik:RadPageView runat="server"  ID="RadPageView2">
               <uc1:ctlAttendanceChart id="ctlAttendanceChart1" runat="server"/>
            </telerik:RadPageView>

            <telerik:RadPageView runat="server"   ID="RadPageView1">
                  <uc1:ctlAttendanceChartForDepartment id="ctlAttendanceChartForDepartment1" runat="server"/>
            </telerik:RadPageView>

       
    </telerik:RadMultiPage>
 
</asp:Content>

