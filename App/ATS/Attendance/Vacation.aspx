<%@ Page Title="الاجازات" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="Vacation.aspx.vb" Inherits="AccountAdmin_WorkException" meta:resourcekey="PageResource1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/ctlMyVacation.ascx" TagName="ctlMyVacation" TagPrefix="uc1" %>
<%@ Register Src="Controls/ctlVacation.ascx" TagName="ctlVacation" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentID="C" ContentPlaceHolderID="C" Runat="Server">

     <telerik:RadTabStrip runat="server"
                             ID="RadTabStrip1"
                             Align="Justify"
                             AutoPostBack="True"
                             MultiPageID="RadMultiPage1"
                             SelectedIndex="0"
         causesvalidation="false">
            <Tabs>

                 <telerik:RadTab  Text="إجازاتي" Width="50%" meta:resourcekey="MeResource1"></telerik:RadTab>
                <telerik:RadTab  Text="إجازات الموظفين" Width="50%" meta:resourcekey="EmpResource1"></telerik:RadTab>
               
            </Tabs>
        </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" causesvalidation="false">

         <telerik:RadPageView runat="server"  ID="RadPageView2">
              <uc1:ctlMyVacation ID="ctlMyVacation1" runat="server" />
            </telerik:RadPageView>

            <telerik:RadPageView runat="server"   ID="RadPageView1">
                 <uc1:ctlVacation ID="ctlVacation1" runat="server"/>
            </telerik:RadPageView>

       
    </telerik:RadMultiPage>

     <%--<uc1:ctlVacation ID="ctlVacation1" runat="server"/>--%>

  <script type = "text/javascript">
      var startdatefromsession = "<%=Session("StartDateClientId")%>"; 
      var enddatefromsession = "<%=Session("EndDateClientId")%>"; 
</script>
</asp:Content>
