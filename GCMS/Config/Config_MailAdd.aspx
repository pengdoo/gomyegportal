<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Config_MailAdd.aspx.cs" Inherits="Config_Config_MailAdd" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<WEBAPPCONTROLS:Tools_Head id="Tools_Head" runat="server"></WEBAPPCONTROLS:Tools_Head>
	</HEAD>
	<body MS_POSITIONING="GridLayout">

					<form id="Form1" method="post" runat="server">
				<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<cc3:Toolsbar id="Toolsbar1" runat="server" OnButtonClick="Toolsbar1_ButtonClick" AltText=" 保 存" Text=" 保 存" Width="55" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"></cc3:Toolsbar>
					<TD></TD>
				</tr>
			</table>
			<table class="table" style="WIDTH: 100%">
				<TR>
					<th noWrap align="left" height="19">
						&nbsp;<b><asp:label id="textMsg" runat="server"></asp:label></b>
					</th>
				</TR>
			</table>
	
			<table class="DialogTab" id="ListTab" height="100%" cellSpacing="1" cellPadding="1" width="100%"
				border="0" runat="server">
										<tr>
											<td vAlign="top">
												<table>
													<tr>
														<td vAlign="top" align="right" width="150">发送邮件服务器：</td>
														<td vAlign="top"><asp:textbox class="inputtext" id="JMail_Server" runat="server" size="30" maxlength="100"></asp:textbox></td>
													</tr>
													<tr>
														<td vAlign="top" align="right" width="150">发送邮件Mail：</td>
														<td vAlign="top"><asp:textbox class="inputtext" id="JMail_From" runat="server" size="30" maxlength="100"></asp:textbox></td>
													</tr>
													<tr>
														<td vAlign="top" align="right" width="150">发送Mail用户名：</td>
														<td vAlign="top"><asp:textbox class="inputtext" id="JMail_MailServerUserName" runat="server" size="30" maxlength="100"></asp:textbox></td>
													</tr>
													<tr>
														<td vAlign="top" align="right" width="150">发送Mail密码：</td>
														<td vAlign="top"><asp:textbox class="inputtext" id="JMail_MailServerPassWord" runat="server" size="30" maxlength="100"
																TextMode="Password"></asp:textbox> 不填写则不修改</td>
													</tr>
												</table>
												<P>&nbsp;&nbsp;&nbsp; <INPUT id="PID" type="hidden" runat="server" NAME="PID">
												</P>
											</td>
										</tr>
									</table>
		
					</form>

	</body>
</HTML>