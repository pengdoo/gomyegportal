<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tools_SelectTreeID.aspx.cs" Inherits="Content_Tools_SelectTreeID" %>

<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">

<LINK href="../Admin_Public/Css/Admin.css" rel=stylesheet>
<link type="text/css" rel="stylesheet" href="../admin_Public/Css/xtree.css">
<script language="javascript">
var CurrentNode=null;

//包含本脚本文件的那个页面必须准备两个函数叫doMoveContent和doCopyContent
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
function closewindow(){
var fanhui="";
for(i=0;i<document.all('CheckID').length;i++)
{
if(document.all('CheckID')[i].checked)
{
fanhui = fanhui + document.all('CheckID')[i].value + " ";
}

}
  //window.returnValue = CName.value;
  window.returnValue = fanhui;
  window.close();
}
</script>

<script language="Javascript" src="../admin_Public/js/SxtreeID.js"></script>
<title></title>
</head>

<body topmargin="0" leftmargin="0" onselectstart="return false;" scroll="no">

<nobr>


<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 90%">

<form name="Form1" method="post" id="Form1">
<asp:Literal ID="lit_mainScript" runat="server"></asp:Literal>
<script type="text/javascript" language="javascript">
if (document.getElementById) {
	var tree = new WebFXTree(' ');
	tree.setBehavior('explorer');
	var aNode=tree.add(new WebFXTreeItem("选择目录","N","-1"));
	aNode.add(new WebFXTreeItem("Loading","Y"));

	document.write(tree);
}


function OpenFolder(path){
CurrentNode = path;
CName.value = path;
	//if (path!="/")
	//  parent.frames["main"].location = "contentlist.asp?columnid=" + path;
	//else
	//  parent.frames["main"].location = "tasklist.asp";
}
</script>
 </form>
</div>
</nobr>

<!-- 上下文菜单 -->
<script>
function showMenu() {
	if (CurrentNode=="" || CurrentNode == null){
		preview.className="menuItemDisable";
	}
	else{
		preview.className="menuItem";
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

</script>
<!-- 上下文菜单结束 -->



<INPUT TYPE="hidden" ID=CName >
<input type="button" value=" 确 认 " onClick="closewindow();" class="button" style="width:80px;" name="button">
<input type="button" value=" 取 消 " onClick="window.close();" class="button" style="width:80px;" name="button2">
</body>

</html>

