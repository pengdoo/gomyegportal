<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_TypeAddFields.aspx.cs" Inherits="Content_Type_TypeAddFields" %>

<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Type_AddFields</title>
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
			
		
		function selectTree(el,ID){
			var args="font-size:10px;dialogWidth:286px;dialogHeight:290px;center:yes;status:no;help:no";
			var argu=new Array();
			argu[0]="";
			var selectdate=window.showModalDialog("Tools_SelectTreeID.aspx?TypeTree_ID="+ID,argu,args);
			if (selectdate!=null)
			Form1.Trees1.value =selectdate;
			}
			
			
			
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<table style="WIDTH: 100%" border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<TR>
						<TD style="HEIGHT: 12px" vAlign="top" align="left">
							<table class="table" style="WIDTH: 100%">
								<TR>
									<th noWrap align="left" height="20">
										&nbsp;<asp:label id="Label1" runat="server"></asp:label></th></TR>
							</table>
							<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
								<tr>
									<TD width="5"><SPAN class="handbtn"></SPAN></TD>
									<cc3:Toolsbar id="Toolsbar1" runat="server" OnButtonClick="Toolsbar1_ButtonClick" AltText="保  存" Text="保  存" Width="55" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"></cc3:Toolsbar>
									<TD></TD>
								</tr>
							</table>
						</TD>
					</TR>
					<TR>
						<td>
							<table class="table" style="WIDTH: 100%">
								<TR>
									<th noWrap align="left" height="19">
										&nbsp;
										<asp:label id="Mssg" runat="server" ForeColor="#FF8000"></asp:label></th></TR>
							</table>
						</td>
					</TR>
				</TBODY>
			</table>

			<asp:TextBox id="Trees1" CssClass='inputtext' runat="server"></asp:TextBox>
			<img src='../Admin_Public/Images/RepeatedRegion.gif' onclick='selectTree("Trees1","-1");'>
			
		</FORM>
		
	</body>
</HTML>

