<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="TestPage" %>

<%@ Register Src="Gomye_Tools/Tools_TreeMenu.ascx" TagName="Tools_TreeMenu" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="js/jquery.treeview/jquery.treeview.css" />
    <script type="text/javascript" src="js/jquery/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" src="js/jquery.treeview/jquery.treeview.min.js"></script>
    <script type="text/javascript">
	$(document).ready(function(){
		$("#treemenu").treeview();
			});
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Tools_TreeMenu id="MainMenu" runat="server">
        </uc1:Tools_TreeMenu></div>
    </form>
</body>
</html>
