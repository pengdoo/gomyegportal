<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_SelectURL.aspx.cs" Inherits="Content_Type_SelectURL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
<link href="../Admin_Public/css/Admin.css" rel="stylesheet" type="text/css" />
<title>������������</title>

<script type="text/javascript" language="javascript">
function calc(){
	var newValue = document.form1.startpoint.value;
	var options = document.all["subdir"];
	for (i=0;i<options.length;i++){
		if (options[i].checked){
			var nv=options[i].value;
			if (nv!="nosubdir")
			  newValue=newValue + nv + "/";
			}
		}
	newValue = newValue + "{@UID}" + document.form1.extname.value;
	document.form1.namemethod.value = newValue;
}
function closeWindow(){
	if (document.form1.namemethod.value!=""){window.returnValue=document.form1.namemethod.value;}
	window.close();
}

function selectDir(){
		var argu = "dialogWidth:34em; dialogHeight:27em;center:yes;status:no;help:no";
		var file = window.showModalDialog("WindowFrame.aspx?loadfile=Tools_FileMain.aspx",null,argu);
		if (file!=null) {
		  document.form1.startpoint.value=file;
		  }
		calc();
}

</script>
</head>

<body oncontextmenu="return false;">

<div align="center">
  <center>
<form name="form1" action="">
<table border="0" cellspacing="1" cellpadding="3" class="table">
  <tr>         
    <td align="right">��ʼ�㣺</td>
    <td ><input type="text" runat="server" name="startpoint"  size="41" onblur="calc();" id="startpoint" >
    <input type="button" value="ѡ��..." name="B5" onclick="selectDir();" class="button"></td>              
  </tr>              
  <tr>         
    <td  align="right" valign="top">��Ŀ¼�������ԣ�</td>
    <td ><input type="radio" value="nosubdir" checked name="subdir">����������Ŀ¼<br/>
      <input type="radio" name="subdir" value="{@year}" onblur="calc();"/>����֣�ÿ��һ��Ŀ¼���磺  
      /2001/<br/>
      <input type="radio" name="subdir" value="{@year}/{@month}" onblur="calc();"/>�����·֣�ÿ��һ��Ŀ¼���磺  
      /2001/12/<br/>
      <input type="radio" name="subdir" value="{@year}/{@month}/{@day}" onblur="calc();"/>�������շ֣�ÿ��һ��Ŀ¼���磺  
      /2001/12/1/<br/>
      <input type="radio" name="subdir" value="{@date}" onblur="calc();"/>���շ֣�ÿ��һ��Ŀ¼���磺  
      /2001_12_1/<br/>
      <input type="radio" name="subdir" value="{@author}" onblur="calc();"/>�����߷֣�ÿ������һ��Ŀ¼���磺  
      /Alan/</td>              
  </tr> 
  <tr>
    <td align="right">�ļ���׺����</td>
    <td>
    <input type="text" name="extname"  size="47" onblur="calc();" id="extname" runat="server"/>
    </td>              
  </tr>              

  <tr>         
    <td  align="right">��������</td>
    <td ><input type="text" name="namemethod"  size="47" id="namemethod" runat="server"/></td>              
  </tr>              
    <tr>              
    <td  colspan="2">            
<p align="right">           
<input type="button" value=" ȷ �� " name="B3" onclick="closeWindow();" class="button"/>&nbsp;&nbsp;&nbsp; 
<input type="button" value=" ȡ �� " name="B4" onclick="window.close();" class="button"/>           
 ��</p>        
    </td> 
    </tr>
    </table>
      </form>
        </center>
</div>
</body>
</html>