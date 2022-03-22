<%@ Page Title="Leave Policy" Language="VB" MasterPageFile="~/Masters/MasterPageEmployee.master" AutoEventWireup="false" CodeFile="LeavePolicy.aspx.vb" Inherits="AccountAdmin_WorkSchedule" %>

<%@ Register Src="~/AccountAdmin/Controls/ctlTimeOffPolicy.ascx" TagPrefix="uc1" TagName="ctlTimeOffPolicy" %>


<asp:Content ContentID="C" ContentPlaceHolderID="C" Runat="Server">
  
         <uc1:ctlTimeOffPolicy runat="server" id="ctlTimeOffPolicy" />
  
   
</asp:Content>
