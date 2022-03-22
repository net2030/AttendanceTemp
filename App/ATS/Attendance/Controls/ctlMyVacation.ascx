<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMyVacation.ascx.vb" Inherits="Attendance_Controls_ctlMyVacation" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>
<%@ Register Src="~/Attendance/Controls/ctlEmployeesLeaveBalance.ascx" TagPrefix="uc1" TagName="ctlEmployeesLeaveBalance" %>
<style type="text/css">
    #tblmain {
        width: 21cm;
        /*border: solid;*/
        font-family: Arial;
        font-size: 18px;
        /*padding-left:20px;*/
    }

        #tblmain td {
            padding-right: 10px;
            padding-top: 5px;
        }

    #tblinfo {
        text-align: center;
        width: 87%;
    }

        #tblinfo td {
            padding-right: 5px;
            padding-left: 5px;
            padding-top: 5px;
            padding-bottom: 5px;
            border: 1px solid black;
            font-size: 12px;
            text-align: center;
        }

    #tblBref {
        width: 87%;
        /*padding-right: 0.5cm;
        padding-left: 0.5cm;*/
    }

        #tblBref td {
            border: 2px solid black;
            font-size: 12px;
            font-family: Arial;
            font-weight: bold;
            text-align: center;
            height: 25px;
            padding: 0 0 0 0;
        }

    #tbldetails {
        padding-top: 25px;
        width: 87%;
        /*padding-right: 100px;*/
    }

        #tbldetails td {
            padding-right: 5px;
            padding-left: 5px;
            padding-top: 5px;
            padding-bottom: 5px;
            border: 1px solid black;
            font-size: 12px;
        }

            #tbldetails td span {
                /*font-family: Arial;*/
            }

    .xGridView {
        width: 18cm;
    }
</style>
<style type="text/css" media="print">
    @page {
        size: 21cm 29cm; /* auto is the initial value */
        margin: 0mm; /* this affects the margin in the printer settings */
    }

    html {
        background-color: #FFFFFF;
        margin: 0px; /* this affects the margin on the html before sending to printer */
    }

    body {
        /*border: solid 1px blue ;*/
        margin-left: 5mm;
        margin-right: 5mm; /* margin you want for the content */
        /*margin-top:3mm;*/
        font-family: Arial;
        font-size: large;
    }

    /*#divtbl {
        align-content: center;
        page-break-after:auto;
    }*/
</style>

<script type="text/javascript">

    function disable_AltEmployeeValidator(ddlId) {
        try {

            var ControlName = document.getElementById(ddlId.id);
            var id = document.getElementById('<%= hdnAltEmployeeRequiredValidator.ClientID%>').value;
            if (ControlName.value != 1)
                ValidatorEnable(document.getElementById(id), false);
            else
                ValidatorEnable(document.getElementById(id), true);

        }
        catch (err) {

        }
    }

    $(document).ready(function () {
        try {

            var validatorid = document.getElementById('<%= hdnAltEmployeeRequiredValidator.ClientID%>').value;
            var ddlAltEmployeeId = document.getElementById('<%= hdnddlVacationtype.ClientID%>').value;
            if (document.getElementById(ddlAltEmployeeId).value != 1)
                ValidatorEnable(document.getElementById(validatorid), false);


            var sdat = document.getElementById(startdatefromsession).value;
            var edat = document.getElementById(enddatefromsession).value;
            var StartDate = new Date(convertToGreg(sdat));
            var EndDate = new Date(convertToGreg(edat));
            document.getElementById("txtvacationperiod").value = ((EndDate.getTime() - StartDate.getTime()) / 1000 / 60 / 60 / 24) + 1;

        }
        catch (err) {

        }


    });

    function printselected1() {

        var styles = document.getElementsByTagName("style");
        //alert(styles.length);
        var printStyle = "<style type='text/css' media='print'> " + styles[1].innerHTML + " </style>";
        //alert(printStyle);

        var maindiv = document.getElementById('<%= PrintDiv.ClientID%>');
        var divs = maindiv.getElementsByTagName("div");

        var oldPage = document.body.innerHTML;

        var newPage = "<html><head><title></title>" + printStyle + "</head><body>";


        newPage = newPage + "<div id='divtbl' >"
        newPage = newPage + maindiv.innerHTML;
        newPage = newPage + "<br/><br/>"
        //newPage = newPage + "<div align='center'><table><tr><td><p style=' font-size:10px'>  „  «·ÿ»«⁄… »≈” Œœ«„ ‰Ÿ«„ Ãœ«Ê· </p></td> <td> <img src='Images/dwam2.png' width='80px' height='40px' /> </td> </tr> </table></div>"
        newPage = newPage + "</div>"

        newPage = newPage + "</body>";


        document.body.innerHTML = newPage;


        window.print();


        document.body.innerHTML = oldPage;
    }
</script>

<asp:HiddenField ID="hdnAltEmployeeRequiredValidator" runat="server" />
<asp:HiddenField ID="hdnddlVacationtype" runat="server" />

<%--<asp:UpdatePanel ID="UpdatePanel12" runat="server">
    <ContentTemplate>--%>
        <x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
            DataKeyNames="VacationId,EmployeeName,EmployeeNo,GovId,DepartmentName,TypeName,EffectiveDate,DateExpire,AltEmployeename,AprovalStatus,ApprovalNotes,Note,Contact,Address,AddedDate,AddedBy" Caption="ﬁ«∆„… »≈Ã«“« Ì"
            SkinID="xgridviewSkinEmployee" Width="98%" Style="text-align: right;" ShowHeaderWhenEmpty="True" OnRowEditing="gvEG_RowEditing" DataSourceID="dsVacations" meta:resourcekey="GridView2Resource1">
            <Columns>

                <asp:BoundField DataField="VacationId" HeaderText="„”·”·" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />

                <asp:BoundField DataField="EmployeeName" HeaderText="≈”„ «·„ÊŸ›" meta:resourcekey="BoundFieldResource2" />


                <asp:BoundField DataField="TypeName" HeaderText="‰Ê⁄ «·≈Ã«“…" meta:resourcekey="BoundFieldResource3" />

                <asp:BoundField DataField="EffectiveDate" HeaderText=" «—ÌŒ «·»œ«Ì…" meta:resourcekey="BoundFieldResource4" />

                <asp:BoundField DataField="DateExpire" HeaderText=" «—ÌŒ «·‰Â«Ì…" meta:resourcekey="BoundFieldResource5" />

                <asp:BoundField DataField="AltEmployeename" HeaderText="«·„ÊŸ› «·»œÌ·" meta:resourcekey="BoundFieldResource6" />

                <%-- <asp:BoundField DataField="DateOfReturn" HeaderText=" «—ÌŒ «·„»«‘—…" />--%>

                <asp:BoundField DataField="AprovalStatus" HeaderText="Õ«·… «·ÿ·»" meta:resourcekey="BoundFieldResource7" />

                <asp:BoundField DataField="ApprovalNotes" HeaderText="„·«ÕŸ« " meta:resourcekey="BoundFieldResource8" />

                <asp:TemplateField HeaderText=" ⁄œÌ·" ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:LinkButton ID="EditLinkButton" runat="server" Text="" CommandName="Edit" CausesValidation="False"
                            Visible='<%# If(Eval("IsApproved").ToString().Equals("True"), False, True) %>' ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="edit_button" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Õ–›" ShowHeader="False" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="" CommandName="DataDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                            Visible='<%# If(Eval("IsApproved").ToString().Equals("True"), False, True) %>' OnClientClick="<%# ResourceHelper.GetDeleteMessageJavascript() %>" CausesValidation="False" ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="1%" CssClass="delete_button" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:Button ID="PrintButton" runat="server"
                            CommandName="print"
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                            Text="ÿ»«⁄… «·ÿ·»" meta:resourcekey="PrintButtonResource1" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </x:GridView>
        <asp:FormView ID="MyVacationFormView" runat="server"
            SkinID="formviewskinemployee"
            DefaultMode="Insert" Width="98%" BorderStyle="Solid" meta:resourcekey="MyVacationFormViewResource1">
            <InsertItemTemplate>
                <table width="100%" class="xformview">
                    <tr>
                        <th colspan="6" class="caption" style="text-align: center;">
                            <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈Ã«“… „ÊŸ›" meta:resourcekey="Literal9Resource2" />
                        </th>
                    </tr>
                    <tr>
                        <th colspan="6" class="FormViewSubHeader" style="text-align: center;">
                            <asp:Literal ID="Literal10" runat="server" Text=" ”ÃÌ· ≈Ã«“…" meta:resourcekey="Literal10Resource2" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource2" />
                        </td>
                        <td>&nbsp; 
                        </td>
                        <td rowspan="6">
                            <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal1Resource2" />

                        </td>
                        <td>
                            <asp:TextBox ID="txtVacationId" runat="server" ReadOnly="True" meta:resourcekey="txtVacationIdResource2"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="  ≈”„ «·„ÊŸ› :" meta:resourcekey="Literal3Resource2" />

                        </td>
                        <td>
                            <asp:TextBox ID="txtEmployeeName" runat="server" ReadOnly="True" Text='<%# Session("EmployeeNameWithCode") %>' meta:resourcekey="txtEmployeeNameResource2"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text="  ‰Ê⁄ «·≈Ã«“… :" meta:resourcekey="Literal4Resource2" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlVacationTypeId" runat="server" Style="margin-right: 0px"
                                DataSourceID="dsVacationTypes"
                                DataTextField="TypeName"
                                DataValueField="TypeId"
                                onchange="disable_AltEmployeeValidator(this)" meta:resourcekey="ddlVacationTypeIdResource2">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="VacationTypeRequiredFieldValidator" ControlToValidate="ddlVacationTypeId"
                                ErrorMessage="≈Œ — ‰Ê⁄ «·«Ã«“… !" meta:resourcekey="VacationTypeRequiredFieldValidatorResource2"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal
                                ID="Literal25" runat="server" Text="«·„ÊŸ› «·»œÌ·:" meta:resourcekey="Literal25Resource2" />
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlAltEmployeeId" runat="server" Style="margin-right: 0px"
                                DataTextField="EmployeeName"
                                DataValueField="EmployeeId" meta:resourcekey="ddlAltEmployeeIdResource2">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource2">....≈Œ —....</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                                ID="AlternativeEmployeeRequiredFieldValidator" Display="Dynamic"
                                ControlToValidate="ddlAltEmployeeId"
                                runat="server" Text="*"
                                ErrorMessage="≈Œ — «·„ÊŸ› «·»œÌ·"
                                ForeColor="Red" meta:resourcekey="AlternativeEmployeeRequiredFieldValidatorResource2"></asp:RequiredFieldValidator>
                            <asp:Literal ID="Literal11" runat="server" Text="—ﬁ„ «·»’„… ··»œÌ·:" meta:resourcekey="Literal11Resource2" />
                            <asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" meta:resourcekey="txtEmployeeCodeResource2" />
                            <asp:Button ID="Button3" runat="server" Text="»ÕÀ" CausesValidation="False" OnClick="FindAltEmployeeByEmployeeCode" meta:resourcekey="Button3Resource2" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text="   »œ√ ›Ì :" meta:resourcekey="Literal5Resource2" />
                        </td>
                        <td>
                            <uc1:MyDate runat="server" ID="datEffectiveDate" SelectedDate="<%# Now.Date %>" />
                        </td>

                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text="   ‰ ÂÌ ›Ì :" meta:resourcekey="Literal6Resource2" />
                        </td>
                        <td>
                            <uc1:MyDate runat="server" ID="datDateExpire" SelectedDate="<%# Now.Date %>" />
                            <asp:CustomValidator ID="CustomValidator2" runat="server"
                                ControlToValidate="datDateExpire$TextBox1"
                                ErrorMessage="End date must be greater or equa !"
                                ClientValidationFunction="CompareDatelocal" meta:resourcekey="CustomValidator2Resource2"></asp:CustomValidator>
                            <asp:CustomValidator ID="ChckbalanceValidator" runat="server"
                                ControlToValidate="datDateExpire$TextBox1"
                                ErrorMessage="·« ÌÊÃœ —’Ìœ ﬂ«›Ì"
                                ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource2"></asp:CustomValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal13" runat="server" Text="«·„œ… »«·ÌÊ„:" meta:resourcekey="Literal13Resource1"></asp:Literal>
                        </td>
                        <td>
                            <input id="txtvacationperiod" style="width: 50px" readonly="true" value="1" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            <asp:Literal ID="Literal7" runat="server" Text="   «—ÌŒ «·„»«‘—… :" meta:resourcekey="Literal7Resource2" />
                        </td>
                        <td>
                            <uc1:MyDate runat="server" ID="datDateOfReturn" SelectedDate="<%# DateAdd(DateInterval.Day, 1, Now.Date) %>" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="   «·⁄‰Ê«‰ «À‰«¡ «·≈Ã«“… :" meta:resourcekey="Literal2Resource2" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="200px" Height="40px" meta:resourcekey="txtAddressResource2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Literal ID="Literal12" runat="server" Text=" Ê”Ì·… «·≈ ’«·:" meta:resourcekey="Literal12Resource2" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtContact" runat="server" TextMode="MultiLine" Width="200px" Height="40px" meta:resourcekey="txtContactResource2"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text="   „·«ÕŸ«  :" meta:resourcekey="Literal8Resource2" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="200px" Height="70px" meta:resourcekey="txtNotesResource2"></asp:TextBox>
                        </td>
                    </tr>
                    <br />
                    <tr>
                        <td>&nbsp
                        </td>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" OnClick="DataAdd"
                                Text="Õ›Ÿ «·≈Ã«“…" meta:resourcekey="Button1Resource2" />

                            <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" CausesValidation="false"
                                Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource2" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>

            <EditItemTemplate>
                <table width="100%" class="xformview">
                    <tr>
                        <th colspan="5" class="caption" style="text-align: center;">
                            <asp:Literal ID="Literal9" runat="server" Text="»Ì«‰«  ≈Ã«“… „ÊŸ›" meta:resourcekey="Literal9Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <th colspan="5" class="FormViewSubHeader" style="text-align: center;">
                            <asp:Literal ID="Literal10" runat="server" Text=" ÕœÌÀ »Ì«‰«  ≈Ã«“…" meta:resourcekey="Literal10Resource1" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Large" meta:resourcekey="Label2Resource1" />
                        </td>
                        <td rowspan="6">
                            <uc1:ctlEmployeesLeaveBalance runat="server" ID="ctlEmployeesLeaveBalance" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text=" —ﬁ„ «·Õ«”»:" meta:resourcekey="Literal1Resource1" />

                        </td>
                        <td>
                            <asp:TextBox ID="txtVacationId" runat="server" ReadOnly="True" Text='<%# Bind("VacationId") %>' meta:resourcekey="txtVacationIdResource1"></asp:TextBox>
                        </td>

                    </tr>


                    <tr>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="  ≈”„ «·„ÊŸ› :" meta:resourcekey="Literal3Resource1" />

                        </td>
                        <td>
                            <asp:TextBox ID="txtEmployeeName" runat="server" ReadOnly="True" Text='<%# Session("EmployeeNameWithCode") %>' meta:resourcekey="txtEmployeeNameResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text="  ‰Ê⁄ «·≈Ã«“… :" meta:resourcekey="Literal4Resource1" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlVacationTypeId" runat="server" Style="margin-right: 0px"
                                DataSourceID="dsVacationTypes"
                                DataTextField="TypeName"
                                DataValueField="TypeId"
                                onchange="disable_AltEmployeeValidator(this)"
                                SelectedValue='<%# Bind("TypeId") %>' meta:resourcekey="ddlVacationTypeIdResource1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="VacationTypeRequiredFieldValidator" ControlToValidate="ddlVacationTypeId"
                                ErrorMessage="≈Œ — ‰Ê⁄ «·«Ã«“… !" meta:resourcekey="VacationTypeRequiredFieldValidatorResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal
                                ID="Literal25" runat="server" Text="«·„ÊŸ› «·»œÌ·:" meta:resourcekey="Literal25Resource1" />
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlAltEmployeeId" runat="server" Style="margin-right: 0px"
                                DataTextField="EmployeeName"
                                DataValueField="EmployeeId" meta:resourcekey="ddlAltEmployeeIdResource1">
                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">....≈Œ —....</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0"
                                ID="AlternativeEmployeeRequiredFieldValidator" Display="Dynamic"
                                ControlToValidate="ddlAltEmployeeId"
                                runat="server" Text="*"
                                ErrorMessage="≈Œ — «·„ÊŸ› «·»œÌ·"
                                ForeColor="Red" meta:resourcekey="AlternativeEmployeeRequiredFieldValidatorResource1"></asp:RequiredFieldValidator>
                            <asp:Literal ID="Literal11" runat="server" Text="—ﬁ„ «·»’„… ··»œÌ·:" meta:resourcekey="Literal11Resource1" />
                            <asp:TextBox ID="txtEmployeeCode" runat="server" Width="70px" meta:resourcekey="txtEmployeeCodeResource1" />
                            <asp:Button ID="Button3" runat="server" Text="»ÕÀ" CausesValidation="False" OnClick="FindAltEmployeeByEmployeeCode" meta:resourcekey="Button3Resource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text="   »œ√ ›Ì :" meta:resourcekey="Literal5Resource1" />
                        </td>
                        <td>
                            <uc1:MyDate runat="server" ID="datEffectiveDate" SelectedDate='<%# Bind("EffectiveDate") %>' />
                        </td>

                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text="   ‰ ÂÌ ›Ì :" meta:resourcekey="Literal6Resource1" />
                        </td>
                        <td>
                            <uc1:MyDate runat="server" ID="datDateExpire" SelectedDate='<%# Bind("DateExpire") %>' />
                            <asp:CustomValidator ID="CustomValidator2" runat="server"
                                ControlToValidate="datDateExpire$TextBox1"
                                ErrorMessage="End date must be greater or equa !"
                                ClientValidationFunction="CompareDatelocal" meta:resourcekey="CustomValidator2Resource1"></asp:CustomValidator>
                            <asp:CustomValidator ID="ChckbalanceValidator" runat="server"
                                ControlToValidate="datDateExpire$TextBox1"
                                ErrorMessage="·« ÌÊÃœ —’Ìœ ﬂ«›Ì"
                                ClientValidationFunction="checkBalance" meta:resourcekey="ChckbalanceValidatorResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal13" runat="server" Text="«·„œ… »«·ÌÊ„:" meta:resourcekey="Literal13Resource1"></asp:Literal>
                        </td>
                        <td>
                            <input id="txtvacationperiod" style="width: 50px" readonly="true" value="1" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            <asp:Literal ID="Literal7" runat="server" Text="   «—ÌŒ «·„»«‘—… :" meta:resourcekey="Literal7Resource1" />
                        </td>
                        <td>
                            <uc1:MyDate runat="server" ID="datDateOfReturn" SelectedDate='<%# Bind("DateOfReturn") %>' />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="   «·⁄‰Ê«‰ «À‰«¡ «·≈Ã«“… :" meta:resourcekey="Literal2Resource1" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="200px" Height="40px" Text='<%# Bind("Address") %>' meta:resourcekey="txtAddressResource1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Literal ID="Literal12" runat="server" Text=" Ê”Ì·… «·≈ ’«·:" meta:resourcekey="Literal12Resource1" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtContact" runat="server" TextMode="MultiLine" Width="200px" Height="40px" Text='<%# Bind("Contact") %>' meta:resourcekey="txtContactResource1"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text="   „·«ÕŸ«  :" meta:resourcekey="Literal8Resource1" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="200px" Height="70px" Text='<%# Bind("Note") %>' meta:resourcekey="txtNotesResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp
                        </td>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" OnClick="DataUpdate"
                                Text=" ÕœÌÀ «·≈Ã«“…" meta:resourcekey="Button1Resource1" />

                            <asp:Button ID="Button2" runat="server" OnClientClick="ReloadSamepage(); return false;" CausesValidation="false"
                                Text="≈·€«¡ «·√„—" meta:resourcekey="Button2Resource1" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>

        </asp:FormView>


<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="LookUp" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDepartmentsList"></asp:ObjectDataSource>



<br />
<br />
<div id="PrintDiv" runat="server"  visible="false">
    <table id="tblmain">
        <tr>
            <td>
                <img src="../Images/ReportHeaderMixedColors.gif" width="100%" height="120px" ></img>
            </td>
        </tr>

        <tr>
            <td style="width: 21cm;" colspan="3">
                <table>
                    <tr>
                        <td rowspan="2" style="text-align: center; width: 30%; font-size:small">
                            <table>
                                <tr>
                                    <td>
                                         <asp:Literal ID="Literal41" runat="server" Text="«·„„·ﬂ… «·⁄—»Ì… «·”⁄ÊœÌ…" meta:resourcekey="Literal41Resource1"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td> <asp:Literal ID="Literal40" runat="server" Text="Ã«„⁄… «·ﬁ’Ì„" meta:resourcekey="Literal40Resource1"></asp:Literal> 
                                    </td>
                                </tr>
                                <tr>
                                    <td> 
                                        <asp:Literal ID="Literal39" runat="server" Text="«·≈œ«—… «·⁄«„… ··„‘«—Ì⁄" meta:resourcekey="Literal39Resource1"></asp:Literal>
                                    </td>
                                </tr>
                             
                            </table>

                        </td>
                        <td rowspan="2" style="text-align: center; padding-left: 80px; width: 40%; padding-right: 80px"><asp:Literal ID="Literal37" runat="server" Text="»”‹‹„ «··Â «·—Õ„‰ «·—ÕÌ„" meta:resourcekey="Literal37Resource1"></asp:Literal><br />
                            <br />
                            <br />
                            <img src="../Uploads/1/Logo/CompanyLogo.gif" style="height: 80px; width: 100px">
                        </td>
                        <td style="width: 30%; align-content: stretch;font-size:small">
                              <table>
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal36" runat="server" Text="«·—ﬁ„:" meta:resourcekey="Literal36Resource1"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNo" runat="server" Text="1234" meta:resourcekey="lblNoResource1" Font-Size="Small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal35" runat="server" Text="«· «—ÌŒ:" meta:resourcekey="Literal35Resource1"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDate" runat="server" Text="1437/01/01 Â‹" meta:resourcekey="lblDateResource1" Font-Size="Small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal34" runat="server" Text="«·„—›ﬁ« " meta:resourcekey="Literal34Resource1"></asp:Literal>
                                    </td>
                                    <td>................
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">&nbsp
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">=========================================================
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">&nbsp
            </td>
        </tr>
        <tr>
            <td style="font-weight: bolder; text-align: center" colspan="3">
                 <asp:Literal ID="Literal33" runat="server" Text=" ‰„Ê–Ã  ﬁœÌ„ ≈Ã«“…" meta:resourcekey="Literal33Resource1"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">&nbsp
            </td>

        </tr>


        <tr>
            <td colspan="3">
                <table id="tblinfo">
                    <tr>
                        <td style="font-size: 14px; font-weight: bold;"> <asp:Literal ID="Literal32" runat="server" Text=" «·—ﬁ„ «·ÊŸÌ›Ì:" meta:resourcekey="Literal32Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Label ID="lblEmployeeNo" runat="server" Text="1254888"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td style="border: 1px solid black; width: 27%; font-size: 14px; font-weight: bold;">
                             <asp:Literal ID="Literal31" runat="server" Text="«·ﬁ”„" meta:resourcekey="Literal31Resource1"></asp:Literal>

                        </td>
                        <td>
                           <asp:Label ID="lblDepartment" runat="server" Text="«·„Ê«—œ «·»‘—Ì…" meta:resourcekey="lblDepartmentResource1"></asp:Label>

                        </td>

                         <td>
                                        <asp:Label ID="id10" runat="server" Text="1" meta:resourcekey="id10Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="id9" runat="server" Text="1" meta:resourcekey="id9Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="id8" runat="server" Text="1" meta:resourcekey="id8Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="id7" runat="server" Text="1" meta:resourcekey="id7Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="id6" runat="server" Text="1" meta:resourcekey="id6Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="id5" runat="server" Text="1" meta:resourcekey="id5Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="id4" runat="server" Text="1" meta:resourcekey="id4Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="id3" runat="server" Text="1" meta:resourcekey="id3Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="id2" runat="server" Text="1" meta:resourcekey="id2Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="id1" runat="server" Text=" 1" meta:resourcekey="id1Resource1"></asp:Label>
                                    </td>

                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table id="tblBref">

                    <tr>

                        <td style="width: 27%">
                            <asp:Literal ID="Literal30" runat="server" Text="«·≈”‹‹‹‹‹‹‹‹‹‹„" meta:resourcekey="Literal30Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal29" runat="server" Text="‰Ê⁄ «·≈Ã«“…" meta:resourcekey="Literal29Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal28" runat="server" Text="  »œ√ ›Ì" meta:resourcekey="Literal28Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal27" runat="server" Text=" ‰ ÂÌ ›Ì" meta:resourcekey="Literal27Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal26" runat="server" Text=" Õ«·… «·ÿ·»" meta:resourcekey="Literal26Resource1"></asp:Literal>
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:Label ID="lblEmployeeName" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEffectivedate" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblExpireDate" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblApproveStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table id="tbldetails">
                    <tr>
                        <td>
                            <asp:Literal ID="Literal24" runat="server" Text=" „ﬁœ„ «·ÿ·»" meta:resourcekey="Literal24Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Label ID="lblAddedBy" runat="server" meta:resourcekey="lblAddedByResource1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal23" runat="server" Text=" «—ÌŒ  ﬁœÌ„ «·ÿ·» :" meta:resourcekey="Literal23Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Label ID="lblAddedDate" runat="server" meta:resourcekey="lblAddedDateResource1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal22" runat="server" Text=" Ê’› «·ÿ·»:" meta:resourcekey="Literal22Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Label ID="lblNote" runat="server" meta:resourcekey="lblNoteResource1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 27%">
                            <asp:Literal ID="Literal21" runat="server" Text=" Ê”Ì·… «·≈ ’«· «À‰«¡ «·≈Ã«“…:" meta:resourcekey="Literal21Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Label ID="lblContact" runat="server" meta:resourcekey="lblContactResource1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal20" runat="server" Text="«·⁄‰Ê«‰ «À‰«¡ «·≈Ã«“… :" meta:resourcekey="Literal20Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Label ID="lblAddess" runat="server" meta:resourcekey="lblAddessResource1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal19" runat="server" Text=" «·„ÊŸ› «·»œÌ·:" meta:resourcekey="Literal19Resource1"></asp:Literal>
                        </td>
                        <td>
                            <asp:Label ID="lblAltEmp" runat="server" meta:resourcekey="lblAltEmpResource1"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>




        <tr>
            <td colspan="3" style="padding-right: 0px">
                <table>
                    <tr>
                        <td style="text-align: center">
                            <asp:Literal ID="Literal18" runat="server" Text=" ”Ã· «·„Ê«›ﬁ« " meta:resourcekey="Literal18Resource1"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <x:GridView ID="grdViewApprovalLog" runat="server" AutoGenerateColumns="False" SkinID="xgridviewSkinEmployee" meta:resourcekey="grdViewApprovalLogResource1">
                                <Columns>
                                    <asp:BoundField DataField="ApprovalPolicyName" HeaderText="”Ì«”… «·„Ê«›ﬁ…" meta:resourcekey="BoundFieldResource99" />
                                    <asp:BoundField DataField="RoleName" HeaderText="«·œÊ—" meta:resourcekey="BoundFieldResource22" />
                                    <asp:BoundField DataField="ApprovedBy" HeaderText=" „  »Ê«”ÿ…" meta:resourcekey="BoundFieldResource3" />
                                    <asp:BoundField DataField="ApprovalStatus" HeaderText="Õ«·… «·„Ê«›ﬁ…" meta:resourcekey="BoundFieldResource4" />

                                    <asp:BoundField DataField="ApprovedOn" HeaderText=" «—ÌŒ «·„Ê«›ﬁ…" meta:resourcekey="BoundFieldResource5" />
                                </Columns>
                            </x:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
              
            </td>
        </tr>
    </table>
    <div id="footer" style="bottom:0;  position: fixed;">
         <img src="../Images/ReportFooter.gif" height="45px" />
    </div>
     

</div>

<asp:Button ID="btnPrint" runat="server" Text="ÿ»«⁄…" OnClientClick="printselected1()" Visible="False" meta:resourcekey="btnPrintResource11" />

<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
<br />
<br />
<asp:ObjectDataSource ID="dsVacationTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" TypeName="ATS.BO.Framework.BOVacationType">
    <SelectParameters>
        <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>


<asp:ObjectDataSource ID="dsVacations" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetVacationsByEmployeeIdDataset" TypeName="ATS.BO.Framework.BOVacation">
    <SelectParameters>
        <asp:SessionParameter Name="UserAccountId" SessionField="AccountEmployeeId" Type="Int32" />
        <asp:SessionParameter Name="EmployeeId" SessionField="AccountEmployeeId" Type="Int32" />
        <asp:Parameter Name="PageNo" Type="Int32" DefaultValue="1" />
        <asp:Parameter DefaultValue="50" Name="PageSize" Type="Int32" />
         <asp:CookieParameter CookieName="CurrentLanguage" DefaultValue="ar" Name="Lang" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>


<asp:Button ID="btnAddVacation" runat="server" 
    Text="≈÷«›… ÃœÌœ"
    CausesValidation="False" meta:resourcekey="btnAddVacationResource1" />




