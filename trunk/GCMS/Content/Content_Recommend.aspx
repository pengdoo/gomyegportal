<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content_Recommend.aspx.cs" Inherits="Content_Content_Recommend" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tree" Src="../Gomye_Tools/Tools_GTreeCheck.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Content_Preview</title>
		<WEBAPPCONTROLS:TOOLS_HEAD id="Tools_Head" runat="server"></WEBAPPCONTROLS:TOOLS_HEAD>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../admin_Public/Css/xtree.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">

					<form id="Form1" method="post" runat="server">

<script type="text/javascript" language="javascript">
var columnid;
var columnname;
var docDict = new ActiveXObject("Scripting.Dictionary");



function closewindow(){
var retV = "";
for(i=0;i<document.getElementsByName("CheckID").length;i++)
	{
		if(document.getElementsByName("CheckID")[i].checked)
		{
		var id = document.getElementsByName("CheckID")[i].value;
		retV = retV + id + ",";
		}
	}

   retV = retV.substr(0,retV.length-1);
	//alert("Content_ViewOrder.aspx?OrderType=Recommend&Content_List="+Form1.InputContent_ID.value+"&cid="+retV);
   	document.form2.action="Content_ViewOrder.aspx?OrderType=Recommend&Content_List="+Form1.InputContent_ID.value+"&cid="+retV;
  	document.form2.submit();
}
</script>

							<table width="100%">
								<tr>
									<td width="100%" height="400">
										<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 450px">
<WEBAPPCONTROLS:TREE id="TypeTree" runat="server"></WEBAPPCONTROLS:TREE></div>
									</td>

								<tr>
									<td align="center"><br/>
										<INPUT class="button" onclick="closewindow();" type="button" value=" 确 认 ">&nbsp; <INPUT class="button" onclick="top.close();" type="button" value=" 取 消 ">
									</td>
								</tr>
							</table>
<INPUT type="hidden" runat="server" id="InputContent_ID">
					</form>
		<form method="post" name="form2">
		</form>
					

	</body>
</HTML>
