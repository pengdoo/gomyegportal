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
        <!--#include virtual="/group/header.aspx"-->
        <div id="subbody">
            <div id="mainContent">
                <div id="innersubContent">
                    <div class="contentmain">
                        <div class="contenttitle">
                            <div class="sitemap">
                                <div class="sitemap_img"></div>
                                ����λ�� �� 
                                <a href="/group/default.aspx">��ҳ</a> &gt <span>���׶�̬</span>
                            </div>
                            <div class="sitemapright">&nbsp;</div>
                        </div>
                        <div class="clear"></div>
                        <!--IE7Ҫ���5px-->
                        <div class="content-width">                           
                            <div class="newslist">
								<h3 class="newstitle">
								    [ <a href="<!--%Response.Output GCMS.Content("Url")%-->"><!--%Response.Output GCMS.Content("Name")%--></a> ]
								</h3>
								<div class="posttime">
								    <!--<a href="javascript:window.history.back();"><img src="/images/more.gif" width="36px" height="12px" /></a>-->
								    <span>�������ڣ�<!--%Response.Output GCMS.Content("SubmitDate")%--></span>
								    <span>���ߣ�<!--%Response.Output GCMS.Content("Original")%--></span>
								</div>
								<div class="newsbody">
									<!--%Response.Output GCMS.Content("Content")%-->
								</div>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
            <div id="sidebar">
                <div id="innersidebar">
                    <!--���-->
                    <div class="submenu">
                        <div class="submenutitle">
                            <span class="sidenum">06</span>
                            <span class="sidetitle">���׶�̬</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
        <!--#include virtual="/footer.aspx"-->
    </div>
</body>
</html>
