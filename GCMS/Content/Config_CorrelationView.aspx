﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Config_CorrelationView.aspx.cs" Inherits="Content_Config_CorrelationView" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_PageHeader" Src="../Gomye_Tools/Tools_PageHeader.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<WEBAPPCONTROLS:Tools_Head id="Tools_Head" runat="server"></WEBAPPCONTROLS:Tools_Head>
		<Script language="javascript">
var curUser="";
var curContent="";
var curActiveElement;

function selectUser(curContent){
	var allFile = document.body.all;
	for (var i=0;i<allFile.length;i++)
		if (allFile[i].className == "fileitem")
			allFile[i].className = "";
			
	var cuEl=eval("item"+curContent);
	cuEl.className="fileitem";
	curUser = curuser;
	top.WriteValue("curContent",curContent);
}

var el;

function showMenu() {
   
//用户操作
if (curContent==""){
	//Attribute.className="menuItemDisable";
	delUser.className="menuItemDisable";
	//setPass.className="menuItemDisable";
	//Roles.className="menuItemDisable";
	}
else
	{
	//Attribute.className="menuItem";
	delUser.className="menuItem";
	//setPass.className="menuItem";
	//Roles.className="menuItem";
	}
	   
   ContextElement=event.srcElement;
   
   var rightedge=document.body.clientWidth-event.clientX;
   var bottomedge=document.body.clientHeight-event.clientY;
   if (rightedge<menu1.offsetWidth)
     menu1.style.posLeft = document.body.scrollLeft + event.clientX - menu1.offsetWidth;
   else
     menu1.style.posLeft = document.body.scrollLeft + event.clientX;
   if (bottomedge<menu1.offsetHeight)
     menu1.style.posTop = event.clientY+document.body.scrollTop-menu1.offsetHeight;
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

function doAttribute(){
	if(Attribute.className!="menuItemDisable"){
	 	var argu = "dialogWidth:32em; dialogHeight:22em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=edituser.asp","用户属性",argu);
	doReFresh();
  	}
}

function doNewUser(){
//	if (newUser.className!="menuItemDisable"){
 //		var argu = "dialogWidth:32em; dialogHeight:22em;center:yes;status:no;help:no";
//		window.showModalDialog("WindowFrame.aspx?loadfile=Config_ChannelAdd.aspx&OrderType=Create","新增角色",argu);
//	doReFresh();
//	}
		var url="Config_ChannelAdd.aspx?OrderType=Create";
  		window.location.href = url;
}

function doSetPass(){
	if(setPass.className!="menuItemDisable"){
	 	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=presetpass.asp","设置口令",argu);
  	}
}

function doRoles(){
	if(Roles.className!="menuItemDisable"){
	 	var argu = "dialogWidth:32em; dialogHeight:24em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=userrole.asp","用户角色",argu);
  	}
}

function doDelFile(){

	if (curContent!=null && curContent!=""){
		top.WriteValue("ClipBoard_Data",curContent);
		}
	else 
	{
	alert("请单击选中需要操作的文件,再进行该命令");
	return;
	}

		var ids=curActiveElement.id.substr(12);
		question = confirm("确定要删除该扩展字段类么吗?　此操作将无法恢复！！！") 
		if (question != "1")
		{
			return false;
		}	

		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=Config_Order.aspx&OrderType=DelFieldsName&FieldsName_ID=" + ids,"删除文件",argu);
		doReFresh();
}

function doReFresh(){
	window.location.reload();
}


function selectContent(curcontent){

	if (curActiveElement!=null){
		curActiveElement.style.color="black";
		curActiveElement.style.background="white";
		}
	try{
		var cuEl=eval("xpTable_item"+curcontent);
	}catch(exception){
		curContent= "";
		curActiveElement = null;
		return;
	}
	cuEl.style.color="white";
	cuEl.style.background="midnightblue";
	curContent = curcontent;
	curActiveElement = cuEl;
}

function openContent(Roles_ID){

	if (curContent!=null && curContent!=""){
		top.WriteValue("ClipBoard_Data",curContent);
		}
	else 
	{
	alert("请单击选中需要操作的文件,再进行该命令");
	return;
	}
	//	var argu = "dialogWidth:32em; dialogHeight:22em;center:yes;status:no;help:no";
	//		window.showModalDialog("WindowFrame.aspx?loadfile=Config_ChannelAdd.aspx&OrderType=Modify&FieldsName_ID="+curContent,"修改角色",argu);	
	//	doReFresh();
		var url="Config_ChannelAdd.aspx?OrderType=Modify&FieldsName_ID="+curContent;
  		window.location.href = url;
}


		</Script>
	</HEAD>
	<body topmargin="0" leftmargin="0" oncontextmenu="showMenu(); return false; ">
		<form id="Form1" method="post" runat="server">
			<WEBAPPCONTROLS:TOOLS_PAGEHEADER id="PageHeader" runat="server" MenuStatus="3" Value="" mod="3"></WEBAPPCONTROLS:TOOLS_PAGEHEADER>
			<TABLE class="coolBar" style="WIDTH: 100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<TD class="coolButton" title="新建" onclick="doNewUser();" width="50" height="20"><IMG src="../Admin_Public/Images/Icon_File_New2.gif">
						新建</TD>
					<TD class="coolButton" title="修改" onclick="openContent();" width="50" height="20"><IMG src="../Admin_Public/Images/Icon_File_Open.gif">修改</TD>
					<TD width="5"><SPAN class="sepbtn1"></SPAN></TD>
					<TD class="coolButton" title="删除" onclick="doDelFile();" width="20" height="20"><IMG src="../Admin_Public/Images/Icon_File_Delete.gif"></TD>
					<TD class="coolButton" onclick="doReFresh();" width="20" height="20"><IMG alt="刷新" src="../Admin_Public/Images/Icon_File_ReFresh.gif"></TD>
					<TD></TD>
				</tr>
			</TABLE>
			<br/>
			<TABLE style="WIDTH: 95%" cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr>
					<TD>
						<DIV class="DivListView" id="scrollDiv" align="center">
							<SCRIPT language="javascript">
	window.onresize=fixSize;
	fixSize();

	function fixSize(){
			scrollDiv.style.height=Math.max(document.body.clientHeight-100,0);
	}
							</SCRIPT>
							<DIV class="listView" id="ContentList" align="center">
								<asp:datagrid id="xpTable" runat="server" AutoGenerateColumns="false" OnItemDataBound="ItemDataBound"
									GridLines="None" HeaderStyle-CssClass="headerTable" CssClass="item">
									<Columns>
										<asp:TemplateColumn HeaderText="ID">
											<HeaderStyle CssClass="id"></HeaderStyle>
										</asp:TemplateColumn>
										<asp:BoundColumn HeaderText="相关内容名称" HeaderStyle-CssClass="submitdate"></asp:BoundColumn>
									</Columns>
								</asp:datagrid>
							</DIV>
						</DIV>
						<!-- 上下文菜单 -->
						<div id="menu1" onclick="clickMenu()" onmouseover="toggleMenu()" onmouseout="toggleMenu()"
							class="menu">
							<div class="menuItem" id="newUser" doFunction="doNewUser();">新增相关内容类</div>
							<div class="menuItem" id="delUser" doFunction="doDelFile();">删除相关内容类</div>
							<hr>
							<div class="menuItem" id="reFresh" doFunction="doReFresh();">刷新</div>
						</div>
						<!-- 上下文菜单结束 -->
					</TD>
				</tr>
			</TABLE>
		</form>
	</body>
</html>
