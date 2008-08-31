<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tools_Left.aspx.cs" Inherits="Tools_Left" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<title>Tools_Left</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<LINK href="admin_public/css/Admin.css" type="text/css" rel="STYLESHEET"/>
		<script language="JavaScript" src="admin_Public/js/coolbuttons.js"></script>
		<script language="javascript">
function changeview(view,url){
for (i=0; i<document.body.all.length; i++) {
if (document.body.all[i].className == "iconselected") {
document.body.all[i].className="icon";
}
}
view.className="iconselected";
if (view.id=="folder")
 top.WriteValue("curPath","/");
else if (view.id=="templates")
 top.WriteValue("curPath","/Templates/");

parent.TypeTree.location=url;
}
		</script>
	</HEAD>
	<body oncontextmenu="self.event.returnValue=false" onselectstart="event.returnValue=false"
		leftMargin="0" topMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="100%" height="20">
						<table class="coolBar" height="20" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td noWrap align="left">&nbsp;工 具
								</td>
								<td valign="middle" width="10"><span class="coolButton" style="WIDTH: 10px; HEIGHT: 10px" onclick="parent.Mainframe.cols = '0,250,*';"><IMG id="IMG1" src="admin_Public/Images/close.gif"/></span>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" align="center" bgColor="buttonshadow">
						<script language="JavaScript"> 
<!-- 
function showitem(id,name,Ename,Pic) 
{ 
	//return ("<span><a href='"+id+"' target=_blank>"+name+"</a></span><br/>") 
	return ("<span class='icon' id='"+Ename+"' onclick=\"changeview("+Ename+",'"+id+"');\" align='center'><IMG src='"+Pic+"' border='0'><br/><br/>"+name+"</span>") 
	} 
	function switchoutlookBar(number) 
	{ 
	var i = outlookbar.opentitle; 
	outlookbar.opentitle=number; 
	var id1,id2,id1b,id2b 
	if (number!=i && outlooksmoothstat==0){ 
	if (number!=-1) 
	{ 
	if (i==-1){ 
	id2="blankdiv"; 
	id2b="blankdiv"; 
	} 
	else{ 
	id2="outlookdiv"+i; 
	id2b="outlookdivin"+i; 
	document.all("outlooktitle"+i).style.border="1px none navy"; 
	document.all("outlooktitle"+i).style.background=outlookbar.maincolor; 
	document.all("outlooktitle"+i).style.color="ButtonText"; 
	document.all("outlooktitle"+i).style.textalign="center"; 
	} 
	id1="outlookdiv"+number 
	id1b="outlookdivin"+number 
	document.all("outlooktitle"+number).style.border="1px none white"; 
	//document.all("outlooktitle"+number).style.background=outlookbar.maincolor; //title 
	document.all("outlooktitle"+number).style.background="ButtonHighlight"; //title 
	document.all("outlooktitle"+number).style.color="ButtonText";
	smoothout(id1,id2,id1b,id2b,0); 
	} 
	else 
	{ 
	document.all("blankdiv").style.display=""; 
	document.all("blankdiv").sryle.height="100%"; 
	document.all("outlookdiv"+i).style.display="none"; 
	document.all("outlookdiv"+i).style.height="0%"; 
	document.all("outlooktitle"+i).style.border="1px none white"; 
	document.all("outlooktitle"+i).style.background=outlookbar.maincolor; 
	document.all("outlooktitle"+i).style.color="ButtonText"; 
	document.all("outlooktitle"+i).style.textalign="center"; 
	} 
	} 
	} 
	function smoothout(id1,id2,id1b,id2b,stat) 
	{ 
	if(stat==0){ 
	tempinnertext1=document.all(id1b).innerHTML; 
	tempinnertext2=document.all(id2b).innerHTML; 
	document.all(id1b).innerHTML=""; 
	document.all(id2b).innerHTML=""; 
	outlooksmoothstat=1; 
	document.all(id1b).style.overflow="hidden"; 
	document.all(id2b).style.overflow="hidden"; 
	document.all(id1).style.height="0%"; 
	document.all(id1).style.display=""; 
	setTimeout("smoothout('"+id1+"','"+id2+"','"+id1b+"','"+id2b+"',"+outlookbar.inc+")",outlookbar.timedalay); 
	} 
	else 
	{ 
	stat+=outlookbar.inc; 
	if (stat>100) 
	stat=100; 
	document.all(id1).style.height=stat+"%"; 
	document.all(id2).style.height=(100-stat)+"%"; 
	if (stat<100) 
	setTimeout("smoothout('"+id1+"','"+id2+"','"+id1b+"','"+id2b+"',"+stat+")",outlookbar.timedalay); 
	else 
	{ 
	document.all(id1b).innerHTML=tempinnertext1; 
	document.all(id2b).innerHTML=tempinnertext2; 
	outlooksmoothstat=0; 
	document.all(id1b).style.overflow="auto"; 
	document.all(id2).style.display="none"; 
	} 
	} 
} 

function getOutLine() 
{ 
	outline="<table "+outlookbar.otherclass+">"; 
	for (i=0;i<(outlookbar.titlelist.length);i++) 
	{ 
	outline+="<tr><td name=outlooktitle"+i+" id=outlooktitle"+i+" "; 
	if (i!=outlookbar.opentitle) 
	outline+=" nowrap align=center style='cursor:hand;background-color:"+outlookbar.maincolor+";color:ButtonText;height:20;border:1 none red' "; 
	else 


	outline+=" nowrap align=center style='cursor:hand;background-color:ButtonHighlight;color:ButtonText;height:20;border:1 none white' "; 
	outline+=outlookbar.titlelist[i].otherclass 
	outline+=" onclick='switchoutlookBar("+i+")'><span class=smallFont>"; 
	outline+=outlookbar.titlelist[i].title+"</span></td></tr>"; 
	//outline+="<tr><td class='icon' id='channel' onclick=\"changeview(channel,'Main_Type.html');\" align='center'><IMG src='../admin_Public/Images/Icon_New_Navigation.gif' border='0'><br/><br/>导 航</td></tr>";

	outline+="<tr><td name=outlookdiv"+i+" valign=top align=center id=outlookdiv"+i+" style='width:100%" 
	if (i!=outlookbar.opentitle) 
	outline+=";display:none;height:0%;"; 
	else 
	outline+=";display:;height:100%;"; 
	outline+="'><div name=outlookdivin"+i+" id=outlookdivin"+i+" style='overflow:auto;width:100%;height:100%'><br/>"; 
	for (j=0;j<outlookbar.itemlist[i].length;j++) 
	outline+=showitem(outlookbar.itemlist[i][j].key,outlookbar.itemlist[i][j].title,outlookbar.itemlist[i][j].Ename,outlookbar.itemlist[i][j].Pic); 
	outline+="</div></td></tr>" 
} 


outline+="</table>" 
return outline 
} 
function show() 
{ 
var outline; 
outline="<div id=outLookBarDiv name=outLookBarDiv style='width=100%;height:100%'>" 
outline+=outlookbar.getOutLine(); 
outline+="</div>" 
document.write(outline); 
} 
function theitem(intitle,instate,inkey,Ename,Pic) 
{ 
this.state=instate; 
this.otherclass=" nowrap "; 
this.key=inkey; 
this.title=intitle; 
this.Ename=Ename; 
this.Pic=Pic; 
} 

function addtitle(intitle) 
{ 
	outlookbar.itemlist[outlookbar.titlelist.length]=new Array(); 
	outlookbar.titlelist[outlookbar.titlelist.length]=new theitem(intitle,1,0); 
	return(outlookbar.titlelist.length-1); 
} 

function additem(intitle,parentid,inkey,Ename,Pic) 
{ 
	if (parentid>=0 && parentid<=outlookbar.titlelist.length) 
	{ 
	outlookbar.itemlist[parentid][outlookbar.itemlist[parentid].length]=new theitem(intitle,2,inkey,Ename,Pic); 
	outlookbar.itemlist[parentid][outlookbar.itemlist[parentid].length-1].otherclass=" nowrap align=left style='height:5'"; 
	return(outlookbar.itemlist[parentid].length-1); 
	} 
	else 
	additem=-1; 
} 

function outlook() 
{ 
	this.titlelist=new Array(); 
	this.itemlist=new Array(); 
	this.divstyle="style='height:100%;width:100%;overflow:auto' align=center"; 
	this.otherclass="border=1 cellspacing='0' cellpadding='0' style='height:100%;width:100%'valign=middle align=center "; 
	this.addtitle=addtitle; 
	this.additem=additem; 
	this.starttitle=-1; 
	this.show=show; 
	this.getOutLine=getOutLine; 
	this.opentitle=this.starttitle; 
	this.reflesh=outreflesh; 
	this.timedelay=50; 
	this.inc=10; 
	this.maincolor = "ButtonFace" 
} 

function outreflesh() 
{ 
	document.all("outLookBarDiv").innerHTML=outlookbar.getOutLine(); 
} 

function locatefold(foldname) 
{ 
	if (foldname=="") 
	foldname = outlookbar.titlelist[0].title 
	for (var i=0;i<outlookbar.titlelist.length;i++) 
	{ 
	if(foldname==outlookbar.titlelist[i].title) 
	{ 
	outlookbar.starttitle=i; 
	outlookbar.opentitle=i; 
	} 
	} 
} 


//--> 
						</script>
						<asp:label id="LeftTools" runat="server"></asp:label>
						<table id="mnuList" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="0" cellPadding="0" align="center"
							border="0">
							<tr>
								<td id="outLookBarShow" style="HEIGHT: 100%" valign="top" align="center" bgColor="appworkspace"
									name="outLookBarShow">
									<script language="JavaScript"> 
<!-- 
locatefold("") 
outlookbar.show() 
//--> 
									</script>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>