<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content_Add.aspx.cs" Inherits="Content_Content_Add" ValidateRequest="false" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_PageHeader" Src="../Gomye_Tools/Tools_PageHeader.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="WebControlToolsbar" Assembly="WebControlToolsbar" %>
<%@ Register TagPrefix="WebAppControls" TagName="InputCalendar" Src="../Gomye_Tools/InputCalendar.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="EditorControl" Src="../Gomye_Tools/EditorControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <WebAppControls:Tools_Head id="Tools_Head" runat="server"></WebAppControls:Tools_Head>
    <script type="text/javascript" language="javascript">
		function selectfile(el) {
			var argu = "dialogWidth:24em; dialogHeight:12em;center:yes;status:no;help:no";
			var newimage=window.showModalDialog("uploadfile.asp",null,argu);
			if (newimage!=null) {
				el.value = newimage;
				}
		}

		function selectdate(el){
			var args="font-size:10px;dialogWidth:286px;dialogHeight:290px;center:yes;status:no;help:no";
			var argu=new Array();
			argu[0]="";
			var selectdate=window.showModalDialog("Tools_SelectDate.html",argu,args);
			if (selectdate!=null)
			el.value=selectdate;
			}

		function doDownload(el){
			try{
				if (el.value!="")
					top.document.all.Download.src="Content_Download.asp?" + el.value;
			}catch(exception){}
		}

		function doPreview(el){
			try{
				if (el.value!="")
					window.open(el.value);
			}catch(exception){}
		}
		
		function selectImages(el) {
			var argu = "dialogWidth:320px; dialogHeight:130px;center:yes;status:no;help:no";
			var newimage=window.showModalDialog("WindowFrame.aspx?loadfile=Tools_UploadFiles.aspx",null,argu);
			if (newimage!=null) {
				el.value = newimage;
				}
		}
		
		function selectImagesD(el) {

			var argu = "dialogWidth:1520px; dialogHeight:1530px;center:yes;status:no;help:no";
			var newimage=window.showModalDialog("WindowFrame.aspx?loadfile=Tools_UploadPhotoD.aspx&sType=<%=Request["TypeTree_ID"]%>",null,argu);
			if (newimage!=null) {
			
						if (newimage.indexOf("|") > 0) 
						{
							var aTmp = newimage.split("|"); 
							Form1.TextBoxPic.value = aTmp[1];
							el.value = aTmp[0];
						}
						else
						{
							el.value = newimage;
						}
				}
		}
				
		function editHTML(el){
			var args="dialogWidth:720px";
			var strHTML = window.showModalDialog("ScriptletHtml/edit.asp",el,args);
			if (strHTML!=null)
				el.value = strHTML;
		}
		
		function selectTree(el,ID){
			var args="font-size:10px;dialogWidth:286px;dialogHeight:290px;center:yes;status:no;help:no";
			var argu=new Array();
			argu[0]="";
			var selectdate=window.showModalDialog("Tools_SelectTree.aspx?TypeTree_ID="+ID,argu,args);
			if (selectdate!=null)
			el.value=selectdate;
			}
			
		function OnsubmitScript(){
		alert("eww");
		return false;
			}
			
				
		</script>
</head>
<body leftMargin="0" topMargin="0">
    <form id="form1" runat="server">
    <WebAppControls:TOOLS_PAGEHEADER id="PageHeader" runat="server" MenuStatus="3" Value="" mod="3"></WebAppControls:TOOLS_PAGEHEADER>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="coolBar">
				<TR><TD width="5"><SPAN class="handbtn"></SPAN></TD>
				<td>
						<asp:panel id="Panel1" runat="server">
									<cc3:Toolsbar id="Toolsbar4" runat="server" AltText="草稿" Text="草稿" Width="60" imageNormal="../Admin_Public/Images/Icon_File_Save.gif" OnButtonClick="Toolsbar4_ButtonClick"></cc3:Toolsbar>
									<cc3:Toolsbar id="Toolsbar5" runat="server" AltText="待审批" Text="待审批" Width="60" imageNormal="../Admin_Public/Images/Icon_File_Save.gif" OnButtonClick="Toolsbar5_ButtonClick"></cc3:Toolsbar>
									<cc3:Toolsbar id="Toolsbar6" runat="server" AltText="发布" Text="发布" Width="60" imageNormal="../Admin_Public/Images/Icon_File_Save.gif" OnButtonClick="Toolsbar6_ButtonClick"></cc3:Toolsbar>
									<cc3:Toolsbar id="Toolsbar3" runat="server" AltText="归档" Text="归档" Width="60" imageNormal="../Admin_Public/Images/Icon_File_Save.gif" OnButtonClick="Toolsbar3_ButtonClick"></cc3:Toolsbar>
						</asp:panel>
						<asp:panel id="Panel2" runat="server">
									<cc3:Toolsbar id="Toolsbar1" runat="server" AltText="草稿" Text="草稿" Width="60" imageNormal="../Admin_Public/Images/Icon_File_Save.gif" OnButtonClick="Toolsbar1_ButtonClick"></cc3:Toolsbar>
									<cc3:Toolsbar id="Toolsbar2" runat="server" AltText="待审批" Text="待审批" Width="60" imageNormal="../Admin_Public/Images/Icon_File_Save.gif" OnButtonClick="Toolsbar2_ButtonClick"></cc3:Toolsbar>
						</asp:panel>
						</td>
						<TD width="5"><SPAN class="sepbtn1"></SPAN></TD>
						<TD class="coolButton" title="相关联文章" onClick="window.location.href = 'Content_AddAll.aspx?TypeTree_ID='+LabelTypeID.innerText+'&Content_ID='+LabelEditContentID.innerText;" width="85" height="20"><IMG src="../Admin_Public/Images/Icon_File_New2.gif">
						相关联文章</TD>
				
				</TR>
			</TABLE>
			<DIV class="DivListView" id="scrollDiv" align="center">
				<SCRIPT language="javascript">
	                    window.onresize=fixSize;
	                    fixSize();

	                    function fixSize(){
		                    scrollDiv.style.height=Math.max(document.body.clientHeight-48,0);
	                    }
				</SCRIPT>
				<TABLE class="DialogTab" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
					<TR>
						<TD vAlign="top">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<asp:panel id="PanelName" runat="server" Visible="false">
							
								<TR>
									<TD style="WIDTH: 100px; HEIGHT: 25px" align="right">名称标题：&nbsp;</TD>
									<TD style="HEIGHT: 25px"><asp:textbox id="TextBoxTitle" runat="server" CssClass="inputtext250"></asp:textbox>&nbsp;
										<asp:label id="LabNameMust" runat="server" Width="200px" ForeColor="#FF8000"></asp:label></TD>
								</TR>
								<TR>
									<TD align="right">发布时间：&nbsp;</TD>
									<TD align="left"><WEBAPPCONTROLS:INPUTCALENDAR id="TextBoxDate" runat="server" CssClass="inputtext250" UniqueID="TextBoxDate" MenuStatus="3"></WEBAPPCONTROLS:INPUTCALENDAR></TD>
								</TR>
								<TR>
									<TD align="right"><asp:label id="LabTxt1" runat="server">文章出处：</asp:label>&nbsp;</TD>
									<TD align="left"><asp:textbox id="TextBoxFrom" runat="server" CssClass="inputtext250"></asp:textbox>&nbsp;
										<asp:label id="LabTxt2" runat="server">出处链接：</asp:label><FONT face="宋体">&nbsp;
											<asp:textbox id="TextBoxLink" runat="server" CssClass="inputtext250"></asp:textbox></FONT></TD>
								<TR>
									<TD align="right">
										<asp:label id="LabTxt4" runat="server">关 键 字：</asp:label>&nbsp;</TD>
									<TD align="left"><asp:textbox id="KeyWord" runat="server" CssClass="inputtext250"></asp:textbox>&nbsp;
										<asp:label id="LabTxt5" runat="server">原 作 者：</asp:label>&nbsp;
										<asp:textbox id="Original" runat="server" CssClass="inputtext250"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 27px" align="right">&nbsp;</TD>
									<TD align="left"><asp:checkbox id="Headnews" runat="server" Text="首页新闻"></asp:checkbox><asp:checkbox id="PictureNews" runat="server" Text="图片新闻"></asp:checkbox></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 27px" align="right">大 图 片：&nbsp;</TD>
									<TD align="left"><asp:textbox id="TextBoxPicD" runat="server" CssClass="inputtext250"></asp:textbox>&nbsp;<INPUT class="button" onclick="selectImagesD(TextBoxPicD);" type="button" value="上传">
									</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 27px" align="right"><asp:label id="LabTxt3" runat="server"></asp:label>&nbsp;</TD>
									<TD align="left"><asp:textbox id="TextBoxPic" runat="server" CssClass="inputtext250"></asp:textbox>&nbsp;<INPUT class="button" onclick="selectImages(TextBoxPic);" type="button" value="上传">
									</TD>
								</TR>

								<TR>
									<TD style="HEIGHT: 27px" align="right">&nbsp;</TD>
									<TD align="left"><asp:textbox id="Picture_Notes" runat="server" CssClass="inputtext250" Width="248px" Height="96px"
											TextMode="MultiLine"></asp:textbox></TD>
								</TR>
								</asp:panel>
								
								<TR>
									<TD align="right" colSpan="2"><asp:table id="Table2" runat="server" Width="100%" border="0" cellPadding="0" cellSpacing="0"></asp:table></TD>
								</TR>
								<asp:panel id="PanelContent" runat="server" Visible="false">
								<TR>
									<TD style="HEIGHT: 360px" vAlign="top" align="right">信息内容：&nbsp;</TD>
									<TD id="TD1" style="HEIGHT: 480px" runat="server">
										<!-----------------------------------------------------------------------------------------------><WEBAPPCONTROLS:EditorControl id="EditorControl1" runat="server" MenuStatus="3" Value=""></WEBAPPCONTROLS:EditorControl>
										<!-----------------------------------------------------------------------------------------------></TD>
								<TR>
									<TD vAlign="top" align="right"></TD>
									<TD align="left"><asp:checkbox id="CheckBoxWebtoThisPic" runat="server" Text="将“信息内容”中的远程图片自动下载到本地" Checked="True"></asp:checkbox><br/>
								</asp:panel>
										<asp:label id="LabelMsg" runat="server" Width="420px" ForeColor="Red"></asp:label><asp:label id="LabelFlag" runat="server" Visible="False"></asp:label><INPUT id="hiddenPage" type="hidden" name="hiddenPage" runat="server">
										<asp:label id="LabelCurPage" runat="server" Visible="False">1</asp:label><INPUT id="DESCRIPTION" type="hidden" name="DESCRIPTION" runat="server"><INPUT id="ButtonChangePage" type="button" runat="server" Visible="False">
										<asp:label id="LabelTypeID" runat="server" ForeColor="Menu"></asp:label><asp:label id="LabelEditContentID" runat="server" ForeColor="Menu"></asp:label>
										<asp:label id="TypeTree_URL" runat="server" ForeColor="Menu"></asp:label><asp:label id="TypeTree_Template" runat="server" ForeColor="Menu"></asp:label>
										<asp:label id="TypeTree_Type" runat="server" ForeColor="Menu"></asp:label></TD>
								</TR>
							</TABLE>
							<br/>
							<br/>
							文章处理意见：(您在发布、驳回、审批等工作流程中的备注信息)
							<asp:TextBox id="TextBoxLogText" runat="server" TextMode="MultiLine" CssClass="inputtext250"
								Width="248px"></asp:TextBox>
							<br/>
							<br/>
							该文件处理记录：
							<table cellSpacing="1" cellPadding="2" width="100%" bgColor="buttonshadow" border="0">
								<asp:datalist id="DataList1" runat="server" Width="100%" RepeatColumns="1" ShowFooter="False"
									ShowHeader="False">
									<ItemTemplate>
										<tr bgColor="buttonhighlight">
											<td vAlign="top" width="25%"><%#DataBinder.Eval(Container.DataItem,"Log_Date")%></td>
											<td vAlign="top" width="25%"><%#DataBinder.Eval(Container.DataItem,"Master_Name")%></td>
											<td vAlign="top" width="25%"><%#DataBinder.Eval(Container.DataItem,"Log_Action")%></td>
											<td vAlign="top" width="25%"><%#DataBinder.Eval(Container.DataItem,"Log_Txt")%></td>
										</tr>
									</ItemTemplate>
								</asp:datalist></table>
			</DIV>
    </form>
</body>
</html>
