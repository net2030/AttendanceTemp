<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlEmployeesLeaveBalance.ascx.vb" Inherits="AccountAdmin_Controls_ctlEmployeesLeaveBalance"  %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="~/ctlDepartmentTree.ascx" TagPrefix="uc1" TagName="ctlDepartmentTree" %>





<%@ Register src="../../GeneralControls/MyDate.ascx" tagname="mydate" tagprefix="uc1" %>
<style type="text/css">
.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}
    </style>
        <x:GridView ID="gvBalance" runat="server" AutoGenerateColumns="False" 
                    SkinID="xgridviewSkinTimesheet"
                    DataKeyNames="TimeOffBalanceId,EmployeeId,VersionNo,TypeId" meta:resourcekey="gvBalanceResource1" >
            <Columns>

                 

                <asp:TemplateField HeaderText = "ÇáäæÚ" meta:resourcekey="TemplateFieldResource1">

                    <ItemTemplate>
                       <%# Eval("TypeName")%>
                    </ItemTemplate>

                </asp:TemplateField>
          

                <asp:TemplateField HeaderText = "ÇáÑÕíÏ ÇáãÊæÝÑ" meta:resourcekey="TemplateFieldResource2">

                    <ItemTemplate>
                       <%# Eval("Available")%>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText = "ÇáÑÕíÏ ÇáãÓÊåáß" meta:resourcekey="TemplateFieldResource3">

                    <ItemTemplate>
                       <%# Eval("Used")%>
                    </ItemTemplate>

                </asp:TemplateField>


                <asp:TemplateField HeaderText = "ÇáÑÕíÏ ÇáãÑÍá" meta:resourcekey="TemplateFieldResource4">

                    <ItemTemplate>
                       <%# Eval("CarryForward")%>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="txtCarryForward" Value='<%# CInt(Eval("CarryForward")) %>' runat="server" Width="70px" Culture="en-US" DbValueFactor="1" LabelCssClass="" LabelWidth="64px" meta:resourcekey="txtCarryForwardResource1">
                            <NumberFormat ZeroPattern="n" DecimalDigits="2" GroupSizes="6"></NumberFormat>
                        </telerik:RadNumericTextBox>
                    </EditItemTemplate>

                    

                </asp:TemplateField>

             <%--  <asp:BoundField DataField="LastEarnedDate" HeaderText="ÊÇÑíÎ ÇáÇßÊÓÇÈ" />--%>
                


                <asp:TemplateField meta:resourcekey="TemplateFieldResource5" >

                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update" meta:resourcekey="lnkUpdateResource1"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" meta:resourcekey="lnkCancelResource1"></asp:LinkButton>

                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" CommandName="Edit" Visible='<%# If(ATS.BO.BOPagePermission.IsPageHasPermissionOf(199, 3) And Eval("IsCarryForward").ToString().Equals("True"), True, False) %>' cssclass="edit_button" runat="server"  Text="Edit" CausesValidation="False" meta:resourcekey="lnkEditResource1" ></asp:LinkButton>
                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>


            <PagerSettings PageButtonCount="21" />
        </x:GridView>















 