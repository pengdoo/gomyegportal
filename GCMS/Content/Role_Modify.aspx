<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Role_Modify.aspx.cs" Inherits="Content_Role_Modify" %>
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
						function AddUser(sRoles_ID)
						{
							//var argu = "dialogWidth:32em; dialogHeight:25em;center:yes;status:no;help:no";
							//window.showModalDialog("WindowFrame.aspx?loadfile=addUserList.aspx&Roles_ID="+sRoles_ID,"添加用户",argu);
							//window.close();
							window.location.href = "Role_AddUserList.aspx?Roles_ID="+sRoles_ID;
						}
						function AddPopedom(sRoles_ID)
						{
							window.location.href = "Role_AddPopedomList.aspx?Roles_ID="+sRoles_ID;
						}
					</SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="coolBar" style="WIDTH:100%;HEIGHT:20px" cellpadding='0' cellspacing='1' border='0'>
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<cc3:toolsbar id="Toolsbar1" OnButtonClick="Toolsbar1_ButtonClick" runat="server" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"
						Width="55" Text=" 保 存" AltText=" 保 存"></cc3:toolsbar>
					<td class="coolButton" onClick="window.location.href = 'Role_AddUserList.aspx?Roles_ID='+Form1.txtTypeTree_ID.value"
						title="添加用户" width="80" height="10">
						<img src="../Admin_Public/Images/Icon_Master_on.gif"> 添加用户</td>
					<cc3:toolsbar id="Toolsbar2" OnButtonClick="Toolsbar2_ButtonClick" runat="server" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"
						Width="80" Text=" 删除用户" AltText=" 删除用户"></cc3:toolsbar>
					<td class="coolButton" onClick="window.location.href = 'Role_AddPopedomList.aspx?Roles_ID='+Form1.txtTypeTree_ID.value"
						title="设置权限" width="80" height="10">
						<img src="../Admin_Public/Images/Icon_Master.gif"> 设置权限</td>
					<td></td>
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
						<table cellSpacing="0" cellPadding="0" width="100%" border="1">
							<tr>
								<td vAlign="top" bgColor="#ffffff"><asp:datagrid id="xpTable" style="BORDER-COLLAPSE: separate" runat="server" Width="100%" AutoGenerateColumns="False"
										Cellpadding="2" BorderWidth="0" BorderStyle="None" CssClass="xpTable">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="选择" HeaderStyle-Font-Bold="True" ItemStyle-Width="30" ItemStyle-Height="18">
												<ItemTemplate>
													<input type=checkbox name='SelectedID' value='<%#DataBinder.Eval(Container.DataItem,"Master_ID")%>'>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Master_Name" SortExpression="Master_Name" HeaderText="成员名称" HeaderStyle-Font-Bold="True"></asp:BoundColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
						</table>
						<INPUT type="hidden" name="txtTypeTree_ID" id="txtTypeTree_ID" runat="server">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
