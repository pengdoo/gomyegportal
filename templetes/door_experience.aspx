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
                                <a href="/door/default.aspx">��ҳ</a> &gt <span>��Ʒ����</span>
                            </div>
                            <div class="sitemapright">&nbsp;</div>
                        </div>
                        <div class="clear"></div>
                        <!--IE7Ҫ���5px-->
                        <div class="content-width">
                            <!--%
'�����б�
For each Item In GCMS.Channels
GCMS.ContentID = Item.ContentID
%-->
                            <div class="newslist">
								<h3 class="newstitle">
								    [ <a href="<!--%Response.Output GCMS.Content("Url")%-->"><!--%Response.Output GCMS.Content("Name")%--></a> ]
								</h3>
								<div class="posttime">
								    <a href="<!--%Response.Output GCMS.Content("Url")%-->"><img src="/images/more.gif" width="36px" height="12px" /></a>
								    <span>�������ڣ�<!--%Response.Output GCMS.Content("SubmitDate")%--></span>
								    <span>���ߣ�<!--%Response.Output GCMS.Content("Original")%--></span>
								</div>
								<div class="newsbody">
									<!--%Response.Output GCMS.Content("Explain")%-->
								</div>
                            </div>
<!--% Next %-->
                        </div>
                        <div class="pager">
<!--%Response.Output "<span>��" & GCMS.AllCount & "����¼</span><span>��" & GCMS.PgCount & "ҳ</span><span>��ǰ��<label>" & GCMS.PgThis & "</label>ҳ</span>"%-->
                            <span class="pagerNum">
                            <!--%if GCMS.PGUP <> "" then Response.Output "<a href='"&GCMS.PGUP&"'>��һҳ</a>" else Response.Output "��һҳ"%-->
							<!--%	For i =1 to GCMS.PgCount
							If i = GCMS.PgThis then
							Response.Output "&nbsp;<span>" & i & "</span>"
							Else
							Response.Output "&nbsp;<a href=" & GCMS.ThisChannelUrl(i) & ">" & i & "</a>"
							End if
							Next
							%-->
                            <!--%if GCMS.PGDN <> "" then Response.Output "<a href='"&GCMS.PGDN&"'>��һҳ</a>" else Response.Output "��һҳ"%-->
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div id="sidebar">
                <div id="innersidebar">
                    <!--���-->
                    <div class="submenu">
                        <div class="submenutitle">
                            <span class="sidenum">02</span>
                            <span class="sidetitle">��Ʒ����</span>
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
