<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Config_ChannelAdd.aspx.cs" Inherits="Content_Config_ChannelAdd" %>

<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<WEBAPPCONTROLS:Tools_Head id="Tools_Head" runat="server"></WEBAPPCONTROLS:Tools_Head>
		<SCRIPT language="JavaScript">
						function closethiswindows()
						{
						top.windowclose();
						}
						
			function FormTreeDefine()
			{					
				var argu = "dialogWidth:32em; dialogHeight:40em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Config_ChannelEdit.aspx&FieldsName_ID="+Form1.txtFieldsName_ID.value,"设置栏目扩展字段",argu);
				//window.location.href = "Type_Edit.aspx?TypeTree_ID="+Form1.txtTypeTree_ID.value;			
			}
		</SCRIPT>
		<Script language="javascript">

		
var curUser="";
var curContent="";
var curActiveElement;
var selectedContent=new Array();

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
	//delUser.className="menuItemDisable";
	//setPass.className="menuItemDisable";
	//Roles.className="menuItemDisable";
	}
else
	{
	//Attribute.className="menuItem";
	//delUser.className="menuItem";
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
	if (newUser.className!="menuItemDisable"){
 		var argu = "dialogWidth:32em; dialogHeight:22em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=Role_Add.aspx&OrderType=AddRole","新增角色",argu);
	doReFresh();
	}
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
		question = confirm("确定要删除该人员信息吗?　此操作将无法恢复！！！") 
		if (question != "1")
		{
			return false;
		}	

		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=Role_Delete.aspx&Roles_ID=" + ids,"删除文件",argu);
		doReFresh();
}

function doReFresh(){
	document.location.reload();
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
		var argu = "dialogWidth:32em; dialogHeight:22em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=Config_ChannelFieldsAdd.aspx&OrderType=Updata&Fields_ID="+curContent+"&FieldsName_ID="+Form1.txtFieldsName_ID.value,"修改角色",argu);	
		doReFresh();
}


		function InitDrag(){
 		var ids="";
		if (selectedContent.length>0){
			for(var i=0;i<selectedContent.length;i++){
				if(i==0)
					ids = selectedContent[i].id.substr(17);

				else
					ids = ids + "," + selectedContent[i].id.substr(17);
				statusEl = eval("document.all.status" + selectedContent[i].id.substr(17));

			}
			}


		window.event.dataTransfer.setData("Text",curContent);
		top.WriteValue("curContentID",curContent);
		top.WriteValue("ContentSelectedIDS",ids);
		if (window.event.shiftKey==true){
			event.dataTransfer.effectAllowed="move";
			}
		else{
			event.dataTransfer.effectAllowed="copy";
			}
		}
 		
		function doMoveUp(){
			var statusEl = eval("document.all.status" + curContent);

			if (curContent!=null && curContent!=""){
				var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Config_Order.aspx&FieldsName_ID="+Form1.txtFieldsName_ID.value+"&OrderType=MoveUp&Fields_ID=" + curContent,"排列文章",argu);
				doReFresh();
 			}
		}
		
		function doMoveDown(){
			var statusEl = eval("document.all.status" + curContent);

			if (curContent!=null && curContent!=""){
 				var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Config_Order.aspx&FieldsName_ID="+Form1.txtFieldsName_ID.value+"&OrderType=MoveDown&Fields_ID=" + curContent,"排列文章",argu);
				doReFresh();
 			}
		}

		function doMoveBefore(tarID){
			var statusEl = eval("document.all.status" + curContent);
			if (curContent!=null && curContent!=""){
 				var argu = "dialogWidth:32em; dialogHeight:32em;center:yes;status:no;help:no";
				//window.showModalDialog("WindowFrame.aspx?loadfile=premoveup.asp&contentid=" + curContent,"排列文章",argu);
				window.showModalDialog("WindowFrame.aspx?loadfile=Config_Order.aspx&OrderType=MoveBefore&FieldsName_ID="+Form1.txtFieldsName_ID.value+"&Fields_ID=" + curContent + "&tarid=" + tarID,"排列文章",argu);
				doReFresh();
 			}
		}


		function FinishDrag(tarID){
			var curContentID = parseInt(top.ReadValue("curContentID"));
			if (isNaN(curContentID)) return;
			doMoveBefore(tarID)
			event.returnValue = false;
			top.WriteValue("curContentID","NaN");
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
		</Script>
	</HEAD>
	<body leftMargin="0" topMargin="0" oncontextmenu="showMenu(); return false; ">
		<form id="Form1" method="post" runat="server">
			<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<cc3:Toolsbar id="ToolsbarMain" runat="server" AltText=" 保 存" Text=" 保 存" Width="55" imageNormal="../Admin_Public/Images/Icon_File_Save.gif" OnButtonClick="ToolsbarMain_ButtonClick"></cc3:Toolsbar>
					<asp:Panel id="Panel1" runat="server">
						<TD class="coolButton" title="管理扩展字段" onclick="FormTreeDefine();" width="99" height="20"><IMG src="../Admin_Public/Images/MakeTemplate.gif">
							管理扩展字段</TD>
					</asp:Panel>
					<TD><FONT face="宋体"></FONT></TD>
				</tr>
			</table>
			<table class="table" style="WIDTH: 100%">
				<tr>
					<th noWrap align="left" height="19">
						&nbsp;<b><asp:label id="textMsg" runat="server"></asp:label></b>
					</th>
				</tr>
			</table>
			<table class="DialogTab" id="ListTab" cellSpacing="1" cellPadding="1" width="100%" border="0"
				runat="server">
				<tr>
					<td valign="top">
						<table>
							<tr>
								<td valign="top" align="right" width="92">扩展字段组名：</td>
								<td valign="top"><FONT face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									</FONT>
									<asp:textbox class="inputtext" id="FieldsName_Name" runat="server" size="30" maxlength="100"></asp:textbox></td>
							</tr>
							<tr>
								<td valign="top" align="right" width="92">数据库表名：</td>
								<td valign="top">ContentUser_<asp:textbox class="inputtext" id="FieldsBase_Name" runat="server" size="30" maxlength="100"></asp:textbox>
									（英文或数字）</td>
							</tr>
							<tr>
								<td valign="top" align="right" width="92">当前状态：</td>
								<td><select id="FieldsName_State" name="FieldsName_State" runat="server">
										<option value="1" selected>启用</option>
										<option value="0">关闭</option>
									</select>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<DIV class="DivListView" id="scrollDiv" align="center">
				<SCRIPT language="javascript">
	window.onresize=fixSize;
	fixSize();

	function fixSize(){
			scrollDiv.style.height=Math.max(document.body.clientHeight-150,0);
	}
				</SCRIPT>
				<DIV class="listView" id="ContentList" align="center">
					<asp:datagrid id="xpTable" runat="server" AutoGenerateColumns="false" OnItemDataBound="ItemDataBound"
						GridLines="None" HeaderStyle-CssClass="headerTable" CssClass="item">
						<Columns>
							<asp:TemplateColumn HeaderText="ID">
								<HeaderStyle CssClass="id"></HeaderStyle>
							</asp:TemplateColumn>
							<asp:BoundColumn HeaderText="字段名称" HeaderStyle-CssClass="submitdate"></asp:BoundColumn>
							<asp:BoundColumn HeaderText="字段说明" HeaderStyle-CssClass="title"></asp:BoundColumn>
							<asp:BoundColumn HeaderText="数据类型" HeaderStyle-CssClass="title"></asp:BoundColumn>
						</Columns>
					</asp:datagrid>
				</DIV>
				<asp:label id="sMenuContent" runat="server"></asp:label>
			</DIV>
			<P>&nbsp;&nbsp;&nbsp; <INPUT id="txtFieldsName_ID" name="txtFieldsName_ID" type="hidden" runat="server">
				<INPUT id="inpFieldsName_Name" type="hidden" runat="server">
			</P>
			<!-- 上下文菜单 -->
			<div id="menu1" onclick="clickMenu()" onmouseover="toggleMenu()" onmouseout="toggleMenu()"
				class="menu">
				<div class="menuItem" id="newUser" doFunction="doMoveUp();">上移一行(<u>N</u>)...</div>
				<div class="menuItem" id="delUser" doFunction="doMoveDown();">下移一行</div>
				<hr>
				<div class="menuItem" id="reFresh" doFunction="doReFresh();">刷新
				</div>
			</div>
			<!-- 上下文菜单结束 -->
		</form>
	</body>
</html>

