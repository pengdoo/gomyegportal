/*
*/
// 当前模式
var sCurrMode = null;
var bEditMode = null;
// 连接对象
var oLinkField = null;
var sBaseUrl = document.location.protocol + '//' + document.location.host ;

// 浏览器版本检测
var BrowserInfo = new Object() ;
BrowserInfo.MajorVer = navigator.appVersion.match(/MSIE (.)/)[1] ;
BrowserInfo.MinorVer = navigator.appVersion.match(/MSIE .\.(.)/)[1] ;
BrowserInfo.IsIE55OrMore = BrowserInfo.MajorVer >= 6 || ( BrowserInfo.MajorVer >= 5 && BrowserInfo.MinorVer >= 5 ) ;

var yToolbars = new Array();  // 工具栏数组

// 当文档完全调入时，进行初始化
var bInitialized = false;

function document.onreadystatechange(){
	if (document.readyState!="complete") return;
	if (bInitialized) return;
	bInitialized = true;

	var i, s, curr;

	// 初始每个工具栏
	for (i=0; i<document.body.all.length;i++){
		curr=document.body.all[i];
		if (curr.className == "yToolbar"){
			InitTB(curr);
			yToolbars[yToolbars.length] = curr;
		}
	}

	oLinkField = parent.document.getElementsByName(sLinkFieldName)[0];
	if (!config.License){
		try{
			SeaskyEditor_License.innerHTML = "";
		}
		catch(e){
		}
	}

	// IE5.5以下版本只能使用纯文本模式
	if (!BrowserInfo.IsIE55OrMore){
		config.InitMode = "TEXT";
	}
	
	if (ContentFlag.value=="0") { 

		var content;
		try 
		{
			var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") 
			xmlDoc.async="false" 
			xmlDoc.loadXML(oLinkField.value)
			var root = xmlDoc.documentElement;
			var page;
			for (var i=0;i<root.childNodes.length;i++) {
			if (i>0) {addTab();}
			page = root.childNodes.item(i);
			content = page.text;
			_SetContent(_GetPages(),content);
			}
		//ContentEdit.value = oLinkField.value;
		//ContentLoad.value = oLinkField.value;

		ContentEdit.value = content;
		ContentLoad.value = content;
		ModeEdit.value = config.InitMode;
		ContentFlag.value = "1";
		setMode(ModeEdit.value);
		setLinkedField() ;
		SeaskyEditor.focus();

		SetActive(1);
		}
		catch(exception)
		{
			ContentFlag.value = "1";
			ContentEdit.value ="";
			ModeEdit.value = config.InitMode;
			setMode(ModeEdit.value);
			setLinkedField() ;
			SeaskyEditor.focus();
		}

	}
	//载入样式表文件
//	SeaskyEditor.document.createStyleSheet(CssFile);
}

// 初始化一个工具栏上的按钮
function InitBtn(btn) {
	btn.onmouseover = BtnMouseOver;
	btn.onmouseout = BtnMouseOut;
	btn.onmousedown = BtnMouseDown;
	btn.onmouseup = BtnMouseUp;
	btn.ondragstart = YCancelEvent;
	btn.onselectstart = YCancelEvent;
	btn.onselect = YCancelEvent;
	btn.YUSERONCLICK = btn.onclick;
	btn.onclick = YCancelEvent;
	btn.YINITIALIZED = true;
	return true;
}

//Initialize a toolbar. 
function InitTB(y) {
	// Set initial size of toolbar to that of the handle
	y.TBWidth = 0;
		
	// Populate the toolbar with its contents
	if (! PopulateTB(y)) return false;
	
	// Set the toolbar width and put in the handle
	y.style.posWidth = y.TBWidth;
	
	return true;
}


// Hander that simply cancels an event
function YCancelEvent() {
	event.returnValue=false;
	event.cancelBubble=true;
	return false;
}

// Toolbar button onmouseover handler
function BtnMouseOver() {
	if (event.srcElement.tagName != "IMG") return false;
	var image = event.srcElement;
	var element = image.parentElement;
	
	// Change button look based on current state of image.
	if (image.className == "Ico") element.className = "BtnMouseOverUp";
	else if (image.className == "IcoDown") element.className = "BtnMouseOverDown";

	event.cancelBubble = true;
}

// Toolbar button onmouseout handler
function BtnMouseOut() {
	if (event.srcElement.tagName != "IMG") {
		event.cancelBubble = true;
		return false;
	}

	var image = event.srcElement;
	var element = image.parentElement;
	yRaisedElement = null;
	
	element.className = "Btn";
	image.className = "Ico";

	event.cancelBubble = true;
}

// Toolbar button onmousedown handler
function BtnMouseDown() {
	if (event.srcElement.tagName != "IMG") {
		event.cancelBubble = true;
		event.returnValue=false;
		return false;
	}

	var image = event.srcElement;
	var element = image.parentElement;

	element.className = "BtnMouseOverDown";
	image.className = "IcoDown";

	event.cancelBubble = true;
	event.returnValue=false;
	return false;
}

// Toolbar button onmouseup handler
function BtnMouseUp() {
	if (event.srcElement.tagName != "IMG") {
		event.cancelBubble = true;
		return false;
	}

	var image = event.srcElement;
	var element = image.parentElement;

	if (element.YUSERONCLICK) eval(element.YUSERONCLICK + "anonymous()");

	element.className = "BtnMouseOverUp";
	image.className = "Ico";

	event.cancelBubble = true;
	return false;
}

// Populate a toolbar with the elements within it
function PopulateTB(y) {
	var i, elements, element;

	// Iterate through all the top-level elements in the toolbar
	elements = y.children;
	for (i=0; i<elements.length; i++) {
		element = elements[i];
		if (element.tagName == "SCRIPT" || element.tagName == "!") continue;
		
		switch (element.className) {
		case "Btn":
			if (element.YINITIALIZED == null) {
				if (! InitBtn(element)) {
					alert("Problem initializing:" + element.id);
					return false;
				}
			}
			
			element.style.posLeft = y.TBWidth;
			y.TBWidth += element.offsetWidth + 1;
			break;
			
		case "TBGen":
			element.style.posLeft = y.TBWidth;
			y.TBWidth += element.offsetWidth + 1;
			break;
			
		case "TBSep":
			element.style.posLeft = y.TBWidth + 2;
			y.TBWidth += 5;
			break;
			
		case "TBHandle":
			element.style.posLeft = 2;
			y.TBWidth += element.offsetWidth + 7;
			break;
			
		default:
			alert("Invalid class: " + element.className + " on Element: " + element.id + " <" + element.tagName + ">");
			return false;
		}
	}

	y.TBWidth += 1;
	return true;
}


// 设置所属表单的提交或reset事件
function setLinkedField() {
	if (! oLinkField) return ;
	var oForm = oLinkField.form ;
	if (!oForm) return ;
	// 附加submit事件
	oForm.attachEvent("onsubmit", AttachSubmit) ;
	if (! oForm.submitEditor) oForm.submitEditor = new Array() ;
	oForm.submitEditor[oForm.submitEditor.length] = AttachSubmit ;
	if (! oForm.originalSubmit) {
		oForm.originalSubmit = oForm.submit ;
		oForm.submit = function() {
			if (this.submitEditor) {
				for (var i = 0 ; i < this.submitEditor.length ; i++) {
					this.submitEditor[i]() ;
				}
			}
			this.originalSubmit() ;
		}
	}
	// 附加reset事件
	oForm.attachEvent("onreset", AttachReset) ;
	if (! oForm.resetEditor) oForm.resetEditor = new Array() ;
	oForm.resetEditor[oForm.resetEditor.length] = AttachReset ;
	if (! oForm.originalReset) {
		oForm.originalReset = oForm.reset ;
		oForm.reset = function() {
			if (this.resetEditor) {
				for (var i = 0 ; i < this.resetEditor.length ; i++) {
					this.resetEditor[i]() ;
				}
			}
			this.originalReset() ;
		}
	}
}


// 附加submit提交事件,大表单数据提交,保存SeaskyEditor中的内容
function AttachSubmit() { 

	//if (!bEditMode) setMode('EDIT');
	ContentEdit.value = getHTML();
	oLinkField.value = ContentEdit.value;
	var oForm = oLinkField.form ;
	if (!oForm) return ;

	//表单限制值设定，限制值是102399，考虑到中文设为一半
	var FormLimit = 50000 ;
	//var FormLimit = 10 ;

	//取当前表单的值 
	var TempVar = new String ;
	TempVar = oLinkField.value ;
	//TempVar = TempVar.replace(",", "GomyeChar(10)" ) ;

	if (sCurrMode=="TEXT"){
		TempVar = HTMLEncode(TempVar);
		oLinkField.value = TempVar;
	}

	// 未提交成功再次处理时，先赋空值
	for (var i=1;i<parent.document.getElementsByName(sLinkFieldName).length;i++) {
		parent.document.getElementsByName(sLinkFieldName)[i].value = "";
	}

	var j = 0
	//如果表单值超过限制，拆成多个对象
	if (TempVar.length > FormLimit) { 
		oLinkField.value = TempVar.substr(0, FormLimit) ;
		TempVar = TempVar.substr(FormLimit) ;
		while (TempVar.length > 0) { 
		j = j + 1
			var objTEXTAREA = oLinkField.document.createElement("TEXTAREA") ;
			objTEXTAREA.name = sLinkFieldName+j ;
			objTEXTAREA.style.display = "none" ;
			objTEXTAREA.value = TempVar.substr(0, FormLimit) ;
			oForm.appendChild(objTEXTAREA) ;

			TempVar = TempVar.substr(FormLimit) ;
		} 
	} 

		parent.document.getElementsByName("FormCount").value = j;
	//alert(parent.document.getElementsByName("FormCount").value);

} 

// 附加Reset事件
function AttachReset() {
	if(sCurrMode == "VIEW" || sCurrMode == "TEXT")
	{
		alert("文本或预览模式下，不允许重置！！");return false;
	}
	//if (!bEditMode) setMode('EDIT');
	if(bEditMode){
		SeaskyEditor.document.body.innerHTML = ContentLoad.value;
	}else{
		SeaskyEditor.document.body.innerText = ContentLoad.value;
	}
}

// 粘贴时自动检测是否来源于Word格式
function onPaste() {
	if (sCurrMode=="VIEW") return false;

	if (sCurrMode=="EDIT"){
		if (config.AutoDetectPasteFromWord && BrowserInfo.IsIE55OrMore)
		{
			var sHTML = GetClipboardHTML() ;
			var re = /<\w[^>]* class="?MsoNormal"?/gi ;
			if ( re.test(sHTML)){
				if ( confirm( "你要粘贴的内容好象是从Word中拷贝出来的，是否要先清除Word格式再粘贴？" ) ){
					cleanAndPaste( sHTML ) ;
					return false ;
				}
			}
			else
			{
				sHTML = sHTML.replace(/<%/g, "&lt;%") ;
				sHTML = sHTML.replace(/%>/g, "%&gt;") ;
				SeaskyEditor.document.selection.createRange().pasteHTML( sHTML ) ;
				return false;
			}
		}
		else
		{
			return true ;
		}
	}
	else
	{
		SeaskyEditor.document.selection.createRange().pasteHTML(HTMLEncode( clipboardData.getData("Text"))) ;
		return false;
	}
	
}

// 快捷键
function onKeyPress(event){
	if ((sCurrMode=="EDIT")||(sCurrMode=="VIEW")){
		return true;
	}
	if (event.keyCode==13){
		var sel = SeaskyEditor.document.selection.createRange();
		sel.pasteHTML("<br/>");
		event.cancelBubble = true;
		event.returnValue = false;
		sel.select();
		sel.moveEnd("character", 1);
		sel.moveStart("character", 1);
		sel.collapse(false);
		return false;
	}

}

// 取剪粘板中的HTML格式数据
function GetClipboardHTML() {
	var oDiv = document.getElementById("SeaskyEditor_Temp_HTML")
	oDiv.innerHTML = "" ;
	
	var oTextRange = document.body.createTextRange() ;
	oTextRange.moveToElementText(oDiv) ;
	oTextRange.execCommand("Paste") ;
	
	var sData = oDiv.innerHTML ;
	oDiv.innerHTML = "" ;
	
	return sData ;
}

// 清除WORD冗余格式并粘贴
function cleanAndPaste( html ) {
	// Remove all SPAN tags
	html = html.replace(/<\/?SPAN[^>]*>/gi, "" );
	// Remove Class attributes
	html = html.replace(/<(\w[^>]*) class=([^ |>]*)([^>]*)/gi, "<$1$3") ;
	// Remove Style attributes
	html = html.replace(/<(\w[^>]*) style="([^"]*)"([^>]*)/gi, "<$1$3") ;
	// Remove Lang attributes
	html = html.replace(/<(\w[^>]*) lang=([^ |>]*)([^>]*)/gi, "<$1$3") ;
	// Remove XML elements and declarations
	html = html.replace(/<\\?\?xml[^>]*>/gi, "") ;
	// Remove Tags with XML namespace declarations: <o:p></o:p>
	html = html.replace(/<\/?\w+:[^>]*>/gi, "") ;
	// Replace the &nbsp;
	html = html.replace(/&nbsp;/, " " );
	// Transform <P> to <DIV>
	var re = new RegExp("(<P)([^>]*>.*?)(<\/P>)","gi") ;	// Different because of a IE 5.0 error
	html = html.replace( re, "<div$2</div>" ) ;
	
	insertHTML( html ) ;
}

// 在当前文档位置插入.
function insertHTML(html) {
	if (isModeView()) return false;
	if (SeaskyEditor.document.selection.type.toLowerCase() != "none"){
		SeaskyEditor.document.selection.clear() ;
	}
	if (sCurrMode!="EDIT"){
		html=HTMLEncode(html);
	}
	SeaskyEditor.document.selection.createRange().pasteHTML(html) ; 
}

// 设置编辑器的内容
function setHTML(html) {
	if (isModeView()) return false;
	ContentEdit.value = html;
	if(sCurrMode=="EDIT"){
		SeaskyEditor.document.body.innerHTML = html;
	}else{
		SeaskyEditor.document.body.innerText = html;
	}
}

// 取编辑器的内容
function getHTML() {
	var html;

	if((sCurrMode=="EDIT")||(sCurrMode=="VIEW")){
		if(config.MenuStatus == "4")
			html = SeaskyEditor.document.all(0).innerHTML;
		else
			//html = SeaskyEditor.document.body.innerHTML

			var pages;
			_preparesubmit();			//首先让SCRIPTLET准备提交工作，将当前编辑页面的内容保存
			
			var Content = '<?xml version="1.0" ?>';
			Content = Content + '<content></content>';
			var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
			xmlDoc.async = false;
			xmlDoc.loadXML('<content></content>');
			var bodyNode = xmlDoc.selectSingleNode("/content");

			pages = _GetPages();
			for (var i=1;i<=pages;i++) {

				var newNode = xmlDoc.createNode(1,"page" + i,"");
				var xmlCData = xmlDoc.createCDATASection(_GetContent(i));
				newNode.appendChild(xmlCData);
				bodyNode.appendChild(newNode);
				}
			
			var localhost = document.location.protocol+ "//" + document.location.host;
			html = "<?xml version='1.0' encoding='gb2312'?>"+ xmlDoc.xml;
			//html = html.replace("\"","&#34;"); 
			//html = html.replace("'","&#39;");
			//html = html.replace("<","&lt;");
			//html = html.replace(">","&gt;");
			//alert(html);

	}else{
		html = SeaskyEditor.document.body.innerText;
	}
	if (sCurrMode!="TEXT"){
		if (config.BaseUrl){
			var re = new RegExp(sBaseUrl.replace(/\//,"\/"),"gi");
			html = html.replace(re, "");
		}
		if ((html.toLowerCase()=="<p>&nbsp;</p>")||(html.toLowerCase()=="<p></p>")){
			html = "";
		}
	}
	return html;
}

// 在尾部追加内容
function appendHTML(html) {
	if (isModeView()) return false;
	if(sCurrMode=="EDIT"){
		SeaskyEditor.document.body.innerHTML += html;
	}else{
		SeaskyEditor.document.body.innerText += html;
	}
}

// 从Word中粘贴，去除格式
function PasteWord(){
	if (!validateMode()) return;
	SeaskyEditor.focus();
	if (BrowserInfo.IsIE55OrMore)
		cleanAndPaste( GetClipboardHTML() ) ;
	else if ( confirm( "此功能要求IE5.5版本以上，你当前的浏览器不支持，是否按常规粘贴进行？" ) )
		format("paste") ;
	SeaskyEditor.focus();
}

// 粘贴纯文本
function PasteText(){
	if (!validateMode()) return;
	SeaskyEditor.focus();
	var sText = HTMLEncode( clipboardData.getData("Text") ) ;
	insertHTML(sText);
	SeaskyEditor.focus();
}

// 检测当前是否允许编辑
function validateMode() {
	if (sCurrMode=="EDIT") return true;
	alert("需转换为编辑状态后才能使用编辑功能！");
	SeaskyEditor.focus();
	return false;
}

// 检测当前是否在预览模式
function isModeView(){
	if (sCurrMode=="VIEW"){
		alert("预览时不允许设置编辑区内容。");
		return true;
	}
	return false;
}

// 格式化编辑器中的内容
function format(what,opt) {
//	alert(what);
//	alert(opt);
	if (!validateMode()) return;
	SeaskyEditor.focus();
	if (opt=="RemoveFormat") {
		what=opt;
		opt=null;
	}

	if (opt==null) SeaskyEditor.document.execCommand(what);
	else SeaskyEditor.document.execCommand(what,"",opt);
	
	SeaskyEditor.focus();
}

// 确保焦点在 SeaskyEditor 内
function VerifyFocus() {
	if ( SeaskyEditor )
		SeaskyEditor.focus();
}

// 改变模式：代码、编辑、文本、预览
function setMode(NewMode){
	if (NewMode!=sCurrMode){
		
		if (!BrowserInfo.IsIE55OrMore){
			if ((NewMode=="CODE") || (NewMode=="EDIT") || (NewMode=="VIEW")){
				alert("HTML编辑模式需要IE5.5版本以上的支持！");
				return false;
			}
		}

		if (NewMode=="TEXT"){
			if (sCurrMode==ModeEdit.value){
				if (!confirm("警告！切换到纯文本模式会丢失您所有的HTML格式，您确认切换吗？")){
					return false;
				}
			}
		}

		var sBody = "";
		switch(sCurrMode){
		case "CODE":
			if (NewMode == "VIEW") {
				alert("代码模式下，不允许预览！");return false;
			}
			else if (NewMode=="TEXT"){
				SeaskyEditor_Temp_HTML.innerHTML = SeaskyEditor.document.body.innerText;
				sBody = SeaskyEditor_Temp_HTML.innerText;
			}else{
				sBody = SeaskyEditor.document.body.innerText;
			}
			break;
		case "TEXT":
			sBody = SeaskyEditor.document.body.innerText;
			sBody = HTMLEncode(sBody);
			break;
		case "EDIT":
		case "VIEW":
			if (NewMode=="TEXT"){
				sBody = SeaskyEditor.document.body.innerText;
			}else{
				if(config.MenuStatus == "4")
					sBody = SeaskyEditor.document.all(0).outerHTML;
				else
					sBody = SeaskyEditor.document.body.innerHTML;
			}
			break;
		default:
			sBody = ContentEdit.value;
			break;
		}

		// 换内容
		switch (NewMode){
		case "CODE":
			SeaskyEditor.document.designMode="On";
			SeaskyEditor_Toolbar.style.display = "none";
			SeaskyEditor.document.open();
			SeaskyEditor.document.write("Seasky Studio.");
			SeaskyEditor.document.body.innerText = sBody;
			//SeaskyEditor.document.body.contentEditable = "true";
			SeaskyEditor.document.close();
			SeaskyEditor.document.createStyleSheet(CssEditor);
			bEditMode=false;
			break;
		case "EDIT":
			SeaskyEditor.document.designMode="On";
			SeaskyEditor_Toolbar.style.display = "block";
			SeaskyEditor.document.open();
			SeaskyEditor.document.write(sBody);
			//SeaskyEditor.document.body.contentEditable="true";
			SeaskyEditor.document.execCommand("2D-Position",true,true);
			SeaskyEditor.document.execCommand("MultipleSelection", true, true);
			SeaskyEditor.document.execCommand("LiveResize", true, true);
			SeaskyEditor.document.close();
			doZoom(nCurrZoomSize);
			SeaskyEditor.document.createStyleSheet(CssFile);
			bEditMode=true;
			break;
		case "TEXT":
			SeaskyEditor.document.designMode="On";
			SeaskyEditor_Toolbar.style.display = "none";
			SeaskyEditor.document.open();
			SeaskyEditor.document.write("Seasky Studio.");
			SeaskyEditor.document.body.innerText = sBody;
			//SeaskyEditor.document.body.contentEditable="true";
			SeaskyEditor.document.close();
			SeaskyEditor.document.createStyleSheet(CssEditor);
			bEditMode=false;
			break;
		case "VIEW":
			if(parent.submitContent != null)
			{
				parent.openPreview();
				return;
			}

			SeaskyEditor.document.designMode="off";
			SeaskyEditor_Toolbar.style.display = "none";
			SeaskyEditor.document.open();
			SeaskyEditor.document.write(sBody);
			//SeaskyEditor.document.body.contentEditable="false";
			SeaskyEditor.document.close();
			//alert(config.StyleEditorHeader+sBody);
			SeaskyEditor.document.createStyleSheet(CssFile);
			bEditMode=false;
			break;
		}
		// 换图片
		try{
			if(parent.submitContent != null && NewMode == "VIEW")
			{
				return;
			}
			document.all["SeaskyEditor_CODE"].className = "StatusBarBtnOff";
			document.all["SeaskyEditor_EDIT"].className = "StatusBarBtnOff";
			document.all["SeaskyEditor_TEXT"].className = "StatusBarBtnOff";
			document.all["SeaskyEditor_VIEW"].className = "StatusBarBtnOff";
			document.all["SeaskyEditor_"+NewMode].className = "StatusBarBtnOn";
		}
		catch(e){
			}
		sCurrMode=NewMode;
		ModeEdit.value = NewMode;
		disableChildren(SeaskyEditor_Toolbar);

		SeaskyEditor.document.body.onpaste = onPaste ;
		SeaskyEditor.document.onkeypress = new Function("return onKeyPress(SeaskyEditor.event);");
		SeaskyEditor.document.oncontextmenu = new Function("return showContextMenu(SeaskyEditor.event);");

		if ((borderShown != "no")&&bEditMode) {
			borderShown = "no";
			showBorders();
		}

	}
	SeaskyEditor.focus();
}

// 使工具栏无效
function disableChildren(obj){
	if (obj){
		obj.disabled=(!bEditMode);
		for (var i=0; i<obj.children.length; i++){
			disableChildren(obj.children[i]);
		}
	}
}



// 显示无模式对话框
function ShowDialog(url, width, height, optValidate) {
	if (optValidate) {
		if (!validateMode()) return;
	}
	SeaskyEditor.focus();
	var arr = showModalDialog(url, window, "dialogWidth:" + width + "px;dialogHeight:" + height + "px;help:no;scroll:no;status:no");
	SeaskyEditor.focus();
}






// 全屏编辑
function Maximize() {
	if (!validateMode()) return;
	window.open("dialog/fullscreen.htm", 'FullScreen'+sLinkFieldName, 'toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,fullscreen=yes');
}

// 创建或修改超级链接
function createLink(){
	if (!validateMode()) return;
	
	if (SeaskyEditor.document.selection.type == "Control") {
		var oControlRange = SeaskyEditor.document.selection.createRange();
		if (oControlRange(0).tagName.toUpperCase() != "IMG") {
			alert("链接只能是图片或文本");
			return;
		}
	}
	
	ShowDialog("dialog/hyperlink.htm", 350, 180, true);
}

// 替换特殊字符
function HTMLEncode(text){
	text = text.replace(/&/g, "&amp;") ;
	text = text.replace(/"/g, "&quot;") ;
	text = text.replace(/</g, "&lt;") ;
	text = text.replace(/>/g, "&gt;") ;
	text = text.replace(/'/g, "&#146;") ;
	text = text.replace(/\ /g,"&nbsp;");
	text = text.replace(/\n/g,"<br/>");
	text = text.replace(/\t/g,"&nbsp;&nbsp;&nbsp;&nbsp;");
	return text;
}

// 插入特殊对象
function insert(what) {
	if (!validateMode()) return;
	SeaskyEditor.focus();
	var sel = SeaskyEditor.document.selection.createRange();

	switch(what){
	case "excel":		// 插入EXCEL表格
		insertHTML("<object classid='clsid:0002E510-0000-0000-C000-000000000046' id='Spreadsheet1' codebase='file:\\Bob\software\office2000\msowc.cab' width='100%' height='250'><param name='HTMLURL' value><param name='HTMLData' value='&lt;html xmlns:x=&quot;urn:schemas-microsoft-com:office:excel&quot;xmlns=&quot;http://www.w3.org/TR/REC-html40&quot;&gt;&lt;head&gt;&lt;style type=&quot;text/css&quot;&gt;&lt;!--tr{mso-height-source:auto;}td{black-space:nowrap;}.wc4590F88{black-space:nowrap;font-family:宋体;mso-number-format:General;font-size:auto;font-weight:auto;font-style:auto;text-decoration:auto;mso-background-source:auto;mso-pattern:auto;mso-color-source:auto;text-align:general;vertical-align:bottom;border-top:none;border-left:none;border-right:none;border-bottom:none;mso-protection:locked;}--&gt;&lt;/style&gt;&lt;/head&gt;&lt;body&gt;&lt;!--[if gte mso 9]&gt;&lt;xml&gt;&lt;x:ExcelWorkbook&gt;&lt;x:ExcelWorksheets&gt;&lt;x:ExcelWorksheet&gt;&lt;x:OWCVersion&gt;9.0.0.2710&lt;/x:OWCVersion&gt;&lt;x:Label Style='border-top:solid .5pt silver;border-left:solid .5pt silver;border-right:solid .5pt silver;border-bottom:solid .5pt silver'&gt;&lt;x:Caption&gt;Microsoft Office Spreadsheet&lt;/x:Caption&gt; &lt;/x:Label&gt;&lt;x:Name&gt;Sheet1&lt;/x:Name&gt;&lt;x:WorksheetOptions&gt;&lt;x:Selected/&gt;&lt;x:Height&gt;7620&lt;/x:Height&gt;&lt;x:Width&gt;15240&lt;/x:Width&gt;&lt;x:TopRowVisible&gt;0&lt;/x:TopRowVisible&gt;&lt;x:LeftColumnVisible&gt;0&lt;/x:LeftColumnVisible&gt; &lt;x:ProtectContents&gt;False&lt;/x:ProtectContents&gt; &lt;x:DefaultRowHeight&gt;210&lt;/x:DefaultRowHeight&gt; &lt;x:StandardWidth&gt;2389&lt;/x:StandardWidth&gt; &lt;/x:WorksheetOptions&gt; &lt;/x:ExcelWorksheet&gt;&lt;/x:ExcelWorksheets&gt; &lt;x:MaxHeight&gt;80%&lt;/x:MaxHeight&gt;&lt;x:MaxWidth&gt;80%&lt;/x:MaxWidth&gt;&lt;/x:ExcelWorkbook&gt;&lt;/xml&gt;&lt;![endif]--&gt;&lt;table class=wc4590F88 x:str&gt;&lt;col width=&quot;56&quot;&gt;&lt;tr height=&quot;14&quot;&gt;&lt;td&gt;&lt;/td&gt;&lt;/tr&gt;&lt;/table&gt;&lt;/body&gt;&lt;/html&gt;'> <param name='DataType' value='HTMLDATA'> <param name='AutoFit' value='0'><param name='DisplayColHeaders' value='-1'><param name='DisplayGridlines' value='-1'><param name='DisplayHorizontalScrollBar' value='-1'><param name='DisplayRowHeaders' value='-1'><param name='DisplayTitleBar' value='-1'><param name='DisplayToolbar' value='-1'><param name='DisplayVerticalScrollBar' value='-1'> <param name='EnableAutoCalculate' value='-1'> <param name='EnableEvents' value='-1'><param name='MoveAfterReturn' value='-1'><param name='MoveAfterReturnDirection' value='0'><param name='RightToLeft' value='0'><param name='ViewableRange' value='1:65536'></object>");
		break;
	case "nowdate":		// 插入当前系统日期
		var d = new Date();
		insertHTML(d.toLocaleDateString());
		break;
	case "nowtime":		// 插入当前系统时间
		var d = new Date();
		insertHTML(d.toLocaleTimeString());
		break;
	case "br":			// 插入换行符
		insertHTML("<br/>")
		break;
	case "code":		// 代码片段样式
		insertHTML('<table width=95% border="0" align="Center" cellpadding="6" cellspacing="0" style="border: 1px Dotted #CCCCCC; TABLE-LAYOUT: fixed"><tr><td bgcolor=#FDFDDF style="WORD-WRAP: break-word"><font style="color: #990000;font-weight:bold">以下是代码片段：</font><br/>'+HTMLEncode(sel.text)+'</td></tr></table>');
		break;
	case "quote":		// 引用片段样式
		insertHTML('<table width=95% border="0" align="Center" cellpadding="6" cellspacing="0" style="border: 1px Dotted #CCCCCC; TABLE-LAYOUT: fixed"><tr><td bgcolor=#F3F3F3 style="WORD-WRAP: break-word"><font style="color: #990000;font-weight:bold">以下是引用片段：</font><br/>'+HTMLEncode(sel.text)+'</td></tr></table>');
		break;
	case "big":			// 字体变大
		insertHTML("<big>" + sel.text + "</big>");
		break;
	case "small":		// 字体变小
		insertHTML("<small>" + sel.text + "</small>");
		break;
	default:
		alert("错误参数调用！");
		break;
	}
	sel=null;
}

// 显示或隐藏指导方针
var borderShown = "no";
function showBorders() {
	if (!validateMode()) return;
	
	var allForms = SeaskyEditor.document.body.getElementsByTagName("FORM");
	var allInputs = SeaskyEditor.document.body.getElementsByTagName("INPUT");
	var allTables = SeaskyEditor.document.body.getElementsByTagName("TABLE");
	var allLinks = SeaskyEditor.document.body.getElementsByTagName("A");

	// 表单
	for (a=0; a < allForms.length; a++) {
		if (borderShown == "no") {
			allForms[a].runtimeStyle.border = "1px dotted #FF0000"
		} else {
			allForms[a].runtimeStyle.cssText = ""
		}
	}

	// Input Hidden类
	for (b=0; b < allInputs.length; b++) {
		if (borderShown == "no") {
			if (allInputs[b].type.toUpperCase() == "HIDDEN") {
				allInputs[b].runtimeStyle.border = "1px dashed #000000"
				allInputs[b].runtimeStyle.width = "15px"
				allInputs[b].runtimeStyle.height = "15px"
				allInputs[b].runtimeStyle.backgroundColor = "#FDADAD"
				allInputs[b].runtimeStyle.color = "#FDADAD"
			}
		} else {
			if (allInputs[b].type.toUpperCase() == "HIDDEN")
				allInputs[b].runtimeStyle.cssText = ""
		}
	}

	// 表格
	for (i=0; i < allTables.length; i++) {
			if (borderShown == "no") {
				allTables[i].runtimeStyle.border = "1px dotted #BFBFBF"
			} else {
				allTables[i].runtimeStyle.cssText = ""
			}

			allRows = allTables[i].rows
			for (y=0; y < allRows.length; y++) {
			 	allCellsInRow = allRows[y].cells
					for (x=0; x < allCellsInRow.length; x++) {
						if (borderShown == "no") {
							allCellsInRow[x].runtimeStyle.border = "1px dotted #BFBFBF"
						} else {
							allCellsInRow[x].runtimeStyle.cssText = ""
						}
					}
			}
	}

	// 链接 A
	for (a=0; a < allLinks.length; a++) {
		if (borderShown == "no") {
			if (allLinks[a].href.toUpperCase() == "") {
				allLinks[a].runtimeStyle.border = "1px dashed #000000"
				allLinks[a].runtimeStyle.width = "20px"
				allLinks[a].runtimeStyle.height = "16px"
				allLinks[a].runtimeStyle.backgroundColor = "#FFFFCC"
				allLinks[a].runtimeStyle.color = "#FFFFCC"					
			}
		} else {
			allLinks[a].runtimeStyle.cssText = ""		
		}
	}

	if (borderShown == "no") {
		borderShown = "yes"
	} else {
		borderShown = "no"
	}

	scrollUp()
}

// 返回页面最上部
function scrollUp() {
	SeaskyEditor.scrollBy(0,0);
}

// 缩放操作
var nCurrZoomSize = 100;
var aZoomSize = new Array(10, 25, 50, 75, 100, 150, 200, 500);
function doZoom(size) {
	SeaskyEditor.document.body.runtimeStyle.zoom = size + "%";
	nCurrZoomSize = size;
	
	SeaskyEditor.focus();
}

// 拼写检查
function spellCheck(){
	ShowDialog('dialog/spellcheck.htm', 300, 220, true)
}

// 查找替换
function findReplace(){
	ShowDialog('dialog/findreplace.htm', 320, 165, true)
}

// 相对(absolute)或绝对位置(static)
function absolutePosition(){
	var objReference	= null;
	var RangeType		= SeaskyEditor.document.selection.type;
	if (RangeType != "Control") return;
	var selectedRange	= SeaskyEditor.document.selection.createRange();
	for (var i=0; i<selectedRange.length; i++){
		objReference = selectedRange.item(i);
		if (objReference.style.position != 'absolute') {
			objReference.style.position='absolute';
		}else{
			objReference.style.position='static';
		}
	}
	SeaskyEditor.content = false;
	//SeaskyEditor.setActive();
}

// 上移(forward)或下移(backward)一层
function zIndex(action){
	var objReference	= null;
	var RangeType		= SeaskyEditor.document.selection.type;
	if (RangeType != "Control") return;
	var selectedRange	= SeaskyEditor.document.selection.createRange();
	for (var i=0; i<selectedRange.length; i++){
		objReference = selectedRange.item(i);
		if (action=='forward'){
			objReference.style.zIndex  +=1;
		}else{
			objReference.style.zIndex  -=1;
		}
		objReference.style.position='absolute';
	}
	SeaskyEditor.content = false;
	//SeaskyEditor.setActive();
}

// 是否选中指定类型的控件
function isControlSelected(tag){
	if (SeaskyEditor.document.selection.type == "Control") {
		var oControlRange = SeaskyEditor.document.selection.createRange();
		if (oControlRange(0).tagName.toUpperCase() == tag) {
			return true;
		}	
	}
	return false;
}

// 改变编辑区高度
function sizeChange(size){
	if (!BrowserInfo.IsIE55OrMore){
		alert("此功能需要IE5.5版本以上的支持！");
		return false;
	}
	for (var i=0; i<parent.frames.length; i++){
		if (parent.frames[i].document==self.document){
			var obj=parent.frames[i].frameElement;
			var height = parseInt(obj.offsetHeight);
			if (height+size>=300){
				obj.height=height+size;
			}
			break;
		}
	}
}

// 热点链接
function mapEdit(){
	if (!validateMode()) return;
	
	var b = false;
	if (SeaskyEditor.document.selection.type == "Control") {
		var oControlRange = SeaskyEditor.document.selection.createRange();
		if (oControlRange(0).tagName.toUpperCase() == "IMG") {
			b = true;
		}
	}
	if (!b){
		alert("热点链接只能作用于图片");
		return;
	}

	window.open("dialog/map.htm", 'mapEdit'+sLinkFieldName, 'toolbar=no,location=no,directories=no,status=not,menubar=no,scrollbars=no,resizable=yes,width=450,height=300');
}

//获取按钮配置信息
function GetXML(id)
{
	var objXMLDoc = new ActiveXObject("Microsoft.XMLDOM");
	objXMLDoc.async = false;
	objXMLDoc.load( "Config/buttonConfig.xml" );
	
	var outstr = "";
	if (objXMLDoc.xml != "")
	{
		var oDataXMLSrc = objXMLDoc.documentElement.selectSingleNode("//buttonConfig/buttons[@id='" + id + "']");
		if ( oDataXMLSrc != null )
		{
			//this.sDataXMLSrc = String( oDataXMLSrc.getAttribute("configName") );
			var nodes = oDataXMLSrc.childNodes;
			if(nodes != null)
			{
				for(var i = 0; i < nodes.length; i++)
				{
					outstr += nodes.item(i).text;
				}
			}
		}
	}
	
	return outstr;
}

//自动排版
function AutoFormat(){
//	var html = get_value();
//	alert(SeaskyEditor.document.body.innerText);
	html = SeaskyEditor.document.body.innerHTML;
	html = formattext(html,true);
	SeaskyEditor.document.body.innerHTML = html;
//	charct();
}