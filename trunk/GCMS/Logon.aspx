<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"  xml:lang="zh-CN" lang="zh-CN">
	<HEAD>
		<title>GCMS V 2008 管理</title>
		<meta NAME="GENERATOR" Content="Microsoft FrontPage 4.0">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
		<LINK href="Admin_Public/css/Admin.css" rel="stylesheet">
<script language="JavaScript" src="Admin_Public/js/coolbuttons.js"></script>
<script language="JavaScript" src="Admin_Public/js/Div_Move.js"></script>
<script language="JavaScript">
			function closeit()
				{
				this.opener=null;
				this.close();
				}
				
				top.window.moveTo(0,0);
				if (document.all) {
					top.window.resizeTo(screen.availWidth,screen.availHeight);
					}
					else if (document.layers||document.getElementById) {
					if (top.window.outerHeight<screen.availHeight||top.window.outerWidth<screen.availWidth){
						top.window.outerHeight = screen.availHeight;
						top.window.outerWidth = screen.availWidth;
					}
				}
</script>


	</HEAD>
	<BODY class="Dialogss">
		<form id="Form1" method="post" runat="server">


		<div id="Layer1" style="Z-INDEX:1; LEFT:expression((document.body.offsetWidth-480)/2);POSITION:absolute; TOP:expression((document.body.offsetHeight-240)/2)">
		<div class="DialogTab" style="Z-INDEX:10000;FILTER:progid:DXImageTransform.Microsoft.Shadow(color=THREEDDARKSHADOW, Direction=135, Strength=4);WIDTH:480px" align="center">
		<div class="TopTitle" onMouseDown="MM_dragLayer('Layer1','',0,0,0,0,true,false,-1,-1,-1,-1,46,30,50,'',false,'')">
										身份验证 :::: GCMS V 2008</div>
								
			<div style="margin:2px; BORDER:1px solid;"><img src="Admin_Public/Images/Main_CMS.gif" width="468" height="60"></div>

		<img src="Admin_Public/Images/Icon_approval.gif" width="19" height="20">

			<asp:Label id="lblMsg" runat="server"></asp:Label>
													<table border="0" width="100%">
														<tr>
															<td width="17%" rowspan="2"></td>
															<td width="49%">账号:&nbsp;
																<asp:textbox class="inputtext" id="txtAdminName" runat="server" Width="150" tabIndex="1"></asp:textbox>
															</td>
															<td width="34%" rowspan="2" valign="middle">
																<table>
																</table>
																<asp:button id="btnSubmit" runat="server" Text=" 登录 " OnClick="btnSubmit_Click"></asp:button>
															</td>
														</tr>
														<tr>
															<td width="49%">口令:&nbsp;
																<asp:textbox class="inputtext" id="txtAdminPwd" runat="server" Width="150" TextMode="Password"
																	tabIndex="1"></asp:textbox>
															</td>
														</tr>
														<tr>
															<td width="17%" Height="30"></td>
															<td width="49%"><FONT face="宋体"></FONT></td>
															<td width="34%"><FONT face="宋体"></FONT></td>
														</tr>
													</table>
			</div>
		</form>
	</BODY>
</html>