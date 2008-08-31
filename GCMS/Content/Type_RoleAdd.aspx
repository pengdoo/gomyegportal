<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_RoleAdd.aspx.cs" Inherits="Content_Type_RoleAdd" %>
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
						function AddUser(TypeTree_ID)
						{
							window.location.href = "Type_AddRoseList.aspx?TypeTree_ID="+TypeTree_ID;
						}

					</SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="coolBar" cellSpacing="1" cellPadding="0" width="100%" border="0">
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<cc3:toolsbar id="Toolsbar1" runat="server" OnButtonClick="Toolsbar1_ButtonClick" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"
						Width="100" Text="添加管理角色" AltText="添加管理角色"></cc3:toolsbar>
						<cc3:toolsbar id="Toolsbar2" OnButtonClick="Toolsbar2_ButtonClick" runat="server" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"
						Width="100" Text="删除角色" AltText="删除角色"></cc3:toolsbar>
						<cc3:toolsbar id="Toolsbar3" runat="server" OnButtonClick="Toolsbar3_ButtonClick" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"
						Width="100" Text="应用到子频道" AltText="应用到子频道"></cc3:toolsbar>
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
						<table cellSpacing="0" cellPadding="0" width="100%" border="1">
							<tr>
								<td vAlign="top" bgColor="#ffffff"><asp:datagrid id="xpTable" style="BORDER-COLLAPSE: separate" runat="server" Width="100%" CssClass="xpTable"
										BorderStyle="None" BorderWidth="0" Cellpadding="2" AutoGenerateColumns="False">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="选择" HeaderStyle-Font-Bold="True" ItemStyle-Width="30" ItemStyle-Height="18">
												<ItemTemplate>
													<input type=checkbox name='SelectedID' value='<%#DataBinder.Eval(Container.DataItem,"Roles_ID")%>'>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Roles_Name" SortExpression="Roles_Name" HeaderText="成员名称" HeaderStyle-Font-Bold="True"></asp:BoundColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
