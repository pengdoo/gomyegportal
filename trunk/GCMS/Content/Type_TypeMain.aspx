<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_TypeMain.aspx.cs" Inherits="Content_Type_TypeMain" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tree" Src="../Gomye_Tools/Tools_GTree.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_PageHeader" Src="../Gomye_Tools/Tools_PageHeader.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <HEAD>
		<title>CMS</title>
		<WEBAPPCONTROLS:Tools_Head id="Tools_Head" runat="server"></WEBAPPCONTROLS:Tools_Head>
		 <link href="../Admin_Public/css/Admin.css" rel="stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="../admin_Public/Css/xtree.css">
		<script language="javascript">
		<!--
			function AddRootType()
			{	
				var argu = "dialogWidth:32em; dialogHeight:28em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Type_add.aspx&type=root","新建根目录",argu);				
				top.location.reload();		
			}

			function doReFresh(){
				window.location.reload();
			}

			//NEW
			function doMainCatalog(){
					try{
							var argu = "dialogWidth:32em; dialogHeight:28em;center:yes;status:no;help:no";
							window.showModalDialog("WindowFrame.aspx?loadfile=Type_Add.aspx&OrderType=root","新建根目录",argu);
							parent.location.reload();
					}catch(exception){}
			}
			//NEW OVER

			//-->
		</script>
</HEAD>
	<body topmargin="0" leftmargin="0" oncontextmenu="showMenu();return false;" scroll="no">
		<form id="Form1" runat="server">
		<WEBAPPCONTROLS:TOOLS_PAGEHEADER id="PageHeader" runat="server" Value="栏  目" Mod="2"></WEBAPPCONTROLS:TOOLS_PAGEHEADER>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td valign="top">
						<table class="coolBar" style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="1" cellPadding="0"
							border="0">
							<tr>
								<TD style="WIDTH: 5px"><SPAN class="handbtn"></SPAN></TD>
								<%if (int.Parse(Session["Roles"].ToString()) == 0){%>
								<td class="coolButton" title="新建" style="WIDTH: 80px; HEIGHT: 20px" onclick="parent.frames['Main_List'].location = 'Type_Add.aspx?OrderType=root'"><IMG src="../Admin_Public/Images/Icon_File_FileCode.gif">
									新建根目录</td>
								<TD style="WIDTH: 5px"><SPAN class="sepbtn1"></SPAN></TD>
								<%}%>
								<td class="coolButton" title="刷新" style="WIDTH: 54px; HEIGHT: 20px" onclick="doReFresh();"><IMG src="../Admin_Public/Images/Icon_File_ReFresh.gif">
									刷 新</td>
								<td><FONT face="宋体"></FONT></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" height="100%">
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 100%">
						<WEBAPPCONTROLS:Tree id="TypeTree" runat="server"></WEBAPPCONTROLS:Tree>
						</div>
					</td>
				</tr>
			</table>
<!-- 上下文菜单 -->
<div id=menu1 onclick="clickMenu()" onmouseover="toggleMenu()" onmouseout="toggleMenu()" class="menu">
<div class="menuItem" id="newRole" doFunction="doMainCatalog();">新建频道</div>
<!--<div class="menuItem" id="navigate" doFunction="doNavigate();">生成导航</div>-->
<hr id="nouse">
<div class="menuItem" id="reFresh" doFunction="doReFresh();">刷新</div>
<hr id="nouse2">
<div class="menuItem" id="moveup" doFunction="doMoveUp();">上移</div>
<div class="menuItem" id="movedown" doFunction="doMoveDown();">下移</div>
<!--<hr id="nouse3">
<div class="menuItem" id="regenall" doFunction="doReGenAll();">重新生成网站</div>-->
</div>
<script>

function showMenu() {
	if (CurrentNode=="" || CurrentNode == null){
		moveup.className="menuItemDisable";
		movedown.className="menuItemDisable";
	}
	else{
		moveup.className="menuItem";
		movedown.className="menuItem";
	}
   var rightedge=document.body.clientWidth-event.clientX;
   var bottomedge=document.body.clientHeight-event.clientY;

   if (rightedge<menu1.offsetWidth){
     if (event.clientX - menu1.offsetWidth>0)
       menu1.style.posLeft = document.body.scrollLeft + event.clientX - menu1.offsetWidth;
     else
       menu1.style.posLeft = document.body.scrollLeft;
     }
   else
     menu1.style.posLeft = document.body.scrollLeft + event.clientX;
   if (bottomedge<menu1.offsetHeight){
     if (event.clientY-menu1.offsetHeight>0)
         menu1.style.posTop = event.clientY+document.body.scrollTop-menu1.offsetHeight;
     else
         menu1.style.posTop = document.body.scrollTop;
     }
   else
     menu1.style.posTop = event.clientY+document.body.scrollTop;
   
   menu1.className = "menushow";
   menu1.setCapture();
}
function toggleMenu() {   
   el=event.srcElement;
   if (el.className=="menuItem") {
      el.className="highlightItem";
   } else if (el.className=="highlightItem") {
      el.className="menuItem";
   }
}
function clickMenu() {
   menu1.releaseCapture();
   menu1.className = "menu";
   //menu1.style.display="none";
   el=event.srcElement;
   if (el.doFunction != null) {
     eval(el.doFunction);
   }
}

function doMoveUp(){
//	var cId = parseInt(CurrentNode.substr(7));
	var cId = parseInt(CurrentNode);
 	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=doMoveUp&TypeTree_ID=" + cId,"排列目录",argu);
	window.location.reload();
}
function doMoveDown(){
//	var cId = parseInt(CurrentNode.substr(7));
	var cId = parseInt(CurrentNode);
 	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=doMoveDown&TypeTree_ID=" + cId,"排列目录",argu);
	window.location.reload();
}

function doNavigate(){
 	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("WindowFrame.aspx?loadfile=generatenavigate.asp","生成导航",argu);
}


function doReGenAll(){
		try{
	 	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=pregenerateall.asp","重新生成网站",argu);
		}catch(exception){}
}

function doMoveChannel(columnid,parent){
	question = confirm("您确认要移动栏目么！"+columnid+" To "+parent) 
	if (question != "1")
	{return false;}
	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=preMoveChannel&TypeTree_ID=" + columnid + "&parent=" + parent,"移动频道",argu);
	doReFresh();
	}

function doCopyChannel(columnid,parent){
	question = confirm("您确认要拷贝栏目么！"+columnid+" To "+parent) 
	if (question != "1")
	{return false;}
	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	//window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=preCopyChannel&TypeTree_ID=" + columnid + "&parent=" + parent,"复制频道结构",argu);
	window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=preCopyChannel&TypeTree_ID=" + columnid + "&parent=" + parent,"复制频道结构",argu);
	doReFresh();
	}


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
	
	
function FinishDrag(tarID){
	var curChannelID = parseInt(top.ReadValue("curChannelID"));
	if (isNaN(curChannelID)) return;
	if (window.event.shiftKey==true)
	{doMoveChannel(curChannelID,tarID);}
	else {doCopyChannel(curChannelID,tarID);}
	event.returnValue = false;
	top.WriteValue("curChannelID","NaN");

//	var cuNode = cuObject;
// 	if (cuObject !=null){
//		cuNode.Tag="N";
//		cuNode.doExpand();
//	}
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
<!-- 上下文菜单结束 -->
		</form>
	</body>
</html>
