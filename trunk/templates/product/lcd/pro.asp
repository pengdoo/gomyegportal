<% response.CharSet = "gb2312"%>
<!--%
'头条新闻
Gcms.GetChannel = GCMS.ChannelID
For Each Item In GCMS.Top(2000)
GCMS.ContentID = Item.ContentID
%-->
<products>
	<product src="<!--%Response.Output GCMS.Channel("Images")%-->" id="<!--%
Response.Output GCMS.Content("ContentID")
%-->" href="<!--%Response.Output GCMS.Content("Url")%-->">
		<size><!--%
Response.Output GCMS.Content("screensize")
%--></size>
		<resolution><!--%
Response.Output GCMS.Content("Maxresolution")
%--></resolution>
		<name><!--%
Response.Output GCMS.Content("productcode")
%--></name>
		<description></description>
	</product>
	<!--%
next
Gcms.GetChannelOver
%-->
</products>
