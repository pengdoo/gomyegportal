<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content_Default.aspx.cs" Inherits="Content_Content_Default" %>

<%@ Register Src="../Gomye_Tools/Tools_TreeMenu.ascx" TagName="Tools_TreeMenu" TagPrefix="uc1" %>

<%@ Register TagPrefix="WebAppControls" TagName="TOOLS" Src="../Gomye_Tools/Tools_GTree.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<script type="text/javascript" src="../js/jquery/jquery-1.2.6.pack.js"></script>
        <script type="text/javascript" src="../js/jquery.treeview/jquery.treeview.min.js"></script>
        <script type="text/javascript" src="../js/jquery.contextmenu/jquery.contextmenu.r2.packed.js"></script>
        <link rel="stylesheet" href="../js/jquery.treeview/jquery.treeview.css" />
        <script type="text/javascript">
	    $(document).ready(function(){
	         $("#treemenu").treeview();//载入菜单
	   
		});
        </script>
	    

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
								<td class="coolButton" title="刷新" onclick="doReFresh();" width="50" height="20"><IMG src="../Admin_Public/Images/Icon_File_ReFresh.gif" width="20">刷 新</td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top" height="100%">
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 100%">
                            <uc1:Tools_TreeMenu ID="MainTree" runat="server" />
						</div>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--
function doReFresh(){

	window.location.reload();
}

//NEW
function doMoveContent(columnid,contentid){
	//question = confirm("您确认要移动文件么！"+contentid+" To "+columnid) 
	//if (question != "1")
	//{return false;}
		
	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("WindowFrame.aspx?loadfile=Content_SysMsg.aspx&OrderType=preMoveContent&columnid=" + columnid + "&Content_List=" + contentid,"",argu);
	parent.frames["Main_List"].doReFresh();
	}
	
	
function doCopyContent(columnid,contentid){
	//question = confirm("您确认要拷贝文件么！"+contentid+" To "+columnid) 
	//if (question != "1")
	//{return false;}

	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("WindowFrame.aspx?loadfile=Content_SysMsg.aspx&OrderType=preCopyContent&columnid=" + columnid + "&Content_List=" + contentid,"",argu);
	parent.frames["Main_List"].doReFresh();
	}

//-->
function InitDrag(){
	window.event.dataTransfer.setData("Text",CurrentNode);
	top.WriteValue("curChannelID",CurrentNode);
	if (window.event.shiftKey==true){
		event.dataTransfer.effectAllowed="move";
		}
	else{
		event.dataTransfer.effectAllowed="copy";
		}
	}
/*function FinishDrag(tarID){
	var curContentID = parseInt(top.ReadValue("curContentID"));
	if (isNaN(curContentID)) return;
	if (window.event.shiftKey==true){doMoveContent(tarID,curContentID);}
	else {doCopyContent(tarID,curContentID);}
	event.returnValue = false;
	top.WriteValue("curContentID","NaN");

//	var cuNode = cuObject;
// 	if (cuObject !=null){
//		cuNode.Tag="N";
//		cuNode.doExpand();
//	}
	}*/
	
function FinishDrag(tarColumn){
	var curContentID = top.ReadValue("ContentSelectedIDS");
	if (curContentID=="") return;
	if (window.event.shiftKey==true){doMoveContent(tarColumn,curContentID);}
	else {doCopyContent(tarColumn,curContentID);}
	event.returnValue = false;
	top.WriteValue("ContentSelectedIDS","");
	}
	
function dragEnter(){
	event.returnValue = false;
	}
function dragOver(){
	event.returnValue = false;
	}
function dragLeave(){
	event.returnValue = false;
	}
</script>
	</body>
</HTML>
