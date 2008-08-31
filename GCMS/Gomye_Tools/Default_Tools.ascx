<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default_Tools.ascx.cs" Inherits="Gomye_Tools_Default_Tools" %>
<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>

						<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
							<tr>
								<td width="5"><SPAN class="handbtn"></SPAN></td>
								<td class="coolButton" nowrap onclick="top.window.location.reload();" title="首页" width="20"
									height="20"><img border="0" src="Admin_Public/Images/Icon_Home.gif" width="16" height="17">
								</td>
								<td class="coolButton" nowrap onclick="javascript:parent.history.length!=0?parent.history.back():alert('无可用历史记录！');"
									title="后退" width="20" height="20"><img border="0" src="Admin_Public/Images/Icon_Back.gif" width="16" height="17">
								</td>
								<td class="coolButton" nowrap onclick="javascript:parent.history.length!=0?parent.history.forward():alert('无可用历史记录！');"
									title="前进" width="20" height="20"><img border="0" src="Admin_Public/Images/Icon_Forword.gif" width="16" height="17">
								</td>
								<td class="coolButton" onClick="top.window.location.reload();" width="20" height="20">
									<img src="Admin_Public/Images/Icon_File_ReFresh.gif" alt="刷新"></td>
								<td width="5"><SPAN class="sepbtn1"></SPAN></td>

								<cc3:Toolsbar id="Toolsbar1" runat="server" AltText ="安全退出" Text="安全退出" Width = 75
								 imageNormal= "Admin_Public/Images/PHPSessionVars.gif" OnButtonClick="Toolsbar1_ButtonClick"></cc3:Toolsbar>
								 
								<td width="5"><SPAN class="sepbtn1"></SPAN></td>

								<!--			<td width="5"><SPAN class="sepbtn1"></SPAN></td>
								<td class="coolButton" id="doContent" onClick="javascript:Content.Tools_Leftframe.location='Content/Tools_Left.aspx'" width="75" height="20">
									<img src="Admin_Public/Images/Icon_Master.gif" alt="内容管理"> 内容管理</td> 
								<td class="coolButton" id="doFeedback" onClick="javascript:Content.Tools_Leftframe.location='Feedback/Tools_Left.aspx';" width="75" height="20">
									<img src="Admin_Public/Images/Icon_Master.gif" alt="信息反馈"> 信息反馈</td> 
					-->

								<td class="coolButton" id="setPass" onClick="doHelp();" width="55" height="20">
									<img src="Admin_Public/Images/Icon_File_Help.gif" alt="帮助"> 帮助</td>
								<td></td>
								<td width="55" align="center"><span id="msgpic"></span></td>
								<td class="coolButton" onClick="Leftswitch();" id="Leftbutton" title="关闭导航" align="center"
									width="18" height="18">
									<font face="Marlett">
										<span id="LeftbuttonFont">3</span></font>
								</td>
								<td width="55" bgcolor="#000000" nowrap align="center" onclick="javascript:this.opener=null;this.close();">
									<OBJECT codeBase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
										height="24" width="57" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" VIEWASTEXT>
										<PARAM NAME="_cx" VALUE="1508">
										<PARAM NAME="_cy" VALUE="635">
										<PARAM NAME="FlashVars" VALUE="">
										<PARAM NAME="Movie" VALUE="Admin_Public/Images/gomye.swf">
										<PARAM NAME="Src" VALUE="Admin_Public/Images/gomye.swf">
										<PARAM NAME="WMode" VALUE="Window">
										<PARAM NAME="Play" VALUE="-1">
										<PARAM NAME="Loop" VALUE="-1">
										<PARAM NAME="Quality" VALUE="High">
										<PARAM NAME="SAlign" VALUE="">
										<PARAM NAME="Menu" VALUE="-1">
										<PARAM NAME="Base" VALUE="">
										<PARAM NAME="AllowScriptAccess" VALUE="">
										<PARAM NAME="Scale" VALUE="NoScale">
										<PARAM NAME="DeviceFont" VALUE="0">
										<PARAM NAME="EmbedMovie" VALUE="0">
										<PARAM NAME="BGColor" VALUE="">
										<PARAM NAME="SWRemote" VALUE="">
										<PARAM NAME="MovieData" VALUE="">
										<PARAM NAME="SeamlessTabbing" VALUE="1">
										<PARAM NAME="Profile" VALUE="0">
										<PARAM NAME="ProfileAddress" VALUE="">
										<PARAM NAME="ProfilePort" VALUE="0">
										<embed src="Admin_Public/Images/gomye.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
											type="application/x-shockwave-flash" width="57" height="24"> </embed>
									</OBJECT>
								</td>
							</tr>
						</table>