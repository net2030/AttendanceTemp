<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlMenuControl.ascx.vb" Inherits="Menus_Controls_ctlMenuControl" %>
    <div>
       <asp:Menu ID="Menu1" DataSourceID="xmlDataSource" runat="server" 
          BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" 
          ForeColor="#990000" StaticSubMenuIndent="10px" StaticDisplayLevels="1" >
          <DataBindings>
            <asp:MenuItemBinding DataMember="MenuItem" 
             NavigateUrlField="NavigateUrl" TextField="Text" ToolTipField="ToolTip"/>
          </DataBindings>
          <StaticSelectedStyle BackColor="#FFCC66" />
          <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
          <DynamicMenuStyle BackColor="#FFFBD6" />
          <DynamicSelectedStyle BackColor="#FFCC66" />
          <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
          <DynamicHoverStyle BackColor="#990000" Font-Bold="False" ForeColor="White"/>
          <StaticHoverStyle BackColor="#990000" Font-Bold="False" ForeColor="White" />
       </asp:Menu>
       <asp:XmlDataSource ID="xmlDataSource" TransformFile="~/Menus/TransformXSLT.xsl"  
          XPath="MenuItems/MenuItem" runat="server"/> 
    </div>
