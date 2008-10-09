<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Setup.aspx.cs" Inherits="Setup" EnableSessionState ="True" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="js/jquery/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" src="js/jquery.progressbar/jquery.progressbar.min.js"></script>
   

</head>
<body>
 <script type="text/javascript">
			$(document).ready(function() {
				
				 $("#progress_sql").progressBar({ barImage: 'js/jquery.progressbar/images/progressbg_yellow.gif'} )
			});
            //window.setInterval("showsqlprog()", 2000);
 
			function showsqlprog() {
				$.get("Service/Session.aspx?action=Get&id=progress_runsql", function(data) {
					if (!data)
						return;
						
				    //var percentage = Math.int(data);
				    //$("#progress_sql").progressBar({ barImage: 'js/jquery.progressbar/images/progressbg_yellow.gif'} )
					$("#progress_sql").progressBar(data);
					})
			};
			
		    
		     
    </script>
    <form id="form1" runat="server">
    <div>
        安装进度<br />
        <span class="progressBar" id="progress_sql">为开始</span>
        <asp:HiddenField runat="server" ID="key" />
        <asp:FileUpload ID="fileSql" runat="server" />
        <asp:Button ID="btnGoInstal" runat="server" OnClick="btnGoInstal_Click" Text="开始安装"  /><br />
        <asp:Button ID="btnClearTree" runat="server" OnClick="btnClearTree_Click" Text="删除多余数据"  /></div>
       
	

    </form>
</body>
</html>
