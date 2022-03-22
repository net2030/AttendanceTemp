<%@ Page Language="VB" MasterPageFile="~/Mobile/Masters/MasterPageMobileEmployee.master" AutoEventWireup="false" CodeFile="Approvals.aspx.vb" Inherits="Mobile_Approvals" title="Approvals" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<%--  <div data-role="header">
    <h1>Approvals Page</h1>
  </div>
  <div role="main" class="ui-content">
    <ul data-role="listview">
      <li>
        
          <a href="GatePassApproval.aspx" target="_parent">GatePass Approval</a>
          
                <div class="noti_bubble" style ="font-size:larger;">
                     <label id="lblAllNotifications" >44</label>
                 </div>
        
     </li>
      <li><a href="VacationApproval.aspx" target="_parent">Vacation Approval</a></li>
      <li><a href="WorkExceptionApproval.aspx" target="_parent">WorkException Approval</a></li>
     
    </ul>
  </div>--%>
<script type = "text/javascript">
    var EmployeeId1 = "<%=Session("AccountEmployeeId").ToString()%>"
    $(document).ready(function () {

        //setInterval(function () {

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
                        document.getElementById('lblVacationsNotification').innerHTML = 0;

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
                        document.getElementById('lblWorkExceptionNotification').innerHTML = 0;

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
                        document.getElementById('lblGatepassNotification').innerHTML = 0;

                },
                failure: function (response) {
                    alert(response.d);
                }
            });

        //}, 1000);
    });

    function OnSuccess1(response) {

        document.getElementById('lblVacationsNotification').innerHTML = response.d
    }

    function OnSuccess2(response) {

        document.getElementById('lblWorkExceptionNotification').innerHTML = response.d
    }



          </script>
<div data-role="content">
	<div class="content-primary">
		<ul data-role="listview">
			<li><a href="VacationApproval.aspx" target="_parent">Vacation Approval <span class="ui-li-count"><label id="lblVacationsNotification" ></label></span></a></li>
			<li><a href="WorkExceptionApproval.aspx" target="_parent">WorkException Approval <span class="ui-li-count"><label id="lblWorkExceptionNotification" ></label></span></a></li>
			<li><a href="GatePassApproval.aspx" target="_parent">GatePass Approval <span class="ui-li-count"><label id="lblGatepassNotification" ></label></span></a></li>
		</ul>
    </div><!--/content-primary -->		
</div>
</asp:Content>

