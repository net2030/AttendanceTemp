<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlGatepassForm.ascx.vb" Inherits="Task_Controls_ctlGatepassForm" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24D65337282035F2"Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>



<div data-role="header" data-theme="a" >
       <h1 >Gate Pass</h1>
</div>

<div data-role="content" data-theme="d" > 
     
      <ul data-role="listview" > 

          <li >
                <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" Font-Size="Large"  />
          </li>

          <li >
              Id:
              <asp:TextBox ID="txtGatepassId" runat="server" ReadOnly="true" Width="100px" ></asp:TextBox>

          </li>

           <li>
           Type:
               <br />
          <asp:DropDownList ID="ddlGatepassTypeId" runat="server" style="margin-right: 0px" 
                                              DataSourceID="dsGatePassTypes" 
                                              DataTextField="GatepassTypeName" 
                                              DataValueField="GatepassTypeId" >

                                <asp:ListItem Value="0">select</asp:ListItem>
                            </asp:DropDownList>
           <%--    <br />--%>
                            <asp:RequiredFieldValidator Font-Size="Small" InitialValue="0" 
                                                        ID="RequiredFieldValidator1" Display="Dynamic" 
                                                        ControlToValidate="ddlGatepassTypeId"
                                                        runat="server"  Text="*" 
                                                        ErrorMessage="*"
                                                        ForeColor="Red">
                            </asp:RequiredFieldValidator>
          </li>

         <li >
           Date:
              <br />          
             <uc1:MyDate runat="server" ID="datGatepassBegDate"  />
          </li>

         <li >
          <%--  End Date
             <br />--%>
                <uc1:MyDate runat="server" ID="datGatepassEndDate" Visible="false" />
            <%--  <br />--%>
             <%--<asp:CustomValidator id="CustomValidator2" runat="server" 
                                                 ControlToValidate = "datGatepassEndDate$TextBox1"
                                                 ErrorMessage = "End date must be greater or equa !"
                                                 ClientValidationFunction="CompareDate" >
                            </asp:CustomValidator>
             <br />--%>

                From:
           <%--   <br />  &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;--%>
             <telerik:radtimepicker ID="timGatepassBegTime" Runat="server" MinDate="1899-01-01" MaxDate="2099-01-01" CssClass="textboxnew ui-input-text ui-body-null ui-corner-all ui-shadow-inset ui-body-c" Font-Bold="True" Width="110px"   >
            </telerik:radtimepicker>

                            <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator2" ControlToValidate="timGatepassBegTime"
                                                        ErrorMessage="*">

                            </asp:RequiredFieldValidator>
             <br /> 
               <br />
                To:&nbsp;&nbsp;&nbsp;&nbsp
           <%--   <br />  &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;--%>
             <telerik:radtimepicker ID="timGatepassEndTime" Runat="server" MinDate="1899-01-01" MaxDate="2099-01-01" CssClass="textboxnew ui-input-text ui-body-null ui-corner-all ui-shadow-inset ui-body-c" Font-Bold="True" Width="110px"  >
             </telerik:radtimepicker>

             <asp:RequiredFieldValidator Font-Size="Small" runat="server" ID="RequiredFieldValidator5" ControlToValidate="timGatepassEndTime"
                                                        ErrorMessage="*">
             </asp:RequiredFieldValidator>
             <br />
             <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="timGatepassEndTime"
                                                  ControlToCompare="timGatepassBegTime" Operator="GreaterThan" Type="String" ErrorMessage="End time must be greater.">
                            </asp:CompareValidator>
             

                            
         </li>
          

         

         

           <li >
              
               Notes
                <br />
              <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" style="margin-right: 0px" Width="200px" Height="50px"></asp:TextBox>
            </Li>
          
          <li>
               <asp:Button ID="btnSave" runat="server" data-inline="true" Text="Save" UseSubmitBehavior="False" />
               <asp:Button ID="Button2" runat="server" data-inline="true" Text="Cancel" UseSubmitBehavior="False" CausesValidation="false" />
          </li>
      </ul>
      
    
 </div>




<asp:ObjectDataSource ID="dsGatePassTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetGatepassTypesList" TypeName="ATS.BO.Framework.BOGatePass">
</asp:ObjectDataSource>













