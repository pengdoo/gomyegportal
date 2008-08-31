<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_AddParenthesis.aspx.cs" Inherits="Content_Type_AddParenthesis" %>
<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Type_Add</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<script language="javascript">
			function closethiswindows()
			{					
				top.window.close();
			}
			
			//命名规则
			function btn_URL() 
			{
					var argu = "dialogWidth:34em; dialogHeight:27em;center:yes;status:no;help:no";
					var file = window.showModalDialog("WindowFrame.aspx?loadfile=Tools_FileMain.aspx",null,argu);
					if (file!=null) {
					document.Form1.TypeTree_URL.value=file;
					}
			}

			 // 页面模板：
			function selectDir()
			{
					var argu = "dialogWidth:34em; dialogHeight:27em;center:yes;status:no;help:no";
					var file = window.showModalDialog("WindowFrame.aspx?loadfile=Tools_FileMain.aspx",null,argu);
					if (file!=null) {
					document.Form1.TypeTree_Template.value=file;
					}
			}



			//-->

		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<table style="WIDTH: 100%" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD style="HEIGHT: 12px" vAlign="top" align="left">
						<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
							<tr>
								<TD width="5"><SPAN class="handbtn"></SPAN></TD>
								<cc3:Toolsbar id="Toolsbar1" OnButtonClick="Toolsbar1_ButtonClick" runat="server" AltText=" 保 存" Text=" 保 存" Width="70" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"></cc3:Toolsbar>
								<TD></TD>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<td>
						<table class="table" style="WIDTH: 100%">
							<TR>
								<th noWrap align="left" height="19">
									&nbsp;
									<asp:label id="Mssg" runat="server"></asp:label></th></TR>
						</table>
					</td>
				</TR>
			</table>
			<TABLE class="DialogTab" id="Table1" style="WIDTH: 100%" height="100%" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" style="WIDTH: 100%" border="0">
							<TR>
								<TD style="WIDTH: 65px" vAlign="middle" align="right">名称：</TD>
								<TD vAlign="top"><INPUT class="inputtext" id="LinkName" type="text" maxLength="100" size="30" runat="server">
								</TD>
							<TR>
								<TD style="WIDTH: 65px" vAlign="middle" align="right">文件：</TD>
								<TD vAlign="top"><INPUT class="inputtext" id="TypeTree_URL" type="text" maxLength="100" size="30" name="TypeTree_URL"
										runat="server"> <input class="button" onclick="btn_URL();" type="button" value="选择..." name="SelectURL"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 65px" vAlign="middle" align="right">模板：</TD>
								<TD vAlign="top"><INPUT class="inputtext" id="TypeTree_Template" type="text" maxLength="100" size="30" name="TypeTree_Template"
										runat="server"> <input class="button" onclick="selectDir();" type="button" value="选择..."></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 65px" vAlign="middle" align="right">状态：</TD>
								<TD vAlign="top">
									<asp:DropDownList id="LinkType" runat="server">
										<asp:ListItem Value="1">可用</asp:ListItem>
										<asp:ListItem Value="0">关闭</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" style="WIDTH: 100%">
				<TR>
					<TH noWrap align="left" height="19">
						<asp:label id="saveResult" runat="server"></asp:label><INPUT id="OrderType" type="hidden" name="OrderType" runat="server">
					</TH>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
