var bV=parseInt(navigator.appVersion);
NS4=(document.layers) ? true : false;
IE4=((document.all)&&(bV>=4))?true:false;
ver4 = (NS4 || IE4) ? true : false;

var CurrentNode;
var oldNode;

isExpanded = false;

function getIndex($1) {
	ind = null;
	for (i=0; i<document.layers.length; i++) {
		whichEl = document.layers[i];
		if (whichEl.id == $1) {
			ind = i;
			break;
		}
	}
	return ind;
}

function FolderInit(){
	tempColl = document.all.tags("DIV");
	for (i=0; i<tempColl.length; i++) {
		if (tempColl(i).className == "child") tempColl(i).style.display = "none";
	}
}

function arrange() {
	nextY = document.layers[firstInd].pageY + document.layers[firstInd].document.height;
	for (i=firstInd+1; i<document.layers.length; i++) {
		whichEl = document.layers[i];
		if (whichEl.visibility != "hide") {
			whichEl.pageY = nextY;
			nextY += whichEl.document.height;
		}
	}
}

function FolderExpand(meName,Path,Last) {
	if (!ver4) return;
	ExpandIE(meName,Path,Last);
}

function ExpandIE($1,path,$2) {
	var part1,part2;
	Expanda = eval($1 + "a");
	Expanda.blur();
	ExpandChild = eval($1 + "Child");
        if ($2 != "top") { 
		ExpandTree = eval("Form1."+$1 + "Tree");
		ExpandPic = eval("Form1."+$1 + "Pic");  //文件夹
		ExpandFolder = eval($1 + "Folder");
	}
	if (ExpandChild.style.display == "none") {
		ExpandChild.style.display = "block";
               if ($2 != "top") { 
			part1="../Admin_Public/Images/Tree_";
			if ($2 == "last") {part1 = part1 + "L";}
			else {part1 = part1 + "T";}
			part2 = "minus.gif";
	/*      ExpandTree.src = part1 + part2; */
			ExpandTree.src = "../Admin_Public/Images/Rminus.gif";
			ExpandPic.src = "../Admin_Public/Images/fo.gif";	//文件夹
			OpenFolder($1,path);

		}
		else { mTree.src = "../Admin_Public/Images/topopen.gif"; }
	}
	else {
		ExpandChild.style.display = "none";
               if ($2 != "top") { 
			part1="../Admin_Public/Images/Tree_";
			if ($2 == "last") {part1 = part1 + "L";}
			else {part1 = part1 + "T";}
			part2 = "plus.gif";
		/*  ExpandTree.src = part1 + part2;*/
			ExpandTree.src = "../Admin_Public/Images/Rplus.gif";
			ExpandPic.src = "../Admin_Public/Images/fc.gif";	//文件夹
			OpenFolder($1,path);
		}
		else { mTree.src = "../Admin_Public/Images/top.gif"; }
	}
}

function OpenFolder($1,path,key,Name){
	//var allFolder = document.body.all;
	//for (var i=0;i<allFolder.length;i++)
	//	if (allFolder[i].className == "itemselected")
	//		allFolder[i].className = "item";

	if (oldNode!=null)oldNode.className ="item";

	ExpandFolder = eval($1 + "Folder");
	ExpandFolder.className = "itemselected";
	oldNode = ExpandFolder;
	CurrentNode = key;
	//window.location = path;
	columnid = path;
	columnname = Name;
	cuC.innerHTML = Name;
}

function showAll() {
	for (i=firstInd; i<document.layers.length; i++) {
		whichEl = document.layers[i];
		whichEl.visibility = "show";
	}
}

function IsonMouseOut($1) {

	ExpandFolder = eval($1 + "Folder");
	if (ExpandFolder.className != "itemselected")
	{
	ExpandFolder.className = "item";
	}
	
}

function IsonMouseOver($1) {

	ExpandFolder = eval($1 + "Folder");
	if (ExpandFolder.className != "itemselected")
	{
	ExpandFolder.className = "itemselectOver";
	}
	
}

//包含本脚本文件的那个页面必须准备两个函数叫doMoveContent和doCopyContent
function FinishDrag(tarColumn){
	var curContentID = parseInt(top.ReadValue("curContentID"));
	if (isNaN(curContentID)) return;
	if (window.event.shiftKey==true){doMoveContent(tarColumn,curContentID);}
	else {doCopyContent(tarColumn,curContentID);}
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

with (document) {
	write("<STYLE TYPE='text/css'>");
	if (NS4) {
		write(".parent { color: black; font-size:9pt; line-height:0pt; color:black; text-decoration:none; margin-top: 0px; margin-bottom: 0px; position:absolute; visibility:hidden }");
		write(".child { text-decoration:none; font-size:9pt; line-height:15pt; position:absolute }");
	        write(".item { color: black; text-decoration:none }");
	        write(".itemselected { color: white;background:HIGHLIGHT;text-decoration:none;}");
	        write(".highlight { color: HIGHLIGHT; text-decoration:none }");
	}
	else {
		write(".parent { font: 12px/13px; Times; text-decoration: none; color: black;align:left; }");
		write(".child { font:12px/13px Times; display:none;align:left; }");
		write(".item { color: black; text-decoration:none; cursor: hand }");
		write(".itemselectOver { color: black;background:Menu;text-decoration:none; cursor: hand; border: 1px solid #999999;}");
		write(".itemselected { color: white;background:HIGHLIGHT;text-decoration:none; cursor: hand ; border: 1px solid HIGHLIGHT;}");
		write(".highlight { color: HIGHLIGHT; text-decoration:none ;border:1px solid white;}");
		write(".icon { margin-right: 5 }")
	}
	write("</STYLE>");
}

onload = FolderInit;