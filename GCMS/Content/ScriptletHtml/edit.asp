<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<meta http-equiv="Content-Language" content="zh-cn">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>HTML编辑</title>

<!-- Styles -->
<link REL="stylesheet" TYPE="text/css" HREF="./Toolbars/toolbars.css">


<!-- Script Functions and Event Handlers -->
<script LANGUAGE="JavaScript" SRC="./Inc/dhtmled.js">
</script>

<!--cookie维护-->
<script LANGUAGE="JavaScript" SRC="./Inc/cookie.js">
</script>

<script ID="clientEventHandlersJS" LANGUAGE="javascript" SRC="edit.js">
</script>

<script LANGUAGE="javascript" FOR="tbContentElement" EVENT="DisplayChanged">
<!--
return tbContentElement_DisplayChanged()
//-->
</script>

<script LANGUAGE="javascript" FOR="tbContentElement" EVENT="ShowContextMenu">
<!--
return tbContentElement_ShowContextMenu()
//-->
</script>

<script LANGUAGE="javascript" FOR="tbContentElement" EVENT="ContextMenuAction(itemIndex)">
<!--
return tbContentElement_ContextMenuAction(itemIndex)
//-->
</script>
<SCRIPT LANGUAGE=javascript FOR=tbContentElement EVENT=DocumentComplete>
<!--
 tbContentElement_DocumentComplete()
//-->
</SCRIPT>

</head>
<body LANGUAGE="javascript" onload="return window_onload()">

<iframe ID="postFrame" src="postform.asp" style="width: 0; height: 0;"></iframe>



<!-- 工具条开始菜单 -->
<div class="tbToolbar" ID="MenuBar">
<div class="tbMenu" ID="FILE">
文件
<div class="tbMenuItem" ID="FILE_SAVE" LANGUAGE="javascript" onclick="return MENU_FILE_SAVE_onclick()">
保存
<img class="tbIcon" src="./images/save.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbSeparator"></div>
<div class="tbMenuItem" ID="FILE_EXIT" LANGUAGE="javascript" onclick="return MENU_FILE_EXIT_onclick()">
 关闭
</div>
</div> 

<div class="tbMenu" ID="EDIT" LANGUAGE="javascript" tbOnMenuShow="return OnMenuShow(QueryStatusEditMenu, EDIT)">
编辑
<div class="tbMenuItem" ID="EDIT_UNDO" LANGUAGE="javascript" onclick="return DECMD_UNDO_onclick()">
Undo
<img class="tbIcon" src="./images/undo.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="EDIT_REDO" LANGUAGE="javascript" onclick="return DECMD_REDO_onclick()">
Redo
<img class="tbIcon" src="./images/redo.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="EDIT_CUT" LANGUAGE="javascript" onclick="return DECMD_CUT_onclick()">
剪切
<img class="tbIcon" src="./images/cut.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="EDIT_COPY" LANGUAGE="javascript" onclick="return DECMD_COPY_onclick()">
拷贝
<img class="tbIcon" src="./images/copy.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="EDIT_PASTE" LANGUAGE="javascript" onclick="return DECMD_PASTE_onclick()">
粘贴
<img class="tbIcon" src="./images/paste.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="EDIT_DELETE" LANGUAGE="javascript" onclick="return DECMD_DELETE_onclick()">
删除
<img class="tbIcon" src="./images/delete.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="EDIT_SELECTALL" LANGUAGE="javascript" onclick="return DECMD_SELECTALL_onclick()">
全选
</div>

<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="EDIT_FINDTEXT" TITLE="Find" LANGUAGE="javascript" onclick="return DECMD_FINDTEXT_onclick()">
查找...
<img class="tbIcon" src="./images/find.gif" WIDTH="23" HEIGHT="22">
</div>
</div>

<div class="tbMenu" ID="VIEW">
查看
<div class="tbSubmenu" TBTYPE="toggle" ID="VIEW_TOOLBARS">
工具条
<div class="tbMenuItem" id="ToolbarMenuStd" TBTYPE="toggle" TBSTATE="checked" ID="TOOLBARS_STANDARD" TBTYPE="toggle" LANGUAGE="javascript" onclick="return TOOLBARS_onclick(StandardToolbar, ToolbarMenuStd)">
标准
</div>
<div class="tbMenuItem" id="ToolbarMenuFmt" TBTYPE="toggle" TBSTATE="checked" ID="TOOLBARS_FORMAT" TBTYPE="toggle" LANGUAGE="javascript" onclick="return TOOLBARS_onclick(FormatToolbar, ToolbarMenuFmt)">
格式
</div>
<div class="tbMenuItem" id="ToolbarMenuAbs" TBTYPE="toggle" TBSTATE="checked" ID="TOOLBARS_ZORDER" TBTYPE="toggle" LANGUAGE="javascript" onclick="return TOOLBARS_onclick(AbsolutePositioningToolbar, ToolbarMenuAbs)">
绝对定位
</div>
<div class="tbMenuItem" id="ToolbarMenuTable" TBTYPE="toggle" TBSTATE="checked" ID="TOOLBARS_TABLE" TBTYPE="toggle" LANGUAGE="javascript" onclick="return TOOLBARS_onclick(TableToolbar, ToolbarMenuTable)">
表格
</div>
</div>
</div> 

<div class="tbMenu" ID="FORMAT" LANGUAGE="javascript" tbOnMenuShow="return OnMenuShow(QueryStatusFormatMenu, FORMAT)">
格式
<div class="tbMenuItem" ID="FORMAT_FONT" LANGUAGE="javascript" onclick="return FORMAT_FONT_onclick()">
字体...
</div>

<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="FORMAT_BOLD" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_BOLD_onclick()">
粗体 
<img class="tbIcon" src="./images/bold.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="FORMAT_ITALIC" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_ITALIC_onclick()">
斜体
<img class="tbIcon" src="./images/italic.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="FORMAT_UNDERLINE" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_UNDERLINE_onclick()">
下划线
<img class="tbIcon" src="./images/under.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="FORMAT_SETFORECOLOR" LANGUAGE="javascript" onclick="return DECMD_SETFORECOLOR_onclick()">
前景色...
<img class="tbIcon" src="./images/fgcolor.GIF" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="FORMAT_SETBACKCOLOR" LANGUAGE="javascript" onclick="return DECMD_SETBACKCOLOR_onclick()">
背景色...
<img class="tbIcon" src="./images/bgcolor.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="FORMAT_JUSTIFYLEFT" TBTYPE="radio" NAME="Justify" LANGUAGE="javascript" onclick="return DECMD_JUSTIFYLEFT_onclick()">
左对齐
<img class="tbIcon" src="./images/left.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="FORMAT_JUSTIFYCENTER" TBTYPE="radio" NAME="Justify" LANGUAGE="javascript" onclick="return DECMD_JUSTIFYCENTER_onclick()">
居中
<img class="tbIcon" src="./images/center.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="FORMAT_JUSTIFYRIGHT" TBTYPE="radio" NAME="Justify" LANGUAGE="javascript" onclick="return DECMD_JUSTIFYRIGHT_onclick()">
右对齐
<img class="tbIcon" src="./images/right.gif" WIDTH="23" HEIGHT="22">
</div> 
</div> 

<div class="tbMenu" ID="HTML" LANGUAGE="javascript" tbOnMenuShow="return OnMenuShow(QueryStatusHTMLMenu, HTML)">
HTML
<div class="tbMenuItem" ID="HTML_HYPERLINK" LANGUAGE="javascript" onclick="return DECMD_HYPERLINK_onclick()">
超级连接...
<img class="tbIcon" src="./images/link.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbMenuItem" ID="HTML_IMAGE" LANGUAGE="javascript" onclick="return DECMD_IMAGE_onclick()">
图象...
<img class="tbIcon" src="./images/image.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbSubmenu" ID="HTML_INTRINSICS">
表单
<div class="tbMenuItem" ID="INTRINSICS_TEXTBOX" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;INPUT type=text&gt;')">
单行文本框
</div>
<div class="tbMenuItem" ID="INTRINSICS_PASSWRD" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;INPUT type=password&gt;')">
口令
</div>
<div class="tbMenuItem" ID="INTRINSICS_FILE" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;INPUT type=file&gt;')">
文件
</div>
<div class="tbMenuItem" ID="INTRINSICS_TEXTAREA" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;TEXTAREA rows=2 cols=20&gt;&lt;/TEXTAREA&gt;')">
多行文本框
</div>

<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="INTRINSICS_CHECKBOX" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;INPUT type=checkbox&gt;')">
检查框
</div>
<div class="tbMenuItem" ID="INTRINSICS_RADIO" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;INPUT type=radio&gt;')">
单选按扭
</div>
 
<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="INTRINSICS_DROPDOWN" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;SELECT&gt;&lt;/SELECT&gt;')">
下拉列表
</div>
<div class="tbMenuItem" ID="INTRINSICS_LISTBOX" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;SELECT size=2&gt;&lt;/SELECT&gt;')">
列表框
</div>
 
<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="INTRINSICS_BUTTON" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;INPUT type=button value=Button&gt;')">
按扭
</div>
<div class="tbMenuItem" ID="INTRINSICS_SUBMIT" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;INPUT type=submit value=Submit&gt;')">
提交按扭
</div>
<div class="tbMenuItem" ID="INTRINSICS_RESET" LANGUAGE="javascript" onclick="return INTRINSICS_onclick('&lt;INPUT type=reset value=Reset&gt;')">
重置按扭
</div>
</div>
</div>
<div class="tbGeneral" ID="URL_LABEL" TITLE="" style="top:6; width:100%; FONT-FAMILY: ms sans serif; FONT-SIZE: 10px;">
&nbsp;
</div>
</div>

<!-- 工具条开始标准 -->

<div class="tbToolbar" ID="StandardToolbar">
<div class="tbButton" ID="MENU_FILE_SAVE" TITLE="Save File" LANGUAGE="javascript" onclick="return MENU_FILE_SAVE_onclick()">
<img class="tbIcon" src="./images/save.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_CUT" TITLE="Cut" LANGUAGE="javascript" onclick="return DECMD_CUT_onclick()">
<img class="tbIcon" src="./images/cut.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_COPY" TITLE="Copy" LANGUAGE="javascript" onclick="return DECMD_COPY_onclick()">
<img class="tbIcon" src="./images/copy.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_PASTE" TITLE="Paste" LANGUAGE="javascript" onclick="return DECMD_PASTE_onclick()">
<img class="tbIcon" src="./images/paste.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_UNDO" TITLE="Undo" LANGUAGE="javascript" onclick="return DECMD_UNDO_onclick()">
<img class="tbIcon" src="./images/undo.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_REDO" TITLE="Redo" LANGUAGE="javascript" onclick="return DECMD_REDO_onclick()">
<img class="tbIcon" src="./images/redo.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_FINDTEXT" TITLE="Find" LANGUAGE="javascript" onclick="return DECMD_FINDTEXT_onclick()">
<img class="tbIcon" src="./images/find.gif" WIDTH="23" HEIGHT="22">
</div>
</div>


<!-- 工具条开始格式 -->

<div class="tbToolbar" ID="FormatToolbar">
<select ID="ParagraphStyle" class="tbGeneral" style="width:90" TITLE="Paragraph Format" LANGUAGE="javascript" onchange="return ParagraphStyle_onchange()">
<option value="&lt;P&gt;">Normal
<option value="&lt;H1&gt;">Heading 1
<option value="&lt;H2&gt;">Heading 2
<option value="&lt;H3&gt;">Heading 3
<option value="&lt;H4&gt;">Heading 4
<option value="&lt;H5&gt;">Heading 5
<option value="&lt;H6&gt;">Heading 6
<option value="&lt;A&gt;">Address
<option value="&lt;PRE&gt;">Formatted
</select>

<select ID="FontName" class="tbGeneral" style="width:140" TITLE="Font Name" LANGUAGE="javascript" onchange="return FontName_onchange()">
<option value="Arial">Arial
<option value="Tahoma">Tahoma
<option value="Courier New">Courier New
<option value="Times New Roman">Times New Roman
<option value="Wingdings">Wingdings
			<option VALUE="黑体">黑体
			<option VALUE="仿宋体">仿宋体
			<option VALUE="楷体">楷体
			<option VALUE="隶书">隶书
			<option VALUE="幼圆">幼圆
			<option VALUE="华文彩云">华文彩云
			<option VALUE="华文细黑">华文细黑
			<option VALUE="华文新魏">华文新魏
			<option VALUE="华文行楷">华文行楷
			<option VALUE="华文中宋">华文中宋
</select>

<select ID="FontSize" class="tbGeneral" style="width:40" TITLE="Font Size" LANGUAGE="javascript" onchange="return FontSize_onchange()">
<option value="1">1
<option value="2">2
<option value="3">3
<option value="4">4
<option value="5">5
<option value="6">6
<option value="7">7
</select>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_BOLD" TITLE="Bold" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_BOLD_onclick()">
<img class="tbIcon" src="./images/bold.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_ITALIC" TITLE="Italic" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_ITALIC_onclick()">
<img class="tbIcon" src="./images/italic.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_UNDERLINE" TITLE="Underline" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_UNDERLINE_onclick()">
<img class="tbIcon" src="./images/under.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_SETFORECOLOR" TITLE="Foreground Color" LANGUAGE="javascript" onclick="return DECMD_SETFORECOLOR_onclick()">
<img class="tbIcon" src="./images/fgcolor.GIF" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_SETBACKCOLOR" TITLE="Background Color" LANGUAGE="javascript" onclick="return DECMD_SETBACKCOLOR_onclick()">
<img class="tbIcon" src="./images/bgcolor.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_JUSTIFYLEFT" TITLE="Align Left" TBTYPE="toggle" NAME="Justify" LANGUAGE="javascript" onclick="return DECMD_JUSTIFYLEFT_onclick()">
<img class="tbIcon" src="./images/left.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_JUSTIFYCENTER" TITLE="Center" TBTYPE="toggle" NAME="Justify" LANGUAGE="javascript" onclick="return DECMD_JUSTIFYCENTER_onclick()">
<img class="tbIcon" src="./images/center.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_JUSTIFYRIGHT" TITLE="Align Right" TBTYPE="toggle" NAME="Justify" LANGUAGE="javascript" onclick="return DECMD_JUSTIFYRIGHT_onclick()">
<img class="tbIcon" src="./images/right.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_ORDERLIST" TITLE="Numbered List" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_ORDERLIST_onclick()">
<img class="tbIcon" src="./images/numlist.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_UNORDERLIST" TITLE="Bulletted List" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_UNORDERLIST_onclick()">
<img class="tbIcon" src="./images/bullist.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_OUTDENT" TITLE="Decrease Indent" LANGUAGE="javascript" onclick="return DECMD_OUTDENT_onclick()">
<img class="tbIcon" src="./images/deindent.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_INDENT" TITLE="Increase Indent" LANGUAGE="javascript" onclick="return DECMD_INDENT_onclick()">
<img class="tbIcon" src="./images/inindent.GIF" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_HYPERLINK" TITLE="连接" LANGUAGE="javascript" onclick="return DECMD_HYPERLINK_onclick()">
<img class="tbIcon" src="./images/link.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_IMAGE" TITLE="插入图片" LANGUAGE="javascript" onclick="return DECMD_IMAGE_onclick()">
<img class="tbIcon" src="./images/image.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_FLASH" TITLE="插入FLASH" LANGUAGE="javascript" onclick="return InsertFlash()">
<img class="tbIcon" src="./images/flash.gif" WIDTH="23" HEIGHT="22">
</div>
</div>

<!-- 工具条开始绝对定位 -->

<div class="tbToolbar" ID="AbsolutePositioningToolbar">
<div class="tbButton" ID="DECMD_ALIGNLEFT" TITLE="左环绕" LANGUAGE="javascript" onclick="return AlignLeft()">
<img class="tbIcon" src="./images/imageleft.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_ALIGNRIGHT" TITLE="右环绕" LANGUAGE="javascript" onclick="return AlignRight()">
<img class="tbIcon" src="./images/imageright.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_ALIGNNONE" TITLE="清除环绕" LANGUAGE="javascript" onclick="return AlignNone()">
<img class="tbIcon" src="./images/imagecenter.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_COPYRIGHT" TITLE="&copyright;" LANGUAGE="javascript" onclick="return InsertChar('&copy;')">
<img class="tbIcon" src="./images/copyright.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_TRADE" TITLE="&trade;" LANGUAGE="javascript" onclick="return InsertChar('&trade;')">
<img class="tbIcon" src="./images/trade.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_REGISTER" TITLE="&register;" LANGUAGE="javascript" onclick="return InsertChar('&reg;')">
<img class="tbIcon" src="./images/reg.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_VISIBLEBORDERS" TITLE="Visible Borders" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_VISIBLEBORDERS_onclick()">
<img class="tbIcon" src="./images/borders.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_SHOWDETAILS" TITLE="Show Details" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_SHOWDETAILS_onclick()">
<img class="tbIcon" src="./images/details.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_MAKE_ABSOLUTE" TBTYPE="toggle" LANGUAGE="javascript" TITLE="Make Absolute" onclick="return DECMD_MAKE_ABSOLUTE_onclick()">
<img class="tbIcon" src="./images/abspos.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_LOCK_ELEMENT" TBTYPE="toggle" LANGUAGE="javascript" TITLE="Lock" onclick="return DECMD_LOCK_ELEMENT_onclick()">
<img class="tbIcon" src="./images/lock.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbMenu" ID="ZORDER" LANGUAGE="javascript" tbOnMenuShow="return OnMenuShow(QueryStatusZOrderMenu, ZORDER)">
Z Order
<div class="tbMenuItem" ID="ZORDER_BRINGFRONT" LANGUAGE="javascript" onclick="return ZORDER_BRINGFRONT_onclick()">
Bring to Front
</div>
<div class="tbMenuItem" ID="ZORDER_SENDBACK" LANGUAGE="javascript" onclick="return ZORDER_SENDBACK_onclick()">
Send to Back
</div>
 
<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="ZORDER_BRINGFORWARD" LANGUAGE="javascript" onclick="return ZORDER_BRINGFORWARD_onclick()">
Bring Forward
</div>
<div class="tbMenuItem" ID="ZORDER_SENDBACKWARD" LANGUAGE="javascript" onclick="return ZORDER_SENDBACKWARD_onclick()">
Send Backward
</div>
 
<div class="tbSeparator"></div>

<div class="tbMenuItem" ID="ZORDER_BELOWTEXT" LANGUAGE="javascript" onclick="return ZORDER_BELOWTEXT_onclick()">
Below Text
</div>
<div class="tbMenuItem" ID="ZORDER_ABOVETEXT" LANGUAGE="javascript" onclick="return ZORDER_ABOVETEXT_onclick()">
Above Text
</div>
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_SNAPTOGRID" TITLE="Snap to Grid" TBTYPE="toggle" LANGUAGE="javascript" onclick="return DECMD_SNAPTOGRID_onclick()">
<img class="tbIcon" src="./images/snapgrid.gif" WIDTH="23" HEIGHT="22">
</div>
</div>

<!-- 工具条开始表格 -->

<div class="tbToolbar" ID="TableToolbar">
<div class="tbButton" ID="DECMD_INSERTTABLE" TITLE="Insert Table" LANGUAGE="javascript" onclick="return TABLE_INSERTTABLE_onclick()">
<img class="tbIcon" src="./images/instable.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_INSERTROW" TITLE="Insert Row" LANGUAGE="javascript" onclick="return TABLE_INSERTROW_onclick()">
<img class="tbIcon" src="./images/insrow.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_DELETEROWS" TITLE="Delete Rows" LANGUAGE="javascript" onclick="return TABLE_DELETEROW_onclick()">
<img class="tbIcon" src="./images/delrow.gif" WIDTH="23" HEIGHT="22">
</div>
 
<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_INSERTCOL" TITLE="Insert Column" LANGUAGE="javascript" onclick="return TABLE_INSERTCOL_onclick()">
<img class="tbIcon" src="./images/inscol.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_DELETECOLS" TITLE="Delete Columns" LANGUAGE="javascript" onclick="return TABLE_DELETECOL_onclick()">
<img class="tbIcon" src="./images/delcol.gif" WIDTH="23" HEIGHT="22">
</div>

<div class="tbSeparator"></div>

<div class="tbButton" ID="DECMD_INSERTCELL" TITLE="Insert Cell" LANGUAGE="javascript" onclick="return TABLE_INSERTCELL_onclick()">
<img class="tbIcon" src="./images/inscell.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_DELETECELLS" TITLE="Delete Cells" LANGUAGE="javascript" onclick="return TABLE_DELETECELL_onclick()">
<img class="tbIcon" src="./images/delcell.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_MERGECELLS" TITLE="Merge Cells" LANGUAGE="javascript" onclick="return TABLE_MERGECELL_onclick()">
<img class="tbIcon" src="./images/mrgcell.gif" WIDTH="23" HEIGHT="22">
</div>
<div class="tbButton" ID="DECMD_SPLITCELL" TITLE="Split Cells" LANGUAGE="javascript" onclick="return TABLE_SPLITCELL_onclick()">
<img class="tbIcon" src="./images/spltcell.gif" WIDTH="23" HEIGHT="22">
</div>
</div>

<!-- DHTML Editing control Object. This will be the body object for the toolbars. -->
<object ID="tbContentElement" CLASS="tbContentElement" CLASSID="clsid:2D360201-FFF5-11d1-8D03-00A0C959BC0A" VIEWASTEXT>
<param name=Scrollbars value=true>
</object>

<!-- DEInsertTableParam Object -->
<object ID="ObjTableInfo" CLASSID="clsid:47B0DFC7-B7A3-11D1-ADC5-006008A5848C" VIEWASTEXT>
</object>

<!-- DEGetBlockFmtNamesParam Object -->
<object ID="ObjBlockFormatInfo" CLASSID="clsid:8D91090E-B955-11D1-ADC5-006008A5848C" VIEWASTEXT>
</object>

<!-- Toolbar Code File. Note: This must always be the last thing on the page -->
<script LANGUAGE="Javascript" SRC="./Toolbars/toolbars.js">
</script>
<script LANGUAGE="Javascript">
tbScriptletDefinitionFile = "./Toolbars/menubody.htm";
</script>
<script LANGUAGE="Javascript" SRC="./Toolbars/tbmenus.js">
</script>
</body>
</html>
