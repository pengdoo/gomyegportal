<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Config_Main.aspx.cs" Inherits="Content_Config_Main" %>

<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<WEBAPPCONTROLS:Tools_Head id="Tools_Head" runat="server"></WEBAPPCONTROLS:Tools_Head>
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
			<script language="javascript">
			<!--
			function doReFresh(){
				window.location.reload();
			}
			//NEW
			//-->
			</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr HEIGHT="15">
					<td height="15" valign="top">
						<table width="100%" height="20" border="0" cellpadding="0" cellspacing="0" class="coolBar">
							<tr>
								<td noWrap align="left">
									&nbsp;栏 目
								</td>
								<td width="10" valign="middle">
									<span class="coolButton" style="WIDTH:10px;HEIGHT:10px" onClick="parent.Mainframe.cols = '0,*';">
										<img src="../Admin_Public/Images/close.gif"></span>
								</td>
							</tr>
						</table>
						<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
							<tr>
								<TD width="5"><SPAN class="handbtn"></SPAN></TD>
								<td class="coolButton" id="setMaster" onClick="doReFresh();" width="60" height="20">
									<img src="../Admin_Public/Images/Icon_File_ReFresh.gif" alt="刷 新"> 刷 新</td>
								<td><FONT face="宋体"></FONT></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr HEIGHT="15">
					<td height="100%" valign="top">
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 100%"><span id="TypeTree_Label1">
								<div><span>&nbsp;<IMG src="../admin_public/images/fo.gif" align="absMiddle" border="0">&nbsp;系统设置</span></div>
								<div class="parent" id="m1Parent"><IMG src="../Admin_Public/Images/Tree_white.gif" align="absMiddle" border="0"><span onmouseup="OpenFolder('m1','Config_ChannelView.aspx','COLUMN_1');" onmouseover="IsonMouseOver('m1');"
										onmouseout="IsonMouseOut('m1');"><IMG src="../admin_public/images/closedfolder.gif" align="absMiddle" border="0" name="m1Pic">&nbsp;<A class="item" href="#nothisanchor" name="m1Folder">&nbsp;扩展字段设置</A></span>
								</div>
								<div class="parent" id="m2Parent">
									<IMG src="../Admin_Public/Images/Tree_white.gif" align="absMiddle" border="0"><SPAN onmouseup="OpenFolder('m2','Config_CorrelationView.aspx','COLUMN_2');" onmouseover="IsonMouseOver('m2');"
										onmouseout="IsonMouseOut('m2');"><IMG src="../Admin_Public/Images/closedfolder.gif" align="absMiddle" border="0" name="m2Pic">&nbsp;<A class="item" href="#nothisanchor" name="m2Folder">&nbsp;相关内容设置</SPAN>
								</div>
						</div>
						<script language="JavaScript" src="../admin_public/js/nav.js"></script>
						
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

