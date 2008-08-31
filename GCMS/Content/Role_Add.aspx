<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Role_Add.aspx.cs" Inherits="Content_Role_Add" %>

<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
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

			<table class="coolBar" cellSpacing="1" cellPadding="0" width="100%" border="0">
							<tr>
								<TD width="5"><SPAN class="handbtn"></SPAN></TD>
								<cc3:toolsbar id="Toolsbar1" runat="server" OnButtonClick="Toolsbar1_ButtonClick" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"
									Width="55" Text=" 保 存" AltText=" 保 存"></cc3:toolsbar>
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
			<table class="DialogTab" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<table>
							<tr>
								<td vAlign="top" align="right">角色名称：</td>
								<td vAlign="top"><asp:textbox class="inputtext" id="Roles_Name" runat="server" size="30" maxlength="100" Width="288px"></asp:textbox></td>
							</tr>
							<tr>
								<td vAlign="top" align="right">角色说明：</td>
								<td vAlign="top"><asp:textbox class="inputtext" id="Roles_Explan" runat="server" size="30" maxlength="100" Width="288px"
										Height="63px"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>