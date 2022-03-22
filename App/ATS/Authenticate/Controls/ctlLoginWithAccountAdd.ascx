<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlLoginWithAccountAdd.ascx.vb" Inherits="Authenticate_Controls_ctlLoginWithAccountAdd" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="OT_body">
       <div class="bg_color">
			   <div class="header_bg">
			   
					<div id="wrapper">
							<div id="header">
								<div class="logo">
                                    <asp:Image ID="imglogo" runat="server" Height="50px" /></div>
								 <div class="header_right">
									 <div class="email">
											<div class="email_text">Email</div>
										
											<div class="email_box">
											<asp:TextBox ID="txtemail" runat="server" style="width:148px; height:16px; float:left; background-color:#ffffff; border: solid #106a84 1px; font-size:12px; font-family:Arial, Helvetica, sans-serif; color:#000000;margin-top:5px; padding:2px;"></asp:TextBox>
                                                </div>
                                        								 	<br /><br /><br /><br /><br /><br /><asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                         <ContentTemplate>
                                         <table width=375><tr><td style="text-align:center"><asp:Label ID="lblErrorMessage" runat="server" Text="Invalid email or password." Width=375px BackColor=MistyRose BorderColor=Red BorderStyle=Solid BorderWidth=1px Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Visible="False"></asp:Label></td></tr></table>
                                         </ContentTemplate>
                                         </asp:UpdatePanel>
									 </div>
									 
									 <div class="email_right">
										<div class="email_text">Password</div>
											<div class="password_box"><asp:TextBox
                                                    ID="txtpassword" runat="server" 
                                                    style="width:150px; height:18px; float:left; background-color:#ffffff; border: solid #106a84 1px; font-size:12px; font-family:Arial, Helvetica, sans-serif; color:#000000; padding-left:1px; margin-top:5px; padding-top:2px;" 
                                                    TextMode="Password"></asp:TextBox></div>
											    <a href="Authenticate/PasswordReset.aspx" target="_blank" id="lnkfp" class="forgot_pass_word" >Forgot your password?</a>
                                                </div>

										<!--<div class="login"><a href="#"> Log In</a></div>-->
										<div class="login">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                            							 <asp:Button ID="btnlogin" runat="server" Text="Log In" border="0" style="width:47px;	height:20px;Background-color:#24b1d7;
		float:right;	text-align:center;cursor:pointer;font-family:'Lucida Grande',Tahoma,Verdana,Arial,sans-serif; border:0px;  color:#FFFFFF; font-weight:bold; font-size:11px; padding: 1px 0 2px;" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>

										</div>
									                                          
						 
								</div>

						   </div>

						   <div class="spacer"></div>
						   							
						 	<!--middle part start here --> 
							<div class="middle_part">
								<div class="middle_part_left">
								<div class="textbold">Online web timesheet that assures project success.</div>
									<div class="left_image_box">
										<!--left_image_box_start-->
										<div class="left_image_boxtesting">
										<div class="left_image_box_left">
												<div class="timeicon" style="margin-left:55px;"></div>
												<div class="text1" style="margin-left:114px; margin-top:-32px;">Timesheet</div>
												<div class="timearrow" style="margin-left:118px; margin-top:5px;"></div>
												
											<div class="clienticon" style="margin-right:115px; float:right; margin-top:-88px;"></div>
											<div class="text1" style="margin-left:253px; margin-top:-62px; float:left; position:absolute;">Clients</div>
											<div class="clientarrow" style="margin-left:220px; margin-top:-48px; float:left; position:absolute;"></div>
												
											<div class="taskicon" style="margin-top:-30px; margin-left:10px;"></div>
											<div class="text1" style="margin-left:68px; margin-top:-31px;">Tasks</div>
											<div class="taskarrow" style="margin-left:82px;"></div>
											
												<div class="adminsicon" style="margin-right:50px; float:right; margin-top:-64px;"></div>
												<div class="text1" style="margin-left:318px; margin-top:-45px; float:left; position:absolute;">Admins</div>
												<div class="adminsarrow" style="margin-left:278px; margin-top:-29px; float:left; position:absolute;"></div>
												
											<div class="milestoneicon" style="margin-left:3px; margin-top:5px;"></div>
											<div class="text1" style="margin-left:53px; margin-top:-57px;">Mile Stones</div>
											<div class="milestonearrow" style="margin-left:47px; margin-top:-12px;"></div>
											
												<div class="managersicon" style="margin-right:26px; float:right; margin-top:-30px;"></div>
												<div class="text1" style="margin-right:-6px; margin-top:-20px; float:right;">Managers</div>
												<div class="managersarrow" style="margin-top:-25px; margin-left:280px; float:left; position:absolute;"></div>
											
											<div class="billingicon" style="margin-top:40px; margin-left:12px;"></div>
											<div class="text1" style="margin-left:81px; margin-top:-9px;">Expenses & Billing</div>
											<div class="billingarrow" style="margin-left:81px; margin-top:-92px;"></div>
											
												<div class="teamicon" style="margin-right:48px; float:right; margin-top:-5px;"></div>
												<div class="text1" style="margin-right:-2px; margin-top:37px; float:right;">Team Members</div>
												<div class="teamarrow" style="margin-right:-85px; margin-top:-40px; float:right;"></div>
												
											<div class="projecticon" style=" margin-top:-113px; margin-left:163px; position:absolute;"></div>
											<div class="project_text" style="margin-left:187px; margin-top:-15px; position:absolute; ">Projects</div>
										</div>
										</div>
										<!--left_image_box_end-->
									</div>
									<div class="textbold12">Track your contractor and employee's timesheet using full featured and easy to use Time Entry tool.</div>
										</div>
								<div class="middle_part_right">
									<div class="signup">Sign Up</div>
									<div class="middle_part_right_txt">Provide your information</div>
									<div class="middle_part_right_line"></div>
										<div class="loginbox">
											<div class="loginbox_left_txt">
												<li>First Name:</li>
												<li style="*width:100px; *margin-left:60px;">Last Name:</li>
												<li>Your Email:</li>
												<li style="*margin-top:11PX;">New Password:</li>
												<li style="*margin-top:14PX;">Verify Password:</li>
												<li style="*margin-top:25PX; margin-top:11px;" ><font class="OrgName">Organization Name:</font></li>
												<li style="margin-top:10px;*margin-top:16px;">Country:
                                                </li>
											</div>
											
											
											<div class="loginbox_right_txt">
												<li style="*margin-left:-15px;">
                                                    <asp:TextBox ID="txtfirstname" runat="server" CssClass="txt_box"></asp:TextBox>
                                                </li>
												<li style="*margin-left:-15px;"><asp:TextBox ID="txtlastname" runat="server" CssClass="txt_box"></asp:TextBox></li>
												<li style="*margin-left:-15px;"><asp:TextBox ID="txtemailr" runat="server" CssClass="txt_box"></asp:TextBox></li>
												<li style="*margin-left:-15px;"><asp:TextBox ID="txtpasswordr" runat="server" CssClass="txt_box" TextMode="Password" MaxLength="50"></asp:TextBox></li>
												<li style="*margin-left:-15px;"><asp:TextBox ID="txtverifypwd" runat="server" CssClass="txt_box" TextMode="Password" MaxLength="50"></asp:TextBox></li>
												<li style="*margin-left:-15px;"><asp:TextBox ID="txtorgname" runat="server" CssClass="txt_box"></asp:TextBox></li>
												<li style="*margin-left:-15px; ">
                                                    <asp:DropDownList ID="drpcountry" runat="server" class="txt_box1" DataSourceID="dsSystemCountries" DataTextField="Country" DataValueField="CountryId">
                                                    </asp:DropDownList>
												</li>
							
							<li>
							<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>                                                                
                                <asp:Button ID="btnregister" runat="server" Text="Sign Up" class="signup_button" border="0" style="font-family:Verdana, Arial, Helvetica, sans-serif;	font-size:12px;	color:#FFFFFF;	list-style:none;	float:right;	list-style:none; font-weight:bold; border:0px; *padding-top:0px; *height:33px; height:32px; padding-top:2px;" ValidationGroup="Signup"/>                                
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress id="UpdateProgress2" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="UpdatePanel4" >
                                <ProgressTemplate>                                
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ajax-loader.gif" ImageAlign="Left"/> 
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            </li>
                            
                                </div>                                
                                </div>
                                <div class="middle_part_right_line1">                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                <table><tr><td style="text-align:center"><asp:Label ID="lblSUEM" runat="server" Text="You must fill in all of the fields." Width=375px BackColor=MistyRose BorderColor=Red BorderStyle=Solid BorderWidth=1px Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Visible="False"></asp:Label></td></tr></table>                                
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                <table><tr><td style="text-align:center"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Width=375px BackColor=MistyRose BorderColor=Red BorderStyle=Solid BorderWidth=1px Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" ErrorMessage="Invalid email address" ControlToValidate="txtemailr" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Signup"></asp:RegularExpressionValidator></td></tr></table>                                
                                <table><tr><td style="text-align:center"><asp:CompareValidator ID="CompareValidator1" runat="server" Width=375px BackColor=MistyRose BorderColor=Red BorderStyle=Solid BorderWidth=1px Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" ErrorMessage="Password Mismatch" ControlToCompare="txtpasswordr" ControlToValidate="txtverifypwd" ValidationGroup="Signup"></asp:CompareValidator></td></tr></table>                                </div>
								</div>
								
							</div>
							<!--middle part start here -->
							<div class="spacer1"></div>
							<!--footer part start here -->
							<div class="footer_OT">
							<asp:Label ID="lblFooter" runat="server"></asp:Label></div>
								<div class="footerdown_line"></div>								
							<div class="footer_part">
                                <asp:ObjectDataSource ID="dsSystemCountries" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCountries" TypeName="SystemDataBLL"></asp:ObjectDataSource>
							<!--footer part link start here -->
								
								<!--footer part link end here -->
								
								
							</div>
							
							<!--footer part end here -->
                        
				</div>
			</div>
			
</div>