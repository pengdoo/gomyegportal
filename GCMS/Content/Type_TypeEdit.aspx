<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_TypeEdit.aspx.cs" Inherits="Content_Type_TypeEdit" %>
<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TypeTree_edit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<script language="javascript">
			function AddFields()
			{
				var argu = "dialogWidth:32em; dialogHeight:32em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Type_TypeAddField.aspx&OrderType=Create&txtType="+Form1.txtType.value+"&TypeTree_ID="+Form1.txtTypeTree_ID.value,"字段定义",argu);
				window.location.href = "Type_TypeEdit.aspx?Type="+Form1.txtType.value+"&TypeTree_ID=" + Form1.txtTypeTree_ID.value;
			}
					
			function UpdataFields(Property_ID,TypeTree_ID)
			{ 
				var argu = "dialogWidth:32em; dialogHeight:32em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Type_TypeAddField.aspx&OrderType=Updata&Property_ID="+Property_ID+"&TypeTree_ID="+TypeTree_ID,"更新字段",argu);		
				window.location.href = "Type_TypeEdit.aspx?TypeTree_ID=" + TypeTree_ID;
			}
					
			function DeleteFields(Property_ID,TypeTree_ID)
			{
				if(confirm("确定要删除此字段吗？此操作是无法恢复！！"))
				{					
					var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
					window.showModalDialog("WindowFrame.aspx?loadfile=Type_TypeAddField.aspx&OrderType=Delete&Property_ID="+Property_ID+"&TypeTree_ID="+TypeTree_ID,"删除字段",argu);		
					window.location.href = "Type_TypeEdit.aspx?TypeTree_ID=" + TypeTree_ID;
				}

			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="table" style="WIDTH: 100%">
				<TR>
					<th noWrap align="left" height="20">
						&nbsp;<asp:label id="Label1" runat="server"></asp:label></th></TR>
			</table>
			<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<cc3:Toolsbar id="Toolsbar1" runat="server"  OnButtonClick="Toolsbar1_ButtonClick" AltText="保存显示方式" Text="保存显示方式" Width="99" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"></cc3:Toolsbar>
					<td class="coolButton" onClick="AddFields();" title="选择扩展字段" width="99" height="20">
						<img src="../Admin_Public/Images/Icon_File_Save.gif"> 选择扩展字段</td>
					<cc3:Toolsbar id="Toolsbar2" OnButtonClick="Toolsbar2_ButtonClick" runat="server" AltText="应用到子目录" Text="应用到子目录" Width="99" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"></cc3:Toolsbar>
					<cc3:Toolsbar id="Toolsbar3" OnButtonClick="Toolsbar3_ButtonClick" runat="server" AltText="应用到同级目录" Text="应用到同级目录" Width="105" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"></cc3:Toolsbar>
					<TD><FONT face="宋体"></FONT></TD>
				</tr>
			</table>
			<table cellspacing="0" cellpadding="0" width="100%" height="90%">
				<tr>
					<td vAlign="top">
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 100%">
							<TABLE class="coolBar" style="TABLE-LAYOUT: fixed; WIDTH: 100%">
								<asp:Label id="AddFieldsWrite" runat="server"></asp:Label>
							</TABLE>
						</div>
						<DIV class="DivListView" id="scrollDiv" onclick="unselect();" align="center">
							<SCRIPT language="javascript">
	window.onresize=fixSize;
	fixSize();

	function fixSize(){
		scrollDiv.style.height=Math.max(document.body.clientHeight-100,0);
	}
							</SCRIPT>
							<DIV class="listView" id="ContentList" onkeyup="ContentList_OnKeyup();" align="center">
								<asp:datagrid id="DateGridList" runat="server" HeaderStyle-CssClass="headerTable" GridLines="None"
									Width="100%" HorizontalAlign="Center" DataKeyField="Content_ID" AutoGenerateColumns="False"
									PageSize="30" AllowPaging="True" AllowSorting="True">
									<Columns>
										<asp:TemplateColumn SortExpression="Content_ID" HeaderText="ID">
											<HeaderStyle CssClass="id"></HeaderStyle>
										</asp:TemplateColumn>
										<asp:BoundColumn SortExpression="title" HeaderText="名称">
											<HeaderStyle CssClass="title"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn SortExpression="author" HeaderText="作者">
											<HeaderStyle CssClass="author"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn SortExpression="submitdate" HeaderText="发布时间">
											<HeaderStyle CssClass="submitdate"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn SortExpression="status" HeaderText="状态">
											<HeaderStyle CssClass="status"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn HeaderText="头条">
											<HeaderStyle CssClass="putintopx"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn HeaderText="图文">
											<HeaderStyle CssClass="isimagenews"></HeaderStyle>
										</asp:BoundColumn>
									</Columns>
									<PagerStyle Visible="False" PageButtonCount="6"></PagerStyle>
								</asp:datagrid></DIV>
						</DIV>
					</td>
				</tr>
			</table>
			<INPUT id="txtType" type="hidden" runat="server" name="txtType"> <INPUT id="txtTypeTree_ID" type="hidden" runat="server" name="txtTypeTree_ID">
		</form>
	</body>
</HTML>
