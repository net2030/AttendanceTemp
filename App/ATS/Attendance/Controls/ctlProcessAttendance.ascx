<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlProcessAttendance.ascx.vb" Inherits="Attendance_Controls_ctlProcessAttendance" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>


<%@ Register Src="../../ctlDepartmentTree.ascx" TagName="ctlDepartmentTree" TagPrefix="uc2" %>

<script type="text/javascript">
    function Toggle() {


        var opt1 = document.getElementById("<%=rdAll.ClientID%>")
        // var opt2 = document.getElementById("<%=rdByDepartment.ClientID%>")
        var opt3 = document.getElementById("<%=rdByEmployee.ClientID%>")

        if (opt1.checked) {
            document.getElementById("<%=LiteralEmployee.ClientID%>").style.display = "none";
            document.getElementById("<%=LiteralEmployee1.ClientID%>").style.display = "none";




            document.getElementById("<%=ddlEmployees.ClientID%>").style.display = "none";
            document.getElementById("<%=Button1.ClientID%>").style.display = "none";
            document.getElementById("<%=txtEmployeeCode.ClientID%>").style.display = "none";

            var validatorId = document.getElementById("<%=CustomValidator1.ClientID%>")
            ValidatorEnable(validatorId, true);

        }
            //else if (opt2.checked) {
            //   document.getElementById("LiteralEmployee").style.display = "none";
            //  document.getElementById("LiteralEmployee1").style.display = "none";
            // document.getElementById("LiteralDepartment").style.display = "inline";

            // document.getElementById("<%=ddlDepartment.ClientID%>").style.display = "inline";
            //  document.getElementById("<%=ddlEmployees.ClientID%>").style.display = "none";
            // document.getElementById("<%=Button1.ClientID%>").style.display = "none";
            // document.getElementById("<%=txtEmployeeCode.ClientID%>").style.display = "none";

            // } 
        else if (opt3.checked) {
            document.getElementById("<%=LiteralEmployee.ClientID%>").style.display = "inline";
        document.getElementById("<%=LiteralEmployee1.ClientID%>").style.display = "inline";
        //document.getElementById("LiteralDepartment").style.display = "none";

        //   document.getElementById("<%=ddlDepartment.ClientID%>").style.display = "none";
        document.getElementById("<%=ddlEmployees.ClientID%>").style.display = "inline";
        document.getElementById("<%=Button1.ClientID%>").style.display = "inline";
        document.getElementById("<%=txtEmployeeCode.ClientID%>").style.display = "inline";

        var validatorId = document.getElementById("<%=CustomValidator1.ClientID%>")
        ValidatorEnable(validatorId, false);
    }
}
var ispost = false ;
$(document).ready(function () {

    ispost = document.getElementById("<%=IsPost.ClientID%>").value

   // alert(ispost)
    if (ispost) {
        Toggle()
        return;
    }

  //  alert(1)
    // document.getElementById("LiteralEmployee").style.display = "none";
    // document.getElementById("LiteralEmployee1").style.display = "none";
    //  document.getElementById("LiteralEnddate").style.display = "none";

        document.getElementById("<%=LiteralEmployee.ClientID%>").style.display = "none";
        document.getElementById("<%=LiteralEmployee1.ClientID%>").style.display = "none";

        //  document.getElementById("<%=ddlDepartment.ClientID%>").style.display = "none";
        document.getElementById("<%=ddlEmployees.ClientID%>").style.display = "none";
        document.getElementById("<%=Button1.ClientID%>").style.display = "none";
        document.getElementById("<%=txtEmployeeCode.ClientID%>").style.display = "none";



    });


    function CompareDateEqual(oSrc, args) {

        try {
            var ParentDiv = document.getElementById("C_C_C_RadPageView1");
            if (ParentDiv && ParentDiv.className == "rmpHiddenView")
                ParentDiv = document.getElementById("C_C_C_RadPageView2");

            var SDate;
            var EDate;
            var dats;


            if (ParentDiv)
                dats = ParentDiv.getElementsByClassName("textboxnew is-calendarsPicker");
            else
                dats = document.getElementsByClassName("textboxnew is-calendarsPicker");

            //alert(ParentDiv.id);

            for (var i = 0; i < dats.length; i++) {
                if (dats[i].id.indexOf("Beg") > -1 || dats[i].id.indexOf("Start") > -1 || dats[i].id.indexOf("Effective") > -1) {
                    SDate = document.getElementById(dats[i].id).value;
                }
                else if (dats[i].id.indexOf("End") > -1 || dats[i].id.indexOf("Expire") > -1) {
                    EDate = document.getElementById(dats[i].id).value;
                }

            }

            //alert(SDate);
            //alert(EDate);

            var StartDate = new Date(convertToGreg(SDate));
            var EndDate = new Date(convertToGreg(EDate));


            //args.IsValid = true;
            //return;

            args.IsValid = (StartDate.getTime() == EndDate.getTime());
        }
        catch (err) {
            args.IsValid = true;
        }
    }


</script>

<div style="border: solid; width: 50%">

    <table class="xAdminOption" style="width: 100%">

        <tr>
            <th colspan="4" class="caption" style="text-align: center;">
                <asp:Literal ID="Literal9" runat="server" Text="„⁄«·Ã… «·Õ÷Ê— Ê«·«‰’—«›  " meta:resourcekey="Literal9Resource1" />
            </th>
        </tr>
        <tr>
            <th colspan="4" class="FormViewSubHeader" style="text-align: center;">
                <asp:Literal ID="Literal10" runat="server" Text="Ì „ Â‰« „⁄«·Ã… «·Õ÷Ê— Ê«·«‰’—«› Õ”» «·ﬁ”„ «Ê Õ”» «·„ÊŸ› Œ·«· › —… „⁄Ì‰…" meta:resourcekey="Literal10Resource1" />
            </th>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:RadioButton ID="rdAll" runat="server" GroupName="Opti" Checked="True" Text="«·ﬂ·" onClick="Toggle();" meta:resourcekey="rdAllResource1" />
            </td>
            <td>
                <asp:RadioButton ID="rdByDepartment" runat="server" GroupName="Opti" Text="Õ”» «·ﬁ”„" onClick="Toggle();" Visible="False" meta:resourcekey="rdByDepartmentResource1" />
            </td>
            <td>
                <asp:RadioButton ID="rdByEmployee" runat="server" GroupName="Opti" Text="Õ”» «·„ÊŸ›" onClick="Toggle();" meta:resourcekey="rdByEmployeeResource1" />
            </td>
        </tr>


        <tr>
            <td>
                <asp:Label ID="LiteralDepartment" runat="server" meta:resourcekey="LiteralDepartmentResource1"></asp:Label>
            </td>
            <%-- <td >
                            <label id="LiteralDepartment" >≈”„ «·ﬁ”„ </label>
                            </td>--%>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server" DataTextField="DepartmentName" DataValueField="AccountDepartmentId" Visible="False" meta:resourcekey="ddlDepartmentResource1" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LiteralEmployee" runat="server" Text="≈”„ «·„ÊŸ›" meta:resourcekey="LiteralEmployeeResource1"></asp:Label>
                <%--<label id="LiteralEmployee"> ≈”„ «·„ÊŸ›  </label>--%>
            </td>
            <td>
                <asp:DropDownList ID="ddlEmployees" runat="server" DataTextField="EmployeeName" DataValueField="EmployeeId" meta:resourcekey="ddlEmployeesResource1" />
            </td>
            <td>
                <asp:Label ID="LiteralEmployee1" runat="server" Text="—ﬁ„ «·»’„…" meta:resourcekey="LiteralEmployee1Resource1"></asp:Label>
            <td>
                <asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" meta:resourcekey="txtEmployeeCodeResource1" />
                <asp:Button ID="Button1" runat="server" Text="»ÕÀ" OnClientClick="ispost=true;" meta:resourcekey="Button1Resource1" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" AssociatedControlID="Startdate" Text="»œ«Ì… «·› —… :" meta:resourcekey="LabelResource1" />
            </td>
            <td>
                <uc1:MyDate ID="Startdate" runat="server" />
            </td>
            <td>
                <asp:Literal ID="Literal3" runat="server" Text="‰Â«Ì… «·› —…" meta:resourcekey="Literal3Resource1"></asp:Literal>

            </td>
            <td>
                <uc1:MyDate ID="Enddate" runat="server" />
                <asp:CustomValidator ID="CustomValidator2" runat="server"
                    ControlToValidate="Enddate$TextBox1"
                    ErrorMessage="End date must be greater or equa !"
                    ClientValidationFunction="CompareDate" meta:resourcekey="CustomValidator2Resource1"></asp:CustomValidator>
                <br>
                <asp:CustomValidator ID="CustomValidator1" runat="server"
                    ControlToValidate="Enddate$TextBox1"
                    ErrorMessage="›Ì Õ«·… «·Ã„Ì⁄ ÌÃ» «‰  ﬂÊ‰ «·„⁄«·Ã… ·ÌÊ„ Ê«Õœ ›ﬁÿ !"
                    ClientValidationFunction="CompareDateEqual"></asp:CustomValidator>
            </td>
        </tr>

        <tr>
            <td></td>
        </tr>

         <tr>
             <td colspan="5">
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" Text=""/>
             </td>
        </tr>

        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnAdd" runat="server" OnClick="ProcessAttendance" Text="„⁄«·Ã…" UseSubmitBehavior="False" meta:resourcekey="btnAddResource1" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
       
    </table>

</div>
<input id="IsPost" runat="server"  type="hidden" value="0" />
<asp:ObjectDataSource ID="dsDepartments" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDepartmentsList" TypeName="LookUp"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="dsEmployees" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetEmployeesList" TypeName="LookUp"></asp:ObjectDataSource>


