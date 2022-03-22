<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMyReports.ascx.vb" Inherits="Employee_Controls_ctlMyReport" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

      <table class="xAdminOption" width="400px" >

        <tr>
            <th colspan="6" class="caption" style="text-align:center;font-size:18px;">
                <asp:Literal ID="Literal9" runat="server" Text=" ÞÇÆãÉ ÇáÊÞÇÑíÑ " meta:resourcekey="Literal9Resource1"/>
            </th>
        </tr>

             <tr>
                <td>

                    <a href="../Reports/AttendanceByEmployee.aspx">
                        <img src="../Images/EmployeeTimeOffReport.gif" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/AttendanceByEmployee.aspx"   title="Dashboard">
                        <asp:Literal ID="Literal2" runat="server" Text="ÊÞÑíÑ ÇáÍÖæÑ ÍÓÈ ÇáãæÙÝ" meta:resourcekey="Literal2Resource1"></asp:Literal>
                    </a>
                </td>
            </tr>
          
            <tr>
                <td>

                    <a href="../Reports/DailyAttendance.aspx">
                        <img src="../Images/AttendanceDetail.gif" alt="HTML tutorial" width="42" height="42">
                    </a>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                <td>
                    <a href="../Reports/DailyAttendance.aspx" title="Dashboard">
                        <asp:Literal ID="Literal3" runat="server" Text="ÊÞÑíÑ ÍÖæÑ æÅäÕÑÇÝ ÍÓÈ ÇáÞÓã" meta:resourcekey="Literal3Resource1"></asp:Literal>
                    </a>
                </td>
            </tr>
            <tr>
                <td>

                    <a href="../Reports/LateComersByDate.aspx">
                        <img src="../Images/EmployeeTimeOffDetail.gif" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/LateComersByDate.aspx"  title="Dashboard"> 
                        <asp:Literal ID="Literal4" runat="server" Text="ÊÞÑíÑ ÇáãÊÃÎÑíä ÍÓÈ ÇáÊÇÑíÎ" meta:resourcekey="Literal4Resource1"></asp:Literal>
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                    <a href="../Reports/AbsentsByDate.aspx">
                        <img src="../Images/TimeOffTypes.gif" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/AbsentsByDate.aspx"  title="Dashboard">
                        <asp:Literal ID="Literal5" runat="server" Text="ÊÞÑíÑ ÇáÛíÇÈ ÍÓÈ ÇáÞÓã" meta:resourcekey="Literal5Resource1"></asp:Literal>
                    </a>
                </td>
            </tr>
             <tr>
                <td>
                    
                    <a href="../Reports/VacationsByDate.aspx">
                        <img src="../Images/HolidayManagement.gif" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/VacationsByDate.aspx"  > 
                          <asp:Literal ID="Literal6" runat="server" Text=" ÊÞÑíÑ ÅÌÇÒÇÊ ÇáãæÙÝíä" meta:resourcekey="Literal6Resource1"></asp:Literal>
                    </a>
                </td>
            </tr>
           <tr>
                <td>
                    
                    <a href="../Reports/AttendanceSummary.aspx">
                        <img src="../Images/details-icon.png" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/AttendanceSummary.aspx"  >
                         <asp:Literal ID="Literal7" runat="server" Text="  ÊÞÑíÑ ãáÎÕ ÇáÍÖæÑ æÇáÇäÕÑÇÝ" meta:resourcekey="Literal7Resource1"></asp:Literal>
                    </a>
                </td>
            </tr>

<%--           <tr>
                <td>
                    
                    <a href="../Reports/EmployeesReport.aspx">
                        <img src="../Images/Employees.gif" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/EmployeesReport.aspx"  > 
                         <asp:Literal ID="Literal8" runat="server" Text="ÊÞÑíÑ ÈíÇäÇÊ ÇáãæÙÝíä" meta:resourcekey="Literal8Resource1"></asp:Literal>
                    </a>
                </td>
            </tr>--%>

           <tr>
                <td>
                    
                    <a href="../Reports/FingerprintNotRegistered.aspx">
                        <img src="../Images/fingerprint.gif" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/FingerprintNotRegistered.aspx"  > 
                        <asp:Literal ID="Literal10" runat="server" Text="ÊÞÑíÑ ÊÓÌíá ÈÕãÇÊ ÇáãæÙÝíä" meta:resourcekey="Literal10Resource1"></asp:Literal>
                    </a>
                </td>
      

          <tr>
                <td>
                    
                    <a href="../Reports/Notification.aspx">
                        <img src="../Images/BillingReport.gif" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/Notification.aspx"  >
                          <asp:Literal ID="Literal14" runat="server" Text="ÇáÅäÐÇÑÇÊ " meta:resourcekey="Literal14Resource1"/>
                    </a>
                </td>
            </tr>
         <%-- <tr>
            <th colspan="6" class="caption" style="text-align:center;font-size:18px;">
                <asp:Literal ID="Literal1" runat="server" Text=" ÊÞÇÑíÑ ÇáÑÓæãÇÊ ÇáÈíÇäíÉ " meta:resourcekey="Literal1Resource1"/>
            </th>
        </tr>
             <tr>
                <td>
                    
                    <a href="../Reports/AttendanceChartByEmployee.aspx">
                        <img src="../Images/AccountCustomField.gif" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/AttendanceChartByEmployee.aspx"  >
                        <asp:Literal ID="Literal11" runat="server" Text="ãÎØØ ãáÎÕ ÇáÍÖæÑ ÍÓÈ ÇáãæÙÝ" meta:resourcekey="Literal11Resource1"></asp:Literal>
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    
                    <a href="../Reports/AttendanceChartSummuryByDepartment.aspx">
                        <img src="../Images/unnamed.png" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/AttendanceChartSummuryByDepartment.aspx"  >
                        <asp:Literal ID="Literal12" runat="server" Text="ãÎØØ ãáÎÕ ÇáÍÖæÑ ÍÓÈ ÇáÞÓã" meta:resourcekey="Literal12Resource1"></asp:Literal>
                    </a>
                </td>
           </tr>
             <tr>
                <td>
                    
                    <a href="../Reports/AttendanceChartComparassionByDepartment.aspx">
                        <img src="../Images/stats.gif" alt="HTML tutorial" width="42" height="42">
                    </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <a href="../Reports/AttendanceChartComparassionByDepartment.aspx"  >
                         <asp:Literal ID="Literal13" runat="server" Text="ãÎØØ ãÞÇÑäÉ ÇáÍÇáÉ Èíä ÇáÇÞÓÇã" meta:resourcekey="Literal13Resource1"></asp:Literal>
                    </a>
                </td>
            </tr>--%>
            </table>


    </ContentTemplate>
</asp:UpdatePanel>
<br />

