<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<TITLE>Gomye Content Management System</TITLE>
<script language="JavaScript" src="admin_public/js/coolbuttons.js"></script>
<link href="admin_public/css/listview.css" type="text/css" rel="STYLESHEET"/>
<link href="admin_public/css/Admin.css" type="text/css" rel="STYLESHEET"/>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
<meta content="C#" name="CODE_LANGUAGE"/>
<meta content="JavaScript" name="vs_defaultClientScript"/>
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<script language="JavaScript" src="Admin_Public/js/cookie.js"></script>


<SCRIPT LANGUAGE="JavaScript">
<!--
//---------------------------------------------------
top.window.moveTo(0,0);
if (document.all) {
	top.window.resizeTo(screen.availWidth,screen.availHeight);
	}
	else if (document.layers||document.getElementById) {
	if (top.window.outerHeight<screen.availHeight||top.window.outerWidth<screen.availWidth){
		top.window.outerHeight = screen.availHeight;
		top.window.outerWidth = screen.availWidth;
	}
}


function Leftswitch(){
	if (LeftbuttonFont.innerText=="3"){
		Content.Mainframe.cols = "0,*";
		LeftbuttonFont.innerText="4";
		Leftbutton.title="打开导航";
		Leftbutton.className="Siconselected";
	}
	else{
		Content.Mainframe.cols = "80,*";
		LeftbuttonFont.innerText="3";
		document.Form1.Content.Height = 0;
		Leftbutton.title="关闭导航";
		Leftbutton.className="coolButton";
	}
}


function Modify()
{
		
		var argu = "dialogWidth:32em; dialogHeight:25em;center:yes;status:no;help:no";
		window.showModalDialog("Content/WindowFrame.aspx?loadfile=User_Add.aspx&OrderType=ModifySession","修改用户",argu);		
}
function doRoles()
{
		
		var argu = "dialogWidth:32em; dialogHeight:25em;center:yes;status:no;help:no";
		window.showModalDialog("Content/WindowFrame.aspx?loadfile=Tools_RolesChange.aspx&OrderType=ModifySession","修改用户",argu);
		top.window.location.reload();	
}

function doHelp()
{
		
		alert("Copyright Gomye");
}

function closeit()
{
		question = confirm("确实要退出系统吗?") 
		if (question != "1")
		{return false;}
		var argu = "dialogWidth:0em; dialogHeight:0em;center:yes;status:no;help:no";
		window.showModalDialog("Content/WindowFrame.aspx?loadfile=Tools_Order.aspx&OrderType=ExitSystem","修改用户",argu);
		this.opener=null;
		this.close();
}


//-->
</SCRIPT>

	</HEAD>
<body oncontextmenu="return false;" leftMargin="0" topMargin="0" scroll="no">
<form id="Form1" method="post" runat="server">



<table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0">
				<tr>
					<td height="15">

						<asp:PlaceHolder id="ContentHeader" runat="server"></asp:PlaceHolder>
					</td>
				</tr>
				<tr>
					<td>
						<iframe id="Content" name="middle" style="Z-INDEX: 2;VISIBILITY: inherit;WIDTH: 100%;HEIGHT: 100%" scrolling="none" frameborder="0" src="Main.html"></iframe></td>
				</tr>
			</table>
		</form>
	</body>
</html>
