<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_AddRoseList.aspx.cs" Inherits="Content_Type_AddRoseList" %>

<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<LINK href="../admin_public/css/xpTable.css" type="text/css" rel="stylesheet">
			<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
				<script language="javascript">
						function OpenParent(sUrl)
						{
							top.frames("funcArea").location.href = sUrl;
						}
				</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<cc3:Toolsbar id="Toolsbar1" OnButtonClick="Toolsbar1_ButtonClick" runat="server" AltText=" 保 存" Text=" 保 存" Width="60" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"></cc3:Toolsbar>
					<TD></TD>
				</tr>
			</table>
			<table id="tablist" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<asp:datagrid id="xpTable" style="BORDER-COLLAPSE: separate" runat="server" CssClass="xpTable"
							Width="100%" BorderStyle="None" BorderWidth="0" Cellpadding="0" AutoGenerateColumns="False">
							<Columns>
								<asp:TemplateColumn HeaderText="选择">
									<ItemStyle Height="18px" Width="50px"></ItemStyle>
									<ItemTemplate>
										<input type=checkbox name='SelectedID' value='<%#DataBinder.Eval(Container.DataItem,"Roles_ID")%>'>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Roles_Name" SortExpression="Roles_Name" HeaderText="角色"></asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

