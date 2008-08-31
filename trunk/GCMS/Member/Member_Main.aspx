﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Member_Main.aspx.cs" Inherits="Member_Member_Main" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<LINK href="../admin_public/css/listview.css" type="text/css" rel="STYLESHEET">
			<LINK href="../admin_public/css/admin.css" type="text/css" rel="STYLESHEET">
	</HEAD>
	<body oncontextmenu="return false" leftMargin="0" topMargin="0" scroll="no">
		<script language="javascript">

var curContent="";
var curActiveElement;

//2002.7.10,增加数据以支持多选,保存所有选择项的contentid
var selectedContent=new Array();

function selectContent(curcontent){
	//如果按了ctrl键,又是右键,则不改变当前选中项
	if (window.event.ctrlKey==true && window.event.button==2){return;}
	
	//ctrl加左键将改变当前项的状态
	var cuEl=eval("DateGridList_item"+curcontent);
	var isOld=false;		//当前项是否已经选中
	if (window.event.ctrlKey==true && (window.event.button==1 || window.event.button==0)){
		for(var i=0;i<selectedContent.length;i++){
			if (selectedContent[i].id == "DateGridList_item" + curcontent){
				isOld = true;
				for (j=i;j<selectedContent.length-1;j++){
					selectedContent[j]=selectedContent[j+1];
				}
				selectedContent.length = selectedContent.length-1;
				cuEl.style.color="black";
				cuEl.style.background="white";
				curContent="";
				curActiveElement = null;
			}
		}
		if (!isOld){
			cuEl.style.color="white";
			cuEl.style.background="midnightblue";
			curContent = curcontent;
			curActiveElement = cuEl;
			selectedContent[selectedContent.length]=curActiveElement;
		}
	}

	//没按ctrl键,如果是左键,总是先清除选项,然后选中当前项,如果是右键,再根据是否已经选中,如果已经选中,不变,否则,清楚选项,然后选中当前项
	else {
		for(var i=0;i<selectedContent.length;i++){
			if (selectedContent[i].id == "DateGridList_item" + curcontent){
				isOld = true;
			}
		}
	
		if(event.button==0 || event.button==1 || (event.button==2 && isOld==false)){
			for (var i=selectedContent.length-1;i>=0;i--){
				selectedContent[i].style.color="black";
				selectedContent[i].style.background="white";
				selectedContent[i]=null;
			}
			selectedContent.length=0;
		
			cuEl.style.color="white";
			cuEl.style.background="midnightblue";
			curContent = curcontent;
			curActiveElement = cuEl;
			selectedContent[selectedContent.length]=curActiveElement;
			parent.frames['Main_List'].location ='Member_View.aspx?TypeTree_ID='+curcontent  //路径

		}
	}
}
function openContent(contentid){
	var statusEl = eval("document.all.status" + curContent);

	doOpen(contentid);

}
function doOpen(contentid){
	  	var width  = Math.floor( screen.width  * .7 );
  		var height = Math.floor( screen.height * .8 );
  		var leftm  = Math.floor( screen.width  * .1)+30;
 		var topm   = Math.floor( screen.height * .05)+30;
 		var argu = "toolbar=0,location=0,maximize=1,directories=0,status=0,menubar=0,scrollbars=1, resizable=1,left=" + leftm+ ",top=" + topm + ", width="+width+", height="+height;
		var url="Roles_Update.aspx?flag=edit&TypeTree_ID="+contentid;
  		//window.open(url,"",argu);
  		//parent.frames['Main_List'].location = url;  //路径
  		this.location = url;
  		
}


function Import(){
	var argu = "dialogWidth:24em; dialogHeight:12em;center:yes;status:no;help:no";
	var cid=window.showModalDialog("importfile.asp?columnid=",null,argu);
	if (cid!=null)
		doOpen(cid);
}
function uploadTRS(){
	var argu = "dialogWidth:24em; dialogHeight:12em;center:yes;status:no;help:no";
	var cid=window.showModalDialog("uploadTrs.asp?columnid=",null,argu);
	doReFresh();
}
function newContent(){
		var width  = Math.floor( screen.width  * .7 );
		var height = Math.floor( screen.height * .7 );
		var leftm  = Math.floor( screen.width  * .2)+30;
		var topm   = Math.floor( screen.height * .2)+30;
		var argu = "toolbar=0,location=0,maximize=1,directories=0,status=1,menubar=0,scrollbars=1, resizable=1,left=" + leftm+ ",top=" + topm + ", width="+width+", height="+height;
		var url="Content_Add.aspx?flag=new&TypeTree_ID="+Form1.TypeTree_ID.value;
		window.open(url,"",argu);
}

function InitDrag(){
	window.event.dataTransfer.setData("Text",curContent);
	top.WriteValue("curContentID",curContent);
	if (window.event.shiftKey==true){
		event.dataTransfer.effectAllowed="move";
		}
	else{
		event.dataTransfer.effectAllowed="copy";
		}
	}

function AdvancedSearch(){
	var width  = 400;
  	var height = 200;
  	var leftm  = Math.floor( (screen.width -400)/2)+30;
 	var topm   = Math.floor( (screen.height -200)/2)+30;
 	var argu = "toolbar=0,location=0,maximize=0,directories=0,status=0,menubar=0,scrollbars=0, resizable=0,left=" + leftm+ ",top=" + topm + ", width="+width+", height="+height;
	window.open("presearch4.asp?columnid=","高级搜索",argu);
}
function ExportContent(){
	window.frames["download"].location="export.asp?columnid=";
}

function doHistory(){
	var width  = 500;
  	var height = 400;
  	var leftm  = Math.floor( (screen.width -500)/2)+30;
 	var topm   = Math.floor( (screen.height -400)/2)+30;
 	var argu = "toolbar=0,location=0,maximize=0,directories=0,status=0,menubar=0,scrollbars=1, resizable=0,left=" + leftm+ ",top=" + topm + ", width="+width+", height="+height;
	window.open("history.asp?cid="+curContent,"稿件处理历史",argu);
}

function doLock(){
	if (Lock.className!="menuItemDisable"){
		try{
	var statusEl = eval("document.all.status" + curContent);

			var f = window.frames["postFrame"].document.postForm;
				f.action="Content_ViewOrder.aspx?OrderType=lock&Content_ID="+curContent;
			f.submit();
			statusEl.src="../Admin_Public/images/ic_lockuser.gif";
		}catch(exception){}
	}
}


function doUnLock(){
	if (unLock.className!="menuItemDisable"){
		try{
	var statusEl = eval("document.all.status" + curContent);

			var f = window.frames["postFrame"].document.postForm;
			f.action="Content_ViewOrder.aspx?OrderType=unlock&Content_ID="+curContent;
			f.submit();
			statusEl.src="../Admin_Public/images/white.gif";
		}catch(exception){}
	}
}


//Copy
function doCopyFile(){
	if (curContent!=null && curContent!=""){
		top.WriteValue("ClipBoard_Data",curContent);
		}
	else 
	alert("请单击选中需要操作的文件,再进行该命令");
}

//Copy Over
function doPasteFile(){
	if (pasteFile.className!="menuItemDisable"){
		try{
		var src = top.ReadValue("ClipBoard_Data");
		if (src==""){return;}
		//top.WriteValue("ClipBoard_ExtData","");
		//window.setTimeout("doReFresh();",2000);
			var f = window.frames["postFrame"].document.postForm;
			f.action="Content_ViewOrder.aspx?OrderType=Paste&Content_ID="+src+"&TypeTree_ID="+Form1.TypeTree_ID.value;
			f.submit();
			doReFresh();
		}catch(exception){}
	}
}


		</script>
		<script>
var el;

function showMenu() {
	//修改,以便支持多选,2002-7-10

   if(selectedContent.length<=0){	//没有选中,只显示新文件和刷新
   		openfile.style.display="none";
   		delfile.style.display = "none";
		preview.style.display = "none";
		moveup.style.display="none";
		movedown.style.display="none";
		menuNouse.style.display="none";
		approval.style.display="none";
		relative.style.display="none";

   }
   else if(selectedContent.length==1){	//选中一个,所有显示
   		openfile.style.display="block";
   		delfile.style.display="block";
		preview.style.display="block";
		moveup.style.display="block";
		movedown.style.display="block";
		menuNouse.style.display="block";
		approval.style.display="block";
		relative.style.display="block";

   }
   else{	//多选,显示新文件,审批,删除,刷新
   		openfile.style.display="none";
   		delfile.style.display="block";
		preview.style.display="none";
		moveup.style.display="none";
		movedown.style.display="none";
		menuNouse.style.display="block";
		approval.style.display="block";
		relative.style.display="none";

   }
	
		if (curContent==""){
			copyFile.className = "menuItemDisable";
			}
		else{
			copyFile.className = "menuItem";
			}

		if (top.ReadValue("ClipBoard_Data")==null || top.ReadValue("ClipBoard_Data")==""){
			pasteFile.className = "menuItemDisable";
			}
		else{
			pasteFile.className = "menuItem";
			}

   ContextElement=event.srcElement;
   
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

function doOpenFile(){
	if (curContent!=null && curContent!=""){
 	openContent(curContent);
 	}
	else 
	alert("请单击选中需要操作的文件,再进行该命令");
}

function doDelFile(){
	if (curContent!=null && curContent!=""){

		question = confirm("确实要进行删除操作吗?该操作将无法恢复！！！") 
		if (question != "1")
		{return false;}

		var statusEl = eval("document.all.status" + curContent);

		//支持多选提交,2002-7-10
		if (selectedContent.length>0){
			var ids="-1";
			for(var i=0;i<selectedContent.length;i++){
				ids = ids + "," + selectedContent[i].id.substr(17);
				statusEl = eval("document.all.status" + selectedContent[i].id.substr(17));

			}
			
			var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
			window.showModalDialog("WindowFrame.aspx?loadfile=Content_ViewOrder.aspx&OrderType=Delete&Content_List=" + ids,"",argu);
			doReFresh();
		}
	}
	else 
	alert("请单击选中需要操作的文件,再进行该命令");
}
function doMoveUp(){
	var statusEl = eval("document.all.status" + curContent);

	if (curContent!=null && curContent!=""){
		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=Content_ViewOrder.aspx&TypeTree_ID="+Form1.TypeTree_ID.value+"&OrderType=MoveUp&Content_ID=" + curContent,"排列文章",argu);
		doReFresh();
 	}
}
function doMoveDown(){
	var statusEl = eval("document.all.status" + curContent);

	if (curContent!=null && curContent!=""){
 		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=Content_ViewOrder.aspx&TypeTree_ID="+Form1.TypeTree_ID.value+"&OrderType=MoveDown&Content_ID=" + curContent,"排列文章",argu);
		doReFresh();
 	}
}

function doRelative(){
	if (curContent!=null && curContent!=""){
		var statusEl = eval("document.all.status" + curContent);

		if (curContent!=null && curContent!=""){
			var argu = "dialogWidth:32em; dialogHeight:34em;center:yes;status:no;help:no";
			window.showModalDialog("WindowFrame.aspx?loadfile=Content_Relative.aspx&Content_ID=" + curContent + "&relid=0","相关文章整理",argu);
		}
	 }
	else 
	alert("请单击选中需要操作的文件,再进行该命令");
}

function doRecommend(){
	if (curContent!=null && curContent!=""){
 		var argu = "dialogWidth:32em; dialogHeight:34em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=recommend/recommend.asp&contentid=" + curContent,"推荐文章",argu);
 	}
}
function doCollection(){
	if (curContent!=null && curContent!=""){
 		var argu = "dialogWidth:32em; dialogHeight:34em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=collection/recommend.asp&contentid=" + curContent,"推荐文章到集合",argu);
 	}
}

function doApproval(){
	var statusEl = eval("document.all.status" + curContent);

	//支持多选提交,2002-7-10
	if (selectedContent.length>0){
		var ids="-1";
		for(var i=0;i<selectedContent.length;i++){
			ids = ids + "," + selectedContent[i].id.substr(17);
			statusEl = eval("document.all.status" + selectedContent[i].id.substr(17));
		}
		
 		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=Content_ViewOrder.aspx&OrderType=Approval&Content_List=" + ids,"稿件签发",argu);
		doReFresh();
 	}
}

function doVersion(){
	if (curContent!=null && curContent!=""){
	  	var width  = 500;
  		var height = 200;
  		var leftm  = Math.floor((screen.width-500)/2);
 		var topm   = Math.floor((screen.height-200)/2)+30;
 		var argu = "toolbar=0,location=0,maximize=1,directories=0,status=0,menubar=0,scrollbars=0, resizable=1,left=" + leftm+ ",top=" + topm + ", width="+width+", height="+height;

		window.open("WindowFrame.aspx?loadfile=version.asp&contentid=" + curContent,"版本",argu);
 	}
}
function doComment(){
	window.open("comment.asp?Content_ID=" + curContent);
}
function doUpdateDate(){
	if (curContent!=null && curContent!=""){
 		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=preupdatedate.asp&contentid=" + curContent,"",argu);
		doReFresh();
 	}
}
function doPreview(){
	if (curContent!=null && curContent!=""){
		window.open("Content_Preview.aspx?Content_ID=" + curContent);
 	}
	else 
	alert("请单击选中需要操作的文件,再进行该命令");
}
function doReFresh(){
	window.location.href="Member_Main.aspx";

}

function unselect(){
	//为多选做了改动,2002-7-10
	
	if (window.event.srcElement.id!="scrollDiv"){return;}
	if (window.event.ctrlKey==true){return;}
	curContent="";
	
	for (var i=selectedContent.length-1;i>=0;i--){
		selectedContent[i].style.color="black";
		selectedContent[i].style.background="white";
		selectedContent[i]=null;
	}
	selectedContent.length=0;
}

function selectall(){
	if (event.srcElement.tagName.toLowerCase()!="body"){return false;}
	if(event.ctrlKey && event.button==0){
	  selectedContent.length = 0;
	  var allitem = document.all.tags("div");
	  for(var i=0;i<allitem.length;i++)
	    if (allitem[i].className=="item"){
		  allitem[i].style.color="white";
		  allitem[i].style.background="midnightblue";
	      selectedContent[selectedContent.length]=allitem[i];
	      }
	}
	return false;
}

function listcontent(el){
	frmList.status.value = el.options(el.selectedIndex).value;
	frmList.submit();
}

function search2(el){
	frmSearch2.key.value = el.value;
	frmSearch2.submit();
}
function search3(){
	frmSearch3.submitdate.value=year.value + "-" + month.value + "-" + day.value;
	frmSearch3.submit();
}
function ContentList_OnKeyup(){
	if (selectedContent.length<=0)return;
	
	if (event.keyCode==46){
	/*	var ids="-1";
		for(var i=0;i<selectedContent.length;i++){
			ids = ids + "," + selectedContent[i].id.substr(17);
		}
		
 		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=predelcontent.asp&contentid=" + ids,"",argu);
		doReFresh();
	*/
	doDelFile();
 	}
	else if(event.keyCode==13){
		openContent(curContent);
	}
	else if(event.keyCode==38){		//向上		
		if (curActiveElement==null) return;
		if (curActiveElement.previousSibling !=null)
			selectContent(curActiveElement.previousSibling.id.substr(17));
	}
	else if(event.keyCode==40){		//向下
		if (curActiveElement==null) return;
		if (curActiveElement.nextSibling !=null)
			selectContent(curActiveElement.nextSibling.id.substr(17));
	}
}
function doMoveBefore(tarID){
	var statusEl = eval("document.all.status" + curContent);

	if (curContent!=null && curContent!=""){
 		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		//window.showModalDialog("WindowFrame.aspx?loadfile=premoveup.asp&contentid=" + curContent,"排列文章",argu);
		window.showModalDialog("WindowFrame.aspx?loadfile=Content_ViewOrder.aspx&OrderType=MoveBefore&TypeTree_ID="+Form1.TypeTree_ID.value+"&Content_ID=" + curContent + "&tarid=" + tarID,"排列文章",argu);
		doReFresh();
 	}
}

//NEW
function doMoveContent(columnid,contentid){
	question = confirm("您确认要让用户"+contentid+" 移动到组 "+columnid+" 吗？") 
	if (question != "1")
	{return false;}
		
	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("../Content/WindowFrame.aspx?loadfile=../Member/Member_ViewOrder.aspx&OrderType=preMoveContent&columnid=" + columnid + "&contentid=" + contentid,"",argu);
	parent.frames["Main_List"].doReFresh();
	}
	
	
function doCopyContent(columnid,contentid){
	question = confirm("您确认要让用户"+contentid+" 复制到组 "+columnid+" 吗？") 
	if (question != "1")
	{return false;}

	var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	window.showModalDialog("../Content/WindowFrame.aspx?loadfile=../Member/Member_ViewOrder.aspx&OrderType=preCopyContent&columnid=" + columnid + "&contentid=" + contentid,"",argu);
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
function FinishDrag(tarID){
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
		<form id="Form1" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr height="15">
					<td vAlign="top" height="15">
						<table class="coolBar" height="20" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td noWrap align="left">&nbsp;上传目录
								</td>
								<td vAlign="middle" width="10"><span class="coolButton" style="WIDTH: 10px; HEIGHT: 10px" onclick="parent.Mainframe.cols = '0,*';"><IMG src="../Admin_Public/Images/close.gif"></span>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="top">
									<table class="coolBar" style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="1" cellPadding="0"
										border="0">
										<tr>
											<TD width="5"><SPAN class="handbtn"></SPAN></TD>
											<td><asp:textbox id="TextName" runat="server" CssClass="inputtext"></asp:textbox>&nbsp;</td>
											<TD width="5"><SPAN class="sepbtn1"></SPAN></TD>
											<td></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="15">
					<td vAlign="top" height="100%">
						<DIV class="DivListView" id="scrollDiv" onclick="unselect();" align="center">
							<SCRIPT language="javascript">
	window.onresize=fixSize;
	fixSize();

	function fixSize(){
		scrollDiv.style.height=Math.max(document.body.clientHeight-55,0);
	}
							</SCRIPT>
							<DIV class="listView" id="ContentList" align="center">
								<asp:datagrid id="DateGridList" runat="server" CssClass="item" OnItemDataBound="ItemDataBound"
									HeaderStyle-CssClass="headerTable" GridLines="None" AutoGenerateColumns="false">
									<Columns>
										<asp:TemplateColumn HeaderText="ID">
											<HeaderStyle CssClass="id"></HeaderStyle>
										</asp:TemplateColumn>
										<asp:BoundColumn HeaderText="用户角色 (双击修改)" HeaderStyle-CssClass="submitdate"></asp:BoundColumn>
									</Columns>
								</asp:datagrid></DIV>
						</DIV>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
