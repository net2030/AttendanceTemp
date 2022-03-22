<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlManageApprovals.ascx.vb" Inherits="Employee_Controls_ctlManageApprovals" %>
<table class="xAdminOption" width="98%">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" class="xAdminOption" width="98%" >
                <tbody>
                    <tr>
                        <th class="caption" colspan="2" style="width: 100%">
                            <asp:Literal ID="Literal1" runat="server" Text="إدارة الموافقات" meta:resourcekey="Literal1Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <th class="FormViewSubHeader" colspan="2" style="width: 100%;">
                            <asp:Literal ID="Literal2" runat="server" Text="الموافقات" meta:resourcekey="Literal2Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%; height: 95px">
                            <table width="150" style="border:0">
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                   <%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(181, 1) Then%>
                                    <td align="center" style="width: 14%; height: 48px;"  valign="top" >
                                        <asp:HyperLink ID="HyperLinkProjectMilestone" runat="server" 
                                                       NavigateUrl="~/Attendance/VacationsListForApproval.aspx" Width="100px">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/TimesheetApprovalActivity.gif" AlternateText="الموافقة على الإجازات" Height="48px" Width="48" />
                                        </asp:HyperLink>

                                        <div style ="position:relative;width:20px;height:20px;font-size:x-large;">
                                           <div class="noti_bubble">
                                            <label id="lblVacationsNotification" ></label>
                                           </div>
                                         </div>

                                    </td>
                                    <% End if %>

                                    <td align="center" style="width: 14%; height: 19px">
                                    </td>
                                 
                             <%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(182, 1) Then%>
                                    <td align="center" style="width: 14%; height: 48px;"  valign="top">
                                        <asp:HyperLink ID="HyperLinkRoles" runat="server" NavigateUrl="~/Attendance/WorkExceptionsListForApproval.aspx"
                                                       Width="100px">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/TimesheetApprovalActivity.gif" AlternateText="الموافقة على استثناءات الدوام" Height="48px" Width="48"/>
                                        </asp:HyperLink>
                                          <div style ="position:relative;width:20px;height:20px;font-size:x-large;">
                                               <div class="noti_bubble">
                                                <label id="lblWorkExceptionNotification" ></label>
                                               </div>
                                           </div>
                                    </td>
                                     <% End if %>
                                    <td align="center" style="width: 14%; height: 19px">
                                    </td>
                                   
                                    <%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(159, 1) Then%>
                                    <td align="center" style="width: 14%; height: 48px;"  valign="top">
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Attendance/GatepassListForApproval.aspx"
                                                       Width="100px">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/TimesheetApprovalActivity.gif" AlternateText="الموافقة على الإستئذان" Height="48px" Width="48"/>
                                        </asp:HyperLink>

                                         <div style ="position:relative;width:20px;height:20px;font-size:x-large;">
                                               <div class="noti_bubble">
                                             
                                                <label id="lblGatepassNotification" ></label>
                                               </div>
                                         </div>
                                    </td>
                                      <% End if %>
                                    <%-- <td align="center" style="width: 14%; height: 48px;"  valign="top">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Attendance/VacationsListForAltEmployeeApproval.aspx"
                                                       Width="100px">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/TimesheetApprovalActivity.gif" AlternateText="الموافقة على الإجازات كبديل" Height="48px" Width="48"/>
                                        </asp:HyperLink>

                                         <div style ="position:relative;width:20px;height:20px;font-size:x-large;">
                                           <div class="noti_bubble">
                                            <label id="lblAltEmpVacationsNotification" ></label>
                                           </div>
                                         </div>
                                    </td>--%>

                                      <%-- <td align="center" style="width: 14%; height: 48px;"  valign="top">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/MAS/WorkExceptionsFinanceApproval.aspx"
                                                       Width="100px">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/TimesheetApprovalActivity.gif" AlternateText="الموافقة المالية" Height="48px" Width="48"/>
                                        </asp:HyperLink>
                                    </td>--%>

                                     
                          
                                </tr>
                                <tr>
                                  <%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(181, 1) Then%>
                                    <td align="center" class="AdministrationOptionsText" style="width: 14%;" valign="top">
                                        <asp:HyperLink ID="TextHyperlinkDepartment" runat="server" NavigateUrl="~/Attendance/VacationsListForApproval.aspx"
                                                       Width="100px" Text="الموافقة على الإجازات" Height="38px" meta:resourcekey="TextHyperlinkDepartmentResource1">

                                        </asp:HyperLink>
                                    </td>
                                    <% End If%>
                                    <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 19px" 
                                        valign="top">
                                    </td>
                                    <%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(182, 1) Then%>
                                    <td align="center" class="AdministrationOptionsText" style="width: 14%;" 
                                        valign="top">
                                        <asp:HyperLink ID="TextHyperlinkRoles" runat="server" NavigateUrl="~/Attendance/WorkExceptionsListForApproval.aspx"
                                                       Width="100px" Text="الموافقة على استثناء الدوام" meta:resourcekey="TextHyperlinkRolesResource1">

                                        </asp:HyperLink>
                                    </td>
                                     <% end if %>
                                    <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 19px"
                                        valign="top">
                                    </td>
                                     <%If ATS.BO.BOPagePermission.IsPageHasPermissionOf(159, 1) Then%>
                                    <td align="center" class="AdministrationOptionsText" style="width: 14%;" 
                                        valign="top">
                                        <asp:HyperLink ID="TextHyperlinkWorkingDays" runat="server" NavigateUrl="~/Attendance/GatepassListForApproval.aspx"
                                                       Width="100px" Text="الموافقة على الإستئذان" meta:resourcekey="TextHyperlinkWorkingDaysResource1">

                                        </asp:HyperLink>
                                    </td>
                                    <% end if %>

                                    <%-- <td align="center" class="AdministrationOptionsText" style="width: 14%;" 
                                        valign="top">
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Attendance/VacationsListForAltEmployeeApproval.aspx"
                                                       Width="100px" Text="الموافقة على الإجازات كبديل">

                                        </asp:HyperLink>
                                    </td>--%>

                                    <%--<td align="center" class="AdministrationOptionsText" style="width: 14%;" 
                                        valign="top">
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/MAS/WorkExceptionsFinanceApproval.aspx"
                                                       Width="100px" Text="الموافقة المالية">

                                        </asp:HyperLink>
                                    </td>--%>


                                    
                                    <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 19px"
                                        valign="top">
                                    </td>
                            
                                    <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px" 
                                        valign="top">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
</table>

 <script type = "text/javascript">
     var EmployeeId1 = "<%=Session("AccountEmployeeId").ToString()%>"
     $(document).ready(function () {

         setInterval(function () {

             $.ajax({
                 type: "POST",
                 url: "../PendingApprovalsRequestCount.aspx/GetCountOfVacationPendingRequest",
                 data: '{EmployeeId: ' + EmployeeId + '}',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     if (response.d > 0 && response.d <= 9)
                         document.getElementById('lblVacationsNotification').innerHTML = response.d;
                     else if (response.d > 0 && response.d > 9)
                         document.getElementById('lblVacationsNotification').innerHTML = 9;
                     else
                         document.getElementById('lblVacationsNotification').innerHTML = "";

                 },
                 failure: function (response) {
                     alert(response.d);
                 }
             });

             $.ajax({
                 type: "POST",
                 url: "../PendingApprovalsRequestCount.aspx/GetCountOfWorkExceptionPendingRequest",
                 data: '{EmployeeId: ' + EmployeeId + '}',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     if (response.d > 0 && response.d <= 9)
                         document.getElementById('lblWorkExceptionNotification').innerHTML = response.d;
                     else if (response.d > 0 && response.d > 9)
                         document.getElementById('lblWorkExceptionNotification').innerHTML = 9;
                     else
                         document.getElementById('lblWorkExceptionNotification').innerHTML = "";

                 },
                 failure: function (response) {
                     alert(response.d);
                 }
             });

             $.ajax({
                 type: "POST",
                 url: "../PendingApprovalsRequestCount.aspx/GetCountOfGatepassPendingRequest",
                 data: '{EmployeeId: ' + EmployeeId + '}',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     if (response.d > 0 && response.d <= 9)
                         document.getElementById('lblGatepassNotification').innerHTML = response.d;
                     else if (response.d > 0 && response.d > 9)
                         document.getElementById('lblGatepassNotification').innerHTML = 9;
                     else
                         document.getElementById('lblGatepassNotification').innerHTML = "";

                 },
                 failure: function (response) {
                     alert(response.d);
                 }
             });

             //        $.ajax({
             //            type: "POST",
             //            url: "../PendingApprovalsRequestCount.aspx/GetCountOfAltEmployeePendingRequest",
             //            data: '{EmployeeId: ' + EmployeeId + '}',
             //            contentType: "application/json; charset=utf-8",
             //            dataType: "json",
             //            success: function (response) {
             //                if (response.d > 0 && response.d <= 9)
             //                    document.getElementById('lblAltEmpVacationsNotification').innerHTML = response.d;
             //                else if (response.d > 0 && response.d > 9)
             //                    document.getElementById('lblAltEmpVacationsNotification').innerHTML = 9;
             //                else
             //                    document.getElementById('lblAltEmpVacationsNotification').innerHTML = "";

             //            },
             //            failure: function (response) {
             //                alert(response.d);
             //            }
             //        });

         }, 1000);
     });


          </script>