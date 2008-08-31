<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProgressWinodowFrame.aspx.cs" Inherits="Content_ProgressWinodowFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
    <Script language="javascript">
    function windowclose(){
	    window.close();
    }
    </Script>
</head>
<body leftmargin="0" topmargin="0" oncontextmenu="return false;">
    <form id="form1" runat="server" >
   <table width="100%" height="100%" border="1" class="DialogTab">
  <tr>
    <td height="64" valign="top"><br/>
      <strong>请稍候,您选择的命令正在执行:</strong><br/> <br/> 
      <table width="400" height="15" border="1" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td >
		 <div style="height: 10px;width: 0px;border: 0px none;background-color: #FF9900;MARGIN: 1px 1px 1px 1px;text-align: left;" id="progress">
		</div>
		  </td>
        </tr>
      </table>
      <br/> <span id="pstr"></span> </td>
  </tr>
</table>

    </form>
</body>
</html>
<script language="javascript">
try{
  if (window.dialogArguments != null && window.dialogArguments!=""){
    document.title = "───────────────" + window.dialogArguments + "────────────────────";
  }
  else
    document.title = "───────────────GCMS────────────────────";
}catch(exception){
    document.title = "───────────────GCMS────────────────────";
    }
</script>