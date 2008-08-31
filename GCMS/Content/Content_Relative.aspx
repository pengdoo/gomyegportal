<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content_Relative.aspx.cs" Inherits="Content_Content_Relative" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Content_Relative</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
		<LINK href="../admin_public/css/xpTable.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<script language="javascript">
	var isbusy=false;
	function disable(){
	  if (!isbusy) {isbusy=true;return true;}
	  alert("对不起，系统正忙...");
	}

	function addrelative(){
	  var retV;
	  var argu = "dialogWidth:40em; dialogHeight:35em;center:yes;status:no;help:no";
	  retV = window.showModalDialog("Content_RelativeList.aspx",null,argu);
	  if (retV!=null){
	document.form2.action="Content_ViewOrder.aspx?OrderType=Relative&TypeTree_ID="+Form1.InputTypeTree_ID.value+"&Content_ID="+Form1.InputContent_ID.value+"&retV="+retV;
	//alert("Content_ViewOrder.aspx?OrderType=Relative&TypeTree_ID="+Form1.InputTypeTree_ID.value+"&Content_ID="+Form1.InputContent_ID.value+"&retV="+retV);
  	document.form2.submit();
	  }
	}
			</script>
			<TABLE style="WIDTH: 466px" align="center">
				<TR>
					<td align="left" style="HEIGHT: 24px"><INPUT TYPE="button" onclick="addrelative();" value="增加相关新闻" class="button" style="WIDTH: 77.631pt; HEIGHT: 19px">
						<INPUT type="hidden" name="InputContent_ID" id="InputContent_ID" runat="server">
						<INPUT type="hidden" name="InputTypeTree_ID" id="InputTypeTree_ID" runat="server">
					</td>
				</TR>
				<tr>
					<td align="right" vAlign="top">&nbsp;
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 350px">
							<asp:datagrid id="typeTable" style="BORDER-COLLAPSE: separate" runat="server" Width="100%" BorderWidth="0px"
								Cellpadding="0" AutoGenerateColumns="False" CssClass="xpTable" OnItemDataBound="ItemDataBound">
								<Columns>
									<asp:TemplateColumn>
										<ItemStyle Height="18px" Width="30px"></ItemStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="ID">
										<ItemStyle Height="18px" Width="30px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="文章名称"></asp:BoundColumn>
									<asp:BoundColumn></asp:BoundColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td align="center"><br/>
						<INPUT TYPE="submit" value="更新" class="button" id="Submit1" name="Submit1" runat="server" onserverclick="Submit1_ServerClick">
						<INPUT TYPE="button" onclick="parent.close();" value="关闭" class="button"></td>
				</tr>
			</TABLE>
		</form>
		<form method="post" name="form2">
			<FONT face="宋体"></FONT>
		</form>
	</body>
</HTML>
