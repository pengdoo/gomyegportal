<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main_Content.aspx.cs" Inherits="Content_Main_Content" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_PageHeader" Src="../Gomye_Tools/Tools_PageHeader.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<WEBAPPCONTROLS:Tools_Head id="Tools_Head" runat="server"></WEBAPPCONTROLS:Tools_Head>
	</HEAD>
	<body oncontextmenu="self.event.returnValue=false" onselectstart="event.returnValue=false"
		leftMargin="0" topMargin="0" scroll="no" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<asp:PlaceHolder id="ContentRight" runat="server"></asp:PlaceHolder>
		</form>
	</body>
</html>
