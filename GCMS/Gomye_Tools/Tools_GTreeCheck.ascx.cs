using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using System.Data.SqlClient;

public partial class Gomye_Tools_Tools_GTreeCheck : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private string _Sql;
    public string Sql
    {
        get { return _Sql; }
        set { _Sql = value; }
    }

    private string _Url;
    public string Url
    {
        get { return _Url; }
        set { _Url = value; }
    }

    private string _Target;
    public string Target
    {
        get { return _Target; }
        set { _Target = value; }
    }

    private string _Mode;
    public string Mode
    {
        get { return _Mode; }
        set { _Mode = value; }
    }

    private string _JS = "";
    public string JS
    {
        get { return _JS; }
        set { _JS = value; }
    }


    protected override void Render(HtmlTextWriter output)
    {

        Type_TypeTree _Type_TypeTree = new Type_TypeTree();

        if (!this.Page.IsClientScriptBlockRegistered("clientScript"))
        {
            output.WriteLine("<script language=\"Javascript\">");
            output.WriteLine("var CurrentNode = null;");

            output.WriteLine("var cuObject=null;");

            output.WriteLine("var webFXTreeConfig = {");
            output.WriteLine("rootIcon        : '../Admin_Public/images/root.gif',");
            output.WriteLine("openRootIcon    : '../Admin_Public/images/root.gif',");
            output.WriteLine("folderIcon      : '../Admin_Public/images/fc.gif',");
            output.WriteLine("openFolderIcon  : '../Admin_Public/images/fo.gif',");
            output.WriteLine("fileIcon        : '../Admin_Public/images/Tree_white.gif',");
            output.WriteLine("iIcon           : '../Admin_Public/images/Tree_white.gif',");
            output.WriteLine("lIcon           : '../Admin_Public/images/Tree_white.gif',");
            output.WriteLine("lMinusIcon      : '../Admin_Public/images/Rminus.gif',");
            output.WriteLine("lPlusIcon       : '../Admin_Public/images/Rplus.gif',");
            output.WriteLine("tIcon           : '../Admin_Public/images/Tree_white.gif',");
            output.WriteLine("tMinusIcon      : '../Admin_Public/images/Rminus.gif',");
            output.WriteLine("tPlusIcon       : '../Admin_Public/images/Rplus.gif',");
            output.WriteLine("blankIcon       : '../Admin_Public/images/Tree_white.gif',");
            output.WriteLine("defaultText     : '文件夹',");
            output.WriteLine("defaultAction   : 'javascript:void(0);',");
            output.WriteLine("defaultBehavior : 'classic'");
            output.WriteLine("};");

            output.WriteLine("	var webFXTreeHandler = {");
            output.WriteLine("	idCounter : 0,");
            output.WriteLine("	idPrefix  : \"webfx-tree-object-\",");
            output.WriteLine("	all       : {},");
            output.WriteLine("	behavior  : null,");
            output.WriteLine("	selected  : null,");
            output.WriteLine("	onSelect  : null, /* should be part of tree, not handler */");
            output.WriteLine("	getId     : function() { return this.idPrefix + this.idCounter++; },");
            output.WriteLine("	toggle    : function (oItem) { this.all[oItem.id.replace('-plus','')].toggle(); },");
            output.WriteLine("	select    : function (oItem) { this.all[oItem.id.replace('-icon','')].select(); },");
            output.WriteLine("	focus     : function (oItem) { this.all[oItem.id.replace('-anchor','')].focus(); },");
            output.WriteLine("	blur      : function (oItem) { this.all[oItem.id.replace('-anchor','')].blur(); },");
            output.WriteLine("	keydown   : function (oItem, e) { return this.all[oItem.id].keydown(e.keyCode); },");
            output.WriteLine("	dropme	  : function (oItem) {cuObject = this.all[oItem.id];},");
            output.WriteLine("	insertHTMLBeforeEnd	:	function (oElement, sHTML) {");
            output.WriteLine("	if (oElement.insertAdjacentHTML != null) {");
            output.WriteLine("	oElement.insertAdjacentHTML(\"BeforeEnd\", sHTML)");
            output.WriteLine("	return;");
            output.WriteLine("	}");
            output.WriteLine("	var df;	// DocumentFragment");
            output.WriteLine("	var r = oElement.ownerDocument.createRange();");
            output.WriteLine("	r.selectNodeContents(oElement);");
            output.WriteLine("	r.collapse(false);");
            output.WriteLine("	df = r.createContextualFragment(sHTML);");
            output.WriteLine("	oElement.appendChild(df);");
            output.WriteLine("	}");
            output.WriteLine("	};");

            output.WriteLine("/*");
            output.WriteLine(" * WebFXTreeAbstractNode class");
            output.WriteLine(" */");

            output.WriteLine("function WebFXTreeAbstractNode(sText, sAction, sType) {");
            output.WriteLine("	this.childNodes  = [];");
            output.WriteLine("	this.id     = webFXTreeHandler.getId();");
            output.WriteLine("	this.text   = sText || webFXTreeConfig.defaultText;");
            output.WriteLine("	this.action = sAction || webFXTreeConfig.defaultAction;");
            output.WriteLine("	this.type	= sType || webFXTreeConfig.defaultType;");
            output.WriteLine("	this._last  = false;");
            output.WriteLine("	webFXTreeHandler.all[this.id] = this;");
            output.WriteLine("}");

            output.WriteLine("/*");
            output.WriteLine(" * To speed thing up if you're adding multiple nodes at once (after load)");
            output.WriteLine(" * use the bNoIdent parameter to prevent automatic re-indentation and call");
            output.WriteLine(" * the obj.ident() method manually once all nodes has been added.");
            output.WriteLine(" */");

            output.WriteLine("WebFXTreeAbstractNode.prototype.add = function (node, bNoIdent) {");
            output.WriteLine("	node.parentNode = this;");
            output.WriteLine("	this.childNodes[this.childNodes.length] = node;");
            output.WriteLine("	var root = this;");
            output.WriteLine("	if (this.childNodes.length >=2) {");
            output.WriteLine("		this.childNodes[this.childNodes.length -2]._last = false;");
            output.WriteLine("	}");
            output.WriteLine("	while (root.parentNode) { root = root.parentNode; }");
            output.WriteLine("	if (root.rendered) {");
            output.WriteLine("		if (this.childNodes.length >= 2) {");
            output.WriteLine("			document.getElementById(this.childNodes[this.childNodes.length -2].id + '-plus').src = ((this.childNodes[this.childNodes.length -2].folder)?((this.childNodes[this.childNodes.length -2].open)?webFXTreeConfig.tMinusIcon:webFXTreeConfig.tPlusIcon):webFXTreeConfig.tIcon);");
            output.WriteLine("			if (this.childNodes[this.childNodes.length -2].folder) {");
            output.WriteLine("				this.childNodes[this.childNodes.length -2].plusIcon = webFXTreeConfig.tPlusIcon;");
            output.WriteLine("				this.childNodes[this.childNodes.length -2].minusIcon = webFXTreeConfig.tMinusIcon;");
            output.WriteLine("			}");
            output.WriteLine("			this.childNodes[this.childNodes.length -2]._last = false;");
            output.WriteLine("		}");
            output.WriteLine("		this._last = true;");
            output.WriteLine("		var foo = this;");
            output.WriteLine("		while (foo.parentNode) {");
            output.WriteLine("			for (var i = 0; i < foo.parentNode.childNodes.length; i++) {");
            output.WriteLine("				if (foo.id == foo.parentNode.childNodes[i].id) { break; }");
            output.WriteLine("			}");
            output.WriteLine("			if (++i == foo.parentNode.childNodes.length) { foo.parentNode._last = true; }");
            output.WriteLine("			else { foo.parentNode._last = false; }");
            output.WriteLine("			foo = foo.parentNode;");
            output.WriteLine("		}");
            output.WriteLine("		webFXTreeHandler.insertHTMLBeforeEnd(document.getElementById(this.id + '-cont'), node.toString());");
            output.WriteLine("		if ((!this.folder) && (!this.openIcon)) {");
            output.WriteLine("			this.icon = webFXTreeConfig.folderIcon;");
            output.WriteLine("			this.openIcon = webFXTreeConfig.openFolderIcon;");
            output.WriteLine("		}");
            output.WriteLine("		if (!this.folder) { this.folder = true; this.collapse(true); }");
            output.WriteLine("		if (!bNoIdent) { this.indent(); }");
            output.WriteLine("	}");
            output.WriteLine("	return node;");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.toggle = function() {");
            output.WriteLine("	if (this.folder) {");
            output.WriteLine("		if (this.open) { this.collapse(); }");
            output.WriteLine("		else { this.expand(); }");
            output.WriteLine("}	}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.select = function() {");
            output.WriteLine("	document.getElementById(this.id + '-anchor').focus();");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.deSelect = function() {");
            output.WriteLine("	document.getElementById(this.id + '-anchor').className = '';");
            output.WriteLine("	if ((this.openIcon) && (webFXTreeHandler.behavior != 'classic')) { document.getElementById(this.id + '-icon').src = this.icon; }");
            output.WriteLine("	webFXTreeHandler.selected = null;");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.focus = function() {");
            output.WriteLine("	if ((webFXTreeHandler.selected) && (webFXTreeHandler.selected != this)) { webFXTreeHandler.selected.deSelect(); }");
            output.WriteLine("	webFXTreeHandler.selected = this;");
            output.WriteLine("	if ((this.openIcon) && (webFXTreeHandler.behavior != 'classic')) { document.getElementById(this.id + '-icon').src = this.openIcon; }");
            output.WriteLine("	document.getElementById(this.id + '-anchor').className = 'selected';");
            output.WriteLine("	document.getElementById(this.id + '-anchor').focus();");
            output.WriteLine("	if (webFXTreeHandler.onSelect) { webFXTreeHandler.onSelect(this); }");

            output.WriteLine("	OpenFolder(this.Path);");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.blur = function() {");
            output.WriteLine("	if ((this.openIcon) && (webFXTreeHandler.behavior != 'classic')) { document.getElementById(this.id + '-icon').src = this.icon; }");
            output.WriteLine("	document.getElementById(this.id + '-anchor').className = 'selected-inactive';");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.doExpand = function() {");
            output.WriteLine("	if (webFXTreeHandler.behavior == 'classic') { document.getElementById(this.id + '-icon').src = this.openIcon; }");
            output.WriteLine("	if (this.childNodes.length) {  document.getElementById(this.id + '-cont').style.display = 'block'; }");
            output.WriteLine("	this.open = true;");

            output.WriteLine("	if (this.Tag!=\"N\"){");
            output.WriteLine("		return;");
            output.WriteLine("	}");

            output.WriteLine("	document.body.style.cursor=\"wait\";");

            output.WriteLine("	var objXMLDom = new ActiveXObject(\"Microsoft.XMLDOM\");");
            output.WriteLine("	objXMLDom.async = false;");

            output.WriteLine("	objXMLDom.load(\"../Content/Tools_Xtree.aspx?TypeTree_ID=\" + escape(this.Path) + \"&Mode=" + this._Mode + "\");"); //路径
            //output.WriteLine("	objXMLDom.loadXML(\"" + ReturnXML(1,this.Path) + "\";");
            //output.WriteLine("	alert(\""+Server.MapPath("//GProtal//GCMS//Content//Tools_Xtree.aspx?TypeTree_ID=")+"\" + escape(this.Path) + \"&Mode="+this._Mode+"\");");

            //output.WriteLine("	//objXMLDom.load(\"getfolder.asp?path=\" + escape(this.Path));");
            //output.WriteLine("	//objXMLDom.load(\"getfolder.asp?path=\" + escape(this.Path));");


            output.WriteLine("	for (var i=0;i<this.childNodes.length;i++){");
            output.WriteLine("	this.childNodes[i].remove();");
            output.WriteLine("	}");
            output.WriteLine("	//原程序有错,执行两次remove保证清空,2004-5-29");

            if (this.Mode != "4")
            {
                output.WriteLine("	for (var i=0;i<this.childNodes.length;i++){");
                output.WriteLine("		this.childNodes[i].remove();");
                output.WriteLine("	}");

                output.WriteLine("	if (objXMLDom.xml==\"\"){");
                output.WriteLine("		this.add(new WebFXTreeItem(\"读取数据错误\",\"Y\"));");
                output.WriteLine("	}");
                output.WriteLine("	else{");
                output.WriteLine("		var oFolders = objXMLDom.selectNodes(\"//folder\");");
                output.WriteLine("		var hasSubfolder = false;");
                output.WriteLine("		var sType = 1;");
                output.WriteLine("		");
                output.WriteLine("		for (var i=0;i<oFolders.length;i++){");
                output.WriteLine("			sType = oFolders[i].getAttribute(\"type\");");  // type 值

                output.WriteLine("			if (oFolders[i].getAttribute(\"hassubfolder\")==\"yes\"){");
                output.WriteLine("				hasSubfolder=true;");
                output.WriteLine("			}else{");
                output.WriteLine("				hasSubfolder=false;");
                output.WriteLine("			}");
                output.WriteLine("			var aNode = this.add(new WebFXTreeItem(oFolders[i].getAttribute(\"name\"),hasSubfolder?\"N\":\"Y\",oFolders[i].getAttribute(\"id\"),sType));");
                output.WriteLine("			if (hasSubfolder){");
                output.WriteLine("				aNode.add(new WebFXTreeItem(\"请梢候...\",\"Y\"));");
                output.WriteLine("			}");
                output.WriteLine("			//aNode.Path=oFolders[i].getAttribute(\"id\")+''");
                output.WriteLine("		}");
                output.WriteLine("		this.Tag=\"Y\";");
                output.WriteLine("	}");
            }



            output.WriteLine("	if (webFXTreeHandler.behavior == 'classic') { document.getElementById(this.id + '-icon').src = this.openIcon; }");
            output.WriteLine("	if (this.childNodes.length) {  document.getElementById(this.id + '-cont').style.display = 'block'; }");
            output.WriteLine("	this.open = true;");

            output.WriteLine("	document.body.style.cursor=\"default\";");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.doCollapse = function() {");
            output.WriteLine("	if (webFXTreeHandler.behavior == 'classic') { document.getElementById(this.id + '-icon').src = this.icon; }");
            output.WriteLine("	if (this.childNodes.length) { document.getElementById(this.id + '-cont').style.display = 'none'; }");
            output.WriteLine("	this.open = false;");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.expandAll = function() {");
            output.WriteLine("	this.expandChildren();");
            output.WriteLine("	if ((this.folder) && (!this.open)) { this.expand(); }");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.expandChildren = function() {");
            output.WriteLine("	for (var i = 0; i < this.childNodes.length; i++) {");
            output.WriteLine("		this.childNodes[i].expandAll();");
            output.WriteLine("} }");

            output.WriteLine("WebFXTreeAbstractNode.prototype.collapseAll = function() {");
            output.WriteLine("	this.collapseChildren();");
            output.WriteLine("	if ((this.folder) && (this.open)) { this.collapse(true); }");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeAbstractNode.prototype.collapseChildren = function() {");
            output.WriteLine("	for (var i = 0; i < this.childNodes.length; i++) {");
            output.WriteLine("		this.childNodes[i].collapseAll();");
            output.WriteLine("} }");

            output.WriteLine("WebFXTreeAbstractNode.prototype.indent = function(lvl, del, last, level, nodesLeft) {");
            output.WriteLine("	/*");
            output.WriteLine("	 * Since we only want to modify items one level below ourself,");
            output.WriteLine("	 * and since the rightmost indentation position is occupied by");
            output.WriteLine("	 * the plus icon we set this to -2");
            output.WriteLine("	 */");
            output.WriteLine("	if (lvl == null) { lvl = -2; }");
            output.WriteLine("	var state = 0;");
            output.WriteLine("	for (var i = this.childNodes.length - 1; i >= 0 ; i--) {");
            output.WriteLine("		state = this.childNodes[i].indent(lvl + 1, del, last, level);");
            output.WriteLine("		if (state) { return; }");
            output.WriteLine("	}");
            output.WriteLine("	if (del) {");
            output.WriteLine("		if ((level >= this._level) && (document.getElementById(this.id + '-plus'))) {");
            output.WriteLine("			if (this.folder) {");
            output.WriteLine("				document.getElementById(this.id + '-plus').src = (this.open)?webFXTreeConfig.lMinusIcon:webFXTreeConfig.lPlusIcon;");
            output.WriteLine("				this.plusIcon = webFXTreeConfig.lPlusIcon;");
            output.WriteLine("				this.minusIcon = webFXTreeConfig.lMinusIcon;");
            output.WriteLine("			}");
            output.WriteLine("			else if (nodesLeft) { document.getElementById(this.id + '-plus').src = webFXTreeConfig.lIcon; }");
            output.WriteLine("			return 1;");
            output.WriteLine("	}	}");
            output.WriteLine("	var foo = document.getElementById(this.id + '-indent-' + lvl);");
            output.WriteLine("	if (foo) {");
            output.WriteLine("		if ((foo._last) || ((del) && (last))) { foo.src =  webFXTreeConfig.blankIcon; }");
            output.WriteLine("		else { foo.src =  webFXTreeConfig.iIcon; }");
            output.WriteLine("	}");
            output.WriteLine("	return 0;");
            output.WriteLine("}");

            output.WriteLine("/*");
            output.WriteLine(" * WebFXTree class");
            output.WriteLine(" */");

            output.WriteLine("function WebFXTree(sText, sAction, sBehavior, sType, sIcon, sOpenIcon) {");
            output.WriteLine("	this.base = WebFXTreeAbstractNode;");
            output.WriteLine("	this.base(sText, sAction, sType);");
            output.WriteLine("	this.Tag=\"N\";");
            output.WriteLine("	this.Path=\"0\";");
            output.WriteLine("	this.icon      = sIcon || webFXTreeConfig.rootIcon;");
            output.WriteLine("	this.openIcon  = sOpenIcon || webFXTreeConfig.openRootIcon;");
            output.WriteLine("	this.open=true;");
            output.WriteLine("	this.folder    = true;");
            output.WriteLine("	this.rendered  = false;");
            output.WriteLine("	this.onSelect  = null;");
            output.WriteLine("	if (!webFXTreeHandler.behavior) {  webFXTreeHandler.behavior = sBehavior || webFXTreeConfig.defaultBehavior; }");
            output.WriteLine("}");

            output.WriteLine("WebFXTree.prototype = new WebFXTreeAbstractNode;");

            output.WriteLine("WebFXTree.prototype.setBehavior = function (sBehavior) {");
            output.WriteLine("	webFXTreeHandler.behavior =  sBehavior;");
            output.WriteLine("};");

            output.WriteLine("WebFXTree.prototype.getBehavior = function (sBehavior) {");
            output.WriteLine("	return webFXTreeHandler.behavior;");
            output.WriteLine("};");

            output.WriteLine("WebFXTree.prototype.getSelected = function() {");
            output.WriteLine("	if (webFXTreeHandler.selected) { return webFXTreeHandler.selected; }");
            output.WriteLine("	else { return null; }");
            output.WriteLine("}");

            output.WriteLine("WebFXTree.prototype.remove = function() { }");

            output.WriteLine("WebFXTree.prototype.expand = function() {");
            output.WriteLine("	this.doExpand();");
            output.WriteLine("}");

            output.WriteLine("WebFXTree.prototype.collapse = function(b) {");
            output.WriteLine("	if (!b) { this.focus(); }");
            output.WriteLine("	this.doCollapse();");
            output.WriteLine("}");

            output.WriteLine("WebFXTree.prototype.getFirst = function() {");
            output.WriteLine("	return null;");
            output.WriteLine("}");

            output.WriteLine("WebFXTree.prototype.getLast = function() {");
            output.WriteLine("	return null;");
            output.WriteLine("}");

            output.WriteLine("WebFXTree.prototype.getNextSibling = function() {");
            output.WriteLine("	return null;");
            output.WriteLine("}");

            output.WriteLine("WebFXTree.prototype.getPreviousSibling = function() {");
            output.WriteLine("	return null;");
            output.WriteLine("}");

            output.WriteLine("WebFXTree.prototype.keydown = function(key) {");
            output.WriteLine("	if (key == 39) {");
            output.WriteLine("		if (!this.open) { this.expand(); }");
            output.WriteLine("		else if (this.childNodes.length) { this.childNodes[0].select(); }");
            output.WriteLine("		return false;");
            output.WriteLine("	}");
            output.WriteLine("	if (key == 37) { this.collapse(); return false; }");
            output.WriteLine("	if ((key == 40) && (this.open) && (this.childNodes.length)) { this.childNodes[0].select(); return false; }");
            output.WriteLine("	return true;");
            output.WriteLine("}");


            //移动
            output.WriteLine("WebFXTree.prototype.toString = function() {");
            output.WriteLine("	var str = \"<div id='\" + this.id + \"' ondblclick='webFXTreeHandler.toggle(this);' class='webfx-tree-item' onkeydown='return webFXTreeHandler.keydown(this, event)' ondragenter='dragEnter()' ondragleave='dragLeave()' ondragover='dragOver()' ondrop='webFXTreeHandler.dropme(this);FinishDrag(0)' >\";");
            output.WriteLine("	str += \"<img id='\" + this.id + \"-icon' class='webfx-tree-icon' src='\" + ((webFXTreeHandler.behavior == 'classic' && this.open)?this.openIcon:this.icon) + \"' onclick='webFXTreeHandler.select(this.parentElement);'><span id='\" + this.id + \"-anchor' onclick='webFXTreeHandler.focus(this);'>\" + this.text + \"</span></div>\";");
            output.WriteLine("	str += \"<div id='\" + this.id + \"-cont' class='webfx-tree-container' style='display: \" + ((this.open)?'block':'none') + \";'>\";");
            output.WriteLine("	for (var i = 0; i < this.childNodes.length; i++) {");
            output.WriteLine("		str += this.childNodes[i].toString(i, this.childNodes.length);");
            output.WriteLine("	}");
            output.WriteLine("	str += \"</div>\";");
            output.WriteLine("	this.rendered = true;");
            output.WriteLine("	return str;");
            output.WriteLine("};");

            /*
             * WebFXTreeItem class
             */

            output.WriteLine("function WebFXTreeItem(sText, sTag, sPath, sType, sAction, eParent, sIcon, sOpenIcon) {");
            output.WriteLine("	this.Tag = sTag;");
            output.WriteLine("	this.Path = sPath;");
            output.WriteLine("	this.Type = sType;");
            output.WriteLine("	this.base = WebFXTreeAbstractNode;");
            output.WriteLine("	this.base(sText, sAction);");
            output.WriteLine("	this.open = false;");
            output.WriteLine("	if (sIcon) { this.icon = sIcon; }");
            output.WriteLine("	if (sOpenIcon) { this.openIcon = sOpenIcon; }");
            output.WriteLine("	if (eParent) { eParent.add(this); }");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeItem.prototype = new WebFXTreeAbstractNode;");

            output.WriteLine("WebFXTreeItem.prototype.remove = function() {");
            output.WriteLine("	var iconSrc = document.getElementById(this.id + '-plus').src;");
            output.WriteLine("	var parentNode = this.parentNode;");
            output.WriteLine("	var prevSibling = this.getPreviousSibling(true);");
            output.WriteLine("	var nextSibling = this.getNextSibling(true);");
            output.WriteLine("	var folder = this.parentNode.folder;");
            output.WriteLine("	var last = ((nextSibling) && (nextSibling.parentNode) && (nextSibling.parentNode.id == parentNode.id))?false:true;");
            output.WriteLine("	this.getPreviousSibling().focus();");
            output.WriteLine("	this._remove();");
            output.WriteLine("	if (parentNode.childNodes.length == 0) {");
            output.WriteLine("		document.getElementById(parentNode.id + '-cont').style.display = 'none';");
            output.WriteLine("		parentNode.doCollapse();");
            output.WriteLine("		parentNode.folder = false;");
            output.WriteLine("		parentNode.open = false;");
            output.WriteLine("	}");
            output.WriteLine("	if (!nextSibling || last) { parentNode.indent(null, true, last, this._level, parentNode.childNodes.length); }");

            output.WriteLine("	if(webFXTreeHandler.behavior == 'classic'){");
            output.WriteLine("		if ((prevSibling == parentNode) && !(parentNode.childNodes.length)) {");
            output.WriteLine("			prevSibling.folder = false;");
            output.WriteLine("			prevSibling.open = false;");
            output.WriteLine("			iconSrc = document.getElementById(prevSibling.id + '-plus').src;");
            output.WriteLine("			iconSrc = iconSrc.replace('minus', '').replace('plus', '');");
            output.WriteLine("			document.getElementById(prevSibling.id + '-plus').src = iconSrc;");
            output.WriteLine("			document.getElementById(prevSibling.id + '-icon').src = webFXTreeConfig.fileIcon;");
            output.WriteLine("		}");
            output.WriteLine("		if (document.getElementById(prevSibling.id + '-plus')) {");
            output.WriteLine("			if (parentNode == prevSibling.parentNode) {");
            output.WriteLine("				iconSrc = iconSrc.replace('minus', '').replace('plus', '');");
            output.WriteLine("				document.getElementById(prevSibling.id + '-plus').src = iconSrc;");
            output.WriteLine("			}");
            output.WriteLine("		}");
            output.WriteLine("	}");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeItem.prototype._remove = function() {");
            output.WriteLine("	for (var i = this.childNodes.length - 1; i >= 0; i--) {");
            output.WriteLine("		this.childNodes[i]._remove();");
            output.WriteLine(" 	}");
            output.WriteLine("	for (var i = 0; i < this.parentNode.childNodes.length; i++) {");
            output.WriteLine("		if (this == this.parentNode.childNodes[i]) {");
            output.WriteLine("			for (var j = i; j < this.parentNode.childNodes.length; j++) {");
            output.WriteLine("				this.parentNode.childNodes[j] = this.parentNode.childNodes[j+1];");
            output.WriteLine("			}");
            output.WriteLine("			this.parentNode.childNodes.length -= 1;");
            output.WriteLine("			if (i + 1 == this.parentNode.childNodes.length) { this.parentNode._last = true; }");
            output.WriteLine("			break;");
            output.WriteLine("	}	}");
            output.WriteLine("	webFXTreeHandler.all[this.id] = null;");
            output.WriteLine("	var tmp = document.getElementById(this.id);");
            output.WriteLine("	if (tmp) { tmp.parentNode.removeChild(tmp); }");
            output.WriteLine("	tmp = document.getElementById(this.id + '-cont');");
            output.WriteLine("	if (tmp) { tmp.parentNode.removeChild(tmp); }");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeItem.prototype.expand = function() {");
            output.WriteLine("	this.doExpand();");
            output.WriteLine("	document.getElementById(this.id + '-plus').src = this.minusIcon;");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeItem.prototype.collapse = function(b) {");
            output.WriteLine("	if (!b) { this.focus(); }");
            output.WriteLine("	this.doCollapse();");
            output.WriteLine("	document.getElementById(this.id + '-plus').src = this.plusIcon;");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeItem.prototype.getFirst = function() {");
            output.WriteLine("	return this.childNodes[0];");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeItem.prototype.getLast = function() {");
            output.WriteLine("	if (this.childNodes[this.childNodes.length - 1].open) { return this.childNodes[this.childNodes.length - 1].getLast(); }");
            output.WriteLine("	else { return this.childNodes[this.childNodes.length - 1]; }");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeItem.prototype.getNextSibling = function() {");
            output.WriteLine("	for (var i = 0; i < this.parentNode.childNodes.length; i++) {");
            output.WriteLine("		if (this == this.parentNode.childNodes[i]) { break; }");
            output.WriteLine("	}");
            output.WriteLine("	if (++i == this.parentNode.childNodes.length) { return this.parentNode.getNextSibling(); }");
            output.WriteLine("	else { return this.parentNode.childNodes[i]; }");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeItem.prototype.getPreviousSibling = function(b) {");
            output.WriteLine("	for (var i = 0; i < this.parentNode.childNodes.length; i++) {");
            output.WriteLine("		if (this == this.parentNode.childNodes[i]) { break; }");
            output.WriteLine("	}");
            output.WriteLine("	if (i == 0) { return this.parentNode; }");
            output.WriteLine("	else {");
            output.WriteLine("		if ((this.parentNode.childNodes[--i].open) || (b && this.parentNode.childNodes[i].folder)) { return this.parentNode.childNodes[i].getLast(); }");
            output.WriteLine("		else { return this.parentNode.childNodes[i]; }");
            output.WriteLine("} }");

            output.WriteLine("WebFXTreeItem.prototype.keydown = function(key) {");
            output.WriteLine("	if ((key == 39) && (this.folder)) {");
            output.WriteLine("		if (!this.open) { this.expand(); }");
            output.WriteLine("		else { this.getFirst().select(); }");
            output.WriteLine("		return false;");
            output.WriteLine("	}");
            output.WriteLine("	else if (key == 37) {");
            output.WriteLine("		if (this.open) { this.collapse(); }");
            output.WriteLine("		else { this.parentNode.select(); }");
            output.WriteLine("		return false;");
            output.WriteLine("	}");
            output.WriteLine("	else if (key == 40) {");
            output.WriteLine("		if (this.open) { this.getFirst().select(); }");
            output.WriteLine("		else {");
            output.WriteLine("			var sib = this.getNextSibling();");
            output.WriteLine("			if (sib) { sib.select(); }");
            output.WriteLine("		}");
            output.WriteLine("		return false;");
            output.WriteLine("	}");
            output.WriteLine("	else if (key == 38) { this.getPreviousSibling().select(); return false; }");
            output.WriteLine("	return true;");
            output.WriteLine("}");

            output.WriteLine("WebFXTreeItem.prototype.toString = function (nItem, nItemCount) {");
            output.WriteLine("	var foo = this.parentNode;");
            output.WriteLine("	var indent = '';");
            output.WriteLine("	if (nItem + 1 == nItemCount) { this.parentNode._last = true; }");
            output.WriteLine("	var i = 0;");
            output.WriteLine("	while (foo.parentNode) {");
            output.WriteLine("		foo = foo.parentNode;");
            output.WriteLine("		indent = \"<img id='\" + this.id + \"-indent-\" + i + \"' src='\" + ((foo._last)?webFXTreeConfig.blankIcon:webFXTreeConfig.iIcon) + \"'>\" + indent;");
            output.WriteLine("		i++;");
            output.WriteLine("	}");
            output.WriteLine("	this._level = i;");
            output.WriteLine("	if (this.childNodes.length) { this.folder = 1; }");
            output.WriteLine("	else { this.open = false; }");
            output.WriteLine("	if ((this.folder) || (webFXTreeHandler.behavior != 'classic')) {");
            output.WriteLine("		if (!this.icon) { this.icon = webFXTreeConfig.folderIcon; }");
            output.WriteLine("		if (!this.openIcon) { this.openIcon = webFXTreeConfig.openFolderIcon; }");
            output.WriteLine("	}");
            output.WriteLine("	else if (!this.icon) { this.icon = webFXTreeConfig.fileIcon; }");
            output.WriteLine("	//var label = this.text.replace(/</g, '&lt;').replace(/>/g, '&gt;');");
            output.WriteLine("	var label = this.text;");
            output.WriteLine("	var str = \"<div id='\" + this.id + \"' ondblclick='webFXTreeHandler.toggle(this);' class='webfx-tree-item' onkeydown='return webFXTreeHandler.keydown(this, event)' ondragenter='dragEnter()' ondragleave='dragLeave()' ondragover='dragOver()' ondrop='webFXTreeHandler.dropme(this);FinishDrag(\" + this.Path + \");'>\";");
            output.WriteLine("	str += indent;");
            output.WriteLine("	str += \"<img id='\" + this.id + \"-plus' src='\" + ((this.folder)?((this.open)?((this.parentNode._last)?webFXTreeConfig.lMinusIcon:webFXTreeConfig.tMinusIcon):((this.parentNode._last)?webFXTreeConfig.lPlusIcon:webFXTreeConfig.tPlusIcon)):((this.parentNode._last)?webFXTreeConfig.lIcon:webFXTreeConfig.tIcon)) + \"' onclick='webFXTreeHandler.toggle(this);'>\";");
            output.WriteLine("	str += \"<img id='\" + this.id + \"-icon' class='webfx-tree-icon' src='\" + ((webFXTreeHandler.behavior == 'classic' && this.open)?this.openIcon:this.icon) + \"' onclick='webFXTreeHandler.focus(this.parentElement);' ondragstart='InitDrag()'><span id='\" + this.id + \"-anchor' onclick='webFXTreeHandler.focus(this);'  onMouseOver='IsonMouseOver(this);' onMouseOut='IsonMouseOut(this);' >\";");

            output.WriteLine("	if(this.Type == 1){");
            output.WriteLine("	str += \"<input type='checkbox' name='CheckID' id ='CheckID' value='\" + this.Path + \"'> \";");
            output.WriteLine("	label = label+\" <font color = green>( 可映射 )</font>\"};");

            output.WriteLine("	str += label + \"</span></div>\";");
            output.WriteLine("	str += \"<div id='\" + this.id + \"-cont' class='webfx-tree-container' style='display: \" + ((this.open)?'block':'none') + \";'>\";");
            output.WriteLine("	for (var i = 0; i < this.childNodes.length; i++) {");
            output.WriteLine("		str += this.childNodes[i].toString(i,this.childNodes.length);");
            output.WriteLine("	}");
            output.WriteLine("	str += \"</div>\";");
            output.WriteLine("	this.plusIcon = ((this.parentNode._last)?webFXTreeConfig.lPlusIcon:webFXTreeConfig.tPlusIcon);");
            output.WriteLine("	this.minusIcon = ((this.parentNode._last)?webFXTreeConfig.lMinusIcon:webFXTreeConfig.tMinusIcon);");
            output.WriteLine("	return str;");
            output.WriteLine("}");

            output.WriteLine("function IsonMouseOut($1) {");
            //output.WriteLine("alert($1);");
            //output.WriteLine("ExpandFolder = eval($1 + \"Folder\");");
            output.WriteLine("if ($1.className != \"selected\")");
            output.WriteLine("{");
            output.WriteLine("$1.className = \"item\";");
            output.WriteLine("}");
            output.WriteLine("}");

            output.WriteLine("function IsonMouseOver($1) {");
            //output.WriteLine("ExpandFolder = eval($1 + \"Folder\");");
            output.WriteLine("if ($1.className != \"selected\")");
            output.WriteLine("{");
            output.WriteLine("$1.className = \"itemselectOver\";");
            output.WriteLine("}");
            output.WriteLine("}");
            output.WriteLine("</script>");

            output.WriteLine("<script language=\"JavaScript\">");
            output.WriteLine("if (document.getElementById) {");
            output.WriteLine("var tree = new WebFXTree('站点根目录');");
            output.WriteLine("tree.setBehavior('explorer');");

            //			foreach(DataRowView drv in dv)
            //			{
           
            SqlDataReader reader = null;
            string sql = this._Sql;
            reader =Tools.DoSqlReader(sql);
            while (reader.Read())
            {
                output.WriteLine("var aNode=tree.add(new WebFXTreeItem(\"" + Tools.WebToDB(reader["TypeTree_CName"].ToString()) + "\",\"N\",\"" + reader["TypeTree_ID"].ToString() + "\",\"" + reader["TypeTree_Type"].ToString() + "\"));");

                if (_Type_TypeTree.HaveSon(int.Parse(reader["TypeTree_ID"].ToString()))) { output.WriteLine("aNode.add(new WebFXTreeItem(\"Loading\",\"Y\"));"); };
            }

            reader.Close();
            output.WriteLine("document.write(tree);}");
            output.WriteLine("function OpenFolder(path){");
            //output.WriteLine(this.Url +"\" + path + \"&defaultstatus="+this._Mode+"\";");
            //output.WriteLine("CurrentNode = path;");
            output.WriteLine("}</script>");
            this.Page.RegisterClientScriptBlock("clientScript", "");

        }
    }





    string ReturnXML(int Mode, int Path)
    {
        Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        //int TypeTree_ID = int.Parse(this.Request["TypeTree_ID"].ToString());
        int TypeTree_ID = Path;
        string sql = "";

        if (Mode == 1)
        {
            sql = "select * from Content_Type_TypeTree where TypeTree_ParentID = " + TypeTree_ID + " order by TypeTree_OrderNum";
        }
        if (Mode == 2)
        {
            sql = "SELECT Content_Type_TypeTree.* FROM Content_Type_TypeTree , Content_RolesConnect WHERE Content_RolesConnect.Roles_ID = " + int.Parse(Session["Roles"].ToString()) + " and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and Content_Type_TypeTree.TypeTree_ParentID= " + TypeTree_ID + " ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";
        }

        string HasSub = "";
        string StringXML = "";

        StringXML = StringXML + ("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        StringXML = StringXML + ("<folders>");


        SqlDataReader reader = null;
        reader =Tools.DoSqlReader(sql);
        while (reader.Read())
        {
            if (!this.Page.IsClientScriptBlockRegistered("clientScript"))
            {

                if (_Type_TypeTree.HaveSon(int.Parse(reader["TypeTree_ID"].ToString())))
                { HasSub = "yes"; }
                else
                { HasSub = "no"; };

                StringXML = StringXML + ("<folder name=\"&lt;font color=gray&gt;" + Tools.WebToDB(reader["TypeTree_CName"].ToString()) + "&lt;/font&gt;\" id=\"" + reader["TypeTree_ID"].ToString() + "\" hassubfolder=\"" + HasSub + "\"/>");
            }
        }
        StringXML = StringXML + ("</folders>");
        reader.Close();
        return StringXML;

    }

}
