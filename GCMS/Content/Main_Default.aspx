<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main_Default.aspx.cs" Inherits="Content_Main_Default" %>


<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<WebAppControls:Tools_Head id="Tools_Head" runat="server"></WebAppControls:Tools_Head>
		<link type="text/css" rel="stylesheet" href="../admin_Public/Css/xtree.css">

	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
					<asp:PlaceHolder id="ContentMain" runat="server"></asp:PlaceHolder>
		</form>
	</body>
</html>
