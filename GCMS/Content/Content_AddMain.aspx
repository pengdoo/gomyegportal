<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content_AddMain.aspx.cs" Inherits="Content_Content_AddMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD html 4.0 Transitional//EN" >
<html>
	<head>
	<title></title>

		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<script language="JavaScript" src="../admin_public/js/coolbuttons.js"></script>
		<link href="../admin_public/css/listview.css" type="text/css" rel="STYLESHEET"/>
			<link href="../admin_public/css/admin.css" type="text/css" rel="STYLESHEET"/>
	</head>
	<body>
	<form id="Form1" runat="server">
		<table height="1020" cellSpacing="0" cellPadding="0" width="453" border="0" >
			<tbody>
				<tr vAlign="top">
					<TD width="453" height="1020">
						
							<table height="451" cellSpacing="0" cellPadding="0" width="1018" border="0" ms_2d_layout="TRUE">
								<tr vAlign="top">
									<TD width="0" height="1"></TD>
									<TD width="1018" rowSpan="2">
										<table height="450" cellSpacing="0" cellPadding="0" width="1017" border="0">
											<tr HEIGHT="15">
												<td height="15" vAlign="top">
													<table width="100%" height="20" border="0" cellpadding="0" cellspacing="0" class="coolBar">
														<tr>
															<td noWrap="noWrap" align="left">
																&nbsp;相关内容
															</td>
															<td width="10" valign="middle">
																<span class="coolButton" onClick="parent.Mainframe.cols = '0,*';">
																	<img src="../Admin_Public/Images/close.gif"></span>
															</td>
														</tr>
													</table>
													<table border="0" width="100%" class="coolBar" cellspacing="1" cellpadding="0">
														<tr>
															<TD width="5"><SPAN class="handbtn"></SPAN></TD>
															<td class="coolButton" id="setMaster" onClick="doReFresh();" width="60" height="20">
																<img src="../Admin_Public/Images/Icon_File_ReFresh.gif" alt="刷 新"> 刷 新</td>
															<td></td>
														</tr>
													</table>
												</td>
											</tr>
											<tr HEIGHT="15">
												<td height="100%" vAlign="top">
													<div><span id="TypeTree_Label1"></span>
															<div><span>&nbsp;<IMG src="../admin_public/images/fo.gif" align="absMiddle" border="0">&nbsp;相关联文章</span></div>
															<div class="parent" id="m1Parent"><IMG src="../Admin_Public/Images/Tree_white.gif" align="absMiddle" border="0"><span onmouseup="OpenFolder('m1','Content_Add.aspx?flag=edit&amp;Content_ID='+Form1.TxtContent_ID.value+'&TypeTree_ID=','COLUMN_1');"
																	onmouseover="IsonMouseOver('m1');" onmouseout="IsonMouseOut('m1');"><IMG src="../admin_public/images/closedfolder.gif" align="absMiddle" border="0" name="m1Pic">&nbsp;<A class="item" href="#nothisanchor" name="m1Folder">&nbsp;返回文章</A></span>
															</div>
															<div class="parent" id="m2Parent">
																<IMG src="../Admin_Public/Images/Tree_white.gif" align="absMiddle" border="0"><SPAN onmouseup="OpenFolder('m2','#','COLUMN_2');" onmouseover="IsonMouseOver('m2');"
																	onmouseout="IsonMouseOut('m2');"><IMG src="../Admin_Public/Images/closedfolder.gif" align="absMiddle" border="0" name="m2Pic">&nbsp;<A class="item" href="#nothisanchor" name="m2Folder">&nbsp;相关文章</A></SPAN>
																<asp:Label id="LabelConnectContent" runat="server"></asp:Label>
															</div>
															<div class="parent" id="m3Parent">
																<IMG src="../Admin_Public/Images/Tree_white.gif" align="absMiddle" border="0"><SPAN onmouseup="OpenFolder('m2','#','COLUMN_2');" onmouseover="IsonMouseOver('m2');"
																	onmouseout="IsonMouseOut('m2');"><IMG src="../Admin_Public/Images/closedfolder.gif" align="absMiddle" border="0" name="m2Pic">&nbsp;<A class="item" href="#nothisanchor" name="m2Folder">&nbsp;子文章</A></SPAN>
																<asp:Label id="LabelSonContent" runat="server"></asp:Label>
															</div>
															<div class="parent" id="m4Parent">
																<IMG src="../Admin_Public/Images/Tree_white.gif" align="absMiddle" border="0"><SPAN onmouseup="OpenFolder('m3','Content_RecommendList.aspx?Content_ID='+Form1.TxtContent_ID.value,'COLUMN_3');"
																	onmouseover="IsonMouseOver('m3');" onmouseout="IsonMouseOut('m3');"><IMG src="../Admin_Public/Images/closedfolder.gif" align="absMiddle" border="0" name="m3Pic">&nbsp;<A class="item" href="#nothisanchor" name="m3Folder">&nbsp;映射到栏目</A></SPAN>
																<asp:Label id="Label1" runat="server"></asp:Label>
															</div>
													</div>
													
													<script language="JavaScript" src="../admin_public/js/nav.js"></script>
												</td>
											</tr>
										</table>
									</TD>
								</tr>
								<tr vAlign="top">
									<TD width="0" height="450"></TD>
									<TD>
										<INPUT type="hidden" id="TxtContent_ID" runat="server"/></TD>
								</tr>
							</table>
					</TD>
					</tr>
					</tbody>
						
		</table>
		</form>
	</body>
</html>
