<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlVacationForm.ascx.vb" Inherits="Task_Controls_ctlVacationForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>


<div data-role="header" data-theme="a">
    <h1>Vacation</h1>
</div>

<div data-role="content" data-theme="d">

    <ul data-role="listview">

        <li>
            <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" Font-Size="Large" />
        </li>

        <li>Id:
             <asp:TextBox ID="txtVacationId" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>

        </li>

        <li>Type<br />
          <asp:DropDownList ID="ddlVacationTypeId" runat="server" Style="margin-right: 0px"
              DataSourceID="dsVacationTypes"
              DataTextField="TypeName"
              DataValueField="TypeId">
          </asp:DropDownList>
            <%--<br />--%>
         <%--   <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="VacationTypeRequiredFieldValidator" ControlToValidate="ddlVacationTypeId"
                ErrorMessage="*">
            </asp:RequiredFieldValidator>--%>
        </li>

        <li>Start Date:
              <br />
            <uc1:MyDate runat="server" ID="datEffectiveDate" />

        </li>

        <li>End Date:
             <br />
            <uc1:MyDate runat="server" ID="datDateExpire" />
            <br />
             <asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "datDateExpire$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" >
                            </asp:CustomValidator>
        </li>



        <li>

            <asp:Literal ID="Literal7" runat="server" Text="Return" />
            <br />
            <uc1:MyDate runat="server" ID="datDateOfReturn" />
            

        </li>


        <li>Notes
            <br />
              <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Style="margin-right: 0px" Width="200px" Height="50px"></asp:TextBox>
        </li>

        <li>
            <asp:Button ID="btnSave" runat="server" data-inline="true" Text="Save" UseSubmitBehavior="False" />

            <asp:Button ID="Button2" runat="server" data-inline="true" Text="Cancel" UseSubmitBehavior="False" CausesValidation="false" />

        </li>

    </ul>


</div>
<asp:ObjectDataSource ID="dsVacationTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetVacationTypesList" TypeName="ATS.BO.Framework.BOVacation"></asp:ObjectDataSource>

   
	



























