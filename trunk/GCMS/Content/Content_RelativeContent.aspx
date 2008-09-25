<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content_RelativeContent.aspx.cs" Inherits="Content_Content_RelativeContent" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<LINK href="../admin_public/css/xpTable.css" type="text/css" rel="stylesheet">
			<LINK href="../admin_public/css/Admin.css" type="text/css" rel="STYLESHEET">
				<script language="javascript">

	function InitCheckbox(){
        var sId = new VBArray(top.docDict.Keys());
        for(var i=0;i<top.docDict.Count;i++){
			var El=document.getElementById(sId.getItem(i));
			if(El!=null)El.checked=true;
		}
	}

    function doDocClick(oEl){
      var key = oEl.id;
      var value = oEl.value;
      if(oEl.checked){
      if(!top.docDict.Exists(key))
          top.docDict.Add(key,value);
      }
      else
        top.docDict.remove(key);
    }

    function onDoClick(oEl){
      doDocClick(oEl);
    }

function selectall(){
  var bv;
  if (Form1.btnselectall.innerText=="全选"){
   bv = true;
   Form1.btnselectall.innerText="全不选";
  }
  else{
   bv = false;
   Form1.btnselectall.innerText="全选";
  }
  var acid = document.all("cid");
  if (acid!=null)
    for (var i=0;i<acid.length;i++)
      {acid[i].checked=bv;
       doDocClick(acid[i]);
      }
}

				</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="InitCheckbox();">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td width="20" height="32"><FONT face="宋体"><button onclick="selectall();" id="btnselectall" type="button" class="button">全选</button></FONT>
					</td>
					<td width="30%" height="32"><input type="text" id="keyword" runat="server"></td>
					<td height="32">
					<button type="button" class="button" id="BUTTON1" runat="server" onserverclick="BUTTON1_ServerClick">搜索</button></td>
				</tr>
				<tr>
					<td colspan="3">
						<TABLE class="DialogTab" id="Table1" style="WIDTH: 100%" cellSpacing="0" cellPadding="0"
							align="center" border="0">
							<TR>
								<TD>
									<TABLE id="tablist" height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
										border="0">
										<TR>
											<TD vAlign="top">
												<TABLE class="TopTitle" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD width="1"><IMG src="../Admin_Public/Images/Icon_File_New.gif"></TD>
														<TD><FONT color="#ffffff">文章列表</FONT></TD>
													</TR>
												</TABLE>
												<asp:datagrid id="typeTable" style="BORDER-COLLAPSE: separate" runat="server" Width="100%" BorderWidth="0px"
													Cellpadding="0" AutoGenerateColumns="False" CssClass="xpTable" OnItemDataBound="ItemDataBound">
													<Columns>
														<asp:TemplateColumn>
															<ItemStyle Height="18px" Width="30px"></ItemStyle>
															<ItemTemplate>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="Content_ID" SortExpression="Content_ID" HeaderText="ID">
															<ItemStyle Height="18px" Width="30px"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="文章名称"></asp:BoundColumn>
														<asp:BoundColumn></asp:BoundColumn>
													</Columns>
												</asp:datagrid></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td colspan="3">说明：选择后您可以切换到别的频道，我们会记住您的选择。搜索时会包括所有子频道。</td>
				</tr>
			</table>

		</form>
			<form action="search.asp" method="get" name="form1">
				<input type="hidden" name="columnid"> <input type="hidden" name="keyword">
			</form>
	</body>
</HTML>