<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Setup_View.aspx.cs" Inherits="Member_Setup_View" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_PageHeader" Src="../Gomye_Tools/Tools_PageHeader.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Setup_View</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<WEBAPPCONTROLS:TOOLS_HEAD id="Tools_Head" Value="" MenuStatus="3" runat="server"></WEBAPPCONTROLS:TOOLS_HEAD>
	</HEAD>
	<body leftMargin="0" topMargin="0" scroll="no" MS_POSITIONING="GridLayout">
		<WEBAPPCONTROLS:TOOLS_PAGEHEADER id="PageHeader" Value="常规设置" runat="server" mod="3"></WEBAPPCONTROLS:TOOLS_PAGEHEADER>
		<form id="Form1" method="post" runat="server">
			<TABLE>
				<TR>
					<TD>注册特殊字符屏蔽</TD>
					<TD><asp:textbox id="BadWords" runat="server" Width="352px" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 21px"></TD>
					<TD style="HEIGHT: 21px"><FONT style="COLOR: red">例如：特殊|字符</FONT></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:button id="Button1" runat="server" Text="确 认"></asp:button></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
