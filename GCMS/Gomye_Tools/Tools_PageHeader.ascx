<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tools_PageHeader.ascx.cs" Inherits="Gomye_Tools_Tools_PageHeader" %>
<script language="JavaScript" src="../Admin_Public/js/coolbuttons.js"></script>
<script language="javascript">
	function Leftswitch(){
	if (LeftbuttonFont.innerText=="3"){
		//parent.parent.parent.Content.Mainframe.cols = "0,250,*";
		parent.parent.parent.Content.Mainframe.Height = "0";
		Tools_Leftframe.cols = "0";
		LeftbuttonFont.innerText="4";
		Leftbutton.title="打开工具";
		LeftbuttonFont.className="Siconselected";
	}
	else{
		parent.parent.parent.Content.Mainframe.cols = "80,250,*";
		LeftbuttonFont.innerText="3";
		Leftbutton.title="关闭工具";
		LeftbuttonFont.className="coolButton";
	}
	}
	
	function Headswitch(){
	if (HeadbuttonFont.innerText=="3"){
		parent.parent.parent.Content.Mainframe.cols = "80,0,*";
		HeadbuttonFont.innerText="4";
		Headbutton.title="打开栏目";
		HeadbuttonFont.className="Siconselected";
	}
	else{
		parent.parent.parent.Content.Mainframe.cols = "80,250,*";
		HeadbuttonFont.innerText="3";
		Headbutton.title="关闭栏目";
		HeadbuttonFont.className="coolButton";

	}
	}
</script>
<asp:Panel id="LeMain" runat="server">
	<TABLE class="coolBar" height="20" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<TD noWrap align="left">&nbsp;栏 目
			</TD>
			<TD valign="middle" width="10"><SPAN class="coolButton" style="WIDTH: 10px; HEIGHT: 10px" onclick="parent.Mainframe.cols = '0,*';"><IMG src="../Admin_Public/Images/close.gif"></SPAN>
			</TD>
		</tr>
	</TABLE>
</asp:Panel>
<asp:Panel id="LeView" runat="server">
	<TABLE class="coolBar" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="100%"
		border="0">
		<tr>
			<TD noWrap align="left">&nbsp;
				<asp:Label id="LabTitleText" runat="server">管理界面</asp:Label>
			</TD>
			<TD id="Leftbutton" title="关闭导航" style="WIDTH: 18px" onclick="Leftswitch();" align="center"><FONT style="FONT-SIZE: 7pt" face="Marlett"><SPAN class="coolButton" id="LeftbuttonFont" style="WIDTH: 8px; HEIGHT: 8px">3</SPAN></FONT>
			</TD>
			<TD id="Headbutton" title="关闭工具" style="WIDTH: 18px" onclick="Headswitch();" align="center"><FONT style="FONT-SIZE: 7pt" face="Marlett"><SPAN class="coolButton" id="HeadbuttonFont" style="WIDTH: 8px; HEIGHT: 8px">3</SPAN></FONT>
			</TD>
		</tr>
	</TABLE>
</asp:Panel>
