<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Type_Delete.aspx.cs" Inherits="Content_Type_Delete" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>TypeTree_delete</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script language=javascript>
		function ReloadWindow()
		{			
			top.location.reload();
		}
		function NoPermitDelete(TypeTree_ID)
		{
			alert("抱歉呀，该目录中包含子目录，请您先删除其子目录哦！");
			window.location.href = "TypeView.aspx?TypeTree_ID=" + TypeTree_ID;
		}
    </script>
  </HEAD>
  <body MS_POSITIONING="GridLayout">
	
    <form id="Form1" method="post" runat="server"><FONT face=宋体></FONT>

     </form>
	
  </body>
</HTML>
