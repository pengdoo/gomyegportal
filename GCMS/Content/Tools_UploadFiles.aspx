<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tools_UploadFiles.aspx.cs" Inherits="Content_Tools_UploadFiles" %>

<HTML>
	<HEAD>
		<title>Tools_UploadFiles</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="DialogTab" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD vAlign="top"><FONT face="宋体"></FONT>
						<br/>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 请选择文件：
						<asp:Label id="Label1" runat="server" Width="168px" ForeColor="#FF8000"></asp:Label>
						<br/>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <INPUT TYPE="file" NAME="file1" size="30" class="inputtext250" id="File1" runat="server">
						<br/>
						<br/>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<INPUT TYPE="submit" name="upload" VALUE=" 确 认 " class="button" id="Submit1" runat="server" onserverclick="Submit1_ServerClick">&nbsp;
						<input type="button" value=" 取 消 " name="B1" onclick="top.close();" class="button">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
