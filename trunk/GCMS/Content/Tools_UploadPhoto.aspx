<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tools_UploadPhoto.aspx.cs" Inherits="Content_Tools_UploadPhoto" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Tools_UploadPhoto</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
		<script language="javascript">
var isBusy=false;
function checkinput(){
  if (isBusy){alert("系统正在处理，请稍安勿燥！");return false;}
  isBusy = true;
  document.body.style.cursor="wait";
  if (document.form1.file1.value=="" && document.form1.file2.value=="" && document.form1.file3.value=="" && document.form1.file4.value=="" && document.form1.file5.value=="" && document.form1.file6.value==""){
  	alert("至少上传一个文件！");
  	document.form1.file1.focus();
        document.body.style.cursor="default";
	isBusy = false;
  	return false;
  	}
  return true;
}
function getExtname(filename){
	var pos = filename.lastIndexOf("\\");
	return (filename.substr(pos+1));
}
function doPreview(){
	if (document.form1.Thumbnail.checked){
		var ihtml="";
		var objfile;
		for(var i=1;i<7;i++){
		objfile = eval("document.form1.file" + i);
		if (objfile.value!=""){
		ihtml = ihtml + "<a href=\"" + objfile.value;
		ihtml = ihtml + "\" target=\"" + document.form1.ThumbnailTarget.value;
		ihtml = ihtml + "\" alt=\"" + document.form1.ThumbnailAlt.value;
		ihtml = ihtml + "\"><img border=0 width=\"" + document.form1.ThumbnailWidth.value;
		ihtml = ihtml + "\" height=\"" + document.form1.ThumbnailHeight.value;
		ihtml = ihtml + "\" src=\"" + objfile.value + "\"></a><br>" + getExtname(objfile.value) + "<br><br>";
		}
		}
		Preview.innerHTML = "<center>" + ihtml + "</center>";
	}
	else{
		var ihtml="";
		var objfile;
		for(var i=1;i<7;i++){
		objfile = eval("document.form1.file" + i);
		if (objfile.value!=""){
		ihtml = ihtml + "<img src=\"" + objfile.value + "\"><br>" + getExtname(objfile.value) + "<br><br>";
		}
		}
		Preview.innerHTML = "<center>" + ihtml + "</center>";
	}
}
function ToggleThumb(){
	if (document.form1.Thumbnail.checked){
		ThumbnailProperty.style.display="block";
	}
	else{
		ThumbnailProperty.style.display="none";
	}
	doPreview();
}
function ToggleMore(){
	if (document.form1.MoreUpload.checked){
		MoreFile.style.display="block";
	}
	else{
		MoreFile.style.display="none";
	}
}
function ToggleWM(){
	if (document.form1.WaterMark.checked){
		WM.style.display="block";
	}
	else{
		WM.style.display="none";
	}
}
function togglelibrary(){
  if (document.all("imagelibrary")[1].checked){
    document.form2.submit();
  }
}


		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="form1" method="post" runat="server">
			<table class="DialogTab" width="100%" align="center" border="0">
				<tr>
					<td vAlign="top" width="60%">请选择文件：<br>
						<INPUT id="file1" type="file" onchange="doPreview();" size="30" name="file1" runat="server"><br>
						<div id="MoreFile" style="DISPLAY: none"><INPUT id="file2" type="file" onchange="doPreview();" size="30" name="file2" runat="server"><br>
							<INPUT id="file3" type="file" onchange="doPreview();" size="30" name="file3" runat="server"><br>
							<INPUT id="file4" type="file" onchange="doPreview();" size="30" name="file4" runat="server"><br>
							<INPUT id="file5" type="file" onchange="doPreview();" size="30" name="file5" runat="server"><br>
							<INPUT id="file6" type="file" onchange="doPreview();" size="30" name="file6" runat="server"><br>
						</div>
						<input id="Thumbnail" onclick="ToggleThumb();" type="checkbox" value="Yes" name="Thumbnail"
							runat="server">生成缩略图 <input id="MoreUpload" onclick="ToggleMore();" type="checkbox" value="Yes" name="MoreUpload"
							runat="server">多图上传 
						<!--<input type=checkbox name="WaterMark" onclick="ToggleWM();" value="Yes">加水印--><br>
						<div id="ThumbnailProperty" style="DISPLAY: none">宽度：<input id="ThumbnailWidth" type="text" onchange="doPreview();" value="140" name="ThumbnailWidth"
								runat="server"><br>
							高度：<input id="ThumbnailHeight" type="text" onchange="doPreview();" value="100" name="ThumbnailHeight"
								runat="server"><br>
							文本：<input id="ThumbnailAlt" type="text" value="点击看大图" name="ThumbnailAlt" runat="server"><br>
							目标：<input id="ThumbnailTarget" type="text" value="_blank" name="ThumbnailTarget" runat="server">&nbsp;&nbsp;
						</div>
						<div id="WM" style="DISPLAY: none">水印文字：<input type="text" name="WMText"><br>
							水印位置：<input type="radio" value="1" name="WMPosition">上<input type="radio" CHECKED value="16" name="WMPosition">居中<input type="radio" value="256" name="WMPosition">下<br>
						</div>
					</td>
					<td style="BORDER-RIGHT: 1px solid; BORDER-TOP: buttonshadow 1px solid; BORDER-LEFT: buttonshadow 1px solid; BORDER-BOTTOM: 1px solid; HEIGHT: 300px"
						vAlign="top" width="40%">预览:<br>
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 312px">
							<div id="Preview" style="OVERFLOW: visible"><FONT face="宋体"></FONT></div>
						</div>
					</td>
				</tr>
				<tr>
					<td colSpan="2">
						<center><asp:button id="btnAttach" runat="server" CssClass="button" Width="64px" Text="上传" OnClick="btnAttach_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="btnCancel" runat="server" CssClass="button" Width="64px" Text="返回"></asp:button><input type="hidden" name="contentid">
						</center>
						<P>
							<br>
							&nbsp;</P>
						<P><FONT face="宋体"></FONT>
							<br>
							&nbsp;</P>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
