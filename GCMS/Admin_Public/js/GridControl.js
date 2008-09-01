var curContent="";
var curActiveElement;

//2002.7.10,增加数据以支持多选,保存所有选择项的contentid
var selectedContent=new Array();


function selectContent(curcontent){
	//如果按了ctrl键,又是右键,则不改变当前选中项
	if (window.event.ctrlKey==true && window.event.button==2){return;}
	
	//ctrl加左键将改变当前项的状态
	var cuEl=eval("item"+curcontent);
	var isOld=false;		//当前项是否已经选中
	
	if (window.event.ctrlKey==true && (window.event.button==1 || window.event.button==0)){
		for(var i=0;i<selectedContent.length;i++){
			if (selectedContent[i].id == "item" + curcontent){
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
			if (selectedContent[i].id == "item" + curcontent){
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
		}
	}
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

function ContentList_OnKeyup(){
	if (selectedContent.length<=0)return;
	
	if (event.keyCode==46){
		var ids="-1";
		for(var i=0;i<selectedContent.length;i++){
			ids = ids + "," + selectedContent[i].id.substr(4);
		}
		
 		var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
		window.showModalDialog("WindowFrame.aspx?loadfile=predelcontent.asp&contentid=" + ids,"",argu);
		doReFresh();
 	}
	else if(event.keyCode==13){
		openContent(curContent);
	}
	else if(event.keyCode==38){		//向上		
		if (curActiveElement==null) return;
		if (curActiveElement.previousSibling !=null)
			selectContent(curActiveElement.previousSibling.id.substr(4));
	}
	else if(event.keyCode==40){		//向下
		if (curActiveElement==null) return;
		if (curActiveElement.nextSibling !=null)
			selectContent(curActiveElement.nextSibling.id.substr(4));
	}
}