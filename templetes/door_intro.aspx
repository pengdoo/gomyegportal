<!--#include virtual="/config.aspx"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>���׼���</title>
	<meta name="Keywords" content="<%=Keywords%>" />
	<meta name="Description" content="<%=Description%>" />
    <script type="text/javascript" src="/scripts/jquery-1.2.6.min.js"></script>
    <script type="text/javascript" src="/scripts/swfobject.js"></script>
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
</head>
<body>
    <div id="container">
        <!--#include virtual="/door/header.aspx"-->
        <div id="subbody">
            <div id="mainContent">
                <div id="innersubContent">
                    <div class="contentmain">
                        <div class="contenttitle">
                            <div class="sitemap">
                                <div class="sitemap_img"></div>
                                ����λ�� �� 
                                <a href="/door/default.aspx">��ҳ</a> &gt <a href="/door/intro.aspx">��˾����</a> &gt <span><!--% Response.Output GCMS.Channel("CName")%--></span>
                            </div>
                            <div class="sitemapright">&nbsp;</div>
                        </div>
                        <div class="clear"></div>
                        <!--IE7Ҫ���5px-->
                        <div class="content-width">
                            <!--����-->
                            <!--%
							Gcms.GetChannel = GCMS.Channel("ChannelID")
							For Each Item In GCMS.Top(1)
							GCMS.ContentID = Item.ContentID
							%-->
							<!--#include virtual="/html/<!--% Response.Output GCMS.Content("ContentID") %-->.aspx"-->
							<!--%
							next
							GCMS.GetChannelOver()
							%-->
                        </div>
                        <div class="pager">
                            
                        </div>
                    </div>
                </div>
            </div>
            <div id="sidebar">
                <div id="innersidebar">
                    <!--���-->
                    <div class="submenu">
                        <div class="submenutitle">
                            <span class="sidenum">01</span>
                            <span class="sidetitle">��˾����</span>
                        </div>
                        <ul><!--%
i = GCMS.Channel("ChannelID")
For Each Channels In GCMS.GetChannels(25)
GCMS.ChannelID = Channels.ChannelID
if GCMS.ChannelID = i then
%-->
<li class="select"><span><!--% Response.Output GCMS.Channel("CName") %--></span></li>
<!--%else%-->
<li><a href="<!--% Response.Output GCMS.Channel("Url") %-->"><!--% Response.Output GCMS.Channel("CName") %--></a></li>
<!--%
end if
next
GCMS.ChannelID=i
%-->
                        </ul>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
        <!--#include virtual="/footer.aspx"-->
    </div>
</body>
</html>