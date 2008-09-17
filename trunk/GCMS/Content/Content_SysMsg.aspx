<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content_SysMsg.aspx.cs" Inherits="Content_Content_SysMsg" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Type_Add</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<script language="javascript">
			function closethiswindows()
			{					
				top.window.close();
			}
			
			//命名规则
			function btn_URL() 
			{
				var oldValue = document.Form1.TypeTree_URL.value;
				var argu = "dialogWidth:37em; dialogHeight:18em;center:yes;status:no;help:no";
				var newValue = window.showModalDialog("Type_SelectURL.aspx?oldValue=" + oldValue,null,argu);
				if (newValue!=null) {
				document.Form1.TypeTree_URL.value=newValue;}
			}

			//-->

		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE class="DialogTab" id="Table1" style="WIDTH: 100%" height="100%" border="0">
				<TR>
					<td vAlign="top" height="100%">
					<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 80%">
						<P>
						</P>
						<P align="center">
							<asp:Label id="Label1" runat="server">Label</asp:Label>
						</P>
</div>
						<P align="center"><INPUT type="button" value="取 消" onclick="closethiswindows()"> &nbsp;
							<INPUT type="button" value="确 认" id="Button1" name="Button1" runat="server" onserverclick="Button1_ServerClick">
						</P>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>