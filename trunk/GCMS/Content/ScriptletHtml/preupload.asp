
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<link rel="STYLESHEET" type="text/css" href="../mall/scriptlet/edit.css">
<script language="javascript">
function checkinput(){
  if (document.form1.pfilename.value==""){
  	alert("�ļ�·������Ϊ�գ�");
  	return false;
  	}
  return true;
}
</script>
</head>

<body topmargin="0" leftmargin="0">
<FORM ACTION="upload.asp" ENCTYPE="multipart/form-data" METHOD="POST" onsubmit="return checkinput();" name="form1">
<br/>
<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��ѡ���ļ���<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<INPUT TYPE=FILE NAME="pfilename" size="30">
<br/>
<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<INPUT TYPE="SUBMIT" name="upload" VALUE = " ȷ �� ">&nbsp; <input type="button" value=" ȡ �� " name="B1" onclick="top.close();"> 
</FORM> 
</body> 
 
</html> 
