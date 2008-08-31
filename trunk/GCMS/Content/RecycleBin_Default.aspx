<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecycleBin_Default.aspx.cs" Inherits="Content_RecycleBin_Default" %>
<%@ Register TagPrefix="WebAppControls" TagName="TOOLS" Src="../Gomye_Tools/TreeControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<script language="javascript">
<!--
function doReFresh(){

	window.location.reload();
}

//NEW
function doMoveContent(columnid,contentid){
	question = confirm("您确认要移动文件么！") 
	if (question != "1")
	{return false;}
		
	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("WindowFrame.aspx?loadfile=Content_ViewOrder.aspx&OrderType=preMoveContent&columnid=" + columnid.substr(7) + "&contentid=" + contentid,"",argu);
	parent.frames["Main_List"].doReFresh();
	}
	
	
function doCopyContent(columnid,contentid){
	question = confirm("您确认要拷贝文件么！") 
	if (question != "1")
	{return false;}

	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("WindowFrame.aspx?loadfile=Content_ViewOrder.aspx&OrderType=preCopyContent&columnid=" + columnid.substr(7) + "&contentid=" + contentid,"",argu);
	parent.frames["Main_List"].doReFresh();
	}

//-->
		</script>
		<script language='JavaScript' src="../admin_public/js/Nav.js"></script>
	</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" scroll="no">
		<form id="Form1" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top">
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
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table class="coolBar" style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="1" cellPadding="0"
							border="0">
							<tr>
								<TD width="5"><SPAN class="handbtn"></SPAN></TD>
								<td class="coolButton" title="刷新" onclick="doReFresh();" width="50" height="20"><IMG src="../Admin_Public/Images/Icon_File_ReFresh.gif" width="20">刷 
									新</td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top" height="100%">
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 100%"><span id="TypeTree_Label1">
								
								<div class="parent" id="m1Parent"><IMG src="../Admin_Public/Images/Tree_white.gif" align="absMiddle" border="0"><span onmouseup="OpenFolder('m1','RecycleBin_View.aspx?TypeTree_ID=1','COLUMN_1');" onmouseover="IsonMouseOver('m1');"
										onmouseout="IsonMouseOut('m1');"><IMG src="../Admin_Public/Images/Update_Record.gif" align="absMiddle" border="0" name="m1Pic">&nbsp;<A class="item" href="#nothisanchor" name="m1Folder">&nbsp;内容回收站</A></span>
								</div>
						</div>
						<script language="JavaScript" src="../admin_public/js/nav.js"></script>
						
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
