<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_TypeMain.aspx.cs" Inherits="Content_Type_TypeMain" %>

<%@ Register Src="../Gomye_Tools/Tools_TreeMenu.ascx" TagName="Tools_TreeMenu" TagPrefix="uc1" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_PageHeader" Src="../Gomye_Tools/Tools_PageHeader.ascx" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_Head" Src="../Gomye_Tools/Tools_Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <HEAD>
		<title>CMS</title>
		<WEBAPPCONTROLS:Tools_Head id="Tools_Head" runat="server"></WEBAPPCONTROLS:Tools_Head>
		<link rel="stylesheet" href="../js/jquery.treeview/jquery.treeview.css" />
		
        <script type="text/javascript" src="../js/jquery/jquery-1.2.6.pack.js"></script>
        <script type="text/javascript" src="../js/jquery.treeview/jquery.treeview.min.js"></script>
        <script type="text/javascript" src="../js/jquery.contextmenu/jquery.contextmenu.r2.packed.js"></script>
        <script type="text/javascript" src="../js/jquery.ui/jquery-ui-droppable-1.5.2.min.js"></script>
        <script type="text/javascript">
	    $(document).ready(function(){
		    $("#treemenu").treeview();//载入菜单
		    
		    $("span").contextMenu('mainContentMenu', {//载入完整菜单
                bindings: {
                  'new': function(t) {
                            var argu = "dialogWidth:32em; dialogHeight:28em;center:yes;status:no;help:no";
							var cId = parseInt(t.id.replace("treemenu-", ""));
							window.showModalDialog("WindowFrame.aspx?loadfile=Type_Add.aspx&OrderType=son&TypeTree_ID="+cId,"新建子目录",argu);
							parent.location.reload();
                  },
                  'refash': function(t) {
                    doReFresh();
                  },
                  'up': function(t) {
                            var cId = parseInt(t.id.replace("treemenu-", ""));
 	                        var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	                        window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=doMoveUp&TypeTree_ID=" + cId,"排列目录",argu);
	                        window.location.reload();
                  },
                  'down': function(t) {
                            var cId = parseInt(CurrentNode);
 	                        var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	                        window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=doMoveDown&TypeTree_ID=" + cId,"排列目录",argu);
	                        window.location.reload();
                  }
                },
                onShowMenu: function(e, menu) {//根据情况屏蔽部分菜单
                if ($(e.target).attr('class') != 'folder') {
                  $('#up, #down', menu).remove();
                }
                return menu;
                }
            });
        
		//节点的拖拽事件
		$('.folder').draggable(
			{
				revert		: true,
				autoSize		: true,
				ghosting			: true
			}
		);

         //节点的拖放事件
        $('.folder').droppable(
			{
				accept			: '.folder',
				drop			: function(ev,ui)
				{
				    //#此处有IE和FF的兼容问题
				    if (window.event.shiftKey==true){
				        doMoveChannel(this,ui.draggable.clone());
				    }
	                else {
	                    doCopyChannel(this,ui.draggable.clone());
	                }
	                event.returnValue = false;

				}
			}
		);
        function doReFresh(){
				window.location.reload();
			}
        function doMoveChannel(org,tar){
            
	        question = confirm("您确认要移动栏目么！将"+$(org).attr('name')+" 移动到 "+$(tar).attr('name')) ;
	        if (question != "1")
	        {return false;}
	        var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	        window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=preMoveChannel&TypeTree_ID=" + $(org).attr('id').replace("treemenu-", "") + "&parent=" + tar.attr('id').replace("treemenu-", ""),"移动频道",argu);
	        doReFresh();
	        }

        function doCopyChannel(org,tar){

	        question = confirm("您确认要拷贝栏目么！将"+$(org).attr('name')+" 复制到 "+$(tar).attr('name')) ;
	        if (question != "1")
	        {return false;}
	        var argu = "dialogWidth:32em; dialogHeight:16em;center:yes;status:no;help:no";
	        window.showModalDialog("WindowFrame.aspx?loadfile=Type_Order.aspx&OrderType=preCopyChannel&TypeTree_ID=" +  $(org).attr('id').replace("treemenu-", "") + "&parent=" + tar.attr('id').replace("treemenu-", ""),"复制频道结构",argu);
	        doReFresh();
	        }   
                
		});
        </script>
		<link href="../Admin_Public/css/Admin.css" rel="stylesheet" type="text/css" />

</HEAD>
	<body topmargin="0" leftmargin="0"  scroll="no">
		<form id="Form1" runat="server">
		<WEBAPPCONTROLS:TOOLS_PAGEHEADER id="PageHeader" runat="server" Value="栏  目" Mod="2"></WEBAPPCONTROLS:TOOLS_PAGEHEADER>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td valign="top">
						<table class="coolBar" style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="1" cellPadding="0"
							border="0">
							<tr>
								<TD style="WIDTH: 5px"><SPAN class="handbtn"></SPAN></TD>
								<%if (int.Parse(Session["Roles"].ToString()) == 0){%>
								<td class="coolButton" title="新建" style="WIDTH: 80px; HEIGHT: 20px" onclick="parent.frames['Main_List'].location = 'Type_Add.aspx?OrderType=root'"><IMG src="../Admin_Public/Images/Icon_File_FileCode.gif">
									新建根目录</td>
								<TD style="WIDTH: 5px"><SPAN class="sepbtn1"></SPAN></TD>
								<%}%>
								<td class="coolButton" title="刷新" style="WIDTH: 54px; HEIGHT: 20px" onclick="doReFresh();"><IMG src="../Admin_Public/Images/Icon_File_ReFresh.gif">
									刷 新</td>
								<td><FONT face="宋体"></FONT></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" height="100%">
						<div style="BORDER-RIGHT: navy 0px solid; PADDING-RIGHT: 0px; BORDER-TOP: navy 0px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: navy 0px solid; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: navy 0px solid; HEIGHT: 100%">
						
                            <uc1:Tools_TreeMenu ID="MainMenu" runat="server" />
						</div>
					</td>
				</tr>
			</table>


		</form>
		<!-- 菜单开始 -->
		<div class="contextMenu" id="mainContentMenu"  style="display: none;" >
            <ul>
              <li id="new">新建频道</li>
              <li id="refash">刷新</li>
              <li id="up">上移</li>
              <li id="down">下移</li>
            </ul>
         </div>
         <!-- 菜单结束 -->

	</body>
</html>
