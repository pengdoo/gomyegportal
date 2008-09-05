<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_TypeView.aspx.cs" Inherits="Content_Type_TypeView" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_PageHeader" Src="../Gomye_Tools/Tools_PageHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
        <link href="../Admin_Public/css/Admin.css" type="text/css"/>
		<WebAppControls:Tools_Head id="Tools_Head" runat="server"></WebAppControls:Tools_Head>
		<script language="javascript">

			function FormDefine()
			{					
				var argu = "dialogWidth:32em; dialogHeight:40em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Type_TypeEdit.aspx&Type=Content&TypeTree_ID="+Form1.txtTypeTree_ID.value,"设置内容扩展字段",argu);
				//window.location.href = "Type_Edit.aspx?TypeTree_ID="+Form1.txtTypeTree_ID.value;			
			}
			function FormTreeDefine()
			{					
				var argu = "dialogWidth:32em; dialogHeight:40em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Type_TypeAddFields.aspx&Type=Type&TypeTree_ID="+Form1.txtTypeTree_ID.value,"设置栏目扩展字段",argu);
				//window.location.href = "Type_Edit.aspx?TypeTree_ID="+Form1.txtTypeTree_ID.value;			
			}
			
			
			function FormCorrelation()
			{					
				var argu = "dialogWidth:32em; dialogHeight:40em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Type_Edit.aspx&Type=Type&TypeTree_ID="+Form1.txtTypeTree_ID.value,"相关管理",argu);
				//window.location.href = "Type_Edit.aspx?TypeTree_ID="+Form1.txtTypeTree_ID.value;			
			}
			
			function DeleteType()
			{
				if(confirm("确定要删除此类别吗？此操作是无法恢复！！"))
				{					
					parent.location.href = "Type_Delete.aspx?TypeTree_ID="+Form1.txtTypeTree_ID.value;
				}
			}
			function RefreshWindow()
			{				
				window.location.href = "Type_TypeView.aspx?TypeTree_ID="+Form1.txtTypeTree_ID.value;
			}
			function FormPopedom()
			{					
				var argu = "dialogWidth:32em; dialogHeight:25em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Type_RoleAdd.aspx&OrderType=Update&TypeTree_ID="+Form1.txtTypeTree_ID.value,"设置目录属性",argu);
			}
			
	
			function FormPopedom_GForums()
			{					
				window.location.href = "Type_AddMemberRoseList.aspx?OrderType=Update&TypeTree_ID="+Form1.txtTypeTree_ID.value;
			}
			
			
			function FormAddParenthesis()
			{					
				window.location.href = "Type_AddParenthesis.aspx?OrderType=Create&TypeTree_ID="+Form1.txtTypeTree_ID.value;
			}
			function Feedback()
			{					
				var argu = "dialogWidth:28em; dialogHeight:25em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=Feedback&TypeTree_ID="+Form1.txtTypeTree_ID.value,"设置目录属性",argu);
			}
			
			
			function AllPush()
			{					
				var argu = "dialogWidth:28em; dialogHeight:10em;center:yes;status:no;help:no";
				window.showModalDialog("ProgressWinodowFrame.aspx?loadfile=Type_Order.aspx&OrderType=AllPush&TypeTree_ID="+Form1.txtTypeTree_ID.value,"全部生成",argu);
			}
			
			function PushLinks()
			{					
				var argu = "dialogWidth:28em; dialogHeight:10em;center:yes;status:no;help:no";
				window.showModalDialog("ProgressWinodowFrame.aspx?loadfile=Type_Order.aspx&OrderType=PushLinks&TypeTree_ID="+Form1.txtTypeTree_ID.value,"生成附带发布列表",argu);
			}
			function PushSingleLink()
			{
			var argu = "dialogWidth:28em; dialogHeight:10em;center:yes;status:no;help:no";
				window.showModalDialog("ProgressWinodowFrame.aspx?loadfile=Type_Order.aspx&OrderType=PushOneLinks&Link_ID="+curContent+"&TypeTree_ID="+Form1.txtTypeTree_ID.value,"生成附带发布列表",argu);
			
			}
						

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
	top.WriteValue("curUser",curContent);
}

var el;

function showMenu() {
   
//用户操作
if (curContent==""){
	delUser.className="menuItemDisable";
	setPass.className="menuItemDisable";
	}
else
	{
	delUser.className="menuItem";
	setPass.className="menuItem";
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
		RefreshWindow();
	}
}



function doSetPass(){
	if(setPass.className!="menuItemDisable"){
		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=presetpass.asp","设置口令",argu);
	}
}

function doRoles(){

	if (curContent!=null && curContent!=""){
		top.WriteValue("ClipBoard_Data",curContent);
		}
	else 
	{
	alert("请单击选中需要操作的文件,再进行该命令");
	return;
	}
	
	//var argu = "dialogWidth:28em; dialogHeight:15em;center:yes;status:no;help:no";
	//window.showModalDialog("WindowFrame.aspx?loadfile=Type_AddParenthesis.aspx&OrderType=Update&Link_ID="+curContent+"&TypeTree_ID="+Form1.txtTypeTree_ID.value,"设置目录属性",argu);
	//RefreshWindow();
	window.location.href = "Type_AddParenthesis.aspx?OrderType=Update&Link_ID="+curContent+"&TypeTree_ID="+Form1.txtTypeTree_ID.value;


}

function openContent(){

	if (curContent!=null && curContent!=""){
		top.WriteValue("ClipBoard_Data",curContent);
		}
	else 
	{
	alert("请单击选中需要操作的文件,再进行该命令");
	return;
	}

	//var argu = "dialogWidth:28em; dialogHeight:15em;center:yes;status:no;help:no";
	//window.showModalDialog("WindowFrame.aspx?loadfile=Type_AddParenthesis.aspx&OrderType=Update&Link_ID="+curContent+"&TypeTree_ID="+Form1.txtTypeTree_ID.value,"设置目录属性",argu);
	//RefreshWindow();
	window.location.href = "Type_AddParenthesis.aspx?OrderType=Update&Link_ID="+curContent+"&TypeTree_ID="+Form1.txtTypeTree_ID.value;
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

		question = confirm("确定要删除该附带发布信息吗?　此操作将无法恢复！！！") 
		if (question != "1")
		{
			return false;
		}	

		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=Type_AddParenthesis.aspx&OrderType=Delete&Link_ID=" + curContent +"&TypeTree_ID="+Form1.txtTypeTree_ID.value,"删除文件",argu);
		RefreshWindow();
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
	
	//document.ContentVilue.SelectID.value=curcontent; //写入ID

}

		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" oncontextmenu="showMenu(); return false;">
		<form id="Form1" method="post" runat="server">
			<WebAppControls:Tools_PageHeader id="PageHeader" runat="server" MenuStatus="3" Value="" mod="3"></WebAppControls:Tools_PageHeader>
			<table class="coolBar" style="WIDTH:100%;HEIGHT:20px" cellpadding='0' cellspacing='1' border='0'>
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<!--					<td class="coolButton" onClick="parent.frames['Main_List'].location.href = 'Type_Add.aspx?OrderType=root';" title="新建根目录" width="80" height="10">
						<img src="../Admin_Public/Images/Icon_File_FileCode.gif"> 新建根目录</td>-->
					<td class="coolButton" onClick="window.location.href = 'Type_Add.aspx?OrderType=son&amp;TypeTree_ID='+Form1.txtTypeTree_ID.value;"
						title="新建子目录" width="80" height="10">
						<img src="../Admin_Public/Images/Icon_File_FileCode.gif"> 新建子目录</td>
					<td class="coolButton" onClick="window.location.href = 'Type_Add.aspx?OrderType=Update&amp;TypeTree_ID='+Form1.txtTypeTree_ID.value"
						title="修改目录属性" width="70" height="10">
						<img src="../Admin_Public/Images/icon-page.gif">修改目录</td>
					<TD width="5"><SPAN class="sepbtn1"></SPAN></TD>
					<td class="coolButton" onClick="RefreshWindow();" title="刷新" width="50" height="10">
						<img src="../Admin_Public/Images/Icon_File_ReFresh.gif"> 刷新</td>
					<td class="coolButton" onClick="DeleteType();" title="删除" width="45" height="10">
						<img src="../Admin_Public/Images/Icon_File_Delete.gif"> 删除</td>
					<TD width="5"><SPAN class="sepbtn1"></SPAN></TD>
					<td class="coolButton" onClick="FormTreeDefine();" title="捆绑子内容目录" width="110" height="20">
						<img src="../Admin_Public/Images/Hyperlink.gif">&nbsp;捆绑子内容目录</td>
					<td class="coolButton" onClick="FormDefine();" title="内容扩展字段" width="99" height="20">
						<img src="../Admin_Public/Images/MakeTemplate.gif"> 内容扩展字段</td>
					<td class="coolButton" onClick="FormPopedom();" title="设置权限" width="73" height="20">
						<img src="../Admin_Public/Images/Icon_New_Master.gif"> 设置权限</td>
					<td></td>
				</tr>
			</table>
			<br/>
			<table class="DialogTab" style="WIDTH:98%" cellSpacing="0" cellPadding="0" align="center"
				border="0">
				<tr>
					<td>
						<table class="TopTitle" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="1"><IMG src="../Admin_Public/Images/Icon_File_New.gif"></td>
								<td><font color="#ffffff">当前目录 -
										<asp:label id="LTypeTree_CNameA" runat="server"></asp:label>
										<asp:label id="LTypeTree_Issuance" runat="server"></asp:label>
										<asp:label id="LTypeTree_Type" runat="server"></asp:label></font></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="coolBar" style="WIDTH:100%;HEIGHT:20px" cellpadding='0' cellspacing='1' border='0'
							id="TABLE1">
							<tr>
								<TD width="5"><SPAN class="handbtn"></SPAN></TD>
								<asp:Panel id="Panelbbs" runat="server" Visible="False">
									<TD class="coolButton" title="设置版主" onclick="FormPopedom_GForums();" width="73" height="20"><IMG src="../Admin_Public/Images/Icon_New_Master.gif">
										设置版主</TD>
								</asp:Panel>
								<asp:Panel id="Panel4" runat="server" Visible="False">
									<TD class="coolButton" title="生成表单" onclick="Feedback();" width="73" height="20"><IMG src="../Admin_Public/Images/MakeNestedTemplate.gif">
										生成表单</TD>
								</asp:Panel>
								<td></td>
							</tr>
						</table>
						<asp:datalist id="DataList1" runat="server" Width="100%" RepeatColumns="0" ShowFooter="False"
							ShowHeader="False">
							<ItemTemplate>
								<table cellSpacing="1" cellPadding="2" width="100%" border="0">
									<tr bgColor="buttonhighlight">
										<td valign="top" width="80">I D 编号：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_ID")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">中文名称：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_CName")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">英文名称：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_EName")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">命名规则：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_URL")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">页面模板：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_Template")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">图片路径：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_PictureURL")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">列表文件：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_ListURL")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">列表模板：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_ListTemplate")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">分类图片：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_Images")%></td>
									</tr>
									<TR bgColor="buttonhighlight">
										<td valign="top">分类说明：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_Explain")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">列表数目：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"List_amount")%></td>
									</tr>
									<tr bgColor="buttonhighlight">
										<td valign="top">编辑语言：</td>
										<td valign="top"><%#DataBinder.Eval(Container.DataItem,"TypeTree_Language")%></td>
									</tr>
								</table>
							</ItemTemplate>
						</asp:datalist>
					</td>
				</tr>
			</table>
			<asp:Panel id="Panel1" runat="server" Visible="False"  >
				<br/>
				<TABLE class="DialogTab" cellSpacing="0" cellPadding="0" width="98%" align="center" Height="200" border="0">
					<tr>
						<TD>
							<TABLE class="TopTitle" cellSpacing="0" cellPadding="0" width="100%"  border="0">
								<tr>
									<TD width="1"><IMG src="../Admin_Public/Images/Icon_File_New.gif"></TD>
									<TD><FONT color="#ffffff">附带发布</FONT></TD>
								</tr>
							</TABLE>
							<TABLE class="coolBar" id="TABLE2" style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="0" cellPadding="0"
								border="0">
								<tr>
									<TD width="5"><SPAN class="handbtn"></SPAN></TD>
									<TD class="coolButton" title="附带发布" onclick="FormAddParenthesis();" width="73" height="20"><IMG src="../Admin_Public/Images/MasterDetail.gif">
										附带发布</TD>
									<TD class="coolButton" title="整栏目发布" onclick="AllPush();" width="88" height="20"><IMG src="../Admin_Public/Images/Icon_File_Color.gif">
										整栏目发布</TD>
									<TD></TD>
								</tr>
							</TABLE>
							<DIV class="DivListView" id="scrollDiv" align="center" >

<SCRIPT language="javascript">
	window.onresize=fixSize;
	fixSize();

	function fixSize(){
		scrollDiv.style.height=100;
	}
</SCRIPT>


								<DIV class="listView" id="listView" align="center">
									<asp:datagrid id="xpTable" runat="server" AutoGenerateColumns="false" OnItemDataBound="ItemDataBound"
										GridLines="None" HeaderStyle-CssClass="headerTable" CssClass="item" BackColor="White"  >
										<Columns>
											<asp:TemplateColumn HeaderText="ID">
												<HeaderStyle CssClass="id"></HeaderStyle>
											</asp:TemplateColumn>
											<asp:BoundColumn HeaderText="名称" HeaderStyle-CssClass="submitdate"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="模板" HeaderStyle-CssClass="submitdate"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="文件" HeaderStyle-CssClass="submitdate"></asp:BoundColumn>
										</Columns>
									</asp:datagrid></DIV>
							</DIV>
						</TD>
					</tr>
				</TABLE>
			</asp:Panel>
			<INPUT id="txtTypeTree_ID" type="hidden" runat="server" name="txtTypeTree_ID"> 
			<!-- 上下文菜单 -->
			<div id="menu1" onclick="clickMenu()" onmouseover="toggleMenu()" onmouseout="toggleMenu()"
				class="menu">
				<div class="menuItem" id="newUser" doFunction="FormAddParenthesis();">新建附带发布(<u>N</u>)...</div>
				<div class="menuItem" id="setPass" doFunction="doRoles();">设置附带发布</div>
				<div class="menuItem" id="set" doFunction="PushSingleLink();">生成本附带发布</div>
				<div class="menuItem" id="Div1" doFunction="PushLinks();">生成全部附带发布</div>
				<hr>
				<div class="menuItem" id="delUser" doFunction="doDelFile();">删除附带发布</div>
				<div class="menuItem" id="reFresh" doFunction="RefreshWindow();">刷新</div>
			</div>
			<!-- 上下文菜单结束 -->
		</form>
	</body>
</html>
