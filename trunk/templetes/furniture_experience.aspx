<!--#include virtual="/config.aspx"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>华鹤集团</title>
	<meta name="Keywords" content="<%=Keywords%>" />
	<meta name="Description" content="<%=Description%>" />
    <script type="text/javascript" src="/scripts/jquery-1.2.6.min.js"></script>    
    <script type="text/javascript" src="/scripts/swfobject.js"></script>
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
	<script>
	function openit(id){
		 $.ajax({
			url: '/html/'+id+'/.aspx',
			type: 'GET',
			dataType: 'xml',
			timeout: 1000,
			error: function(){
			alert('Error loading XML document');
			},
			success: function(xml){
				alert(xml);
			}
		});
	}
	</script>
</head>
<body>
    <div id="container">
        <!--#include virtual="/furniture/header.aspx"-->
        <div id="subbody">
            <div id="mainContent">
                <div id="innersubContent">
                    <div class="contentmain">
                        <div class="contenttitle">
                            <div class="sitemap">
                                <div class="sitemap_img"></div>
                                您的位置 ： 
                                <a href="/furniture/default.aspx">首页</a> &gt 产品体验 &gt <span><!--% Response.Output GCMS.Channel("CName")%--></span>
                            </div>
                            <div class="sitemapright">&nbsp;</div>
                        </div>
                        <div class="clear"></div>
                        <!--IE7要多出5px-->
                        <div class="content-width">
                            <div class="sort"><span><!--% Response.Output GCMS.Channel("CName") %--></span> | 
				<a href="http://125.64.92.90:8085/experience.aspx?ClassID=<!--% Response.Output GCMS.Channel("EName") %-->">产品目录</a>  | 
				<a href="javascript:void(0);">推荐给好友</a>
				</div>
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
                    </div>
                </div>
            </div>
            <div id="sidebar">
                <div id="innersidebar">
                    <!--侧边-->
                    <div class="submenu">
                        <div class="submenutitle">
                            <span class="sidenum">02</span>
                            <span class="sidetitle">产品体验</span>
                        </div>
						<ul>
<!--%
i = GCMS.Channel("ChannelID")
For Each Channels In GCMS.GetChannels(19)
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
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
        <!--#include virtual="/footer.aspx"-->
    </div>
</body>
</html>
