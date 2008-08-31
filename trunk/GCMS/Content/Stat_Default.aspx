<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stat_Default.aspx.cs" Inherits="Content_Stat_Default" %>
<%@ Register TagPrefix="WebAppControls" TagName="TOOLS" Src="../Gomye_Tools/Tools_GTree.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<script language='JavaScript' src="../admin_public/js/Nav.js"></script>
		<link type="text/css" rel="stylesheet" href="../admin_Public/Css/xtree.css">
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
			<script>
var Num=2;  //这里是增加选项卡的数目
var carNum=2+Num
function document.onselectstart()
{
var obj=event.srcElement 
 if(obj.tagName=="SPAN")
 {
 return false;
 }
}


function document.onmousedown()
{
 var obj=event.srcElement
 var pobj=obj.parentElement.id;
 if(obj.className=="span")
 {
  for(i=1;i<carNum;i++)
  {
   if(pobj==("btn"+i))
   {
    document.all("td"+i).style.backgroundColor="menu"
    document.all("btn"+i).style.height=20
    content(i)
   }
   else
   {
    document.all("td"+i).style.backgroundColor="white"
    document.all("btn"+i).style.height=18
   }
  }
 } 
}

function content(i)
{
//这里是菜单名
 mnuItem(1,"访问")
 mnuItem(2,"文章")
 mnuItem(3,"工作")
//End
 if(i==1)
 {
	span1.style.visibility = "visible";
	span2.style.visibility = "hidden";
	span3.style.visibility = "hidden";
 }
 if(i==2)
 {
	span1.style.visibility = "hidden";
	span2.style.visibility = "visible";
	span3.style.visibility = "hidden"; }
 if(i==3)
 {
	span1.style.visibility = "hidden";
	span2.style.visibility = "hidden";
	span3.style.visibility = "visible"; }
}
function mnuItem(i,con)
{
 document.all("MenuName"+i).innerText=con
}
			</script>
			<style>TD { FONT-SIZE: 9pt }
	.span { PADDING-RIGHT: 2px; PADDING-LEFT: 2px; PADDING-BOTTOM: 0px; WIDTH: 100%; CURSOR: default; PADDING-TOP: 2px; HEIGHT: 100% }
	</style>
	</HEAD>
	<body leftMargin="0" topMargin="0" scroll="no" onload="content(1);" onselectstart="return false"
		oncontextmenu="return false;">
		<form id="Form1" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<table width="100%" height="20" border="0" cellpadding="0" cellspacing="0" class="coolBar">
							<tr>
								<td noWrap align="left">
									&nbsp;统 计
								</td>
								<td width="10" valign="middle">
									<span class="coolButton" style="WIDTH:10px;HEIGHT:10px" onClick="parent.Mainframe.cols = '0,*';">
										<img src="../Admin_Public/Images/close.gif"></span>
								</td>
							</tr>
						</table>
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
					<td vAlign="top">
						<table width="100%" onselectstart="return false" bgcolor="menu" align="center" cellpadding="0"
							cellspacing="0">
							<tr>
								<td>
									<table cellpadding="0" cellspacing="0" onselectstart="return false">
										<TBODY>
											<tr height="20">
												<td valign="bottom">
													<table cellspacing="0" cellpadding="0" bgcolor="menu" width="65">
														<tr>
															<td width="1" height="1"></td>
															<td width="1" height="1"></td>
															<td bgcolor="white"></td>
															<td></td>
															<td></td>
														</tr>
														<tr>
															<td width="1" height="1"></td>
															<td width="1" height="1" bgcolor="white"></td>
															<td></td>
															<td bgcolor="black"></td>
															<td></td>
														</tr>
														<tr>
															<td width="1" bgcolor="white"></td>
															<td width="1" height="1"></td>
															<td id="btn1" height="20"><span class="span" id="MenuName1"></span></td>
															<td width="1" bgcolor="gray"></td>
															<td bgcolor="black" width="1"></td>
														</tr>
														<tr>
															<td bgcolor="white"></td>
															<td colspan="4" height="1" bgcolor="menu" id="td1"></td>
														</tr>
													</table>
												</td>
												<script>
for(i=2;i<carNum;i++)
{
 tdBody="<td valign=bottom>"
 tdBody+="<table cellspacing=0 cellpadding=0 bgcolor=menu>"
 tdBody+="<tr>"
 tdBody+="<td width=1 height=1><\/td><td width=1 height=1><\/td>"
 tdBody+="<td bgcolor=white><\/td><td></td><td></td>"
 tdBody+="<\/tr>"
 tdBody+="<tr><td width=1 height=1><\/td><td width=1 height=1 bgcolor=white><\/td>"
 tdBody+="<td></td><td bgcolor=black><\/td><td></td>"
 tdBody+="<\/tr>"
 tdBody+="<tr><td width=1 bgcolor=white><\/td><td width=1 height=1><\/td>"
 tdBody+="<td id=btn"+i+" height=18 width=65><span class=span id=MenuName"+i+"><\/span></td><td width=1 bgcolor=gray><\/td><td bgcolor=black width=1><\/td>"
 tdBody+="<\/tr>"
 tdBody+="<tr><td bgcolor=white><\/td><td colspan=4 height=1 bgcolor=white id=td"+i+"><\/td>"
 tdBody+="<\/tr>"
 tdBody+="<\/table>"
 tdBody+="<\/td>";
document.write(tdBody)
}

												</script>
								</td>
								<td valign="bottom">
									<table cellspacing="0" cellpadding="0" bgcolor="menu" width="30">
										<tr>
											<td height="1" bgcolor="white"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<div style="BORDER-RIGHT:1px outset; PADDING-RIGHT:10px; PADDING-LEFT:10px; PADDING-BOTTOM:10px; BORDER-LEFT:white 1px solid; WIDTH:100%; PADDING-TOP:10px; BORDER-BOTTOM:1px outset"
							id="scrollDiv">
							<!--这里是内容-->
							<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: buttonshadow 1px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: buttonshadow 1px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; BACKGROUND-COLOR: buttonhighlight"
								id="scrollDiv2">
								<div id="span1" style="VISIBILITY: hidden">
									<WEBAPPCONTROLS:TOOLS id="TypeTree" runat="server" Value="" MenuStatus="3"></WEBAPPCONTROLS:TOOLS>
								</div>
								<div id="span2"></div>
								<div id="span3">
<table width="100%" border="0" cellspacing="2" cellpadding="2">
  <tr>
    <td valign="top"><a href="Stat_ViewMember.aspx" target=Main_Type>所有管理员</a></td>
  </tr>
</table>
								</div>
							</div>
							<!--这里是内容-->
						</div>
					</td>
				</tr>
			</table>
			
			<SCRIPT language="javascript">
	window.onresize=fixSize;
	fixSize();

	function fixSize(){
		scrollDiv.style.height=Math.max(document.body.clientHeight-75,0);
		scrollDiv2.style.height=Math.max(document.body.clientHeight-100,0);
	}
	content(1);

			</SCRIPT>
		</form>
	</body>
</HTML>
