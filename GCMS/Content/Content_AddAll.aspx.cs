using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using GCMS.PageCommonClassLib;

public partial class Content_Content_AddAll : GCMS.PageCommonClassLib.PageBase
{
    //订阅页面的自定义事件
    protected override void OnPreInit(EventArgs e)
    {
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);//注册验证错误处理
        base.OnPreInit(e);
    }

    /// <summary>
    /// 验证失败事件响应
    /// </summary>
    void OnSessionAtuhFaiedEvent()
    {
        GSystem.SystemState = EnumTypes.SystemStates.Overtime;
        this.Response.Write("<script language=javascript>alert(\"超时操作！！！\");parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(GetHtml());
    }

    private string GetHtml()
    {
        StringBuilder sb=new StringBuilder();
        //================================================================
        //关于完成此内容框架集的说明
        //1. 为“contents”框架添加 src=\"\" 页的 URL。
        //2. 为“main”框架添加 src=\"\" 页的 URL。
        //3. 将 BASE target=\"main\" 元素添加到“contents”页的 
        //    HEAD，以将“main”设置为默认框架，“contents”页的链接将
        //    在该框架中显示其他页。
        //================================================================
        string flag = string.Empty;
        if (Request.QueryString["flag"] != null)
        {
            flag = Request.QueryString["flag"].ToString();
        }
        string Content_ID = string.Empty;
        if (Request.QueryString["Content_ID"] != null)
        {
            flag = Request.QueryString["Content_ID"].ToString();
        }
        string TypeTree_ID = string.Empty;
        if (Request.QueryString["TypeTree_ID"] != null)
        {
            flag = Request.QueryString["TypeTree_ID"].ToString();
        }
        sb.AppendLine("<frameset cols=\"200,*\" bordercolor=\"scrollbar\" id=\"Mainframe\">");
        sb.AppendLine("<frame src=\"Content_AddMain.aspx?flag=" + flag + "&TypeTree_ID=" + TypeTree_ID + "&Content_ID=" + Content_ID + "\" id=\"TypeTree\" scrolling=\"no\">");
        sb.AppendLine("<frame src=\"Content_Relative.aspx?flag=" + flag + "&TypeTree_ID=" + TypeTree_ID + "&Content_ID=" + Content_ID + "\" id=\"Main_List\" scrolling=\"no\" frameborder=\"no\" name=\"Main_Type\">");
		sb.AppendLine(	"<noframes>");
		sb.AppendLine(	"<pre id=\"p2\">");
		sb.AppendLine(	"</pre>");	
        sb.AppendLine(	"<p id=\"p1\">");	     
	    sb.AppendLine(	"此 HTML 框架集显示多个 Web 页。若要查看此框架集，请使用支持 HTML 4.0 及更高版本的 Web 浏览器。");
		sb.AppendLine(	"</p>");	     
		sb.AppendLine(	"</noframes>");
        sb.AppendLine("</frameset>");
        return sb.ToString();
    }
}
