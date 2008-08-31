<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tools_FileMain.aspx.cs" Inherits="Content_Tools_FileMain" %>
<html>
<head>
<title>Web Edit</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
 <link href="../Admin_Public/css/Admin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
function closeWindow(){
	window.returnValue=document.webedit.curpath.value;
	window.close();
}
</script>

   
</head>

<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" rightmargin="0" topmargin="0" bottommargin="0">

<form runat="server" id="webedit" name="webedit" method="post">
  <table width="534" border="1" cellspacing="0" cellpadding="0" bordercolordark="#FFFFFF" bordercolorlight="#666666">
    <tr> 
      <td height="30" bgcolor="#CCCCCC" width="778"> 
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr> 
            <td height="20">&nbsp;位置:
                  <input type="text" name="curpath" style="width:350" class="tx" id="curpath" runat="server">
            
              <input type="button" name="Button" value="打 开" class="bt1" onclick="closeWindow();" id="btn_openpath" >
            </td>
            <td height="20" width="0%">&nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
    <tr> 
      <td height="340" align="left" valign="top" width="777"> 
        <table width="100%" border="1" cellspacing="0" cellpadding="0">
          <tr> 
            <td height="340" align="left" valign="top" width="160"> 
              <table border="0" cellspacing="0" cellpadding="0" width="160">
                <tr align="right" bgcolor="#EEEEEE"> 
                  <td height="25" valign="middle" colspan="5">&nbsp; 
                    <input type="button" name="Button4" value="新建" class="bt1"  id="btn_newDir" onserverclick="btn_newDir_ServerClick" runat="server">
                  </td>
                </tr>
                <tr align="left" bgcolor="#EEEEEE"> 
                  <td height="25" valign="middle" colspan="5"> &nbsp;<asp:LinkButton ID="lkbtn_backToRoot" runat="server" Text="↑上一级目录" OnClick="lkbtn_backToRoot_Click"></asp:LinkButton></td>
                </tr>
                                <tr> 
                  <td align="center" valign=top height="280">
                                    <div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 100%">
<table >

<asp:Repeater runat="server" ID="rep_folders" OnItemCommand="rep_folders_ItemCommand" >
<ItemTemplate>
<tr> 

<td width="24" height="14" align="center" valign="middle">
<img src="../admin_public/Images/fc.gif" width="16" height="15"/></td>
                  <td align="left" width="112">&nbsp;
                  <asp:LinkButton ID="lkbtn_dir" runat="server" Text='<%#Eval("Name")%>' CommandName="chdir" CommandArgument='<%#Eval("Name")%>'></asp:LinkButton>
                   </td> </tr> 
</ItemTemplate>
</asp:Repeater>

                

 <tr>  <td style="width: 3px">  </td></tr>
</table>
</div>
  </td>
                  </tr>
                  
                <tr bgcolor="#EEEEEE"> 
                  <td height="21" colspan="5">
                      共<asp:Literal ID="lib_dirCount" runat="server"></asp:Literal>个目录
                  </td>
                </tr>
              </table>
            </td>
            <td width="8" height="340" bgcolor="#CCCCCC"></td>
            <td width="550" height="340" align="left" valign="top"> 
              <table border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr align="right" bgcolor="#EEEEEE"> 
                    <td height="25" valign="middle" colspan="8"> 
                  </td>
                </tr>
                <tr align="left"> 
                  <td height="25" valign="middle" colspan="8" bgcolor="#EEEEEE">总<asp:Literal ID="lit_fileTotleCount" runat="server"></asp:Literal>个文件,共<asp:Literal
                          ID="lit_filePageCount" runat="server"></asp:Literal>页,
                 
                    第 
                    <input type="text" name="fliter2" style="width:30" class="tx" id="currentpage" runat="server">
                    页 
                    <input type="button" runat="server" id="btn_goto" name="Button5" value="转到" class="bt1" onserverclick="btn_goto_ServerClick"  >
                  </td>
                </tr></table>
				
				
<div style="padding:0px;margin:0px;border:0px solid navy;overflow-y:scroll;height:280;width:100%;">
				<table width="100%" border="0" cellspacing="0" cellpadding="0">
             <asp:Repeater ID="rep_files" runat="server" OnItemCommand="rep_files_ItemCommand">
             <ItemTemplate>
                <tr>
                  <td width="17" align="center" valign="middle"><img src="../admin_public/Images/<%#procGetFormat(Eval("Name").ToString())%>.gif" width="16" height="16"></td>
                  <td width="213">&nbsp;<asp:LinkButton ID="lkbtn_file" runat="server" Text='<%#Eval("Name")%>' CommandName="select" CommandArgument='<%#Eval("Name")%>'></asp:LinkButton></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
              </table>  </div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td bgcolor="#EEEEEE" style="height: 28px">当前共<asp:Literal ID="lit_fileCount" runat="server"></asp:Literal>个文件,共<asp:Literal
                            ID="lit_fileSizeCount" runat="server"></asp:Literal>K 
                    </td>
                  </tr>
                </table>
            
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <tr> 
      <td height="24" align="center" valign="middle" bgcolor="#CCCCCC" width="777">&nbsp;</td>
    </tr>
  </table>
</form>
</body>
</html>

