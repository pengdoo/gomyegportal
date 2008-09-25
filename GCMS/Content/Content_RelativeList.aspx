<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content_RelativeList.aspx.cs" Inherits="Content_Content_RelativeList" %>

<%@ Register TagPrefix="WebAppControls" TagName="TOOLS" Src="../Gomye_Tools/Tools_GTree.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>相关文章列表</title>
		<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET"/>
		<script language='JavaScript' src="../admin_public/js/Nav_RelativeList.js"></script>
		<link type="text/css" rel="stylesheet" href="../admin_Public/Css/xtree.css">/
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<script language="javascript">
                    var columnid;
                    var columnname;
                    var docDict = new ActiveXObject("Scripting.Dictionary");


                    function closewindow(){
                       var retV = "";
                            var sId = new VBArray(top.docDict.Keys());
                            for(var i=0;i<top.docDict.Count;i++){
			                    var id = sId.getItem(i).substr(4);
			                    retV = retV + id + ",";
		                    }
                       retV = retV.substr(0,retV.length-1);
                       top.returnValue=retV;
                       top.close();
                    }
			</script>
			<CENTER>
				<table align="center">
					<TBODY>
						<tr>
							<td style="WIDTH: 201px">
<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 450px">
<WEBAPPCONTROLS:TOOLS id="TypeTree" runat="server"></WEBAPPCONTROLS:TOOLS>
</div>
							</td>
							<td style="WIDTH: 401px"><iframe id="Content_RelativeContent" name="Content_RelativeContent" src="Main_Content.aspx?RightID=0" scrolling="Auto" width ="100%" style="height:450px" frameborder=0></iframe>
							</td>
						</tr>
						<tr>
							<td align="center" colSpan="2"><br>
								<INPUT onclick="closewindow();" type="button" class="button" value=" 确 认 "> <INPUT onclick="top.close();" class="button" type="button" value=" 取 消 ">
							</td>
						</tr>
					</TBODY>
				</table>
			</CENTER>
		</form>
	</body>
</HTML>

