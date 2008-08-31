<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Add.aspx.cs" Inherits="Content_User_Add" %>
<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<LINK href="../admin_public/CSS/Default.CSS" type="text/css" rel="stylesheet">
			<LINK href="../admin_public/CSS/menuStyleXP.CSS" type="text/css" rel="stylesheet">
				<SCRIPT language="JavaScript" src="../admin_public/js/Menu.js"></SCRIPT>
				<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
				<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
					<SCRIPT language="JavaScript">
						function closethiswindows()
						{
						top.windowclose();
						}
					</SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0">
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
								<td vAlign="top" align="right">用户名：</td>
								<td vAlign="top"><asp:textbox class="inputtext" id="Master_UserName" runat="server" size="30" maxlength="100"></asp:textbox></td>
							</tr>
							<tr>
								<td vAlign="top" align="right">姓 名：</td>
								<td vAlign="top"><asp:textbox class="inputtext" id="Master_Name" runat="server" size="30" maxlength="100"></asp:textbox></td>
							</tr>
							<tr>
								<td vAlign="top" align="right">密 码：</td>
								<td vAlign="top"><asp:textbox class="inputtext" id="Master_Password" runat="server" size="30" maxlength="100"
										TextMode="Password"></asp:textbox></td>
							</tr>
							<tr>
								<td vAlign="top" align="right">确认密码：</td>
								<td vAlign="top"><asp:textbox class="inputtext" id="Master_Password1" runat="server" size="30" TextMode="Password"
										maxLength="100"></asp:textbox>&nbsp;
								</td>
							</tr>
							<tr>
								<td vAlign="top" align="right">E-Mail：</td>
								<td vAlign="top"><asp:textbox class="inputtext" id="Master_Email" runat="server" size="30" maxlength="100"></asp:textbox></td>
							</tr>
							<tr>
								<td vAlign="top" align="right">电 话：</td>
								<td vAlign="top"><asp:textbox class="inputtext" id="Master_Tel" runat="server" size="30" maxlength="100"></asp:textbox></td>
							</tr>
							<tr>
								<td vAlign="top" align="right">备 注：</td>
								<td vAlign="top"><asp:textbox class="inputtext" id="Master_Note" runat="server" size="30" maxlength="100" Height="50px"></asp:textbox></td>
							</tr>
							<tr>
								<td vAlign="top" align="right">当前状态：</td>
								<td><select id="Master_Usableness" name="Master_Usableness" runat="server">
										<option value="1" selected>可用</option>
										<option value="0">关闭</option>
									</select>
								</td>
							</tr>
						</table>
						&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
