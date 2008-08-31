<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Config_ChannelEdit.aspx.cs" Inherits="Content_Config_ChannelEdit" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<title>TypeTree_edit</title>
		<WEBAPPCONTROLS:Tools_Head id="Tools_Head" runat="server"></WEBAPPCONTROLS:Tools_Head>
		<script language="javascript">
			function AddFields()
			{
				var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Config_ChannelFieldsAdd.aspx&OrderType=Create&FieldsName_ID="+Form1.txtFieldsName_ID.value,"字段定义",argu);	
				window.location.href = "Config_ChannelEdit.aspx?FieldsName_ID=" + Form1.txtFieldsName_ID.value;
			}
					
			function UpdataFields(Fields_ID)
			{	
				var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
				window.showModalDialog("WindowFrame.aspx?loadfile=Config_ChannelFieldsAdd.aspx&OrderType=Updata&Fields_ID="+Fields_ID+"&FieldsName_ID="+Form1.txtFieldsName_ID.value,"更新字段",argu);		
				window.location.href = "Config_ChannelEdit.aspx?FieldsName_ID=" + Form1.txtFieldsName_ID.value;
			}
					
			function DeleteFields(Fields_ID)
			{
				if(confirm("确定要删除此字段吗？此操作是无法恢复！！"))
				{					
					var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
					window.showModalDialog("WindowFrame.aspx?loadfile=Config_ChannelFieldsAdd.aspx&OrderType=Delete&Fields_ID="+Fields_ID+"&FieldsName_ID="+Form1.txtFieldsName_ID.value,"删除字段",argu);		
					window.location.href = "Config_ChannelEdit.aspx?FieldsName_ID=" + Form1.txtFieldsName_ID.value;
				}

			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="table" style="WIDTH: 100%">
				<tr>
					<th noWrap align="left" height="20">
						&nbsp;<asp:label id="Label1" runat="server"></asp:label></th></tr>
			</table>
			<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
				<tr>
					<TD width="5"><SPAN class="handbtn"></SPAN></TD>
					<TD class="coolButton" title="插入字段" onclick="AddFields();" width="70" height="20"><IMG src="../Admin_Public/Images/Icon_File_Save.gif">
						插入字段</TD>
					<TD></TD>
				</tr>
			</table>
			<table cellspacing="0" cellpadding="0" width="100%" height="90%">
				<tr>
					<td valign="top">
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 100%">
							<TABLE class="coolBar" style="TABLE-LAYOUT: fixed; WIDTH: 100%">
								<asp:Panel id="Panel1" runat="server" Visible="False">
									<TBODY>
										<tr>
											<TD style="WIDTH: 75px">名称标题：</TD>
											<TD>
												<asp:TextBox id="TypeTree_CName" runat="server" Width="216px" ReadOnly="True" BackColor="White"
													CssClass="inputtext"></asp:TextBox></TD>
											<TD style="WIDTH: 75px"></TD>
										</tr>
										<TR valign="top">
											<TD>信息内容：</TD>
											<TD>
												<asp:TextBox id="TypeTree_Explain" runat="server" Width="216px" ReadOnly="True" BackColor="White"
													CssClass="inputtext" TextMode="MultiLine" Height="88px"></asp:TextBox></TD>
											<TD style="WIDTH: 75px"></TD>
										</tr>
								</asp:Panel>
								<asp:Label id="AddFieldsWrite" runat="server"></asp:Label></TABLE>
							<INPUT type="hidden" id="txtFieldsName_ID" name="txtFieldsName_ID" runat="server"></div>
					</td>
				</tr>
				</table>
		</form>
	</body>
</html>