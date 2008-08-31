<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Delete.aspx.cs" Inherits="Content_User_Delete" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<script language="javascript">
    function Refresh()
    {
		//top.location.reload();
		window.location.href = "User_View.aspx";
    }
    
    function fMsg(sMsg)
	{
		alert(sMsg);
		window.location.href = "User_View.aspx";
	}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体"></FONT>
		</form>
	</body>
</HTML>
