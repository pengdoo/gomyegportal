<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_TypeAddField.aspx.cs" Inherits="Content_Type_TypeAddField" %>

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
						
			function closethiswindows()
			{					
				top.window.close();
			}
				</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="coolBar" cellSpacing="1" cellPadding="0" width="100%" border="0">
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<cc3:toolsbar id="Toolsbar1" runat="server" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"
						Width="55" Text=" 保 存" AltText=" 保 存"  OnButtonClick="Toolsbar1_ButtonClick"></cc3:toolsbar>
					<TD></TD>
				</tr>
			</table>
			<table id="tablist" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 300px">
							<asp:datagrid id="xpTable" style="BORDER-COLLAPSE: separate" runat="server" CssClass="xpTable"
								Width="100%" BorderStyle="None" BorderWidth="0" Cellpadding="0" AutoGenerateColumns="False">
								<Columns>
									<asp:TemplateColumn HeaderText="选择">
										<ItemStyle Height="18px" Width="50px"></ItemStyle>
										<ItemTemplate>
											<input type=radio name='FieldsName_ID' value='<%#DataBinder.Eval(Container.DataItem,"FieldsName_ID")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="FieldsName_Name" SortExpression="FieldsName_Name" HeaderText="扩展字段名"></asp:BoundColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
