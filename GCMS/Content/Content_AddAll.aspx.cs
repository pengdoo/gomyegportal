//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-26
//
// 功能描述: 相关文章设置(未完成)
//
// 已修改问题:
// 未修改问题:
// 修改记录
//   2008-8-26 添加注释
//   2008-8-31  规范【自定义事件】【SQL引用】【字符处理】【页面参数获取】代码
//----------------------------------系统引用-------------------------------------
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
//----------------------------------项目引用-----------------------------------
using GCMS.PageCommonClassLib;
//------------------------------------------------------------------------------

public partial class Content_Content_AddAll : GCMS.PageCommonClassLib.PageBase
{
    #region 自定义事件的注册和处理
    //订阅页面的自定义事件
    protected override void OnPreInit(EventArgs e)
    {
        //用户验证事件注册
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);
        //Session或QueryString获取失败事件注册
        this.SessionOrQueryGetFaiedEvent += new ParameterAuthHandler(OnSessionOrQueryGetFaiedEvent);
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
    /// <summary>
    /// Session或Query的访问失败默认响应
    /// </summary>
    /// <param name="key"></param>
    void OnSessionOrQueryGetFaiedEvent(string key)
    {
        //#未完成代码#
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(GetHtml());
    }
    #endregion 自定义事件的注册和处理

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
        string flag = this.GetQueryString("flag",null);
        
        string Content_ID = this.GetQueryString("Content_ID", null);
        
        string TypeTree_ID =TypeTree_ID = this.GetQueryString("TypeTree_ID", null);
        
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
