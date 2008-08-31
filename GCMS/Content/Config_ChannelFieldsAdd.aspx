<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Config_ChannelFieldsAdd.aspx.cs" Inherits="Content_Config_ChannelFieldsAdd" %>
<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<title>Type_AddFields</title>
		<WEBAPPCONTROLS:Tools_Head id="Tools_Head" runat="server"></WEBAPPCONTROLS:Tools_Head>
		<script language="javascript">
			function closethiswindows()
			{					
				top.window.close();
			}
			
			function selectDir()
			{
				var argu = "width=500; Height=370;status=no";
				var file = window.open("TypeTree_selectDir.aspx?dir=rootDir","选择目录",argu);				
				if (file!=null)
				{
					//document.Form1.testdir.value=file;
				}
			}
			
			function isselect(){
			var fieldtype = document.Form1.Property_InputType.options[document.Form1.Property_InputType.options.selectedIndex].value; 
			if (fieldtype =="SELECT"){
			document.Form1.Property_InputOptions.className="";
			document.Form1.Property_InputOptions.disabled = false;
			//prompt1.innerText = "选项：";
			//prompt2.innerText = "每行一个选项";
			}
			else if (fieldtype=="LABEL"){
			document.Form1.Property_InputOptions.className="";
			document.Form1.Property_InputOptions.disabled = false;
			//prompt1.innerText = "文字：";
			//prompt2.innerText = "请输入标签的说明文字.";
			}
			else if (fieldtype=="TREES"){
			document.Form1.Property_InputOptions.className="";
			document.Form1.Property_InputOptions.disabled = false;
			//prompt1.innerText = "文字：";
			//prompt2.innerText = "请输入标签的说明文字.";
			}
			else{
			document.Form1.Property_InputOptions.className="disable";
			document.Form1.Property_InputOptions.disabled = true;
			//prompt1.innerText = "";
			//prompt2.innerText = "";
				}
			}

			function open(){
			if (Property.value == "SELECT"){
			document.form1.Property_InputOptions.disabled = false;
			}
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<table style="WIDTH: 100%" border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<TD style="HEIGHT: 12px" valign="top" align="left">
							<table class="table" style="WIDTH: 100%">
								<tr>
									<th noWrap align="left" height="20">
										&nbsp;<asp:label id="Label1" runat="server"></asp:label></th></tr>
							</table>
							<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
								<tr>
									<TD width="5"><SPAN class="handbtn"></SPAN></TD>
									<cc3:Toolsbar id="Toolsbar1" runat="server" AltText="保  存" Text="保  存" Width="55" imageNormal="../Admin_Public/Images/Icon_File_Save.gif"  OnButtonClick="Toolsbar1_ButtonClick"></cc3:Toolsbar>
									<TD></TD>
								</tr>
							</table>
						</TD>
					</tr>
					<tr>
						<td>
							<table class="table" style="WIDTH: 100%">
								<tr>
									<th noWrap align="left" height="19">
										&nbsp;
										<asp:label id="Mssg" runat="server" ForeColor="#FF8000"></asp:label></th></tr>
							</table>
						</td>
					</tr>
				</TBODY>
			</table>
			<TABLE class="DialogTab" id="Table1" style="WIDTH: 100%" height="100%" border="0">
				<tr>
					<TD valign="top">
						<TABLE id="Table4" style="WIDTH: 100%" border="0">
							<tr>
								<TD style="WIDTH: 65px; HEIGHT: 3px" valign="middle" align="right">字段：</TD>
								<TD style="HEIGHT: 3px" valign="top">
									<asp:textbox class="inputtext" id="Property_Name" runat="server" maxlength="100" size="30"></asp:textbox>&nbsp;字母数字以字母开头</TD>
							</tr>
							<tr>
								<TD style="WIDTH: 65px" valign="middle" align="right">别名：</TD>
								<TD valign="top">
									<asp:textbox class="inputtext" id="Property_Alias" runat="server" maxlength="100" size="30"></asp:textbox>&nbsp;用于表单提示</TD>
							</tr>
							<tr>
								<TD style="WIDTH: 65px; HEIGHT: 26px" valign="middle" align="right">类型：</TD>
								<TD style="HEIGHT: 26px" valign="top">
									<select size="1" name="Property_InputType" onChange="isselect();" id="Property_InputType"
										runat="server">
										<option value="TEXT" selected>单行文本</option>
										<option value="TEXTAREA">多行文本</option>
										<option value="SELECT">下拉列表</option>
										<option value="IMAGE">图片</option>
										<option value="FILE">文件</option>
										<option value="DATETIME">日期</option>
										<option value="TREES">目录树</option>
										<option value="NUMBER">数字</option>
										<option value="LABEL">标签</option>
									</select>
								</TD>
							</tr>
							<tr>
								<TD style="WIDTH: 65px" valign="middle" align="right"></TD>
								<TD valign="top">
									<asp:textbox class="inputtext" id="Property_InputOptions" runat="server" maxlength="100" size="30"
										Width="200px" Height="70px" TextMode="MultiLine" Enabled="False"></asp:textbox></TD>
							</tr>
						</TABLE>
					</TD>
				</tr>
			</TABLE>
			<TABLE id="Table3" style="WIDTH: 100%">
				<tr>
					<TH noWrap align="left" height="19"><INPUT type="hidden" id ="OldName" runat="server">
						<asp:label id="saveResult" runat="server"></asp:label><INPUT id="OrderType" type="hidden" runat="server"><INPUT type="hidden" ID="inpFieldsName_ID" runat="server">
					</TH>
				</tr>
			</TABLE>
		</FORM>
		
	</body>
</html>