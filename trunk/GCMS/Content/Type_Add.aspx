<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_Add.aspx.cs" Inherits="Content_Type_Add" %>
<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<title>Type_Add</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
		<LINK href="../admin_public/css/listview.css" type="text/css" rel="STYLESHEET">
		<STYLE>.cmsField {
	BORDER-RIGHT: green 1px solid; BORDER-TOP: green 1px solid; BACKGROUND: white; BORDER-LEFT: green 1px solid; BORDER-BOTTOM: green 1px solid
}
		</STYLE>
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<script language="javascript">
			function closethiswindows()
			{					
				top.window.close();
			}
			
			//命名规则
			function btn_URL() 
			{
				var oldValue = document.Form1.TypeTree_URL.value;
				var argu = "dialogWidth:37em; dialogHeight:18em;center:yes;status:no;help:no";
				var newValue = window.showModalDialog("Type_SelectURL.aspx?oldValue=" + oldValue,null,argu);
				if (newValue!=null) {
				document.Form1.TypeTree_URL.value=newValue;}
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
			
			// 列表文件
			function List_URL() 
			{
					var argu = "dialogWidth:34em; dialogHeight:27em;center:yes;status:no;help:no";
					var file = window.showModalDialog("WindowFrame.aspx?loadfile=Tools_FileMain.aspx",null,argu);
					if (file!=null) {
				document.Form1.TypeTree_ListURL.value=file;}
			}

			// 列表模板：
			function selectList()
			{ 
					var argu = "dialogWidth:34em; dialogHeight:27em;center:yes;status:no;help:no";
					var file = window.showModalDialog("WindowFrame.aspx?loadfile=Tools_FileMain.aspx",null,argu);
					if (file!=null) {
					document.Form1.TypeTree_ListTemplate.value=file;
					}
			}
			
			
			// 列表模板：
			function PictureURL()
			{ 
					var argu = "dialogWidth:34em; dialogHeight:27em;center:yes;status:no;help:no";
					var file = window.showModalDialog("WindowFrame.aspx?loadfile=Tools_FileMain.aspx",null,argu);
					if (file!=null) {
					document.Form1.TypeTree_PictureURL.value=file;
					}
			}
			
			function selectImages(el) {
					var argu = "dialogWidth:320px; dialogHeight:130px;center:yes;status:no;help:no";
					var newimage=window.showModalDialog("WindowFrame.aspx?loadfile=Tools_UploadFiles.aspx",null,argu);
					if (newimage!=null) {
						el.value = newimage;
						}
			}
			
			
			function editHTML(el){
			var args="dialogWidth:720px";
			var strHTML = window.showModalDialog("ScriptletHtml/edit.asp",el,args);
			if (strHTML!=null)
				el.value = strHTML;
			}
			//-->

		function selectdate(el){
			var args="font-size:10px;dialogWidth:286px;dialogHeight:290px;center:yes;status:no;help:no";
			var argu=new Array();
			argu[0]="";
			var selectdate=window.showModalDialog("Tools_SelectDate.html",argu,args);
			if (selectdate!=null)
			el.value=selectdate;
			}
			
		
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<table style="WIDTH: 100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<table class="table" style="WIDTH: 100%">
							<tr>
								<th noWrap align="left" height="20">
									&nbsp;<asp:label id="Mssg" runat="server"></asp:label></th></tr>
						</table>
						<table class="coolBar" cellSpacing="1" cellPadding="0" width="100%" border="0">
							<tr>
								<TD width="5"><SPAN class="handbtn"></SPAN></TD>
								<cc3:toolsbar id="Toolsbar1" runat="server" AltText=" 保 存" Text=" 保 存" Width="55" imageNormal="../Admin_Public/Images/Icon_File_Save.gif" OnButtonClick="Toolsbar1_ButtonClick"></cc3:toolsbar>
								<TD></TD>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<TABLE class="DialogTab" id="Table1" style="WIDTH: 100%" height="100%" border="0">
				<tr>
					<TD valign="top">
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 80%">
							<TABLE id="Table2" style="WIDTH: 100%" border="0">
								<tr>
									<TD style="WIDTH: 65px; HEIGHT: 3px" valign="middle" align="right">中文名称：</TD>
									<TD style="HEIGHT: 3px" valign="top"><asp:textbox class="inputtext" id="TypeTree_CName" runat="server" maxlength="100" size="30"></asp:textbox></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">英文名称：</TD>
									<TD valign="top"><asp:textbox class="inputtext" id="TypeTree_EName" runat="server" maxlength="100" size="30"></asp:textbox></TD>
								</tr>
								<tr>
									<TD align="right" colSpan="2"><asp:table id="Table4" runat="server" Width="100%" cellSpacing="0" cellPadding="1" border="0"></asp:table></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">命名规则：</TD>
									<TD valign="top"><INPUT class="inputtext" id="TypeTree_URL" type="text" maxLength="100" size="30" name="TypeTree_URL"
											runat="server"> <input class="button" onclick="btn_URL();" type="button" value="选择..." name="SelectURL"></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">页面模板：</TD>
									<TD valign="top"><FONT face="宋体"><INPUT class="inputtext" id="TypeTree_Template" type="text" maxLength="100" size="30" name="TypeTree_Template"
												runat="server"></FONT> <input class="button" onclick="selectDir();" type="button" value="选择..."></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">图片路径：</TD>
									<TD valign="top"><FONT face="宋体"><INPUT class="inputtext" id="TypeTree_PictureURL" type="text" maxLength="100" size="30"
												name="TypeTree_PictureURL" runat="server"></FONT> <input class="button" onclick="PictureURL();" type="button" value="选择..."></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">列表文件：</TD>
									<TD valign="top"><FONT face="宋体"><INPUT class="inputtext" id="TypeTree_ListURL" type="text" maxLength="100" size="30" name="TypeTree_ListURL"
												runat="server"></FONT> <input class="button" onclick="List_URL();" type="button" value="选择..."></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">列表模板：</TD>
									<TD valign="top"><FONT face="宋体"><INPUT class="inputtext" id="TypeTree_ListTemplate" type="text" maxLength="100" size="30"
												name="TypeTree_ListTemplate" runat="server"></FONT> <input class="button" onclick="selectList();" type="button" value="选择..." name="SelectURL"></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">分类图片：</TD>
									<TD valign="top"><FONT face="宋体"><INPUT class="inputtext" id="TypeTree_Images" type="text" maxLength="100" size="30" name="TypeTree_Images"
												runat="server"></FONT> <input class="button" onclick="selectImages(TypeTree_Images);" type="button" value="选择..."
											name="SelectURL"></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">分类说明：</TD>
									<TD valign="top"><asp:textbox class="inputtext" id="TypeTree_Explain" runat="server" Width="200px" maxlength="100"
											size="30" Height="70px" TextMode="MultiLine"></asp:textbox></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">
										<P>列表数目：</P>
									</TD>
									<TD valign="top"><asp:textbox class="inputtext" id="List_Amount" runat="server" maxlength="100" size="30"></asp:textbox>&nbsp;&nbsp;条/页</TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">当前状态：</TD>
									<TD><asp:dropdownlist id="TypeTree_Issuance" runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">栏目类别：</TD>
									<TD><asp:dropdownlist id="TypeTree_Type" runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">编辑语言：</TD>
									<TD><asp:dropdownlist id="TypeTree_Language" runat="server">
											<asp:ListItem Value="GB2312">简体中文或英文</asp:ListItem>
											<asp:ListItem Value="BIG5">繁体中文</asp:ListItem>
											<asp:ListItem Value="Shift_JIS">日文</asp:ListItem>
											<asp:ListItem Value="euc-kr">韩文</asp:ListItem>
											<asp:ListItem Value="windows-1251">俄语</asp:ListItem>
										</asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD style="WIDTH: 65px" valign="middle" align="right">邮件通知：</TD>
									<TD><asp:checkbox id="MailMsg" runat="server" Text="待发布通知"></asp:checkbox></TD>
								</tr>
							</TABLE>
						</div>
					</TD>
				</tr>
			</TABLE>
			<TABLE id="Table3" style="WIDTH: 100%">
				<tr>
					<TH noWrap align="left" height="19">
						<asp:label id="saveResult" runat="server"></asp:label><INPUT id="OrderType" type="hidden" name="OrderType" runat="server">
					</TH>
				</tr>
			</TABLE>
			<INPUT type="hidden" id="inpTypeTree_XML" name="inpTypeTree_XML" runat="server">
			<INPUT type="hidden" id="inpTypeTree_TypeFields" name="inpTypeTree_TypeFields" runat="server">
		</FORM>
	</body>
</html>

