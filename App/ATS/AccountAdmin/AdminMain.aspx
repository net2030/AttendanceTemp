<%@ Page Language="VB" MasterPageFile="~/Masters/MasterpageEmployee.master" AutoEventWireup="false" CodeFile="AdminMain.aspx.vb" Inherits="AccountAdmin_AdminMain" Title="TimeLive - Admin Options" EnableViewState="false" meta:resourcekey="PageResource1" %>

<asp:Content ID="C" ContentPlaceHolderID="C" runat="Server">

    <table class="xAdminOption" width="98%">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="xAdminOption" width="98%" id="AdminMain" border="0">
                    <tbody>
                        <tr>
                            <th class="caption" colspan="2" style="width: 1706px">
                                <asp:Literal ID="Literal1" runat="server" Text="ÅÏÇÑÉ ÇáäÙÇã" meta:resourcekey="Literal1Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <th class="FormViewSubHeader" colspan="2" style="width: 1706px; height: 22px">
                                <asp:Literal ID="Literal9" runat="server" Text="Çáåíßá ÇáÊäÙíãí" meta:resourcekey="Literal9Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 1706px; height: 95px">
                                <table width="150" style="border: 0">
                                    <tr>
                                        <td align="center" style="width: 10%;" valign="top">
                                            <asp:HyperLink ID="HyperLink2" runat="server"
                                                NavigateUrl="~/AccountAdmin/AccountLocations.aspx">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Locations.gif" AlternateText="ÇáãæÇÞÚ" Width="48px" Height="48px" />

                                            </asp:HyperLink>
                                        </td>
                                        <td align="center" style="width: 14%; height: 19px"></td>
                                        <td align="center" style="width: 10%;" valign="top">
                                            <asp:HyperLink ID="HyperLink3" runat="server"
                                                NavigateUrl="~/AccountAdmin/AccountDepartments.aspx" Width="100px">
                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Department.gif" AlternateText="ÇáÃÞÓÇã" Width="48px" Height="48px" />

                                            </asp:HyperLink>
                                        </td>
                                        <td align="center" style="width: 14%; height: 19px"></td>
                                        <td align="center" style="width: 10%;" valign="top">
                                            <asp:HyperLink ID="HyperLink54" runat="server" NavigateUrl="~/AccountAdmin/AccountRoles.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Roles.gif" AlternateText="Role" Width="48px" Height="48px" />

                                            </asp:HyperLink>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="center" class="AdministrationOptionsText"
                                            style="width: 14%; height: 38px" valign="top">
                                            <asp:HyperLink ID="HyperLink55" runat="server" Text="ÇáãæÇÞÚ" NavigateUrl="~/AccountAdmin/AccountLocations.aspx"
                                                Width="100px" meta:resourcekey="HyperLink55Resource1"></asp:HyperLink>
                                        </td>
                                        <td
                                            align="center" class="AdministrationOptionsText" style="width: 14%; height: 19px"
                                            valign="top"></td>
                                        <td align="center" class="AdministrationOptionsText"
                                            style="width: 14%; height: 38px" valign="top">
                                            <asp:HyperLink ID="HyperLink56" runat="server" Text="ÇáÃÞÓÇã" NavigateUrl="~/AccountAdmin/AccountDepartments.aspx"
                                                Width="100px" meta:resourcekey="HyperLink56Resource1"></asp:HyperLink>
                                        </td>
                                        <td
                                            align="center" class="AdministrationOptionsText" style="width: 14%; height: 19px"
                                            valign="top"></td>
                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 38px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink57" runat="server" Text="ÇáÇÏæÇÑ" NavigateUrl="~/AccountAdmin/AccountRoles.aspx"
                                                Width="100px" meta:resourcekey="HyperLink57Resource1"></asp:HyperLink>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" style="width: 1706px; height: 95px">
                                <table width="150" style="border: 0">
                                    <tr>
                                        <td align="center" style="width: 10%;" valign="top">
                                            <asp:HyperLink ID="HyperLink18" runat="server"
                                                NavigateUrl="~/AccountAdmin/Employer.aspx">
                                                <asp:Image ID="Image15" runat="server" ImageUrl="~/Images/Employer.gif" AlternateText="ÌåÇÊ ÇáÊæÙíÝ" Width="48px" Height="48px" />

                                            </asp:HyperLink>
                                        </td>
                                        <td align="center" style="width: 14%; height: 19px"></td>
                                        <td align="center" style="width: 10%;" valign="top">
                                            <asp:HyperLink ID="HyperLink19" runat="server"
                                                NavigateUrl="~/AccountAdmin/ContractTypes.aspx" Width="100px">
                                                <asp:Image ID="Image16" runat="server" ImageUrl="~/Images/ContractTypes.gif" AlternateText="ÇäæÇÚ ÇáÚÞæÏ" Width="48px" Height="48px" />

                                            </asp:HyperLink>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td align="center" class="AdministrationOptionsText"
                                            style="width: 14%; height: 38px" valign="top">
                                            <asp:HyperLink ID="HyperLink21" runat="server" Text="ÌåÇÊ ÇáÊæÙíÝ" NavigateUrl="~/AccountAdmin/Employer.aspx"
                                                Width="100px" meta:resourcekey="HyperLink21Resource1"></asp:HyperLink>
                                        </td>
                                        <td
                                            align="center" class="AdministrationOptionsText" style="width: 14%; height: 19px"
                                            valign="top"></td>
                                        <td align="center" class="AdministrationOptionsText"
                                            style="width: 14%; height: 38px" valign="top">
                                            <asp:HyperLink ID="HyperLink22" runat="server" Text="ÇäæÇÚ ÇáÚÞæÏ" NavigateUrl="~/AccountAdmin/ContractTypes.aspx"
                                                Width="100px" meta:resourcekey="HyperLink22Resource1"></asp:HyperLink>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <th class="FormViewSubHeader" colspan="2" style="width: 1706px; height: 22px" dir="rtl">
                                <asp:Literal ID="Literal2" runat="server" Text="ÅÚÏÇÏÇÊ ÇáÈÑäÇãÌ " meta:resourcekey="Literal2Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 1706px; height: 95px">
                                <table width="150" style="border: 0">
                                    <tr>
                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/AccountAdmin/AccountPagePermission.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/RolesPermission.gif" AlternateText="ÇáÕáÇÍíÇÊ" Width="48px" Height="48px" meta:resourcekey="Image8Resource1" />

                                            </asp:HyperLink>
                                        </td>

                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/AccountAdmin/DevicesList.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/fingerprint-reader-icon.gif" AlternateText="ÞÇÆãÉ ÇáÃÌåÒÉ" Width="48px" Height="48px" meta:resourcekey="Image7Resource1" />

                                            </asp:HyperLink>
                                        </td>

                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/AccountAdmin/AccountPreferences.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/license_manager.png" AlternateText="ÅÚÏÇÏÇÊ ÚÇãÉ æÑÎÕÉ ÇáÈÑäÇãÌ" Width="48px" Height="48px" meta:resourcekey="Image12Resource1" />

                                            </asp:HyperLink>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink13" runat="server" Text="ÇáÕáÇÍíÇÊ" NavigateUrl="~/AccountAdmin/AccountPagePermission.aspx"
                                                Width="100px" meta:resourcekey="HyperLink13Resource1"></asp:HyperLink>
                                        </td>

                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink111" runat="server" Text="ÞÇÆãÉ ÇáÃÌåÒÉ" NavigateUrl="~/AccountAdmin/DevicesList.aspx"
                                                Width="100px" meta:resourcekey="HyperLink111Resource1"></asp:HyperLink>
                                        </td>

                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink7" runat="server" Text="ÅÚÏÇÏÇÊ ÚÇãÉ æÑÎÕÉ ÇáÈÑäÇãÌ" NavigateUrl="~/AccountAdmin/AccountPreferences.aspx"
                                                Width="100px" meta:resourcekey="HyperLink7Resource1"></asp:HyperLink>
                                        </td>


                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th class="FormViewSubHeader" colspan="2" style="width: 1706px; height: 22px" dir="rtl">
                                <asp:Literal ID="Literal3" runat="server" Text="ÅÚÏÇÏÇÊ ÇáÍÖæÑ æÇáÇäÕÑÇÝ" meta:resourcekey="Literal3Resource1" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 1706px; height: 95px">
                                <table width="150" style="border: 0">
                                    <tr>
                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink24" runat="server" NavigateUrl="~/AccountAdmin/Policy.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/TimesheetPeriodTypes.gif" AlternateText="ÓíÇÓÇÊ ÇáÏæÇã" Width="48px" Height="48px" meta:resourcekey="Image3Resource1" />

                                            </asp:HyperLink>
                                        </td>

                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink28" runat="server" NavigateUrl="~/AccountAdmin/WorkSchedule.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image13" runat="server" ImageUrl="~/Images/WorkingDays.gif" AlternateText="ÌÏÇæá ÇáÏæÇã" Width="48px" Height="48px" meta:resourcekey="Image13Resource1" />

                                            </asp:HyperLink>
                                        </td>

                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink29" runat="server" NavigateUrl="~/AccountAdmin/Holiday.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image14" runat="server" ImageUrl="~/Images/HolidayManagement.gif" AlternateText="ÇáÚØá ÇáÑÓãíÉ" Width="48px" Height="48px" meta:resourcekey="Image14Resource1" />

                                            </asp:HyperLink>
                                        </td>



                                    </tr>

                                    <tr>
                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink36" runat="server" Text="ÓíÇÓÇÊ ÇáÏæÇã" NavigateUrl="~/AccountAdmin/Policy.aspx"
                                                Width="100px" meta:resourcekey="HyperLink36Resource1"></asp:HyperLink>
                                        </td>

                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink41" runat="server" Text="ÌÏÇæá ÇáÏæÇã" NavigateUrl="~/AccountAdmin/WorkSchedule.aspx"
                                                Width="100px" meta:resourcekey="HyperLink41Resource1"></asp:HyperLink>
                                        </td>

                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink42" runat="server" Text="ÇáÚØá ÇáÑÓãíÉ" NavigateUrl="~/AccountAdmin/Holiday.aspx"
                                                Width="100px" meta:resourcekey="HyperLink42Resource1"></asp:HyperLink>
                                        </td>


                                    </tr>
                                </table>
                            </td>
                        </tr>


                        <tr>
                            <td colspan="2" style="width: 1706px; height: 95px">
                                <table width="150" style="border: 0">
                                    <tr>
                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/AccountAdmin/VacationTypes.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Holidays.gif" AlternateText=" ÃäæÇÚ ÇáÅÌÇÒÇÊ" Width="48px" Height="48px" meta:resourcekey="Image1Resource1" />

                                            </asp:HyperLink>
                                        </td>

                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/AccountAdmin/WorkExceptionTypes.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Exception.gif" AlternateText="ÃäæÇÚ ÇáÅÓÊËäÇÁÇÊ" Width="48px" Height="48px" meta:resourcekey="Image2Resource1" />

                                            </asp:HyperLink>
                                        </td>

                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/AccountAdmin/GatepassTypes.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/GatepassTypes.gif" AlternateText="ÃäæÇÚ ÇáÅÓÊÆÐÇäÇÊ" Width="48px" Height="48px" meta:resourcekey="Image9Resource1" />

                                            </asp:HyperLink>
                                        </td>




                                    </tr>

                                    <tr>
                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink8" runat="server" Text="ÃäæÇÚ ÇáÅÌÇÒÇÊ" NavigateUrl="~/AccountAdmin/VacationTypes.aspx"
                                                Width="100px" meta:resourcekey="HyperLink8Resource1"></asp:HyperLink>
                                        </td>

                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink11" runat="server" Text="ÃäæÇÚ ÇáÅÓÊËäÇÁÇÊ" NavigateUrl="~/AccountAdmin/WorkExceptionTypes.aspx"
                                                Width="100px" meta:resourcekey="HyperLink11Resource1"></asp:HyperLink>
                                        </td>

                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink12" runat="server" Text="ÃäæÇÚ ÇáÅÓÊÆÐÇäÇÊ" NavigateUrl="~/AccountAdmin/GatepassTypes.aspx"
                                                Width="100px" meta:resourcekey="HyperLink12Resource1"></asp:HyperLink>
                                        </td>




                                    </tr>
                                </table>
                            </td>
                        </tr>


                        <tr style="display:block">
                            <td colspan="2" style="width: 1706px; height: 95px">
                                <table width="150" style="border: 0">
                                    <tr>
                                        <td align="center" style="width: 14%; height: 48px;display:block;" valign="top">
                                            <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/AccountAdmin/LeavePolicy.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/TimeOffPolicy.gif" AlternateText="ÓíÇÓÉ ÇáÅÌÇÒÇÊ æÇáÅÓÊÆÐÇäÇÊ æÇáÅÓÊËäÇÁÇÊ" Width="48px" Height="48px" meta:resourcekey="Image10Resource1" />

                                            </asp:HyperLink>
                                        </td>

                                        <td align="center" style="width: 14%; height: 48px" valign="top">
                                            <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/AccountAdmin/ApprovalPolicy.aspx"
                                                Width="100px">
                                                <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/TimesheetApprovalActivity.gif" AlternateText="ãÓÇÑÇÊ ÇáãæÇÝÞÇÊ" Width="48px" Height="48px" meta:resourcekey="Image11Resource1" />

                                            </asp:HyperLink>
                                        </td>
                                    </tr>

                                    <tr>


                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px;display:block;"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink15" runat="server" Text="ÓíÇÓÉ ÇáÅÌÇÒÇÊ æÇáÅÓÊÆÐÇäÇÊ æÇáÅÓÊËäÇÁÇÊ" NavigateUrl="~/AccountAdmin/LeavePolicy.aspx"
                                                Width="100px" meta:resourcekey="HyperLink15Resource1"></asp:HyperLink>
                                        </td>
                                        <td align="center" class="AdministrationOptionsText" style="width: 14%; height: 48px"
                                            valign="top">
                                            <asp:HyperLink ID="HyperLink16" runat="server" Text="ãÓÇÑÇÊ ÇáãæÇÝÞÇÊ" NavigateUrl="~/AccountAdmin/ApprovalPolicy.aspx"
                                                Width="100px" meta:resourcekey="HyperLink16Resource1"></asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
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
</asp:Content>
