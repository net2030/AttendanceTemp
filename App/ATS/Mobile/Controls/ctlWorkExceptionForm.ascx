<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlWorkExceptionForm.ascx.vb" Inherits="Task_Controls_ctlWorkExceptionForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>



<div data-role="header" data-theme="a">
    <h1>WorkException</h1>
</div>

<div data-role="content" data-theme="d">

    <ul data-role="listview">

        <li>
            <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" Font-Size="Large" />
        </li>

        <li>Id:
             <asp:TextBox ID="txtWorkExceptionId" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>

        </li>

        <li>Type:
         <asp:DropDownList ID="ddlWorkExceptionTypeId" runat="server" style="margin-right: 0px"
                                              DataSourceID="dsWorkExceptionTypes" 
                                              DataTextField="WorkExceptionTypeName" 
                                              DataValueField="WorkExceptionTypeId" >
                            <asp:ListItem Value="0">....Choose....</asp:ListItem>
                            </asp:DropDownList>
            <%--<br />
                        <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                    ID="RequiredFieldValidator2" Display="Dynamic" 
                                                    ControlToValidate="ddlWorkExceptionTypeId"
                                                    runat="server"  Text="*" 
                                                    ErrorMessage="*"
                                                    ForeColor="Red" ValidationGroup="Insert">
                        </asp:RequiredFieldValidator>--%>
        </li>

        <li>From:
           <%--   <br />--%>
              <uc1:MyDate runat="server" ID="datWorkExceptionBegDate" />
        </li>

        <li>To:
            <%-- <br />--%>
            <uc1:MyDate runat="server" ID="datWorkExceptionEndDate" />
           <br />
             <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "datWorkExceptionEndDate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" >
                            </asp:CustomValidator>
        </li>

        <li>
            Notes
            <br />
              <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Style="margin-right: 0px" Width="200px" Height="50px"></asp:TextBox>
        </li>

        <li>
            <asp:Button ID="btnSave" runat="server" data-inline="true" Text="Save" UseSubmitBehavior="False" />

            <asp:Button ID="Button2" runat="server" data-inline="true" Text="Cancel" UseSubmitBehavior="False" CausesValidation="false"/>



        </li>

    </ul>


</div>


<asp:ObjectDataSource ID="dsWorkExceptionTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWorkExceptionTypesDataset" TypeName="ATS.BO.Framework.BOWorkExceptionType">
    <SelectParameters>
        <asp:Parameter DefaultValue="1" Name="PageNo" Type="Int32" />
        <asp:Parameter DefaultValue="50" Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

   
	



























