<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlLogsImport.ascx.vb" Inherits="AccountAdmin_Controls_ctlLogsImport" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="~/GeneralControls/MyDate.ascx" TagPrefix="uc1" TagName="MyDate" %>




   <%--  <asp:Timer ID="Timer1" runat="server" Interval="20000">
      </asp:Timer>--%>

    <div>
    
        <table  class="xAdminOption" style="">
                

                            <tr>

                                 <td  style="vertical-align:bottom">
                                     <asp:Label ID="Label1" runat="server" Text="�������:   " AssociatedControlID="LogDate"></asp:Label>
                                     </td>
                                <td style="vertical-align:top">
                                    <uc1:MyDate runat="server" ID="LogDate"  />
                                 </td>

                              
                               
                                </tr>
            <tr>
                <td>
                    
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
            </tr>
            </table>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>
              <asp:Button ID="Button1" runat="server" Text=" ������� �������" OnClick="GetLogs"/>
        <br />
        <br />
                  <x:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                           Caption ="����� ������� ���� �� ���������  " 
                                SkinID="xgridviewSkinEmployee"  CssClass="TableView" OnPageIndexChanging="GridView2_PageIndexChanging" ShowHeaderWhenEmpty="True" PageSize="50" Width="70%">
                        <Columns>

                            <asp:BoundField DataField="UserId" HeaderText="��� ������" />

                            <asp:BoundField DataField="LogDateTime" HeaderText="�����" />

                            <asp:BoundField DataField="LogType" HeaderText="��� ������" />

                            <asp:BoundField DataField="EventType" HeaderText="��� �����" />

                            <asp:BoundField DataField="MachineName" HeaderText="������" />

                      </Columns>
                </x:GridView>


  </div>

  <%--      
<asp:ObjectDataSource id="dsLogs" runat="server" TypeName="BioStarSDK1" SelectMethod="GetLogs" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:Parameter Name="day" Type="DateTime" />
    </SelectParameters>
</asp:ObjectDataSource>--%>

    </ContentTemplate>
</asp:UpdatePanel>



