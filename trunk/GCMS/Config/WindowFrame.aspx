<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WindowFrame.aspx.cs" Inherits="Content_WindowFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
     <link href="../Admin_Public/css/Admin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
function windowclose(){
	window.close();
}
</script>
</head>
<body >
    <form id="form1" runat="server">

    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
try{
  if (window.dialogArguments != null && window.dialogArguments!=""){
    document.title = "───────────────" + window.dialogArguments + "─1───────────────────";
  }
  else
    document.title = "───────────────GCMS────────────────────";
}catch(exception){
    document.title = "───────────────GCMS────────────────────";
    }
</script>